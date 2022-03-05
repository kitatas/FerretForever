using System;
using Ferret.Common;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.Common.Presentation.View;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class InGameController : IPostInitializable, IDisposable
    {
        private readonly SaveDataUseCase _saveDataUseCase;
        private readonly IBgmController _bgmController;
        private readonly ISeController _seController;
        private readonly VolumeView _volumeView;
        private readonly CompositeDisposable _disposable;

        public InGameController(SaveDataUseCase saveDataUseCase, IBgmController bgmController,
            ISeController seController, VolumeView volumeView)
        {
            _saveDataUseCase = saveDataUseCase;
            _bgmController = bgmController;
            _seController = seController;
            _volumeView = volumeView;
            _disposable = new CompositeDisposable();
        }

        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);
            }

            InitVolume();
            _bgmController.Play(BgmType.Title, true);
        }

        private void InitVolume()
        {
            _volumeView.SetBgmVolume(_saveDataUseCase.GetBgmVolume());
            _volumeView.SetSeVolume(_saveDataUseCase.GetSeVolume());

            _volumeView.bgmValueChanged
                .Subscribe(_bgmController.SetVolume)
                .AddTo(_disposable);

            _volumeView.seValueChanged
                .Subscribe(_seController.SetVolume)
                .AddTo(_disposable);

            _volumeView.bgmSliderPointerUp
                .Subscribe(_ =>
                {
                    _saveDataUseCase.SaveBgmVolume(_volumeView.bgmVolume);
                })
                .AddTo(_disposable);

            _volumeView.seSliderPointerUp
                .Subscribe(x =>
                {
                    _seController.Play(SeType.Button);
                    _saveDataUseCase.SaveSeVolume(_volumeView.seVolume);
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}