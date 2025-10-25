using System.Collections.Generic;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class BannerService : IBannerService
    {
        #region Private properties

        private readonly IBannerRepository _repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        /// <param name="repository"></param>
        public BannerService(IBannerRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<BannerBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BannerBE Seleccionar(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(BannerBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(BannerBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(BannerBE entity)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}