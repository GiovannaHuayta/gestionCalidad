using PROMPERU.PeruInfo.WEB.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PROMPERU.PeruInfo.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Web.Helpers.AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            AutoMapperConfiguration.InitializeAutoMapper();
        }
        protected void Application_Error() {
            Exception ex = Server.GetLastError();

            if (ex is HttpException httpException)
            {
                int httpCode = httpException.GetHttpCode();

                if (httpCode == 404)
                {
                    Response.Clear();

                    // Configurar el código de estado HTTP
                    Response.StatusCode = 404;
                    Response.TrySkipIisCustomErrors = true;

                    // Transferir el control a la página de error manteniendo la URL original
                    Server.TransferRequest("/Error/Index");
                }
            }


        }
    }
}
