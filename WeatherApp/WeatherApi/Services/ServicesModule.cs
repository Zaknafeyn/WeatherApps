using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Services.Interfaces;
using Services.Portable;
using Services.Portable.Service;
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
            var logger = new DebugLogger();
            var localStorage = new LocalStorageService(logger);
            localStorage.Read();

            //_unityContainer.RegisterInstance(new RestClient());
            //_unityContainer.RegisterType(typeof(IWeatherService), typeof(weatherService));
            _unityContainer.RegisterInstance(typeof(IWeatherService), new WeatherService());
            _unityContainer.RegisterInstance(typeof(ILocalStorageService), localStorage);
            _unityContainer.RegisterType(typeof(ILogger), typeof(DebugLogger));
        }
    }
}