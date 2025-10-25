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
    public class TarjetaService : ITarjetaService
    {
        #region Private Variables

        private readonly ITarjetaRepository _repository;

        #endregion

        #region Constructor

        public TarjetaService(ITarjetaRepository repository)
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
        public bool Actualizar(TarjetaBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(TarjetaBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(TarjetaBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<TarjetaBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        /// <summary>
        /// Listado por idioma y subcategoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <returns></returns>
        public List<TarjetaBE> Listar(int idiomaId, int subcategoriaId)
        {
            return _repository.Listar(idiomaId, subcategoriaId);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public TarjetaBE Seleccionar(int id, int idiomaId)
        {
            return _repository.Seleccionar(id, idiomaId);
        }

        #endregion
    }
}
