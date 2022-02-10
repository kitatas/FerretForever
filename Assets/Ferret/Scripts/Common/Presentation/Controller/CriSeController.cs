using System;
using System.Threading;
using CriWare;
using Cysharp.Threading.Tasks;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.Common.Utility;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class CriSeController : ISeController, IDisposable
    {
        private CriAtomExPlayer _criAtomExPlayer;
        private CriAtomExAcb _criAtomExAcb;

        public CriSeController()
        {
        }

        public async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.WaitWhile(() => CriAtom.CueSheetsAreLoading, cancellationToken: token);
            _criAtomExPlayer = new CriAtomExPlayer();
            _criAtomExPlayer.Loop(false);
            _criAtomExAcb = CriAtom.GetAcb(SeConfig.CUE_SHEET_NAME);
        }

        public void Play(SeType seType)
        {
            _criAtomExPlayer.SetCue(_criAtomExAcb, seType.ConvertCueName());
            _criAtomExPlayer.Start();
        }

        public void SetVolume(float value)
        {
            _criAtomExPlayer.SetVolume(value);
        }

        public void Dispose()
        {
            _criAtomExPlayer?.Dispose();
            _criAtomExAcb?.Dispose();
        }
    }
}