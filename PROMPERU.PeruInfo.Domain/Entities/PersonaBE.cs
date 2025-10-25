using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class PersonaBE
    {
        public int Id { get; set; }
        public int? SubcategoriaId { get; set; }
        public string SubcategoriaNombre { get; set; }
        public string SubcategoriaSlug { get; set; }
        public int? CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaSlug { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Imagen { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public int? Anio { get; set; }
        public string AltImagen { get; set; }
        public bool? Destacado { get; set; }
        public int? Tipo { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public bool? Activo { get; set; }
        public int TraduccionId { get; set; }
        public int? IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Resumen { get; set; }
        public string Detalle { get; set; }
        public string TituloSeo { get; set; }
        public string Keywords { get; set; }
        public string DescripcionSeo { get; set; }
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
