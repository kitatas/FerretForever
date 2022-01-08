using System;
using System.Collections.Generic;
using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using Ferret.InGame.Presentation.Controller;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerContainerUseCase
    {
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerRepository _playerRepository;
        private readonly ReactiveCollection<PlayerController> _players;

        public PlayerContainerUseCase(PlayerFactory playerFactory, PlayerRepository playerRepository)
        {
            _playerFactory = playerFactory;
            _playerRepository = playerRepository;
            _players = new ReactiveCollection<PlayerController>();
        }

        public IReadOnlyReactiveCollection<PlayerController> players => _players;

        public void Generate(Vector3 position)
        {
            _playerFactory.Set(_playerRepository.Get().player);
            var player = _playerFactory.Rent();
            player.SetUp(position);
            _players.Add(player);
        }

        public void HitBalloon(BalloonController balloon)
        {
            var loop = balloon.type switch
            {
                BalloonType.Five => 5,
                BalloonType.Ten  => 10,
                _ => throw new ArgumentOutOfRangeException(nameof(balloon.type), balloon.type, null)
            };

            var balloonX = balloon.position.x;
            var y = balloon.position.y;
            for (int i = 0; i < loop; i++)
            {
                var x = Mathf.Clamp(Random.Range(balloonX - 2.0f, balloonX + 1.0f), -8.0f, 5.0f);
                var z = Random.Range(-1.0f, -0.1f);
                Generate(new Vector3(x, y, z));
            }
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