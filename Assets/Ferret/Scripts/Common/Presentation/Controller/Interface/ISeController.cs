using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ferret.Common.Presentation.Controller.Interface
{
    public interface ISeController
    {
        UniTask InitAsync(CancellationToken token);
        void Play(SeType seType);
        void SetVolume(float value);
    }
}