using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.InGame.Domain.UseCase;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseGameState
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly SceneLoader _sceneLoader;

        public ResultState(UserRecordUseCase userRecordUseCase, SceneLoader sceneLoader)
        {
            _userRecordUseCase = userRecordUseCase;
            _sceneLoader = sceneLoader;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _sceneLoader.LoadingSceneAsync(SceneName.Ranking, _userRecordUseCase.UpdateScoreAsync(token), token);

            return GameState.None;
        }
    }
}