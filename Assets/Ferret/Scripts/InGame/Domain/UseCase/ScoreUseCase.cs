using Ferret.InGame.Data.Entity;
using UniRx;

namespace Ferret.InGame.Domain.UseCase
{
    public sealed class ScoreUseCase
    {
        private readonly ScoreEntity _scoreEntity;
        private readonly ReactiveProperty<float> _score;

        public ScoreUseCase(ScoreEntity scoreEntity)
        {
            _scoreEntity = scoreEntity;
            _score = new ReactiveProperty<float>(_scoreEntity.Get());
        }

        public IReadOnlyReactiveProperty<float> score => _score;

        public void Update(float deltaTime)
        {
            _scoreEntity.Add(deltaTime);
            _score.Value = _scoreEntity.Get();
        }
    }
}