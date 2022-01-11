using EFUK;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class GimmickController
    {
        private readonly BalloonPoolUseCase _balloonPoolUseCase;
        private readonly EnemyPoolUseCase _enemyPoolUseCase;
        private readonly PlayerPoolUseCase _playerPoolUseCase;
        private readonly PlayerContainerUseCase _playerContainerUseCase;
        private readonly PlayerCountUseCase _playerCountUseCase;
        private readonly GroundController _groundController;
        private readonly BridgeView _bridgeView;

        private int _counter;
        private int _initCounter;

        private readonly int _interval = 86;

        public GimmickController(BalloonPoolUseCase balloonPoolUseCase,
            EnemyPoolUseCase enemyPoolUseCase, PlayerPoolUseCase playerPoolUseCase,
            PlayerContainerUseCase playerContainerUseCase, PlayerCountUseCase playerCountUseCase,
            GroundController groundController, BridgeView bridgeView)
        {
            _balloonPoolUseCase = balloonPoolUseCase;
            _enemyPoolUseCase = enemyPoolUseCase;
            _playerPoolUseCase = playerPoolUseCase;
            _playerContainerUseCase = playerContainerUseCase;
            _playerCountUseCase = playerCountUseCase;
            _groundController = groundController;
            _bridgeView = bridgeView;

            _counter = 0;
            _initCounter = 0;
        }

        public void Init()
        {
            for (int i = 0; i < InGameConfig.INIT_PLAYER_COUNT; i++)
            {
                var position = new Vector3(-3.0f - i, 2.0f, -0.01f * i);
                _playerPoolUseCase.Rent(position);
            }

            _groundController.Init(SetUp);
            _bridgeView.Init();
        }

        private void SetUp(GroundView groundView)
        {
            var ground = groundView.gameObject;
            groundView.Activate(true);

            _counter++;
            if (_counter % _interval == 0)
            {
                _counter = 0;
                ground.SetChild(_bridgeView.gameObject);
                _bridgeView.SetUp();
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
            if ((_counter % _interval).IsBetween(_interval - 3, _interval - 1) ||
                (_counter % _interval) == 4)
            {
                return;
            }

            // ギミック生成
            var rand = Random.Range(0, 60);
            if (rand.IsBetween(0, 1))
            {
                var balloon = _balloonPoolUseCase.Rent(BalloonType.Five);
                ground.SetChild(balloon.gameObject);
                balloon.SetUp(x =>
                {
                    _playerPoolUseCase.Increase(x);
                    _playerCountUseCase.Increase(x.type.ConvertInt());
                });
            }
            else if (rand.IsBetween(2, 3))
            {
                var balloon = _balloonPoolUseCase.Rent(BalloonType.Ten);
                ground.SetChild(balloon.gameObject);
                balloon.SetUp(x =>
                {
                    _playerPoolUseCase.Increase(x);
                    _playerCountUseCase.Increase(x.type.ConvertInt());
                });
            }
            else if (rand.IsBetween(4, 5))
            {
                var enemy = _enemyPoolUseCase.Rent(EnemyType.Wolf);
                ground.SetChild(enemy.gameObject);
                enemy.SetUp(x =>
                {
                    _playerPoolUseCase.Decrease(x);
                    _playerCountUseCase.Decrease();
                });
            }
            else if (rand.IsBetween(6, 7))
            {
                var enemy = _enemyPoolUseCase.Rent(EnemyType.Hawk);
                ground.SetChild(enemy.gameObject);
                enemy.SetUp(x =>
                {
                    _playerPoolUseCase.Decrease(x);
                    _playerCountUseCase.Decrease();
                });
            }
        }

        public void Tick(float deltaTime)
        {
            _groundController.Tick(deltaTime);
        }

        public void SetUpNext()
        {
            _bridgeView.SetUpNext();
        }

        public bool IsNoPlayer()
        {
            return _playerContainerUseCase.IsNone();
        }

        public void JumpAll()
        {
            _playerContainerUseCase.JumpAll();
        }

        public bool IsArriveBridge()
        {
            return _bridgeView.isArrive;
        }
    }
}