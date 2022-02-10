using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;

namespace Ferret.Common.Domain.UseCase
{
    public sealed class SaveDataUseCase
    {
        private readonly SaveData _saveData;
        private readonly SaveDataRepository _saveDataRepository;

        public SaveDataUseCase(SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            _saveData = _saveDataRepository.Load();
        }

        public float GetBgmVolume() => _saveData.bgmVolume;

        public float GetSeVolume() => _saveData.seVolume;

        public void SaveBgmVolume(float value)
        {
            _saveData.bgmVolume = value;
            _saveDataRepository.Save(_saveData);
        }

        public void SaveSeVolume(float value)
        {
            _saveData.seVolume = value;
            _saveDataRepository.Save(_saveData);
        }
    }
}