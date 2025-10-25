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
    public class NosPreparamosService : INosPreparamosService
    {
        #region Private Variables

        private readonly INosPreparamosRepository _repository;

        #endregion

        #region Constructor

        public NosPreparamosService(INosPreparamosRepository repository)
        {
            _repository = repository;
        }

        #endregion

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(NosPreparamosBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<NosPreparamosBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public NosPreparamosBE Seleccionar(int id)
        {
            return _repository.Seleccionar(id);
        }
    }
}
