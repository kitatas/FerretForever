using Ferret.Common.Data.Entity;

namespace Ferret.InGame.Data.Entity
{
    public sealed class ScoreEntity : BaseEntity<float>
    {
        public ScoreEntity()
        {
            Set(0.0f);
        }

        public void Add(float deltaTime)
        {
            Set(Get() + deltaTime);
        }
    }
}