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
        private readonly RankingDataUseCase _rankingDataUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly RankingView _rankingView;
        private readonly RecordView _recordView;
        private readonly TweetButtonView _tweetButtonView;
        private readonly ErrorPopupView _errorPopupView;

        public ResultController(RankingDataUseCase rankingDataUseCase, UserRecordUseCase userRecordUseCase,
            RankingView rankingView, RecordView recordView, TweetButtonView tweetButtonView, ErrorPopupView errorPopupView)
        {
            _rankingDataUseCase = rankingDataUseCase;
            _userRecordUseCase = userRecordUseCase;
            _rankingView = rankingView;
            _recordView = recordView;
            _tweetButtonView = tweetButtonView;
            _errorPopupView = errorPopupView;
        }

        public async UniTask InitViewAsync(CancellationToken token)
        {
            // ランキング更新待ち
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            try
            {
                var rankingData = await _rankingDataUseCase.GetRankDataAsync(token);
                _rankingView.SetData(rankingData, _userRecordUseCase.GetUid());

                _recordView.SetRecord(_userRecordUseCase.GetHighRecord(), _userRecordUseCase.GetCurrentRecord());
                _tweetButtonView.Init(_userRecordUseCase.GetCurrentRecord().score);
            }
            catch (CustomPlayFabException e)
            {
                // TODO: エラーメッセージの修正
                UnityEngine.Debug.LogWarning($"[CustomPlayFabException]: {e}");
                await _errorPopupView.PopupAsync($"[CustomPlayFabException]: {e}", token);
                await InitViewAsync(token);
            }
            catch (Exception e)
            {
                // TODO: エラーメッセージの修正
                UnityEngine.Debug.LogWarning($"[Exception]: {e}");
                await _errorPopupView.PopupAsync($"[Exception]: {e}", token);
                await InitViewAsync(token);
            }
        }
    }
}