using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ferret.InGame.Presentation.View
{
    public sealed class InputView : MonoBehaviour
    {
        [SerializeField] private Button mainButton = default;

        private readonly Subject<Unit> _push = new Subject<Unit>();
        public IObservable<Unit> OnPush() => _push;

        private ReactiveProperty<bool> _isPush;
        public bool isPush => _isPush.Value;

        public void Init()
        {
            mainButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _push.OnNext(Unit.Default);
                    _isPush.Value = true;
                })
                .AddTo(this);

            _isPush = new ReactiveProperty<bool>(false);
            _isPush
                .Where(x => x)
                .DelayFrame(1) // TODO: delayではない、1フレ無視にする
                // .ThrottleFirst()?
                .Subscribe(_ => _isPush.Value = false)
                .AddTo(this);
        }
    }
}