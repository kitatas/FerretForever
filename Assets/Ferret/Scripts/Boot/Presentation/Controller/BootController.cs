using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Boot.Domain.UseCase;
using Ferret.Boot.Presentation.View;
using UnityEngine;
using VContainer.Unity;

namespace Ferret.Boot.Presentation.Controller
{
    public sealed class BootController : IPostInitializable, IDisposable
    {
        private readonly LoginUseCase _loginUseCase;

        private readonly CancellationTokenSource _tokenSource;
        private readonly NameRegistrationView _nameRegistrationView;

        public BootController(LoginUseCase loginUseCase, NameRegistrationView nameRegistrationView)
        {
            _loginUseCase = loginUseCase;
            _nameRegistrationView = nameRegistrationView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _nameRegistrationView.Init();
            Load();
        }

        private void Load()
        {
            try
            {
                UniTask.Void(async _ =>
                {
                    var response = await _loginUseCase.LoginAsync(_tokenSource.Token);

                    // TODO: 新規IDの場合
                    if (response.NewlyCreated)
                    {
                        Debug.Log($"new data: {response.PlayFabId}");
                    }
                    else
                    {
                        Debug.Log($"success: {response.PlayFabId}");
                    }

                    // 既存ユーザーの場合
                    if (_loginUseCase.SyncUserRecord(response))
                    {

                    }
                    // 新規ユーザーの場合
                    else
                    {
                        // 入力完了待ち
                        await _nameRegistrationView.DecisionNameAsync(_tokenSource.Token);

                        // 名前登録
                        await _loginUseCase.RegisterUserNameAsync(_nameRegistrationView.inputName, _tokenSource.Token);
                    }

                }, _tokenSource.Token);
            }
            catch (Exception e)
            {
                // TODO: viewに反映
                // TODO: viewから再ロード
                Debug.LogError($"{e}");
                throw;
            }
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource?.Dispose();
        }
    }
}