using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EffectData), menuName = "DataTable/" + nameof(EffectData), order = 0)]
    public sealed class EffectData : ScriptableObject
    {
        [SerializeField] private EffectType effectType = default;
        [SerializeField] private EffectView effectView = default;

        public EffectType type => effectType;
        public EffectView effect => effectView;
    }
}