using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using UnityEngine;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class EnemyContainerUseCase
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyRepository _enemyRepository;

        public EnemyContainerUseCase(EnemyFactory enemyFactory, EnemyRepository enemyRepository)
        {
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
        }

        public GameObject Generate(EnemyType type)
        {
            var enemy = _enemyFactory.Rent(_enemyRepository.Find(type).enemy);
            enemy.SetUp();
            return enemy.gameObject;
        }
    }
}