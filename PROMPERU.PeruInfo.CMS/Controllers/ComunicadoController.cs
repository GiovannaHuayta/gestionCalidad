using AutoMapper;
using log4net;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.DTO;
using PROMPERU.PeruInfo.CMS.Filtros;
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
    
    public class ComunicadoController : Controller
    {
        #region Private Variables

        private readonly IComunicadoService _comunicadoService;
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
        public ComunicadoController(IComunicadoService ComunicadoService, IMapper mapper)
        {
            _comunicadoService = ComunicadoService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        // GET: Comunicado
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Comunicados
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="alerta"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _mapper.Map<List<ComunicadoBE>, List<ComunicadoDTO>>(_comunicadoService.Listar(0, false, string.Empty))
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Seleccionar Comunicado por id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seleccionar(int id, int idiomaId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var comunicadoDTO = _mapper.Map<ComunicadoBE, ComunicadoDTO>(_comunicadoService.Seleccionar(id, idiomaId));
                return Json(comunicadoDTO);
            }
            else
            {
                // Usuario no autenticado, devuelve un estado 401
                Response.StatusCode = 401;
                return Json(new { error = "Usuario no autenticado" });
            }
        }

        /// <summary>
        ///     Insertar Comunicado
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insertar(ComunicadoBE entity)
        {
            entity.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            entity.IpCreacion = GetIp();

            entity.Slug = entity.Titulo.GenerateSlug();

            int result = _comunicadoService.Insertar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Actualizar Comunicado
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(ComunicadoBE entity)
        {
            entity.UsuarioModificacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            entity.IpModificacion = GetIp();

            entity.Slug = entity.Titulo.GenerateSlug();

            bool result = _comunicadoService.Actualizar(entity);

            return Json(result);
        }

        /// <summary>
        ///     Eliminar Comunicado
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Eliminar(ComunicadoBE entity)
        {
            entity.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            entity.IpEliminacion = GetIp();

            bool result = _comunicadoService.Eliminar(entity);

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

                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string newFileName = "comunicado" + "_" + timeStamp + Path.GetExtension(file.FileName);


                string path = Path.Combine(Server.MapPath("~/files/comunicado"), newFileName);
                file.SaveAs(path);

                return Json(new { file = newFileName });

            }
            catch (Exception ex)
            {
                Log.Error($"ComunicadoController:::{ex.Message}");

                return Json(new { message = ex.Message });
            }
        }

        #endregion
    }
}