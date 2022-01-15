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
        private readonly InputView _inputView;

        public MainState(PlayerContainerUseCase playerContainerUseCase, ScoreUseCase scoreUseCase,
            GimmickController gimmickController, InputView inputView)
        {
            _playerContainerUseCase = playerContainerUseCase;
            _scoreUseCase = scoreUseCase;
            _gimmickController = gimmickController;
            _inputView = inputView;
        }

        public override GameState state => GameState.Main;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _gimmickController.Init();
            _inputView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_gimmickController.IsArriveBridge())
                {
                    break;
                }

                // 残機0になったら終了
                if (_playerContainerUseCase.IsNone())
                {
                    return GameState.Result;
                }

                var deltaTime = Time.deltaTime;
                _scoreUseCase.Update(deltaTime);
                _gimmickController.Tick(state, deltaTime);

                if (_inputView.isPush)
                {
                    _playerContainerUseCase.JumpAll();
                }

                await UniTask.Yield(token);
            }

            _gimmickController.SetUpNext();

            return GameState.Bridge;
        }
    }
}