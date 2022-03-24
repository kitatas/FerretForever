using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ferret.Common.Presentation.Controller
{
    public interface IBgmController
    {
        UniTask InitAsync(CancellationToken token);
        void Play(BgmType bgmType, bool isLoop);
        void Stop();
        void SetVolume(float value);
    }
}