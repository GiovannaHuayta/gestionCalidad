using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class IdiomaService : IIdiomaService
    {
        #region Private Variables

        private readonly IIdiomaRepository _repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public IdiomaService(IIdiomaRepository repository)
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
        public bool Actualizar(IdiomaBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(IdiomaBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(IdiomaBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<IdiomaBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IdiomaBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}