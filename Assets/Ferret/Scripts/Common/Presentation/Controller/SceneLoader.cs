using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Presentation.View;
using UnityEngine.SceneManagement;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class SceneLoader : IDisposable
    {
        private readonly TransitionMaskView _transitionMaskView;
        private readonly CancellationTokenSource _tokenSource;

        public SceneLoader(TransitionMaskView transitionMaskView)
        {
            _transitionMaskView = transitionMaskView;
            _tokenSource = new CancellationTokenSource();
            _transitionMaskView.Init();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public void FadeLoadScene(SceneName sceneName)
        {
            FadeLoadSceneAsync(sceneName, _tokenSource.Token).Forget();
        }

        public async UniTask FadeLoadSceneAsync(SceneName sceneName, CancellationToken token)
        {
            await FadeInAsync(token);

            await LoadSceneAsync(sceneName, token);

            await FadeOutAsync(token);
        }

        public void LoadScene(SceneName sceneName)
        {
            LoadSceneAsync(sceneName, _tokenSource.Token).Forget();
        }

        public async UniTask FadeInAsync(CancellationToken token)
        {
            await _transitionMaskView.FadeInAsync(token);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        }

        public async UniTask LoadSceneAsync(SceneName sceneName, CancellationToken token)
        {
            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
        }

        public async UniTask FadeOutAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            await _transitionMaskView.FadeOutAsync(token);
        }
    }
}