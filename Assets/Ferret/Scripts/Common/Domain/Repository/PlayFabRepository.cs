using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Data.DataStore;
using Newtonsoft.Json;
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
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true,
                    GetPlayerStatistics = true,
                    GetTitleData = true,
                    GetUserAccountInfo = true,
                    GetUserData = true,
                    GetUserInventory = true,
                    GetUserVirtualCurrency = true,
                }
            };

            var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
            if (response.Error != null)
            {
                throw new Exception($"{response.Error.GenerateErrorReport()}");
            }

            return response.Result;
        }

        public UserRecord FetchUserRecord(Dictionary<string, UserData> userData)
        {
            return userData.TryGetValue(MasterConfig.USER_KEY, out var user)
                ? JsonConvert.DeserializeObject<UserRecord>(user.Value)
                : new UserRecord();
        }

        public async UniTask<UpdateUserDataResult> UpdateUserRecordAsync(UserRecord userRecord, CancellationToken token)
        {
            var jsonData = JsonConvert.SerializeObject(userRecord);
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { MasterConfig.USER_KEY, jsonData },
                },
            };

            var response = await PlayFabClientAPI.UpdateUserDataAsync(request);
            if (response.Error != null)
            {
                throw new Exception($"{response.Error.GenerateErrorReport()}");
            }

            return response.Result;
        }
    }
}