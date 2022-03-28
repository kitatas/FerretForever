using System.Collections.Generic;
using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RankingView : MonoBehaviour
    {
        [SerializeField] private RectTransform viewport = default;
        [SerializeField] private RankingDetailView detailView = default;

        public void SetData(IEnumerable<RankingData> rankingData)
        {
            foreach (var data in rankingData)
            {
                var detail = Instantiate(detailView, viewport);
                detail.SetData(data);
            }
        }
    }
}