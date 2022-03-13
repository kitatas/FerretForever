using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class ScorePresenter : IPostInitializable
    {
        private readonly ScoreUseCase _scoreUseCase;
        private readonly ScoreView _scoreView;

        public ScorePresenter(ScoreUseCase scoreUseCase, ScoreView scoreView)
        {
            _scoreUseCase = scoreUseCase;
            _scoreView = scoreView;
        }

        public void PostInitialize()
        {
            _scoreUseCase.score
                .Subscribe(_scoreView.Show)
                .AddTo(_scoreView);
        }
    }
}