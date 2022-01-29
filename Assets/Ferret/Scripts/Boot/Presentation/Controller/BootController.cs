using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Boot.Domain.UseCase;
using Ferret.Boot.Presentation.View;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.Boot.Presentation.Controller
{
    public sealed class BootController : IPostInitializable, IDisposable
    {
        private readonly LoginUseCase _loginUseCase;

        private readonly CancellationTokenSource _tokenSource;
        private readonly LoadingView _loadingView;
        private readonly NameRegistrationView _nameRegistrationView;
        private readonly SceneLoader _sceneLoader;

        public BootController(LoginUseCase loginUseCase, LoadingView loadingView,
            NameRegistrationView nameRegistrationView, SceneLoader sceneLoader)
        {
            _loginUseCase = loginUseCase;
            _loadingView = loadingView;
            _nameRegistrationView = nameRegistrationView;
            _sceneLoader = sceneLoader;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _nameRegistrationView.Init();
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.Init();
            }

            Load();
        }

        private void Load()
        {
            try
            {
                UniTask.Void(async _ =>
                {
                    var response = await _loginUseCase.LoginAsync(_tokenSource.Token);

                    // 既存ユーザーの場合
                    if (_loginUseCase.SyncUserRecord(response))
                    {

                    }
                    // 新規ユーザーの場合
                    else
                    {
                        _loadingView.Activate(false);

                        // 入力完了待ち
                        await _nameRegistrationView.DecisionNameAsync(_tokenSource.Token);
                        _loadingView.Activate(true);

                        // 名前登録
                        await _loginUseCase.RegisterUserNameAsync(_nameRegistrationView.inputName, _tokenSource.Token);
                    }

                    await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: _tokenSource.Token);

                    _loadingView.Activate(false);
                    _sceneLoader.LoadScene(SceneName.Main);

                }, _tokenSource.Token);
            }
            catch (Exception e)
            {
                // TODO: viewに反映
                // TODO: viewから再ロード
                Debug.LogError($"{e}");
                throw;
            }
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource?.Dispose();
        }
    }
}