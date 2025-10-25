using PROMPERU.PeruInfo.ApplicationService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.WEB.Controllers
{
    [HandleError]
    public class ErrorController : BaseController
    {
        #region Private Variables

        private readonly IPaginaService _paginaService;

      

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="idiomaService"></param>
        /// <param name="paginaService"></param>
        /// <param name="publicacionService"></param>
        /// <param name="comunicadoService"></param>
        public ErrorController(
            IIdiomaService idiomaService, 
            IPaginaService paginaService, 
            IPublicacionService publicacionService, 
            IComunicadoService comunicadoService) 
            : base(idiomaService, paginaService, publicacionService, comunicadoService)
        {
            _paginaService = paginaService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Vista de privacidad (Error)
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Index(string slug)
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        #endregion
    }
}