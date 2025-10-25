using PROMPERU.PeruInfo.ApplicationService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS.Controllers
{
    public class IdiomaController : Controller
    {
        #region Private Variables

        private readonly IIdiomaService _idiomaService;

        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller" /> class.</summary>
        public IdiomaController(IIdiomaService idiomaService)
        {
            _idiomaService = idiomaService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Listar idiomas.
        /// </summary>
        /// <returns></returns>
        
        public JsonResult Listar()
        {
            var data = new
            {
                data = _idiomaService.Listar()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}