using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class PlayerCountPresenter : IPostInitializable
    {
        private readonly PlayerCountUseCase _playerCountUseCase;
        private readonly PlayerCountView _playerCountView;

        public PlayerCountPresenter(PlayerCountUseCase playerCountUseCase, PlayerCountView playerCountView)
        {
            _playerCountUseCase = playerCountUseCase;
            _playerCountView = playerCountView;
        }

        public void PostInitialize()
        {
            _playerCountUseCase.playerCount
                .Subscribe(_playerCountView.DisplayPlayerCount)
                .AddTo(_playerCountView);

            _playerCountUseCase.victimCount
                .Subscribe(_playerCountView.DisplayVictimCount)
                .AddTo(_playerCountView);
        }
    }
}