using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using PROMPERU.PeruInfo.CMS.Models;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class TarjetaController : Controller
    {
        #region Private Variables

        private readonly ITarjetaService _tarjetaService;
        private readonly IGaleriaService _galeriaService;
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

        /// <summary>
        /// Insertar la galería de la tarjeta
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        private void InsertarGaleriaTarjeta(TarjetaBE entity, GaleriaSubcategoria galeria)
        {
            GaleriaBE galeriaBe = new GaleriaBE
            {
                IdiomaId = entity.IdiomaId,
                EntidadTipo = "Tarjeta",
                EntidadId = entity.Id,
                RutaDekstop = galeria.ImagenDesktop,
                Uso = "portada",
                TextoAlternativo = galeria.ImagenTextoAlternativo,
                UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString()),
                FechaCreacion = DateTime.UtcNow.AddHours(-5),
                IpCreacion = GetIp()
            };

            _galeriaService.Insertar(galeriaBe);
        }

        /// <summary>
        /// Actualizar galería de la tarjeta.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        private void ActualizarGaleriaTarjeta(TarjetaBE entity, GaleriaSubcategoria galeria)
        {
            GaleriaBE galeriaBe = new GaleriaBE
            {
                Id = galeria.ImagenId ?? 0,
                TraduccionId = galeria.ImagenTraduccionId ?? 0,
                IdiomaId = entity.IdiomaId,
                EntidadTipo = "Tarjeta",
                EntidadId = entity.Id,
                Uso = "portada",
                RutaDekstop = galeria.ImagenDesktop,
                TextoAlternativo = galeria.ImagenTextoAlternativo,
                UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString()),
                FechaModificacion = DateTime.UtcNow.AddHours(-5),
                IpModificacion = GetIp()
            };

            _galeriaService.Actualizar(galeriaBe);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tarjetaService"></param>
        /// <param name="galeriaService"></param>
        public TarjetaController(ITarjetaService tarjetaService, IGaleriaService galeriaService)
        {
            _tarjetaService = tarjetaService;
            _galeriaService = galeriaService;
        }

        #endregion

        #region Public Methods

        // GET: Tarjeta
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Tarjetas
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _tarjetaService.Listar(0)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar Tarjeta por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            return Json(_tarjetaService.Seleccionar(id, idiomaId));
        }

        /// <summary>
        ///     Insertar Tarjeta
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(TarjetaBE entity, GaleriaSubcategoria galeria)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            int result = _tarjetaService.Insertar(entity);

            InsertarGaleriaTarjeta(entity, galeria);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Tarjeta
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(TarjetaBE entity, GaleriaSubcategoria galeria)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            bool result = _tarjetaService.Actualizar(entity);

            if (galeria.ImagenId == null)
            {
                InsertarGaleriaTarjeta(entity, galeria);
            }
            else
            {
                ActualizarGaleriaTarjeta(entity, galeria);
            }

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Tarjeta
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(TarjetaBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _tarjetaService.Eliminar(entity);

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

                string path = Path.Combine(Server.MapPath("~/files/tarjeta"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                return Json(new { file = file.FileName });
            }
            catch (Exception ex)
            {
                Log.Error($"TarjetaController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Listar galeria de la subcategoría.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="tipo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Galeria(int idioma, string tipo, int id)
        {
            var data = new
            {
                data = _galeriaService.Listar(idioma, tipo, id)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}