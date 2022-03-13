using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class UpdateScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI updating = default;
        [SerializeField] private TextMeshProUGUI success = default;
        [SerializeField] private TextMeshProUGUI failed = default;

        public void Display(UpdateScreen updateScreen)
        {
            // TODO: 動的変更
            updating.text = updateScreen.updating;
            success.text = updateScreen.success;
            failed.text = updateScreen.failed;
        }
    }
}