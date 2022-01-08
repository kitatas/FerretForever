using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class PlayerCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerCountView = default;

        public void Display(int count)
        {
            playerCountView.text = $"{count.ToString()}";
        }
    }
}