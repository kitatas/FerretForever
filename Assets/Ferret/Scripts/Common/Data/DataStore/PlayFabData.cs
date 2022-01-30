using System.Linq;
using PlayFab.ClientModels;

namespace Ferret.Common.Data.DataStore
{
    public sealed class UserRecord
    {
        public string userName;
        public int playCount;
        public RecordData highRecord;
        public RecordData totalRecord;

        public UserRecord()
        {
            userName = "";
            playCount = 0;
            highRecord = new RecordData
            {
                score = 0,
                victimCount = 0,
            };
            totalRecord = new RecordData
            {
                score = 0,
                victimCount = 0,
            };
        }
    }

    public sealed class RecordData
    {
        public float score;
        public int victimCount;
    }

    public sealed class RankingData
    {
        public string playerId;
        public int playerRank;
        public string playerName;
        public float highScore;

        public RankingData(PlayerLeaderboardEntry entry)
        {
            playerId = entry.PlayFabId;
            playerRank = entry.Position + 1;
            playerName = entry.DisplayName;
            highScore = entry.Profile.Statistics?.FirstOrDefault(x => x.Name == MasterConfig.RANKING_NAME)?.Value ?? 0;
        }
    }
}