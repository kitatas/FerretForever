using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Common.Presentation.View
{
    public sealed class TransitionMaskView : MonoBehaviour
    {
        [SerializeField] private Image mask = default;

        private readonly float _animationTime = 0.5f;

        public void Init()
        {

        }

        public async UniTask FadeInAsync(CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(mask.rectTransform
                    .DOAnchorPosX(-1066.0f, 0.0f)
                    .SetEase(Ease.Linear))
                .Append(mask.rectTransform
                    .DOAnchorPosX(0.0f, _animationTime)
                    .SetEase(Ease.Linear))
                .WithCancellation(token);
        }

        public async UniTask FadeOutAsync(CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(mask.rectTransform
                    .DOAnchorPosX(1066.0f, _animationTime)
                    .SetEase(Ease.Linear))
                .WithCancellation(token);
        }
    }
}