using Ferret.Common;
using UniRx;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class LanguageTypeUseCase
    {
        private readonly ReactiveProperty<LanguageType> _language;

        public LanguageTypeUseCase()
        {
            _language = new ReactiveProperty<LanguageType>(LanguageType.None);
        }

        public IReadOnlyReactiveProperty<LanguageType> language => _language;

        public void SetLanguage(LanguageType type)
        {
            _language.Value = type;
        }
    }
}