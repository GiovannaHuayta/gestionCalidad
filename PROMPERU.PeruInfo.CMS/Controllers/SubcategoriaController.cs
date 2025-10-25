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
using log4net;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class SubcategoriaController : Controller
    {
        #region Private Variables

        private readonly ISubcategoriaService _subcategoriaService;
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
        /// Insertar galería subcategoría.
        /// </summary>
        /// <param name="galeria"></param>
        /// <param name="entity"></param>
        private void InsertarGaleriaSubcategoria(GaleriaSubcategoria galeria, SubcategoriaBE entity)
        {
            if (galeria.ImagenId == null)
            {
                GaleriaBE galeriaBe = new GaleriaBE
                {
                    IdiomaId = entity.IdiomaId,
                    EntidadId = entity.Id,
                    EntidadTipo = "Subcategoria",
                    RutaDekstop = galeria.ImagenDesktop,
                    RutaMovil = galeria.ImagenMovil,
                    Uso = "portada",
                    CodigoYoutube = null,
                    FechaCreacion = DateTime.UtcNow.AddHours(-5),
                    UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString()),
                    IpCreacion = GetIp(),
                    TextoAlternativo = galeria.ImagenTextoAlternativo
                };
                
                _galeriaService.Insertar(galeriaBe);
            }

            if (galeria.ImagenBloqueCategoriaId != null) return;
            
            GaleriaBE galeriaBloqueCategoriaBe = new GaleriaBE
            {
                IdiomaId = entity.IdiomaId,
                EntidadId = entity.Id,
                EntidadTipo = "Subcategoria",
                RutaDekstop = galeria.ImagenBloqueCategoria,
                RutaMovil = null,
                Uso = "bloque",
                CodigoYoutube = null,
                FechaCreacion = DateTime.UtcNow.AddHours(-5),
                UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString()),
                IpCreacion = GetIp(),
                TextoAlternativo = galeria.ImagenBloqueCategoriaTextoAlternativo
            };

            _galeriaService.Insertar(galeriaBloqueCategoriaBe);
        }
        
        /// <summary>
        /// Actualizar galería de la subcategoría.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        private void ActualizarGaleriaSubcategoria(SubcategoriaBE entity, GaleriaSubcategoria galeria)
        {
            GaleriaBE galeriaBe = new GaleriaBE
            {
                IdiomaId = entity.IdiomaId,
                Id = galeria.ImagenId ?? 0,
                TraduccionId = galeria.ImagenTraduccionId ?? 0,
                EntidadId = entity.Id,
                EntidadTipo = "Subcategoria",
                RutaDekstop = galeria.ImagenDesktop,
                RutaMovil = galeria.ImagenMovil,
                Uso = "portada",
                CodigoYoutube = null,
                FechaModificacion = DateTime.UtcNow.AddHours(-5),
                UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString()),
                IpModificacion = GetIp(),
                TextoAlternativo = galeria.ImagenTextoAlternativo
            };

            _galeriaService.Actualizar(galeriaBe);

            GaleriaBE galeriaBloqueCategoriaBe = new GaleriaBE
            {
                IdiomaId = entity.IdiomaId,
                Id = galeria.ImagenBloqueCategoriaId ?? 0,
                TraduccionId = galeria.ImagenBloqueCategoriaTraduccionId ?? 0,
                EntidadId = entity.Id,
                EntidadTipo = "Subcategoria",
                RutaDekstop = galeria.ImagenBloqueCategoria,
                RutaMovil = null,
                Uso = "bloque",
                CodigoYoutube = null,
                FechaModificacion = DateTime.UtcNow.AddHours(-5),
                UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString()),
                IpModificacion = GetIp(),
                TextoAlternativo = galeria.ImagenBloqueCategoriaTextoAlternativo
            };

            _galeriaService.Actualizar(galeriaBloqueCategoriaBe);
        }

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="subcategoriaService"></param>
        /// <param name="galeriaService"></param>
        public SubcategoriaController(ISubcategoriaService subcategoriaService, IGaleriaService galeriaService)
        {
            _subcategoriaService = subcategoriaService;
            _galeriaService = galeriaService;
        }

        #endregion

        #region Public Methods

        // GET: Subcategoria
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Subcategorias
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _subcategoriaService.Listar(0)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar Subcategoria por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            return Json(_subcategoriaService.Seleccionar(id, idiomaId));
        }

        /// <summary>
        ///     Insertar Subcategoria
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(SubcategoriaBE entity, GaleriaSubcategoria galeria)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            List<SubcategoriaBE> subcategorias = _subcategoriaService.Listar(entity.IdiomaId) ?? new List<SubcategoriaBE>();

            SubcategoriaBE subcategoria = subcategorias.FirstOrDefault(x => x.Nombre == entity.Nombre) ?? new SubcategoriaBE();

            if (subcategoria.Slug == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = subcategorias.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                string newSlug = subcategoria.Slug;
                if (subcategoria.Nombre != entity.Nombre)
                {
                    newSlug = slugs.Count > 1 ? entity.Nombre.GenerateUniqueSlug(slugs) : entity.Nombre.GenerateSlug();
                }

                entity.Slug = newSlug;
            }

            int result = _subcategoriaService.Insertar(entity);

            InsertarGaleriaSubcategoria(galeria, entity);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Subcategoria
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="galeria"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(SubcategoriaBE entity, GaleriaSubcategoria galeria)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            List<SubcategoriaBE> subcategorias = _subcategoriaService.Listar(entity.IdiomaId) ?? new List<SubcategoriaBE>();

            SubcategoriaBE subcategoria = subcategorias.FirstOrDefault(x => x.Id == entity.Id) ?? new SubcategoriaBE();

            if (subcategoria.Slug == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = subcategorias.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                string newSlug = subcategoria.Slug;
                
                if (subcategoria.Nombre != entity.Nombre)
                {
                    newSlug = slugs.Count > 1 ? entity.Nombre.GenerateUniqueSlug(slugs) : entity.Nombre.GenerateSlug();
                }

                entity.Slug = newSlug;
            }

            bool result = _subcategoriaService.Actualizar(entity);

            if (galeria.ImagenId == null || galeria.ImagenBloqueCategoriaId == null)
            {
                InsertarGaleriaSubcategoria(galeria, entity);
            }
            else
            {
                ActualizarGaleriaSubcategoria(entity, galeria);
            }
            
            return Json(result);
        }

        /// <summary>
        ///     Eliminar Subcategoria
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(SubcategoriaBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _subcategoriaService.Eliminar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Listar subcategorias en select.
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarParaSelect()
        {
            var data = new
            {
                data = _subcategoriaService.Listar(1)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Lista los registros por categoria id.
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        public JsonResult ListarPorCategoria(int categoriaId)
        {
            var data = new
            {
                data = _subcategoriaService.Listar(categoriaId, 1)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
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

                string path = Path.Combine(Server.MapPath("~/files/subcategoria"), Path.GetFileName(file.FileName));
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