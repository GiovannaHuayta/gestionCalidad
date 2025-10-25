namespace PROMPERU.PeruInfo.CMS.Models
{
    public class GaleriaSubcategoria
    {
        public int? ImagenId { get; set; }
        public int? ImagenTraduccionId { get; set; }
        public string ImagenDesktop { get; set; }
        public string ImagenMovil { get; set; }
        public string ImagenTextoAlternativo { get; set; }
        public int? ImagenBloqueCategoriaId { get; set; }
        public int? ImagenBloqueCategoriaTraduccionId { get; set; }
        public string ImagenBloqueCategoria { get; set; }
        public string ImagenBloqueCategoriaTextoAlternativo { get; set; }
    }
}