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
        private readonly CompositeDisposable _disposable;
        private readonly CancellationTokenSource _tokenSource;

        public GameStatePresenter(GameStateUseCase gameStateUseCase, GameStateController gameStateController,
            ErrorPopupView errorPopupView)
        {
            _gameStateUseCase = gameStateUseCase;
            _gameStateController = gameStateController;
            _errorPopupView = errorPopupView;
            _disposable = new CompositeDisposable();
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
                .AddTo(_disposable);
        }

        private async UniTask ExecStateAsync(GameState state, CancellationToken token)
        {
            try
            {
                var nextState = await _gameStateController.TickAsync(state, token);
                _gameStateUseCase.SetState(nextState);
            }
            catch (CustomPlayFabException e)
            {
                // TODO: エラーメッセージの修正
                UnityEngine.Debug.LogWarning($"[CustomPlayFabException]: {e}");
                await _errorPopupView.PopupAsync($"[CustomPlayFabException]: {e}", token);
                await ExecStateAsync(_gameStateUseCase.currentState, token);
            }
            catch (Exception e)
            {
                // TODO: エラーメッセージの修正
                UnityEngine.Debug.LogWarning($"[Exception]: {e}");
                await _errorPopupView.PopupAsync($"[Exception]: {e}", token);
                await ExecStateAsync(_gameStateUseCase.currentState, token);
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}