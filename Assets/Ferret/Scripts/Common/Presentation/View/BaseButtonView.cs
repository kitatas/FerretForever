using System;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Common.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public class BaseButtonView : MonoBehaviour
    {
        public Action push;

        public void Init()
        {
            var button = GetComponent<Button>();
            var scale = transform.localScale;

            push += () =>
            {
                DOTween.Sequence()
                    .Append(button.image.rectTransform
                        .DOScale(scale * 0.8f, UiConfig.BUTTON_ANIMATION_TIME))
                    .Append(button.image.rectTransform
                        .DOScale(scale, UiConfig.BUTTON_ANIMATION_TIME))
                    .SetLink(gameObject);
            };

            button
                .OnPointerClickAsObservable()
                .Subscribe(_ => push?.Invoke())
                .AddTo(this);
        }
    }
}