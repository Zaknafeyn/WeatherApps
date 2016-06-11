using Prism.Modularity;
using Prism.Regions;
using Services;
using WeatherModule.Views;

namespace WeatherModule
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
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(RecentCitiesView));
        }
    }
}