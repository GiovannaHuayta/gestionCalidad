using System;
using System.Collections.Generic;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class CategoriaService : ICategoriaService
    {
        #region Private Variables

        private readonly ICategoriaRepository _repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<CategoriaBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<CategoriaBE> Listar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listar los registros por idioma.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public List<CategoriaBE> ListarPorIdioma(int idioma, bool activo = true)
        {
            return _repository.ListarPorIdioma(idioma, activo);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoriaBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        public CategoriaBE Seleccionar(int id, int idioma)
        {
            return _repository.Seleccionar(id, idioma);
        }

        /// <summary>
        /// Seleccionar un registro por el idioma y el slug.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public CategoriaBE SeleccionarPorSlug(int idioma, string slug)
        {
            return _repository.SeleccionarPorSlug(idioma, slug);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(CategoriaBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(CategoriaBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(CategoriaBE entity)
        {
            return _repository.Eliminar(entity);
        }

        #endregion
    }
}
