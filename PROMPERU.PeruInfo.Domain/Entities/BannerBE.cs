using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class BannerBE
    {
        public int Id { get; set; }
        public int? CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public int? SubcategoriaId { get; set; }
        public string SubcategoriaNombre { get; set; }
        public int? PaginaId { get; set; }
        public int TraduccionId { get; set; }
        public int? ImagenId { get; set; }
        public int? ImagenTraduccionId { get; set; }
        public string ImagenDesktop { get; set; }
        public string ImagenMovil { get; set; }
        public string ImagenTextoAlternativo { get; set; }
        public string Titulo { get; set; }
        public string Bajada { get; set; }
        public int? Orden { get; set; }
        public string Link { get; set; }
        public bool? VentanaNueva { get; set; }
        public int? IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpModificacion { get; set; }
        public string IpEliminacion { get; set; }

    }
}
