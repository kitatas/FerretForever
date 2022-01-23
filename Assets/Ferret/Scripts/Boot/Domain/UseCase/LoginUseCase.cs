using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Domain.Repository;
using PlayFab.ClientModels;

namespace Ferret.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly SaveDataRepository _saveDataRepository;
        private readonly PlayFabRepository _playFabRepository;

        public LoginUseCase(SaveDataRepository saveDataRepository, PlayFabRepository playFabRepository)
        {
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
    }
}