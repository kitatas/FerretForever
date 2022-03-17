using UnityEngine;

namespace Ferret.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeData), menuName = "DataTable/" + nameof(SeData), order = 0)]
    public sealed class SeData : ScriptableObject
    {
        [SerializeField] private SeType type = default;
        [SerializeField] private AudioClip clip = default;

        public SeType seType => type;
        public AudioClip audioClip => clip;
    }
}