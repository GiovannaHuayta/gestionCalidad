using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System.Collections.Generic;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class ComunicadoService : IComunicadoService
    {
        #region Private Variables

        private readonly IComunicadoRepository _repository;

        #endregion

        #region Constructor

        public ComunicadoService(IComunicadoRepository repository)
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
        public bool Actualizar(ComunicadoBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(ComunicadoBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(ComunicadoBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="alerta"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public List<ComunicadoBE> Listar(int idiomaId, bool alerta, string titulo)
        {
            return _repository.Listar(idiomaId, alerta, titulo);
        }

        /// <summary>
        /// Listar todos los registros por tipo e idioma.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<ComunicadoBE> Listar(int tipo, int idiomaId)
        {
            return _repository.Listar(tipo, idiomaId);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public ComunicadoBE Seleccionar(int id, int idiomaId)
        {
            return _repository.Seleccionar(id, idiomaId);
        }

        #endregion
    }
}
