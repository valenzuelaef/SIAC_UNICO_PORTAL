using Claro.SIACU.Web.WebApplication.DashboardService;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using System.Linq;

namespace Claro.SIACU.Web.WebApplication.Controllers
{
    public class RedirectController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSessionsParams(string strIdSession, string strOption, string strApplication)
        {
            DashboardService.RedirectSessionResponseDashboard objRedirectSessionResponseDashboard;
            DashboardService.RedirectSessionRequestDashboard objRedirectSessionRequestDashboard = new DashboardService.RedirectSessionRequestDashboard()
            {
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession),
                Application = strApplication,
                Option = strOption
            };
            try
            {
                objRedirectSessionResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.RedirectSessionResponseDashboard>(
                    objRedirectSessionRequestDashboard.audit.Session,
                    objRedirectSessionRequestDashboard.audit.transaction,
                    () => { return new DashboardService.DashboardServiceClient().GetRedirectSession(objRedirectSessionRequestDashboard); });
            }
            catch (Exception ex)
            {
                objRedirectSessionResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objRedirectSessionRequestDashboard.audit.transaction, ex.Message);
            }
            if (objRedirectSessionResponseDashboard != null)
            {
                if (objRedirectSessionResponseDashboard.CodeError == "0")
                {
                    if (objRedirectSessionResponseDashboard.listRedirect.Count == 0)
                    {
                        Claro.Web.Logging.Error(strIdSession, objRedirectSessionRequestDashboard.audit.transaction, objRedirectSessionResponseDashboard.ErrorMsg);
                        throw new Claro.MessageException(objRedirectSessionResponseDashboard.ErrorMsg);
                    }
                }
                else
                {
                    Claro.Web.Logging.Error(strIdSession, objRedirectSessionRequestDashboard.audit.transaction, objRedirectSessionResponseDashboard.ErrorMsg);
                    throw new Claro.MessageException(objRedirectSessionResponseDashboard.ErrorMsg);
                }
            }
            return Json(objRedirectSessionResponseDashboard.listRedirect);
        }

        public JsonResult RedirectApp(string strIdSession, string strAppDest, string strOption, string strData)
        {
            string[] response = new string[2];
            DashboardService.InsertRedirectComResponseDashboard objInsertRedirectComResponseDashboard;
            DashboardService.InsertRedirectComRequestDashboard objInsertRedirectComRequestDashboard = new InsertRedirectComRequestDashboard()
            {
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession),
                Option = strOption,
                IpClient = App_Code.Common.GetClientIP(),
                IpServer = App_Code.Common.GetApplicationIp(),
                JsonParameters = strData,
                AppDest = strAppDest
            };

            try
            {
                objInsertRedirectComResponseDashboard = new DashboardService.DashboardServiceClient().InsertRedirectCommunication(objInsertRedirectComRequestDashboard);
            }
            catch (Exception ex)
            {
                objInsertRedirectComResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, ex.Message);
            }
            if (objInsertRedirectComResponseDashboard.ResultRegCommunication == "ok")
            {
                Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL: " + objInsertRedirectComResponseDashboard.Url);
                if (KEY.AppSettings("strFlagPiloto") == Claro.Constants.NumberOneString)
                {
                    string serverPiloto = string.Empty;
                    string urlRedirect = objInsertRedirectComResponseDashboard.Url;

                    string UrlPiloto =
                          (
                              urlRedirect.IndexOf(KEY.AppSettings("strNodoBalanceadorSiacu")) >= 0 ? KEY.AppSettings("strNodoPilotoSiacu18") :
                              urlRedirect.IndexOf(KEY.AppSettings("strNodoBalanceadorSiapost")) >= 0 ? KEY.AppSettings("strNodoPilotoSiacpost22") :
                              urlRedirect.IndexOf(KEY.AppSettings("strNodoBalanceadorSiacRec")) >= 0 ? KEY.AppSettings("strNodoPilotoSiacRec03") :
                              urlRedirect.IndexOf(KEY.AppSettings("strNodoBalanceadorSistec")) >= 0 ? KEY.AppSettings("strNodoPilotoSistec04") : KEY.AppSettings("strNodoPiloto")
                          );

                    if (urlRedirect.Contains("SIOP_DEAU"))
                    {
                        UrlPiloto = @"http://intranetsiop/";
                    }

                    Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL urlRedirect: " + urlRedirect);

                    string urlRedirectPiloto =
                         (
                             urlRedirect.IndexOf(KEY.AppSettings("strDirectorioBalanceadorSiacu")) >= 0 ? urlRedirect.Replace(KEY.AppSettings("strDirectorioBalanceadorSiacu"), KEY.AppSettings("strDirectorioPilotoSiacu")) :
                             urlRedirect.IndexOf(KEY.AppSettings("strDirectorioBalanceadorSiapost")) >= 0 ? urlRedirect.Replace(KEY.AppSettings("strDirectorioBalanceadorSiapost"), KEY.AppSettings("strDirectorioPilotoSiacpost")) :
                             urlRedirect.IndexOf(KEY.AppSettings("strDirectorioBalanceadorSiacRec")) >= 0 ? urlRedirect.Replace(KEY.AppSettings("strDirectorioBalanceadorSiacRec"), KEY.AppSettings("strDirectorioPilotoSiacRec")) :
                             urlRedirect.IndexOf(KEY.AppSettings("strDirectorioBalanceadorSistec")) >= 0 ? urlRedirect.Replace(KEY.AppSettings("strDirectorioBalanceadorSistec"), KEY.AppSettings("strDirectorioPilotoSistec")) : urlRedirect
                         );

                    Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL urlRedirect: " + urlRedirect);

                    serverPiloto = UrlPiloto + urlRedirectPiloto.Substring(urlRedirectPiloto.IndexOf("/", urlRedirectPiloto.IndexOf("/", urlRedirectPiloto.IndexOf("/") + Claro.Constants.NumberOne) + Claro.Constants.NumberOne) + Claro.Constants.NumberOne);

                    Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL urlRedirectPiloto : " + urlRedirectPiloto);

                    //serverPiloto = UrlPiloto + urlRedirect.Substring(urlRedirect.IndexOf("/", urlRedirect.IndexOf("/", urlRedirect.IndexOf("/") + Claro.Constants.NumberOne) + Claro.Constants.NumberOne) + Claro.Constants.NumberOne);
                    response[0] = serverPiloto;
                    response[1] = objInsertRedirectComResponseDashboard.Sequence;
                    Claro.Web.Logging.Info(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, "URL Piloto: " + serverPiloto);
                }
                else
                {
                    response[0] = objInsertRedirectComResponseDashboard.Url;
                    response[1] = objInsertRedirectComResponseDashboard.Sequence;
                }
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, objInsertRedirectComRequestDashboard.audit.transaction, objInsertRedirectComResponseDashboard.ResultRegCommunication);
                throw new Claro.MessageException(objInsertRedirectComResponseDashboard.ResultRegCommunication);
            }
            return Json(response);
        }

        public JsonResult ValidateRedirect(string strIdSession, string sequence)
        {
            string[] response = new string[3];
            DashboardService.ValidateRedirectComResponseDashboard objValidateRedirectComResponseDashboard;
            DashboardService.ValidateRedirectComRequestDashboard objValidateRedirectComRequestDashboard = new ValidateRedirectComRequestDashboard()
            {
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession),
                Sequence = sequence,
                Server = App_Code.Common.GetApplicationIp()
            };

            try
            {
                objValidateRedirectComResponseDashboard = new DashboardService.DashboardServiceClient().ValidateRedirectCommunication(objValidateRedirectComRequestDashboard);
            }
            catch (Exception ex)
            {
                objValidateRedirectComResponseDashboard = null;
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, ex.Message);
            }
            if (objValidateRedirectComResponseDashboard.ResultValCommunication)
            {
                response[0] = objValidateRedirectComResponseDashboard.UrlDest;
                response[1] = objValidateRedirectComResponseDashboard.Availability;
                response[2] = objValidateRedirectComResponseDashboard.JsonParameters;
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, objValidateRedirectComResponseDashboard.ErrorMsg);
                throw new Claro.MessageException(objValidateRedirectComResponseDashboard.ErrorMsg);
            }
            return Json(response);
        }

        public Dictionary<string, string> serverVariables(string strIdSession, string strIdTransaction)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            try
            {
                variables.Add(Claro.SIACU.Constants.AppUser, (string)HttpContext.Session["lgn"]);
                variables.Add(Claro.SIACU.Constants.IpClient, App_Code.Common.GetClientIP());
                variables.Add(Claro.SIACU.Constants.IpServer, App_Code.Common.GetApplicationIp());
                variables.Add(Claro.SIACU.Constants.strNameAplication, KEY.AppSettings("NombreAplicacion"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            return variables;
        }
    }
}