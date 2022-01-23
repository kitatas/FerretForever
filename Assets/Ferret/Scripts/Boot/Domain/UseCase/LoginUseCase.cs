using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Domain.Repository;
using PlayFab;
using PlayFab.ClientModels;

namespace Ferret.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly SaveDataRepository _saveDataRepository;

        public LoginUseCase(SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
        }

        public async UniTask<LoginResult> LoginAsync(CancellationToken token)
        {
            PlayFabSettings.staticSettings.TitleId = MasterConfig.TITLE_ID;

            var saveData = _saveDataRepository.Load();
            if (string.IsNullOrEmpty(saveData.uid))
            {
                var (response, uid) = await CreateUserDataAsync(token);

                saveData.uid = uid;
                _saveDataRepository.Save(saveData);

                return response;
            }
            else
            {
                return await LoadUserDataAsync(saveData.uid, token);
            }
        }

        private static async UniTask<(LoginResult, string)> CreateUserDataAsync(CancellationToken token)
        {
            while (true)
            {
                var uid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

                var request = new LoginWithCustomIDRequest
                {
                    CustomId = uid,
                    CreateAccount = true,
                };

                // ログイン
                var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
                if (response.Error != null)
                {
                    throw new Exception($"{response.Error.GenerateErrorReport()}");
                }

                // 既にIDが存在している場合
                if (response.Result.LastLoginTime.HasValue)
                {
                    continue;
                }

                return (response.Result, uid);
            }
        }

        private static async UniTask<LoginResult> LoadUserDataAsync(string uid, CancellationToken token)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = uid,
                CreateAccount = true,
            };

            var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
            if (response.Error != null)
            {
                throw new Exception($"{response.Error.GenerateErrorReport()}");
            }

            return response.Result;
        }
    }
}