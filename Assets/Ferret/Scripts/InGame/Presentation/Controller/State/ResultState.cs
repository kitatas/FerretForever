using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseGameState
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly SceneLoader _sceneLoader;
        private readonly InputView _inputView;
        private readonly ResultView _resultView;

        public ResultState(UserRecordUseCase userRecordUseCase, SceneLoader sceneLoader, InputView inputView,
            ResultView resultView)
        {
            _userRecordUseCase = userRecordUseCase;
            _sceneLoader = sceneLoader;
            _inputView = inputView;
            _resultView = resultView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _resultView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _resultView.SetUpAsync(token);

            await _inputView.OnPush().ToUniTask(true, token);

            _sceneLoader.LoadingScene(
                SceneName.Ranking,
                _userRecordUseCase.UpdateRecordAsync(token)
            );

            return GameState.None;
        }
    }
}