using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class BridgeState : BaseGameState
    {
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly BridgeAxisView _bridgeAxisView;

        public BridgeState(PlayerContainerUseCase playerContainerUseCase, BridgeAxisView bridgeAxisView)
        {
            _playerContainerUseCase = playerContainerUseCase;
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
            var loop = Mathf.Min(_playerContainerUseCase.players.Count, InGameConfig.VICTIM_COUNT);
            var height = 0.0f;
            for (int i = 0; i < loop; i++)
            {
                var victim = _playerContainerUseCase.GetVictim();
                _bridgeAxisView.CreateBridge(victim);

                await DOTween.Sequence()
                    .Append(victim.transform
                        .DOLocalMoveX(0.0f, 0.05f))
                    .Append(victim.transform
                        .DOLocalMoveY(height, 0.05f))
                    .WithCancellation(token);

                height += 0.8f;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            // 橋を架ける
            if (loop == InGameConfig.VICTIM_COUNT)
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