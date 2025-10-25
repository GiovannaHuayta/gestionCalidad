using PROMPERU.PeruInfo.ApplicationService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    
    public class TipoController : Controller
    {
        #region Private Variables

        private readonly ITipoService _tipoService;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public TipoController(ITipoService tipoService)
        {
            _tipoService = tipoService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Listar Tipos en select.
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarParaSelect()
        {
            var data = new
            {
                data = _tipoService.Listar(1)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}