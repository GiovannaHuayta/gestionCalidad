using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IPersonaTipoService
    {
        /// <summary>
        /// Listar por idioma.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<PersonaTipoBE> Listar(int idiomaId);
    }
}
