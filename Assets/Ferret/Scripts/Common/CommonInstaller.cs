using Ferret.Common.Data.Entity;
using Ferret.Common.Domain.Repository;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace Ferret.Common
{
    public sealed class CommonInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<UserRecordEntity>(Lifetime.Singleton);

            // Repository
            builder.Register<PlayFabRepository>(Lifetime.Singleton);
            builder.Register<SaveDataRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SaveDataUseCase>(Lifetime.Singleton);

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // MonoBehaviour
            FindObjectOfType<DontDestroyController>().Init();
            builder.RegisterInstance<CriBgmController>(FindObjectOfType<CriBgmController>()).AsImplementedInterfaces();
            builder.RegisterInstance<CriSeController>(FindObjectOfType<CriSeController>()).AsImplementedInterfaces();
            builder.RegisterInstance<ErrorPopupView>(FindObjectOfType<ErrorPopupView>());
            builder.RegisterInstance<LoadingView>(FindObjectOfType<LoadingView>());
            builder.RegisterInstance<TransitionMaskView>(FindObjectOfType<TransitionMaskView>());
        }
    }
}