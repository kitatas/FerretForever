using Ferret.Common.Data.Entity;

namespace Ferret.InGame.Data.Entity
{
    public sealed class GameStateEntity : BaseEntity<GameState>
    {
        public GameStateEntity()
        {
            Set(GameState.Title);
        }
    }
}