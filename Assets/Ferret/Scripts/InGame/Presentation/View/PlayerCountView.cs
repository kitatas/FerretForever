using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class PlayerCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerCountView = default;
        [SerializeField] private TextMeshProUGUI victimCountView = default;

        public void DisplayPlayerCount(int count)
        {
            playerCountView.text = $"{count.ToString()}";
        }

        public void DisplayVictimCount(int count)
        {
            victimCountView.text = $"{count.ToString()}";
        }
    }
}