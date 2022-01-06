using Ferret.InGame.Data.DataStore;
using Ferret.InGame.Data.Entity;
using Ferret.InGame.Domain.Factory;
using Ferret.InGame.Domain.Repository;
using Ferret.InGame.Domain.UseCase;
using Ferret.InGame.Presentation.Controller;
using Ferret.InGame.Presentation.Presenter;
using Ferret.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ferret.InGame
{
    public sealed class InGameInstaller : LifetimeScope
    {
        [SerializeField] private PlayerTable playerTable = default;

        [SerializeField] private GroundController groundController = default;

        [SerializeField] private InputView inputView = default;
        [SerializeField] private ScoreView scoreView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<PlayerTable>(playerTable);

            // Entity
            builder.Register<GameStateEntity>(Lifetime.Scoped);
            builder.Register<ScoreEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<PlayerFactory>(Lifetime.Scoped);

            // Repository
            builder.Register<PlayerRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<GameStateUseCase>(Lifetime.Scoped);
            builder.Register<PlayerContainerUseCase>(Lifetime.Scoped);
            builder.Register<ScoreUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.Register<MainState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.Register<GameStateController>(Lifetime.Scoped);
            builder.RegisterInstance<GroundController>(groundController);

            // Presenter
            builder.RegisterEntryPoint<GameStatePresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<ScorePresenter>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<InputView>(inputView);
            builder.RegisterInstance<ScoreView>(scoreView);
        }
    }
}