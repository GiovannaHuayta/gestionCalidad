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

namespace PROMPERU.PeruInfo.CMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IIdiomaRepository, IdiomaRepository>();
            container.RegisterType<IComunicadoRepository, ComunicadoRepository>();
            container.RegisterType<ISubcategoriaRepository, SubcategoriaRepository>();
            container.RegisterType<ITipoRepository, TipoRepository>();
            container.RegisterType<IPublicacionRepository, PublicacionRepository>();
            container.RegisterType<ICategoriaRepository, CategoriaRepository>();
            container.RegisterType<IPaisRepository, PaisRepository>();
            container.RegisterType<INegocioRepository, NegocioRepository>();
            container.RegisterType<IPersonaRepository, PersonaRepository>();
            container.RegisterType<ITarjetaRepository, TarjetaRepository>();
            container.RegisterType<IContactoRepository, ContactoRepository>();
            container.RegisterType<IUsuarioRepository, UsuarioRepository>();
            container.RegisterType<ICampaniaRepository, CampaniaRepository>();
            container.RegisterType<IPaginaRepository, PaginaRepository>();
            container.RegisterType<IGaleriaRepository, GaleriaRepository>();
            container.RegisterType<IPersonaTipoRepository, PersonaTipoRepository>();
            container.RegisterType<IBannerRepository, BannerRepository>();
            container.RegisterType<INosPreparamosRepository, NosPreparamosRepository>();
            container.RegisterType<IDeportistasRepository, DeportistasRepository>();
            container.RegisterType<IPalabraAlientoRepository, PalabraAlientoRepository>();
            container.RegisterType<IGaleriaPeruParisRepository, GaleriaPeruParisRepository>();

            container.RegisterType<IIdiomaService, IdiomaService>();
            container.RegisterType<IComunicadoService, ComunicadoService>();
            container.RegisterType<ISubcategoriaService, SubcategoriaService>();
            container.RegisterType<ITipoService, TipoService>();
            container.RegisterType<IPublicacionService, PublicacionService>();
            container.RegisterType<ICategoriaService, CategoriaService>();
            container.RegisterType<IPaisService, PaisService>();
            container.RegisterType<INegocioService, NegocioService>();
            container.RegisterType<IPersonaService, PersonaService>();
            container.RegisterType<ITarjetaService, TarjetaService>();
            container.RegisterType<IContactoService, ContactoService>();
            container.RegisterType<IUsuarioService, UsuarioService>();
            container.RegisterType<ICampaniaService, CampaniaService>();
            container.RegisterType<IPaginaService, PaginaService>();
            container.RegisterType<IGaleriaService, GaleriaService>();
            container.RegisterType<IPersonaTipoService, PersonaTipoService>();
            container.RegisterType<IBannerService, BannerService>();
            container.RegisterType<INosPreparamosService, NosPreparamosService>();
            container.RegisterType<IDeportistaService, DeportistaService>();
            container.RegisterType<IPalabraAlientoService, PalabraAlientoService>();
            container.RegisterType<IGaleriaPeruParisService, GaleriaPeruParisService>();

            AdoHelper.ConnectionString =
                ConfigurationManager.ConnectionStrings["PeruInfoDbConnectionString"].ConnectionString;

            AdoHelperSecundario.ConnectionString =
                ConfigurationManager.ConnectionStrings["JuegosOlimpicosDbConnectionString"].ConnectionString;

            IMapper mapper = AutoMapperConfiguration.InitializeAutoMapper().CreateMapper();
            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}