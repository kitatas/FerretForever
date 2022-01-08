using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType = default;

        public EnemyType type => enemyType;
        public Vector3 position => transform.position;

        public void Init(Action release)
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent<PlayerController>(out var player))
                    {
                        if (player.status != PlayerStatus.Blow)
                        {
                            // TODO: player吹っ飛ばす
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

        public void SetUp()
        {

        }
    }
}