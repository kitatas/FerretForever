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
        [SerializeField] private LanguageView languageView = default;
        [SerializeField] private RankingView rankingView = default;
        [SerializeField] private RecordView recordView = default;
        [SerializeField] private TweetButtonView tweetButtonView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<ResultUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<OutGameController>(Lifetime.Scoped);
            builder.Register<ResultController>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<InputView>(inputView);
            builder.RegisterInstance<LanguageView>(languageView);
            builder.RegisterInstance<RankingView>(rankingView);
            builder.RegisterInstance<RecordView>(recordView);
            builder.RegisterInstance<TweetButtonView>(tweetButtonView);
        }
    }
}