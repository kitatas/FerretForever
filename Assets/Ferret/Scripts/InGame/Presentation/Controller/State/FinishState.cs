using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class FinishState : BaseGameState
    {
        private readonly IBgmController _bgmController;
        private readonly InputView _inputView;
        private readonly ResultView _resultView;

        public FinishState(IBgmController bgmController, InputView inputView, ResultView resultView)
        {
            _bgmController = bgmController;
            _inputView = inputView;
            _resultView = resultView;
        }

        public override GameState state => GameState.Finish;

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

            return GameState.Result;
        }
    }
}