using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface IBannerRepository
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        List<BannerBE> Listar();

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BannerBE Seleccionar(int id);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(BannerBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(BannerBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(BannerBE entity);
    }
}
