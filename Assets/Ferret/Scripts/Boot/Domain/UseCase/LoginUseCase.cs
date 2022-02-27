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
        private readonly SaveDataRepository _saveDataRepository;
        private readonly PlayFabRepository _playFabRepository;

        public LoginUseCase(UserRecordEntity userRecordEntity, AchievementMasterEntity achievementMasterEntity, SaveDataRepository saveDataRepository, PlayFabRepository playFabRepository)
        {
            _userRecordEntity = userRecordEntity;
            _achievementMasterEntity = achievementMasterEntity;
            _saveDataRepository = saveDataRepository;
            _playFabRepository = playFabRepository;
        }

        public async UniTask<LoginResult> LoginAsync(CancellationToken token)
        {
            var saveData = _saveDataRepository.Load();
            if (string.IsNullOrEmpty(saveData.uid))
            {
                var (response, uid) = await _playFabRepository.CreateUserDataAsync(token);

                saveData.uid = uid;
                _saveDataRepository.Save(saveData);

                return response;
            }
            else
            {
                return await _playFabRepository.LoadUserDataAsync(saveData.uid, token);
            }
        }

        public bool SyncUserRecord(LoginResult response)
        {
            if (response.InfoResultPayload == null)
            {
                throw new Exception($"response.InfoResultPayload is null.");
            }

            var userRecord = _playFabRepository.FetchUserRecord(response.InfoResultPayload.UserData);
            if (string.IsNullOrEmpty(userRecord.uid))
            {
                userRecord.uid = response.PlayFabId;
            }
            _userRecordEntity.Set(userRecord);

            var achievementMaster = _playFabRepository.FetchAchievementMaster(response.InfoResultPayload.TitleData);
            _achievementMasterEntity.Set(achievementMaster);

            return _userRecordEntity.IsSync();
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