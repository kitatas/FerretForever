using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;
using UnityEngine;

namespace Ferret.Common.Domain.UseCase
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

        public (MainScene, Sprite) FindMainData(LanguageType type)
        {
            return (
                _languageRepository.FindJsonData(type).main,
                _languageRepository.GetHintImage(type)
            );
        }

        public ResultScene FindResultScene(LanguageType type)
        {
            return _languageRepository.FindJsonData(type).result;
        }
        
        public CommonError FindCommonError(LanguageType type)
        {
            return _languageRepository.FindJsonData(type).error;
        }
    }
}