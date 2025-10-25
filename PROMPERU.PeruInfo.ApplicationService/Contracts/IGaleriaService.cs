using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IGaleriaService
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        List<GaleriaBE> Listar();

        /// <summary>
        /// Lista todos los registros por idioma.
        /// </summary>
        /// <returns></returns>
        List<GaleriaBE> Listar(int idiomaId);

        /// <summary>
        /// Listar los registros por tipo e id.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        List<GaleriaBE> Listar(int idiomaId, string tipo, int id);
        
        /// <summary>
        /// Listar registros por idioma y tipo.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        List<GaleriaBE> Listar(int idiomaId, string tipo);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GaleriaBE Seleccionar(int id);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(GaleriaBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(GaleriaBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(GaleriaBE entity);
    }
}