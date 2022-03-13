using System.Collections.Generic;
using EFUK;
using Ferret.Common.Data.DataStore;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class AchievementView : MonoBehaviour
    {
        [SerializeField] private RectTransform viewPort = default;
        [SerializeField] private AchievementDetailView detailView = default;

        public void SetData(IEnumerable<AchievementData> achievementData)
        {
            viewPort.gameObject.DestroyChildren();

            foreach (var data in achievementData)
            {
                var detail = Instantiate(detailView, viewPort);
                detail.SetData(data);
            }
        }
    }
}