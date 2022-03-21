using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.View;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class ErrorController
    {
        private readonly LanguageUseCase _languageUseCase;
        private readonly SaveDataUseCase _saveDataUseCase;
        private readonly ErrorPopupView _errorPopupView;
        private readonly LoadingView _loadingView;

        public ErrorController(LanguageUseCase languageUseCase, SaveDataUseCase saveDataUseCase,
            ErrorPopupView errorPopupView, LoadingView loadingView)
        {
            _languageUseCase = languageUseCase;
            _saveDataUseCase = saveDataUseCase;
            _errorPopupView = errorPopupView;
            _loadingView = loadingView;

            _errorPopupView.Init();
        }

        public async UniTask PopupErrorAsync(Exception exception, CancellationToken token)
        {
            _loadingView.Activate(false);
            var commonError = _languageUseCase.FindCommonError(_saveDataUseCase.GetLanguageType());
            await _errorPopupView.PopupAsync($"{exception.ConvertErrorMessage(commonError)}", token);
        }

        public async UniTask PopupRegistrationErrorAsync(CancellationToken token)
        {
            _loadingView.Activate(false);
            var commonError = _languageUseCase.FindCommonError(_saveDataUseCase.GetLanguageType());
            await _errorPopupView.PopupAsync(commonError.registration, token);
        }
    }
}