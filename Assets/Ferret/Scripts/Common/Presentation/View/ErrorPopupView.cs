using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Common.Presentation.View
{
    public sealed class ErrorPopupView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panel = default;
        [SerializeField] private TextMeshProUGUI errorMessage = default;
        [SerializeField] private Button closeButton = default;

        public void Init()
        {
            panel.blocksRaycasts = false;
            panel.alpha = 0.0f;
            transform.localScale = Vector3.one * 0.9f;
        }

        public async UniTask PopupAsync(string message, CancellationToken token)
        {
            errorMessage.text = $"{message}";

            await DOTween.Sequence()
                .Append(DOTween.To(
                        () => panel.alpha,
                        x => panel.alpha = x,
                        1.0f,
                        UiConfig.POPUP_ANIMATION_TIME)
                    .SetEase(Ease.OutBack))
                .Join(panel.transform
                    .DOScale(Vector3.one, UiConfig.POPUP_ANIMATION_TIME)
                    .SetEase(Ease.OutBack))
                .SetLink(panel.gameObject)
                .WithCancellation(token);

            panel.blocksRaycasts = true;

            await closeButton.OnClickAsync(token);

            await DOTween.Sequence()
                .Append(DOTween.To(
                        () => panel.alpha,
                        x => panel.alpha = x,
                        0.0f,
                        UiConfig.POPUP_ANIMATION_TIME)
                    .SetEase(Ease.OutQuart))
                .Join(panel.transform
                    .DOScale(Vector3.one * 0.9f, UiConfig.POPUP_ANIMATION_TIME)
                    .SetEase(Ease.OutQuart))
                .SetLink(panel.gameObject)
                .WithCancellation(token);

            Init();
        }
    }
}