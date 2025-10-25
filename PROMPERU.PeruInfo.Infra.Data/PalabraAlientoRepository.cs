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
    public class PalabraAlientoRepository : IPalabraAlientoRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static PalabrasAlientoBE Obtener(SqlDataReader reader)
        {
            return new PalabrasAlientoBE
            {
                Id = reader.GetNullableInt("PAli_Id"),
                Mensaje = reader.GetNullableString("PAli_Mensaje"),
                MotivoRechazo = reader.GetNullableString("PAli_MotivoRechazo"),
                FechaCreacion = reader.GetDateTime("PAli_FechaCreacion"),
                FechaEdicion = reader.GetDateTime("PAli_FechaEdicion"),
                UsuarioEdicion = reader.GetNullableInt("PAli_UsuarioEdicion"),
                IpCreacion = reader.GetNullableString("PAli_IpCreacion"),
                Activo = reader.GetNullableBoolean("PAli_Activo"),
                EstadoAprobacion = reader.GetNullableInt("PAli_EstadoAprobacion")
            };
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PalabrasAlientoBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@PAli_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter motivoRechazoParameter = new SqlParameter("@PAli_MotivoRechazo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.MotivoRechazo
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@PAli_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEdicion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@PAli_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEdicion
                };

                SqlParameter activoParameter = new SqlParameter("@PAli_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter estadoAprobacionParameter = new SqlParameter("@PAli_EstadoAprobacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.EstadoAprobacion
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    adoHelper.ExecNonQueryProc("[USP_PalabrasAliento_UPD]",
                        idParameter,
                        motivoRechazoParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        activoParameter,
                        estadoAprobacionParameter);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("PalabrasAlientoRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<PalabrasAlientoBE> Listar()
        {
            try
            {
                List<PalabrasAlientoBE> items = new List<PalabrasAlientoBE>();

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_PalabrasAliento_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("PalabrasAlientoRepository:::" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public PalabrasAlientoBE Seleccionar(int id)
        {
            try
            {
                PalabrasAlientoBE items = null;

                SqlParameter idParameter = new SqlParameter("@PAli_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_PalabrasAliento_SEL]", idParameter))
                    {
                        while (reader.Read()) items = Obtener(reader);
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("PalabrasAlientoRepository:::" + ex.Message);

                return null;
            }
        }

        #endregion
    }
}
