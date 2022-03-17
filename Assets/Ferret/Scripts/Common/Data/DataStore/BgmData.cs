using UnityEngine;

namespace Ferret.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmData), menuName = "DataTable/" + nameof(BgmData), order = 0)]
    public sealed class BgmData : ScriptableObject
    {
        [SerializeField] private BgmType type = default;
        [SerializeField] private AudioClip clip = default;

        public BgmType bgmType => type;
        public AudioClip audioClip => clip;
    }
}