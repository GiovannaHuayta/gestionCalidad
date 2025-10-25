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
    public class TipoRepository : ITipoRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private TipoBE Obtener(SqlDataReader reader)
        {
            return new TipoBE
            {
                Id = reader.GetNullableInt("Tipo_Id"),
                TraduccionId = reader.GetNullableInt("Tipo_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Tipo_IdiomaId"),
                Nombre = reader.GetNullableString("Ttra_Nombre"),
                Slug = reader.GetNullableString("Ttra_Slug"),
                FechaCreacion = reader.GetDateTime("Tipo_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Tipo_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Tipo_IpCreacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(TipoBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(TipoBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(TipoBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<TipoBE> Listar(int idiomaId)
        {
            try
            {
                List<TipoBE> items = new List<TipoBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Ttra_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Tipo_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"TipoRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TipoBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
