using Ferret.Common.Data.DataStore;
using Ferret.Common.Data.Entity;
using Ferret.Common.Domain.Repository;
using Ferret.Common.Domain.UseCase;
using Ferret.Common.Presentation.Controller;
using Ferret.Common.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ferret.Common
{
    public sealed class CommonInstaller : LifetimeScope
    {
        [SerializeField] private bool isCriSound = default;
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;
        [SerializeField] private LanguageTable languageTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<AchievementMasterEntity>(Lifetime.Singleton);
            builder.Register<UserRecordEntity>(Lifetime.Singleton);

            // DataStore
            builder.RegisterInstance<LanguageTable>(languageTable);

            // Repository
            builder.Register<PlayFabRepository>(Lifetime.Singleton);
            builder.Register<SaveDataRepository>(Lifetime.Singleton);
            builder.Register<LanguageRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SaveDataUseCase>(Lifetime.Singleton);
            builder.Register<LanguageUseCase>(Lifetime.Singleton);

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<ErrorController>(Lifetime.Singleton);

            // MonoBehaviour
            FindAnyObjectByType<DontDestroyController>().Init();
            ConfigureSound(builder);
            builder.RegisterInstance<ErrorPopupView>(FindAnyObjectByType<ErrorPopupView>());
            builder.RegisterInstance<LoadingView>(FindAnyObjectByType<LoadingView>());
            builder.RegisterInstance<TransitionMaskView>(FindAnyObjectByType<TransitionMaskView>());
        }

        private void ConfigureSound(IContainerBuilder builder)
        {
            if (isCriSound)
            {
                var bgm = FindAnyObjectByType<CriBgmController>();
                var se = FindAnyObjectByType<CriSeController>();
                builder.RegisterInstance<CriBgmController>(bgm).AsImplementedInterfaces();
                builder.RegisterInstance<CriSeController>(se).AsImplementedInterfaces();
            }
            else
            {
                builder.RegisterInstance<BgmTable>(bgmTable);
                builder.RegisterInstance<SeTable>(seTable);
                builder.Register<SoundRepository>(Lifetime.Singleton);
                builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

                var bgm = FindAnyObjectByType<BgmController>();
                var se = FindAnyObjectByType<SeController>();
                builder.RegisterInstance<BgmController>(bgm).AsImplementedInterfaces();
                builder.RegisterInstance<SeController>(se).AsImplementedInterfaces();
            }
        }
    }
}