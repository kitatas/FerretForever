using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EFUK;
using TMPro;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class ResultView : MonoBehaviour
    {
        [SerializeField] private RectTransform finishText = default;
        [SerializeField] private TextMeshProUGUI tapScreen = default;

        private readonly float _animationTime = 0.5f;

        public void Init()
        {
            finishText
                .DOAnchorPosY(160.0f, 0.0f);
        }

        public async UniTask SetUpAsync(CancellationToken token)
        {
            await finishText
                .DOAnchorPosY(-160.0f, _animationTime)
                .WithCancellation(token);

            SetTapScreen();
        }

        private void SetTapScreen()
        {
            tapScreen.color = tapScreen.color.SetAlpha(1.0f);
            tapScreen
                .DOFade(0.0f, _animationTime)
                .SetEase(Ease.InQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }
    }
}