using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using UnityEngine;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class BalloonContainerUseCase
    {
        private readonly BalloonFactory _balloonFactory;
        private readonly BalloonRepository _balloonRepository;

        public BalloonContainerUseCase(BalloonFactory balloonFactory, BalloonRepository balloonRepository)
        {
            _balloonFactory = balloonFactory;
            _balloonRepository = balloonRepository;
        }

        public GameObject Generate(BalloonType type)
        {
            var balloon = _balloonFactory.Rent(_balloonRepository.Find(type).balloon);
            balloon.SetUp();
            return balloon.gameObject;
        }
    }
}