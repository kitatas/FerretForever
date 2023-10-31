using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;

namespace Ferret.Common.Domain.UseCase
{
    public sealed class SaveDataUseCase
    {
        private SaveData _saveData;
        private readonly SaveDataRepository _saveDataRepository;

        public SaveDataUseCase(SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            _saveData = _saveDataRepository.Load();
        }

        public bool HasUid() => string.IsNullOrEmpty(uid) == false;

        public string uid => _saveData.uid;

        public float bgmVolume => _saveData.bgmVolume;

        public float seVolume => _saveData.seVolume;

        public (float bgm, float se) volume => (bgmVolume, seVolume);

        public LanguageType languageType => _saveData.language != LanguageType.None
            ? _saveData.language
            : LanguageType.English;

        public void SaveUid(string value)
        {
            _saveData.uid = value;
            _saveDataRepository.Save(_saveData);
        }

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

        public void SaveLanguage(LanguageType type)
        {
            _saveData.language = type;
            _saveDataRepository.Save(_saveData);
        }

        public void DeleteSaveData()
        {
            _saveData = _saveDataRepository.Delete();
        }
    }
}