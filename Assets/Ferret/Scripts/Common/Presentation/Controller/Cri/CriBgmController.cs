using System.Threading;
using CriWare;
using Cysharp.Threading.Tasks;
using UniEx;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class CriBgmController : BaseCriAtomSource, IBgmController
    {
        public async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.WaitWhile(() => CriAtom.CueSheetsAreLoading, cancellationToken: token);
            SetCueSheet(BgmConfig.CUE_SHEET_NAME);
        }

        public void Play(BgmType bgmType, bool isLoop)
        {
            if (IsSourceStatus(CriAtomSource.Status.Playing))
            {
                Stop();
            }

            SetLoop(isLoop);
            // loop設定直後に反映されないための措置
            this.DelayFrame(15, () => PlayCue(bgmType.ConvertCueName()));
        }

        public void Stop()
        {
            StopSource();
        }

        public void SetVolume(float value)
        {
            SetVolumeSource(value);
        }
    }
}