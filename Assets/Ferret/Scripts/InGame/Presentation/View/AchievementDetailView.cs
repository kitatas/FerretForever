using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AchievementDetailView : MonoBehaviour
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private TextMeshProUGUI message = default;

        public void SetData(AchievementData data)
        {
            if (data.isAchieve)
            {
                icon.color = Color.white;
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