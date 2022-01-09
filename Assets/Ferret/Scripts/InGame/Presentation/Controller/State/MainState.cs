using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class MainState : BaseGameState
    {
        private readonly ScoreUseCase _scoreUseCase;
        private readonly GimmickController _gimmickController;
        private readonly InputView _inputView;

        public MainState(ScoreUseCase scoreUseCase, GimmickController gimmickController, InputView inputView)
        {
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
                if (_gimmickController.IsNoPlayer())
                {
                    return GameState.Result;
                }

                var deltaTime = Time.deltaTime;
                _scoreUseCase.Update(deltaTime);
                _gimmickController.Tick(deltaTime);

                if (_inputView.isPush)
                {
                    _gimmickController.JumpAll();
                }

                await UniTask.Yield(token);
            }

            _gimmickController.SetUpNext();

            return GameState.Bridge;
        }
    }
}