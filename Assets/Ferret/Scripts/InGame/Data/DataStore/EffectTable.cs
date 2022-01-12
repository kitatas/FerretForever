using System.Collections.Generic;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EffectTable), menuName = "DataTable/" + nameof(EffectTable), order = 0)]
    public sealed class EffectTable : ScriptableObject
    {
        [SerializeField] private List<EffectData> dataList = default;

        public List<EffectData> list => dataList;
    }
}