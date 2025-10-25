using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class GaleriaCampaniaController : Controller
    {
        #region Private Properties

        private readonly IGaleriaService _galeriaService;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

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
        /// Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.
        /// </summary>
        /// <param name="galeriaService"></param>
        public GaleriaCampaniaController(IGaleriaService galeriaService)
        {
            _galeriaService = galeriaService;
        }

        #endregion

        #region Public Methods

        // GET
        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lista los banners
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Listar()
        {
            var data = new
            {
                data = _galeriaService.Listar(0)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar Campania por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            GaleriaBE galeria = _galeriaService
                .Listar(idiomaId)
                .FirstOrDefault(x => x.Id == id);

            return Json(galeria);
        }

        /// <summary>
        /// Insertar banner.
        /// </summary>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(GaleriaBE galeria)
        {
            try
            {
                galeria.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
                galeria.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                galeria.IpCreacion = GetIp();

                int result = _galeriaService.Insertar(galeria);

                return Json(new { success = true, id = result, message = "Se registró correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, id = 0, message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza banner.
        /// </summary>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(GaleriaBE galeria)
        {
            galeria.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            galeria.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            galeria.IpModificacion = GetIp();

            _galeriaService.Actualizar(galeria);

            return Json(new { success = true, id = 0, message = "Se actualizó con éxito" });
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

                string path = Path.Combine(Server.MapPath("~/files/galeria"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                return Json(new { file = file.FileName });
            }
            catch (Exception ex)
            {
                Log.Error($"BannerController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}