using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class MainState : BaseGameState
    {
        public override GameState state => GameState.Main;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            return GameState.Result;
        }
    }
}