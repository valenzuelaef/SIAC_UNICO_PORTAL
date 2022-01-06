using Claro.Data;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;

namespace Claro.SIACU.Data.Management
{
    public static class Common
    {

        /// <summary>
        /// Método que inserta las notificaciones. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="item">Objeto Instant</param>
        /// <param name="rMsgText">Mensaje</param>
        /// <returns>Devuelve valor booleano y mensaje.</returns>
        public static bool InsertInstant(string strIdSession, string strTransaction, Instant item, out string rMsgText)
        {
            DbParameter[] parameters = new DbParameter[] {
                                                   new DbParameter("P_TELEFONO", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("P_CUENTA", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("P_CONTRATO", DbType.String,20,ParameterDirection.Input), 
                                                   new DbParameter("P_TIPO_TELEFONO", DbType.String,6,ParameterDirection.Input),
												   new DbParameter("P_DESCRIPCION", DbType.String,3500,ParameterDirection.Input),
												   new DbParameter("P_FECHA_VIGENCIA_INI", DbType.DateTime,ParameterDirection.Input),
												   new DbParameter("P_FECHA_VIGENCIA_FIN", DbType.DateTime,ParameterDirection.Input),												   
												   new DbParameter("P_LOGIN", DbType.String,20,ParameterDirection.Input),													   
												   new DbParameter("P_COLOR", DbType.String,255,ParameterDirection.Input),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
											       
											                            };

            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;


            parameters[0].Value = item.DATO;
            parameters[1].Value = item.CUENTA;
            parameters[3].Value = item.TIPOTELEFONO;
            parameters[4].Value = item.DESCRIPCION;
            parameters[5].Value = item.FECHA_VIGENCIA_INICIO;
            parameters[6].Value = item.FECHA_VIGENCIA_FIN;
            parameters[7].Value = item.LOGIN;
            parameters[8].Value = item.COLOR;

            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_INSERTAR_INSTANTANEA_NEW_HFC, parameters);


                salida = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {


                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return salida;
        }

        /// <summary>
        /// Método que actualiza las notificaciones.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="item">Objeto Instant</param>
        /// <param name="rMsgText">Mensaje</param>
        /// <returns>Devuelve valor booleano y mensaje.</returns>
        public static bool UpdateInstant(string strIdSession, string strTransaction, Instant item, out string rMsgText)
        {

            DbParameter[] parameters = new DbParameter[] {
                                                    new DbParameter("P_INSTANTANEA_ID", DbType.Int32,ParameterDirection.Input), 
												   new DbParameter("P_DESCRIPCION", DbType.String,3500,ParameterDirection.Input),
												   new DbParameter("P_FECHA_VIGENCIA_INI", DbType.DateTime,ParameterDirection.Input),
												   new DbParameter("P_FECHA_VIGENCIA_FIN", DbType.DateTime,ParameterDirection.Input),
												   new DbParameter("P_LOGIN", DbType.String,20,ParameterDirection.Input),
                                                   new DbParameter("P_COLOR", DbType.String,20,ParameterDirection.Input),                                                               
												   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
											   };

            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            parameters[0].Value = item.ID_INSTANTANEA;
            parameters[1].Value = item.DESCRIPCION;
            parameters[2].Value = item.FECHA_VIGENCIA_INICIO;
            parameters[3].Value = item.FECHA_VIGENCIA_FIN;
            parameters[4].Value = item.LOGIN;
            parameters[5].Value = item.COLOR;


            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_ACTUALIZAR_INSTANTANEA_NEW, parameters);

                salida = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {

                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return salida;

        }

        /// <summary>
        /// Método que desactiva las notificaciones.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="item">Objeto Instant</param>
        /// <param name="rMsgText">Mensaje</param>
        /// <returns>Devuelve valor booleano y mensaje.</returns>
        public static bool DeactivateInstant(string strIdSession, string strTransaction, Instant item, out string rMsgText)
        {
            DbParameter[] parameters = new DbParameter[] {
												   new DbParameter("P_INSTANTANEA_ID", DbType.Int32,ParameterDirection.Input), 												   
												   new DbParameter("P_LOGIN", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
											   };
            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            parameters[0].Value = item.ID_INSTANTANEA;
            parameters[1].Value = item.LOGIN;
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_ELIMINAR_INSTANTANEA, parameters);

                salida = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {

                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return salida;
        }

        /// <summary>
        /// Método para obtener las instantáneas.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="vDato">Dato</param>
        /// <param name="vTipoTelefono">Tipo de teléfono</param>
        /// <param name="fecha">Fecha</param>
        /// <returns>Devuelve listado de instantáneas.</returns>
        public static List<Instant> ListInstant(string strIdSession, string strTransaction, string vDato, string vTipoTelefono, string fecha)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_DATO_BUSQUEDA", DbType.String,255, ParameterDirection.Input, vDato),
                new DbParameter("P_TIPO_TELEFONO", DbType.String,255, ParameterDirection.Input, vTipoTelefono),
                 new DbParameter("P_FECHA_PROCESO", DbType.String, ParameterDirection.Input, fecha),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Instant>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_INSTANTANEAS, parameters);

        }

        /// <summary>
        /// Método para obtener instantáneas por id.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="idInstant">Id de instantánea</param>
        /// <returns>Devuelve objeto Instant con información de instantáneas.</returns>
        public static Instant GetinstantById(string strIdSession, string strTransaction, int idInstant)
        {
            DbParameter[] parameters = new DbParameter[] {
                                                    new DbParameter("P_INSTANTANEA_ID", DbType.Int32,ParameterDirection.Input,idInstant), 
												    new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)};


            return DbFactory.ExecuteReader<Entity.Dashboard.Instant>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_INSTANTANEA_BY_ID, parameters);

        }

