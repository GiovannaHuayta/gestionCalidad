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
    public class GaleriaPeruParisService : IGaleriaPeruParisService    {
        
            #region Private Variables

            private readonly IGaleriaPeruParisRepository _repository;

            #endregion

            #region Constructor

            public GaleriaPeruParisService(IGaleriaPeruParisRepository repository)
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
            public bool Actualizar(GaleriaPeruParisBE entity)
            {
                return _repository.Actualizar(entity);
            }

            /// <summary>
            /// Listar todos los registros.
            /// </summary>
            /// <returns></returns>
            public List<GaleriaPeruParisBE> Listar()
            {
                return _repository.Listar();
            }

            /// <summary>
            /// Selecciona un registro.
            /// </summary>
            /// <returns></returns>
            public GaleriaPeruParisBE Seleccionar(int id)
            {
                return _repository.Seleccionar(id);
            }

            /// <summary>
            /// Selecciona un registro.
            /// </summary>
            /// <returns></returns>
            public int Insertar(GaleriaPeruParisBE entity)
            {
                return _repository.Insertar(entity);
            }

            /// <summary>
            /// Actualiza un registro existente.
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public bool Eliminar(GaleriaPeruParisBE entity)
            {
                return _repository.Eliminar(entity);
            }
              

            #endregion
        
    }
}
