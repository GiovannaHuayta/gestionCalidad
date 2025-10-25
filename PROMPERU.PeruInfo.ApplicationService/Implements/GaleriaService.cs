using System.Collections.Generic;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class GaleriaService : IGaleriaService
    {
        #region Private Variables

        private readonly IGaleriaRepository _repository;   

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public GaleriaService(IGaleriaRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<GaleriaBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Lista todos los registros por idioma.
        /// </summary>
        /// <returns></returns>
        public List<GaleriaBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        /// <summary>
        /// Listar los registros por tipo e id.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GaleriaBE> Listar(int idiomaId, string tipo, int id)
        {
            return _repository.Listar(idiomaId, tipo, id);
        }

        /// <summary>
        /// Listar registros por idioma y tipo.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<GaleriaBE> Listar(int idiomaId, string tipo)
        {
            return _repository.Listar(idiomaId, tipo);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GaleriaBE Seleccionar(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(GaleriaBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(GaleriaBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(GaleriaBE entity)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}