using System;
using Ferret.Common;
using Ferret.Common.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class LoadButtonView : BaseButtonView
    {
        [SerializeField] private SceneName sceneName = default;

        public void InitButton(Action<SceneName> action)
        {
            push += () =>
            {
                action?.Invoke(sceneName);
            };
        }
    }
}