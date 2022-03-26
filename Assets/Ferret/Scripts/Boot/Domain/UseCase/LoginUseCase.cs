using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Data.Entity;
using Ferret.Common.Domain.Repository;
using PlayFab.ClientModels;

namespace Ferret.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly UserRecordEntity _userRecordEntity;
        private readonly AchievementMasterEntity _achievementMasterEntity;
        private readonly PlayFabRepository _playFabRepository;

        public LoginUseCase(UserRecordEntity userRecordEntity, AchievementMasterEntity achievementMasterEntity, PlayFabRepository playFabRepository)
        {
            _userRecordEntity = userRecordEntity;
            _achievementMasterEntity = achievementMasterEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask<bool> IsLoginAsync(string uid, CancellationToken token)
        {
            var response = await _playFabRepository.LoadUserDataAsync(uid, token);
            FetchData(response);

            return _userRecordEntity.IsSync();
        }

        public async UniTask<string> CreateUidAsync(CancellationToken token)
        {
            var (response, uid) = await _playFabRepository.CreateUserDataAsync(token);
            FetchData(response);

            return uid;
        }

        private void FetchData(LoginResult response)
        {
            if (response.InfoResultPayload == null)
            {
                throw new Exception($"response.InfoResultPayload is null.");
            }

            FetchMasterData(response);
            FetchUserData(response);
        }

        private void FetchMasterData(LoginResult response)
        {
            var achievementMaster = _playFabRepository.FetchAchievementMaster(response.InfoResultPayload.TitleData);
            _achievementMasterEntity.Set(achievementMaster);
        }

        private void FetchUserData(LoginResult response)
        {
            var userRecord = _playFabRepository.FetchUserRecord(response.InfoResultPayload.UserData);
            if (string.IsNullOrEmpty(userRecord.uid))
            {
                userRecord.uid = response.PlayFabId;
            }
            _userRecordEntity.Set(userRecord);
        }

        public async UniTask<bool> RegisterUserNameAsync(string userName, CancellationToken token)
        {
            var isSuccess = await _playFabRepository.UpdateDisplayNameAsync(userName, token);
            if (isSuccess == false)
            {
                return false;
            }

            _userRecordEntity.UpdateName(userName);
            await _playFabRepository.UpdateUserRecordAsync(_userRecordEntity.Get(), token);
            return true;
        }
    }
}