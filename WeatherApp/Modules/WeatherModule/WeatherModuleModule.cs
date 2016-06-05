using Prism.Modularity;
using Prism.Regions;
using WpfApplication1.Views;

namespace WpfApplication1
{
    public class WeatherModuleModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public WeatherModuleModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("SecondaryRegion", typeof(WeatherView));
        }
    }
}