using Ferret.Common.Data.Entity;

namespace Ferret.InGame.Data.Entity
{
    public sealed class VictimCountEntity : BaseEntity<int>
    {
        public VictimCountEntity()
        {
            Set(0);
        }

        public void Add(int value)
        {
            Set(Get() + value);
        }
    }
}