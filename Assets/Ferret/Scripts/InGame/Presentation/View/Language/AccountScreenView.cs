using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AccountScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI confirmText = default;
        [SerializeField] private TextMeshProUGUI deleteText = default;

        public void Display(AccountScreen accountScreen)
        {
            confirmText.text = accountScreen.deleteConfirm;
            deleteText.text = accountScreen.deleteComplete;
        }
    }
}