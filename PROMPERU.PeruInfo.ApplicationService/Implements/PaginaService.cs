using System.Collections.Generic;
using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class PaginaService : IPaginaService
    {
        #region Private Variables

        private readonly IPaginaRepository _repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:System.Object" /> class.</summary>
        public PaginaService(IPaginaRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<PaginaBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Lista todos los registros por idioma.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public List<PaginaBE> ListarPorIdioma(int idioma, bool activo = true)
        {
            return _repository.ListarPorIdioma(idioma, activo);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PaginaBE Seleccionar(int id)
        {
            return _repository.Seleccionar(id);
        }

        /// <summary>
        /// Selecciona un registro por slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public PaginaBE Seleccionar(string slug)
        {
            return _repository.Seleccionar(slug);
        }

        /// <summary>
        /// Seleccionar un registro por idioma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        public PaginaBE SeleccionarPorIdioma(int id, int idioma)
        {
            return _repository.SeleccionarPorIdioma(id, idioma);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PaginaBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PaginaBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PaginaBE entity)
        {
            return _repository.Eliminar(entity);
        }

        #endregion
    }
}