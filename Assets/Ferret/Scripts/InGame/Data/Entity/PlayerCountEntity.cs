using Ferret.Common.Data.Entity;

namespace Ferret.InGame.Data.Entity
{
    public sealed class PlayerCountEntity : BaseEntity<int>
    {
        public PlayerCountEntity()
        {
            Set(InGameConfig.INIT_PLAYER_COUNT);
        }

        public void Add(int value)
        {
            Set(Get() + value);
        }
    }
}