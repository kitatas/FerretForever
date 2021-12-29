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

        public void Init()
        {
            mainButton
                .OnClickAsObservable()
                .Subscribe(_ => _push.OnNext(Unit.Default))
                .AddTo(this);
        }
    }
}