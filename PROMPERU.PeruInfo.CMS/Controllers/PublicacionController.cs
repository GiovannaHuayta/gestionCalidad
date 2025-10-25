using AutoMapper;
using log4net;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.DTO;
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

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class PublicacionController : Controller
    {
        #region Private Variables

        private readonly IPublicacionService _publicacionService;
        private readonly IMapper _mapper;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

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
        public PublicacionController(IPublicacionService publicacionService, IMapper mapper)
        {
            _publicacionService = publicacionService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        // GET: Publicacion
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Publicacions
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _mapper.Map<List<PublicacionBE>, List<PublicacionDTO>>(_publicacionService.Listar(0))
            };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;

            return json;
        }

        /// <summary>
        ///     Seleccionar Publicacion por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            return Json(_mapper.Map<PublicacionBE, PublicacionDTO>(_publicacionService.Seleccionar(id, idiomaId)));
        }

        /// <summary>
        ///     Insertar Publicacion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(PublicacionBE entity)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            List<PublicacionBE> publicaciones = _publicacionService.Listar(entity.IdiomaId);

            PublicacionBE publicacion = publicaciones.FirstOrDefault(x => x.Titulo == entity.Titulo);

            if (publicacion == null)
            {
                entity.Slug = entity.Titulo.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = publicaciones.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                var new_slug = publicacion.Slug;
                if (publicacion.Titulo != entity.Titulo)
                {
                    if (slugs.Count() > 1)
                    {
                        new_slug = entity.Titulo.GenerateUniqueSlug(slugs);
                    }
                    else
                    {
                        new_slug = entity.Titulo.GenerateSlug();
                    }
                }

                entity.Slug = new_slug;
            }

            int result = _publicacionService.Insertar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Publicacion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(PublicacionBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            List<PublicacionBE> publicaciones = _publicacionService.Listar(entity.IdiomaId);

            PublicacionBE publicacion = publicaciones.FirstOrDefault(x => x.Id == entity.Id);

            //if (publicacion == null)
            //{
            //    entity.Slug = entity.Titulo.GenerateSlug();
            //}
            //else
            //{
            //    // Para la generación de slugs únicos
            //    List<RutaSlug> slugs = publicaciones.Select(x => new RutaSlug
            //    {
            //        Slug = x.Slug
            //    }).ToList();

            //    var new_slug = publicacion.Slug;
            //    if (publicacion.Titulo != entity.Titulo)
            //    {
            //        if (slugs.Count() > 1)
            //        {
            //            new_slug = entity.Titulo.GenerateUniqueSlug(slugs);
            //        }
            //        else
            //        {
            //            new_slug = entity.Titulo.GenerateSlug();
            //        }
            //    }

            //    entity.Slug = new_slug;
            //}

            bool result = _publicacionService.Actualizar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Publicacion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(PublicacionBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _publicacionService.Eliminar(entity);

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


                //string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

                //string cleanFileName = RemoveInvalidFileNameChars(fileNameWithoutExtension);

                
                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string newFileName = "publicacion" + "_" + timeStamp + Path.GetExtension(file.FileName);

                string path = Path.Combine(Server.MapPath("~/files/publicacion"), newFileName);
                file.SaveAs(path);

                return Json(new { file = newFileName });

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