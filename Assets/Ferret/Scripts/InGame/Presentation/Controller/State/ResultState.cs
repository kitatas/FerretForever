using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseGameState
    {
        private readonly InputView _inputView;
        private readonly ResultView _resultView;

        public ResultState(InputView inputView, ResultView resultView)
        {
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

            return GameState.None;
        }
    }
}