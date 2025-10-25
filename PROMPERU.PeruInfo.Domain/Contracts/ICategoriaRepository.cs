using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface ICategoriaRepository
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<CategoriaBE> Listar(int idiomaId);

        /// <summary>
        /// Listar los registros por idioma.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        List<CategoriaBE> ListarPorIdioma(int idioma, bool activo = true);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        CategoriaBE Seleccionar(int id, int idioma);

        /// <summary>
        /// Seleccionar un registro por el idioma y el slug.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        CategoriaBE SeleccionarPorSlug(int idioma, string slug);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(CategoriaBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(CategoriaBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(CategoriaBE entity);
    }
}
