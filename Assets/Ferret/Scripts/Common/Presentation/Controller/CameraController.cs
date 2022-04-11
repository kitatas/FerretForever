using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.Common.Presentation.Controller
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private CanvasScaler canvasScaler = default;

        private void Awake()
        {
            var resolution = canvasScaler.referenceResolution;
            var r = resolution.y / resolution.x;
            var s = (float) Screen.height / (float) Screen.width;
            var d = s / r;

            if (d > 1.0f)
            {
                GetComponent<Camera>().orthographicSize *= d;
            }
        }

        public void Shake()
        {
            transform
                .DOShakePosition(0.5f, 0.5f)
                .SetLink(gameObject);
        }
    }
}