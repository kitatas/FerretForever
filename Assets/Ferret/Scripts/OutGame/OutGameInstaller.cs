using Ferret.OutGame.Domain.UseCase;
using Ferret.OutGame.Presentation.Controller;
using Ferret.OutGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ferret.OutGame
{
    public sealed class OutGameInstaller : LifetimeScope
    {
        [SerializeField] private InputView inputView = default;
        [SerializeField] private RankingView rankingView = default;
        [SerializeField] private RecordView recordView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<RankingDataUseCase>(Lifetime.Scoped);
            builder.Register<UserRecordUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<OutGameController>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<InputView>(inputView);
            builder.RegisterInstance<RankingView>(rankingView);
            builder.RegisterInstance<RecordView>(recordView);
        }
    }
}