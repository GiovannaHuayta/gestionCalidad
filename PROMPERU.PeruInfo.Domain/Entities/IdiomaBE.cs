namespace PROMPERU.PeruInfo.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class IdiomaBE
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Prefijo { get; set; }
        public string Slug { get; set; }
        public bool? Activo { get; set; }
    }
}
