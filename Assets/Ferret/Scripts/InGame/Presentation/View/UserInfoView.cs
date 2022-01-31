using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI uidText = default;

        public void SetUserData(UserRecord record)
        {
            uidText.text = $"{record.uid}";
        }
    }
}