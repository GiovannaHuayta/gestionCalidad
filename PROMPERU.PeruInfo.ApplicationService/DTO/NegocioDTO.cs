using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    public class NegocioDTO
    {
        public int Id { get; set; }
        public int? IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public int? PaisId { get; set; }
        public string PaisNombre { get; set; }
        public string Titulo { get; set; }
        public string Slug { get; set; }
        public string Resumen { get; set; }
        public string Detalle { get; set; }
        public string Imagen { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
        public string Imagen4 { get; set; }
        public string Imagen5 { get; set; }
        public string AltImagen { get; set; }
        public string FechaPublicacion { get; set; }
        public string FechaExpiracion { get; set; }
        public bool? Destacado { get; set; }
        public string TituloSeo { get; set; }
        public string Keywords { get; set; }
        public string DescripcionSeo { get; set; }
        public bool? Activo { get; set; }
        public string DireccionWeb { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string FechaCreacion { get; set; }
        public int? UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
        public string FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
