using System;
using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class BalloonController : MonoBehaviour, IPoolObject
    {
        [SerializeField] private BalloonType balloonType = default;

        private Action _release;
        private Action<BalloonController> _increase;
        private bool _isActive;

        public BalloonType type => balloonType;
        public Vector3 position => transform.position;
        public GameObject self => gameObject;

        public void Init(Action release)
        {
            _release = release;

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
                            Release();
                        }
                    }
                })
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

        public void Release()
        {
            transform.SetParent(null);
            _release?.Invoke();
        }
    }
}