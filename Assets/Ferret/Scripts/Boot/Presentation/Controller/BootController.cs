using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Boot.Domain.UseCase;
using Ferret.Boot.Presentation.View;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.Boot.Presentation.Controller
{
    public sealed class BootController : IPostInitializable, IDisposable
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly LoadingView _loadingView;
        private readonly ErrorPopupView _errorPopupView;
        private readonly NameRegistrationView _nameRegistrationView;
        private readonly SceneLoader _sceneLoader;
        private readonly CancellationTokenSource _tokenSource;

        public BootController(LoginUseCase loginUseCase, LoadingView loadingView, ErrorPopupView errorPopupView,
            NameRegistrationView nameRegistrationView, SceneLoader sceneLoader)
        {
            _loginUseCase = loginUseCase;
            _loadingView = loadingView;
            _errorPopupView = errorPopupView;
            _nameRegistrationView = nameRegistrationView;
            _sceneLoader = sceneLoader;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _errorPopupView.Init();
            _nameRegistrationView.Init();
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.Init();
            }

            Boot();
        }

        private void Boot()
        {
            try
            {
                Load();
            }
            catch (Exception e)
            {
                Boot();
                throw;
            }
        }

        private void Load()
        {
            UniTask.Void(async _ =>
            {
                _loadingView.Activate(true);

                var response = await _loginUseCase.LoginAsync(_tokenSource.Token);
                _loadingView.Activate(false);

                // 既存ユーザーの場合
                if (_loginUseCase.SyncUserRecord(response))
                {

                }
                // 新規ユーザーの場合
                else
                {
                    await CheckNameAsync(_tokenSource.Token);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: _tokenSource.Token);

                _sceneLoader.LoadScene(SceneName.Main);

            }, _tokenSource.Token);
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

                await _errorPopupView.PopupAsync(token);
                _nameRegistrationView.ResetName();
            }
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource?.Dispose();
        }
    }
}