using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class PaisService : IPaisService
    {
        #region Private Variables

        private readonly IPaisRepository _repository;

        #endregion

        #region Constructor

        public PaisService(IPaisRepository repository)
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
        public bool Actualizar(PaisBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PaisBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PaisBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<PaisBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PaisBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
