using System.Collections.Generic;

namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    // ReSharper disable once InconsistentNaming
    public class CategoriaDTO
    {
        public CategoriaDTO()
        {
            Subcategorias = new List<SubcategoriaDTO>();
        }
        
        public int Id { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Descripcion { get; set; }
        public string BloqueHtml { get; set; }
        public string TituloSeo { get; set; }
        public string DescripcionSeo { get; set; }
        public string Keywords { get; set; }
        public bool? Activo { get; set; }
        public bool? VisibleBuscador { get; set; }
        public string Color { get; set; }
        public bool? VisibleHome { get; set; }
        public string FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
        public string FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set; }

        public List<SubcategoriaDTO> Subcategorias { get; set; }
    }
}