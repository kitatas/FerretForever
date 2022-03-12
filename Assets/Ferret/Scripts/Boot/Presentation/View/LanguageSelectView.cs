using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ferret.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Boot.Presentation.View
{
    public sealed class LanguageSelectView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panel = default;
        [SerializeField] private Button englishButton = default;
        [SerializeField] private Button japaneseButton = default;

        public void Init()
        {
            panel.blocksRaycasts = false;
            panel.alpha = 0.0f;
            transform.localScale = Vector3.one * 0.9f;
        }

        public async UniTask<LanguageType> DecisionLanguageAsync(CancellationToken token)
        {
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

            var index = await UniTask.WhenAny(
                englishButton.OnClickAsync(token),
                japaneseButton.OnClickAsync(token)
            );

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

            return index.ConvertLanguage();
        }
    }
}