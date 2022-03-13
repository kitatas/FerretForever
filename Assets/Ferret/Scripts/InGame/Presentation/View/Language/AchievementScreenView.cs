using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AchievementScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = default;
        [SerializeField] private TextMeshProUGUI playCount = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TextMeshProUGUI totalScore = default;
        [SerializeField] private TextMeshProUGUI totalVictimCount = default;

        public void Display(AchievementScreen achievementScreen)
        {
            title.text = achievementScreen.title;
            playCount.text = achievementScreen.playCount;
            highScore.text = achievementScreen.highScore;
            totalScore.text = achievementScreen.totalScore;
            totalVictimCount.text = achievementScreen.totalVictimCount;
        }
    }
}