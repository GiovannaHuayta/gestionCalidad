using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class UsuarioBE
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Perfil { get; set; }
        public string Clave { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow.AddHours(-5);
        public DateTime? FechaEdicion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int? UsuarioEdicion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpEdicion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
