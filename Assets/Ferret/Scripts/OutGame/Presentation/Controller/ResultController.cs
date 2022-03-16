using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common;
using Ferret.Common.Presentation.View;
using Ferret.OutGame.Domain.UseCase;
using Ferret.OutGame.Presentation.View;

namespace Ferret.OutGame.Presentation.Controller
{
    public sealed class ResultController
    {
        private readonly LanguageUseCase _languageUseCase;
        private readonly RankingDataUseCase _rankingDataUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly LanguageView _languageView;
        private readonly RankingView _rankingView;
        private readonly RecordView _recordView;
        private readonly TweetButtonView _tweetButtonView;
        private readonly ErrorPopupView _errorPopupView;
        private readonly LoadingView _loadingView;

        public ResultController(LanguageUseCase languageUseCase, RankingDataUseCase rankingDataUseCase,
            UserRecordUseCase userRecordUseCase, LanguageView languageView, RankingView rankingView, RecordView recordView,
            TweetButtonView tweetButtonView, ErrorPopupView errorPopupView, LoadingView loadingView)
        {
            _languageUseCase = languageUseCase;
            _rankingDataUseCase = rankingDataUseCase;
            _userRecordUseCase = userRecordUseCase;
            _languageView = languageView;
            _rankingView = rankingView;
            _recordView = recordView;
            _tweetButtonView = tweetButtonView;
            _errorPopupView = errorPopupView;
            _loadingView = loadingView;
        }

        public async UniTask InitViewAsync(CancellationToken token)
        {
            // ランキング更新待ち
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            try
            {
                var rankingData = await _rankingDataUseCase.GetRankDataAsync(token);
                _rankingView.SetData(rankingData, _userRecordUseCase.GetUid());

                var currentRecord = _userRecordUseCase.GetCurrentRecord();
                _recordView.SetRecord(_userRecordUseCase.GetHighRecord(), currentRecord);

                var resultScene = _languageUseCase.GetResultSceneData();
                _languageView.Display(resultScene);

                var tweetMessage = string.Format(resultScene.tweet,
                    currentRecord.victimCount.ToString(), currentRecord.score.ToString("F2"));
                _tweetButtonView.InitTweet(tweetMessage);

                _loadingView.Activate(false);
            }
            catch (Exception e)
            {
                _loadingView.Activate(false);
                await _errorPopupView.PopupAsync($"{e.ConvertErrorMessage()}", token);
                await InitViewAsync(token);
            }
        }
    }
}