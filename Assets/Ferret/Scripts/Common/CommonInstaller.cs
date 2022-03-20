using System.Collections.Generic;
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

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // MonoBehaviour
            FindObjectOfType<DontDestroyController>().Init();
            var (bgm, se) = ConfigureSound(builder);
            builder.RegisterInstance<ErrorPopupView>(FindObjectOfType<ErrorPopupView>());
            builder.RegisterInstance<LoadingView>(FindObjectOfType<LoadingView>());
            builder.RegisterInstance<TransitionMaskView>(FindObjectOfType<TransitionMaskView>());

            autoInjectGameObjects = new List<GameObject>
            {
                bgm,
                se,
            };
        }

        private (GameObject, GameObject) ConfigureSound(IContainerBuilder builder)
        {
            if (isCriSound)
            {
                var bgm = FindObjectOfType<CriBgmController>();
                var se = FindObjectOfType<CriSeController>();
                builder.RegisterInstance<CriBgmController>(bgm).AsImplementedInterfaces();
                builder.RegisterInstance<CriSeController>(se).AsImplementedInterfaces();
                return (bgm.gameObject, se.gameObject);
            }
            else
            {
                builder.RegisterInstance<BgmTable>(bgmTable);
                builder.RegisterInstance<SeTable>(seTable);
                builder.Register<SoundRepository>(Lifetime.Singleton);
                builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
                
                var bgm = FindObjectOfType<BgmController>();
                var se = FindObjectOfType<SeController>();
                builder.RegisterInstance<BgmController>(bgm).AsImplementedInterfaces();
                builder.RegisterInstance<SeController>(se).AsImplementedInterfaces();
                return (bgm.gameObject, se.gameObject);
            }
        }
    }
}