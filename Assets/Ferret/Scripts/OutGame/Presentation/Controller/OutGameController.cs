using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.OutGame.Domain.UseCase;
using Ferret.OutGame.Presentation.View;
using VContainer.Unity;

namespace Ferret.OutGame.Presentation.Controller
{
    public sealed class OutGameController : IPostInitializable, IDisposable
    {
        private readonly RankingDataUseCase _rankingDataUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly IBgmController _bgmController;
        private readonly SceneLoader _sceneLoader;
        private readonly InputView _inputView;
        private readonly RankingView _rankingView;
        private readonly RecordView _recordView;

        private readonly CancellationTokenSource _tokenSource;

        public OutGameController(RankingDataUseCase rankingDataUseCase, UserRecordUseCase userRecordUseCase,
            IBgmController bgmController, SceneLoader sceneLoader, InputView inputView, RankingView rankingView, RecordView recordView)
        {
            _rankingDataUseCase = rankingDataUseCase;
            _userRecordUseCase = userRecordUseCase;
            _bgmController = bgmController;
            _sceneLoader = sceneLoader;
            _inputView = inputView;
            _rankingView = rankingView;
            _recordView = recordView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            InitAsync(_tokenSource.Token).Forget();
        }

        private async UniTask InitAsync(CancellationToken token)
        {
            var rankingData = await _rankingDataUseCase.GetRankDataAsync(token);
            _rankingView.SetData(rankingData);

            _recordView.SetHighRecord(_userRecordUseCase.GetHighRecord());
            _recordView.SetCurrentRecord(_userRecordUseCase.GetCurrentRecord());

            _sceneLoader.LoadingFadeOut();

            _bgmController.Play(BgmType.Result, true);

            await _inputView.OnClickAsync(token);

            _sceneLoader.LoadScene(SceneName.Main);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}