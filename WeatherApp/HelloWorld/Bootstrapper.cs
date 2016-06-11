using Prism.Unity;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using HelloWorld.Views;
using Services;
using WeatherModule;

namespace HelloWorld
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(ServicesModule));
            catalog.AddModule(typeof(WeatherModuleModule));
        }
    }
}
