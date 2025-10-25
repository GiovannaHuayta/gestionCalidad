using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class ComunicadoBE
    {
        public int Id { get; set; }
        public DateTime? FechaPublicacion { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
