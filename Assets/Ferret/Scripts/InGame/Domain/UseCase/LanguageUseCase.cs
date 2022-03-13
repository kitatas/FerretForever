using Ferret.Common;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;
using UnityEngine;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class LanguageUseCase
    {
        private readonly LanguageRepository _languageRepository;

        public LanguageUseCase(LanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public (MainScene, Sprite) FindData(LanguageType type)
        {
            return (
                _languageRepository.FindJsonData(type).main,
                _languageRepository.GetHintImage(type)
            );
        }
    }
}