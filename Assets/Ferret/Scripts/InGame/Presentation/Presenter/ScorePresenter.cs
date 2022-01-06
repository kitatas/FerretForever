using System;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class ScorePresenter : IPostInitializable, IDisposable
    {
        private readonly ScoreUseCase _scoreUseCase;
        private readonly ScoreView _scoreView;
        private readonly CompositeDisposable _disposable;

        public ScorePresenter(ScoreUseCase scoreUseCase, ScoreView scoreView)
        {
            _scoreUseCase = scoreUseCase;
            _scoreView = scoreView;
            _disposable = new CompositeDisposable();
        }

        public void PostInitialize()
        {
            _scoreUseCase.score
                .Subscribe(_scoreView.Show)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}