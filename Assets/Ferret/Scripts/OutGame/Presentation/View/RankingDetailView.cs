using Ferret.Common;
using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RankingDetailView : MonoBehaviour
    {
        [SerializeField] private Image background = default;
        [SerializeField] private TextMeshProUGUI rank = default;
        [SerializeField] private TextMeshProUGUI userName = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetData(RankingData rankingData, bool isSelf)
        {
            // 自身のラベルのみ色を変更
            if (isSelf)
            {
                background.color = RankingConfig.SELF_BACKGROUND_COLOR;
            }

            rank.text = $"{rankingData.playerRank.ToString()}";
            userName.text = $"{rankingData.playerName}";

            var highScore = rankingData.highScore / MasterConfig.SCORE_RATE;
            score.text = $"{highScore.ToString("F2")}";
        }
    }
}