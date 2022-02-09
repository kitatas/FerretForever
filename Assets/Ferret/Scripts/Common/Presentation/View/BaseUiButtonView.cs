using System;
using DG.Tweening;
using Doozy.Runtime.UIManager.Components;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ferret.Common.Presentation.View
{
    [RequireComponent(typeof(UIButton))]
    public class BaseUiButtonView : MonoBehaviour
    {
        public Action push;

        public void Init()
        {
            var button = GetComponent<UIButton>();
            var scale = transform.localScale;

            push += () =>
            {
                DOTween.Sequence()
                    .Append(button.rectTransform
                        .DOScale(scale * 0.8f, UiConfig.BUTTON_ANIMATION_TIME))
                    .Append(button.rectTransform
                        .DOScale(scale, UiConfig.BUTTON_ANIMATION_TIME))
                    .SetLink(gameObject);
            };

            button
                .OnPointerClickAsObservable()
                .Subscribe(_ => push?.Invoke())
                .AddTo(this);
        }

        public void Activate(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}