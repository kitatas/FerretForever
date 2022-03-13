using System;
using Ferret.Common;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.Common.Presentation.View;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class InGameController : IPostInitializable, IDisposable
    {
        private readonly AchievementUseCase _achievementUseCase;
        private readonly LanguageUseCase _languageUseCase;
        private readonly LanguageTypeUseCase _languageTypeUseCase;
        private readonly SaveDataUseCase _saveDataUseCase;
        private readonly IBgmController _bgmController;
        private readonly ISeController _seController;
        private readonly AchievementView _achievementView;
        private readonly LanguageView _languageView;
        private readonly VolumeView _volumeView;
        private readonly CompositeDisposable _disposable;

        public InGameController(AchievementUseCase achievementUseCase, LanguageUseCase languageUseCase,
            LanguageTypeUseCase languageTypeUseCase, SaveDataUseCase saveDataUseCase, IBgmController bgmController,
            ISeController seController, AchievementView achievementView, LanguageView languageView,
            VolumeView volumeView)
        {
            _achievementUseCase = achievementUseCase;
            _languageUseCase = languageUseCase;
            _languageTypeUseCase = languageTypeUseCase;
            _saveDataUseCase = saveDataUseCase;
            _bgmController = bgmController;
            _seController = seController;
            _achievementView = achievementView;
            _languageView = languageView;
            _volumeView = volumeView;
            _disposable = new CompositeDisposable();
        }

        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);

                if (button is LanguageButtonView languageButton)
                {
                    languageButton.push += () => _languageTypeUseCase.SetLanguage(languageButton.languageType);
                }
            }

            InitVolume();
            InitLanguage();
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

        private void InitLanguage()
        {
            _languageTypeUseCase.SetLanguage(_saveDataUseCase.GetLanguageType());

            _languageTypeUseCase.language
                .Subscribe(x =>
                {
                    var (mainSceneData, hintImage) = _languageUseCase.FindData(x);
                    _languageView.Display(mainSceneData);
                    _languageView.SetHint(hintImage);
                    
                    _saveDataUseCase.SaveLanguage(x);
                    InitAchievement(x);
                })
                .AddTo(_languageView);
        }

        private void InitAchievement(LanguageType type)
        {
            var achievementData = _achievementUseCase.GetAchievementStatus();
            foreach (var data in achievementData)
            {
                data.detail = data.isAchieve
                    ? string.Format(GetAchievementDetail(data.type, type), data.value.ToString())
                    : AchievementConfig.DETAIL_SECRET;
            }

            _achievementView.SetData(achievementData);
        }

        private string GetAchievementDetail(AchievementType achievement, LanguageType languageType)
        {
            var screen = _languageUseCase.FindData(languageType).Item1.achievement;
            return achievement switch
            {
                AchievementType.PlayCount   => screen.playCount,
                AchievementType.HighScore   => screen.highScore,
                AchievementType.TotalScore  => screen.totalScore,
                AchievementType.TotalVictim => screen.totalVictimCount,
                _ => throw new ArgumentOutOfRangeException(nameof(achievement), achievement, null)
            };
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}