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
    public class NosPreparamosRepository:INosPreparamosRepository
    {
        #region Private Variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        

        #endregion

        #region Private Methods

        private NosPreparamosBE Obtener(SqlDataReader reader)
        {
            return new NosPreparamosBE
            {
                Id = reader.GetNullableInt("NPre_Id"),
                Titulo = reader.GetNullableString("NPre_Titulo"),
                SubTitulo = reader.GetNullableString("NPre_SubTitulo"),
                Imagen = reader.GetNullableString("NPre_Imagen"),
                AltImagen = reader.GetNullableString("NPre_AltImagen"),
                LinkRedSocial = reader.GetNullableString("NPre_LinkRedSocial"),
                HtmlRedSocial = reader.GetNullableString("NPre_HtmlRedSocial"),
                Activo = reader.GetNullableBoolean("NPre_Activo"),
                FechaCreacion = reader.GetDateTime("NPre_FechaCreacion"),
                UsuarioCreacion = reader.GetNullableInt("NPre_UsuarioCreacion"),
                IpCreacion = reader.GetNullableString("NPre_IpCreacion"),
                FechaModificacion = reader.GetNullableDateTime("NPre_FechaEdicion"),
                UsuarioModificacion = reader.GetNullableInt("NPre_UsuarioEdicion"),
                IpModificacion = reader.GetNullableString("NPre_IpEdicion"),
                FechaEliminacion = reader.GetNullableDateTime("NPre_FechaELiminacion"),
                UsuarioEliminacion = reader.GetNullableInt("NPre_UsuarioEliminacion"),
                IpEliminacion = reader.GetNullableString("NPre_IpEliminacion")
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Actualizar(NosPreparamosBE entity)
        {
            try
            {
                SqlParameter idParameter = new SqlParameter("@NPre_Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Id
                };

                SqlParameter tituloParameter = new SqlParameter("@NPre_Titulo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Titulo
                };

                SqlParameter subTituloParameter = new SqlParameter("@NPre_SubTitulo", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.SubTitulo
                };

                SqlParameter imagenParameter = new SqlParameter("@NPre_Imagen", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Imagen
                };

                SqlParameter altImagenParameter = new SqlParameter("@NPre_AltImagen", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.AltImagen
                };

                SqlParameter linkRedSocialParameter = new SqlParameter("@NPre_LinkRedSocial", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.LinkRedSocial
                };

                SqlParameter htmlRedSocialParameter = new SqlParameter("@NPre_HtmlRedSocial", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.HtmlRedSocial
                };

                SqlParameter activoParameter = new SqlParameter("@NPre_Activo", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.Activo
                };

                SqlParameter fechaEdicionParameter = new SqlParameter("@NPre_FechaEdicion", SqlDbType.DateTime)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.FechaModificacion
                };

                SqlParameter usuarioEdicionParameter = new SqlParameter("@NPre_UsuarioEdicion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.UsuarioModificacion
                };

                SqlParameter ipEdicionParameter = new SqlParameter("@NPre_IpEdicion", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = entity.IpModificacion
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    adoHelper.ExecNonQueryProc("[USP_NosPreparamos_UPD]",
                        idParameter,
                        tituloParameter,
                        subTituloParameter,
                        imagenParameter,
                        altImagenParameter,
                        linkRedSocialParameter,
                        htmlRedSocialParameter,
                        activoParameter,
                        fechaEdicionParameter,
                        usuarioEdicionParameter,
                        ipEdicionParameter);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("NosPreparamosRepository:::" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Listar todos los registros.
        /// </summary>
        /// <returns></returns>
        public List<NosPreparamosBE> Listar()
        {
            try
            {
                List<NosPreparamosBE> items = new List<NosPreparamosBE>();

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_NosPreparamos_LIS]"))
                    {
                        while (reader.Read()) items.Add(Obtener(reader));
                    }
                }

                return items;
                
            }
            catch (Exception ex)
            {
                Log.Error("NosPreparamosRepository:::" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Selecciona un registro.
        /// </summary>
        /// <returns></returns>
        public NosPreparamosBE Seleccionar(int id)
        {
            try
            {
                NosPreparamosBE items = null;

                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = id
                };

                using (AdoHelperSecundario adoHelper = new AdoHelperSecundario())
                {
                    using (SqlDataReader reader = adoHelper.ExecDataReaderProc("[USP_NosPreparamos_SEL]",idParameter))
                    {
                        while (reader.Read()) items = Obtener(reader);
                    }
                }

                return items;

            }
            catch (Exception ex)
            {
                Log.Error("NosPreparamosRepository:::" + ex.Message);

                return null;
            }
        }

        #endregion

    }
}
