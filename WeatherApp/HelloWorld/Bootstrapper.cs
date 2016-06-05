using Prism.Unity;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ModuleA;
using HelloWorld.Views;
using Services;
using WpfApplication1;

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
            catalog.AddModule(typeof(ModuleAModule));
            catalog.AddModule(typeof(WeatherModuleModule));
        }
    }
}
