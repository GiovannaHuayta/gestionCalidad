using AutoMapper;
using log4net;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.DTO;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    [Authorize]
    public class PalabrasAlientoController : Controller
    {
        #region Private Variables

        private readonly IPalabraAlientoService _PalabrasAlientoService;
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
        public PalabrasAlientoController(IPalabraAlientoService palabrasAlientoService, IMapper mapper)
        {
            _PalabrasAlientoService = palabrasAlientoService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        // GET: PalabrasAliento
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Listar Contenido Deportistas
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {

            var data = new
            {
                data = _mapper.Map<List<PalabrasAlientoBE>, List<PalabrasAlientoDTO>>(_PalabrasAlientoService.Listar())
            };

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;

            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;

            return json;
        }

        /// <summary>
        ///     Actualizar PalabrasAliento
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Actualizar(PalabrasAlientoBE entity)
        {
            entity.UsuarioEdicion = int.Parse(Session["UsuarioId"].ToString());
            entity.FechaEdicion = DateTime.UtcNow.AddHours(-5);


            bool result = _PalabrasAlientoService.Actualizar(entity);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Seleccionar(int id)
        {
            return Json(_PalabrasAlientoService.Seleccionar(id));
        }

        #endregion
    }
}