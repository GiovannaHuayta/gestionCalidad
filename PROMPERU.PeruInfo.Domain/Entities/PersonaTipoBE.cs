using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.Domain.Entities
{
    public class PersonaTipoBE
    {
        public int Id { get; set; }
        public string Icono { get; set; }
        public bool? Activo { get; set; }
        public int TraduccionId { get; set; }
        public int IdiomaId { get; set; }
        public string IdiomaNombre { get; set; }
        public string Nombre { get; set; }
    }
}
