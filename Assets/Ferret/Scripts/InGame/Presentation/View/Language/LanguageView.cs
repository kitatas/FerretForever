using Ferret.Common.Data.DataStore;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.InGame.Presentation.View
{
    public sealed class LanguageView : MonoBehaviour
    {
        [SerializeField] private OptionScreenView optionScreenView = default;
        [SerializeField] private AccountScreenView accountScreenView = default;
        [SerializeField] private AchievementScreenView achievementScreenView = default;
        [SerializeField] private InformationScreenView informationScreenView = default;
        [SerializeField] private Image hint = default;

        public void Display((MainScene mainScene, Sprite hintSprite) mainData)
        {
            optionScreenView.Display(mainData.mainScene.option);
            accountScreenView.Display(mainData.mainScene.account);
            achievementScreenView.Display(mainData.mainScene.achievement);
            informationScreenView.Display(mainData.mainScene.information);
            hint.sprite = mainData.hintSprite;
        }
    }
}