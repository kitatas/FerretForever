using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class UserInfoPresenter : IPostInitializable, IDisposable
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly UserInfoView _userInfoView;
        private readonly CancellationTokenSource _tokenSource;

        public UserInfoPresenter(UserRecordUseCase userRecordUseCase, UserInfoView userInfoView)
        {
            _userRecordUseCase = userRecordUseCase;
            _userInfoView = userInfoView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _userInfoView.SetUserData(_userRecordUseCase.GetUserRecord());
            _userInfoView.InitButton(x =>
            {
                UniTask.Void(async _ =>
                {
                    await _userRecordUseCase.UpdateUserNameAsync(x, _tokenSource.Token);
                    _userInfoView.UpdateUserName();
                }, _tokenSource.Token);
            });
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}