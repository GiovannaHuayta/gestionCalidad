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
    public class PersonaRepository : IPersonaRepository
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Private Methods

        private static PersonaBE Obtener(SqlDataReader reader)
        {
            return new PersonaBE
            {
                Id = reader.GetNullableInt("Pers_Id"),
                SubcategoriaId = reader.GetNullableInt("Pers_SubcategoriaId"),
                SubcategoriaNombre = reader.GetNullableString("Subcategoria_Nombre"),
                SubcategoriaSlug = reader.GetNullableString("Pers_SubcategoriaSlug"),
                CategoriaId = reader.GetNullableInt("Pers_CategoriaId"),
                CategoriaNombre = reader.GetNullableString("Categoria_Nombre"),
                CategoriaSlug = reader.GetNullableString("Pers_CategoriaSlug"),
                Nombre = reader.GetNullableString("Pers_Nombre"),
                Slug = reader.GetNullableString("Pers_Slug"),
                Imagen = reader.GetNullableString("Pers_Imagen"),
                Imagen2 = reader.GetNullableString("Pers_Imagen2"),
                Imagen3 = reader.GetNullableString("Pers_Imagen3"),
                Anio = reader.GetNullableInt("Pers_Anio"),
                AltImagen = reader.GetNullableString("Pers_AltImagen"),
                Destacado = reader.GetNullableBoolean("Pers_Destacado"),
                Tipo = reader.GetNullableInt("Pers_Tipo"),
                Facebook = reader.GetNullableString("Pers_Facebook"),
                Twitter = reader.GetNullableString("Pers_Twitter"),
                Instagram = reader.GetNullableString("Pers_Instagram"),
                Youtube = reader.GetNullableString("Pers_Youtube"),
                Activo = reader.GetNullableBoolean("Pers_Activo"),
                TraduccionId = reader.GetNullableInt("Ptra_Id"),
                IdiomaId = reader.GetNullableInt("Pers_IdiomaId"),
                IdiomaNombre = reader.GetNullableString("Pers_IdiomaNombre"),
                Resumen = reader.GetNullableString("Pers_Resumen"),
                Detalle = reader.GetNullableString("Pers_Detalle"),
                TituloSeo = reader.GetNullableString("Pers_TituloSEO"),
                Keywords = reader.GetNullableString("Pers_Keywords"),
                DescripcionSeo = reader.GetNullableString("Pers_DescripcionSEO"),
                FechaCreacion = reader.GetDateTime("Pers_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("Pers_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("Pers_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("Pers_FechaModificacion"),
                UsuarioModificacion = reader.GetNullableInt("Pers_UsuarioModificacion"),
                IpModificacion = reader.GetNullableString("Pers_IpModificacion"),
                FechaEliminacion = reader.GetNullableDateTime("Pers_FechaEliminacion"),
                UsuarioEliminacion = reader.GetNullableInt("Pers_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("Pers_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(PersonaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Pers_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Pers_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter subcategoriaIdParameter = new SqlParameter("@Pers_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.SubcategoriaId ?? DBNull.Value
                };

                SqlParameter nombreParameter = new SqlParameter("@Pers_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Nombre ?? DBNull.Value
                };

                SqlParameter slugParameter = new SqlParameter("@Pers_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Slug ?? DBNull.Value
                };

                SqlParameter imagenParameter = new SqlParameter("@Pers_Imagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen ?? DBNull.Value
                };

                SqlParameter imagen2Parameter = new SqlParameter("@Pers_Imagen2", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen2 ?? DBNull.Value
                };

                SqlParameter imagen3Parameter = new SqlParameter("@Pers_Imagen3", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen3 ?? DBNull.Value
                };

                SqlParameter anioParameter = new SqlParameter("@Pers_Anio", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Anio ?? DBNull.Value
                };

                SqlParameter altImagenParameter = new SqlParameter("@Pers_AltImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.AltImagen ?? DBNull.Value
                };

                SqlParameter destacadoParameter = new SqlParameter("@Pers_Destacado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Destacado ?? DBNull.Value
                };

                SqlParameter tipoParameter = new SqlParameter("@Pers_Tipo", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Tipo ?? DBNull.Value
                };

                SqlParameter facebookParameter = new SqlParameter("@Pers_Facebook", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Facebook ?? DBNull.Value
                };

                SqlParameter twitterParameter = new SqlParameter("@Pers_Twitter", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Twitter ?? DBNull.Value
                };

                SqlParameter instagramParameter = new SqlParameter("@Pers_Instagram", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Instagram ?? DBNull.Value
                };

                SqlParameter youtubeParameter = new SqlParameter("@Pers_Youtube", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Youtube ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Pers_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Pers_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IdiomaId ?? DBNull.Value
                };

                SqlParameter resumenParameter = new SqlParameter("@Pers_Resumen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Resumen ?? DBNull.Value
                };

                SqlParameter detalleParameter = new SqlParameter("@Pers_Detalle", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Detalle ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Pers_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Pers_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Pers_DescripcionSEO", SqlDbType.VarChar, 300)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter fechaModificacionParameter = new SqlParameter("@Pers_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaModificacion ?? DBNull.Value
                };

                SqlParameter usuarioModificacionParameter = new SqlParameter("@Pers_UsuarioEdicion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioModificacion ?? DBNull.Value
                };

                SqlParameter ipModificacionParameter = new SqlParameter("@Pers_IpEdicion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpModificacion ?? DBNull.Value
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Persona_UPD]",
                        idParameter,
                        traduccionIdParameter,
                        subcategoriaIdParameter,
                        nombreParameter,
                        slugParameter,
                        imagenParameter,
                        imagen2Parameter,
                        imagen3Parameter,
                        anioParameter,
                        altImagenParameter,
                        destacadoParameter,
                        tipoParameter,
                        facebookParameter,
                        twitterParameter,
                        instagramParameter,
                        youtubeParameter,
                        activoParameter,
                        idiomaIdParameter,
                        resumenParameter,
                        detalleParameter,
                        tituloSeoParameter,
                        keywordsParameter,
                        descripcionSeoParameter,
                        fechaModificacionParameter,
                        usuarioModificacionParameter,
                        ipModificacionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("PersonaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Elimina un registro.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Eliminar(PersonaBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@Pers_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter traduccionIdParameter = new SqlParameter("@Pers_TraduccionId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.TraduccionId
                };

                SqlParameter fechaEliminacionParameter = new SqlParameter("@Pers_FechaEliminacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaEliminacion
                };

                SqlParameter usuarioEliminacionParameter = new SqlParameter("@Pers_UsuarioEliminacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioEliminacion
                };

                SqlParameter ipEliminacionParameter = new SqlParameter("@Pers_IpEliminacion", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpEliminacion
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    adoHelper.ExecNonQueryProc("[USP_Persona_DEL]",
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
                Log.Error("PersonaRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insertar(PersonaBE entity)
        {
            try
            {
                int insertedId;

                SqlParameter idParameter = new SqlParameter("@Pers_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                SqlParameter subcategoriaIdParameter = new SqlParameter("@Pers_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.SubcategoriaId ?? DBNull.Value
                };

                SqlParameter nombreParameter = new SqlParameter("@Pers_Nombre", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Nombre ?? DBNull.Value
                };

                SqlParameter slugParameter = new SqlParameter("@Pers_Slug", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Slug ?? DBNull.Value
                };

                SqlParameter imagenParameter = new SqlParameter("@Pers_Imagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen ?? DBNull.Value
                };

                SqlParameter imagen2Parameter = new SqlParameter("@Pers_Imagen2", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen2 ?? DBNull.Value
                };

                SqlParameter imagen3Parameter = new SqlParameter("@Pers_Imagen3", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Imagen3 ?? DBNull.Value
                };

                SqlParameter anioParameter = new SqlParameter("@Pers_Anio", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Anio ?? DBNull.Value
                };

                SqlParameter altImagenParameter = new SqlParameter("@Pers_AltImagen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.AltImagen ?? DBNull.Value
                };

                SqlParameter destacadoParameter = new SqlParameter("@Pers_Destacado", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Destacado ?? DBNull.Value
                };

                SqlParameter tipoParameter = new SqlParameter("@Pers_Tipo", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Tipo ?? DBNull.Value
                };

                SqlParameter facebookParameter = new SqlParameter("@Pers_Facebook", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Facebook ?? DBNull.Value
                };

                SqlParameter twitterParameter = new SqlParameter("@Pers_Twitter", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Twitter ?? DBNull.Value
                };

                SqlParameter instagramParameter = new SqlParameter("@Pers_Instagram", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Instagram ?? DBNull.Value
                };

                SqlParameter youtubeParameter = new SqlParameter("@Pers_Youtube", SqlDbType.VarChar, 150)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Youtube ?? DBNull.Value
                };

                SqlParameter activoParameter = new SqlParameter("@Pers_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Activo ?? DBNull.Value
                };

                SqlParameter idiomaIdParameter = new SqlParameter("@Pers_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IdiomaId ?? DBNull.Value
                };

                SqlParameter resumenParameter = new SqlParameter("@Pers_Resumen", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Resumen ?? DBNull.Value
                };

                SqlParameter detalleParameter = new SqlParameter("@Pers_Detalle", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Detalle ?? DBNull.Value
                };

                SqlParameter tituloSeoParameter = new SqlParameter("@Pers_TituloSEO", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.TituloSeo ?? DBNull.Value
                };

                SqlParameter keywordsParameter = new SqlParameter("@Pers_KeyWords", SqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.Keywords ?? DBNull.Value
                };

                SqlParameter descripcionSeoParameter = new SqlParameter("@Pers_DescripcionSEO", SqlDbType.VarChar, 300)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.DescripcionSeo ?? DBNull.Value
                };

                SqlParameter fechaCreacionParameter = new SqlParameter("@Pers_FechaCreacion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.FechaCreacion ?? DBNull.Value
                };

                SqlParameter usuarioCreacionParameter = new SqlParameter("@Pers_UsuarioCreacion", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.UsuarioCreacion ?? DBNull.Value
                };

                SqlParameter ipCreacionParameter = new SqlParameter("@Pers_IpCreacion", SqlDbType.VarChar, 15)
                {
                    Direction = ParameterDirection.Input,
                    Value = (object)entity.IpCreacion ?? DBNull.Value
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    insertedId = adoHelper.ExecNonQueryProc("[USP_Persona_INS]",
                        idParameter,
                        subcategoriaIdParameter,
                        nombreParameter,
                        slugParameter,
                        imagenParameter,
                        imagen2Parameter,
                        imagen3Parameter,
                        anioParameter,
                        altImagenParameter,
                        destacadoParameter,
                        tipoParameter,
                        facebookParameter,
                        twitterParameter,
                        instagramParameter,
                        youtubeParameter,
                        activoParameter,
                        idiomaIdParameter,
                        resumenParameter,
                        detalleParameter,
                        tituloSeoParameter,
                        keywordsParameter,
                        descripcionSeoParameter,
                        fechaCreacionParameter,
                        usuarioCreacionParameter,
                        ipCreacionParameter);
                }

                return insertedId;
            }
            catch (Exception ex)
            {
                Log.Error("PersonaRepository:::" + ex.Message);

                return 0;
            }
        }

        /// <summary>
        /// Lista todos los registros.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subCategoriaId"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<PersonaBE> Listar(int? idiomaId, int? subCategoriaId, string nombre)
        {
            try
            {
                List<PersonaBE> items = new List<PersonaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Pers_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter subCategoriaIdParameter = new SqlParameter("@Pers_SubcategoriaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = subCategoriaId
                };

                SqlParameter nombreParameter = new SqlParameter("@Pers_Nombre", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Input,
                    Value = nombre
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Personas_LIS]", idiomaIdParameter,
                               subCategoriaIdParameter, nombreParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PersonaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listar por idioma y subcategoría.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoria"></param>
        /// <returns></returns>
        public List<PersonaBE> Listar(int idiomaId, string subcategoria)
        {
            try
            {
                List<PersonaBE> items = new List<PersonaBE>();

                SqlParameter idiomaIdParameter = new SqlParameter("@Pers_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter subcategoriaSlugParameter = new SqlParameter("@Pers_SubcategoriaSlug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = subcategoria
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_Persona_LIS_PorSubcategoria]",
                               idiomaIdParameter, subcategoriaSlugParameter))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                Log.Error($"PersonaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro por su id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idiomaId"></param>
        /// <returns></returns>
        public PersonaBE Seleccionar(int id, int idiomaId)
        {
            try
            {
                PersonaBE item = null;

                SqlParameter idParameter = new SqlParameter("@Pers_Id", SqlDbType.Int)
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
                           adoHelper.ExecDataReaderProc("[USP_Persona_SEL]", idParameter, idiomaIdParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PersonaRepository:::{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Seleccionar por slug.
        /// </summary>
        /// <param name="idiomaId"></param>
        /// <param name="subcategoria"></param>
        /// <param name="slug"></param>
        /// <returns></returns>
        public PersonaBE Seleccionar(int idiomaId, string subcategoria, string slug)
        {
            try
            {
                PersonaBE item = null;

                SqlParameter idiomaIdParameter = new SqlParameter("@Pers_IdiomaId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = idiomaId
                };

                SqlParameter subcategoriaSlugParameter = new SqlParameter("@Pers_SubcategoriaSlug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = subcategoria
                };

                SqlParameter personaSlugParameter = new SqlParameter("@Pers_Slug", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = slug
                };

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    using (SqlDataReader reader =
                           adoHelper.ExecDataReaderProc("[USP_Persona_SEL_PorSlug]", idiomaIdParameter,
                               subcategoriaSlugParameter, personaSlugParameter))
                    {
                        while (reader.Read()) item = Obtener(reader);
                    }
                }

                return item;
            }
            catch (Exception ex)
            {
                Log.Error($"PersonaRepository:::{ex.Message}");
                return null;
            }
        }

        #endregion
    }
}