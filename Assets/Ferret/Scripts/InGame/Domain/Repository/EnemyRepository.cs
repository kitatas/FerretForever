using Ferret.InGame.Data.DataStore;

namespace Ferret.InGame.Domain.Repository
{
    public sealed class EnemyRepository
    {
        private readonly EnemyTable _enemyTable;

        public EnemyRepository(EnemyTable enemyTable)
        {
            _enemyTable = enemyTable;
        }

        public EnemyData Find(EnemyType type)
        {
            return _enemyTable.list
                .Find(x => x.type == type);
        }
    }
}