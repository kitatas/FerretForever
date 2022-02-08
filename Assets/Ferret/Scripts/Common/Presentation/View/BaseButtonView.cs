using System;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Common.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public sealed class BaseButtonView : MonoBehaviour
    {
        public Action push;

        private const float _animationTime = 0.1f;

        public void Init()
        {
            var button = GetComponent<Button>();
            var scale = transform.localScale;

            push += () =>
            {
                DOTween.Sequence()
                    .Append(button.image.rectTransform
                        .DOScale(scale * 0.8f, _animationTime))
                    .Append(button.image.rectTransform
                        .DOScale(scale, _animationTime))
                    .SetLink(gameObject);
            };

            button
                .OnPointerDownAsObservable()
                .Subscribe(_ => push?.Invoke())
                .AddTo(this);
        }
    }
}