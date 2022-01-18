using EFUK;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class GimmickController
    {
        private readonly BalloonPoolUseCase _balloonPoolUseCase;
        private readonly EffectPoolUseCase _effectPoolUseCase;
        private readonly EnemyPoolUseCase _enemyPoolUseCase;
        private readonly PlayerPoolUseCase _playerPoolUseCase;
        private readonly PlayerCountUseCase _playerCountUseCase;
        private readonly GroundController _groundController;
        private readonly BridgeView _bridgeView;
        private readonly BridgeAxisView _bridgeAxisView;

        private int _counter;
        private int _initCounter;

        private readonly int _interval = 86;

        public GimmickController(BalloonPoolUseCase balloonPoolUseCase, EffectPoolUseCase effectPoolUseCase,
            EnemyPoolUseCase enemyPoolUseCase, PlayerPoolUseCase playerPoolUseCase, PlayerCountUseCase playerCountUseCase,
            GroundController groundController, BridgeView bridgeView, BridgeAxisView bridgeAxisView)
        {
            _balloonPoolUseCase = balloonPoolUseCase;
            _effectPoolUseCase = effectPoolUseCase;
            _enemyPoolUseCase = enemyPoolUseCase;
            _playerPoolUseCase = playerPoolUseCase;
            _playerCountUseCase = playerCountUseCase;
            _groundController = groundController;
            _bridgeView = bridgeView;
            _bridgeAxisView = bridgeAxisView;

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
            _bridgeAxisView.Init();
        }

        private void SetUp(GroundView groundView)
        {
            groundView.SetUp();

            _counter++;
            if (_counter % _interval == 0)
            {
                _counter = 0;
                groundView.SavePool(_bridgeView);
                _bridgeView.SetUp();
                _bridgeAxisView.SetUp();
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
                groundView.SavePool(balloon);
                balloon.SetUp(x =>
                {
                    _playerPoolUseCase.Increase(x);
                    _playerCountUseCase.Increase(x.type.ConvertInt());
                    var effect = _effectPoolUseCase.Rent(EffectType.Crash);
                    groundView.SavePool(effect);
                    effect.Play(balloon.position, EffectColor.Green);
                });
            }
            else if (rand.IsBetween(2, 3))
            {
                var balloon = _balloonPoolUseCase.Rent(BalloonType.Ten);
                groundView.SavePool(balloon);
                balloon.SetUp(x =>
                {
                    _playerPoolUseCase.Increase(x);
                    _playerCountUseCase.Increase(x.type.ConvertInt());
                    var effect = _effectPoolUseCase.Rent(EffectType.Crash);
                    groundView.SavePool(effect);
                    effect.Play(balloon.position, EffectColor.Magenta);
                });
            }
            else if (rand.IsBetween(4, 5))
            {
                var enemy = _enemyPoolUseCase.Rent(EnemyType.Wolf);
                groundView.SavePool(enemy);
                enemy.SetUp(x =>
                {
                    if (_playerPoolUseCase.IsDecrease(x))
                    {
                        _playerCountUseCase.Decrease();
                        var effect = _effectPoolUseCase.Rent(EffectType.Explode);
                        groundView.SavePool(effect);
                        effect.Play(x.position, EffectColor.White);
                    }
                });
            }
            else if (rand.IsBetween(6, 7))
            {
                var enemy = _enemyPoolUseCase.Rent(EnemyType.Hawk);
                groundView.SavePool(enemy);
                enemy.SetUp(x =>
                {
                    if (_playerPoolUseCase.IsDecrease(x))
                    {
                        _playerCountUseCase.Decrease();
                        var effect = _effectPoolUseCase.Rent(EffectType.Explode);
                        groundView.SavePool(effect);
                        effect.Play(x.position, EffectColor.White);
                    }
                });
            }
        }

        public void Tick(GameState state, float deltaTime)
        {
            _groundController.Tick(state, deltaTime);
        }

        public void SetUpNext()
        {
            _bridgeView.SetUpNext();
        }

        public bool IsArriveBridge()
        {
            return _bridgeView.isArrive;
        }
    }
}