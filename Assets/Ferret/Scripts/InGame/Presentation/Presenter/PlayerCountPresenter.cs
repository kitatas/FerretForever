using System;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class PlayerCountPresenter : IPostInitializable, IDisposable
    {
        private readonly PlayerCountUseCase _playerCountUseCase;
        private readonly PlayerCountView _playerCountView;
        private readonly CompositeDisposable _disposable;

        public PlayerCountPresenter(PlayerCountUseCase playerCountUseCase, PlayerCountView playerCountView)
        {
            _playerCountUseCase = playerCountUseCase;
            _playerCountView = playerCountView;
            _disposable = new CompositeDisposable();
        }

        public void PostInitialize()
        {
            _playerCountUseCase.count
                .Subscribe(_playerCountView.Display)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}