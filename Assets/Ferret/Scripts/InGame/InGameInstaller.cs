using Ferret.InGame.Data.Entity;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.Controller;
using Ferret.InGame.Presentation.Presenter;
using VContainer;
using VContainer.Unity;

namespace Ferret.InGame
{
    public sealed class InGameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<GameStateEntity>(Lifetime.Scoped);

            // Domain
            builder.Register<GameStateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.Register<MainState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.Register<GameStateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<GameStatePresenter>(Lifetime.Scoped);
        }
    }
}