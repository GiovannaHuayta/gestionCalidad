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
    public class TarjetaRepository : ITarjetaRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private TarjetaBE Obtener(SqlDataReader reader)
        {
            return new TarjetaBE
            {
                Id = reader.GetNullableInt("Tarj_Id"),
                SubcategoriaId = reader.GetNullableInt("Tarj_SubcategoriaId"),
                SubcategoriaNombre = reader.GetNullableString("Tarj_SubcategoriaNombre"),
                CategoriaNombre = reader.GetNullableString("Tarj_CategoriaNombre"),
                TraduccionId = reader.GetNullableInt("Tarj_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Tarj_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Tarj_IdiomaNombre"),
                Titulo = reader.GetNullableString("Tarj_Titulo"),
                Descripcion = reader.GetNullableString("Tarj_Descripcion"),
                NombreBoton = reader.GetNullableString("Tarj_NombreBoton"),
                Link = reader.GetNullableString("Tarj_Link"),
                VentanaNueva = reader.GetNullableBoolean("Tarj_VentanaNueva"),
                Orden = reader.GetNullableInt("Tarj_Orden"),
                Activo = reader.GetNullableBoolean("Tarj_Activo"),
                ImagenDesktop = reader.GetNullableString("Tarj_ImagenDesktop"),
                ImagenMovil = reader.GetNullableString("Tarj_ImagenMovil"),
                ImagenAlt = reader.GetNullableString("Tarj_ImagenAlt"),
                FechaCreacion = reader.GetDateTime("Tarj_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Tarj_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Tarj_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Tarj_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Tarj_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Tarj_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Tarj_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Tarj_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Tarj_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(TarjetaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Tarj_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Tarj_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter subCategoriaIdParameter = new SqlParameter("@Tarj_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubcategoriaId
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Tarj_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Tarj_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Titulo ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Tarj_Descripcion", SqlDbType.VarChar, 400)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Descripcion ?? DBNull.Value
                };

                SqlParameter nombreBotonParameter = new SqlParameter("@Tarj_NombreBoton", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.NombreBoton ?? DBNull.Value
                };

                SqlParameter linkParameter = new SqlParameter("@Tarj_Link", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Link ?? DBNull.Value
                };

                SqlParameter ventanaNuevaParameter = new SqlParameter("@Tarj_VentanaNueva", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VentanaNueva ?? DBNull.Value
                };

                SqlParameter ordenParameter = new SqlParameter("@Tarj_Orden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Orden ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Tarj_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Tarj_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaModificacion ?? DBNull.Value
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Tarj_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioModificacion ?? DBNull.Value
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Tarj_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpModificacion ?? DBNull.Value
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Tarjeta_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        subCategoriaIdParameter,
                        idiomaIdParameter,
                        tituloParameter,
                        descripcionParameter,
                        nombreBotonParameter,
                        linkParameter,
                        ventanaNuevaParameter,
                        ordenParameter,
                        activoParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("TarjetaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(TarjetaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Tarj_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Tarj_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Tarj_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Tarj_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Tarj_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Tarjeta_DEL]",
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
                Log.Error("TarjetaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(TarjetaBE entity)
        {
            try
            {
                object insertedId;

                SqlParameter subCategoriaIdParameter = new SqlParameter("@Tarj_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubcategoriaId
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Tarj_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Tarj_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Titulo ?? DBNull.Value
                };

                SqlParameter descripcionParameter = new SqlParameter("@Tarj_Descripcion", SqlDbType.VarChar, 400)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Descripcion ?? DBNull.Value
                };

                SqlParameter nombreBotonParameter = new SqlParameter("@Tarj_NombreBoton", SqlDbType.VarChar, 50)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.NombreBoton ?? DBNull.Value
                };

                SqlParameter linkParameter = new SqlParameter("@Tarj_Link", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Link ?? DBNull.Value
                };

                SqlParameter ventanaNuevaParameter = new SqlParameter("@Tarj_VentanaNueva", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.VentanaNueva ?? DBNull.Value
                };

                SqlParameter ordenParameter = new SqlParameter("@Tarj_Orden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Orden ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Tarj_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Tarj_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Tarj_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Tarj_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecScalarProc("[USP_Tarjeta_INS]",
                        subCategoriaIdParameter,
                        idiomaIdParameter,
                        tituloParameter,
                        descripcionParameter,
                        nombreBotonParameter,
                        linkParameter,
                        ventanaNuevaParameter,
                        ordenParameter,
                        activoParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return int.Parse(insertedId.ToString());
            }
            catch (Exception ex)
            {
                Log.Error("TarjetaRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public List<TarjetaBE> Listar(int idiomaId)
        {
            try
            {
                List<TarjetaBE> items = new List<TarjetaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Idio_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Tarjeta_LIS]", idiomaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"TarjetaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listado por idioma y subcategoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoriaId"></param>
        /// <returns></returns>
        public List<TarjetaBE> Listar(int idiomaId, int subcategoriaId)
        {
            try
            {
                List<TarjetaBE> items = new List<TarjetaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Tarj_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter categoriaIdParameter = new SqlParameter("@Tarj_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = subcategoriaId
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Tarjeta_LIS_PorSubcategoria]",
                               idiomaIdParameter, categoriaIdParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"TarjetaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public TarjetaBE Seleccionar(int id, int idiomaId)
        {
            try
            {
                TarjetaBE item = null;

                SqlParameter idParameter = new SqlParameter("@Tarj_Id", SqlDbType.Int)
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
                           adoHelper.ExecDataReaderProc("[USP_Tarjeta_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"TarjetaRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}