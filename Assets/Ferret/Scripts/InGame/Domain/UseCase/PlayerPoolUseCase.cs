using Ferret.InGame.Data.Container;
using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class PlayerPoolUseCase
    {
        private readonly PlayerContainer _playerContainer;
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerRepository _playerRepository;

        public PlayerPoolUseCase(PlayerContainer playerContainer, PlayerFactory playerFactory, PlayerRepository playerRepository)
        {
            _playerContainer = playerContainer;
            _playerFactory = playerFactory;
            _playerRepository = playerRepository;
        }

        public void Generate(Vector3 position)
        {
            _playerFactory.Set(_playerRepository.Get().player);
            var player = _playerFactory.Rent();
            player.SetUp(position, _playerFactory.Return);
            _playerContainer.Add(player);
        }

        public void HitBalloon(BalloonController balloon)
        {
            var balloonX = balloon.position.x;
            var y = balloon.position.y;
            for (int i = 0; i < balloon.type.ConvertInt(); i++)
            {
                var x = Mathf.Clamp(Random.Range(balloonX - 2.0f, balloonX + 1.0f), -8.0f, 5.0f);
                var z = Random.Range(-1.0f, -0.1f);
                Generate(new Vector3(x, y, z));
            }
        }

        public void Decrease(PlayerController player)
        {
            _playerContainer.Remove(player);
            _playerFactory.Return(player);
        }
    }
}