using AutoMapper;
using log4net;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class NosPreparamosController : Controller
    {
        #region Private Variables

        private readonly INosPreparamosService _nosPreparamosService;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
        public NosPreparamosController(INosPreparamosService nosPreparamosService)
        {
            _nosPreparamosService = nosPreparamosService;
        }

        #endregion

        #region Public Methods

        // GET: NosPreparamos
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Contenido NosPreparamos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Listar()
        {
            var data = new
            {
                data = _nosPreparamosService.Listar()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     Actualizar Registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(NosPreparamosBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();
            

            bool result = _nosPreparamosService.Actualizar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Sube la imagen para el bloque.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file == null) return Json(new { message = "No se encontró el archivo" });                               

                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string newFileName = "nosPreparamos" + "_" + timeStamp + Path.GetExtension(file.FileName);

                string path = Path.Combine(Server.MapPath("~/files/nosPreparamos"), newFileName);
                file.SaveAs(path);

                return Json(new { file = newFileName });

            }
            catch (Exception ex)
            {
                Log.Error($"NosPreparamosController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id)
        {
            return Json(_nosPreparamosService.Seleccionar(id));
        }

        #endregion
    }
}