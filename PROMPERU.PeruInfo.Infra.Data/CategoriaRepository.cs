using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using log4net;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using PROMPERU.PeruInfo.Infra.Data.Utils;

namespace PROMPERU.PeruInfo.Infra.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        #endregion

        #region Private Methods

        private CategoriaBE Obtener(SqlDataReader reader)
        {
            return new CategoriaBE
            {
                Id = reader.GetNullableInt("Cate_Id"),
                TraduccionId = reader.GetNullableInt("Cate_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Cate_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Cate_IdiomaNombre"),
                ImagenDesktop = reader.GetNullableString("Cate_ImagenDesktop"),
                ImagenMovil = reader.GetNullableString("Cate_ImagenMovil"),
                ImagenTextoAlternativo = reader.GetNullableString("Cate_ImagenTextoAlternativo"),
                ImagenId = reader.GetNullableInt("Cate_ImagenId"),
                ImagenTraduccionId = reader.GetNullableInt("Cate_ImagenTraduccionId"),
                Nombre = reader.GetNullableString("Cate_Nombre"),
                Slug = reader.GetNullableString("Cate_Slug"),
                Titulo = reader.GetNullableString("Cate_Titulo"),
                Subtitulo = reader.GetNullableString("Cate_Subtitulo"),
                Descripcion = reader.GetNullableString("Cate_Descripcion"),
                BloqueHtml = reader.GetNullableString("Cate_BloqueHTML"),
                TituloSeo = reader.GetNullableString("Cate_TituloSEO"),
                DescripcionSeo = reader.GetNullableString("Cate_DescripcionSEO"),
                Keywords = reader.GetNullableString("Cate_Keywords"),
                Activo = reader.GetNullableBoolean("Cate_Activo"),
                VisibleBuscador = reader.GetNullableBoolean("Cate_VisibleBuscador"),
                VisibleHome = reader.GetNullableBoolean("Cate_VisibleHome"),
                Color = reader.GetNullableString("Cate_Color"),
                FechaCreacion = reader.GetDateTime("Cate_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Cate_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Cate_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Cate_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Cate_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Cate_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Cate_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Cate_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Cate_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(CategoriaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Cate_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Cate_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter colorParameter = new SqlParameter("@Cate_Color", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Color ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Cate_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Cate_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Cate_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter tituloParameter = new SqlParameter("@Cate_Titulo", SqlDbType.VarChar, 250)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Titulo ?? DBNull.Value
                };

                SqlParameter subtituloParameter = new SqlParameter("@Cate_Subtitulo", SqlDbType.VarChar, 250)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Subtitulo ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Cate_Descripcion", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Descripcion ?? DBNull.Value
                };

                SqlParameter bloqueHtmlParameter = new SqlParameter("@Cate_BloqueHTML", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.BloqueHtml ?? DBNull.Value
                };               

                SqlParameter tituloSeoParameter = new SqlParameter("@Cate_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Cate_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Cate_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Cate_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter visibleBuscadorParameter = new SqlParameter("@Cate_VisibleBuscador", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VisibleBuscador ?? DBNull.Value
                };

                SqlParameter visibleHomeParameter = new SqlParameter("@Cate_VisibleHome", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VisibleHome ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Cate_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Cate_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Cate_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Categoria_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        colorParameter,
                        idiomaIdParameter,
                        nombreParameter,
                        slugParameter,
                        tituloParameter,
                        subtituloParameter,
                        descripcionParameter,
                        bloqueHtmlParameter,                        
                        tituloSeoParameter,
                        descripcionSeoParameter,
                        keywordsParameter,
                        activoParameter,
                        visibleBuscadorParameter,
                        visibleHomeParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("CategoriaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(CategoriaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Cate_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Cate_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Cate_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Cate_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Cate_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Categoria_DEL]",
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
                Log.Error("CategoriaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Seleccionar un registro por el idioma y el slug.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public CategoriaBE SeleccionarPorSlug(int idioma, string slug)
        {
            try
            {
                CategoriaBE item = new CategoriaBE();

                SqlParameter idiomaSqlParameter = new SqlParameter("@Cate_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idioma
                };

                SqlParameter slugSqlParameter = new SqlParameter("@Cate_slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = slug
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("USP_Categoria_SEL_PorIdiomaSlug",
                               idiomaSqlParameter, slugSqlParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"CategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(CategoriaBE entity)
        {
            try
            {
                object insertedId;

                SqlParameter colorParameter = new SqlParameter("@Cate_Color", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Color ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Cate_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Cate_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Cate_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter tituloParameter = new SqlParameter("@Cate_Titulo", SqlDbType.VarChar, 250)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Titulo ?? DBNull.Value
                };

                SqlParameter subtituloParameter = new SqlParameter("@Cate_Subtitulo", SqlDbType.VarChar, 250)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Subtitulo ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Cate_Descripcion", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Descripcion ?? DBNull.Value
                };

                SqlParameter bloqueHtmlParameter = new SqlParameter("@Cate_BloqueHTML", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.BloqueHtml ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Cate_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Cate_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Cate_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Cate_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter visibleBuscadorParameter = new SqlParameter("@Cate_VisibleBuscador", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VisibleBuscador ?? DBNull.Value
                };

                SqlParameter visibleHomeParameter = new SqlParameter("@Cate_VisibleHome", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VisibleHome ?? DBNull.Value
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Cate_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Cate_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Cate_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecScalarProc("[USP_Categoria_INS]",
                        colorParameter,
                        idiomaIdParameter,
                        nombreParameter,
                        slugParameter,
                        tituloParameter,
                        subtituloParameter,
                        descripcionParameter,
                        bloqueHtmlParameter,
                        tituloSeoParameter,
                        descripcionSeoParameter,
                        keywordsParameter,
                        activoParameter,
                        visibleBuscadorParameter,
                        visibleHomeParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return int.Parse(insertedId.ToString());
            }
            catch (Exception ex)
            {
                Log.Error("CategoriaRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<CategoriaBE> Listar(int idiomaId)
        {
            try
            {
                List<CategoriaBE> items = new List<CategoriaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Categoria_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"CategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar los registros por idioma.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public List<CategoriaBE> ListarPorIdioma(int idioma, bool activo = true)
        {
            try
            {
                List<CategoriaBE> items = new List<CategoriaBE>();

                SqlParameter idiomaSqlParameter = new SqlParameter("@Cate_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idioma
                };

                SqlParameter activoSqlParameter = new SqlParameter("@Cate_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = activo
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("USP_Categoria_LIS_PorIdioma",
                               idiomaSqlParameter, activoSqlParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"CategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        public CategoriaBE Seleccionar(int id, int idioma)
        {
            try
            {
                CategoriaBE item = null;

                SqlParameter idParameter = new SqlParameter("@Cate_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idioma
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Categoria_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"CategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoriaBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}