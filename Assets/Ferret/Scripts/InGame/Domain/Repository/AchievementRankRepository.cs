using Ferret.Common;
using Ferret.InGame.Data.DataStore;

namespace Ferret.InGame.Domain.Repository
{
    public sealed class AchievementRankRepository
    {
        private readonly AchievementRankTable _achievementRankTable;

        public AchievementRankRepository(AchievementRankTable achievementRankTable)
        {
            _achievementRankTable = achievementRankTable;
        }

        public AchievementRankData Find(AchievementRank rank)
        {
            return _achievementRankTable.list
                .Find(x => x.rank == rank);
        }
    }
}