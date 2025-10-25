using System.Collections.Generic;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.Domain.Contracts
{
    public interface ISubcategoriaRepository
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<SubcategoriaBE> Listar(int idiomaId);

        /// <summary>
        /// Lista los registros por categoria id.
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<SubcategoriaBE> Listar(int categoriaId, int idiomaId);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        SubcategoriaBE Seleccionar(int id, int idiomaId);

        /// <summary>
        /// Seleccionar subcategoría por slug.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="slug"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        SubcategoriaBE Seleccionar(string categoria, string slug, int idiomaId);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(SubcategoriaBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(SubcategoriaBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(SubcategoriaBE entity);
    }
}