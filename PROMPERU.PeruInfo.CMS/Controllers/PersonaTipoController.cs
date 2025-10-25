using AutoMapper;
using log4net;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using System.Reflection;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    
    public class PersonaTipoController : Controller
    {
        #region Private Variables

        private readonly IPersonaTipoService _personaTipoService;
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
        public PersonaTipoController(IPersonaTipoService personaTipoService, IMapper mapper)
        {
            _personaTipoService = personaTipoService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Listar Personas Tipo
        /// </summary>
        /// <returns></returns>
        public JsonResult Listar()
        {
            var data = new
            {
                data = _personaTipoService.Listar(1)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}