using System;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class PlayerCountPresenter : IPostInitializable, IDisposable
    {
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly PlayerCountView _playerCountView;
        private readonly CompositeDisposable _disposable;

        public PlayerCountPresenter(PlayerContainerUseCase playerContainerUseCase, PlayerCountView playerCountView)
        {
            _playerContainerUseCase = playerContainerUseCase;
            _playerCountView = playerCountView;
            _disposable = new CompositeDisposable();
        }

        public void PostInitialize()
        {
            _playerContainerUseCase.players
                .ObserveCountChanged()
                .Subscribe(_playerCountView.Display)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}