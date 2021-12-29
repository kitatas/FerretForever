using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PlayerTable), menuName = "DataTable/" + nameof(PlayerTable), order = 0)]
    public sealed class PlayerTable : ScriptableObject
    {
        [SerializeField] private PlayerData[] dataList = default;

        public PlayerData[] list => dataList;
    }
}