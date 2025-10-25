using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.DTO
{
    // ReSharper disable once InconsistentNaming
    public class ContactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Consulta { get; set; }
        public string FechaRegistro { get; set; }
        public string Categoria { get; set; }
    }
}
