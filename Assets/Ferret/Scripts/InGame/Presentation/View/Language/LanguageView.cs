using Ferret.Common.Data.DataStore;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.InGame.Presentation.View
{
    public sealed class LanguageView : MonoBehaviour
    {
        [SerializeField] private OptionScreenView optionScreenView = default;
        [SerializeField] private AchievementScreenView achievementScreenView = default;
        [SerializeField] private InformationScreenView informationScreenView = default;
        [SerializeField] private Image hint = default;

        public void Display(MainScene mainScene)
        {
            optionScreenView.Display(mainScene.option);
            achievementScreenView.Display(mainScene.achievement);
            informationScreenView.Display(mainScene.information);
        }

        public void SetHint(Sprite sprite)
        {
            hint.sprite = sprite;
        }
    }
}