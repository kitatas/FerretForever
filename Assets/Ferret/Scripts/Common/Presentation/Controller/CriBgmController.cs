using System;
using System.Threading;
using CriWare;
using Cysharp.Threading.Tasks;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.Common.Utility;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class CriBgmController : IBgmController, IDisposable
    {
        private CriAtomExPlayer _criAtomExPlayer;
        private CriAtomExAcb _criAtomExAcb;

        public CriBgmController()
        {
        }

        public async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.WaitWhile(() => CriAtom.CueSheetsAreLoading, cancellationToken: token);
            _criAtomExPlayer = new CriAtomExPlayer();
            _criAtomExAcb = CriAtom.GetAcb(BgmConfig.CUE_SHEET_NAME);
        }

        public void Play(BgmType bgmType, bool isLoop)
        {
            if (_criAtomExPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                _criAtomExPlayer.Stop();
            }

            _criAtomExPlayer.SetCue(_criAtomExAcb, bgmType.ConvertCueName());
            _criAtomExPlayer.Loop(isLoop);
            _criAtomExPlayer.Start();
        }

        public void Stop()
        {
            _criAtomExPlayer.Stop();
        }

        public void Dispose()
        {
            _criAtomExPlayer?.Dispose();
            _criAtomExAcb?.Dispose();
        }
    }
}