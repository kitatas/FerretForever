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

        public BalloonType type => balloonType;
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
                            // TODO: player生成

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

        public void SetUp()
        {
            transform.SetPositionY(Random.Range(1.5f, 6.0f));
        }
    }
}