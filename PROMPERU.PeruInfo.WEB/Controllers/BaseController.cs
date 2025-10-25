using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using PROMPERU.PeruInfo.WEB.Helpers;

namespace PROMPERU.PeruInfo.WEB.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        #region Private Variables

        private readonly IIdiomaService _idiomaService;
        private readonly IPaginaService _paginaService;
        private readonly IPublicacionService _publicacionService;
        private readonly IComunicadoService _comunicadoService;

        #endregion

        #region Constructor

        /// <summary>
        /// Base controller constructor.
        /// </summary>
        /// <param name="idiomaService"></param>
        /// <param name="paginaService"></param>
        /// <param name="publicacionService"></param>
        /// <param name="comunicadoService"></param>
        public BaseController(
            IIdiomaService idiomaService, 
            IPaginaService paginaService,
            IPublicacionService publicacionService,
            IComunicadoService comunicadoService)
        {
            _idiomaService = idiomaService;
            _paginaService = paginaService;
            _publicacionService = publicacionService;
            _comunicadoService = comunicadoService;
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ipPublica = Request.UserHostAddress;
            ViewBag.IpPublica = ipPublica;
            string sessionIdioma = filterContext.RouteData.Values["culture"]?.ToString();
            string idiomaPais = sessionIdioma;

            if (string.IsNullOrEmpty(sessionIdioma))
            {
                string respuestaApi = ObtenerRespuestaApi();

                string paisResponse = respuestaApi.Substring(1, 2);

                string[] idiomas = { "ar", "bo", "cl", "co", "cr", "cu", "do", "ec", "sv", "gq", "gt", "hn", "mx", "ni", "pa", "py", "pe", "pr", "es", "uy", "ve", "er", "ER" };

                int indice = Array.IndexOf(idiomas, paisResponse);
                if (indice >= 0)
                {
                    idiomaPais = "es-pe";
                }
                else
                {
                    idiomaPais = "en-us";
                }
            }            

            string lang = filterContext.RouteData.Values["culture"]?.ToString() ?? idiomaPais;

            filterContext.ActionParameters["culture"] = lang.ToLower();

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(lang.ToLower());

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            ViewBag.Culture = lang.ToLower();
            LanguageManager.SetLanguage(lang.ToLower());
            base.OnActionExecuting(filterContext);
        }


        #endregion

        #region Public Methods
        public void ValidarPagina(bool condicion) {
            if (condicion)
            {
                throw new HttpException(404, "Bad Request");
            }
        }
        public string ObtenerRespuestaApi()
        {
            string ipPublica = Request.UserHostAddress;

            var urlApi = ConfigurationManager.AppSettings["UrlApi"];
            var usuarioApi = ConfigurationManager.AppSettings["UsuarioSitioWeb"];
            var claveApi = ConfigurationManager.AppSettings["ClaveSitioWeb"];
            
            string datosLogin = usuarioApi + ":" + claveApi;

            HttpMessageHandler handler = new HttpClientHandler()
            {
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(urlApi),
                Timeout = new TimeSpan(0, 2, 0)
            };

            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(datosLogin);
            string val = System.Convert.ToBase64String(plainTextBytes);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

            StringBuilder postData = new StringBuilder();
            postData.Append(String.Format("{0}={1}", HttpUtility.HtmlEncode("Descripcion"), HttpUtility.HtmlEncode(ipPublica)));

            StringContent myStringContent = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = httpClient.PostAsync(urlApi, myStringContent).Result;
            string content = string.Empty;

            using (StreamReader stream = new StreamReader(response.Content.ReadAsStreamAsync().Result, System.Text.Encoding.GetEncoding(65001)))
            {
                content = stream.ReadToEnd();
            }

            return content;
        }

        public string ObtenerIpPublica()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] ipAddressWithText = response.Split(':');
            string ipAddressWithHTMLEnd = ipAddressWithText[1].Substring(1);
            string[] ipAddress = ipAddressWithHTMLEnd.Split('<');
            string mainIP = ipAddress[0];

            return mainIP;
        }

        /// <summary>
        /// Redirect to default culture.
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToLocalized()
        {
            HttpCookie langCookie = Request.Cookies["culture"];

            return RedirectPermanent(langCookie == null ? "/es-pe/" : $"/{langCookie.Value}/");
        }

        /// <summary>
        /// Language menu partial view.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult LanguageMenu()
        {
            return PartialView("Partials/_LanguageMenu", _idiomaService.Listar());
        }

        /// <summary>
        /// Page menu partial view.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PageMenu(bool footer = false)
        {
            List<PaginaBE> items = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.Posicion != "Header" && x.Posicion != "Home" && x.Posicion != "Footer")
                .ToList();

            return PartialView(!footer ? "Partials/_PageMenu" : "Partials/_Footer", items);
        }

        [ChildActionOnly]
        public ActionResult HeaderMenu()
        {
            dynamic model = new ExpandoObject();
            model.Alertas = _comunicadoService
                .Listar(1, ViewBag.Culture == "es-pe" ? 1 : 2)
                .OrderByDescending(x => x.Id)
                .Take(3)
                .ToList();

            model.Paginas = _paginaService
                .ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.Posicion == "Header")
                .ToList();

            return PartialView("Partials/_HeaderMenu", model);
        }

        /// <summary>
        /// Listar publicaciones por.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Publicaciones(int tipo)
        {
            return PartialView(tipo == 2 ? "Partials/_Noticias" : "Partials/_Blog",
                _publicacionService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2, tipo));
        }

        /// <summary>
        /// Lista publicaciones para una categoría.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <returns></returns>
        public ActionResult PublicacionesInterna(int tipo, string categoria, int subcategoria = 0)
        {
            IEnumerable<PublicacionBE> publicaciones = _publicacionService
                .Listar(ViewBag.Culture == "es-pe" ? 1 : 2, tipo)
                .Where(x => x.CategoriaNombre == categoria);

            if (subcategoria != 0)
            {
                publicaciones = publicaciones.Where(x => x.SubcategoriaId == subcategoria);
            }

            return PartialView("Partials/_NoticiasInterna", publicaciones.ToList());
        }

        /// <summary>
        /// Listado de publicaciones relacionadas recientes.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="subcategoria"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Relacionados(int tipo, int subcategoria = 0)
        {
            List<PublicacionBE> publicaciones = _publicacionService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2, tipo);

            if (subcategoria != 0)
            {
                publicaciones = publicaciones.Where(x => x.SubcategoriaId == subcategoria).ToList();
            }

            return PartialView(subcategoria == 0
                    ? "Partials/_RelacionadosCategoria"
                    : "Partials/_Relacionados",
                publicaciones.Take(3).ToList());
        }

        /// <summary>
        /// Vista de alerta
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Alertas()
        {
            return PartialView("Partials/_AlertModal", 
                _comunicadoService
                    .Listar(1, ViewBag.Culture == "es-pe" ? 1 : 2)
                    .OrderByDescending(x => x.Id)
                    .Take(2)
                    .ToList());
        }

        #endregion
    }
}