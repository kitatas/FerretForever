using System;
using EFUK;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
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
    public sealed class PlayerController : MonoBehaviour, IPoolObject
    {
        private PlayerMoveUseCase _playerMoveUseCase;
        private PlayerView _playerView;
        private ISeController _seController;

        private Action _release;

        public PlayerStatus status { get; private set; }
        public Vector3 position => transform.position;
        public GameObject self => gameObject;

        public void Init(Action release, ISeController seController)
        {
            _playerMoveUseCase = new PlayerMoveUseCase(GetComponent<Rigidbody2D>());
            _playerView = GetComponent<PlayerView>();
            _release = release;
            _seController = seController;
            status = PlayerStatus.None;

            _playerView.Init();

            // 接地
            this.OnCollisionEnter2DAsObservable()
                .Where(_ => status == PlayerStatus.Jump)
                .Where(other => other.gameObject.CompareTag(TagConfig.GROUND))
                .Subscribe(_ => status = PlayerStatus.Run)
                .AddTo(this);
        }

        public void SetUp(Vector3 setUpPosition)
        {
            transform.position = setUpPosition;
            transform.eulerAngles = Vector3.zero;
            status = PlayerStatus.Jump;
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
                _seController.Play(SeType.Jump);
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
            _playerMoveUseCase.ConvertBridge();
        }

        public void CollapseBridge()
        {
            _playerMoveUseCase.Collapse();
        }

        public void Blow()
        {
            status = PlayerStatus.Blow;
            _playerMoveUseCase.Blow();
            this.Delay(5.0f, Release);
        }

        public void Release()
        {
            transform.SetParent(null);
            _release?.Invoke();
        }
    }
}