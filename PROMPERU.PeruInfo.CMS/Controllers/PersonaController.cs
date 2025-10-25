using AutoMapper;
using log4net;
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

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        #region Private Variables

        private readonly IPersonaService _personaService;
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
        public PersonaController(IPersonaService personaService, IMapper mapper)
        {
            _personaService = personaService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        // GET: Persona
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Personas
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="alerta"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _personaService.Listar(0, 0, string.Empty)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar Persona por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            return Json(_personaService.Seleccionar(id, idiomaId));
        }

        /// <summary>
        ///     Insertar Persona
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(PersonaBE entity)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            List<PersonaBE> Personaes = _personaService.Listar(entity.IdiomaId, 0, string.Empty) ?? new List<PersonaBE>();

            PersonaBE Persona = Personaes.FirstOrDefault(x => x.Nombre == entity.Nombre) ?? new PersonaBE();

            if (Persona == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = Personaes.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                var new_slug = Persona.Slug;
                if (Persona.Nombre != entity.Nombre)
                {
                    if (slugs.Count() > 1)
                    {
                        new_slug = entity.Nombre.GenerateUniqueSlug(slugs);
                    }
                    else
                    {
                        new_slug = entity.Nombre.GenerateSlug();
                    }
                }

                entity.Slug = new_slug;
            }

            int result = _personaService.Insertar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Persona
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(PersonaBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            List<PersonaBE> Personaes = _personaService.Listar(entity.IdiomaId, entity.SubcategoriaId, entity.Nombre) ?? new List<PersonaBE>();

            PersonaBE Persona = Personaes.FirstOrDefault(x => x.Id == entity.Id) ?? new PersonaBE();

            if (Persona == null)
            {
                entity.Slug = entity.Nombre.GenerateSlug();
            }
            else
            {
                // Para la generación de slugs únicos
                List<RutaSlug> slugs = Personaes.Select(x => new RutaSlug
                {
                    Slug = x.Slug
                }).ToList();

                var new_slug = Persona.Slug;
                if (Persona.Nombre != entity.Nombre)
                {
                    if (slugs.Count() > 1)
                    {
                        new_slug = entity.Nombre.GenerateUniqueSlug(slugs);
                    }
                    else
                    {
                        new_slug = entity.Nombre.GenerateSlug();
                    }
                }

                entity.Slug = new_slug;
            }

            bool result = _personaService.Actualizar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Persona
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(PersonaBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _personaService.Eliminar(entity);

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

                string path = Path.Combine(Server.MapPath("~/files/persona"), Path.GetFileName(file.FileName));
                file.SaveAs(path);

                return Json(new { file = file.FileName });

            }
            catch (Exception ex)
            {
                Log.Error($"PersonaController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}