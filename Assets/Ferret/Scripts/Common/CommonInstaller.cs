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
            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // MonoBehaviour
            FindObjectOfType<DontDestroyController>().Init();
            builder.RegisterInstance<TransitionMaskView>(DontDestroyController.Instance.maskView);
        }
    }
}