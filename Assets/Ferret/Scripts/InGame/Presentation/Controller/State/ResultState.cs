using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseGameState
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly IBgmController _bgmController;
        private readonly SceneLoader _sceneLoader;
        private readonly InputView _inputView;
        private readonly ResultView _resultView;

        public ResultState(UserRecordUseCase userRecordUseCase, IBgmController bgmController, SceneLoader sceneLoader,
            InputView inputView, ResultView resultView)
        {
            _userRecordUseCase = userRecordUseCase;
            _bgmController = bgmController;
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

            _bgmController.Stop();

            await _sceneLoader.LoadingSceneAsync(SceneName.Ranking, _userRecordUseCase.UpdateScoreAsync(token), token);

            return GameState.None;
        }
    }
}