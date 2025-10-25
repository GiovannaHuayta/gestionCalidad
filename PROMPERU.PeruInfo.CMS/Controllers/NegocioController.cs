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
    public class NegocioController : Controller
    {
        #region Private Variables

        private readonly INegocioService _negocioService;
        private readonly IMapper _mapper;

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
        public NegocioController(INegocioService negocioService, IMapper mapper)
        {
            _negocioService = negocioService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        // GET: Negocio
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Negocios
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="alerta"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _mapper.Map<List<NegocioBE>, List<NegocioDTO>>(_negocioService.Listar(0, 0, string.Empty))
            };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;

            return json;
        }

        /// <summary>
        ///     Seleccionar Negocio por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id)
        {
            return Json(_mapper.Map<NegocioBE, NegocioDTO>(_negocioService.Seleccionar(id)));
        }

        /// <summary>
        ///     Insertar Negocio
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(NegocioBE entity)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            List<NegocioBE> Negocios = _negocioService.Listar(0, entity.IdiomaId, string.Empty) ?? new List<NegocioBE>();

            NegocioBE Negocio = Negocios.FirstOrDefault(x => x.Titulo == entity.Titulo) ?? new NegocioBE();

            if (Negocio == null)
            {
                entity.Slug = entity.Titulo.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = Negocios.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                var new_slug = Negocio.Slug;
                if (Negocio.Titulo != entity.Titulo)
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

            int result = _negocioService.Insertar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Negocio
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(NegocioBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            List<NegocioBE> Negocios = _negocioService.Listar(0, entity.IdiomaId, string.Empty) ?? new List<NegocioBE>();

            NegocioBE Negocio = Negocios.FirstOrDefault(x => x.Id == entity.Id) ?? new NegocioBE();

            if (Negocio == null)
            {
                entity.Slug = entity.Titulo.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = Negocios.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                var new_slug = Negocio.Slug;
                if (Negocio.Titulo != entity.Titulo)
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

            bool result = _negocioService.Actualizar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Negocio
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(NegocioBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _negocioService.Eliminar(entity);

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

                string path = Path.Combine(Server.MapPath("~/files/negocio"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                return Json(new { file = file.FileName });

            }
            catch (Exception ex)
            {
                Log.Error($"NegocioController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}