using System.Net.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PROMPERU.PeruInfo.WEB
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
              name: "SitemapNewsEs",
              url: "es-pe/sitemapnews-pe.xml",
              defaults: new { controller = "Home", action = "SitemapNews", lang = "es" } // Cambiado "pe" por "es"
            );


            routes.MapRoute(
                name: "SitemapNewsEn",
                url: "en-us/sitemapnews-en.xml",
                defaults: new { controller = "Home", action = "SitemapNews", lang = "en" }
            );

            routes.MapRoute(
                "StoreContacto",
                "contacto",
                new { controller = "Home", action = "Contacto" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );

            routes.MapRoute(
                "Root",
                "",
                new
                {
                    controller = "Base",
                    action = "RedirectToLocalized"
                }
            );

            routes.MapRoute(
                "PublicacionApi",
                "{culture}/api/publicacion",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "PublicacionApi"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );
            
            routes.MapRoute(
                "EmbajadoresApi",
                "{culture}/api/personas",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "EmbajadoresApi"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "RestaurantesApi",
                "{culture}/api/negocios",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "RestaurantesApi"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "Campania",
                "{culture}/{categoria}/{subcategoria}/{campaniaId}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Campania"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "Home",
                "{culture}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Index"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "Privacidad",
                "{culture}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Privacidad"
                },
                new
                {
                    culture = @"(\w{2})|(\w{2}-\w{2})",
                    slug = "terminos-y-condiciones|terms-and-conditions|aviso-de-privacidad|privacy-policy"
                }
            );

            routes.MapRoute(
                "Apps",
                "{culture}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Index"
                    //action = "Apps"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", slug = "nuestras-apps" }
            );

            routes.MapRoute(
                "Comunicados",
                "{culture}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Comunicados"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", slug = "covid-19" }
            );

            routes.MapRoute(
                "BusquedaHome",
                "{culture}/{section}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "BusquedaHome"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", section = "blogperu|noticias|news|search" }
            );

            routes.MapRoute(
                "Categoria",
                "{culture}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Categoria"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "Contacto",
                "{culture}/{categoria}/{subcategoria}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Contacto"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", subcategoria = "contacto|contact-us" }
            );

            routes.MapRoute(
                "Premios",
                "{culture}/{categoria}/{subcategoria}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Premios"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", subcategoria = "acerca-de|about" }
            );

            routes.MapRoute(
                "Embajador",
                "{culture}/{categoria}/{subcategoria}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Embajador"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", subcategoria = "embajadores|amigos-del-peru|ambassadors" }
            );

            routes.MapRoute(
                "Embajadores",
                "{culture}/{categoria}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Embajadores"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", slug = "embajadores|amigos-del-peru|ambassadors" }
            );

            routes.MapRoute(
                "Restaurantes",
                "{culture}/{categoria}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Restaurantes"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})", slug = "restaurantes-en-el-mundo|restaurants-in-the-world" }
            );

            routes.MapRoute(
                "Subcategoria",
                "{culture}/{categoria}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Subcategoria"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "Busqueda",
                "{culture}/{categoria}/{subcategoria}/{id}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Busqueda"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
                "Publicacion",
                "{culture}/{categoria}/{seccion}/{categoriaId}/{subcategoriaId}/{slug}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Publicacion"
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

            routes.MapRoute(
            "Error",
            "{controller}/{action}",
            new
            {
                culture = "es-pe",
                controller = "Error",
                action = "Index",
            },
            new { culture = @"(\w{2})|(\w{2}-\w{2})" }
        );

            
            routes.MapRoute(
                "Default",
                "{culture}/{controller}/{action}/{id}",
                new
                {
                    culture = "es-pe",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new { culture = @"(\w{2})|(\w{2}-\w{2})" }
            );

        }
    }
}