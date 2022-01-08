using System.Collections.Generic;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BalloonTable), menuName = "DataTable/" + nameof(BalloonTable), order = 0)]
    public sealed class BalloonTable : ScriptableObject
    {
        [SerializeField] private List<BalloonData> dataList = default;

        public List<BalloonData> list => dataList;
    }
}