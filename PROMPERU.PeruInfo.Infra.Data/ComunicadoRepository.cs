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
    public class ComunicadoRepository : IComunicadoRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private ComunicadoBE Obtener(SqlDataReader reader)
        {
            return new ComunicadoBE
            {
                Id = reader.GetNullableInt("Comu_Id"),
                FechaPublicacion = reader.GetNullableDateTime("Comu_FechaPublicacion"),
                Activo = reader.GetNullableBoolean("Comu_Activo"),
                Alerta = reader.GetNullableBoolean("Comu_Alerta"),
                TraduccionId = reader.GetNullableInt("Ctra_Id"),
                IdiomaId = reader.GetNullableInt("Comu_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Comu_IdiomaNombre"),
                Titulo = reader.GetNullableString("Ctra_Titulo"),
                Slug = reader.GetNullableString("Comu_Slug"),
                Descripcion = reader.GetNullableString("Ctra_Descripcion"),
                ArchivoDescarga = reader.GetNullableString("Ctra_ArchivoDescarga"),
                Keywords = reader.GetNullableString("Comu_Keywords"),
                FechaCreacion = reader.GetDateTime("Comu_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Comu_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Comu_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Comu_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Comu_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Comu_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Comu_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Comu_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Comu_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(ComunicadoBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Comu_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Comu_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaPublicacionParameter = new SqlParameter("@Comu_FechaPublicacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaPublicacion ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Comu_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter alertaParameter = new SqlParameter("@Comu_Alerta", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Alerta
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Comu_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Comu_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter slugParameter = new SqlParameter("@Comu_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Slug ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Comu_Descripcion", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Descripcion ?? DBNull.Value
                };

                SqlParameter archivoDescargaParameter = new SqlParameter("@Comu_ArchivoDescarga", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.ArchivoDescarga ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Comu_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Comu_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Comu_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Comu_IpEdicion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Comunicado_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        fechaPublicacionParameter,
                        activoParameter,
                        alertaParameter,
                        idiomaIdParameter,
                        tituloParameter,
                        slugParameter,
                        descripcionParameter,
                        archivoDescargaParameter,
                        keywordsParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("ComunicadoRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(ComunicadoBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Comu_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Comu_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Comu_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Comu_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Comu_IpEliminacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Comunicado_DEL]",
                        idParameter,
                        traduccionIdParameter,
                        fechaEliminacionParameter,
                        usuarioEliminacionParameter,
                        ipEliminacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("ComunicadoRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(ComunicadoBE entity)
        {
            try
            {
                int insertedId;

                SqlParameter idParameter = new SqlParameter("@Comu_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter fechaPublicacionParameter = new SqlParameter("@Comu_FechaPublicacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaPublicacion ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Comu_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter alertaParameter = new SqlParameter("@Comu_Alerta", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Alerta
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Comu_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Comu_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter slugParameter = new SqlParameter("@Comu_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Slug ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Comu_Descripcion", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Descripcion ?? DBNull.Value
                };

                SqlParameter archivoDescargaParameter = new SqlParameter("@Comu_ArchivoDescarga", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.ArchivoDescarga ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Comu_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Comu_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Comu_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Comu_IpCreacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_Comunicado_INS]",
                                    idParameter,
                                    fechaPublicacionParameter,
                                    activoParameter,
                                    alertaParameter,
                                    idiomaIdParameter,
                                    tituloParameter,
                                    slugParameter,
                                    descripcionParameter,
                                    archivoDescargaParameter,
                                    keywordsParameter,
                                    fechaCreacionParameter,
                                    usuarioCreacionParameter,
                                    ipCreacionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("ComunicadoRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="alerta"></param>
        /// <param name="titulo"></param>
        public List<ComunicadoBE> Listar(int idiomaId, bool alerta, string titulo)
        {
            try
            {
                List<ComunicadoBE> items = new List<ComunicadoBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Ctra_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter alertaParameter = new SqlParameter("@Comu_Alerta", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = alerta
                };

                SqlParameter tituloParameter = new SqlParameter("@Ctra_Titulo", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Input,
                    Value = titulo
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Comunicado_LIS]", idiomaIdParameter, alertaParameter, tituloParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"ComunicadoRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar todos los registros por tipo e idioma.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<ComunicadoBE> Listar(int tipo, int idiomaId)
        {
            try
            {
                List<ComunicadoBE> items = new List<ComunicadoBE>();

                SqlParameter tipoParameter = new SqlParameter("@Comu_Tipo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = tipo
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Comu_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Comunicado_LIS_PorTipo]", tipoParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"ComunicadoRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public ComunicadoBE Seleccionar(int id, int idiomaId)
        {
            try
            {
                ComunicadoBE item = null;

                SqlParameter idParameter = new SqlParameter("@Comu_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Comunicado_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"ComunicadoRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
