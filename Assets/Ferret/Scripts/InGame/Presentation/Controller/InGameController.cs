using Ferret.Common.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class InGameController : IPostInitializable
    {
        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseUiButtonView>())
            {
                button.Init();
            }
        }
    }
}