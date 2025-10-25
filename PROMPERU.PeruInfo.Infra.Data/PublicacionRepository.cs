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
    public class PublicacionRepository : IPublicacionRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static PublicacionBE Obtener(SqlDataReader reader)
        {
            return new PublicacionBE
            {
                Id = reader.GetNullableInt("Publ_Id"),
                SubcategoriaId = reader.GetNullableInt("Publ_SubcategoriaId"),
                SubcategoriaNombre = reader.GetNullableString("Publ_Subcategoria"),
                SubcategoriaSlug = reader.GetNullableString("Publ_SubcategoriaSlug"),
                CategoriaId = reader.GetNullableInt("Publ_CategoriaId"),
                CategoriaNombre = reader.GetNullableString("Publ_CategoriaNombre"),
                CategoriaSlug = reader.GetNullableString("Publ_CategoriaSlug"),
                CategoriaColor = reader.GetNullableString("Publ_CategoriaColor"),
                TipoId = reader.GetNullableInt("Publ_TipoId"),
                TipoNombre = reader.GetNullableString("Publ_TipoNombre"),
                TipoSlug = reader.GetNullableString("Publ_TipoSlug"),
                Imagen = reader.GetNullableString("Publ_Imagen"),
                Imagen2 = reader.GetNullableString("Publ_Imagen2"),
                Imagen3 = reader.GetNullableString("Publ_Imagen3"),
                Imagen4 = reader.GetNullableString("Publ_Imagen4"),
                Imagen5 = reader.GetNullableString("Publ_Imagen5"),
                ImagenFuente = reader.GetNullableString("Publ_ImagenFuente"),
                IdDnn = reader.GetNullableString("Publ_IdDNN"),
                Destacado = reader.GetNullableBoolean("Publ_Destacado"),
                FechaPublicacion = reader.GetDateTime("Publ_FechaPublicacion"),
                FechaExpiracion = reader.GetDateTime("Publ_FechaExpiracion"),
                TraduccionId = reader.GetNullableInt("Publ_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Publ_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Publ_IdiomaNombre"),
                Titulo = reader.GetNullableString("Publ_Titulo"),
                Slug = reader.GetNullableString("Publ_Slug"),
                Resumen = reader.GetNullableString("Publ_Resumen"),
                Detalle = reader.GetNullableString("Publ_Detalle"),
                AltImagen = reader.GetNullableString("Publ_AltImagen"),
                TituloSeo = reader.GetNullableString("Publ_TituloSEO"),
                DescripcionSeo = reader.GetNullableString("Publ_DescripcionSEO"),
                Keywords = reader.GetNullableString("Publ_Keywords"),
                Activo = reader.GetNullableBoolean("Publ_Activo"),
                Contador = reader.GetNullableInt("Publ_Contador"),
                DescubreCategoria = reader.GetNullableInt("Publ_DescubreCategoria"),
                DescubreNombre = reader.GetNullableString("Publ_DescubreNombre"),
                DescubreLink = reader.GetNullableString("Publ_DescubreLink"),
                DescubreImagen = reader.GetNullableString("Publ_DescubreImagen"),
                CategoriaNotas = reader.GetNullableInt("Publ_CategoriaNotas"),
                FechaCreacion = reader.GetDateTime("Publ_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Publ_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Publ_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Publ_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Publ_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Publ_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Publ_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Publ_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Publ_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PublicacionBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Publ_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Publ_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter subcategoriaIdParameter = new SqlParameter("@Publ_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubcategoriaId
                };

                SqlParameter tipoIdParameter = new SqlParameter("@Publ_TipoId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TipoId
                };

                SqlParameter imagenParameter = new SqlParameter("@Publ_Imagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen ?? DBNull.Value
                };

                SqlParameter imagen2Parameter = new SqlParameter("@Publ_Imagen2", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen2 ?? DBNull.Value
                };

                SqlParameter imagen3Parameter = new SqlParameter("@Publ_Imagen3", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen3 ?? DBNull.Value
                };

                SqlParameter imagen4Parameter = new SqlParameter("@Publ_Imagen4", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen4 ?? DBNull.Value
                };

                SqlParameter imagen5Parameter = new SqlParameter("@Publ_Imagen5", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen5 ?? DBNull.Value
                };

                SqlParameter imagenFuenteParameter = new SqlParameter("@Publ_ImagenFuente", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.ImagenFuente ?? DBNull.Value
                };

                SqlParameter destacadoParameter = new SqlParameter("@Publ_Destacado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Destacado
                };

                SqlParameter fechaPublicacionParameter = new SqlParameter("@Publ_FechaPublicacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaPublicacion
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Publ_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Publ_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter slugParameter = new SqlParameter("@Publ_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter resumenParameter = new SqlParameter("@Publ_Resumen", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Resumen ?? DBNull.Value
                };

                SqlParameter detalleParameter = new SqlParameter("@Publ_Detalle", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Detalle ?? DBNull.Value
                };

                SqlParameter altImagenParameter = new SqlParameter("@Publ_AltImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.AltImagen ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Publ_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Publ_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Publ_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Publ_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter descubreNombreParameter = new SqlParameter("@Publ_DescubreNombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescubreNombre ?? DBNull.Value
                };

                SqlParameter descubreLinkParameter = new SqlParameter("@Publ_DescubreLink", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescubreLink ?? DBNull.Value
                };

                SqlParameter descubreImagenParameter = new SqlParameter("@Publ_DescubreImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescubreImagen ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Publ_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Publ_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Publ_IpEdicion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Publicacion_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        subcategoriaIdParameter,
                        tipoIdParameter,
                        imagenParameter,
                        imagen2Parameter,
                        imagen3Parameter,
                        imagen4Parameter,
                        imagen5Parameter,
                        imagenFuenteParameter,
                        destacadoParameter,
                        fechaPublicacionParameter,
                        idiomaIdParameter,
                        tituloParameter,
                        slugParameter,
                        resumenParameter,
                        detalleParameter,
                        altImagenParameter,
                        tituloSeoParameter,
                        descripcionSeoParameter,
                        keywordsParameter,
                        activoParameter,
                        descubreNombreParameter,
                        descubreLinkParameter,
                        descubreImagenParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("PublicacionRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PublicacionBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Publ_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Publ_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Publ_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Publ_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Publ_IpEliminacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Publicacion_DEL]",
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
                Log.Error("PublicacionRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PublicacionBE entity)
        {
            try
            {
                int insertedId;

                SqlParameter idParameter = new SqlParameter("@Publ_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter subcategoriaIdParameter = new SqlParameter("@Publ_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubcategoriaId
                };

                SqlParameter tipoIdParameter = new SqlParameter("@Publ_TipoId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TipoId
                };

                SqlParameter imagenParameter = new SqlParameter("@Publ_Imagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen ?? DBNull.Value
                };

                SqlParameter imagen2Parameter = new SqlParameter("@Publ_Imagen2", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen2 ?? DBNull.Value
                };

                SqlParameter imagen3Parameter = new SqlParameter("@Publ_Imagen3", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen3 ?? DBNull.Value
                };

                SqlParameter imagen4Parameter = new SqlParameter("@Publ_Imagen4", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen4 ?? DBNull.Value
                };

                SqlParameter imagen5Parameter = new SqlParameter("@Publ_Imagen5", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen5 ?? DBNull.Value
                };

                SqlParameter imagenFuenteParameter = new SqlParameter("@Publ_ImagenFuente", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.ImagenFuente ?? DBNull.Value
                };

                SqlParameter destacadoParameter = new SqlParameter("@Publ_Destacado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Destacado
                };

                SqlParameter fechaPublicacionParameter = new SqlParameter("@Publ_FechaPublicacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaPublicacion
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Publ_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Publ_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter slugParameter = new SqlParameter("@Publ_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter resumenParameter = new SqlParameter("@Publ_Resumen", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Resumen ?? DBNull.Value
                };

                SqlParameter detalleParameter = new SqlParameter("@Publ_Detalle", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Detalle ?? DBNull.Value
                };

                SqlParameter altImagenParameter = new SqlParameter("@Publ_AltImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.AltImagen ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Publ_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Publ_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Publ_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Publ_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter descubreNombreParameter = new SqlParameter("@Publ_DescubreNombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescubreNombre ?? DBNull.Value
                };

                SqlParameter descubreLinkParameter = new SqlParameter("@Publ_DescubreLink", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescubreLink ?? DBNull.Value
                };

                SqlParameter descubreImagenParameter = new SqlParameter("@Publ_DescubreImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescubreImagen ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Publ_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Publ_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Publ_IpCreacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_Publicacion_INS]",
                        idParameter,
                        subcategoriaIdParameter,
                        tipoIdParameter,
                        imagenParameter,
                        imagen2Parameter,
                        imagen3Parameter,
                        imagen4Parameter,
                        imagen5Parameter,
                        imagenFuenteParameter,
                        destacadoParameter,
                        fechaPublicacionParameter,
                        idiomaIdParameter,
                        tituloParameter,
                        slugParameter,
                        resumenParameter,
                        detalleParameter,
                        altImagenParameter,
                        tituloSeoParameter,
                        descripcionSeoParameter,
                        keywordsParameter,
                        activoParameter,
                        descubreNombreParameter,
                        descubreLinkParameter,
                        descubreImagenParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("PublicacionRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(int idiomaId)
        {
            try
            {
                List<PublicacionBE> items = new List<PublicacionBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Publicacion_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar publicación por idioma y por tipo.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(int idiomaId, int tipoId)
        {
            try
            {
                List<PublicacionBE> items = new List<PublicacionBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Publ_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter tipoIdSqlParameter = new SqlParameter("@Publ_TipoId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = tipoId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Publicacion_LIS_PorIdioma]",
                               idiomaIdParameter, tipoIdSqlParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar publicaciones por idioma, tipo y categoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(int idiomaId, int tipoId, int categoriaId)
        {
            try
            {
                List<PublicacionBE> items = new List<PublicacionBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Publ_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter tipoIdSqlParameter = new SqlParameter("@Publ_TipoId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = tipoId
                };

                SqlParameter categoriaIdSqlParameter = new SqlParameter("@Publ_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = categoriaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc(
                               "[USP_Publicacion_LIS_PorIdiomaPorCategoria]", idiomaIdParameter, tipoIdSqlParameter,
                               categoriaIdSqlParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar publicacion por filtros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipoId"></param>
        /// <param name="buscado"></param>
        /// <param name="categoria"></param>
        /// <param name="subcategoria"></param>
        /// <param name="inicio"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public List<PublicacionBE> Listar(string idiomaId, string tipoId = null, string buscado = null, string categoria = null,
            string subcategoria = null, string inicio = null, string fin = null)
        {
            try
            {
                List<PublicacionBE> items = new List<PublicacionBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Publ_IdiomaId", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter tipoIdSqlParameter = new SqlParameter("@Publ_TipoId", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = tipoId
                };

                SqlParameter buscadoSqlParameter = new SqlParameter("@Publ_Buscado", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = buscado
                };

                SqlParameter categoriaSqlParameter = new SqlParameter("@Publ_CategoriaSlug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = categoria
                };

                SqlParameter subcategoriaSqlParameter = new SqlParameter("@Publ_SubcategoriaSlug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = subcategoria
                };

                SqlParameter inicioSqlParameter = new SqlParameter("@Publ_FechaInicio", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = inicio
                };

                SqlParameter finSqlParameter = new SqlParameter("@Publ_FechaFin", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = fin
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc(
                               "[USP_Publicacion_LIS_PorBusqueda]",
                               idiomaIdParameter, 
                               tipoIdSqlParameter,
                               buscadoSqlParameter,
                               categoriaSqlParameter,
                               subcategoriaSqlParameter,
                               inicioSqlParameter,
                               finSqlParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public PublicacionBE Seleccionar(int id, int idiomaId)
        {
            try
            {
                PublicacionBE item = null;

                SqlParameter idParameter = new SqlParameter("@Publ_Id", SqlDbType.Int)
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
                           adoHelper.ExecDataReaderProc("[USP_Publicacion_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Seleccionar publicacion por slug y categoría.
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="categoriaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public PublicacionBE Seleccionar(string slug, int categoriaId, int subcategoriaId, int idiomaId)
        {
            try
            {
                PublicacionBE item = null;

                SqlParameter slugSqlParameter = new SqlParameter("@Publ_Slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = slug
                };

                SqlParameter categoriaIdSqlParameter = new SqlParameter("@Publ_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = categoriaId
                };

                SqlParameter subcategoriaIdSqlParameter = new SqlParameter("@Publ_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = subcategoriaId
                };

                SqlParameter idiomaIdSqlParameter = new SqlParameter("@Publ_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Publicacion_SEL_PorSlug]", slugSqlParameter,
                               categoriaIdSqlParameter, subcategoriaIdSqlParameter, idiomaIdSqlParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PublicacionRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}