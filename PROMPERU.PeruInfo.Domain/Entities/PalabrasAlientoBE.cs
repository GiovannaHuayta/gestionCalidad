using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    public class PalabrasAlientoBE
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public string MotivoRechazo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int UsuarioEdicion { get; set; }
        public string IpCreacion { get; set; }
        public int EstadoAprobacion { get; set; }
        public bool? Activo { get; set; }

    }
}
