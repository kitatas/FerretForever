using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ferret.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Boot.Presentation.View
{
    public sealed class NameRegistrationView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panel = default;
        [SerializeField] private TMP_InputField inputField = default;
        [SerializeField] private Button decisionButton = default;

        public string inputName => inputField.text;

        public void Init()
        {
            panel.blocksRaycasts = false;
            panel.alpha = 0.0f;
            transform.localScale = Vector3.one * 0.9f;
        }

        public async UniTask DecisionNameAsync(CancellationToken token)
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

            await decisionButton.OnClickAsync(token);

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

        public void ResetName()
        {
            inputField.text = "";
        }
    }
}