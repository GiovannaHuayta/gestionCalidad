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
    public class GaleriaPeruParisController : Controller
    {
        #region Private Variables

        private readonly IGaleriaPeruParisService _galeriaPeruParis;

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
        public GaleriaPeruParisController(IGaleriaPeruParisService galeriaPeruParis)
        {
            _galeriaPeruParis = galeriaPeruParis;
        }

        #endregion

        #region Public Methods
        // GET: GaleriaPeruParis
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Inserta Contenido GaleriaPeruParis
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(GaleriaPeruParisBE entity)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            int result = _galeriaPeruParis.Insertar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Listar Contenido Deportistas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Listar()
        {
            var data = new
            {
                data = _galeriaPeruParis.Listar()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     Actualizar Deportista
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(GaleriaPeruParisBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();


            bool result = _galeriaPeruParis.Actualizar(entity);

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
                if (file == null)
                    return Json(new { message = "No se encontró el archivo" });

                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string newFileName = "galeriaPP" + "_" + timeStamp + Path.GetExtension(file.FileName);

                string directoryPath = Server.MapPath("~/files/GaleriaPeruParis");

                if (!Directory.Exists(directoryPath))
                {
                    // Crear la carpeta si no existe
                    Directory.CreateDirectory(directoryPath);
                }

                string path = Path.Combine(directoryPath, newFileName);
                file.SaveAs(path);

                return Json(new { file = newFileName });
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaPeruParisController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Seleccionar(int id)
        {
            return Json(_galeriaPeruParis.Seleccionar(id));
        }

        /// <summary>
        ///  Elimina un registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            GaleriaPeruParisBE entity = new GaleriaPeruParisBE();
            entity.Id = id;
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _galeriaPeruParis.Eliminar(entity);

            return Json(result);
        }

        #endregion

    }
}