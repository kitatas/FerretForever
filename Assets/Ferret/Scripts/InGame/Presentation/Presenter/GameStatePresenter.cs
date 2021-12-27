using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private readonly CompositeDisposable _disposable;
        private readonly CancellationTokenSource _tokenSource;

        public GameStatePresenter(GameStateUseCase gameStateUseCase, GameStateController gameStateController)
        {
            _gameStateUseCase = gameStateUseCase;
            _gameStateController = gameStateController;
            _disposable = new CompositeDisposable();
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _gameStateController.InitAsync(_tokenSource.Token).Forget();

            _gameStateUseCase.gameState
                .Subscribe(x =>
                {
                    UniTask.Void(async _ =>
                    {
                        var nextState = await _gameStateController.TickAsync(x, _tokenSource.Token);
                        _gameStateUseCase.SetState(nextState);
                    }, _tokenSource.Token);
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}