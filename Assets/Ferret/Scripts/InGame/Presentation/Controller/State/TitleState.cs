using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseGameState
    {
        private readonly InputView _inputView;
        private readonly TitleView _titleView;
        private readonly GroundController _groundController;

        public TitleState(InputView inputView, TitleView titleView, GroundController groundController)
        {
            _inputView = inputView;
            _titleView = titleView;
            _groundController = groundController;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputView.isPush)
                {
                    break;
                }

                var deltaTime = Time.deltaTime;
                _groundController.Tick(state, deltaTime);

                await UniTask.Yield(token);
            }

            _titleView.SetUp();

            return GameState.Main;
        }
    }
}