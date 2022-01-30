using Ferret.Common.Data.DataStore;
using Ferret.OutGame.Application;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RankingView : MonoBehaviour
    {
        [SerializeField] private RectTransform viewport = default;
        [SerializeField] private RankingDetailView detailView = default;

        public void SetData(RankingData[] rankingData)
        {
            var length = Mathf.Min(rankingData.Length, RankingConfig.SHOW_MAX_RANK);

            for (int i = 0; i < length; i++)
            {
                var detail = Instantiate(detailView, viewport);
                detail.SetData(rankingData[i]);
            }
        }
    }
}