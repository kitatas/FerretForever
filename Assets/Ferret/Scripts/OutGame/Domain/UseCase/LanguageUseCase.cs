using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;

namespace Ferret.OutGame.Domain.UseCase
{
    public sealed class LanguageUseCase
    {
        private readonly LanguageRepository _languageRepository;
        private readonly SaveDataRepository _saveDataRepository;

        public LanguageUseCase(LanguageRepository languageRepository, SaveDataRepository saveDataRepository)
        {
            _languageRepository = languageRepository;
            _saveDataRepository = saveDataRepository;
        }

        public ResultScene GetResultSceneData()
        {
            var type = _saveDataRepository.Load().language;
            return _languageRepository.FindJsonData(type).result;
        }
    }
}