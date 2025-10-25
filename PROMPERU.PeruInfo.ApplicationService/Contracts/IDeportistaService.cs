using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IDeportistaService
    {
        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(DeportistasBE entity);

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        List<DeportistasBE> Listar();

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        DeportistasBE Seleccionar(int id);
    }
}
