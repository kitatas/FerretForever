using System.Collections.Generic;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(AchievementRankTable), menuName = "DataTable/" + nameof(AchievementRankTable), order = 0)]
    public sealed class AchievementRankTable : ScriptableObject
    {
        [SerializeField] private List<AchievementRankData> dataList = default;

        public List<AchievementRankData> list => dataList;
    }
}