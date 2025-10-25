using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface INegocioRepository
    {
        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        List<NegocioBE> Listar();

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="paisId"></param>
        /// <param name="idiomaId"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        List<NegocioBE> Listar(int paisId, int? idiomaId, string titulo);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        NegocioBE Seleccionar(int id);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(NegocioBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(NegocioBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(NegocioBE entity);
    }
}