using EFUK;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class GimmickController
    {
        private readonly BalloonContainerUseCase _balloonContainerUseCase;
        private readonly EnemyContainerUseCase _enemyContainerUseCase;

        private int _counter;
        private int _initCounter;

        private readonly int _interval = 86;

        public GimmickController(BalloonContainerUseCase balloonContainerUseCase, EnemyContainerUseCase enemyContainerUseCase)
        {
            _balloonContainerUseCase = balloonContainerUseCase;
            _enemyContainerUseCase = enemyContainerUseCase;

            _counter = 0;
            _initCounter = 0;
        }

        public void SetUp(GroundView groundView)
        {
            groundView.Activate(true);

            _counter++;
            if (_counter % _interval == 0)
            {
                _counter = 0;
                // TODO: 橋の看板表示
                return;
            }

            // 橋を作るための空き
            if ((_counter % _interval).IsBetween(1, 3))
            {
                // 開始直後は空きを作らない
                if (++_initCounter <= 3)
                {
                    return;
                }

                groundView.Activate(false);
                return;
            }

            // 橋の前後にはギミックなし
            if ((_counter % _interval).IsBetween(-3, 4))
            {
                return;
            }

            // ギミック生成
            var ground = groundView.gameObject;
            var rand = Random.Range(0, 60);
            if (rand.IsBetween(0, 1))
            {
                var balloon = _balloonContainerUseCase.Generate(BalloonType.Five);
                ground.SetChild(balloon);
                balloon.transform.SetLocalPositionX(0.0f);
            }
            else if (rand.IsBetween(2, 3))
            {
                var balloon = _balloonContainerUseCase.Generate(BalloonType.Ten);
                ground.SetChild(balloon);
                balloon.transform.SetLocalPositionX(0.0f);
            }
            else if (rand.IsBetween(4, 5))
            {
                var enemy = _enemyContainerUseCase.Generate(EnemyType.Wolf);
                ground.SetChild(enemy);
                enemy.transform.localPosition = new Vector3(0.0f, 1.3f, 0.0f);
            }
            else if (rand.IsBetween(6, 7))
            {
                var enemy = _enemyContainerUseCase.Generate(EnemyType.Hawk);
                ground.SetChild(enemy);
                enemy.transform.localPosition = new Vector3(0.0f, 6.0f, 0.0f);
            }
        }
    }
}