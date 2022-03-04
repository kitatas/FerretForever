using Ferret.Common;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(AchievementRankData), menuName = "DataTable/" + nameof(AchievementRankData), order = 0)]
    public sealed class AchievementRankData : ScriptableObject
    {
        [SerializeField] private AchievementRank achievementRank = default;
        [SerializeField] private Color mainColor = default;

        public AchievementRank rank => achievementRank;
        public Color color => mainColor;
    }
}