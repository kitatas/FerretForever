using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ferret.InGame.Presentation.Controller
{
    public abstract class BaseGameState
    {
        public abstract GameState state { get; }

#pragma warning disable CS1998
        public virtual async UniTask InitAsync(CancellationToken token)
#pragma warning restore CS1998
        {

        }

#pragma warning disable CS1998
        public virtual async UniTask<GameState> TickAsync(CancellationToken token)
#pragma warning restore CS1998
        {
            return GameState.None;
        }
    }
}