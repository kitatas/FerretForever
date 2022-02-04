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
        [SerializeField] private ErrorPopupView errorPopupView = default;
        [SerializeField] private NameRegistrationView nameRegistrationView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<BootController>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<ErrorPopupView>(errorPopupView);
            builder.RegisterInstance<NameRegistrationView>(nameRegistrationView);
        }
    }
}