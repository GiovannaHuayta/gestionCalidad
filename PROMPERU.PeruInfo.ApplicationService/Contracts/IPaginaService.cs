using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IPaginaService
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        List<PaginaBE> Listar();

        /// <summary>
        /// Lista todos los registros por idioma.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        List<PaginaBE> ListarPorIdioma(int idioma, bool activo = true);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PaginaBE Seleccionar(int id);

        /// <summary>
        /// Selecciona un registro por slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        PaginaBE Seleccionar(string slug);

        /// <summary>
        /// Seleccionar un registro por idioma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        PaginaBE SeleccionarPorIdioma(int id, int idioma);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(PaginaBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(PaginaBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(PaginaBE entity);
    }
}