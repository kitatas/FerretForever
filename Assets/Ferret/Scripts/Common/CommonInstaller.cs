using Ferret.Common.Domain.Repository;
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
            // Repository
            builder.Register<PlayFabRepository>(Lifetime.Singleton);
            builder.Register<SaveDataRepository>(Lifetime.Singleton);

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // MonoBehaviour
            FindObjectOfType<DontDestroyController>().Init();
            builder.RegisterInstance<TransitionMaskView>(DontDestroyController.Instance.maskView);
        }
    }
}