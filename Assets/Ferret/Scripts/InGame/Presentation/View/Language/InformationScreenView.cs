using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class InformationScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = default;
        [SerializeField] private TextMeshProUGUI credit = default;
        [SerializeField] private TextMeshProUGUI license = default;
        [SerializeField] private TextMeshProUGUI policy = default;

        public void Display(InformationScreen informationScreen)
        {
            title.text = informationScreen.title;
            credit.text = informationScreen.credit;
            license.text = informationScreen.license;
            policy.text = informationScreen.policy;
        }
    }
}