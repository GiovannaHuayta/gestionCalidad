using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface IComunicadoRepository
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="alerta"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        List<ComunicadoBE> Listar(int idiomaId, bool alerta, string titulo);

        /// <summary>
        /// Listar todos los registros por tipo e idioma.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<ComunicadoBE> Listar(int tipo, int idiomaId);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        ComunicadoBE Seleccionar(int id, int idiomaId);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(ComunicadoBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(ComunicadoBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(ComunicadoBE entity);
    }
}