using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    public class ComunicadoDTO
    {
        public int Id { get; set; }
        public string FechaPublicacion { get; set; }
        public bool? Activo { get; set; }
        public bool? Alerta { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Titulo { get; set; }
        public string Slug { get; set; }
        public string Descripcion { get; set; }
        public string ArchivoDescarga { get; set; }
        public string Keywords { get; set; }
        public string FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
        public string FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
