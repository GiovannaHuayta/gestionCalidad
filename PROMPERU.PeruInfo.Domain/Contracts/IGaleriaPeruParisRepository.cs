using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface IGaleriaPeruParisRepository
    {
        /// <summary>
        /// Inserta un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(GaleriaPeruParisBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(GaleriaPeruParisBE entity);

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        List<GaleriaPeruParisBE> Listar();

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        GaleriaPeruParisBE Seleccionar(int id);

        /// <summary>
        /// Elimina un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(GaleriaPeruParisBE entity);
    }
}
