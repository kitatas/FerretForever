using System;
using EFUK;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ferret.InGame.Presentation.Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        private PlayerMoveUseCase _playerMoveUseCase;
        private Action<BalloonType> _increase;

        public PlayerStatus status { get; private set; }

        public void Init()
        {
            _playerMoveUseCase = new PlayerMoveUseCase(GetComponent<Rigidbody2D>());
            status = PlayerStatus.None;

            // 接地
            this.OnCollisionEnter2DAsObservable()
                .Where(_ => status == PlayerStatus.Jumping)
                .Where(other => other.gameObject.CompareTag(TagConfig.GROUND))
                .Subscribe(_ => status = PlayerStatus.Run)
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent<BalloonView>(out var balloon))
                    {
                        _increase?.Invoke(balloon.type);
                    }
                })
                .AddTo(this);
        }

        public void SetUp(Action<BalloonType> increase)
        {
            _increase = increase;
            status = PlayerStatus.Jumping;
        }

        public void Jump()
        {
            if (status == PlayerStatus.Run)
            {
                var delayTime = Random.Range(0f, 0.05f);
                this.Delay(delayTime, _playerMoveUseCase.Jump);

                status = PlayerStatus.Jump;
                this.DelayFrame(10, () => status = PlayerStatus.Jumping);
            }
        }
    }
}