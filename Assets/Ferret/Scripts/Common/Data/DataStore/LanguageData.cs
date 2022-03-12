using UnityEngine;

namespace Ferret.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(LanguageData), menuName = "DataTable/" + nameof(LanguageData), order = 0)]
    public sealed class LanguageData : ScriptableObject
    {
        [SerializeField] private LanguageType type = default;
        [SerializeField] private TextAsset json = default;
        [SerializeField] private Sprite hint = default;

        public LanguageType languageType => type;
        public TextAsset jsonData => json;
        public Sprite hintImage => hint;
    }
}