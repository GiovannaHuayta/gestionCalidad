using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class TipoBE
    {
        public int Id { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
    }
}
