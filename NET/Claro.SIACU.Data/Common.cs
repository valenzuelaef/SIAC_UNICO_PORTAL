using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COLIVING_CONSULTCLAVE = Claro.SIACU.ProxyService.SIAC.Claves;
using KEY = Claro.ConfigurationManager;
namespace Claro.SIACU.Data
{
    public static class Common
    {
        /// <summary>
        /// Método que valida y desencripta los valores User y Pass del regedit.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strKeyValue">Nombre del Regedit Instalado</param>
        /// <param name="ipAplicacion">Ip de Aplicacion</param>
        /// <param name="ipTransicion">Ip de Transición</param>
        /// <param name="usrAplicacion">Nombre de Usuario</param>
        /// <param name="idAplicacion">Id de Aplicacion</param>
        /// <param name="User">Usuario </param>
        /// <param name="Pass">Clave</param>
        /// <returns>Devuelve un booleano => True: Se desencriptó con éxito | False: Sucedió un error</returns>
        public static bool IsOkGetKey(string idSession, string idTransaccion, string strKeyValue, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, out string User, out string Pass)
        {
            try
            {
                const string TIMRootRegistry = @"SOFTWARE\TIM";
                string strKey = strKeyValue;
                string cryptoUser = "";
                string cryptoPass = "";

                using (var root = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64))
                {
                    using (var key = root.OpenSubKey(TIMRootRegistry + @"\" + strKey, false))
                    {
                        cryptoUser = key.GetValue("User").ToString();
                        cryptoPass = key.GetValue("Password").ToString();
                    }
                }

                COLIVING_CONSULTCLAVE.desencriptarResponseBody objdesencriptarResponse = new COLIVING_CONSULTCLAVE.desencriptarResponseBody();
                COLIVING_CONSULTCLAVE.desencriptarRequestBody objdesencriptarRequest = new COLIVING_CONSULTCLAVE.desencriptarRequestBody()
                {
                    idTransaccion = idTransaccion,
                    ipAplicacion = ipAplicacion,
                    ipTransicion = ipTransicion,
                    usrAplicacion = usrAplicacion,
                    idAplicacion = KEY.AppSettings("CodAplicacion"),
                    codigoAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                    usuarioAplicacionEncriptado = cryptoUser,
                    claveEncriptado = cryptoPass,
                };

                objdesencriptarResponse.codigoResultado = Claro.Web.Logging.ExecuteMethod<string>(idSession, idTransaccion, () =>
                {
                    return Configuration.ServiceConfiguration.CONSULTCLAVE.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );
                });

                User = objdesencriptarResponse.usuarioAplicacion;
                Pass = objdesencriptarResponse.clave;
                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar usuarioAplicacion: {0}", objdesencriptarResponse.usuarioAplicacion));
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar clave: {0}", objdesencriptarResponse.clave));
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar codigoResultado: {0}", objdesencriptarResponse.codigoResultado));
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar mensajeResultado: {0}", objdesencriptarResponse.mensajeResultado));
                    return true;
                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, idTransaccion, ex.Message);
                User = "";
                Pass = "";
                return false;
            }

            return false;

        }

        /// Método que valida y desencripta los valores User y Pass de las llaves configurables.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strKeyValueEncrypted">Credenciales Encriptadas(User|Password)</param>
        /// <param name="ipAplicacion">Ip de Aplicacion</param>
        /// <param name="ipTransicion">Ip de Transición</param>
        /// <param name="usrAplicacion">Nombre de Usuario</param>
        /// <param name="idAplicacion">Id de Aplicacion</param>
        /// <param name="User">Usuario </param>
        /// <param name="Pass">Clave</param>
        /// <returns>Devuelve un booleano => True: Se desencriptó con éxito | False: Sucedió un error</returns>
        public static bool IsOkGetKeyEncrypted(string idSession, string idTransaccion, string strKeyValueEncrypted, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, out string User, out string Pass)
        {
            try
            {
                string cryptoUser = strKeyValueEncrypted.Split('|')[0];
                string cryptoPass = strKeyValueEncrypted.Split('|')[1];

                COLIVING_CONSULTCLAVE.desencriptarResponseBody objdesencriptarResponse = new COLIVING_CONSULTCLAVE.desencriptarResponseBody();
                COLIVING_CONSULTCLAVE.desencriptarRequestBody objdesencriptarRequest = new COLIVING_CONSULTCLAVE.desencriptarRequestBody()
                {
                    idTransaccion = idTransaccion,
                    ipAplicacion = ipAplicacion,
                    ipTransicion = ipTransicion,
                    usrAplicacion = usrAplicacion,
                    idAplicacion = KEY.AppSettings("CodAplicacion"),
                    codigoAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                    usuarioAplicacionEncriptado = cryptoUser,
                    claveEncriptado = cryptoPass,
                };

                objdesencriptarResponse.codigoResultado = Claro.Web.Logging.ExecuteMethod<string>(idSession, idTransaccion, () =>
                {
                    return Configuration.ServiceConfiguration.CONSULTCLAVE.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );
                });

                User = objdesencriptarResponse.usuarioAplicacion;
                Pass = objdesencriptarResponse.clave;
                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar usuarioAplicacion: {0}", objdesencriptarResponse.usuarioAplicacion));
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar clave: {0}", objdesencriptarResponse.clave));
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar codigoResultado: {0}", objdesencriptarResponse.codigoResultado));
                    Claro.Web.Logging.Info(idSession, idTransaccion, String.Format("CONSULTA_CLAVES.desencriptar mensajeResultado: {0}", objdesencriptarResponse.mensajeResultado));
                    return true;
                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, idTransaccion, ex.Message);
                User = "";
                Pass = "";
                return false;
            }

            return false;

        }
    }
}
