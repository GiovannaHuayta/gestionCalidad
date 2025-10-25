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
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static SubcategoriaBE Obtener(SqlDataReader reader)
        {
            return new SubcategoriaBE
            {
                Id = reader.GetNullableInt("Subc_Id"),
                CategoriaId = reader.GetNullableInt("Subc_CategoriaId"),
                CategoriaNombre = reader.GetNullableString("Subc_CategoriaNombre"),
                CategoriaSlug = reader.GetNullableString("Subc_CategoriaSlug"),
                CategoriaColor = reader.GetNullableString("Subc_CategoriaColor"),
                IdPadre = reader.GetNullableInt("Subc_IdPadre"),
                TraduccionId = reader.GetNullableInt("Subc_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Subc_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Subc_IdiomaNombre"),
                Nombre = reader.GetNullableString("Subc_Nombre"),
                Slug = reader.GetNullableString("Subc_Slug"),
                Subtitulo = reader.GetNullableString("Subc_Subtitulo"),
                TituloBloqueCategoria = reader.GetNullableString("Subc_TituloBloqueCategoria"),
                DescripcionBloqueCategoria = reader.GetNullableString("Subc_DescripcionBloqueCategoria"),
                NombreBotonBloqueCategoria = reader.GetNullableString("Subc_NombreBotonBloqueCategoria"),
                LinkExterno = reader.GetNullableString("Subc_LinkExterno"),
                OrdenBloqueCategoria = reader.GetNullableInt("Subc_OrdenBloqueCategoria"),
                TituloSeo = reader.GetNullableString("Subc_TituloSEO"),
                DescripcionSeo = reader.GetNullableString("Subc_DescripcionSEO"),
                Keywords = reader.GetNullableString("Subc_Keywords"),
                Activo = reader.GetNullableBoolean("Subc_Activo"),
                VisibleBuscador = reader.GetNullableBoolean("Subc_VisibleBuscador"),
                FechaCreacion = reader.GetDateTime("Subc_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Subc_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Subc_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Subc_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Subc_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Subc_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Subc_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Subc_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Subc_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(SubcategoriaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Subc_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Subc_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter categoriaIdParameter = new SqlParameter("@Subc_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CategoriaId
                };

                SqlParameter idPadreParameter = new SqlParameter("@Subc_IdPadre", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IdPadre ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Subc_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Subc_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Subc_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter subTituloIdParameter = new SqlParameter("@Subc_Subtitulo", SqlDbType.VarChar, 400)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Subtitulo ?? DBNull.Value
                };

                SqlParameter tituloBloqueCategoriaParameter =
                    new SqlParameter("@Subc_TituloBloqueCategoria", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.TituloBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter descripcionBloqueCategoriaParameter =
                    new SqlParameter("@Subc_DescripcionBloqueCategoria", SqlDbType.VarChar, 2000)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.DescripcionBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter nombreBotonBloqueCategoriaParameter =
                    new SqlParameter("@Subc_NombreBotonBloqueCategoria", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.NombreBotonBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter linkExternoParameter = new SqlParameter("@Subc_LinkExterno", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.LinkExterno ?? DBNull.Value
                };

                SqlParameter ordenBloqueCategoriaParameter =
                    new SqlParameter("@Subc_OrdenBloqueCategoria", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.OrdenBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter tituloSeoParameter = new SqlParameter("@Subc_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Subc_DescripcionSEO", SqlDbType.VarChar, 400)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Subc_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Subc_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter visibleBuscadorParameter = new SqlParameter("@Subc_VisibleBuscador", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VisibleBuscador ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Subc_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaModificacion ?? DBNull.Value
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Subc_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioModificacion ?? DBNull.Value
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Subc_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpModificacion ?? DBNull.Value
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Subcategoria_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        categoriaIdParameter,
                        idPadreParameter,
                        idiomaIdParameter,
                        nombreParameter,
                        slugParameter,
                        subTituloIdParameter,
                        tituloBloqueCategoriaParameter,
                        descripcionBloqueCategoriaParameter,
                        nombreBotonBloqueCategoriaParameter,
                        linkExternoParameter,
                        ordenBloqueCategoriaParameter,
                        tituloSeoParameter,
                        descripcionSeoParameter,
                        keywordsParameter,
                        activoParameter,
                        visibleBuscadorParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("SubcategoriaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(SubcategoriaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Subc_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Subc_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Subc_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Subc_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Subc_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Subcategoria_DEL]",
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
                Log.Error("SubcategoriaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Seleccionar subcategoría por slug.
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="slug"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public SubcategoriaBE Seleccionar(string categoria, string slug, int idiomaId)
        {
            try
            {
                SubcategoriaBE item = null;

                SqlParameter categoriaSqlParameter = new SqlParameter("@Subc_CategoriaSlug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = categoria
                };

                SqlParameter slugSqlParameter = new SqlParameter("@Subc_Slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = slug
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Subc_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Subcategoria_SEL_PorSlug]", 
                               categoriaSqlParameter,
                               slugSqlParameter, 
                               idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"SubcategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(SubcategoriaBE entity)
        {
            try
            {
                object insertedId;

                SqlParameter categoriaIdParameter = new SqlParameter("@Subc_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CategoriaId
                };

                SqlParameter idPadreParameter = new SqlParameter("@Subc_IdPadre", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IdPadre ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Subc_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Subc_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Subc_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter subTituloIdParameter = new SqlParameter("@Subc_Subtitulo", SqlDbType.VarChar, 400)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Subtitulo ?? DBNull.Value
                };

                SqlParameter tituloBloqueCategoriaParameter =
                    new SqlParameter("@Subc_TituloBloqueCategoria", SqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.TituloBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter descripcionBloqueCategoriaParameter =
                    new SqlParameter("@Subc_DescripcionBloqueCategoria", SqlDbType.VarChar, 2000)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.DescripcionBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter nombreBotonBloqueCategoriaParameter =
                    new SqlParameter("@Subc_NombreBotonBloqueCategoria", SqlDbType.VarChar, 50)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.NombreBotonBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter linkExternoParameter = new SqlParameter("@Subc_LinkExterno", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.LinkExterno ?? DBNull.Value
                };

                SqlParameter ordenBloqueCategoriaParameter =
                    new SqlParameter("@Subc_OrdenBloqueCategoria", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = (object)entity.OrdenBloqueCategoria ?? DBNull.Value
                    };

                SqlParameter tituloSeoParameter = new SqlParameter("@Subc_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Subc_DescripcionSEO", SqlDbType.VarChar, 400)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Subc_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Subc_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter visibleBuscadorParameter = new SqlParameter("@Subc_VisibleBuscador", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VisibleBuscador ?? DBNull.Value
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Subc_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Subc_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Subc_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecScalarProc("[USP_Subcategoria_INS]",
                        categoriaIdParameter,
                        idPadreParameter,
                        idiomaIdParameter,
                        nombreParameter,
                        slugParameter,
                        subTituloIdParameter,
                        tituloBloqueCategoriaParameter,
                        descripcionBloqueCategoriaParameter,
                        nombreBotonBloqueCategoriaParameter,
                        linkExternoParameter,
                        ordenBloqueCategoriaParameter,
                        tituloSeoParameter,
                        descripcionSeoParameter,
                        keywordsParameter,
                        activoParameter,
                        visibleBuscadorParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return int.Parse(insertedId.ToString());
            }
            catch (Exception ex)
            {
                Log.Error("SubcategoriaRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<SubcategoriaBE> Listar(int idiomaId)
        {
            try
            {
                List<SubcategoriaBE> items = new List<SubcategoriaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Subcategoria_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"SubcategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lista los registros por categoria id.
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<SubcategoriaBE> Listar(int categoriaId, int idiomaId)
        {
            try
            {
                List<SubcategoriaBE> items = new List<SubcategoriaBE>();

                SqlParameter idiomaIdSqlParameter = new SqlParameter("@Subc_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter categoriaIdSqlParameter = new SqlParameter("@Subc_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = categoriaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Subcategoria_LIS_PorCategoriaPorIdioma]",
                               categoriaIdSqlParameter, idiomaIdSqlParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"SubcategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public SubcategoriaBE Seleccionar(int id, int idiomaId)
        {
            try
            {
                SubcategoriaBE item = null;

                SqlParameter idParameter = new SqlParameter("@Subc_Id", SqlDbType.Int)
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
                           adoHelper.ExecDataReaderProc("[USP_Subcategoria_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"SubcategoriaRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}