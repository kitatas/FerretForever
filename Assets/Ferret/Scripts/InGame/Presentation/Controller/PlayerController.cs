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
    [RequireComponent(typeof(PlayerView))]
    public sealed class PlayerController : MonoBehaviour
    {
        private PlayerMoveUseCase _playerMoveUseCase;
        private PlayerView _playerView;

        private Action<PlayerController> _backPool;

        public PlayerStatus status { get; private set; }

        public void Init()
        {
            _playerMoveUseCase = new PlayerMoveUseCase(GetComponent<Rigidbody2D>());
            _playerView = GetComponent<PlayerView>();
            status = PlayerStatus.None;

            _playerView.Init();

            // 接地
            this.OnCollisionEnter2DAsObservable()
                .Where(_ => status == PlayerStatus.Jumping)
                .Where(other => other.gameObject.CompareTag(TagConfig.GROUND))
                .Subscribe(_ => status = PlayerStatus.Run)
                .AddTo(this);

            // 画面外に出たら強制的にpoolに戻す
            this.UpdateAsObservable()
                .Where(_ => transform.position.x < -10.0f)
                .Where(_ => gameObject.activeSelf)
                .Subscribe(_ =>
                {
                    gameObject.transform.SetParent(null);
                    _backPool?.Invoke(this);
                })
                .AddTo(this);
        }

        public void SetUp(Vector3 position, Action<PlayerController> backPool)
        {
            transform.position = position;
            transform.eulerAngles = Vector3.zero;
            _backPool = backPool;
            status = PlayerStatus.Jumping;
            _playerMoveUseCase.SetConstraint(RigidbodyConstraints2D.FreezeRotation);
            _playerView.SetUp();
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

        public void SetUpBridge()
        {
            status = PlayerStatus.Bridge;
            _playerMoveUseCase.SetSimulate(false);
            _playerView.SetUpBridge();
        }

        public void ConvertBridge()
        {
            _playerMoveUseCase.SetSimulate(true);
            _playerMoveUseCase.SetConstraint(RigidbodyConstraints2D.FreezeAll);
        }
    }
}