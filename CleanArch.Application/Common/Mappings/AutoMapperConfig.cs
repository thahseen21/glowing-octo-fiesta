using AutoMapper;
using System.Reflection;

namespace CleanArch.Application.Common.Mappings
{
    public class AutoMapperConfig
    {
        public static IMapperConfigurationExpression MapperConfigurationExpression = null!;
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // if there is any configuration
                // should go here
                // https://docs.automapper.org/en/stable/Configuration.html
                MapperConfigurationExpression = cfg;
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return config;
        }
    }
}
