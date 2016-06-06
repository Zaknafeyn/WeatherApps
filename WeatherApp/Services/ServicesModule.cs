using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Services.Interfaces;
using Services.Service;

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

            var localStorage = new LocalStorageService();
            localStorage.Read();

            //_unityContainer.RegisterInstance(new RestClient());
            //_unityContainer.RegisterType(typeof(IWeatherSevice), typeof(WeatherSevice));
            _unityContainer.RegisterInstance(typeof(IWeatherSevice), new WeatherSevice(new RestClient()));
            _unityContainer.RegisterInstance(typeof(ILocalStorageService), localStorage);
        }
    }
}