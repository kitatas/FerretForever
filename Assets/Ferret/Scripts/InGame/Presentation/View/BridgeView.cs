using EFUK;
using Ferret.InGame.Presentation.Controller;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class BridgeView : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Collider2D collider2d = default;

        public bool isArrive { get; private set; }

        public void Init()
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(_ => isArrive == false)
                .Subscribe(other =>
                {
                    if (other.TryGetComponent<PlayerController>(out var player))
                    {
                        isArrive = true;
                        collider2d.enabled = false;
                    }
                })
                .AddTo(this);
        }

        public void SetUpNext()
        {
            isArrive = false;
        }

        public void SetUp()
        {
            transform.SetLocalPositionX(0.0f);
            collider2d.enabled = true;
        }

        public void Release()
        {
            transform.SetParent(null);
        }
    }
}