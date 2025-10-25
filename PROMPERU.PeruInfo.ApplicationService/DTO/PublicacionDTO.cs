namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    // ReSharper disable once InconsistentNaming
    public class PublicacionDTO
    {
        public int Id { get; set; }
        public int SubcategoriaId { get; set; }
        public string SubcategoriaNombre { get; set; }
        public string SubcategoriaSlug { get; set; }
        public int? CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaSlug { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public string TipoSlug{ get; set; }
        public string Imagen { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }
        public string Imagen5 { get; set; }
        public string ImagenFuente { get; set; }
        public string IdDnn { get; set; }
        public bool? Destacado { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Titulo { get; set; }
        public string Slug { get; set; }
        public string Resumen { get; set; }
        public string Detalle { get; set; }
        public string AltImagen { get; set; }
        public string TituloSeo { get; set; }
        public string DescripcionSeo { get; set; }
        public string Keywords { get; set; }
        public bool? Activo { get; set; }
        public int? Contador { get; set; }
        public int? DescubreCategoria { get; set; }
        public string DescubreNombre { get; set; }
        public string DescubreLink { get; set; }
        public string DescubreImagen { get; set; }
        public int? CategoriaNotas { get; set; }
        public string FechaPublicacion { get; set; }
        public string FechaExpiracion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public string FechaEliminacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public int UsuarioEliminacion { get; set; }
        public string IpCreacion { get; set; }
        public string IpModificacion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
