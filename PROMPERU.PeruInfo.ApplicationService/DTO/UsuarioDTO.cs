using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Perfil { get; set; }
        public string Clave { get; set; }
        public bool? Activo { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEdicion { get; set; }
        public string FechaEliminacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int? UsuarioEdicion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpEdicion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
