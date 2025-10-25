using log4net;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using PROMPERU.PeruInfo.Infra.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.Infra.Data
{
    public class IdiomaRepository :IIdiomaRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private IdiomaBE Obtener(SqlDataReader reader)
        {
            return new IdiomaBE
            {
                Id = reader.GetNullableInt("Idio_Id"),
                Nombre = reader.GetNullableString("Idio_Nombre"),
                Prefijo = reader.GetNullableString("Idio_Prefijo"),
                Slug = reader.GetNullableString("Idio_Slug"),
                Activo = reader.GetNullableBoolean("Idio_Activo")
            };
        }

        #endregion

        #region Public Methods

        public bool Actualizar(IdiomaBE entity)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(IdiomaBE entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(IdiomaBE entity)
        {
            throw new NotImplementedException();
        }

        public List<IdiomaBE> Listar()
        {
            try
            {
                List<IdiomaBE> items = new List<IdiomaBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Idioma_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"IdiomaRepository:::{ex.Message}");
                return null;
            }
        }

        public IdiomaBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
