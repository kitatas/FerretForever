using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText = default;

        public void Show(float value)
        {
            scoreText.text = $"{value.ToString("F2")}";
        }
    }
}