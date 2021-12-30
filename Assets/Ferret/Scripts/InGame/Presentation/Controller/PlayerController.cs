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

        public void Init()
        {
            _playerMoveUseCase = new PlayerMoveUseCase(GetComponent<Rigidbody2D>());

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
        }

        public void Jump()
        {
            // TODO: 接地状態
            {
                var delayTime = Random.Range(0f, 0.05f);
                this.Delay(delayTime, _playerMoveUseCase.Jump);
            }
        }
    }
}