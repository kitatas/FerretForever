using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Domain.UseCase;
using VContainer;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class SeController : BaseAudioSource, ISeController
    {
        private ISeUseCase _seUseCase;

        [Inject]
        private void Construct(ISeUseCase seUseCase)
        {
            _seUseCase = seUseCase;
        }

        public async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public void Play(SeType seType)
        {
            var clip = _seUseCase.GetSe(seType);
            if (clip == null)
            {
                return;
            }

            _audioSource.PlayOneShot(clip);
        }

        public void SetVolume(float value)
        {
            _audioSource.volume = value;
        }
    }
}