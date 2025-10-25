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
    public class PersonaService : IPersonaService
    {
        #region Private Variables

        private readonly IPersonaRepository _repository;

        #endregion

        #region Constructor

        public PersonaService(IPersonaRepository repository)
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
        public bool Actualizar(PersonaBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PersonaBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PersonaBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subCategoriaId"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PersonaBE> Listar(int? idiomaId, int? subCategoriaId, string nombre)
        {
            return _repository.Listar(idiomaId, subCategoriaId, nombre);
        }

        /// <summary>
        /// Listar por idioma y subcategoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoria"></param>
        /// <returns></returns>
        public List<PersonaBE> Listar(int idiomaId, string subcategoria)
        {
            return _repository.Listar(idiomaId, subcategoria);
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public PersonaBE Seleccionar(int id, int idiomaId)
        {
            return _repository.Seleccionar(id, idiomaId);
        }

        /// <summary>
        /// Seleccionar por slug.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public PersonaBE Seleccionar(int idiomaId, string subcategoria, string slug)
        {
            return _repository.Seleccionar(idiomaId, subcategoria, slug);
        }

        #endregion
    }
}
