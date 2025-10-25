using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface ITarjetaRepository
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<TarjetaBE> Listar(int idiomaId);

        /// <summary>
        /// Listado por idioma y subcategoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <returns></returns>
        List<TarjetaBE> Listar(int idiomaId, int subcategoriaId);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        TarjetaBE Seleccionar(int id, int idiomaId);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(TarjetaBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(TarjetaBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(TarjetaBE entity);
    }
}