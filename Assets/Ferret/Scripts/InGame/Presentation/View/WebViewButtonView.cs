using EFUK;
using Ferret.Common;
using Ferret.Common.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class WebViewButtonView : BaseButtonView
    {
        [SerializeField] private WebviewType type = default;
        [SerializeField] private RectTransform panel = default;
        [SerializeField] private Canvas canvas = default;
        [SerializeField] private BaseButtonView closeButton = default;

        private void Start()
        {
            (int left, int top, int right, int bottom) = GetMargins();

            WebViewObject webViewObject = null;

            push += () =>
            {
                webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();
                webViewObject.Init(enableWKWebView: true);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                webViewObject.bitmapRefreshCycle = 1;
#endif
                webViewObject.LoadURL(type.ConvertUrl());
                webViewObject.SetMargins(left, top, right, bottom);

                this.Delay(UiConfig.POPUP_ANIMATION_TIME, () =>
                {
                    // ポップアップ完了後に表示する
                    webViewObject.SetVisibility(true);
                });
            };

            closeButton.push += () =>
            {
                if (webViewObject != null)
                {
                    webViewObject.SetVisibility(false);
                    Destroy(webViewObject.gameObject);
                }
            };
        }

        private (int, int, int, int) GetMargins()
        {
            var corners = new Vector3[4];
            panel.GetWorldCorners(corners);
            var screenCorner1 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[1]);
            var screenCorner3 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[3]);
            var rect = new Rect
            {
                x = screenCorner1.x,
                y = screenCorner3.y,
                width = screenCorner3.x - screenCorner1.x,
                height = screenCorner1.y - screenCorner3.y,
            };

            return (
                (int)rect.xMin,
                Screen.height - (int)rect.yMax,
                Screen.width - (int)rect.xMax,
                (int)rect.yMin
            );
        }
    }
}