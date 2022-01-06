using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.App_Code
{
    public static class Common
    {
        public static string GetTransactionID()
        {
            Random rd = new Random(Guid.NewGuid().GetHashCode());
            return DateTime.Now.ToString("yyyyMMddHHMMss") + rd.Next(100000, 999999).ToString();
        }

        public static string GetApplicationIp()
        {
            string[] names = KEY.AppSettings("RequestHeaderIpClient").Split(',');
            return Claro.Web.HttpRequest.UserHostAddress(names);
        }

        public static string GetApplicationIpServer()
        {
            return Convert.ToString(HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"]);
        } 

        public static string GetApplicationName()
        {
            return Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
        }

        public static string GetApplicationCode()
        {
            return KEY.AppSettings("ApplicationCode");
        }

        [Browsable(false)]
        public static string CurrentUser
        {
            get
            {
                string strDomainUser = HttpContext.Current.Request.ServerVariables["LOGON_USER"];
                string strUser = KEY.AppSettings("TestUser", "");

                if (string.IsNullOrEmpty(strUser))
                {
                    strUser = strDomainUser.Substring(strDomainUser.IndexOf("\\", System.StringComparison.Ordinal) + 1);
                }

                return strUser.ToUpperInvariant();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static string GetPhoneNumber(string strPhone)
        {
            return ((strPhone.Substring(0, 2) == KEY.AppSettings("ConstKeyCodigoInternacional") ? strPhone : KEY.AppSettings("ConstKeyCodigoInternacional") + strPhone.Trim()));
        }

        /// <summary>
        /// Metodo que hace una invocacion al registro de auditoria
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strClientIP"></param>
        /// <param name="strClientName"></param>
        /// <param name="strText"></param>
        /// <param name="objAuditRequest"></param>
        /// <returns></returns>
        public static string InsertAudit(SecurityAudit.AuditRequest audit, string strPhone, string strTransaction, string strText)
        {
            string strResponse = "";

            string strUserName = CurrentUser;
            string strAmount = Claro.Constants.NumberZeroString;
            string strService = KEY.AppSettings("gConstEvtServicio");
            string strServerIP = GetApplicationIp();
            string strServerName = GetApplicationName();

            string strClientIP = GetClientIP();
            string strClientName = GetClientName();

            SecurityAudit.AuditClient objAuditClient = new SecurityAudit.AuditClient();
            SecurityAudit.RegisterRequest objRegisterRequest = new SecurityAudit.RegisterRequest()
            {
                audit = audit,
                userName = strUserName,
                amount = strAmount,
                service = strService,
                phone = strPhone,
                text = strText,
                transaction = strTransaction,
                clientIP = strClientIP,
                clientName = strClientName,
                serverIP = strServerIP,
                serverName = strServerName
            };

            try
            {
                objRegisterRequest.audit = audit;
                Claro.Web.Logging.ExecuteMethod(   
                    objRegisterRequest.audit .Session ,
                    objRegisterRequest.audit .transaction ,
                    () => { objAuditClient.Register(objRegisterRequest); });  
                strResponse = Claro.SIACU.Constants.RegisterAuditFor + strPhone;
            }
            catch (Exception ex)
            {
                strResponse = ex.Message.ToString();
                Claro.Web.Logging.Error(objRegisterRequest.audit.Session, objRegisterRequest.audit.transaction, ex.Message);
            }

            return strResponse;
        }

        /// <summary>
        /// Metodo que hace una invocacion al registro de auditoria
        /// </summary>
        /// <param name="strPhone"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strClientIP"></param>
        /// <param name="strClientName"></param>
        /// <param name="strText"></param>
        /// <param name="objAuditRequest"></param>
        /// <returns></returns>
        public static string InsertAudit(SecurityAudit.AuditRequest audit, string strPhone, string strTransaction, string strText, string strService)
        {
            string strResponse = "";

            string strUserName = CurrentUser;
            string strAmount = Claro.Constants.NumberZeroString;
            string strServerIP = GetApplicationIp();
            string strServerName = GetApplicationName();

            string strClientIP = GetClientIP();
            string strClientName = GetClientName();

            SecurityAudit.AuditClient objAuditClient = new SecurityAudit.AuditClient();
            SecurityAudit.RegisterRequest objRegisterRequest = new SecurityAudit.RegisterRequest()
            {
                audit = audit,
                userName = strUserName,
                amount = strAmount,
                service = strService,
                phone = strPhone,
                text = strText,
                transaction = strTransaction,
                clientIP = strClientIP,
                clientName = strClientName,
                serverIP = strServerIP,
                serverName = strServerName
            };

            try
            {
                objRegisterRequest.audit = audit;
                Claro.Web.Logging.ExecuteMethod(
                    objRegisterRequest.audit.Session,
                    objRegisterRequest.audit.transaction,
                    () => { objAuditClient.Register(objRegisterRequest); });
                strResponse = Claro.SIACU.Constants.RegisterAuditFor + strPhone;
            }
            catch (Exception ex)
            {
                strResponse = ex.Message.ToString();
                Claro.Web.Logging.Error(objRegisterRequest.audit.Session, objRegisterRequest.audit.transaction, ex.Message);
            }

            return strResponse;
        }

        public static TAudit CreateAuditRequest<TAudit>(string strIdSession)
        {
            TAudit audit = Activator.CreateInstance<TAudit>();
            foreach (PropertyInfo propertyInfo in audit.GetType().GetProperties())
            {
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.Transaction)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetTransactionID());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.ApplicationName)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetApplicationName());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.IpAddress)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetApplicationIp());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.UserName)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.CurrentUser);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Claro.SIACU.Constants.Session)
                {
                    propertyInfo.SetValue(audit, strIdSession);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.idApplication)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("ApplicationCode"));
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Canal)
                {
                    propertyInfo.SetValue(audit, Tools.Utils.Constants.DAC);
                }
            }
            return audit;
        }

        public static TAudit CreateAuditDpRequest<TAudit>(string modulo, string operation)
        {
            TAudit audit = Activator.CreateInstance<TAudit>();
            foreach (PropertyInfo propertyInfo in audit.GetType().GetProperties())
            {
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Consumer)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("consumer"));
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Country)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("country"));
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Dispositivo)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetApplicationIp());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Language)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("language"));
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Modulo)
                {
                    propertyInfo.SetValue(audit, modulo);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.MsgType)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("msgType"));
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Operation)
                {
                    propertyInfo.SetValue(audit, operation);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Pid)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.GetTransactionID());
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.System)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("system"));
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.Timestamp)
                {
                    propertyInfo.SetValue(audit, DateTime.Now);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.UserId)
                {
                    propertyInfo.SetValue(audit, App_Code.Common.CurrentUser);
                }
                if (propertyInfo.Name.ToString().ToUpperInvariant() == Tools.Utils.Constants.WsIp)
                {
                    propertyInfo.SetValue(audit, KEY.AppSettings("wsIp"));
                }
            }
            return audit;
        }

        public static string GetClientIP()
        {
            string strIpClient = Convert.ToString(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(strIpClient))
            {
                strIpClient = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
            }
            return strIpClient;
        }

        public static string GetClientName()
        {
            return Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
        }

        public static string GetUnlimitedPackageCode(string strPackageCode, string strKey)
        {
            string[] arrPackageCodes = KEY.AppSettings(strKey).Split('|');

            foreach (string packageCode in arrPackageCodes)
            {
                if (packageCode.Split(';')[0] == strPackageCode)
                    return packageCode.Split(';')[1];
            }
            return string.Empty;
        }


        public static String Encrypt(String plainText, String key)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return ByteArrayToString(Encrypt(plainBytes, GetRijndaelManaged(key)));
        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public static RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }
    }
}