using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    public class DeportistasBE
    {
        public int Id { get; set; }
        public string NombreDeportista { get; set; }
        public string ImagenDesktop { get; set; }
        public string ImagenMobile { get; set; }
        public string Disciplina { get; set; }
        public string LinkNota { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpModificacion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
