using System.Collections.Generic;
using UnityEngine;

namespace Ferret.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable), order = 0)]
    public sealed class SeTable : ScriptableObject
    {
        [SerializeField] private List<SeData> list = default;

        public List<SeData> dataList => list;
    }
}