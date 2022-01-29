using Ferret.Boot.Domain.UseCase;
using Ferret.Boot.Presentation.Controller;
using Ferret.Boot.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ferret.Boot
{
    public sealed class BootInstaller : LifetimeScope
    {
        [SerializeField] private LoadingView loadingView = default;
        [SerializeField] private NameRegistrationView nameRegistrationView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<BootController>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<LoadingView>(loadingView);
            builder.RegisterInstance<NameRegistrationView>(nameRegistrationView);
        }
    }
}