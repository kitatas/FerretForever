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
                UpdateNameAsync(x, _tokenSource.Token).Forget();
            });
        }

        private async UniTaskVoid UpdateNameAsync(string name,  CancellationToken token)
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
            catch (Exception e)
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