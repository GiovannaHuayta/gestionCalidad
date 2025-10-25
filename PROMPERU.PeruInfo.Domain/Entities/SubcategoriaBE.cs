using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class SubcategoriaBE
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaSlug { get; set; }
        public string CategoriaColor { get; set; }
        public int? IdPadre { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Subtitulo { get; set; }
        public string TituloBloqueCategoria { get; set; }
        public string DescripcionBloqueCategoria { get; set; }
        public string NombreBotonBloqueCategoria { get; set; }
        public string LinkExterno { get; set; }
        public int? OrdenBloqueCategoria { get; set; }
        public string TituloSeo { get; set; }
        public string DescripcionSeo { get; set; }
        public string Keywords { get; set; }
        public bool? Activo { get; set; }
        public bool? VisibleBuscador { get; set; }
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
