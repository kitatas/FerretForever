using Ferret.Common;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;

namespace Ferret.Boot.Domain.UseCase
{
    public sealed class LanguageUseCase
    {
        private readonly LanguageRepository _languageRepository;

        public LanguageUseCase(LanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public BootScene FindBootData(LanguageType type)
        {
            return _languageRepository.FindJsonData(type).boot;
        }
    }
}