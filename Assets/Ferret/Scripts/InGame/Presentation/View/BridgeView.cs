using EFUK;
using Ferret.InGame.Presentation.Controller;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class BridgeView : MonoBehaviour
    {
        private Collider2D _collider;
        
        public bool isArrive { get; private set; }

        public void Init()
        {
            _collider = GetComponent<Collider2D>();
            
            Activate(false);

            this.OnTriggerEnter2DAsObservable()
                .Where(_ => isArrive == false)
                .Subscribe(other =>
                {
                    if (other.TryGetComponent<PlayerController>(out var player))
                    {
                        isArrive = true;
                        _collider.enabled = false;
                    }
                })
                .AddTo(this);

            // 画面外に出たら強制的にpoolに戻す
            this.UpdateAsObservable()
                .Where(_ => transform.position.x < -11.0f)
                .Where(_ => gameObject.activeSelf)
                .Subscribe(_ => Activate(false))
                .AddTo(this);
        }

        public void SetUpNext()
        {
            isArrive = false;
        }

        public void SetUp()
        {
            transform.SetLocalPositionX(0.0f);
            _collider.enabled = true;
            Activate(true);
        }

        private void Activate(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}