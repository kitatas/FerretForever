using System;
using DG.Tweening;
using UniEx;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ferret.Common.Presentation.View
{
    public class BaseButtonView : MonoBehaviour
    {
        public Action push;

        public void Init()
        {
            var rectTransform = transform.ToRectTransform();
            var scale = transform.localScale;

            push += () =>
            {
                DOTween.Sequence()
                    .Append(rectTransform
                        .DOScale(scale * 0.8f, UiConfig.BUTTON_ANIMATION_TIME))
                    .Append(rectTransform
                        .DOScale(scale, UiConfig.BUTTON_ANIMATION_TIME))
                    .SetLink(gameObject);
            };

            GetComponent<UIBehaviour>()
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