using Autofac;
using Services.Portable.API;
using Services.Portable.Service;

namespace Services.Portable
{
    public class ServiceApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<WeatherApi>().AsSelf();
            builder.RegisterType<IWeatherService>() .As<IWeatherService>().SingleInstance();
        }
    }
}