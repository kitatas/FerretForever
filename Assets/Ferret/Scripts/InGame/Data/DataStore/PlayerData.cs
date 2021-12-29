using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = "DataTable/" + nameof(PlayerData), order = 0)]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField] private PlayerColor playerColor = default;
        [SerializeField] private PlayerController playerController = default;

        public PlayerColor color => playerColor;
        public PlayerController player => playerController;
    }
}