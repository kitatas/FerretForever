using System.Collections.Generic;
using UnityEngine;

namespace Ferret.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(LanguageTable), menuName = "DataTable/" + nameof(LanguageTable), order = 0)]
    public sealed class LanguageTable : ScriptableObject
    {
        [SerializeField] private List<LanguageData> list = default;
        public List<LanguageData> dataList => list;
        public List<LanguageJsonData> jsonData { get; private set; }

        public void Init()
        {
            jsonData = new List<LanguageJsonData>();
            foreach (var languageData in dataList)
            {
                var data = JsonUtility.FromJson<LanguageJsonData>(languageData.jsonData.ToString());
                jsonData.Add(data);
            }
        }
    }
}