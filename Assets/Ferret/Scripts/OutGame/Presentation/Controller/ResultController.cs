using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using Ferret.OutGame.Domain.UseCase;
using Ferret.OutGame.Presentation.View;

namespace Ferret.OutGame.Presentation.Controller
{
    public sealed class ResultController
    {
        private readonly LanguageUseCase _languageUseCase;
        private readonly SaveDataUseCase _saveDataUseCase;
        private readonly ResultUseCase _resultUseCase;
        private readonly LanguageView _languageView;
        private readonly RankingView _rankingView;
        private readonly RecordView _recordView;
        private readonly TweetButtonView _tweetButtonView;
        private readonly ErrorController _errorController;
        private readonly LoadingView _loadingView;

        public ResultController(LanguageUseCase languageUseCase, SaveDataUseCase saveDataUseCase,
            ResultUseCase resultUseCase, ErrorController errorController, LanguageView languageView,
            RankingView rankingView, RecordView recordView, TweetButtonView tweetButtonView, LoadingView loadingView)
        {
            _languageUseCase = languageUseCase;
            _saveDataUseCase = saveDataUseCase;
            _resultUseCase = resultUseCase;
            _errorController = errorController;
            _languageView = languageView;
            _rankingView = rankingView;
            _recordView = recordView;
            _tweetButtonView = tweetButtonView;
            _loadingView = loadingView;
        }

        public async UniTask InitViewAsync(CancellationToken token)
        {
            _loadingView.Activate(true);

            // ランキング更新待ち
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            try
            {
                // ランキングリストの作成
                var rankingData = await _resultUseCase.GetRankDataAsync(token);
                _rankingView.SetData(rankingData);

                // 自身のスコアを設定
                _recordView.SetRecord(_resultUseCase.GetSelfRecord());

                // 選択中の言語から文言を設定
                var resultScene = _languageUseCase.FindResultScene(_saveDataUseCase.languageType);
                _languageView.Display(resultScene);
                _tweetButtonView.InitTweet(_resultUseCase.BuildTweetMessage(resultScene));

                _loadingView.Activate(false);
            }
            catch (Exception e)
            {
                await _errorController.PopupErrorAsync(e, token);
                await InitViewAsync(token);
            }
        }
    }
}