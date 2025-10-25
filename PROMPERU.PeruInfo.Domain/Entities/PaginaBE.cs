using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class PaginaBE
    {
        public int Id { get; set; }
        public string Posicion { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string LinkExterno { get; set; }
        public string Contenido { get; set; }

        /*SEO*/

        public string TituloSeo { get; set; }
        public string DescripcionSeo { get; set; }
        public string CanonicalSeo { get; set; }

        /*FIN SEO*/

        public bool? Activo { get; set; }
        public int? Orden { get; set; }
        public int? IdPadre { get; set; }
        public bool? Informativa { get; set; }
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
