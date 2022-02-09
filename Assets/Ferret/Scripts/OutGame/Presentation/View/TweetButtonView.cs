using Ferret.Common;
using Ferret.Common.Presentation.View;
using UnityEngine;
using UnityEngine.Networking;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class TweetButtonView : BaseButtonView
    {
        public void Init(float score)
        {
            var tweetText = $"{score.ToString("F2")}m\n" + $"#{GameConfig.GAME_ID}\n";
            tweetText += $"{GameConfig.APP_URL}";
            var url = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweetText)}";

            push += () => Application.OpenURL(url);
        }
    }
}