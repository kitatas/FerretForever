using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using Ferret.InGame.Presentation.View;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class EffectPoolUseCase
    {
        private readonly EffectFactory _effectFactory;
        private readonly EffectRepository _effectRepository;

        public EffectPoolUseCase(EffectFactory effectFactory, EffectRepository effectRepository)
        {
            _effectFactory = effectFactory;
            _effectRepository = effectRepository;
        }

        public EffectView Rent(EffectType type)
        {
            return _effectFactory.Rent(_effectRepository.Find(type).effect);
        }
    }
}