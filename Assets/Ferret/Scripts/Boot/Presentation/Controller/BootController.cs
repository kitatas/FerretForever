using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Boot.Domain.UseCase;
using Ferret.Boot.Presentation.View;
using Ferret.Common;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.Boot.Presentation.Controller
{
    public sealed class BootController : IPostInitializable, IDisposable
    {
        private readonly LanguageUseCase _languageUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly SaveDataUseCase _saveDataUseCase;
        private readonly LoadingView _loadingView;
        private readonly LanguageView _languageView;
        private readonly LanguageSelectView _languageSelectView;
        private readonly NameRegistrationView _nameRegistrationView;
        private readonly IBgmController _bgmController;
        private readonly ISeController _seController;
        private readonly ErrorController _errorController;
        private readonly SceneLoader _sceneLoader;
        private readonly CancellationTokenSource _tokenSource;

        public BootController(LanguageUseCase languageUseCase, LoginUseCase loginUseCase, SaveDataUseCase saveDataUseCase,
            LoadingView loadingView, LanguageView languageView, LanguageSelectView languageSelectView, NameRegistrationView nameRegistrationView,
            IBgmController bgmController, ISeController seController, ErrorController errorController, SceneLoader sceneLoader)
        {
            _languageUseCase = languageUseCase;
            _loginUseCase = loginUseCase;
            _saveDataUseCase = saveDataUseCase;
            _loadingView = loadingView;
            _languageView = languageView;
            _languageSelectView = languageSelectView;
            _nameRegistrationView = nameRegistrationView;
            _bgmController = bgmController;
            _seController = seController;
            _errorController = errorController;
            _sceneLoader = sceneLoader;
            _tokenSource = new CancellationTokenSource();

            _loadingView.Activate(false);
        }

        public void PostInitialize()
        {
            _languageSelectView.Init();
            _nameRegistrationView.Init();
            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);
            }

            BootAsync(_tokenSource.Token).Forget();
        }

        private async UniTask BootAsync(CancellationToken token)
        {
            try
            {
                _loadingView.Activate(true);

                var response = await _loginUseCase.LoginAsync(token);

                await (
                    _bgmController.InitAsync(token),
                    _seController.InitAsync(token)
                );

                _loadingView.Activate(false);

                // 既存ユーザーの場合
                if (_loginUseCase.SyncUserRecord(response))
                {

                }
                // 新規ユーザーの場合
                else
                {
                    // 言語選択
                    var language = await _languageSelectView.DecisionLanguageAsync(token);
                    _saveDataUseCase.SaveLanguage(language);
                    _languageView.SetTextData(_languageUseCase.FindBootData(language));

                    // 名前入力
                    await CheckNameAsync(token);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

                _sceneLoader.LoadScene(SceneName.Main);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning($"{e}");
                await _errorController.PopupErrorAsync(e, token);

                await BootAsync(token);
            }
        }

        private async UniTask CheckNameAsync(CancellationToken token)
        {
            while (true)
            {
                // 入力完了待ち
                await _nameRegistrationView.DecisionNameAsync(token);
                _loadingView.Activate(true);

                // 名前登録
                var isSuccess = await _loginUseCase.RegisterUserNameAsync(_nameRegistrationView.inputName, token);
                _loadingView.Activate(false);

                if (isSuccess)
                {
                    break;
                }

                await _errorController.PopupRegistrationErrorAsync(token);
                _nameRegistrationView.ResetName();
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}