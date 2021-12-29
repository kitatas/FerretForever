using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class MainState : BaseGameState
    {
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly InputView _inputView;

        public MainState(PlayerContainerUseCase playerContainerUseCase, InputView inputView)
        {
            _playerContainerUseCase = playerContainerUseCase;
            _inputView = inputView;
        }

        public override GameState state => GameState.Main;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _inputView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (_playerContainerUseCase.IsNone() == false)
            {
                await _inputView.OnPush().ToUniTask(true, token);

                _playerContainerUseCase.JumpAll();
            }

            await UniTask.Yield(token);
            return GameState.Result;
        }
    }
}