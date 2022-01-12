using Ferret.InGame.Data.DataStore;

namespace Ferret.InGame.Domain.Repository
{
    public sealed class EffectRepository
    {
        private readonly EffectTable _effectTable;

        public EffectRepository(EffectTable effectTable)
        {
            _effectTable = effectTable;
        }

        public EffectData Find(EffectType type)
        {
            return _effectTable.list
                .Find(x => x.type == type);
        }
    }
}