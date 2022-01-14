using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EFUK;
using Ferret.InGame.Presentation.Controller;
using UnityEngine;

namespace Ferret.InGame.Presentation.View
{
    public sealed class BridgeAxisView : MonoBehaviour, IPoolObject
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
            Release();
        }

        public async UniTask CreateBridgeAsync(PlayerController victim, float height, CancellationToken token)
        {
            gameObject.SetChild(victim.gameObject);
            _victims.Add(victim);
            victim.SetUpBridge();

            await DOTween.Sequence()
                .Append(victim.transform
                    .DOLocalMoveX(0.0f, 0.05f))
                .Append(victim.transform
                    .DOLocalMoveY(height, 0.05f))
                .WithCancellation(token);
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
                .WithCancellation(token);

            foreach (var victim in _victims)
            {
                victim.CollapseBridge();
            }
        }

        public void Release()
        {
            foreach (var victim in _victims)
            {
                victim.Release();
            }

            _victims.Clear();
        }
    }
}