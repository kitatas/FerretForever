using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class EnemyController : MonoBehaviour, IPoolObject
    {
        [SerializeField] private EnemyType enemyType = default;

        private Action _release;
        private Action<PlayerController> _decrease;
        private Vector3 _setUpPosition;

        public EnemyType type => enemyType;
        public Vector3 position => transform.position;
        public GameObject self => gameObject;

        public void Init(Action release)
        {
            _release = release;
            var y = type switch
            {
                EnemyType.Wolf => 1.8f,
                EnemyType.Hawk => 6.3f,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            _setUpPosition = new Vector3(0.0f, y, 0.0f);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent<PlayerController>(out var player))
                    {
                        if (player.status != PlayerStatus.Blow)
                        {
                            player.Blow();
                            _decrease?.Invoke(player);
                        }
                    }
                })
                .AddTo(this);
        }

        public void SetUp(Action<PlayerController> decrease)
        {
            _decrease = decrease;
            transform.localPosition = _setUpPosition;
        }

        public void Release()
        {
            transform.SetParent(null);
            _release?.Invoke();
        }
    }
}