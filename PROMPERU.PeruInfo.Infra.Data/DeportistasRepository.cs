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
    public class DeportistasRepository : IDeportistasRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private DeportistasBE Obtener(SqlDataReader reader)
        {
            return new DeportistasBE
            {
                Id = reader.GetNullableInt("Depo_Id"),
                NombreDeportista = reader.GetNullableString("Depo_NombreDeportista"),
                ImagenDesktop = reader.GetNullableString("Depo_ImagenDesktop"),
                ImagenMobile = reader.GetNullableString("Depo_ImagenMovil"),
                Disciplina = reader.GetNullableString("Depo_Disciplina"),
                LinkNota = reader.GetNullableString("Depo_LinkNota"),
                Activo = reader.GetNullableBoolean("Depo_Activo"),
                FechaCreacion = reader.GetDateTime("Depo_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Depo_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Depo_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Depo_FechaEdicion"),
                UsuarioModificacion = reader.GetNullableInt("Depo_UsuarioEdicion"),
                IpModificacion = reader.GetNullableString("Depo_IpEdicion"),
                FechaEliminacion = reader.GetNullableDateTime("Depo_FechaELiminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Depo_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Depo_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(DeportistasBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Depo_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter nombreDeportistaParameter = new SqlParameter("@Depo_NombreDeportista", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.NombreDeportista
                };

                SqlParameter imagenDesktopParameter = new SqlParameter("@Depo_ImagenDesktop", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.ImagenDesktop
                };

                SqlParameter imagenMovilParameter = new SqlParameter("@Depo_ImagenMovil", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.ImagenMobile
                };

                SqlParameter disciplinaParameter = new SqlParameter("@Depo_Disciplina", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Disciplina
                };

                SqlParameter linkNotaParameter = new SqlParameter("@Depo_LinkNota", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.LinkNota
                };

                SqlParameter activoParameter = new SqlParameter("@Depo_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@Depo_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@Depo_UsuarioEdicion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@Depo_IpEdicion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    adoHelper.ExecNonQueryProc("[USP_Deportistas_UPD]",
                        idParameter,
                        nombreDeportistaParameter,
                        imagenDesktopParameter,
                        imagenMovilParameter,
                        disciplinaParameter,
                        linkNotaParameter,
                        activoParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        ipEdicionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("DeportistaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<DeportistasBE> Listar()
        {
            try
            {
                List<DeportistasBE> items = new List<DeportistasBE>();

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Deportistas_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("DeportistaRepository:::" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public DeportistasBE Seleccionar(int id)
        {
            try
            {
                DeportistasBE items = null;

                SqlParameter idParameter = new SqlParameter("@Depo_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Deportistas_SEL]",idParameter))
                    {
                        while (reader.Read()) items = Obtener(reader);
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("DeportistaRepository:::" + ex.Message);

                return null;
            }
        }
        #endregion
    }
}
