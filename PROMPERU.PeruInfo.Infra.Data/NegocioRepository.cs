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
    public class NegocioRepository : INegocioRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private NegocioBE Obtener(SqlDataReader reader)
        {
            return new NegocioBE
            {
                Id = reader.GetNullableInt("Nego_Id"),
                IdiomaId = reader.GetNullableInt("Nego_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Nego_IdiomaNombre"),
                PaisId = reader.GetNullableInt("Nego_PaisId"),
                PaisNombre = reader.GetNullableString("Pais_Nombre"),
                Titulo = reader.GetNullableString("Nego_Titulo"),
                Slug = reader.GetNullableString("Nego_Slug"),
                Resumen = reader.GetNullableString("Nego_Resumen"),
                Detalle = reader.GetNullableString("Nego_Detalle"),
                Imagen = reader.GetNullableString("Nego_Imagen"),
                Imagen2 = reader.GetNullableString("Nego_Imagen2"),
                Imagen3 = reader.GetNullableString("Nego_Imagen3"),
                Imagen4 = reader.GetNullableString("Nego_Imagen4"),
                Imagen5 = reader.GetNullableString("Nego_Imagen5"),
                AltImagen = reader.GetNullableString("Nego_AltImagen"),
                FechaPublicacion = reader.GetDateTime("Nego_FechaPublicacion"),
                FechaExpiracion = reader.GetDateTime("Nego_FechaExpiracion"),
                Destacado = reader.GetNullableBoolean("Nego_Destacado"),
                TituloSeo = reader.GetNullableString("Nego_TituloSEO"),
                Keywords = reader.GetNullableString("Nego_Keywords"),
                DescripcionSeo = reader.GetNullableString("Nego_DescripcionSEO"),
                Activo = reader.GetNullableBoolean("Nego_Activo"),
                DireccionWeb = reader.GetNullableString("Nego_DireccionWeb"),
                Telefono = reader.GetNullableString("Nego_Telefono"),
                Ciudad = reader.GetNullableString("Nego_Ciudad"),
                Direccion = reader.GetNullableString("Nego_Direccion"),
                FechaCreacion = reader.GetDateTime("Nego_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Nego_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Nego_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Nego_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Nego_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Nego_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Nego_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Nego_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Nego_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(NegocioBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Nego_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Nego_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IdiomaId ?? DBNull.Value
                };

                SqlParameter paisIdParameter = new SqlParameter("@Nego_PaisId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.PaisId ?? DBNull.Value
                };

                SqlParameter tituloParameter = new SqlParameter("@Nego_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter slugParameter = new SqlParameter("@Nego_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Slug ?? DBNull.Value
                };

                SqlParameter resumenParameter = new SqlParameter("@Nego_Resumen", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Resumen ?? DBNull.Value
                };

                SqlParameter detalleParameter = new SqlParameter("@Nego_Detalle", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Detalle ?? DBNull.Value
                };

                SqlParameter imagenParameter = new SqlParameter("@Nego_Imagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen ?? DBNull.Value
                };

                SqlParameter imagen2Parameter = new SqlParameter("@Nego_Imagen2", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen2 ?? DBNull.Value
                };

                SqlParameter imagen3Parameter = new SqlParameter("@Nego_Imagen3", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen3 ?? DBNull.Value
                };

                SqlParameter imagen4Parameter = new SqlParameter("@Nego_Imagen4", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen4 ?? DBNull.Value
                };

                SqlParameter imagen5Parameter = new SqlParameter("@Nego_Imagen5", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen5 ?? DBNull.Value
                };

                SqlParameter altImagenParameter = new SqlParameter("@Nego_AltImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.AltImagen ?? DBNull.Value
                };

                SqlParameter fechaPublicacionParameter = new SqlParameter("@Nego_FechaPublicacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaPublicacion
                };

                SqlParameter destacadoParameter = new SqlParameter("@Nego_Destacado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Destacado
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Nego_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Nego_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Nego_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Nego_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter direccionWebParameter = new SqlParameter("@Nego_DireccionWeb", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DireccionWeb ?? DBNull.Value
                };

                SqlParameter telefonoParameter = new SqlParameter("@Nego_Telefono", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Telefono ?? DBNull.Value
                };

                SqlParameter ciudadParameter = new SqlParameter("@Nego_Ciudad", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Ciudad ?? DBNull.Value
                };

                SqlParameter direccionParameter = new SqlParameter("@Nego_Direccion", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Direccion ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Nego_FechaModificacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Nego_UsuarioModificacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Nego_IpModificacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Negocio_UPD]",
                        idParameter,
                        idiomaIdParameter,
                        paisIdParameter,
                        tituloParameter,
                        slugParameter,
                        resumenParameter,
                        detalleParameter,
                        imagenParameter,
                        imagen2Parameter,
                        imagen3Parameter,
                        imagen4Parameter,
                        imagen5Parameter,
                        altImagenParameter,
                        fechaPublicacionParameter,
                        destacadoParameter,
                        tituloSeoParameter,
                        keywordsParameter,
                        descripcionSeoParameter,
                        activoParameter,
                        direccionWebParameter,
                        telefonoParameter,
                        ciudadParameter,
                        direccionParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("NegocioRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(NegocioBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Nego_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Nego_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Nego_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Nego_IpEliminacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Negocio_DEL]",
                        idParameter,
                        fechaEliminacionParameter,
                        usuarioEliminacionParameter,
                        ipEliminacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("NegocioRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(NegocioBE entity)
        {
            try
            {
                int insertedId;

                SqlParameter idParameter = new SqlParameter("@Nego_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Nego_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IdiomaId ?? DBNull.Value
                };

                SqlParameter paisIdParameter = new SqlParameter("@Nego_PaisId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.PaisId ?? DBNull.Value
                };

                SqlParameter tituloParameter = new SqlParameter("@Nego_Titulo", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter slugParameter = new SqlParameter("@Nego_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Slug ?? DBNull.Value
                };

                SqlParameter resumenParameter = new SqlParameter("@Nego_Resumen", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Resumen ?? DBNull.Value
                };

                SqlParameter detalleParameter = new SqlParameter("@Nego_Detalle", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Detalle ?? DBNull.Value
                };

                SqlParameter imagenParameter = new SqlParameter("@Nego_Imagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen ?? DBNull.Value
                };

                SqlParameter imagen2Parameter = new SqlParameter("@Nego_Imagen2", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen2 ?? DBNull.Value
                };

                SqlParameter imagen3Parameter = new SqlParameter("@Nego_Imagen3", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen3 ?? DBNull.Value
                };

                SqlParameter imagen4Parameter = new SqlParameter("@Nego_Imagen4", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen4 ?? DBNull.Value
                };

                SqlParameter imagen5Parameter = new SqlParameter("@Nego_Imagen5", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen5 ?? DBNull.Value
                };

                SqlParameter altImagenParameter = new SqlParameter("@Nego_AltImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.AltImagen ?? DBNull.Value
                };

                SqlParameter fechaPublicacionParameter = new SqlParameter("@Nego_FechaPublicacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaPublicacion
                };

                SqlParameter destacadoParameter = new SqlParameter("@Nego_Destacado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Destacado
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Nego_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Nego_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Nego_DescripcionSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Nego_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter direccionWebParameter = new SqlParameter("@Nego_DireccionWeb", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DireccionWeb ?? DBNull.Value
                };

                SqlParameter telefonoParameter = new SqlParameter("@Nego_Telefono", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Telefono ?? DBNull.Value
                };

                SqlParameter ciudadParameter = new SqlParameter("@Nego_Ciudad", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Ciudad ?? DBNull.Value
                };

                SqlParameter direccionParameter = new SqlParameter("@Nego_Direccion", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Direccion ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Nego_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Nego_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Nego_IpCreacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_Negocio_INS]",
                                    idParameter,
                                    idiomaIdParameter,
                                    paisIdParameter,
                                    tituloParameter,
                                    slugParameter,
                                    resumenParameter,
                                    detalleParameter,
                                    imagenParameter,
                                    imagen2Parameter,
                                    imagen3Parameter,
                                    imagen4Parameter,
                                    imagen5Parameter,
                                    altImagenParameter,
                                    fechaPublicacionParameter,
                                    destacadoParameter,
                                    tituloSeoParameter,
                                    keywordsParameter,
                                    descripcionSeoParameter,
                                    activoParameter,
                                    direccionWebParameter,
                                    telefonoParameter,
                                    ciudadParameter,
                                    direccionParameter,
                                    fechaModificacionParameter,
                                    usuarioModificacionParameter,
                                    ipModificacionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("NegocioRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<NegocioBE> Listar()
        {
            try
            {
                List<NegocioBE> items = new List<NegocioBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Negocio_LIS_Front]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"NegocioRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="paisId"></param>
        /// <param name="idiomaId"></param>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public List<NegocioBE> Listar(int paisId, int? idiomaId, string titulo)
        {
            try
            {
                List<NegocioBE> items = new List<NegocioBE>();

                SqlParameter paisIdParameter = new SqlParameter("@Nego_PaisId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = paisId
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Nego_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter tituloParameter = new SqlParameter("@Nego_Titulo", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Input,
                    Value = titulo
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Negocio_LIS]", paisIdParameter, idiomaIdParameter, tituloParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"NegocioRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NegocioBE Seleccionar(int id)
        {
            try
            {
                NegocioBE item = null;

                SqlParameter idParameter = new SqlParameter("@Nego_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Negocio_SEL]", idParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"NegocioRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
