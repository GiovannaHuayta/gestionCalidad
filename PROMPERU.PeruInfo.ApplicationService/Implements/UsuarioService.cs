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
    public class UsuarioService : IUsuarioService
    {
        #region Private Variables

        private readonly IUsuarioRepository _repository;

        #endregion

        #region Constructor

        public UsuarioService(IUsuarioRepository repository)
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
        public bool Actualizar(UsuarioBE entity)
        {
            return _repository.Actualizar(entity);
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(UsuarioBE entity)
        {
            return _repository.Eliminar(entity);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(UsuarioBE entity)
        {
            return _repository.Insertar(entity);
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<UsuarioBE> Listar()
        {
            return _repository.Listar();
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioBE Seleccionar(int id)
        {
            return _repository.Seleccionar(id);
        }

        /// <summary>
        ///     Selecciona un usuario por correo.
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public UsuarioBE SeleccionarPorCorreo(string correo, string clave)
        {
            return _repository.SeleccionarPorCorreo(correo, clave);
        }

        #endregion
    }
}
