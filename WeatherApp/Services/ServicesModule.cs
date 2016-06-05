using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Services.Interfaces;

namespace Services
{
    public class ServicesModule : IModule
    {
        IUnityContainer _unityContainer;

        public ServicesModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            //_unityContainer.RegisterInstance(new RestClient());
            //_unityContainer.RegisterType(typeof(IWeatherSevice), typeof(WeatherSevice));
            _unityContainer.RegisterInstance(typeof(IWeatherSevice), new WeatherSevice(new RestClient()));
        }
    }
}