using System.ComponentModel;
using Ferret.Common;
using Ferret.Common.Presentation.Controller;
using VContainer.Unity;

public partial class SROptions
{
    [Category("Debug")]
    [Sort(0)]
    [DisplayName("Back Title")]
    public void BackTitle()
    {
        var container = VContainerSettings.Instance.RootLifetimeScope.Container;
        var sceneLoader = container.Resolve(typeof(SceneLoader)) as SceneLoader;
        sceneLoader?.LoadScene(SceneName.Main);
    }
}