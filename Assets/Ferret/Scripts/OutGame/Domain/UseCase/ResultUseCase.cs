using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Data.Entity;
using Ferret.Common.Domain.Repository;
using Ferret.OutGame.Data.Entity;
using UniEx;

namespace Ferret.OutGame.Domain.UseCase
{
    public sealed class ResultUseCase
    {
        private readonly UserRecordEntity _userRecordEntity;
        private readonly PlayFabRepository _playFabRepository;

        public ResultUseCase(UserRecordEntity userRecordEntity, PlayFabRepository playFabRepository)
        {
            _userRecordEntity = userRecordEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask<IEnumerable<RankingData>> GetRankDataAsync(CancellationToken token)
        {
            var rankingData = await _playFabRepository.GetRankDataAsync(token);
            foreach (var data in rankingData)
            {
                data.displayScore = data.highScore / MasterConfig.SCORE_RATE;
                data.isSelf = data.playerId.Equals(_userRecordEntity.Get().uid);
            }

            return rankingData;
        }

        public ResultRecordEntity GetSelfRecord()
        {
            var highScore = _userRecordEntity.Get().highRecord.score;
            var currentScore = _userRecordEntity.Get().currentRecord.score;

            return new ResultRecordEntity(
                highScore,
                currentScore,
                highScore.IsEqual(currentScore)
            );
        }

        public string BuildTweetMessage(ResultScene resultScene)
        {
            return string.Format(
                resultScene.tweet,
                _userRecordEntity.Get().currentRecord.victimCount.ToString(),
                _userRecordEntity.Get().currentRecord.score.ToString("F2")
            );
        }
    }
}