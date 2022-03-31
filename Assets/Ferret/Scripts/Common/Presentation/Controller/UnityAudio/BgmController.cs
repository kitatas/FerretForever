using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Domain.UseCase;
using VContainer;

namespace Ferret.Common.Presentation.Controller
{
    public sealed class BgmController : BaseAudioSource, IBgmController
    {
        private IBgmUseCase _bgmUseCase;

        [Inject]
        private void Construct(IBgmUseCase bgmUseCase)
        {
            _bgmUseCase = bgmUseCase;
        }

        public async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public void Play(BgmType bgmType, bool isLoop)
        {
            var clip = _bgmUseCase.GetBgm(bgmType);
            if (clip == null)
            {
                return;
            }

            _audioSource.clip = clip;
            _audioSource.loop = isLoop;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void SetVolume(float value)
        {
            _audioSource.volume = value;
        }
    }
}