using Ferret.InGame.Data.Container;
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
        [SerializeField] private BalloonTable balloonTable = default;
        [SerializeField] private EnemyTable enemyTable = default;
        [SerializeField] private PlayerTable playerTable = default;

        [SerializeField] private GroundController groundController = default;

        [SerializeField] private BridgeAxisView bridgeAxisView = default;
        [SerializeField] private BridgeView bridgeView = default;
        [SerializeField] private InputView inputView = default;
        [SerializeField] private PlayerCountView playerCountView = default;
        [SerializeField] private ScoreView scoreView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Container
            builder.Register<PlayerContainer>(Lifetime.Scoped);

            // DataStore
            builder.RegisterInstance<BalloonTable>(balloonTable);
            builder.RegisterInstance<EnemyTable>(enemyTable);
            builder.RegisterInstance<PlayerTable>(playerTable);

            // Entity
            builder.Register<GameStateEntity>(Lifetime.Scoped);
            builder.Register<PlayerCountEntity>(Lifetime.Scoped);
            builder.Register<ScoreEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<BalloonFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory>(Lifetime.Scoped);
            builder.Register<PlayerFactory>(Lifetime.Scoped);

            // Repository
            builder.Register<BalloonRepository>(Lifetime.Scoped);
            builder.Register<EnemyRepository>(Lifetime.Scoped);
            builder.Register<PlayerRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<BalloonContainerUseCase>(Lifetime.Scoped);
            builder.Register<EnemyContainerUseCase>(Lifetime.Scoped);
            builder.Register<GameStateUseCase>(Lifetime.Scoped);
            builder.Register<PlayerPoolUseCase>(Lifetime.Scoped);
            builder.Register<PlayerContainerUseCase>(Lifetime.Scoped);
            builder.Register<PlayerCountUseCase>(Lifetime.Scoped);
            builder.Register<ScoreUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<TitleState>(Lifetime.Scoped);
            builder.Register<MainState>(Lifetime.Scoped);
            builder.Register<BridgeState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.Register<GameStateController>(Lifetime.Scoped);
            builder.Register<GimmickController>(Lifetime.Scoped);
            builder.RegisterInstance<GroundController>(groundController);

            // Presenter
            builder.RegisterEntryPoint<GameStatePresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PlayerCountPresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<ScorePresenter>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<BridgeAxisView>(bridgeAxisView);
            builder.RegisterInstance<BridgeView>(bridgeView);
            builder.RegisterInstance<InputView>(inputView);
            builder.RegisterInstance<PlayerCountView>(playerCountView);
            builder.RegisterInstance<ScoreView>(scoreView);
        }
    }
}