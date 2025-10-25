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
    public class PersonaTipoRepository : IPersonaTipoRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static PersonaTipoBE Obtener(SqlDataReader reader)
        {
            return new PersonaTipoBE
            {
                Id = reader.GetNullableInt("PeTi_Id"),
                Icono = reader.GetNullableString("PeTi_Icono"),
                Activo = reader.GetNullableBoolean("PeTi_Activo"),
                TraduccionId = reader.GetNullableInt("PeTi_TraduccionId"),
                IdiomaId = reader.GetNullableInt("PeTi_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("PeTi_IdiomaNombre"),
                Nombre = reader.GetNullableString("PeTi_Nombre")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Listar por idioma.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<PersonaTipoBE> Listar(int idiomaId)
        {
            try
            {
                List<PersonaTipoBE> items = new List<PersonaTipoBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_PersonaTipo_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PersonaTipoRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
