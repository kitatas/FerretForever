using Ferret.Boot.Domain.UseCase;
using Ferret.Boot.Presentation.Controller;
using VContainer;
using VContainer.Unity;

namespace Ferret.Boot
{
    public sealed class BootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<BootController>(Lifetime.Scoped);
        }
    }
}