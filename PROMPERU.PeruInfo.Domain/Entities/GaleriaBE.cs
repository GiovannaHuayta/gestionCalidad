using System;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class GaleriaBE
    {
        public int Id { get; set; }
        public int? CampaniaId { get; set; }
        public string CampaniaNombre { get; set; }
        public string EntidadTipo { get; set; }
        public int? EntidadId { get; set; }
        public string RutaDekstop { get; set; }
        public string RutaMovil { get; set; }
        public string Uso { get; set; }
        public string CodigoYoutube { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string TextoAlternativo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? UsuarioEliminacion { get; set; }
        public string IpEliminacion { get; set; }
    }
}
