using EFUK;
using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RecordView : MonoBehaviour
    {
        [SerializeField] private GameObject newRecordLabel = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetRecord(RecordData highRecord, RecordData currentRecord)
        {
            var highScoreValue = highRecord.score;
            var scoreValue = currentRecord.score;

            newRecordLabel.SetActive(scoreValue.Equal(highScoreValue));

            highScore.text = $"{highScoreValue.ToString("F2")}";
            score.text = $"{scoreValue.ToString("F2")}";
        }
    }
}