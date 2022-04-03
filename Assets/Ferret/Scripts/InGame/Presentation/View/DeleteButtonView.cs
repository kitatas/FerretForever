using System;
using Ferret.Common.Presentation.View;

namespace Ferret.InGame.Presentation.View
{
    public sealed class DeleteButtonView : BaseButtonView
    {
        public void InitButton(Action action)
        {
            push += () =>
            {
                action?.Invoke();
            };
        }
    }
}