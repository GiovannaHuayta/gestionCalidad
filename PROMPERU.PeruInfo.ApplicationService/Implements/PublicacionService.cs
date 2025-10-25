using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System.Collections.Generic;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class PublicacionService : IPublicacionService
    {
        #region Private Variables

        private readonly IPublicacionRepository _repository;

        #endregion

        #region Constructor

        public PublicacionService(IPublicacionRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PublicacionBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PublicacionBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PublicacionBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        /// <summary>
        /// Listar publicaciones por idioma, tipo y categoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(int idiomaId, int tipoId, int categoriaId)
        {
            return _repository.Listar(idiomaId, tipoId, categoriaId);
        }

        /// <summary>
        /// Listar publicación por idioma y por tipo.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(int idiomaId, int tipoId)
        {
            return _repository.Listar(idiomaId, tipoId);
        }

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
        public List<PublicacionBE> Listar(string idiomaId, string tipoId = null, string buscado = null, string categoria = null,
            string subcategoria = null, string inicio = null, string fin = null)
        {
            return _repository.Listar(idiomaId, tipoId, buscado, categoria, subcategoria, inicio, fin);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public PublicacionBE Seleccionar(int id, int idiomaId)
        {
            return _repository.Seleccionar(id, idiomaId);
        }

        /// <summary>
        /// Seleccionar publicacion por slug y categoría.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="categoriaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public PublicacionBE Seleccionar(string slug, int categoriaId,  int subcategoriaId, int idiomaId)
        {
            return _repository.Seleccionar(slug, categoriaId, subcategoriaId, idiomaId);
        }

        #endregion
    }
}
