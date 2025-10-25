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
    public class GaleriaPeruParisRepository : IGaleriaPeruParisRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private GaleriaPeruParisBE Obtener(SqlDataReader reader)
        {
            return new GaleriaPeruParisBE
            {
                Id = reader.GetNullableInt("GPPa_Id"),
                Titulo = reader.GetNullableString("GPPa_Titulo"),
                Descripcion = reader.GetNullableString("GPPa_Descripcion"),
                TipoArchivo = reader.GetNullableString("GPPa_TipoArchivo"),
                NombreArchivo = reader.GetNullableString("GPPa_NombreArchivo"),
                Activo = reader.GetNullableBoolean("GPPa_Activo"),
                FechaCreacion = reader.GetDateTime("GPPa_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("GPPa_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("GPPa_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("GPPa_FechaEdicion"),
                UsuarioModificacion = reader.GetNullableInt("GPPa_UsuarioEdicion"),
                IpModificacion = reader.GetNullableString("GPPa_IpEdicion"),
                FechaEliminacion = reader.GetNullableDateTime("GPPa_FechaELiminacion"),
                UsuarioEliminacion = reader.GetNullableInt("GPPa_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("GPPa_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(GaleriaPeruParisBE entity)
        {
            int insertedId;
            try
            {
                SqlParameter idParameter = new SqlParameter("@GPPa_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter tituloParameter = new SqlParameter("@GPPa_Titulo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter descripcionParameter = new SqlParameter("@GPPa_Descripcion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Descripcion
                };

                SqlParameter tipoArchivoParameter = new SqlParameter("@GPPa_TipoArchivo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TipoArchivo
                };

                SqlParameter nombreArchivoParameter = new SqlParameter("@GPPa_NombreArchivo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.NombreArchivo
                };

                SqlParameter activoParameter = new SqlParameter("@GPPa_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@GPPa_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@GPPa_UsuarioCreacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@GPPa_IpCreacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_GaleriaPeruParis_INS]",
                        idParameter,
                        tituloParameter,
                        descripcionParameter,
                        tipoArchivoParameter,
                        nombreArchivoParameter,
                        activoParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        ipEdicionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("GaleriaPeruParisRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(GaleriaPeruParisBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@GPPa_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter tituloParameter = new SqlParameter("@GPPa_Titulo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter descripcionParameter = new SqlParameter("@GPPa_Descripcion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Descripcion
                };

                SqlParameter tipoArchivoParameter = new SqlParameter("@GPPa_tipoArchivo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TipoArchivo
                };

                SqlParameter nombreArchivoParameter = new SqlParameter("@GPPa_NombreArchivo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.NombreArchivo
                };

                SqlParameter activoParameter = new SqlParameter("@GPPa_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@GPPa_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@GPPa_UsuarioEdicion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@GPPa_IpEdicion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    adoHelper.ExecNonQueryProc("[USP_GaleriaPeruParis_UPD]",
                        idParameter,
                        tituloParameter,
                        descripcionParameter,
                        tipoArchivoParameter,
                        nombreArchivoParameter,
                        activoParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        ipEdicionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("GaleriaPeruParisRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<GaleriaPeruParisBE> Listar()
        {
            try
            {
                List<GaleriaPeruParisBE> items = new List<GaleriaPeruParisBE>();

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_GaleriaPeruParis_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("GaleriaPeruParisRepository:::" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public GaleriaPeruParisBE Seleccionar(int id)
        {
            try
            {
                GaleriaPeruParisBE items = null;

                SqlParameter idParameter = new SqlParameter("@GPPa_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_GaleriaPeruParis_SEL]", idParameter))
                    {
                        while (reader.Read()) items = Obtener(reader);
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("GaleriaPeruParisRepository:::" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Elimina un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(GaleriaPeruParisBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@GPPa_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@GPPa_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@GPPa_UsuarioEliminacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@GPPa_IpEliminacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    adoHelper.ExecNonQueryProc("[USP_GaleriaPeruParis_DEL]",
                        idParameter,
                        fechaEliminacionParameter,
                        usuarioEliminacionParameter,
                        ipEliminacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("GaleriaPeruParisRepository:::" + ex.Message);

                return false;
            }
        }
        #endregion
    }
}
