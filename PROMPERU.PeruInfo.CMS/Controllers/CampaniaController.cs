using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.CMS.Models;
using PROMPERU.PeruInfo.Domain.Entities;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using log4net;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class CampaniaController : Controller
    {
        #region Private Variables

        private readonly ICampaniaService _campaniaService;
        private readonly IGaleriaService _galeriaService;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        #endregion

        #region Private Methods

        /// <summary>
        /// Get IP.
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
        private void InsertarCampaniaGaleria(CampaniaBE entity, GaleriaBE galeria, int result)
        {
            galeria.EntidadId = result;
            galeria.IdiomaId = entity.IdiomaId;
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
        private void UpdateCampaniaGaleria(CampaniaBE entity, GaleriaBE galeria)
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
        /// Constructor.
        /// </summary>
        /// <param name="campaniaService"></param>
        /// <param name="galeriaService"></param>
        public CampaniaController(ICampaniaService campaniaService, IGaleriaService galeriaService)
        {
            _campaniaService = campaniaService;
            _galeriaService = galeriaService;
        }

        #endregion

        #region Public Methods

        // GET: Campania
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Campañas
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _campaniaService.Listar(0)
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer
            {
                MaxJsonLength = 500000000
            };

            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;

            return json;
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
            return Json(_campaniaService.Seleccionar(id, idiomaId));
        }

        /// <summary>
        ///     Insertar Campania
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(CampaniaBE entity, GaleriaBE galeria)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            List<CampaniaBE> campanias = _campaniaService.Listar(entity.IdiomaId);

            CampaniaBE campania = campanias.FirstOrDefault(x => x.Nombre == entity.Nombre);

            if (campania == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = campanias.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                string newSlug = campania.Slug;

                if (campania.Nombre != entity.Nombre)
                {
                    newSlug = slugs.Count() > 1 ? entity.Nombre.GenerateUniqueSlug(slugs) : entity.Nombre.GenerateSlug();
                }

                entity.Slug = newSlug;
            }

            int result = _campaniaService.Insertar(entity);

            InsertarCampaniaGaleria(entity, galeria, result);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Campania
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(CampaniaBE entity, GaleriaBE galeria)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            List<CampaniaBE> campanias = _campaniaService.Listar(entity.IdiomaId);

            CampaniaBE campania = campanias.FirstOrDefault(x => x.Id == entity.Id);

            if (campania == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = campanias.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                string newSlug = campania.Slug;
                if (campania.Nombre != entity.Nombre)
                {
                    newSlug = slugs.Count() > 1 ? entity.Nombre.GenerateUniqueSlug(slugs) : entity.Nombre.GenerateSlug();
                }

                entity.Slug = newSlug;
            }

            bool result = _campaniaService.Actualizar(entity);

            switch (entity.ImagenId)
            {
                case 0:
                case null:
                    InsertarCampaniaGaleria(entity, galeria, entity.TraduccionId);
                    break;
                default:
                    UpdateCampaniaGaleria(entity, galeria);
                    break;
            }

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Campania
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(CampaniaBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _campaniaService.Eliminar(entity);

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

                string path = Path.Combine(Server.MapPath("~/files/campania"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                return Json(new { file = file.FileName });
            }
            catch (Exception ex)
            {
                Log.Error($"CampaniaController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}