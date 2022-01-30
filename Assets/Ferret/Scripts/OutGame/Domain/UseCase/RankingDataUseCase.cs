using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Data.DataStore;
using Ferret.Common.Domain.Repository;

namespace Ferret.OutGame.Domain.UseCase
{
    public sealed class RankingDataUseCase
    {
        private readonly PlayFabRepository _playFabRepository;

        public RankingDataUseCase(PlayFabRepository playFabRepository)
        {
            _playFabRepository = playFabRepository;
        }

        public async UniTask<RankingData[]> GetRankDataAsync(CancellationToken token)
        {
            return await _playFabRepository.GetRankDataAsync(token);
        }
    }
}