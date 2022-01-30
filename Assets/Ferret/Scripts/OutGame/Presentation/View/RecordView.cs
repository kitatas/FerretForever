using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetHighRecord(RecordData recordData)
        {
            highScore.text = $"{recordData.score.ToString("F2")}";
        }

        public void SetCurrentRecord(RecordData recordData)
        {
            score.text = $"{recordData.score.ToString("F2")}";
        }
    }
}