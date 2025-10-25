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
    public class UsuarioRepository : IUsuarioRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private UsuarioBE Obtener(SqlDataReader reader)
        {
            return new UsuarioBE
            {
                Id = reader.GetNullableInt("Ucms_Id"),
                Email = reader.GetNullableString("Ucms_Email"),
                Nombres = reader.GetNullableString("Ucms_Nombres"),
                Apellidos = reader.GetNullableString("Ucms_Apellidos"),
                Perfil = reader.GetNullableInt("Ucms_Perfil"),
                Clave = reader.GetNullableString("Ucms_Clave"),
                Activo = reader.GetNullableBoolean("Ucms_Activo"),
                FechaCreacion = reader.GetDateTime("Ucms_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Ucms_UsuarioCMSCreacion"),
                IpCreacion = reader.GetNullableString("Ucms_IpCreacion"),
                FechaEdicion = reader.GetDateTime("Ucms_FechaEdicion"),
                UsuarioEdicion = reader.GetNullableInt("Ucms_UsuarioCMSEdicion"),
                IpEdicion = reader.GetNullableString("Ucms_IpEdicion"),
                FechaEliminacion = reader.GetDateTime("Ucms_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Ucms_UsuarioCMSEliminacion"),
                IpEliminacion = reader.GetNullableString("Ucms_IpEliminacion")
            };
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
            try
            {
                SqlParameter idParameter = new SqlParameter("@Usua_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter eMailParameter = new SqlParameter("@Usua_Email", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Email
                };

                SqlParameter nombresParameter = new SqlParameter("@Usua_Nombres", SqlDbType.VarChar, 120)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombres
                };

                SqlParameter apellidosParameter = new SqlParameter("@Usua_Apellidos", SqlDbType.VarChar, 120)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Apellidos
                };

                SqlParameter perfilParameter = new SqlParameter("@Usua_Perfil", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Perfil
                };

                SqlParameter activoParameter = new SqlParameter("@Usua_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter claveParameter = new SqlParameter("@Usua_Clave", SqlDbType.VarChar, 80)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Clave
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Usua_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaCreacion ?? DBNull.Value
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@Usua_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEdicion
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Usua_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaEliminacion ?? DBNull.Value
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Usua_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioCreacion ?? DBNull.Value
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@Usua_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEdicion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Usua_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioEliminacion ?? DBNull.Value
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Usua_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpCreacion ?? DBNull.Value
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@Usua_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEdicion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Usua_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpEliminacion ?? DBNull.Value
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_UsuarioCMS_UPD]", idParameter, eMailParameter, nombresParameter, apellidosParameter, perfilParameter,
                        activoParameter, claveParameter, fechaCreacionParameter, fechaEdicionParameter, fechaEliminacionParameter, usuarioCreacionParameter,
                        usuarioEdicionParameter, usuarioEliminacionParameter, ipCreacionParameter, ipEdicionParameter, ipEliminacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("UsuarioRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(UsuarioBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Usua_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter eMailParameter = new SqlParameter("@Usua_Email", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Email
                };

                SqlParameter nombresParameter = new SqlParameter("@Usua_Nombres", SqlDbType.VarChar, 120)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombres
                };

                SqlParameter apellidosParameter = new SqlParameter("@Usua_Apellidos", SqlDbType.VarChar, 120)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Apellidos
                };

                SqlParameter perfilParameter = new SqlParameter("@Usua_Perfil", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Perfil
                };

                SqlParameter activoParameter = new SqlParameter("@Usua_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter claveParameter = new SqlParameter("@Usua_Clave", SqlDbType.VarChar, 80)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Clave
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Usua_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaCreacion ?? DBNull.Value
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@Usua_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaEdicion ?? DBNull.Value
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Usua_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Usua_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioCreacion ?? DBNull.Value
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@Usua_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioEdicion ?? DBNull.Value
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Usua_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion 
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Usua_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpCreacion ?? DBNull.Value
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@Usua_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpEdicion ?? DBNull.Value
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Usua_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_UsuarioCMS_DEL]", idParameter, eMailParameter, nombresParameter, apellidosParameter, perfilParameter,
                        activoParameter, claveParameter, fechaCreacionParameter, fechaEdicionParameter, fechaEliminacionParameter, usuarioCreacionParameter,
                        usuarioEdicionParameter, usuarioEliminacionParameter, ipCreacionParameter, ipEdicionParameter, ipEliminacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("UsuarioRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(UsuarioBE entity)
        {
            try
            {
                int insertedId;

                SqlParameter idParameter = new SqlParameter("@Usua_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter eMailParameter = new SqlParameter("@Usua_Email", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Email
                };

                SqlParameter nombresParameter = new SqlParameter("@Usua_Nombres", SqlDbType.VarChar, 120)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombres
                };

                SqlParameter apellidosParameter = new SqlParameter("@Usua_Apellidos", SqlDbType.VarChar, 120)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Apellidos
                };

                SqlParameter perfilParameter = new SqlParameter("@Usua_Perfil", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Perfil
                };

                SqlParameter activoParameter = new SqlParameter("@Usua_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter claveParameter = new SqlParameter("@Usua_Clave", SqlDbType.VarChar, 80)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Clave
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Usua_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@Usua_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaEdicion ?? DBNull.Value
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Usua_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaEliminacion ?? DBNull.Value
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Usua_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@Usua_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioEdicion ?? DBNull.Value
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Usua_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioEliminacion ?? DBNull.Value
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Usua_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@Usua_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpEdicion ?? DBNull.Value
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Usua_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpEliminacion ?? DBNull.Value
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_UsuarioCMS_INS]", idParameter, eMailParameter, nombresParameter, apellidosParameter, perfilParameter,
                        activoParameter, claveParameter, fechaCreacionParameter, fechaEdicionParameter, fechaEliminacionParameter, usuarioCreacionParameter, 
                        usuarioEdicionParameter, usuarioEliminacionParameter, ipCreacionParameter, ipEdicionParameter, ipEliminacionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("UsuarioRepository:::" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<UsuarioBE> Listar()
        {
            try
            {
                List<UsuarioBE> items = new List<UsuarioBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_UsuarioCMS_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"UsuarioRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioBE Seleccionar(int id)
        {
            try
            {
                UsuarioBE item = null;

                SqlParameter idParameter = new SqlParameter("@Usua_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_UsuarioCMS_SEL]", idParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"UsuarioRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     Selecciona un usuario por correo.
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public UsuarioBE SeleccionarPorCorreo(string correo, string clave)
        {
            try
            {
                UsuarioBE item = null;

                SqlParameter correoParametro = new SqlParameter("@Ucms_Email", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = correo
                };

                SqlParameter claveParametro = new SqlParameter("@Ucms_Clave", SqlDbType.VarChar, 80)
                {
                    Direction = ParameterDirection.Input,
                    Value = clave
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_UsuarioCMS_SEL_PorEmail]", correoParametro, claveParametro))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"UsuarioRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
