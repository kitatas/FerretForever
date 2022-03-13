using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class LanguagePresenter : IPostInitializable
    {
        private readonly LanguageTypeUseCase _languageTypeUseCase;
        private readonly LanguageUseCase _languageUseCase;
        private readonly LanguageView _languageView;

        public LanguagePresenter(LanguageTypeUseCase languageTypeUseCase, LanguageUseCase languageUseCase,
            LanguageView languageView)
        {
            _languageTypeUseCase = languageTypeUseCase;
            _languageUseCase = languageUseCase;
            _languageView = languageView;
        }

        public void PostInitialize()
        {
            _languageTypeUseCase.language
                .Subscribe(x =>
                {
                    var (mainSceneData, hintImage) = _languageUseCase.FindData(x);
                    _languageView.Display(mainSceneData);
                    _languageView.SetHint(hintImage);
                })
                .AddTo(_languageView);
        }
    }
}