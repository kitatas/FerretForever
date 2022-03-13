using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AchievementScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = default;

        public void Display(AchievementScreen achievementScreen)
        {
            title.text = achievementScreen.title;
        }
    }
}