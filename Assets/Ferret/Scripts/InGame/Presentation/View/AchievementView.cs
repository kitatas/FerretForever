using System.Collections.Generic;
using Ferret.Common.Data.DataStore;
using UniEx;
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