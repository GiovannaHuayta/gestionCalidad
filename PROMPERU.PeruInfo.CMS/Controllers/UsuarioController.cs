using AutoMapper;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.ApplicationService.DTO;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PROMPERU.PeruInfo.CMS.Controllers
{    
    public class UsuarioController : Controller
    {
        #region Private Variables

        private readonly IUsuarioService _usuarioService;

        protected IMapper _mapper;

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
        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Vista del listado de usuarios de sistema.
        /// </summary>
        /// <returns></returns>        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Lista todos los usuarios de sistema.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public JsonResult Listar()
        {
            var data = new
            {
                data = _mapper.Map<List<UsuarioBE>, List<UsuarioDTO>>(_usuarioService.Listar()) 
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Registrar nuevo usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult Insertar(UsuarioBE usuario)
        {
            usuario.UsuarioCreacion = int.Parse(Session["UsuarioId"].ToString());
            usuario.FechaCreacion = DateTime.UtcNow.AddHours(-5);
            usuario.IpCreacion = GetIp();

            int result = _usuarioService.Insertar(usuario);

            return Json(result);
        }

        /// <summary>
        ///     Seleccionar usuario por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult Seleccionar(int id)
        {
            UsuarioBE usuario = _usuarioService.Seleccionar(id);

            return Json(usuario);
        }

        /// <summary>
        ///     Actualizar usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult Actualizar(UsuarioBE usuario)
        {
            usuario.UsuarioEdicion = int.Parse(Session["UsuarioId"].ToString());
            usuario.FechaEdicion = DateTime.UtcNow.AddHours(-5);
            usuario.IpEdicion = GetIp();

            bool result = _usuarioService.Actualizar(usuario);

            return Json(result);
        }

        /// <summary>
        ///     Eliminar usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public JsonResult Eliminar(UsuarioBE usuario)
        {
            usuario.UsuarioEliminacion = int.Parse(Session["UsuarioId"].ToString());
            usuario.FechaEliminacion = DateTime.UtcNow.AddHours(-5);
            usuario.IpEliminacion = GetIp();

            bool result = _usuarioService.Eliminar(usuario);

            return Json(result);
        }

        /// <summary>
        ///     Muestra la vista para el login de usuario.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///     Inicia sesión de usuario al sistema.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string correo, string contrasenia)
        {
            UsuarioBE usuarioAutenticado = _usuarioService.SeleccionarPorCorreo(correo, contrasenia);

            if (usuarioAutenticado == null)
            {
                ModelState.AddModelError("", @"Usuario no existe");

                return View();
            }

            if (usuarioAutenticado.Clave == contrasenia)
            {
                Session["UsuarioId"] = usuarioAutenticado.Id;
                Session["UsuarioNombre"] = usuarioAutenticado.Nombres;
                FormsAuthentication.SetAuthCookie(usuarioAutenticado.Email, true);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", @"Usuario inválido");

            return View();
        }

        /// <summary>
        /// Cierra sesión de usuario.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["UsuarioId"] = null;
            Session["UsuarioNombre"] = null;

            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult VerificarSesion()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            return Json(new { autenticado = isAuthenticated });
        }

        #endregion
    }
}