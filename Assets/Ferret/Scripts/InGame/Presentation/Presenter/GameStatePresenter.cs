using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.View;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.Controller;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class GameStatePresenter : IPostInitializable, IDisposable
    {
        private readonly GameStateUseCase _gameStateUseCase;
        private readonly GameStateController _gameStateController;
        private readonly ErrorPopupView _errorPopupView;
        private readonly LoadingView _loadingView;
        private readonly CancellationTokenSource _tokenSource;

        public GameStatePresenter(GameStateUseCase gameStateUseCase, GameStateController gameStateController,
            ErrorPopupView errorPopupView, LoadingView loadingView)
        {
            _gameStateUseCase = gameStateUseCase;
            _gameStateController = gameStateController;
            _errorPopupView = errorPopupView;
            _loadingView = loadingView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _gameStateController.InitAsync(_tokenSource.Token).Forget();

            _gameStateUseCase.gameState
                .Subscribe(x =>
                {
                    ExecStateAsync(x, _tokenSource.Token).Forget();
                })
                .AddTo(_tokenSource.Token);
        }

        private async UniTask ExecStateAsync(GameState state, CancellationToken token)
        {
            try
            {
                var nextState = await _gameStateController.TickAsync(state, token);
                _gameStateUseCase.SetState(nextState);
            }
            catch (Exception e)
            {
                _loadingView.Activate(false);
                await _errorPopupView.PopupAsync($"{e.ConvertErrorMessage()}", token);
                await ExecStateAsync(_gameStateUseCase.currentState, token);
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}