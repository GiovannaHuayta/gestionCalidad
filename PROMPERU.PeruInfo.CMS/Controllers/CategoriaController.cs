using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.CMS.Models;
using PROMPERU.PeruInfo.Domain.Entities;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Reflection;
using System.Web.Script.Serialization;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        #region Private Variables

        private readonly ICategoriaService _categoriaService;
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
        /// Insertar la galería de la categoría.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <param name="result"></param>
        private void InsertGaleriaCategoria(CategoriaBE entity, GaleriaBE galeria, int result)
        {
            galeria.EntidadId = result;
            galeria.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            galeria.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            galeria.IdiomaId = entity.IdiomaId;
            galeria.IpCreacion = GetIp();

            _galeriaService.Insertar(galeria);
        }

        /// <summary>
        /// Actualizar la galería de la categoría.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        private void ActualizarGaleriaCategoria(CategoriaBE entity, GaleriaBE galeria)
        {
            galeria.Id = entity.ImagenId ?? 0;
            galeria.TraduccionId = entity.ImagenTraduccionId ?? 0;
            galeria.EntidadId = entity.Id;
            galeria.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            galeria.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            galeria.IpModificacion = GetIp();

            _galeriaService.Actualizar(galeria);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoriaService"></param>
        /// <param name="galeriaService"></param>
        public CategoriaController(ICategoriaService categoriaService, IGaleriaService galeriaService)
        {
            _categoriaService = categoriaService;
            _galeriaService = galeriaService;
        }

        #endregion

        #region Public Methods

        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Categorias
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _categoriaService.Listar(0)
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
        ///     Seleccionar Categoria por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            return Json(_categoriaService.Seleccionar(id, idiomaId));
        }

        /// <summary>
        ///     Insertar Categoria
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(CategoriaBE entity, GaleriaBE galeria)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            List<CategoriaBE> categorias = _categoriaService.Listar(entity.IdiomaId);

            CategoriaBE categoria = categorias.FirstOrDefault(x => x.Nombre == entity.Nombre);

            if (categoria == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = categorias.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                string newSlug = categoria.Slug;

                if (categoria.Nombre != entity.Nombre)
                {
                    newSlug = slugs.Count() > 1
                        ? entity.Nombre.GenerateUniqueSlug(slugs)
                        : entity.Nombre.GenerateSlug();
                }

                entity.Slug = newSlug;
            }

            int result = _categoriaService.Insertar(entity);

            InsertGaleriaCategoria(entity, galeria, result);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Categoria
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(CategoriaBE entity, GaleriaBE galeria)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            List<CategoriaBE> categorias = _categoriaService.Listar(entity.IdiomaId);

            CategoriaBE categoria = categorias.FirstOrDefault(x => x.Id == entity.Id);

            if (categoria == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = categorias.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                string newSlug = categoria.Slug;

                if (categoria.Nombre != entity.Nombre)
                {
                    newSlug = slugs.Count() > 1
                        ? entity.Nombre.GenerateUniqueSlug(slugs)
                        : entity.Nombre.GenerateSlug();
                }

                entity.Slug = newSlug;
            }

            bool result = _categoriaService.Actualizar(entity);

            if (entity.ImagenId == 0)
            {
                InsertGaleriaCategoria(entity, galeria, entity.Id);
            }
            else
            {
                ActualizarGaleriaCategoria(entity, galeria);
            }

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Categoria
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(CategoriaBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _categoriaService.Eliminar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Listar Categorias en select.
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarParaSelect()
        {
            var data = new
            {
                data = _categoriaService.Listar(1)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
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

                string path = Path.Combine(Server.MapPath("~/files/categoria"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                return Json(new { file = file.FileName });
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}