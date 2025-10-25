using System;
using System.Collections.Generic;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class CategoriaBE
    {
        public CategoriaBE()
        {
            Subcategorias = new List<SubcategoriaBE>();
        }

        public int Id { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string ImagenDesktop { get; set; }
        public string ImagenMovil { get; set; }
        public string ImagenTextoAlternativo { get; set; }
        public int? ImagenId { get; set; }
        public int? ImagenTraduccionId { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set; }

        public List<SubcategoriaBE> Subcategorias { get; set; }
    }
}
