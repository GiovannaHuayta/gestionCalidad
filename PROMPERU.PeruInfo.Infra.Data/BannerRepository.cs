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
    public class BannerRepository : IBannerRepository
    {
        #region Private Properties

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static BannerBE Obtener(SqlDataReader reader)
        {
            return new BannerBE
            {
                Id = reader.GetNullableInt("Bann_Id"),
                CategoriaId = reader.GetNullableInt("Bann_CategoriaId"),
                CategoriaNombre = reader.GetNullableString("Bann_CategoriaNombre"),
                SubcategoriaId = reader.GetNullableInt("Bann_SubcategoriaId"),
                SubcategoriaNombre = reader.GetNullableString("Bann_SubcategoriaNombre"),
                PaginaId = reader.GetNullableInt("Bann_PaginaId"),
                TraduccionId = reader.GetNullableInt("Bann_TraduccionId"),
                ImagenId = reader.GetNullableInt("Bann_ImagenId"),
                ImagenTraduccionId = reader.GetNullableInt("Bann_ImagenTraduccionId"),
                ImagenDesktop = reader.GetNullableString("Bann_ImagenRutaDesktop"),
                ImagenMovil = reader.GetNullableString("Bann_ImagenRutaMovil"),
                ImagenTextoAlternativo = reader.GetNullableString("Bann_ImagenTextoAlternativo"),
                Titulo = reader.GetNullableString("Bann_Titulo"),
                Bajada = reader.GetNullableString("Bann_Bajada"),
                Orden = reader.GetNullableInt("Bann_Orden"),
                Link = reader.GetNullableString("Bann_Link"),
                VentanaNueva = reader.GetNullableBoolean("Bann_VentanaNueva"),
                IdiomaId = reader.GetNullableInt("Bann_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Bann_IdiomaNombre"),
                Activo = reader.GetNullableBoolean("Bann_Activo"),
                FechaCreacion = reader.GetDateTime("Bann_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Bann_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Bann_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Bann_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Bann_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Bann_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Bann_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Bann_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Bann_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<BannerBE> Listar()
        {
            try
            {
                List<BannerBE> items = new List<BannerBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Banner_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"BannerRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BannerBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(BannerBE entity)
        {
            try
            {
                object lastId;

                SqlParameter categoriaIdParameter = new SqlParameter("@Bann_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CategoriaId
                };

                SqlParameter subcategoriaIdParameter = new SqlParameter("@Bann_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubcategoriaId
                };

                SqlParameter paginaIdParameter = new SqlParameter("@Bann_PaginaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.PaginaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Bann_Titulo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter bajadaParameter = new SqlParameter("@Bann_Bajada", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Bajada
                };

                SqlParameter ordenParameter = new SqlParameter("@Bann_Orden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Orden
                };

                SqlParameter linkParameter = new SqlParameter("@Bann_Link", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Link
                };

                SqlParameter ventanaNuevaParameter = new SqlParameter("@Bann_VentanaNueva", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.VentanaNueva
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Bann_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter activoParameter = new SqlParameter("@Bann_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Bann_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Bann_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Bann_IpCreacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    lastId = adoHelper.ExecScalarProc("[USP_Banner_INS]",
                        categoriaIdParameter,
                        subcategoriaIdParameter,
                        paginaIdParameter,
                        tituloParameter,
                        bajadaParameter,
                        ordenParameter,
                        linkParameter,
                        ventanaNuevaParameter,
                        idiomaIdParameter,
                        activoParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return int.Parse(lastId.ToString());
            }
            catch (Exception ex)
            {
                Log.Error($"BannerRepository:::{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        public bool Actualizar(BannerBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Bann_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Bann_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter categoriaIdParameter = new SqlParameter("@Bann_CategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CategoriaId
                };

                SqlParameter subcategoriaIdParameter = new SqlParameter("@Bann_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubcategoriaId
                };

                SqlParameter paginaIdParameter = new SqlParameter("@Bann_PaginaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.PaginaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Bann_Titulo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter bajadaParameter = new SqlParameter("@Bann_Bajada", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Bajada
                };

                SqlParameter ordenParameter = new SqlParameter("@Bann_Orden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Orden
                };

                SqlParameter linkParameter = new SqlParameter("@Bann_Link", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Link
                };

                SqlParameter ventanaNuevaParameter = new SqlParameter("@Bann_VentanaNueva", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.VentanaNueva
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Bann_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter activoParameter = new SqlParameter("@Bann_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Bann_FechaModificacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Bann_UsuarioModificacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Bann_IpModificacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecScalarProc("[USP_Banner_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        categoriaIdParameter,
                        subcategoriaIdParameter,
                        paginaIdParameter,
                        tituloParameter,
                        bajadaParameter,
                        ordenParameter,
                        linkParameter,
                        ventanaNuevaParameter,
                        idiomaIdParameter,
                        activoParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"BannerRepository:::{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(BannerBE entity)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}