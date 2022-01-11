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
        private readonly ReactiveProperty<int> _count;
        private readonly CancellationTokenSource _tokenSource;

        public PlayerCountUseCase(PlayerCountEntity playerCountEntity)
        {
            _playerCountEntity = playerCountEntity;
            _count = new ReactiveProperty<int>(_playerCountEntity.Get());
            _tokenSource = new CancellationTokenSource();
        }

        public IReadOnlyReactiveProperty<int> count => _count;

        public void Increase(int value)
        {
            IncreaseAsync(value, _tokenSource.Token).Forget();
        }

        private async UniTaskVoid IncreaseAsync(int value, CancellationToken token)
        {
            for (int i = 0; i < value; i++)
            {
                _playerCountEntity.Add(1);
                _count.Value = _playerCountEntity.Get();
                await UniTask.Yield(token);
            }
        }

        public void Decrease()
        {
            _playerCountEntity.Add(-1);
            _count.Value = _playerCountEntity.Get();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}