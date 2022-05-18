using AutoMapper;
using SolarCoffe.Web.Profiles;

namespace SolarCoffe.Web.Extensions
{
    public static class MapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services) {
            var config = new MapperConfiguration(o => {
                o.AddProfile(new ProductProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}