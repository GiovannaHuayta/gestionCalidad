using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System.Collections.Generic;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class SubcategoriaService : ISubcategoriaService
    {
        #region Private Variables

        private readonly ISubcategoriaRepository _repository;

        #endregion

        #region Constructor

        public SubcategoriaService(ISubcategoriaRepository repository)
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
        public bool Actualizar(SubcategoriaBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(SubcategoriaBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Seleccionar subcategoría por slug.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="slug"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public SubcategoriaBE Seleccionar(string categoria, string slug, int idiomaId)
        {
            return _repository.Seleccionar(categoria, slug, idiomaId);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(SubcategoriaBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<SubcategoriaBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        /// <summary>
        /// Lista los registros por categoria id.
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<SubcategoriaBE> Listar(int categoriaId, int idiomaId)
        {
            return _repository.Listar(categoriaId, idiomaId);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public SubcategoriaBE Seleccionar(int id, int idiomaId)
        {
            return _repository.Seleccionar(id, idiomaId);
        }

        #endregion
    }
}