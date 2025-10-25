using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Contracts
{
    public interface IPublicacionService
    {
        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        List<PublicacionBE> Listar(int idiomaId);
        
        /// <summary>
        /// Listar publicación por idioma y por tipo.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <returns></returns>
        List<PublicacionBE> Listar(int idiomaId, int tipoId);
        
        /// <summary>
        /// Listar publicaciones por idioma, tipo y categoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        List<PublicacionBE> Listar(int idiomaId, int tipoId, int categoriaId);

        /// <summary>
        /// Listar publicacion por filtros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="buscado"></param>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <param name="inicio"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        List<PublicacionBE> Listar(string idiomaId, string tipoId = null, string buscado = null,
            string categoria = null, string subcategoria = null, string inicio = null, string fin = null);

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        PublicacionBE Seleccionar(int id, int idiomaId);

        /// <summary>
        /// Seleccionar publicacion por slug y categoría.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="categoriaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        PublicacionBE Seleccionar(string slug, int categoriaId, int subcategoriaId, int idiomaId);

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insertar(PublicacionBE entity);

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Actualizar(PublicacionBE entity);

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Eliminar(PublicacionBE entity);
    }
}
