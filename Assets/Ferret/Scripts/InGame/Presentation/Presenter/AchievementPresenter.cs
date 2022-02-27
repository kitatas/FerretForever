using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.View;
using VContainer.Unity;

namespace Ferret.InGame.Presentation.Presenter
{
    public sealed class AchievementPresenter : IPostInitializable
    {
        private readonly AchievementUseCase _achievementUseCase;
        private readonly AchievementView _achievementView;

        public AchievementPresenter(AchievementUseCase achievementUseCase, AchievementView achievementView)
        {
            _achievementUseCase = achievementUseCase;
            _achievementView = achievementView;
        }

        public void PostInitialize()
        {
            _achievementView.SetData(_achievementUseCase.GetAchievementStatus());
        }
    }
}