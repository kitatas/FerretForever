using Ferret.Common.Data.DataStore;

namespace Ferret.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public BgmData FindBgm(BgmType type)
        {
            return _bgmTable.dataList
                .Find(x => x.bgmType == type);
        }

        public SeData FindSe(SeType type)
        {
            return _seTable.dataList
                .Find(x => x.seType == type);
        }

        public SeData[] FindAllSe(SeType type)
        {
            return _seTable.dataList
                .FindAll(x => x.seType == type)
                .ToArray();
        }
    }
}