using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Data.Entity;
using UniRx;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerCountUseCase : IDisposable
    {
        private readonly PlayerCountEntity _playerCountEntity;
        private readonly VictimCountEntity _victimCountEntity;
        private readonly ReactiveProperty<int> _playerCount;
        private readonly ReactiveProperty<int> _victimCount;
        private readonly CancellationTokenSource _tokenSource;

        public PlayerCountUseCase(PlayerCountEntity playerCountEntity, VictimCountEntity victimCountEntity)
        {
            _playerCountEntity = playerCountEntity;
            _victimCountEntity = victimCountEntity;
            _playerCount = new ReactiveProperty<int>(_playerCountEntity.Get());
            _victimCount = new ReactiveProperty<int>(_victimCountEntity.Get());
            _tokenSource = new CancellationTokenSource();
        }

        public IReadOnlyReactiveProperty<int> playerCount => _playerCount;

        public IReadOnlyReactiveProperty<int> victimCount => _victimCount;

        public void Increase(int value)
        {
            IncreaseAsync(value, _tokenSource.Token).Forget();
        }

        private async UniTaskVoid IncreaseAsync(int value, CancellationToken token)
        {
            for (int i = 0; i < value; i++)
            {
                _playerCountEntity.Add(1);
                _playerCount.Value = _playerCountEntity.Get();
                await UniTask.Yield(token);
            }
        }

        public void Decrease()
        {
            _playerCountEntity.Add(-1);
            _playerCount.Value = _playerCountEntity.Get();

            _victimCountEntity.Add(1);
            _victimCount.Value = _victimCountEntity.Get();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}