        /// <summary>
        /// Método para obtener banner.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="vFecha">Fecha</param>
        /// <param name="vEstado">Estado</param>
        /// <param name="vTipoTelefono">Tipo de teléfono</param>
        /// <returns>Devuelve listado de objetos Banner con información de banners.</returns>
        public static List<Entity.Management.Banner> GetBanner(string strIdSession, string strTransaction, DateTime vFecha, string vEstado, string vTipoTelefono)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_FECHA_PROCESO", DbType.DateTime, ParameterDirection.Input, vFecha),
                new DbParameter("P_ESTADO", DbType.String,1, ParameterDirection.Input, vEstado),
                new DbParameter("P_TIPO_TELEFONO", DbType.String,3, ParameterDirection.Input, vTipoTelefono),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
             };
            Entity.Management.Banner objBanner = null;
            List<Entity.Management.Banner> ListBanner = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_BANNER, parameters, (IDataReader reader) =>
            {
                ListBanner = new List<Entity.Management.Banner>();

                while (reader.Read())
                {
                    objBanner = new Entity.Management.Banner();

                    objBanner.ID_BANNER = Convert.ToInt(reader["ID_BANNER"]);
                    if (objBanner.ID_BANNER != 0)
                    {
                        objBanner.MENSAJE = Convert.ToString(reader["MENSAJE"]);
                        objBanner.ORDEN_PRIORIDAD = Convert.ToInt(reader["ORDEN_PRIORIDAD"]);
                        objBanner.FECHA_VIGENCIA_INICIO = Convert.ToDate(reader["FECHA_VIGENCIA_INICIO"]);
                        objBanner.FECHA_VIGENCIA_FIN = Convert.ToDate(reader["FECHA_VIGENCIA_FIN"]);
                        objBanner.ESTADO = Convert.ToString(reader["ESTADO"]);
                        objBanner.LOGIN_REGISTRO = Convert.ToString(reader["LOGIN_REGISTRO"]);
                        objBanner.LOGIN_MODIFICACION = Convert.ToString(reader["LOGIN_MODIFICACION"]);
                        ListBanner.Add(objBanner);
                    }
                }
            });

            return ListBanner;
        }

        public static bool GetCreate(string strIdSession, string strTransaction, int idBanner, string mensaje, DateTime fechaVigenciaInicio, DateTime fechaVigenciaFin, string login, int ordenPrioridad, string tipoTelefono)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_BANNER", DbType.Int32,ParameterDirection.Input, idBanner),
				new DbParameter("P_MENSAJE", DbType.String,300,ParameterDirection.Input, mensaje),
				new DbParameter("P_FECHA_VIGENCIA_INICIO", DbType.DateTime,ParameterDirection.Input, fechaVigenciaInicio),
				new DbParameter("P_FECHA_VIGENCIA_FIN", DbType.DateTime,ParameterDirection.Input, fechaVigenciaFin),
				new DbParameter("P_ORDEN_PRIORIDAD", DbType.Int32,ParameterDirection.Input, ordenPrioridad),
				new DbParameter("P_LOGIN", DbType.String,20,ParameterDirection.Input, login),
				new DbParameter("P_TIPO_TELEFONO", DbType.String,20,ParameterDirection.Input, tipoTelefono),
		        new DbParameter("P_FLAG_PROCESO", DbType.String,255,ParameterDirection.Output),
				new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
             };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_INSERTAR_ACTUALIZAR_BANNER, parameters);
            return true;
        }

        public static bool GetModify(string strIdSession, string strTransaction, int idBanner, string mensaje, DateTime fechaVigenciaInicio, DateTime fechaVigenciaFin, string login, int ordenPrioridad, string tipoTelefono)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_BANNER", DbType.Int32,ParameterDirection.Input, idBanner),
				new DbParameter("P_MENSAJE", DbType.String,300,ParameterDirection.Input, mensaje),
				new DbParameter("P_FECHA_VIGENCIA_INICIO", DbType.DateTime,ParameterDirection.Input, fechaVigenciaInicio),
				new DbParameter("P_FECHA_VIGENCIA_FIN", DbType.DateTime,ParameterDirection.Input, fechaVigenciaFin),
				new DbParameter("P_ORDEN_PRIORIDAD", DbType.Int32,ParameterDirection.Input, ordenPrioridad),
				new DbParameter("P_LOGIN", DbType.String,20,ParameterDirection.Input, login),
				new DbParameter("P_TIPO_TELEFONO", DbType.String,20,ParameterDirection.Input, tipoTelefono),
		        new DbParameter("P_FLAG_PROCESO", DbType.String,255,ParameterDirection.Output),
				new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
             };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_INSERTAR_ACTUALIZAR_BANNER, parameters);
            return true;
        }

        public static Entity.Management.Banner GetBannerId(string strIdSession, string strTransaction, int idBanner)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_BANNER", DbType.Int32,ParameterDirection.Input, idBanner),
		        new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
             };

            return DbFactory.ExecuteReader<Entity.Management.Banner>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_BANNER_BY_ID, parameters);
        }

        public static bool GetDeactivate(string strIdSession, string strTransaction, int idBanner, string login)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_BANNER", DbType.Int32,ParameterDirection.Input, idBanner),
				new DbParameter("P_LOGIN", DbType.String,20,ParameterDirection.Input, login),
		        new DbParameter("P_FLAG_PROCESO", DbType.String,255,ParameterDirection.Output),
				new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
             };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_DESACTIVAR_BANNER, parameters);
            return true;
        }

        /// <summary>
        /// Metodo que obtiene un número correlativo
        /// </summary>
        /// <param name="iduser">Id Usuario</param>
        /// <returns></returns>
        public static bool GetInstantCorrelative(string strIdSession, string strTransaction, string strLogin, out string rMsgCorrelativeText)
        {
            DbParameter[] parameters = new DbParameter[] {
												   new DbParameter("p_login",DbType.String,20,ParameterDirection.Input), 												   
												   new DbParameter("p_out",   DbType.String,255, ParameterDirection.Output)
											   };
            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            parameters[0].Value = strLogin;
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_CORRELATIVO_INSTANTANEA, parameters);
                salida = true;
            }
            catch (Exception ex) { throw; }
            finally
            {
                rMsgCorrelativeText = parameters[parameters.Length - 1].Value.ToString();
            }
            return salida;
        }


        public static bool SaveInstantMasiveHeader(string strIdSession, string strTransaction, Instant item, out decimal rIdGenerado, out string rHeader)
        {
            DbParameter[] parameters = new DbParameter[] {
                                                   new DbParameter("p_usuario_registro", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("p_nombre_archivo_instantanea",DbType.String,60,ParameterDirection.Input), 
                                                   new DbParameter("p_nombre_archivo_descripcion",DbType.String,60,ParameterDirection.Input), 
												   new DbParameter("p_descripcion",DbType.String,4000,ParameterDirection.Input), 
												   new DbParameter("p_tipo_telefono",DbType.String,20,ParameterDirection.Input), 
												   new DbParameter("p_fecha_vigencia_inicio",DbType.DateTime,ParameterDirection.Input), 
												   new DbParameter("p_fecha_vigencia_fin",DbType.DateTime,ParameterDirection.Input), 
												   new DbParameter("p_id_generado",DbType.Decimal,ParameterDirection.Output), 
												   new DbParameter("p_out",DbType.String,255,ParameterDirection.Output)
											        };
            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            parameters[0].Value = item.LOGIN;
            parameters[1].Value = item.NOMBRE_ARCHIVO_INSTANTANEA;
            parameters[2].Value = item.NOMBRE_ARCHIVO_DESCRIPCION;
            parameters[3].Value = item.DESCRIPCION;
            parameters[4].Value = item.TIPOTELEFONO;
            parameters[5].Value = item.FECHA_VIGENCIA_INICIO;
            parameters[6].Value = item.FECHA_VIGENCIA_FIN;

            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_INSTANTANEAS_CABECERA, parameters);
                salida = true;
            }

            finally
            {
                rIdGenerado = Convert.ToDecimal(parameters[parameters.Length - 2].Value.ToString());
                rHeader = parameters[parameters.Length - 1].Value.ToString();
            }
            return salida;
        }

        public static bool SaveIntantMasiveDetail(string strIdSession, string strTransaction, decimal IdGeneradoHd, string strLogin, Instant item)
        {
            DbParameter[] parameters = new DbParameter[] {
                                                    new DbParameter("p_id_peticion",DbType.Int32,ParameterDirection.Input),
                                                    new DbParameter("p_usuario_registro",DbType.String,20,ParameterDirection.Input),
                                                    new DbParameter("p_telefono",DbType.String,20,ParameterDirection.Input),
                                                    new DbParameter("p_out",DbType.String,20,ParameterDirection.Output)
            };
            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            parameters[0].Value = IdGeneradoHd;
            parameters[1].Value = strLogin;
            parameters[2].Value = item.DATO;

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_INSTANTANEAS_DETALLE, parameters);
            salida = true;

            return salida;
        }
    }
}
