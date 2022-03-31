using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using Ferret.OutGame.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.OutGame.Presentation.Controller
{
    public sealed class OutGameController : IPostInitializable, IDisposable
    {
        private readonly IBgmController _bgmController;
        private readonly ISeController _seController;
        private readonly SceneLoader _sceneLoader;
        private readonly ResultController _resultController;
        private readonly InputView _inputView;

        private readonly CancellationTokenSource _tokenSource;

        public OutGameController(IBgmController bgmController, ISeController seController, SceneLoader sceneLoader,
            ResultController resultController, InputView inputView)
        {
            _bgmController = bgmController;
            _seController = seController;
            _sceneLoader = sceneLoader;
            _resultController = resultController;
            _inputView = inputView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);
            }

            InitAsync(_tokenSource.Token).Forget();
        }

        private async UniTask InitAsync(CancellationToken token)
        {
            await _resultController.InitViewAsync(token);

            _bgmController.Play(BgmType.Result, true);

            await _sceneLoader.FadeOutAsync(token);

            await _inputView.OnClickAsync(token);

            _sceneLoader.FadeLoadScene(SceneName.Main);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}