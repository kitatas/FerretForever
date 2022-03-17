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
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;
        [SerializeField] private LanguageTable languageTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<AchievementMasterEntity>(Lifetime.Singleton);
            builder.Register<UserRecordEntity>(Lifetime.Singleton);

            // DataStore
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);
            builder.RegisterInstance<LanguageTable>(languageTable);

            // Repository
            builder.Register<PlayFabRepository>(Lifetime.Singleton);
            builder.Register<SaveDataRepository>(Lifetime.Singleton);
            builder.Register<LanguageRepository>(Lifetime.Singleton);
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SaveDataUseCase>(Lifetime.Singleton);
            builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // MonoBehaviour
            FindObjectOfType<DontDestroyController>().Init();
            // builder.RegisterInstance<CriBgmController>(FindObjectOfType<CriBgmController>()).AsImplementedInterfaces();
            // builder.RegisterInstance<CriSeController>(FindObjectOfType<CriSeController>()).AsImplementedInterfaces();
            var bgm = FindObjectOfType<BgmController>();
            var se = FindObjectOfType<SeController>();
            builder.RegisterInstance<BgmController>(bgm).AsImplementedInterfaces();
            builder.RegisterInstance<SeController>(se).AsImplementedInterfaces();
            builder.RegisterInstance<ErrorPopupView>(FindObjectOfType<ErrorPopupView>());
            builder.RegisterInstance<LoadingView>(FindObjectOfType<LoadingView>());
            builder.RegisterInstance<TransitionMaskView>(FindObjectOfType<TransitionMaskView>());

            autoInjectGameObjects = new List<GameObject>
            {
                bgm.gameObject,
                se.gameObject,
            };
        }
    }
}