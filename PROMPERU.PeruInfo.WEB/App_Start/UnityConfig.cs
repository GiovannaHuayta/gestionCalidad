using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.Implements;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Infra.Data;
using PROMPERU.PeruInfo.Infra.Data.Utils;
using Unity;
using Unity.Mvc5;

namespace PROMPERU.PeruInfo.WEB
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            container
                .RegisterType<IPaginaRepository, PaginaRepository>()
                .RegisterType<IPaginaService, PaginaService>()
                .RegisterType<IIdiomaRepository, IdiomaRepository>()
                .RegisterType<IIdiomaService, IdiomaService>()
                .RegisterType<ICategoriaRepository, CategoriaRepository>()
                .RegisterType<ICategoriaService, CategoriaService>()
                .RegisterType<IPublicacionRepository, PublicacionRepository>()
                .RegisterType<IPublicacionService, PublicacionService>()
                .RegisterType<ISubcategoriaRepository, SubcategoriaRepository>()
                .RegisterType<ISubcategoriaService, SubcategoriaService>()
                .RegisterType<ITarjetaRepository, TarjetaRepository>()
                .RegisterType<ITarjetaService, TarjetaService>()
                .RegisterType<IPersonaRepository, PersonaRepository>()
                .RegisterType<IPersonaService, PersonaService>()
                .RegisterType<INegocioRepository, NegocioRepository>()
                .RegisterType<INegocioService, NegocioService>()
                .RegisterType<IComunicadoRepository, ComunicadoRepository>()
                .RegisterType<IComunicadoService, ComunicadoService>()
                .RegisterType<IContactoRepository, ContactoRepository>()
                .RegisterType<IContactoService, ContactoService>()
                .RegisterType<IBannerRepository, BannerRepository>()
                .RegisterType<IBannerService, BannerService>()
                .RegisterType<IGaleriaRepository, GaleriaRepository>()
                .RegisterType<IGaleriaService, GaleriaService>()
                .RegisterType<IPaisRepository, PaisRepository>()
                .RegisterType<IPaisService, PaisService>()
                .RegisterType<IPersonaTipoRepository, PersonaTipoRepository>()
                .RegisterType<IPersonaTipoService, PersonaTipoService>()
                .RegisterType<ICampaniaRepository, CampaniaRepository>()
                .RegisterType<ICampaniaService, CampaniaService>();

            AdoHelper.ConnectionString =
                ConfigurationManager.ConnectionStrings["PeruInfoDbConnectionString"].ConnectionString;

            IMapper mapper = AutoMapperConfiguration.InitializeAutoMapper().CreateMapper();
            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}