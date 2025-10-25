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
    public class NegocioService : INegocioService
    {
        #region Private Variables

        private readonly INegocioRepository _repository;

        #endregion

        #region Constructor

        public NegocioService(INegocioRepository repository)
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
        public bool Actualizar(NegocioBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(NegocioBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(NegocioBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<NegocioBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="paisId"></param>
        /// <param name="idiomaId"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public List<NegocioBE> Listar(int paisId, int? idiomaId, string titulo)
        {
            return _repository.Listar(paisId, idiomaId, titulo);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NegocioBE Seleccionar(int id)
        {
            return _repository.Seleccionar(id);
        }

        #endregion
    }
}
