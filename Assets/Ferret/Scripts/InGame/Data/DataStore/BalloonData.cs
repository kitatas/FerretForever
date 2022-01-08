using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BalloonData), menuName = "DataTable/" + nameof(BalloonData), order = 0)]
    public sealed class BalloonData : ScriptableObject
    {
        [SerializeField] private BalloonType balloonType = default;
        [SerializeField] private BalloonController balloonController = default;

        public BalloonType type => balloonType;
        public BalloonController balloon => balloonController;
    }
}