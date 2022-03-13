using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class OptionScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = default;
        [SerializeField] private TextMeshProUGUI userName = default;
        [SerializeField] private TextMeshProUGUI placeholder = default;
        [SerializeField] private TextMeshProUGUI language = default;
        [SerializeField] private TextMeshProUGUI soundVolume = default;

        public void Display(OptionScreen optionScreen)
        {
            title.text = optionScreen.title;
            userName.text = optionScreen.userName;
            placeholder.text = optionScreen.placeHolder;
            language.text = optionScreen.language;
            soundVolume.text = optionScreen.soundVolume;
        }
    }
}