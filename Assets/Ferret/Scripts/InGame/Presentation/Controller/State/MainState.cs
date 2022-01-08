using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class MainState : BaseGameState
    {
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly ScoreUseCase _scoreUseCase;
        private readonly GimmickController _gimmickController;
        private readonly GroundController _groundController;
        private readonly BridgeView _bridgeView;
        private readonly InputView _inputView;

        public MainState(PlayerContainerUseCase playerContainerUseCase, ScoreUseCase scoreUseCase,
            GimmickController gimmickController, GroundController groundController, BridgeView bridgeView,
            InputView inputView)
        {
            _playerContainerUseCase = playerContainerUseCase;
            _scoreUseCase = scoreUseCase;
            _gimmickController = gimmickController;
            _groundController = groundController;
            _bridgeView = bridgeView;
            _inputView = inputView;
        }

        public override GameState state => GameState.Main;

        public override async UniTask InitAsync(CancellationToken token)
        {
            for (int i = 0; i < InGameConfig.INIT_PLAYER_COUNT; i++)
            {
                var position = new Vector3(-3.0f - i, 2.0f, -0.01f * i);
                _playerContainerUseCase.Generate(position);
            }

            _groundController.Init(_gimmickController);
            _bridgeView.Init();
            _inputView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (_bridgeView.isArrive == false)
            {
                // 残機0になったら終了
                if (_playerContainerUseCase.IsNone())
                {
                    return GameState.Result;
                }

                var deltaTime = Time.deltaTime;
                _scoreUseCase.Update(deltaTime);
                _groundController.Tick(deltaTime);

                if (_inputView.isPush)
                {
                    _playerContainerUseCase.JumpAll();
                }

                await UniTask.Yield(token);
            }

            _bridgeView.SetUpNext();

            return GameState.Bridge;
        }
    }
}