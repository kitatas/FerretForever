using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EFUK;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Common.Presentation.View
{
    public sealed class TransitionMaskView : MonoBehaviour
    {
        [SerializeField] private Image mask = default;
        [SerializeField] private Image raycastBlocker = default;

        private readonly float _animationTime = 0.1f;

        public void Init()
        {
            raycastBlocker.raycastTarget = false;
            Hide();
        }

        private void Hide()
        {
            mask.rectTransform
                .DOAnchorPosX(-1140.0f, 0.0f)
                .SetEase(Ease.Linear);
        }

        public async UniTask FadeInAsync(CancellationToken token)
        {
            raycastBlocker.raycastTarget = true;

            if (mask.rectTransform.anchoredPosition.x.Equal(0.0f) == false)
            {
                Hide();
            }

            await DOTween.Sequence()
                .Append(mask.rectTransform
                    .DOAnchorPosX(0.0f, _animationTime)
                    .SetEase(Ease.Linear))
                .WithCancellation(token);
        }

        public async UniTask FadeOutAsync(CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(mask.rectTransform
                    .DOAnchorPosX(1140.0f, _animationTime)
                    .SetEase(Ease.Linear))
                .WithCancellation(token);

            raycastBlocker.raycastTarget = false;
        }
    }
}