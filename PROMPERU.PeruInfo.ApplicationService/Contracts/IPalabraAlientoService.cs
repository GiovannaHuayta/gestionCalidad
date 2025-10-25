using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IPalabraAlientoService
    {
        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(PalabrasAlientoBE entity);

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        List<PalabrasAlientoBE> Listar();

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        PalabrasAlientoBE Seleccionar(int id);
    }
}
