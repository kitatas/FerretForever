using Ferret.OutGame.Data.Entity;
using TMPro;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class RecordView : MonoBehaviour
    {
        [SerializeField] private GameObject newRecordLabel = default;
        [SerializeField] private GameObject background = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetRecord(ResultRecordEntity record)
        {
            newRecordLabel.SetActive(record.isNewRecord);
            background.SetActive(record.isNewRecord);

            highScore.text = $"{record.highScore.ToString("F2")}";
            score.text = $"{record.currentScore.ToString("F2")}";
        }
    }
}