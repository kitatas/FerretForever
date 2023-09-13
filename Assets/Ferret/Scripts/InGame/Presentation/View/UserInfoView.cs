using System;
using Ferret.Common;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Presentation.View;
using TMPro;
using UniEx;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI uidText = default;
        [SerializeField] private TMP_InputField nameField = default;
        [SerializeField] private BaseButtonView changeButton = default;
        [SerializeField] private BaseButtonView closeButton = default;
        [SerializeField] private TextMeshProUGUI noticeText = default;
        private string _prevUserName;
        private UpdateScreen _updateScreen;

        public void SetUserData(UserRecord record)
        {
            _prevUserName = record.userName;
            uidText.text = $"{record.uid}";
            nameField.text = _prevUserName;
        }

        public void SetLanguage(UpdateScreen updateScreen)
        {
            _updateScreen = updateScreen;
        }

        public void InitButton(Action<string> action)
        {
            changeButton.push += () =>
            {
                action?.Invoke(nameField.text);
                closeButton.Activate(false);
                noticeText.text = _updateScreen.updating;
            };

            closeButton.push += () =>
            {
                this.Delay(UiConfig.POPUP_ANIMATION_TIME, () =>
                {
                    noticeText.text = $"";
                });
            };
        }

        public void UpdateSuccessUserName()
        {
            closeButton.Activate(true);
            noticeText.text = _updateScreen.success;
            _prevUserName = nameField.text;
        }

        public void UpdateFailedUserName()
        {
            closeButton.Activate(true);
            noticeText.text = _updateScreen.failed;
            nameField.text = _prevUserName;
        }
    }
}