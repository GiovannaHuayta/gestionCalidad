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
    public class PaginaRepository : IPaginaRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        #endregion

        #region Private Methods

        private static PaginaBE Obtener(SqlDataReader reader)
        {
            return new PaginaBE
            {
                Id = reader.GetNullableInt("Pagi_Id"),
                Posicion = reader.GetNullableString("Pagi_Posicion"),
                TraduccionId = reader.GetNullableInt("Pagi_TraduccionId"),
                IdiomaId = reader.GetNullableInt("Pagi_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Pagi_IdiomaNombre"),
                Nombre = reader.GetNullableString("Pagi_Nombre"),
                Slug = reader.GetNullableString("Pagi_Slug"),
                LinkExterno = reader.GetNullableString("Pagi_LinkExterno"),
                Contenido = reader.GetNullableString("Pagi_Contenido"),
                Activo = reader.GetNullableBoolean("Pagi_Activo"),
                Orden = reader.GetNullableInt("Pagi_Orden"),
                IdPadre = reader.GetNullableInt("Pagi_IdPadre"),
                Informativa = reader.GetNullableBoolean("Pagi_Informativa"),
                FechaCreacion = reader.GetDateTime("Pagi_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Pagi_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Pagi_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Pagi_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Pagi_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Pagi_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Pagi_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Pagi_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Pagi_IpEliminacion"),
                
                TituloSeo = reader.GetNullableString("Pagi_TituloSeo"),
                DescripcionSeo = reader.GetNullableString("Pagi_DescripcionSeo"),
                CanonicalSeo = reader.GetNullableString("Pagi_CanonicalSeo")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<PaginaBE> Listar()
        {
            try
            {
                List<PaginaBE> items = new List<PaginaBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("USP_Pagina_LIS"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lista todos los registros por idioma.
        /// </summary>
        /// <param name="idioma"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public List<PaginaBE> ListarPorIdioma(int idioma, bool activo = true)
        {
            try
            {
                List<PaginaBE> items = new List<PaginaBE>();

                SqlParameter idiomaSqlParameter = new SqlParameter("@Pagi_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idioma
                };

                SqlParameter activoSqlParameter = new SqlParameter("@Pagi_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = activo
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("USP_Pagina_LIS_PorIdioma", idiomaSqlParameter, activoSqlParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PaginaBE Seleccionar(int id)
        {
            try
            {
                PaginaBE item = new PaginaBE();

                SqlParameter idSqlParameter = new SqlParameter("Pagi_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("USP_Pagina_SEL", idSqlParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public PaginaBE Seleccionar(string slug)
        {
            try
            {
                PaginaBE item = new PaginaBE();

                SqlParameter slugSqlParameter = new SqlParameter("@Pagi_Slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = slug
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Pagina_SEL_PorSlug]", slugSqlParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Seleccionar un registro por idioma.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idioma"></param>
        /// <returns></returns>
        public PaginaBE SeleccionarPorIdioma(int id, int idioma)
        {
            try
            {
                PaginaBE item = new PaginaBE();

                SqlParameter idSqlParameter = new SqlParameter("Pagi_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                SqlParameter idiomaSqlParameter = new SqlParameter("Pagi_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idioma
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("USP_Pagina_SEL_PorIdioma", idSqlParameter, idiomaSqlParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PaginaBE entity)
        {
            try
            {
                object id;

                SqlParameter idiomaSqlParameter = new SqlParameter("@Pagi_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Pagi_Nombre", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre
                };

                SqlParameter slugParameter = new SqlParameter("@Pagi_Slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug
                };

                SqlParameter linkExternoParameter = new SqlParameter("@Pagi_LinkExterno", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.LinkExterno
                };

                SqlParameter contenidoParameter = new SqlParameter("@Pagi_Contenido", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Contenido
                };

                SqlParameter activoParameter = new SqlParameter("@Pagi_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter ordenParameter = new SqlParameter("@Pagi_Orden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Orden
                };

                SqlParameter padreIdParameter = new SqlParameter("@Pagi_PadreId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdPadre
                };

                SqlParameter informativaParameter = new SqlParameter("@Pagi_Informativa", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Informativa
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@Pagi_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@Pagi_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@Pagi_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                /*Informacion SEO*/

                SqlParameter TituloSeo = new SqlParameter("@Pagi_TituloSeo", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TituloSeo
                };

                SqlParameter DescripcionSeo = new SqlParameter("@Pagi_DescripcionSeo", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.DescripcionSeo
                };

                SqlParameter CanonicalSeo = new SqlParameter("@Pagi_CanonicalSeo", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CanonicalSeo
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    id = adoHelper.ExecScalarProc("[USP_Pagina_INS]",
                        idiomaSqlParameter,
                        nombreParameter,
                        slugParameter,
                        linkExternoParameter,
                        contenidoParameter,
                        activoParameter,
                        ordenParameter,
                        padreIdParameter,
                        informativaParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        ipEdicionParameter,
                        TituloSeo,
                        DescripcionSeo,
                        CanonicalSeo);
                }

                return int.Parse(id.ToString());
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PaginaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Pagi_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter idiomaSqlParameter = new SqlParameter("@Pagi_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Pagi_Nombre", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Nombre ?? string.Empty
                };

                SqlParameter slugParameter = new SqlParameter("@Pagi_Slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Slug ?? string.Empty
                };

                SqlParameter linkExternoParameter = new SqlParameter("@Pagi_LinkExterno", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.LinkExterno ?? string.Empty
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Pagi_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter contenidoParameter = new SqlParameter("@Pagi_Contenido", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Contenido ?? string.Empty
                };

                SqlParameter activoParameter = new SqlParameter("@Pagi_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter ordenParameter = new SqlParameter("@Pagi_Orden", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Orden
                };

                SqlParameter padreIdParameter = new SqlParameter("@Pagi_PadreId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdPadre ?? 0
                };

                SqlParameter informativaParameter = new SqlParameter("@Pagi_Informativa", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Informativa ?? false
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@Pagi_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@Pagi_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@Pagi_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                /*Informacion SEO*/

                SqlParameter TituloSeo = new SqlParameter("@Pagi_TituloSeo", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TituloSeo ?? string.Empty
                };

                SqlParameter DescripcionSeo = new SqlParameter("@Pagi_DescripcionSeo", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.DescripcionSeo ?? string.Empty
                };

                SqlParameter CanonicalSeo = new SqlParameter("@Pagi_CanonicalSeo", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CanonicalSeo ?? string.Empty
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Pagina_UPD]",
                        idParameter,
                        idiomaSqlParameter,
                        nombreParameter,
                        slugParameter,
                        linkExternoParameter,
                        traduccionIdParameter,
                        contenidoParameter,
                        activoParameter,
                        ordenParameter,
                        padreIdParameter,
                        informativaParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        ipEdicionParameter,
                        TituloSeo,
                        DescripcionSeo,
                        CanonicalSeo);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("PaginaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PaginaBE entity)
        {
            try
            {
                SqlParameter traduccionIdParameter = new SqlParameter("@Pagi_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Pagi_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Pagi_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Pagi_IpEliminacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Pagina_DEL]",
                        traduccionIdParameter,
                        fechaEliminacionParameter,
                        usuarioEliminacionParameter,
                        ipEliminacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"PaginaRepository:::{ex.Message}");
                return false;
            }
        }

        #endregion
    }
}