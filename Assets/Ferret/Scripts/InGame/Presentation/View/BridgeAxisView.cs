using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EFUK;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class BridgeAxisView : MonoBehaviour
    {
        private List<PlayerController> _victims;
        private readonly Vector3 _setUpPosition = new Vector3(0.5f, -1.4f, 0.0f);
        private readonly Vector3 _rotateVector = new Vector3(0.0f, 0.0f, -90.0f);

        public void Init()
        {
            _victims = new List<PlayerController>();
        }

        public void SetUp()
        {
            transform.eulerAngles = Vector3.zero;
            transform.localPosition = _setUpPosition;
            _victims.Clear();
        }

        public void CreateBridge(PlayerController victim)
        {
            gameObject.SetChild(victim.gameObject);
            _victims.Add(victim);
            victim.SetUpBridge();
        }

        public async UniTask BuildBridgeAsync(CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(transform
                    .DOLocalRotate(_rotateVector, 1.0f)
                    .SetEase(Ease.InCirc))
                .Join(transform
                    .DOLocalMoveY(-1.9f, 1.0f)
                    .SetEase(Ease.InCirc))
                .WithCancellation(token);

            foreach (var victim in _victims)
            {
                victim.ConvertBridge();
            }
        }

        public async UniTask BuildBridgeFailedAsync(CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(transform
                    .DOLocalRotate(_rotateVector, 0.6f)
                    .SetEase(Ease.InCirc))
                .Append(transform
                    .DOLocalMoveY(-4.0f, 0.4f)
                    .SetEase(Ease.InCirc))
                .WithCancellation(token);
        }
    }
}