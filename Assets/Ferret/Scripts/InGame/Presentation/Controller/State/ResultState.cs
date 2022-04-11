using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using Ferret.InGame.Domain.UseCase;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseGameState
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly LoadingView _loadingView;
        private readonly SceneLoader _sceneLoader;

        public ResultState(UserRecordUseCase userRecordUseCase, LoadingView loadingView, SceneLoader sceneLoader)
        {
            _userRecordUseCase = userRecordUseCase;
            _loadingView = loadingView;
            _sceneLoader = sceneLoader;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _sceneLoader.FadeInAsync(token);

            _loadingView.Activate(true);

            await _userRecordUseCase.SendScoreAsync(token);

            _sceneLoader.LoadScene(SceneName.Ranking);

            return GameState.None;
        }
    }
}