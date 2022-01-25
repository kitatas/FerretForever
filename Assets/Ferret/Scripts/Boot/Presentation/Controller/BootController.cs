using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Boot.Domain.UseCase;
using UnityEngine;
using VContainer.Unity;

namespace Ferret.Boot.Presentation.Controller
{
    public sealed class BootController : IPostInitializable, IDisposable
    {
        private readonly LoginUseCase _loginUseCase;

        private readonly CancellationTokenSource _tokenSource;

        public BootController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
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
                        // TODO: 入力待ち

                        // 名前登録
                        // TODO: viewから受け取る
                        await _loginUseCase.RegisterUserNameAsync("test user", _tokenSource.Token);
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