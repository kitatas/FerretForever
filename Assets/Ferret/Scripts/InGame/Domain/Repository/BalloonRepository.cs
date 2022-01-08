using Ferret.InGame.Data.DataStore;

namespace Ferret.InGame.Domain.Repository
{
    public sealed class BalloonRepository
    {
        private readonly BalloonTable _balloonTable;

        public BalloonRepository(BalloonTable balloonTable)
        {
            _balloonTable = balloonTable;
        }

        public BalloonData Find(BalloonType type)
        {
            return _balloonTable.list
                .Find(x => x.type == type);
        }
    }
}