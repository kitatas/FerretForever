using Coffee.UIEffects;
using Ferret.Common;
using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AchievementDetailView : MonoBehaviour
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private UIShiny shiny = default;
        [SerializeField] private TextMeshProUGUI message = default;

        public void SetData(AchievementData data)
        {
            shiny.enabled = data.isAchieve && data.rank != AchievementRank.Normal;

            if (data.isAchieve)
            {
                icon.color = data.color;
                message.text = $"{data.detail}";
            }
            else
            {
                icon.color = Color.black;
                message.text = $"???";
            }
        }
    }
}