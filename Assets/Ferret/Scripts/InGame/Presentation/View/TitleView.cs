using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private RectTransform titleName = default;
        [SerializeField] private RectTransform sideButton = default;
        [SerializeField] private RectTransform counterText = default;
        [SerializeField] private RectTransform bottomText = default;
        [SerializeField] private TextMeshProUGUI tapScreen = default;

        private Tween _tapTween;

        private readonly float _animationTime = 0.5f;

        public void Init()
        {
            titleName
                .DOAnchorPosY(-135.0f, 0.0f);

            sideButton
                .DOAnchorPosX(-50.0f, 0.0f);

            counterText
                .DOAnchorPosY(50.0f, 0.0f);

            bottomText
                .DOAnchorPosY(30.0f, 0.0f);

            _tapTween = tapScreen
                .DOFade(0.0f, _animationTime)
                .SetEase(Ease.InQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        public void SetUp()
        {
            titleName
                .DOAnchorPosY(135.0f, _animationTime);

            sideButton
                .DOAnchorPosX(50.0f, _animationTime);

            counterText
                .DOAnchorPosY(-50.0f, _animationTime);

            bottomText
                .DOAnchorPosY(-30.0f, _animationTime);

            _tapTween?.Kill();
            tapScreen
                .DOFade(0.0f, _animationTime);
        }
    }
}