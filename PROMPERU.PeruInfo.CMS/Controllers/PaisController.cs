using PROMPERU.PeruInfo.ApplicationService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    public class PaisController : Controller
    {
        #region Private Variables

        private readonly IPaisService _paisService;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Listar Paiss en select.
        /// </summary>
        /// <returns></returns>
        public JsonResult ListarParaSelect()
        {
            var data = new
            {
                data = _paisService.Listar(1)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}