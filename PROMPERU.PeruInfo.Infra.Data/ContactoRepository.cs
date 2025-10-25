using log4net;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using PROMPERU.PeruInfo.Infra.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PROMPERU.PeruInfo.Infra.Data
{
    public class ContactoRepository : IContactoRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private ContactoBE Obtener(SqlDataReader reader)
        {
            return new ContactoBE
            {
                Id = reader.GetNullableInt("Cont_Id"),
                Nombre = reader.GetNullableString("Cont_Nombre"),
                Apellidos = reader.GetNullableString("Cont_Apellidos"),
                Correo = reader.GetNullableString("Cont_Correo"),
                Consulta = reader.GetNullableString("Cont_Consulta"),
                FechaRegistro = reader.GetNullableDateTime("Cont_FechaRegistro"),
                Categoria = reader.GetNullableString("Cont_Categoria")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(ContactoBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(ContactoBE entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(ContactoBE entity)
        {
            try
            {
                SqlParameter nombreParameter = new SqlParameter("@Cont_Nombre", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };
                
                SqlParameter apellidoParameter = new SqlParameter("@Cont_Apellidos", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Apellidos
                };
                
                SqlParameter correoParameter = new SqlParameter("@Cont_Correo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Correo
                };
                
                SqlParameter consultaParameter = new SqlParameter("@Cont_Consulta", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Consulta
                };
                
                SqlParameter fechaRegistroParameter = new SqlParameter("@Cont_FechaRegistro", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaRegistro
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Contacto_INS]", 
                        nombreParameter, 
                        apellidoParameter, 
                        correoParameter, 
                        consultaParameter,
                        fechaRegistroParameter);
                }

                return 1;
            }
            catch (Exception ex)
            {
                Log.Error($"ContactoRepository:::{ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<ContactoBE> Listar()
        {
            try
            {
                List<ContactoBE> items = new List<ContactoBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Contacto_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"ContactoRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactoBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
