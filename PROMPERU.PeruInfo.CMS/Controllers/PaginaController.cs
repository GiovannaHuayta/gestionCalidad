using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.CMS.Models;
using PROMPERU.PeruInfo.Domain.Entities;
using SlugGenerator;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class PaginaController : Controller
    {
        #region Private Variables

        private readonly IPaginaService _paginaService;

        #endregion

        #region Private Methods

        /// <summary>
        /// Obtener Ip
        /// </summary>
        /// <returns></returns>
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
        /// Constructor
        /// </summary>
        /// <param name="paginaService"></param>
        public PaginaController(IPaginaService paginaService)
        {
            _paginaService = paginaService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Vista home páginas.
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Listar páginas.
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _paginaService.Listar()
                    .Where(x => x.Posicion == "Categoria" || x.Posicion == "Header")
                    .Where(x => x.Informativa != true)
                    .ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        /// <summary>
        /// Lista las categoría padre
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public JsonResult Padre(int idiomaId)
        {
            var data = new
            {
                data = _paginaService.Listar()
                    .Where(x => x.Posicion == "Categoria" || x.Posicion == "Header")
                    .Where(x => x.IdPadre == 0 || x.IdPadre == null)
                    .Where(x => x.IdiomaId == idiomaId)
                    .ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar página por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            return Json(_paginaService.SeleccionarPorIdioma(id, idiomaId));
        }

        /// <summary>
        ///     Insertar Categoria
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(PaginaBE entity)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            int result = _paginaService.Insertar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Categoria
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

        /// <summary>
        ///     Eliminar Categoria
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(PaginaBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _paginaService.Eliminar(entity);

            return Json(result);
        }

        #endregion
    }
}