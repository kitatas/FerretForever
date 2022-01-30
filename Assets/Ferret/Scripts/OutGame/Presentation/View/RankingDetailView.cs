using Ferret.Common;
using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RankingDetailView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rank = default;
        [SerializeField] private TextMeshProUGUI userName = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetData(RankingData rankingData)
        {
            rank.text = $"{rankingData.playerRank.ToString()}";
            userName.text = $"{rankingData.playerName}";

            var highScore = rankingData.highScore / MasterConfig.SCORE_RATE;
            score.text = $"{highScore.ToString("F2")}";
        }
    }
}