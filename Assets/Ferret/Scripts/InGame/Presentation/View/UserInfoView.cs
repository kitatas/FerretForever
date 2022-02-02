using System;
using EFUK;
using Ferret.Common;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Presentation.View;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI uidText = default;
        [SerializeField] private TMP_InputField nameField = default;
        [SerializeField] private BaseUiButtonView changeButton = default;
        [SerializeField] private BaseUiButtonView closeButton = default;
        [SerializeField] private TextMeshProUGUI noticeText = default;

        public void SetUserData(UserRecord record)
        {
            uidText.text = $"{record.uid}";
            nameField.text = $"{record.userName}";
        }

        public void InitButton(Action<string> action)
        {
            changeButton.push += () =>
            {
                action?.Invoke(nameField.text);
                closeButton.Activate(false);
                noticeText.text = $"Now Updating...";
            };

            closeButton.push += () =>
            {
                this.Delay(UiConfig.POPUP_ANIMATION_TIME, () =>
                {
                    noticeText.text = $"";
                });
            };
        }

        public void UpdateUserName()
        {
            closeButton.Activate(true);
            noticeText.text = $"Update Success!!";
        }
    }
}