using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.Boot.Presentation.View
{
    public sealed class LanguageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameRegisterTitle = default;
        [SerializeField] private TextMeshProUGUI placeholder = default;
        [SerializeField] private TextMeshProUGUI caption1 = default;
        [SerializeField] private TextMeshProUGUI caption2 = default;

        public void SetTextData(BootScene bootScene)
        {
            nameRegisterTitle.text = bootScene.nameRegisterTitle;
            placeholder.text = bootScene.placeholder;
            caption1.text = bootScene.caption1;
            caption2.text = bootScene.caption2;
        }
    }
}