using Claro.Helpers;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult DialogDefault()
        {
            return View();
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Popup()
        {
            return View();
        }

        public ActionResult DialogTemplate()
        {
            return View();
        }

        public ActionResult OptionsDefault()
        {
            return View();
        }

        public ActionResult DisplayContent(string strOriginType)
        {

            strOriginType = strOriginType != KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString() ?
                                             strOriginType : KEY.AppSettings("strTipoGeneralFIJA").Split('|')[0].ToString();

            switch (strOriginType)
            {
                case Claro.SIACU.Constants.PostpaidMajuscule:
                case Claro.SIACU.Constants.LTE:
                case Claro.SIACU.Constants.HFC:
                case Claro.SIACU.Constants.DTH:
                case Claro.SIACU.Constants.IFI:
                case Claro.SIACU.Constants.FIJA:
                    return RedirectToAction(Claro.SIACU.Constants.Index, Claro.SIACU.Constants.Postpaid, new { @area = Claro.SIACU.Constants.Dashboard });
                case Claro.SIACU.Constants.PrepaidMajuscule:
                    return RedirectToAction(Claro.SIACU.Constants.Index, Claro.SIACU.Constants.Prepaid, new { @area = Claro.SIACU.Constants.Dashboard });
                default:
                    return null;
            }
        }
        public JsonResult GetValueConfig(string strIdSession, string json)
        {
            JsonResult response = null;
            SecurityService.AuditRequest audit = App_Code.Common.CreateAuditRequest<SecurityService.AuditRequest>(strIdSession);
            try
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                var t = obj.GetType();
                var obj2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json);
                Dictionary<string, string> keys = new Dictionary<string, string>();
                string msj = string.Empty;
                foreach (string item in obj2)
                {
                    try
                    {
                        keys.Add(item, KEY.AppSettings(item).ToString());
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("Error al Obtener Valor Config Key: {0} , Detalle: {1}", item, ex.Message));
                        msj = msj + item + ";";
                    }

                }
                response = Json(new { Config = keys, MsjErrorKey = msj }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("Error al Obtener : {0}", ex.Message));

            }

            return response;
        }

        public ActionResult Logon()
        {

            string strIdSession = App_Code.Common.GetTransactionID();
            SecurityService.LogonResponse oLogonResponse;
            string strOptionType = string.Empty;
            string strServerName = System.Web.HttpContext.Current.Server.MachineName;
            string strNroNodo = string.Empty;

            SecurityService.LogonRequest oLogonRequest = new SecurityService.LogonRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<SecurityService.AuditRequest>(strIdSession),
                applicationId = Int32.Parse(App_Code.Common.GetApplicationCode()),
                userName = App_Code.Common.CurrentUser,
                isMenuSU = true
            };

            try
            {
                strOptionType = KEY.AppSettings("optionType");
                Claro.Web.Logging.Info(strIdSession, oLogonRequest.audit.transaction, string.Format("LOGON_USER: {0}", App_Code.Common.CurrentUser));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oLogonRequest.audit.Session, oLogonRequest.audit.transaction, ex.Message);
            }
            oLogonRequest.optionType = strOptionType;

            try
            {
                oLogonResponse = Claro.Web.Logging.ExecuteMethod<SecurityService.LogonResponse>(
                    oLogonRequest.audit.Session,
                    oLogonRequest.audit.transaction,
                    () => { return new SecurityService.SecurityClient().Logon(oLogonRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, oLogonRequest.audit.transaction, ex.Message);
                oLogonResponse = null;
                throw new Claro.MessageException(oLogonRequest.audit.transaction);
            }
            if (strServerName.Length > 1)
            {
                strNroNodo = strServerName.Substring((strServerName.Length - 2), 2);
            }

            return Json(new
            {
                data = oLogonResponse,
                idSession = oLogonRequest.audit.Session,
                SearchLength = KEY.AppSettings("SearchLength"),
                nroNodo = strNroNodo,
                OptionPermissionsMenu = Json(new
                {
                    TFI = KEY.AppSettings("gConstOpcionesTFI"),
                    TPI = KEY.AppSettings("OpcionesTPI"),
                    Internet = KEY.AppSettings("OpcionesInternet"),
                    Fijo = KEY.AppSettings("OpcionesFijoPost"),
                    HCTNumberOnlyTFI = KEY.AppSettings("OpcionesHCTNumberOnlyTFI")
                }, JsonRequestBehavior.AllowGet)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPortability(string strIdSession, string vTelefono)
        {
            CommonService.PortabilityResponseCommon oPeticion;
            CommonService.PortabilityRequestCommon ORequest = new CommonService.PortabilityRequestCommon()
            {
                Telephone = vTelefono,
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
            };


            try
            {
                oPeticion = Claro.Web.Logging.ExecuteMethod<CommonService.PortabilityResponseCommon>(
                    ORequest.audit.Session,
                    ORequest.audit.transaction,
                    () => { return new CommonService.CommonServiceClient().GetPortability(ORequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, ORequest.audit.transaction, ex.Message);
                oPeticion = null;
            }

            return Json(new { data = oPeticion.ListPortability, respuesta = oPeticion.Respuesta });
        }

        public JsonResult GetOptions(string strIdSession, string strApplicationType, string strUserId)
        {
            SecurityService.OptionsResponse oOptionResponse;
            string strModuleType = "";

            string OPTIONS_PRODUCTO_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[2].ToString();
            strApplicationType = strApplicationType != KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString() ?
                                                       strApplicationType : KEY.AppSettings("strTipoGeneralFIJA").Split('|')[0].ToString();


            switch (strApplicationType)
            {
                case Tools.Utils.Constants.OLO:
                    strModuleType = Tools.Utils.Constants.OLO;
                    break;
                case Claro.SIACU.Constants.PostpaidMajuscule:
                    strModuleType = Claro.SIACU.Constants.PostpaidMajusculeSpanish;
                    break;
                case Claro.SIACU.Constants.PrepaidMajuscule:
                    strModuleType = Claro.SIACU.Constants.PrepaidMajusculeSpanish;
                    break;
                case Claro.SIACU.Constants.DTH:
                case Claro.SIACU.Constants.HFC:
                case Claro.SIACU.Constants.LTE:
                case Claro.SIACU.Constants.IFI:
                    strModuleType = strApplicationType;
                    break;
                case Claro.SIACU.Constants.FIJA:
                    strModuleType = OPTIONS_PRODUCTO_FIXE;
                    break;
            }

            SecurityService.OptionsRequest oOptionRequest = new SecurityService.OptionsRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<SecurityService.AuditRequest>(strIdSession),
                module = strModuleType,
                applicationId = Int32.Parse(App_Code.Common.GetApplicationCode()),
                userId = Int32.Parse(strUserId)
            };

            try
            {
                oOptionResponse = Claro.Web.Logging.ExecuteMethod<SecurityService.OptionsResponse>(
                    oOptionRequest.audit.Session,
                    oOptionRequest.audit.transaction,
                    () => { return new SecurityService.SecurityClient().GetOptions(oOptionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, oOptionRequest.audit.transaction, ex.Message);
                oOptionResponse = null;
            }

            return Json(new { data = oOptionResponse });
        }

        public JsonResult ValidateQuery(string strIdSession, string strSearchType, string strSearchValue, bool NotEvalState, bool FlagSearchType, string userId, bool IsPrepaid, bool IsPostPaid)
        {
            DashboardService.CustomerResponseDashboard objCustomerResponseDashboard;
            DashboardService.CustomerPostPaid objCustomerPost = null;
            DashboardService.CustomerPrePaid objCustomerPre = null;
            DashboardService.CustomerRequestDashboard objCustomerRequestDashboard = new DashboardService.CustomerRequestDashboard()
            {
                TypeSearch = strSearchType,
                ValueSearch = strSearchValue,
                ApplicationType = "",
                NotEvalState = NotEvalState,
                FlagSearchType = FlagSearchType,
                UserId = string.IsNullOrEmpty(userId) ? 0 : int.Parse(userId),
                IsPrepaid = IsPrepaid,
                IsPostPaid = IsPostPaid,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
            };
            try
            {
                objCustomerResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerResponseDashboard>(
                    objCustomerRequestDashboard.audit.Session,
                    objCustomerRequestDashboard.audit.transaction,
                    () => { return new DashboardService.DashboardServiceClient().GetCustomer(objCustomerRequestDashboard); });

            }
            catch (Exception ex)
            {
                objCustomerResponseDashboard = null;
                    Claro.Web.Logging.Error(strIdSession, objCustomerRequestDashboard.audit.transaction, ex.Message);
                throw new Claro.MessageException(strIdSession);
            }

            if (objCustomerResponseDashboard.objOptions != null)
            {
                string strTraCod = string.Empty;
                string strTypeSearch = string.Empty;
                string strAccount = string.Empty;
                string strText = string.Empty;

                switch (strSearchType)
                {
                    case Claro.Constants.NumberOneString:// Telefono  
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaTelefono");
                        strTypeSearch = Claro.SIACU.Constants.TelephoneNumber;
                        break;
                    case Claro.Constants.NumberTwoString: //Cuenta  
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaCuenta");
                        strTypeSearch = Claro.SIACU.Constants.Account;
                        strAccount = strSearchValue;
                        strSearchValue = "";
                        break;
                    case Claro.Constants.NumberThreeString: //Contrato   
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaContrato");
                        strTypeSearch = Claro.SIACU.Constants.Contract;
                        break;
                    case Claro.Constants.NumberFourString: //CustomerID  
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaCustomerID");
                        strTypeSearch = Claro.SIACU.Constants.CustomerID;
                        break;

                    case Claro.Constants.NumberEightString: // Nro Recibo
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaNroRecibo");
                        strTypeSearch = Claro.SIACU.Constants.Receipt;
                        break;
                    case Claro.Constants.NumberNineString: // CHIP  
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaICCID");
                        strTypeSearch = Claro.SIACU.Constants.ICCID;
                        break;
                    case Claro.Constants.NumberTenString:  //Cintillo
                        strTraCod = KEY.AppSettings("strAudiTraCodBusquedaCintillo");
                        strTypeSearch = Claro.SIACU.Constants.Cintillo;
                        break;
                }

                SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
                try
                {
                    if (strSearchType == Claro.Constants.NumberTwoString)
                    {
                        strText = strTypeSearch + Claro.SIACU.Constants.DoubleScript + strAccount;
                    }
                    else
                    {
                        strText = strTypeSearch + Claro.SIACU.Constants.DoubleScript + strSearchValue;
                    }

                    Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequestDashboard.audit.Session,
                    objCustomerRequestDashboard.audit.transaction, () => { return App_Code.Common.InsertAudit(objaudit, strSearchValue, strTraCod, strText); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                }
            }

            if (objCustomerResponseDashboard != null && !objCustomerResponseDashboard.ApplicationType.Equals("NOPRECISADO"))
            {
                switch (objCustomerResponseDashboard.ApplicationType)
                {
                    case Claro.SIACU.Constants.PrepaidMajuscule:
                        objCustomerPre = objCustomerResponseDashboard.InterfaceCustomer == null ? null : (DashboardService.CustomerPrePaid)objCustomerResponseDashboard.InterfaceCustomer;
                        if (objCustomerPre != null) objCustomerPre.TipoProducto = objCustomerResponseDashboard.ApplicationType;
                        return Json(new { OriginType = objCustomerResponseDashboard.ApplicationType, data = new PrepaidController().DataCustomerModel(objCustomerPre, strSearchValue), strCodeResponseService = objCustomerResponseDashboard.CodeResponse, strMsjValidation = objCustomerResponseDashboard.MsjValidation, Options = objCustomerResponseDashboard.objOptions });
                    default:
                        objCustomerPost = objCustomerResponseDashboard.InterfaceCustomer == null ? null : (DashboardService.CustomerPostPaid)objCustomerResponseDashboard.InterfaceCustomer;
                        if (objCustomerPost != null)
                        {
                            objCustomerPost.itm = objCustomerResponseDashboard.itm;
                            return Json(new { OriginType = objCustomerResponseDashboard.ApplicationType, data = new PostpaidController().DataCustomerModel(objCustomerPost, strSearchValue, objCustomerPost.Application), Igv = objCustomerResponseDashboard.ListIndicatorIGV, strMsjValidation = objCustomerResponseDashboard.MsjValidation, Options = objCustomerResponseDashboard.objOptions });
                        }
                        else
                        {
                            return Json(new { OriginType = objCustomerResponseDashboard.ApplicationType, data = new PostpaidController().DataCustomerModel(null, "", ""), Igv = objCustomerResponseDashboard.ListIndicatorIGV, strMsjValidation = objCustomerResponseDashboard.MsjValidation, Options = objCustomerResponseDashboard.objOptions });
                        }
                }
            }
            else
            {
                return Json(new { OriginType = "", data = new PostpaidController().DataCustomerModel(null, "", ""), strMsjValidation = objCustomerResponseDashboard.MsjValidation, Options = objCustomerResponseDashboard.objOptions });
            }
        }

        /// <summary>
        /// Método para validar que el número a buscar este en proceso de migración
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strType"></param>
        /// <param name="strValue"></param>
        /// <returns> false si no esta en proceso y true si lo esta </returns>
        public JsonResult ValidateMigration(string strIdSession, string strType, string strValue)
        {
            Boolean status = false;
            string message = string.Empty;
            ColivingService.AuditRequest audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession);
            ColivingService.ConsultaLineaResponse objConsultaLineaResponse = new ColivingService.ConsultaLineaResponse();
            try
            {
                ColivingService.ColivingServiceClient objCliente = new ColivingService.ColivingServiceClient();
                string number = string.Empty;
                // Si es ICCID o RECIBO se obtiene el número y se busca en la pivot si no lo encuentra es tobe o ya esta migrado
                if (strType.Equals(Tools.Utils.Constants.NumberNineString) || strType.Equals(Tools.Utils.Constants.NumberEightString))
                {
                    number = objCliente.GetAccountTelephoneCustomer(audit.Session, audit.transaction, strValue, strType);
                    if (!string.IsNullOrEmpty(number))
                    {
                       string type = Tools.Utils.Constants.NumberOneString;
                        if (strType.Equals(Tools.Utils.Constants.NumberEightString))
                        {
                            type = Tools.Utils.Constants.NumberTwoString;
                        }
                        objConsultaLineaResponse = ConsultarPivot(strIdSession, type, number);
                    }
                }
                else // Para los otros tipos de búsqueda consulta a la pivot
                {
                    objConsultaLineaResponse = ConsultarPivot(strIdSession, strType, strValue);
                }
                if (objConsultaLineaResponse!= null && objConsultaLineaResponse.itm != null && objConsultaLineaResponse.itm.estado == KEY.AppSettings("strKeyEstadoMigracion"))
                {
                    status = true;
                    message = KEY.AppSettings("strKeyMensajeEstadoMigracion");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

            return Json(new { estado = status, mensaje = message });

        }

        private ColivingService.ConsultaLineaResponse ConsultarPivot(string strIdSession, string strType, string strValue)
        {

            ColivingService.ColivingServiceClient objCliente = new ColivingService.ColivingServiceClient();
            ColivingService.ConsultaLineaResponse objConsultaLineaResponse = null;

            ColivingService.ConsultaLineaRequest objConsultaLineaRequest = new ColivingService.ConsultaLineaRequest()
            {
                Type = strType,
                Value = strValue,
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession)
            };
            try
            {
                objConsultaLineaResponse = new ColivingService.ConsultaLineaResponse();
                objConsultaLineaResponse = Claro.Web.Logging.ExecuteMethod<ColivingService.ConsultaLineaResponse>(
                      objConsultaLineaRequest.audit.Session,
                      objConsultaLineaRequest.audit.transaction,
                      () => { return objCliente.ConsultarLineaCuenta(objConsultaLineaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsultaLineaRequest.audit.Session, objConsultaLineaRequest.audit.transaction, ex.Message);
            }
            return objConsultaLineaResponse;
        }


    }
}