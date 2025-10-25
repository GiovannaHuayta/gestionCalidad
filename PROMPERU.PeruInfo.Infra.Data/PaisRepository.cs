using log4net;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using PROMPERU.PeruInfo.Infra.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.Infra.Data
{
    public class PaisRepository : IPaisRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private PaisBE Obtener(SqlDataReader reader)
        {
            return new PaisBE
            {
                Id = reader.GetNullableInt("Pais_Id"),
                TraduccionId = reader.GetNullableInt("Pais_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Ptra_IdiomaId"),
                Nombre = reader.GetNullableString("Ptra_Nombre"),
                Activo = reader.GetNullableBoolean("PTra_Activo"),
                FechaCreacion = reader.GetDateTime("Pais_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Pais_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Pais_IpCreacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PaisBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PaisBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PaisBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<PaisBE> Listar(int idiomaId)
        {
            try
            {
                List<PaisBE> items = new List<PaisBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Ptra_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Pais_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PaisRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PaisBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
