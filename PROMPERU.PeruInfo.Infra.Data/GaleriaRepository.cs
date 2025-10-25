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
    public class GaleriaRepository : IGaleriaRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        #endregion

        #region PrivateMethods

        public static GaleriaBE Obtener(SqlDataReader reader)
        {
            return new GaleriaBE
            {
                Id = reader.GetNullableInt("Gale_Id"),
                CampaniaId = reader.GetNullableInt("Gale_CampaniaId"),
                CampaniaNombre = reader.GetNullableString("Gale_CampaniaNombre"),
                EntidadTipo = reader.GetNullableString("Gale_EntidadTipo"),
                EntidadId = reader.GetNullableInt("Gale_EntidadId"),
                IdiomaId = reader.GetNullableInt("Gale_IdiomaId"),
                TraduccionId = reader.GetNullableInt("Gale_TraduccionId"),
                RutaDekstop = reader.GetNullableString("Gale_RutaDekstop"),
                RutaMovil = reader.GetNullableString("Gale_RutaMovil"),
                TextoAlternativo = reader.GetNullableString("Gale_TextoAlternativo"),
                Uso = reader.GetNullableString("Gale_Uso"),
                CodigoYoutube = reader.GetNullableString("Gale_CodigoYoutube"),
                FechaCreacion = reader.GetDateTime("Gale_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Gale_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Gale_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Gale_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Gale_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Gale_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Gale_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Gale_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Gale_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<GaleriaBE> Listar()
        {
            try
            {
                List<GaleriaBE> items = new List<GaleriaBE>();

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Galeria_LIS]"))
                    {
                        while (reader.Read())
                        {
                            items.Add(Obtener(reader));
                        }
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lista todos los registros por idioma.
        /// </summary>
        /// <returns></returns>
        public List<GaleriaBE> Listar(int idiomaId)
        {
            try
            {
                List<GaleriaBE> items = new List<GaleriaBE>();

                SqlParameter idiomaParameter = new SqlParameter("@Gale_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };


                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Galeria_LIS_PorCampania]",
                               idiomaParameter))
                    {
                        while (reader.Read())
                        {
                            items.Add(Obtener(reader));
                        }
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar los registros por tipo e id.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GaleriaBE> Listar(int idiomaId, string tipo, int id)
        {
            try
            {
                List<GaleriaBE> items = new List<GaleriaBE>();

                SqlParameter idiomaParameter = new SqlParameter("@Gale_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter entidadTipoParameter = new SqlParameter("@Gale_EntidadTipo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = tipo
                };

                SqlParameter entidadIdParameter = new SqlParameter("@Gale_EntidadId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Galeria_LIS_PorTipo]",
                               idiomaParameter,
                               entidadTipoParameter,
                               entidadIdParameter))
                    {
                        while (reader.Read())
                        {
                            items.Add(Obtener(reader));
                        }
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar registros por idioma y tipo.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<GaleriaBE> Listar(int idiomaId, string tipo)
        {
            try
            {
                List<GaleriaBE> items = new List<GaleriaBE>();

                SqlParameter idiomaParameter = new SqlParameter("@Gale_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter entidadTipoParameter = new SqlParameter("@Gale_EntidadTipo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = tipo
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Galeria_LIS_PorIdioma]",
                               idiomaParameter,
                               entidadTipoParameter))
                    {
                        while (reader.Read())
                        {
                            items.Add(Obtener(reader));
                        }
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GaleriaBE Seleccionar(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(GaleriaBE entity)
        {
            try
            {
                SqlParameter entidadIdParameter = new SqlParameter("@Gale_EntidadId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.EntidadId
                };

                SqlParameter entidadTipoParameter = new SqlParameter("@Gale_EntidadTipo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.EntidadTipo
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Gale_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IdiomaId
                };

                SqlParameter rutaDesktopParameter = new SqlParameter("@Gale_RutaDekstop", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.RutaDekstop
                };

                SqlParameter rutaMovilParameter = new SqlParameter("@Gale_RutaMovil", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.RutaMovil
                };

                SqlParameter usoParameter = new SqlParameter("@Gale_Uso", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Uso
                };

                SqlParameter codigoYoutubeParameter = new SqlParameter("@Gale_CodigoYoutube", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CodigoYoutube
                };

                SqlParameter textoAlternativoParameter = new SqlParameter("@Gale_TextoAlternativo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TextoAlternativo
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Gale_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaCreacion
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Gale_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioCreacion
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Gale_IpCreacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpCreacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Galeria_INS]",
                        entidadTipoParameter,
                        entidadIdParameter,
                        rutaDesktopParameter,
                        rutaMovilParameter,
                        usoParameter,
                        codigoYoutubeParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter,
                        idiomaIdParameter,
                        textoAlternativoParameter);
                }

                return 1;
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaRepository:::{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(GaleriaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Gale_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Gale_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter entidadIdParameter = new SqlParameter("@Gale_EntidadId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.EntidadId
                };

                SqlParameter entidadTipoParameter = new SqlParameter("@Gale_EntidadTipo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.EntidadTipo
                };

                SqlParameter rutaDesktopParameter = new SqlParameter("@Gale_RutaDekstop", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.RutaDekstop
                };

                SqlParameter rutaMovilParameter = new SqlParameter("@Gale_RutaMovil", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.RutaMovil
                };

                SqlParameter usoParameter = new SqlParameter("@Gale_Uso", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Uso
                };

                SqlParameter codigoYoutubeParameter = new SqlParameter("@Gale_CodigoYoutube", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.CodigoYoutube
                };

                SqlParameter textoAlternativoParameter = new SqlParameter("@Gale_TextoAlternativo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TextoAlternativo
                };

                SqlParameter fechaModificacionParameter =
                    new SqlParameter("@Gale_FechaModificacion", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Input,
                        Value = entity.FechaModificacion
                    };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Gale_UsuarioModificacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Gale_IpModificacion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Galeria_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        entidadTipoParameter,
                        entidadIdParameter,
                        rutaDesktopParameter,
                        rutaMovilParameter,
                        usoParameter,
                        codigoYoutubeParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter,
                        textoAlternativoParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"GaleriaRepository:::{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(GaleriaBE entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}