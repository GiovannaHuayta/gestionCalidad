using System;
using AutoMapper;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.DTO;
using PROMPERU.PeruInfo.Domain.Entities;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web;
using PROMPERU.PeruInfo.WEB.Helpers;
using System.Text;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace PROMPERU.PeruInfo.WEB.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        #region Private Variables

        private readonly ICategoriaService _categoriaService;
        private readonly ISubcategoriaService _subcategoriaService;
        private readonly ITarjetaService _tarjetaService;
        private readonly IPaginaService _paginaService;
        private readonly INegocioService _negocioService;
        private readonly IComunicadoService _comunicadoService;
        private readonly IPersonaService _personaService;
        private readonly IPublicacionService _publicacionService;
        private readonly IContactoService _contactoService;
        private readonly IBannerService _bannerService;
        private readonly IGaleriaService _galeriaService;
        private readonly IPaisService _paisService;
        private readonly IPersonaTipoService _personaTipoService;
        private readonly ICampaniaService _campaniaService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Home controller constructor.
        /// </summary>
        /// <param name="paginaService"></param>
        /// <param name="categoriaService"></param>
        /// <param name="subcategoriaService"></param>
        /// <param name="tarjetaService"></param>
        /// <param name="idiomaService"></param>
        /// <param name="negocioService"></param>
        /// <param name="comunicadoService"></param>
        /// <param name="personaService"></param>
        /// <param name="publicacionService"></param>
        /// <param name="contactoService"></param>
        /// <param name="bannerService"></param>
        /// <param name="galeriaService"></param>
        /// <param name="paisService"></param>
        /// <param name="personaTipoService"></param>
        /// <param name="campaniaService"></param>
        /// <param name="mapper"></param>
        public HomeController(
            ICategoriaService categoriaService,
            ISubcategoriaService subcategoriaService,
            ITarjetaService tarjetaService,
            IIdiomaService idiomaService,
            IPaginaService paginaService,
            INegocioService negocioService,
            IComunicadoService comunicadoService,
            IPersonaService personaService,
            IPublicacionService publicacionService,
            IContactoService contactoService,
            IBannerService bannerService,
            IGaleriaService galeriaService,
            IPaisService paisService,
            IPersonaTipoService personaTipoService,
            ICampaniaService campaniaService,
            IMapper mapper)
            : base(idiomaService, paginaService, publicacionService, comunicadoService)
        {
            _categoriaService = categoriaService;
            _subcategoriaService = subcategoriaService;
            _tarjetaService = tarjetaService;
            _paginaService = paginaService;
            _negocioService = negocioService;
            _comunicadoService = comunicadoService;
            _personaService = personaService;
            _publicacionService = publicacionService;
            _contactoService = contactoService;
            _bannerService = bannerService;
            _galeriaService = galeriaService;
            _paisService = paisService;
            _personaTipoService = personaTipoService;
            _campaniaService = campaniaService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            dynamic homeModel = new ExpandoObject();
            homeModel.Categorias = _categoriaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2);
            homeModel.Banner = _bannerService.Listar()
                .Where(x => x.PaginaId == 1)
                .Where(x => x.IdiomaId == (ViewBag.Culture == "es-pe" ? 1 : 2))
                //Agregado por OTI
                .Where(x => x.Activo == true)
                .ToList();
            //Generar documento XML para el sitemap
            homeModel.Paginas = _paginaService
                .ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.Informativa == false)
                .ToList();
            homeModel.publicaciones= _publicacionService
                .Listar(ViewBag.Culture == "es-pe" ? 1 : 2);
            SiteMaps crearSiteMap = new SiteMaps();
            crearSiteMap.GenerarSiteMap(homeModel.Paginas, homeModel.publicaciones, ViewBag.Culture);
            return View(homeModel);
        }

        /// <summary>
        /// Vista de las categorías.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Categoria(string slug)
        {
            CategoriaBE categoria = _categoriaService.SeleccionarPorSlug(ViewBag.Culture == "es-pe" ? 1 : 2, slug);

            //Validando Si existe categoria
            ValidarPagina(categoria.Activo == null);


            List<SubcategoriaBE> subcategorias =
                _subcategoriaService.Listar(categoria.Id, ViewBag.Culture == "es-pe" ? 1 : 2);

            List<GaleriaBE> galeria = _galeriaService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2, "Subcategoria");

            dynamic categoriaModel = new ExpandoObject();
            categoriaModel.Categoria = categoria;
            categoriaModel.Subcategorias = subcategorias;
            categoriaModel.Galeria = galeria;
            categoriaModel.Banners = _bannerService.Listar()
                .Where(x => x.CategoriaId == categoria.Id)
                .Where(x => x.IdiomaId == (ViewBag.Culture == "es-pe" ? 1 : 2))
                .ToList();

            return View("~/Views/Categoria/Index.cshtml", categoriaModel);
        }

        /// <summary>
        /// Vista de la subcategoría.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Subcategoria(string categoria, string slug)
        {
            SubcategoriaBE subcategoria =
                _subcategoriaService.Seleccionar(categoria, slug, ViewBag.Culture == "es-pe" ? 1 : 2);

            ValidarPagina(subcategoria == null);

            List<TarjetaBE> tarjetas = _tarjetaService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2, subcategoria.Id)
                .OrderBy(x => x.Orden)
                .ToList();
            GaleriaBE galeria = _galeriaService
                .Listar(ViewBag.Culture == "es-pe" ? 1 : 2, "Subcategoria", subcategoria.Id)
                .FirstOrDefault(x => x.Uso == "portada");

            dynamic subcategoriaModel = new ExpandoObject();
            subcategoriaModel.Subcategoria = subcategoria;
            subcategoriaModel.Tarjetas = tarjetas ?? new List<TarjetaBE>();
            subcategoriaModel.Galeria = galeria;
            subcategoriaModel.Banners = _bannerService.Listar()
                .Where(x => x.SubcategoriaId == subcategoria.Id)
                .Where(x => x.IdiomaId == (ViewBag.Culture == "es-pe" ? 1 : 2))
                .ToList();

            return View("~/Views/Subcategoria/Index.cshtml", subcategoriaModel);
        }

        /// <summary>
        /// Vista del resultado de busqueda.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Busqueda(string categoria, string subcategoria, int? id)
        {
            if(id is null)
            {
                throw new HttpException(404, "Publicación no encontrada");
            }

            List<PaginaBE> paginas = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2).Where(x=>x.Slug == categoria+"/"+subcategoria+"/"+id).ToList();

            List<CategoriaBE> categorias = _categoriaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.VisibleBuscador == true)
                .ToList();

            List<SubcategoriaBE> subcategorias = _subcategoriaService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.VisibleBuscador == true)
                .ToList();

            int idiomaId = ViewBag.Culture == "es-pe" ? 1 : 2;

            dynamic busquedaModel = new ExpandoObject();
            if (idiomaId == 1)
            {
                busquedaModel.TipoId = subcategoria == "noticias" ? "2" : "1";
            }
            else
            {
                busquedaModel.TipoId = subcategoria == "news" ? "2" : "1";
            }

            busquedaModel.CategoriaSlug = categoria;
            busquedaModel.Categorias = categorias;
            busquedaModel.Subcategorias = subcategorias;
            busquedaModel.Paginas = paginas;


            return View("~/Views/Busqueda/Index.cshtml", busquedaModel);
        }

        /// <summary>
        /// Busqueda de publicaciones
        /// </summary>
        /// <returns></returns>
        public JsonResult PublicacionApi(string idioma, string tipo, string buscado, string categoria,
            string subcategoria, string inicio, string fin)
        {
            if (categoria == null)
                subcategoria = null;

            List<PublicacionDTO> publicaciones =
                _mapper.Map<List<PublicacionBE>, List<PublicacionDTO>>(
                    _publicacionService.Listar(idioma, tipo, buscado, categoria, subcategoria, inicio, fin));

            return Json(new { data = publicaciones.Take(800) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Vista búsqueda home.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public ActionResult BusquedaHome(string section)
        {
            List<PaginaBE> paginas = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2).Where(x => x.Slug == section).ToList();

            List<CategoriaBE> categorias = _categoriaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.VisibleBuscador == true)
                .ToList();

            List<SubcategoriaBE> subcategorias = _subcategoriaService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.VisibleBuscador == true)
                .ToList();

            int idiomaId = ViewBag.Culture == "es-pe" ? 1 : 2;

            dynamic busquedaModel = new ExpandoObject();
            if (idiomaId == 1)
            {
                busquedaModel.TipoId = section == "noticias" ? "2" : "1";
            }
            else
            {
                busquedaModel.TipoId = section == "news" ? "2" : "1";
            }
            busquedaModel.CategoriaSlug = "";
            busquedaModel.Categorias = categorias;
            busquedaModel.Subcategorias = subcategorias;
            busquedaModel.Paginas = paginas;

            return View("~/Views/Busqueda/Index.cshtml", busquedaModel);
        }

        /// <summary>
        /// Vista de blog / noticia.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="seccion"></param>
        /// <param name="categoriaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Publicacion(string culture, string categoria, string seccion, int categoriaId, int subcategoriaId,
            string slug)
        {
            // TODO: actualizar contador.
            if(culture.ToLower() != "es-pe" && culture.ToLower() != "en-us")
            {
                throw new HttpException(404, "Publicación no encontrada");
            }

            var publicacion = _publicacionService.Seleccionar(slug, categoriaId, subcategoriaId, ViewBag.Culture == "es-pe" ? 1 : 2);

            // Verificar si la publicación es nula
            if (publicacion == null)
            {
                // Lanza una excepción HttpException con código 404
                throw new HttpException(404, "Publicación no encontrada");
            }

            if (publicacion.CategoriaSlug != categoria)
            {
                throw new HttpException(404, "Publicación no encontrada");
            }

            if (ViewBag.Culture == "es-pe")
            {
                if (seccion != "blogperu")
                {
                    throw new HttpException(404, "Publicación no encontrada");
                }
            }
            else
            {
                if (seccion != "blogperu")
                {
                    throw new HttpException(404, "Publicación no encontrada");
                }
            }

            // Retorna la vista si los datos existen
            return View("~/Views/Publicacion/Index.cshtml", publicacion);
        }

        /// <summary>
        /// Vista de embajadores / campañas / amigos del Perú.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Embajadores(string categoria, string slug)
        {
            SubcategoriaBE subcategoria =
                _subcategoriaService.Seleccionar(categoria, slug, ViewBag.Culture == "es-pe" ? 1 : 2);

            dynamic embajadorModel = new ExpandoObject();
            embajadorModel.Subcategoria = subcategoria;
            embajadorModel.Personas = _personaService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2, slug);
            embajadorModel.PersonaTipo = _personaTipoService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2);
            embajadorModel.Galeria = _galeriaService
                .Listar(ViewBag.Culture == "es-pe" ? 1 : 2, "Subcategoria", subcategoria.Id)
                .FirstOrDefault(x => x.Uso == "portada");

            return View("~/Views/Embajadores/Index.cshtml", embajadorModel);
        }

        /// <summary>
        /// Busqueda de embajadores.
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="idioma"></param>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public JsonResult EmbajadoresApi(string persona, string idioma, string nombre, string tipo)
        {
            IEnumerable<PersonaBE> personas = _personaService.Listar(int.Parse(idioma), persona);

            if (!string.IsNullOrEmpty(nombre))
            {
                personas = personas.Where(x => x.Nombre.ToLower().Contains(nombre.ToLower()));
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                personas = personas.Where(x => x.Tipo == int.Parse(tipo));
            }

            return Json(new { data = personas.ToList() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Vista detalle de embajador
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Embajador(string categoria, string subcategoria, string slug)
        {
            return View("~/Views/Embajadores/Detalle.cshtml",
                _personaService.Seleccionar(ViewBag.Culture == "es-pe" ? 1 : 2, subcategoria, slug));
        }

        /// <summary>
        /// Vista premios.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Premios(string categoria, string subcategoria, string slug)
        {
            return View("~/Views/Premios/Index.cshtml");
        }

        /// <summary>
        /// Vista restaurantes en el mundo.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Restaurantes(string categoria, string slug)
        {
            SubcategoriaBE subcategoria =
                _subcategoriaService.Seleccionar(categoria, slug, ViewBag.Culture == "es-pe" ? 1 : 2);

            CategoriaBE categoriaData =
                _categoriaService.SeleccionarPorSlug(ViewBag.Culture == "es-pe" ? 1 : 2, categoria);

            List<NegocioBE> negocios = _negocioService.Listar()
                .Where(x => x.IdiomaId == (ViewBag.Culture == "es-pe" ? 1 : 2))
                .ToList();

            List<PaisBE> paises = _paisService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2);

            dynamic restaurantesModel = new ExpandoObject();
            restaurantesModel.Subcategoria = subcategoria;
            restaurantesModel.Categoria = categoriaData;
            restaurantesModel.Negocios = negocios;
            restaurantesModel.Paises = paises;

            return View("~/Views/Restaurantes/Index.cshtml", restaurantesModel);
        }

        /// <summary>
        /// Busqueda de restaurantes
        /// </summary>
        /// <returns></returns>
        public JsonResult RestaurantesApi(string idioma, string nombre, string pais)
        {
            try
            {
                IEnumerable<NegocioBE> restaurantes = _negocioService.Listar()
                    .Where(x => x.IdiomaId == int.Parse(idioma));

                if (!string.IsNullOrEmpty(nombre))
                {
                    restaurantes = restaurantes.Where(x => x.Titulo.ToLower().Contains(nombre.ToLower()));
                }

                if (!string.IsNullOrEmpty(pais))
                {
                    restaurantes = restaurantes.Where(x => x.PaisId == int.Parse(pais));
                }

                return Json(new { data = restaurantes.ToList().Take(500) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = ex.Message,
                    trace = ex
                });
            }
        }

        /// <summary>
        /// Vista nuestras apps.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Apps(string slug)
        {
            List<PaginaBE> paginas = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2).Where(x => x.Slug == slug).ToList();
           // busquedaModel.Paginas = paginas;
           ViewBag.Paginas = paginas;

            return View("~/Views/Apps/Index.cshtml");
        }

        /// <summary>
        /// Vista comunicados.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Comunicados(string slug)
        {
            List<PaginaBE> paginas = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2).Where(x => x.Slug == slug).ToList();
            // busquedaModel.Paginas = paginas;
            ViewBag.Paginas = paginas;

            return View("~/Views/Comunicados/Index.cshtml",
                _comunicadoService.Listar(0, ViewBag.Culture == "es-pe" ? 1 : 2));
        }

        /// <summary>
        /// Vista del formulario de contacto.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <returns></returns>
        public ActionResult Contacto(string categoria, string subcategoria)
        {
           List<PaginaBE> paginas = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2).Where(x => x.Slug == categoria+"/"+subcategoria).ToList();
            ViewBag.Paginas = paginas;

            return View("~/Views/Contacto/Index.cshtml");
        }

        /// <summary>
        /// Guardar datos de contacto.
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Contacto(ContactoBE contacto)
        {
            try
            {
                contacto.FechaRegistro = DateTime.UtcNow.AddHours(-5);

                ContactoDTO ContactoDto = _mapper.Map<ContactoBE, ContactoDTO>(contacto);

                var archivo = string.Empty;
                archivo = System.Web.HttpContext.Current.Server.MapPath("~/Content/templates/mail/mail_contacto.txt");

                string cuerpoMensaje = System.IO.File.ReadAllText(archivo)
                 .Replace("%Nombre%", ContactoDto.Nombre)
                 .Replace("%Apellidos%", ContactoDto.Apellidos)
                 .Replace("%Correo%", ContactoDto.Correo)
                 .Replace("%Consulta%", ContactoDto.Consulta);

                var mailAsunto = ConfigurationManager.AppSettings["MailContactoAsunto"];
                var mailDestinatario = ConfigurationManager.AppSettings["MailContactoDestinatario"];

                int result = 0;

                if (Enviar(mailAsunto, cuerpoMensaje, mailDestinatario))
                {
                    result = _contactoService.Insertar(contacto);
                }

                return Json(result >= 0 ? new { success = true } : new { success = false });
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
                return Json(false);
            }
        }

        /// <summary>
        /// Vista de privacidad (TyC / Aviso Privacidad)
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Privacidad(string slug)
        {
            return View("~/Views/Privacidad/Index.cshtml", _paginaService.Seleccionar(slug));
        }

        /// <summary>
        /// Vista de detalle de campaña.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <param name="campaniaId"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult Campania(string categoria, string subcategoria, int campaniaId, string slug)
        {
            CampaniaBE campania = _campaniaService.Seleccionar(campaniaId, ViewBag.Culture == "es-pe" ? 1 : 2);

            if (campania == null|| subcategoria == "noticias"|| subcategoria == "news"|| subcategoria == "search" || subcategoria == "blogperu")
            {
                throw new HttpException(404, "Publicación no encontrada");
            }

            dynamic model = new ExpandoObject();
            model.Campania = campania;
            model.Galeria = _galeriaService.Listar(ViewBag.Culture == "es-pe" ? 1 : 2)
                .Where(x => x.EntidadId == campania.Id)
                //.Where(x => x.Uso == "portada")
                .ToList();

            List<PaginaBE> paginas = _paginaService.ListarPorIdioma(ViewBag.Culture == "es-pe" ? 1 : 2).Where(x => x.Slug == slug).ToList();
            // busquedaModel.Paginas = paginas;
            ViewBag.Paginas = paginas;

            return View("~/Views/Campania/Index.cshtml", model);
        }

        // Metodo de Envio
        public static bool Enviar(string subject, string body, string email)
        {
            bool success = false;
            try
            {
                var mailName = ConfigurationManager.AppSettings["MailName"];
                var mailDe = ConfigurationManager.AppSettings["MailDe"];
                var credencialUser = ConfigurationManager.AppSettings["CredencialUser"];
                var credencialPass = ConfigurationManager.AppSettings["CredencialPass"];
                var host = ConfigurationManager.AppSettings["Host"];
                var port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                var ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["Ssl"]);
                var domain = ConfigurationManager.AppSettings["domain"];

                //Create the msg object to be sent
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                //Add your email address to the recipients
                msg.To.Add(email);
                //Configure the address we are sending the mail from
                msg.From = new MailAddress(mailDe, mailName);
                msg.Subject = subject;
                msg.Body = body;

                //Configure an SmtpClient to send the mail.            
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = ssl;
                client.Host = host;
                client.Port = port;

                client.UseDefaultCredentials = false;
                //Setup credentials to login to our sender email address ("UserName", "Password")
                NetworkCredential credentials = new NetworkCredential(credencialUser, credencialPass);
                client.Credentials = credentials;

                //Send the msg
                client.Send(msg);
                msg.Dispose();

                //true
                success = true;
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
                success = false;
            }
            return success;
        }

        private List<PublicacionBE> ObtenerNoticiasRecientes(int idiomaId)
        {
            //return _publicacionService.Listar(idiomaId)
            // .Where(n => n.TipoNombre == "Noticias" || n.TipoNombre == "News")
            // .OrderByDescending(n => n.FechaPublicacion)  // Primero ordenar por fecha de publicación
            // .ThenByDescending(n => n.FechaCreacion) // Luego ordenar por la hora, minutos y segundos
            // .Take(1000)
            // .ToList();

            return _publicacionService.Listar(idiomaId)
                      .Where(n => (n.TipoNombre == "Noticias" || n.TipoNombre == "News")
                                  && n.FechaPublicacion >= DateTime.UtcNow.AddDays(-2)  // Solo noticias de los últimos 2 días
                                  )
                      .OrderByDescending(n => n.FechaPublicacion)  // Primero ordenar por fecha de publicación
                      .ThenByDescending(n => n.FechaCreacion) // Luego ordenar por la hora y segundos de creación
                      .Take(1000)
                      .ToList();
            }

        public ContentResult SitemapNews(string lang)
        {
            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"]; // URL desde Web.config
            string language = (lang == "es") ? "es" : "en";

            // Definir idioma en número para consulta a la base de datos
            int lan = (lang == "es") ? 1 : 2;

            // Obtener noticias recientes desde la base de datos
            var noticias = ObtenerNoticiasRecientes(lan)
                .Select(n => new
                {
                    //Url = $"{baseUrl}/{(lan == 1 ? "es-pe" : "en-us")}/{n.CategoriaSlug}/{(lan == 1 ? "blogperu" : "blogperu")}/{n.CategoriaId}/{n.SubcategoriaId}/{n.Slug}",
                    //Title = n.Titulo,
                    //PublicationDate = n.FechaPublicacion.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    //Language = language

                    Url = $"{baseUrl}/{(lan == 1 ? "es-pe" : "en-us")}/{n.CategoriaSlug}/{(lan == 1 ? "blogperu" : "blogperu")}/{n.CategoriaId}/{n.SubcategoriaId}/{n.Slug}",
                    Title = System.Security.SecurityElement.Escape(n.Titulo), // Escapar caracteres especiales en XML
                    PublicationDate = new DateTime(
                            n.FechaPublicacion.Year,
                            n.FechaPublicacion.Month,
                            n.FechaPublicacion.Day,
                            n.FechaCreacion.Hour,
                            n.FechaCreacion.Minute,
                            n.FechaCreacion.Second,
                            DateTimeKind.Utc
                        ).ToString("yyyy-MM-ddTHH:mm:ss") + "+00:00", // se agregar manualmente la zona horaria
                     Language = language

                });

            // Espacios de nombres XML
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace news = "http://www.google.com/schemas/sitemap-news/0.9";

            // Generar documento XML
            var sitemap = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(ns + "urlset",
                    new XAttribute(XNamespace.Xmlns + "news", news.NamespaceName),
                    noticias.Select(n =>
                        new XElement(ns + "url",
                            new XElement(ns + "loc", n.Url),
                            new XElement(news + "news",
                                new XElement(news + "publication",
                                    new XElement(news + "name", "Peru Info"),
                                    new XElement(news + "language", n.Language)
                                ),
                                new XElement(news + "publication_date", n.PublicationDate),
                                new XElement(news + "title", n.Title)
                            )
                        )
                    )
                )
            );

            return Content(sitemap.ToString(), "application/xml", Encoding.UTF8);
        }


        #endregion
    }
}