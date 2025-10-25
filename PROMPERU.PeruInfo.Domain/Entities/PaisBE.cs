using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class PaisBE
    {
        public int Id { get; set; }
        public int TraduccionId { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public int? IdiomaId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
    }
}
