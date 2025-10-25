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
    public class CampaniaRepository : ICampaniaRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static CampaniaBE Obtener(SqlDataReader reader)
        {
            return new CampaniaBE
            {
                Id = reader.GetNullableInt("Camp_Id"),
                Activo = reader.GetNullableBoolean("Camp_Activo"),
                TraduccionId = reader.GetNullableInt("Camp_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Camp_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Camp_IdiomaNombre"),
                Nombre = reader.GetNullableString("Camp_Nombre"),
                Slug = reader.GetNullableString("Camp_Slug"),
                ImagenId = reader.GetNullableInt("Camp_ImagenId"),
                ImagenTraduccionId = reader.GetNullableInt("Camp_ImagenTraduccionId"),
                ImagenDesktop = reader.GetNullableString("Camp_ImagenDesktop"),
                ImagenMovil = reader.GetNullableString("Camp_ImagenMovil"),
                ImageTextoAlternativo = reader.GetNullableString("Camp_ImagenTextoAlternativo"),
                Anio = reader.GetNullableInt("Camp_Anio"),
                Publico = reader.GetNullableString("Camp_Publico"),
                Ubicacion = reader.GetNullableString("Camp_Ubicacion"),
                Descripcion = reader.GetNullableString("Camp_Descripcion"),
                Link = reader.GetNullableString("Camp_Link"),
                DescripcionSeo = reader.GetNullableString("Camp_DescripcionSEO"),
                TituloSeo = reader.GetNullableString("Camp_TituloSEO"),
                Keywords = reader.GetNullableString("Camp_Keywords"),
                FechaCreacion = reader.GetDateTime("Camp_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Camp_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Camp_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Camp_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Camp_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Camp_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Camp_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Camp_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Camp_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(CampaniaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Camp_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Camp_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter activoParameter = new SqlParameter("@Camp_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Camp_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Camp_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Camp_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter anioParameter = new SqlParameter("@Camp_Anio", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Anio ?? DBNull.Value
                };

                SqlParameter publicoParameter = new SqlParameter("@Camp_Publico", SqlDbType.VarChar,255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Publico ?? DBNull.Value
                };

                SqlParameter ubicacionParameter = new SqlParameter("@Camp_Ubicacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Ubicacion ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Camp_Descripcion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Descripcion
                };

                SqlParameter linkParameter = new SqlParameter("@Camp_Link", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Link ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Camp_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Camp_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Camp_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Camp_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Camp_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Camp_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Campania_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        activoParameter,
                        idiomaIdParameter,
                        nombreParameter,
                        slugParameter,
                        anioParameter,
                        publicoParameter,
                        ubicacionParameter,
                        descripcionParameter,
                        linkParameter,
                        descripcionSeoParameter,
                        tituloSeoParameter,
                        keywordsParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("CampaniaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(CampaniaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Camp_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Camp_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Camp_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Camp_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Camp_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Campania_DEL]",
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
                Log.Error("CampaniaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(CampaniaBE entity)
        {
            try
            {
                int insertedId;

                SqlParameter idParameter = new SqlParameter("@Camp_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter activoParameter = new SqlParameter("@Camp_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Camp_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Camp_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Camp_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter anioParameter = new SqlParameter("@Camp_Anio", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Anio ?? DBNull.Value
                };

                SqlParameter publicoParameter = new SqlParameter("@Camp_Publico", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Publico ?? DBNull.Value
                };

                SqlParameter ubicacionParameter = new SqlParameter("@Camp_Ubicacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Ubicacion ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Camp_Descripcion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Descripcion
                };

                SqlParameter linkParameter = new SqlParameter("@Camp_Link", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Link ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Camp_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Camp_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Camp_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Camp_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Camp_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Camp_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_Campania_INS]",
                        idParameter,
                        activoParameter,
                        idiomaIdParameter,
                        nombreParameter,
                        slugParameter,
                        anioParameter,
                        publicoParameter,
                        ubicacionParameter,
                        descripcionParameter,
                        linkParameter,
                        descripcionSeoParameter,
                        tituloSeoParameter,
                        keywordsParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("CampaniaRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<CampaniaBE> Listar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<CampaniaBE> Listar(int idiomaId)
        {
            try
            {
                List<CampaniaBE> items = new List<CampaniaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Campania_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"CampaniaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CampaniaBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public CampaniaBE Seleccionar(int id, int idiomaId)
        {
            try
            {
                CampaniaBE item = null;

                SqlParameter idParameter = new SqlParameter("@Camp_Id", SqlDbType.Int)
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
                           adoHelper.ExecDataReaderProc("[USP_Campania_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"CampaniaRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
