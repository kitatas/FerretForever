using System;
using Ferret.InGame.Data.Entity;
using UniRx;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class GameStateUseCase
    {
        private readonly GameStateEntity _gameStateEntity;
        private readonly ReactiveProperty<GameState> _gameState;

        public GameStateUseCase(GameStateEntity gameStateEntity)
        {
            _gameStateEntity = gameStateEntity;
            _gameState = new ReactiveProperty<GameState>(_gameStateEntity.Get());
        }

        public IObservable<GameState> gameState => _gameState.Where(x => x != GameState.None);

        public void SetState(GameState state)
        {
            _gameStateEntity.Set(state);
            _gameState.Value = _gameStateEntity.Get();
        }

        public GameState currentState => _gameStateEntity.Get();
    }
}