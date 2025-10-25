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
    public class BannerSubcategoriaController : Controller
    {
        #region Private Properties

        private readonly IBannerService _bannerService;
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

        /// <summary>
        /// Insertar banner galería.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <param name="result"></param>
        private void InsertarBannerGaleria(BannerBE entity, GaleriaBE galeria, int result)
        {
            galeria.EntidadId = result;
            galeria.IdiomaId = entity.IdiomaId ?? 1;
            galeria.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            galeria.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            galeria.IpCreacion = GetIp();

            _galeriaService.Insertar(galeria);
        }

        /// <summary>
        /// Actualizar banner galeria.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        private void UpdateBannerGaleria(BannerBE entity, GaleriaBE galeria)
        {
            galeria.Id = entity.ImagenId ?? 0;
            galeria.TraduccionId = entity.ImagenTraduccionId ?? 0;
            galeria.EntidadId = entity.TraduccionId;
            galeria.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            galeria.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            galeria.IpModificacion = GetIp();

            _galeriaService.Actualizar(galeria);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.
        /// </summary>
        /// <param name="bannerService"></param>
        /// <param name="galeriaService"></param>
        public BannerSubcategoriaController(IBannerService bannerService, IGaleriaService galeriaService)
        {
            _bannerService = bannerService;
            _galeriaService = galeriaService;
        }

        #endregion

        #region Public Methods

        // GET
        [Authorize]
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
                data = _bannerService
                    .Listar()
                    .Where(x => x.CategoriaId == 0)
                    .Where(x => x.SubcategoriaId != 0)
                    .Where(x => x.PaginaId == 0)
                    .ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar Campania por id.
        /// </summary>
        /// <param name="traduccionId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int traduccionId, int idiomaId)
        {
            BannerBE banner = _bannerService
                .Listar()
                .Where(x => x.TraduccionId == traduccionId)
                .FirstOrDefault(x => x.IdiomaId == idiomaId);

            return Json(banner);
        }

        /// <summary>
        /// Insertar banner.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(BannerBE entity, GaleriaBE galeria)
        {
            try
            {
                entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
                entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                entity.IpCreacion = GetIp();

                int result = _bannerService.Insertar(entity);

                InsertarBannerGaleria(entity, galeria, result);

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
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(BannerBE entity, GaleriaBE galeria)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            _bannerService.Actualizar(entity);

            switch (entity.ImagenId)
            {
                case 0:
                case null:
                    InsertarBannerGaleria(entity, galeria, entity.TraduccionId);
                    break;
                default:
                    UpdateBannerGaleria(entity, galeria);
                    break;
            }

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

                string path = Path.Combine(Server.MapPath("~/files/banner"), Path.GetFileName(file.FileName));
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