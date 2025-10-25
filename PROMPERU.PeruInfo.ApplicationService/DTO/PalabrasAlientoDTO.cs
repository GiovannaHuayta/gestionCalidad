using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    public class PalabrasAlientoDTO
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public string MotivoRechazo { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEdicion { get; set; }
        public int UsuarioEdicion { get; set; }
        public string IpCreacion { get; set; }
        public int EstadoAprobacion { get; set; }
        public bool? Activo { get; set; }
    }
}
