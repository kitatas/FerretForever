using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseGameState
    {
        private readonly InputView _inputView;
        private readonly TitleView _titleView;

        public TitleState(InputView inputView, TitleView titleView)
        {
            _inputView = inputView;
            _titleView = titleView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _inputView.OnPush().ToUniTask(true, token);

            _titleView.SetUp();

            return GameState.Main;
        }
    }
}