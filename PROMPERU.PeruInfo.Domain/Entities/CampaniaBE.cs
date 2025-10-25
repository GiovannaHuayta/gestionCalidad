using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class CampaniaBE
    {
        public int Id { get; set; }
        public bool? Activo { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string ImagenDesktop { get; set; }
        public string ImagenMovil { get; set; }
        public string ImageTextoAlternativo { get; set; }
        public int? ImagenId { get; set; }
        public int? ImagenTraduccionId { get; set; }
        public int Anio { get; set; }
        public string Publico { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public string Link { get; set; }
        public string DescripcionSeo { get; set; }
        public string TituloSeo { get; set; }
        public string Keywords { get; set; }
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