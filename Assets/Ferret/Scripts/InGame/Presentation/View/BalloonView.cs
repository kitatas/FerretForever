using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class BalloonView : MonoBehaviour
    {
        [SerializeField] private BalloonType balloonType = default;

        public BalloonType type => balloonType;
    }
}