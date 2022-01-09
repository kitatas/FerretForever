using System;
using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class BalloonController : MonoBehaviour
    {
        [SerializeField] private BalloonType balloonType = default;

        private Action<BalloonController> _increase;
        private bool _isActive;

        public BalloonType type => balloonType;
        public Vector3 position => transform.position;

        public void Init(Action release)
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(_ => _isActive)
                .Subscribe(other =>
                {
                    if (other.TryGetComponent<PlayerController>(out var player))
                    {
                        if (player.status != PlayerStatus.Blow)
                        {
                            _isActive = false;

                            // player生成
                            _increase?.Invoke(this);

                            // back pool
                            release?.Invoke();
                        }
                    }
                })
                .AddTo(this);

            // 画面外に出たら強制的にpoolに戻す
            this.UpdateAsObservable()
                .Where(_ => position.x < -11.0f)
                .Where(_ => gameObject.activeSelf)
                .Subscribe(_ => release?.Invoke())
                .AddTo(this);
        }

        public void SetUp(Action<BalloonController> increase)
        {
            _increase = increase;
            _isActive = true;

            transform
                .SetPositionY(Random.Range(1.0f, 5.5f))
                .SetLocalPositionX(0.0f);
        }
    }
}