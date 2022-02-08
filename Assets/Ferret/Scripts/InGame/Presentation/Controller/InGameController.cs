using Ferret.Common;
using Ferret.Common.Presentation.Controller.Interface;
using Ferret.Common.Presentation.View;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Ferret.InGame.Presentation.Controller
{
    public sealed class InGameController : IPostInitializable
    {
        private readonly ISeController _seController;

        public InGameController(ISeController seController)
        {
            _seController = seController;
        }

        public void PostInitialize()
        {
            foreach (var button in Object.FindObjectsOfType<BaseButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);
            }

            foreach (var button in Object.FindObjectsOfType<BaseUiButtonView>())
            {
                button.Init();
                button.push += () => _seController.Play(SeType.Button);
            }
        }
    }
}