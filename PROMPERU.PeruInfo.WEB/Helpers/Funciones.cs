using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROMPERU.PeruInfo.WEB.Helpers
{
    public class Funciones
    {
        public static Boolean ValidarUsuario()
        {
            return HttpContext.Current.Session["UsuarioId"] != null;
        }
    }
}