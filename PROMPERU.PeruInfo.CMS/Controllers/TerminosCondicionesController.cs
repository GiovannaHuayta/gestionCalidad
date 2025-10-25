using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class TerminosCondicionesController : Controller
    {
        #region Private Variables

        private readonly IPaginaService _paginaService;

        #endregion

        #region Private Methods

        private static string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.
        /// </summary>
        public TerminosCondicionesController(IPaginaService paginaService)
        {
            _paginaService = paginaService;
        }

        #endregion

        #region Public Methods

        // GET: TerminosCondiciones
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Seleccionar un Término y Condición por Idioma.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SeleccionarPorIdioma(int idiomaId)
        {
            System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return Json(_paginaService.SeleccionarPorIdioma(59, idiomaId));
        }

        /// <summary>
        ///     Actualizar Término y Condición.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(PaginaBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            bool result = _paginaService.Actualizar(entity);

            return Json(result);
        }

        #endregion
    }
}