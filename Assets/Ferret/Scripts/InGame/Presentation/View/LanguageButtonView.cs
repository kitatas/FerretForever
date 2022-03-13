using System;
using Ferret.Common;
using Ferret.Common.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class LanguageButtonView : BaseButtonView
    {
        [SerializeField] private LanguageType type = default;

        public void InitButton(Action<LanguageType> action)
        {
            push += () =>
            {
                action?.Invoke(type);
            };
        }
    }
}