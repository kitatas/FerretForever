using Ferret.Common.Presentation.View;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class InGameController : IPostInitializable
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly UserInfoView _userInfoView;

        public InGameController(UserRecordUseCase userRecordUseCase, UserInfoView userInfoView)
        {
            _userRecordUseCase = userRecordUseCase;
            _userInfoView = userInfoView;
        }

        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseUiButtonView>())
            {
                button.Init();
            }

            _userInfoView.SetUserData(_userRecordUseCase.GetUserRecord());
        }
    }
}