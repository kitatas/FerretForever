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

        private const float _animationTime = 0.1f;

        public void Init()
        {
            var button = GetComponent<UIButton>();
            var scale = transform.localScale;

            push += () =>
            {
                DOTween.Sequence()
                    .Append(button.rectTransform
                        .DOScale(scale * 0.8f, _animationTime))
                    .Append(button.rectTransform
                        .DOScale(scale, _animationTime))
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