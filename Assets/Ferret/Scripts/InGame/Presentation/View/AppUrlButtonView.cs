using Ferret.Common;
using Ferret.Common.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AppUrlButtonView : BaseButtonView
    {
        public void InitButton()
        {
            push += () => Application.OpenURL(GameConfig.DEVELOPER_APP_URL);
        }
    }
}