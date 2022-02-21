using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Presentation.View;
using UnityEngine.SceneManagement;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class SceneLoader : IDisposable
    {
        private readonly LoadingView _loadingView;
        private readonly TransitionMaskView _transitionMaskView;
        private readonly CancellationTokenSource _tokenSource;

        public SceneLoader(LoadingView loadingView, TransitionMaskView transitionMaskView)
        {
            _loadingView = loadingView;
            _transitionMaskView = transitionMaskView;
            _tokenSource = new CancellationTokenSource();

            _loadingView.Activate(false);
            _transitionMaskView.Init();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public void LoadScene(SceneName sceneName)
        {
            LoadSceneAsync(sceneName, _tokenSource.Token).Forget();
        }

        private async UniTask LoadSceneAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionMaskView.FadeInAsync(token);

            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            await _transitionMaskView.FadeOutAsync(token);
        }

        public async UniTask LoadingSceneAsync(SceneName sceneName, UniTask loadTask, CancellationToken token)
        {
            await _transitionMaskView.FadeInAsync(token);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            _loadingView.Activate(true);
            await loadTask;

            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
        }

        public void LoadingFadeOut()
        {
            LoadingFadeOutAsync(_tokenSource.Token).Forget();
        }

        private async UniTask LoadingFadeOutAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            _loadingView.Activate(false);
            await _transitionMaskView.FadeOutAsync(token);
        }
    }
}