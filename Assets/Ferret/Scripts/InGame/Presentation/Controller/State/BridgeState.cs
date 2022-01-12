using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class BridgeState : BaseGameState
    {
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly PlayerCountUseCase _playerCountUseCase;
        private readonly BridgeAxisView _bridgeAxisView;

        public BridgeState(PlayerContainerUseCase playerContainerUseCase, PlayerCountUseCase playerCountUseCase,
            BridgeAxisView bridgeAxisView)
        {
            _playerContainerUseCase = playerContainerUseCase;
            _playerCountUseCase = playerCountUseCase;
            _bridgeAxisView = bridgeAxisView;
        }

        public override GameState state => GameState.Bridge;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _bridgeAxisView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _bridgeAxisView.SetUp();

            // 橋形成
            var victimCount = _playerContainerUseCase.GetVictimCount();
            var height = 0.0f;
            for (int i = 0; i < victimCount; i++)
            {
                _playerCountUseCase.Decrease();
                var victim = _playerContainerUseCase.GetVictim();
                await _bridgeAxisView.CreateBridgeAsync(victim, height, token);

                height += 0.8f;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            // 橋を架ける
            if (victimCount == InGameConfig.MAX_VICTIM_COUNT)
            {
                await _bridgeAxisView.BuildBridgeAsync(token);
            }
            else
            {
                await _bridgeAxisView.BuildBridgeFailedAsync(token);
            }

            if (_playerContainerUseCase.IsNone())
            {
                return GameState.Result;
            }
            else
            {
                return GameState.Main;
            }
        }
    }
}