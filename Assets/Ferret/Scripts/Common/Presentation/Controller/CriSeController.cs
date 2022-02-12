using System.Threading;
using CriWare;
using Cysharp.Threading.Tasks;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.Common.Utility;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class CriSeController : BaseCriAtomSource, ISeController
    {
        public async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.WaitWhile(() => CriAtom.CueSheetsAreLoading, cancellationToken: token);
            SetCueSheet(SeConfig.CUE_SHEET_NAME);
            SetLoop(false);
        }

        public void Play(SeType seType)
        {
            PlayCue(seType.ConvertCueName());
        }

        public void SetVolume(float value)
        {
            SetVolumeSource(value);
        }
    }
}