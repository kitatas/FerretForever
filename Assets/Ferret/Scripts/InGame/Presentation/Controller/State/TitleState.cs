using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseGameState
    {
        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            return GameState.Main;
        }
    }
}