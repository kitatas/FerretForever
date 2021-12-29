using System;
using System.Collections.Generic;
using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using Ferret.InGame.Presentation.Controller;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerContainerUseCase
    {
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerRepository _playerRepository;
        private readonly List<PlayerController> _players;

        public PlayerContainerUseCase(PlayerFactory playerFactory, PlayerRepository playerRepository)
        {
            _playerFactory = playerFactory;
            _playerRepository = playerRepository;
            _players = new List<PlayerController>();
        }

        public void Generate()
        {
            _playerFactory.Set(_playerRepository.Get().player);
            var player = _playerFactory.Rent();
            _players.Add(player);
        }

        public void JumpAll()
        {
            foreach (var player in _players)
            {
                player.Jump();
            }
        }

        public bool IsNone()
        {
            return _players.Count == 0;
        }
    }
}