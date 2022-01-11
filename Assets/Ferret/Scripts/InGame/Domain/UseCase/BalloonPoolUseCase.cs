using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using Ferret.InGame.Presentation.Controller;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class BalloonPoolUseCase
    {
        private readonly BalloonFactory _balloonFactory;
        private readonly BalloonRepository _balloonRepository;

        public BalloonPoolUseCase(BalloonFactory balloonFactory, BalloonRepository balloonRepository)
        {
            _balloonFactory = balloonFactory;
            _balloonRepository = balloonRepository;
        }

        public BalloonController Rent(BalloonType type)
        {
            return _balloonFactory.Rent(_balloonRepository.Find(type).balloon);
        }
    }
}