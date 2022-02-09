using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ferret.OutGame.Domain.UseCase;
using Ferret.OutGame.Presentation.View;

namespace Ferret.OutGame.Presentation.Controller
{
    public sealed class ResultController
    {
        private readonly RankingDataUseCase _rankingDataUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly RankingView _rankingView;
        private readonly RecordView _recordView;
        private readonly TweetButtonView _tweetButtonView;

        public ResultController(RankingDataUseCase rankingDataUseCase, UserRecordUseCase userRecordUseCase,
            RankingView rankingView, RecordView recordView, TweetButtonView tweetButtonView)
        {
            _rankingDataUseCase = rankingDataUseCase;
            _userRecordUseCase = userRecordUseCase;
            _rankingView = rankingView;
            _recordView = recordView;
            _tweetButtonView = tweetButtonView;
        }

        public async UniTask InitViewAsync(CancellationToken token)
        {
            // ランキング更新待ち
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            var rankingData = await _rankingDataUseCase.GetRankDataAsync(token);
            _rankingView.SetData(rankingData);

            _recordView.SetRecord(_userRecordUseCase.GetHighRecord(), _userRecordUseCase.GetCurrentRecord());
            _tweetButtonView.Init(_userRecordUseCase.GetCurrentRecord().score);
        }
    }
}