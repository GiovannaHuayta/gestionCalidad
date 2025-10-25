using AutoMapper;
using PROMPERU.PeruInfo.ApplicationService;

namespace PROMPERU.PeruInfo.WEB
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationServiceProfile());
            });

            return config;
        }
    }
}