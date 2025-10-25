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
    public class PalabraAlientoService: IPalabraAlientoService
    {
        #region Private Variables

        private readonly IPalabraAlientoRepository _repository;

        #endregion

        #region Constructor

        public PalabraAlientoService(IPalabraAlientoRepository repository)
        {
            _repository = repository;
        }

        #endregion

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PalabrasAlientoBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<PalabrasAlientoBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public PalabrasAlientoBE Seleccionar(int id)
        {
            return _repository.Seleccionar(id);
        }
    }
}
