using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace Ferret.Common.Domain.Repository
{
    public sealed class PlayFabRepository
    {
        public PlayFabRepository()
        {
            PlayFabSettings.staticSettings.TitleId = MasterConfig.TITLE_ID;
        }

        public async UniTask<(LoginResult, string)> CreateUserDataAsync(CancellationToken token)
        {
            while (true)
            {
                var uid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

                var response = await LoadUserDataAsync(uid, token);

                // 既にIDが存在している場合
                if (response.LastLoginTime.HasValue)
                {
                    continue;
                }

                return (response, uid);
            }
        }

        public async UniTask<LoginResult> LoadUserDataAsync(string uid, CancellationToken token)
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