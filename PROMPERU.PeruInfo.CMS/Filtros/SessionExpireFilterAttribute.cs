using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PROMPERU.PeruInfo.CMS.Filtros
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Obtener el nombre del controlador y la acción
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            
            var excludedActions = new[]
            {
            new { Controller = "Usuario", Action = "Login" },
            new { Controller = "Usuario", Action = "logout" } 
        };

            // Verificar si la acción actual está en la lista de exclusiones
            bool isExcluded = excludedActions.Any(e => e.Controller == controllerName && e.Action == actionName);

            if (!isExcluded && !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Verificar si es una solicitud AJAX
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401; // No autorizado
                    filterContext.Result = new JsonResult
                    {
                        Data = new { error = "Usuario no autenticado" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Usuario" },
                    { "action", "Login" }
                });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}