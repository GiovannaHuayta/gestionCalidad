using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class DeportistaService: IDeportistaService
    {
        #region Private Variables

        private readonly IDeportistasRepository _repository;

        #endregion

        #region Constructor

        public DeportistaService(IDeportistasRepository repository)
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
        public bool Actualizar(DeportistasBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<DeportistasBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public DeportistasBE Seleccionar(int id)
        {
            return _repository.Seleccionar(id);
        }
        #endregion
    }
}
