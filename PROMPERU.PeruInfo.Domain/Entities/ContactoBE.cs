using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class ContactoBE
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Consulta { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Categoria { get; set; }

    }
}
