using Ferret.Common.Data.DataStore;
using TMPro;
using UnityEngine;

namespace Ferret.OutGame.Presentation.View
{
    public sealed class LanguageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = default;
        [SerializeField] private TextMeshProUGUI ranking = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void Display(ResultScene resultScene)
        {
            title.text = resultScene.title;
            ranking.text = resultScene.ranking;
            highScore.text = resultScene.highScore;
            score.text = resultScene.score;
        }
    }
}