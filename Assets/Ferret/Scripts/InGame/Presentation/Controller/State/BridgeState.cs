using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class BridgeState : BaseGameState
    {
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly PlayerCountUseCase _playerCountUseCase;
        private readonly ISeController _serController;
        private readonly BridgeAxisView _bridgeAxisView;

        public BridgeState(PlayerContainerUseCase playerContainerUseCase, PlayerCountUseCase playerCountUseCase,
            ISeController seController, BridgeAxisView bridgeAxisView)
        {
            _playerContainerUseCase = playerContainerUseCase;
            _playerCountUseCase = playerCountUseCase;
            _serController = seController;
            _bridgeAxisView = bridgeAxisView;
        }

        public override GameState state => GameState.Bridge;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _serController.Play(SeType.Fall);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            // 橋形成
            var victimCount = _playerContainerUseCase.GetVictimCount();
            var height = 0.0f;
            for (int i = 0; i < victimCount; i++)
            {
                _playerCountUseCase.Decrease();
                var victim = _playerContainerUseCase.GetVictim();
                await _bridgeAxisView.CreateBridgeAsync(victim, height, token);

                _serController.Play(SeType.Build);
                height += 0.8f;
            }

            // 10匹で橋形成ができなかった時、BGMの辻褄合わせを行う
            var adjustTime = (InGameConfig.MAX_VICTIM_COUNT - victimCount) * InGameConfig.CONVERT_BRIDGE_TIME;
            if (adjustTime > 0.0f)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(adjustTime), cancellationToken: token);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            // 橋を架ける
            if (victimCount == InGameConfig.MAX_VICTIM_COUNT)
            {
                await _bridgeAxisView.BuildBridgeAsync(token);
                _serController.Play(SeType.Ground);
            }
            else
            {
                await _bridgeAxisView.BuildBridgeFailedAsync(token);
            }

            if (_playerContainerUseCase.IsNone())
            {
                return GameState.Finish;
            }
            else
            {
                return GameState.Main;
            }
        }
    }
}