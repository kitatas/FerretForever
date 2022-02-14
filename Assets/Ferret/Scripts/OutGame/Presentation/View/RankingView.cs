using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RankingView : MonoBehaviour
    {
        [SerializeField] private RectTransform viewport = default;
        [SerializeField] private RankingDetailView detailView = default;

        public void SetData(RankingData[] rankingData, string uid)
        {
            var length = Mathf.Min(rankingData.Length, RankingConfig.SHOW_MAX_RANK);

            for (int i = 0; i < length; i++)
            {
                var detail = Instantiate(detailView, viewport);
                var isSelf = rankingData[i].playerId.Equals(uid);
                detail.SetData(rankingData[i], isSelf);
            }
        }
    }
}