using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller;
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
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly IBgmController _bgmController;
        private readonly ISeController _seController;
        private readonly SceneLoader _sceneLoader;
        private readonly AchievementView _achievementView;
        private readonly LanguageView _languageView;
        private readonly UserInfoView _userInfoView;
        private readonly VolumeView _volumeView;
        private readonly CancellationTokenSource _tokenSource;

        public InGameController(AchievementUseCase achievementUseCase, LanguageUseCase languageUseCase,
            LanguageTypeUseCase languageTypeUseCase, SaveDataUseCase saveDataUseCase,
            UserRecordUseCase userRecordUseCase, IBgmController bgmController, ISeController seController,
            SceneLoader sceneLoader, AchievementView achievementView, LanguageView languageView, VolumeView volumeView,
            UserInfoView userInfoView)
        {
            _achievementUseCase = achievementUseCase;
            _languageUseCase = languageUseCase;
            _languageTypeUseCase = languageTypeUseCase;
            _saveDataUseCase = saveDataUseCase;
            _userRecordUseCase = userRecordUseCase;
            _bgmController = bgmController;
            _seController = seController;
            _sceneLoader = sceneLoader;
            _achievementView = achievementView;
            _languageView = languageView;
            _volumeView = volumeView;
            _userInfoView = userInfoView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);

                switch (button)
                {
                    case LanguageButtonView languageButton:
                        languageButton.InitButton(_languageTypeUseCase.SetLanguage);
                        break;
                    case DeleteButtonView deleteButton:
                        deleteButton.InitButton(_saveDataUseCase.DeleteSaveData);
                        break;
                    case LoadButtonView loadButton:
                        loadButton.InitButton(x =>
                        {
                            _sceneLoader.FadeLoadScene(x);
                            _bgmController.Stop();
                        });
                        break;
                    case AppUrlButtonView appUrlButtonView:
                        appUrlButtonView.InitButton();
                        break;
                }
            }

            InitVolume();
            InitLanguage();
            InitUserInfo();
            _bgmController.Play(BgmType.Title, true);
        }

        private void InitVolume()
        {
            _volumeView.SetVolume(_saveDataUseCase.volume);

            _volumeView.bgmValueChanged
                .Subscribe(_bgmController.SetVolume)
                .AddTo(_volumeView);

            _volumeView.seValueChanged
                .Subscribe(_seController.SetVolume)
                .AddTo(_volumeView);

            _volumeView.bgmSliderPointerUp
                .Subscribe(_ =>
                {
                    _saveDataUseCase.SaveBgmVolume(_volumeView.bgmVolume);
                })
                .AddTo(_volumeView);

            _volumeView.seSliderPointerUp
                .Subscribe(_ =>
                {
                    _seController.Play(SeType.Button);
                    _saveDataUseCase.SaveSeVolume(_volumeView.seVolume);
                })
                .AddTo(_volumeView);
        }

        private void InitLanguage()
        {
            var achievementData = _achievementUseCase.GetAchievementStatus();

            _languageTypeUseCase.SetLanguage(_saveDataUseCase.languageType);
            _languageTypeUseCase.language
                .Subscribe(x =>
                {
                    _saveDataUseCase.SaveLanguage(x);

                    var mainData = _languageUseCase.FindMainData(x);
                    _languageView.Display(mainData);
                    _userInfoView.SetLanguage(mainData.mainScene.update);

                    foreach (var data in achievementData)
                    {
                        data.detail = data.isAchieve
                            ? string.Format(data.type.ConvertDetail(mainData.mainScene.achievement), data.value.ToString())
                            : AchievementConfig.DETAIL_SECRET;
                    }
                    _achievementView.SetData(achievementData);
                })
                .AddTo(_languageView);
        }

        private void InitUserInfo()
        {
            _userInfoView.SetUserData(_userRecordUseCase.GetUserRecord());
            _userInfoView.InitButton(x =>
            {
                UpdateNameAsync(x, _tokenSource.Token).Forget();
            });
        }

        private async UniTaskVoid UpdateNameAsync(string name, CancellationToken token)
        {
            try
            {
                var isSuccess = await _userRecordUseCase.UpdateUserNameAsync(name, token);
                if (isSuccess)
                {
                    _userInfoView.UpdateSuccessUserName();
                }
                else
                {
                    _userInfoView.UpdateFailedUserName();
                }
            }
            catch (Exception)
            {
                _userInfoView.UpdateFailedUserName();
                throw;
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}