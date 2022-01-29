using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Data.Entity;
using Ferret.Common.Domain.Repository;
using Ferret.InGame.Data.Entity;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class UserRecordUseCase
    {
        private readonly UserRecordEntity _userRecordEntity;
        private readonly ScoreEntity _scoreEntity;
        private readonly VictimCountEntity _victimCountEntity;
        private readonly PlayFabRepository _playFabRepository;

        public UserRecordUseCase(UserRecordEntity userRecordEntity, ScoreEntity scoreEntity,
            VictimCountEntity victimCountEntity, PlayFabRepository playFabRepository)
        {
            _userRecordEntity = userRecordEntity;
            _scoreEntity = scoreEntity;
            _victimCountEntity = victimCountEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask UpdateRecordAsync(CancellationToken token)
        {
            _userRecordEntity.UpdateScore(_scoreEntity.Get(), _victimCountEntity.Get());

            await UniTask.WhenAll(
                _playFabRepository.UpdateUserRecordAsync(_userRecordEntity.Get(), token),
                _playFabRepository.SendRankDataAsync(_userRecordEntity.Get().highRecord.score, token)
            );
        }
    }
}