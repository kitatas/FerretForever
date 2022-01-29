using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ferret.Boot.Presentation.View
{
    public sealed class LoadingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loadText = default;
        [SerializeField] private GameObject icon = default;

        private void Start()
        {
            var animator = new DOTweenTMPAnimator(loadText);
            var offset = Vector3.up * 5.0f;
            var rate = 0.05f;
            var length = animator.textInfo.characterCount;
            var loopInterval = length * rate + 0.5f;
            for (int i = 0; i < length; i++)
            {
                var interval = i * 0.1f;
                DOTween.Sequence()
                    .AppendInterval(0.5f)
                    .Append(animator
                        .DOOffsetChar(i, animator.GetCharOffset(i) + offset, 0.2f)
                        .SetLoops(2, LoopType.Yoyo)
                        .SetDelay(interval))
                    .AppendInterval(loopInterval - interval)
                    .SetLoops(-1)
                    .SetLink(loadText.gameObject);
            }
        }

        public void Activate(bool value)
        {
            loadText.gameObject.SetActive(value);
            icon.SetActive(value);
        }
    }
}