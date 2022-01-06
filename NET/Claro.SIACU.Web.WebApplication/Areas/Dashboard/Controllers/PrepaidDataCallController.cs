using Claro.SIACU.Web.WebApplication.CommonService;
using Claro.SIACU.Web.WebApplication.PrepaidService;
using Claro.SIACU.Web.WebApplication.DashboardService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HELPER_CALL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataCallHelper;
using KEY = Claro.ConfigurationManager;
using MODELS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;
using System.DirectoryServices;
using System.Text;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PrepaidDataCallController : Controller
    {
        public ActionResult VisualizeCall(string strIdSession, string strDateFrom, string strDateTo, string TrafficType)
        {
            List<MODELS.PrepaidDataCall.GenericBagModel> listGenericBagModel = null;
            CommonService.GroupBagListResponseCommon objGroupBagListResponse;
            CommonService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            try
            {
                CommonService.GroupBagLisRequestCommon objRequest = new CommonService.GroupBagLisRequestCommon() { audit = objAuditRequest };
                objGroupBagListResponse = Claro.Web.Logging.ExecuteMethod<CommonService.GroupBagListResponseCommon>(() =>
                {
                    return new CommonService.CommonServiceClient().GetGroupBagList(objRequest);
                });
            }
            catch (Exception ex)
            {
                objGroupBagListResponse = null;
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                throw new Claro.MessageException(objAuditRequest.transaction);
            }

            if (objGroupBagListResponse != null)
            {
                listGenericBagModel = new List<MODELS.PrepaidDataCall.GenericBagModel>();
                foreach (ListItem item in objGroupBagListResponse.ListItem)
                {
                    listGenericBagModel.Add(new MODELS.PrepaidDataCall.GenericBagModel()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }
            MODELS.PrepaidDataCall.VisualizeCallModel objVisualizeCallModel = new MODELS.PrepaidDataCall.VisualizeCallModel()
            {
                listGenericBagModel = listGenericBagModel,
                DateFrom = strDateFrom,
                DateTo = strDateTo,
                TrafficType = TrafficType
            };
            return View(objVisualizeCallModel);
        }
        private bool IsAuthenticated(string strIdSession, string user, string pass)
        {
            PrepaidService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
            string strDominio = ConfigurationManager.AppSettings("DominioLDAP");
            System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(strDominio, user, pass);
            try
            {
                var obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + user + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult resul = search.FindOne();

                if (resul == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, "Metodo IsAuthenticated Error: " + ex.Message);
                return false;
            }
        }
        public JsonResult CheckingUser(string strIdSession, string user, string pass, string option)
        {
            DashboardService.CheckingUserResponse objCheckingUserResponse = null;
            DashboardService.ReadOptionsByUserResponse objReadOptionsByUserResponse = null;
            DashboardService.EmployeeResponse objEmployeeResponse = null;
            DashboardService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            bool result = false;
            int responseCode = 0;
            string UserValidator = "";
            string usu = "";
            try
            {

                result = IsAuthenticated(strIdSession, user, pass);
                if (result == true)
                {
                    result = false;
                    DashboardService.CheckingUserRequest objCheckingUserRequest = new DashboardService.CheckingUserRequest()
                    {
                        audit = objAuditRequest,
                        Usuario = user,
                        AppCod = int.Parse(KEY.AppSettings("ApplicationCode"))
                    };
                    objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.CheckingUserResponse>(() =>
                    {
                        return new DashboardService.DashboardServiceClient().CheckingUser(objCheckingUserRequest);
                    });

                    if (objCheckingUserResponse != null)
                    {

                        if (objCheckingUserResponse.consultasSeguridad != null && (objCheckingUserResponse.consultasSeguridad != null && objCheckingUserResponse.consultasSeguridad.Count > 0))
                        {
                            usu = objCheckingUserResponse.consultasSeguridad[0].USUACCOD;
                        }
                        int IdUser = 0;
                        bool est = int.TryParse(usu, out IdUser);
                        if (!est)
                        {
                            DashboardService.EmployeeRequest objEmployeeRequestEmployee = new DashboardService.EmployeeRequest()
                            {
                                audit = objAuditRequest,
                                UserName = user
                            };

                            objEmployeeResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.EmployeeResponse>(() =>
                            {
                                return new DashboardService.DashboardServiceClient().GetEmployeByUser(objEmployeeRequestEmployee);
                            });
                            if (objEmployeeResponse != null && (objEmployeeResponse.lstEmployee != null && objEmployeeResponse.lstEmployee.Count > 0))
                            {
                                usu = objEmployeeResponse.lstEmployee[0].UserID.ToString();
                            }
                            est = int.TryParse(usu, out IdUser);
                        }
                        if (est)
                        {
                            DashboardService.ReadOptionsByUserRequest objReadOptionsByUserRequest = new DashboardService.ReadOptionsByUserRequest()
                            {
                                audit = objAuditRequest,
                                IdUser = IdUser,
                                IdAplication = int.Parse(ConfigurationManager.AppSettings("ApplicationCode"))
                            };
                            objReadOptionsByUserResponse = Claro.Web.Logging.ExecuteMethod<DashboardService.ReadOptionsByUserResponse>(() =>
                            {
                                return new DashboardService.DashboardServiceClient().ReadOptionsByUser(objReadOptionsByUserRequest);
                            });
                        }
                        StringBuilder strPermission = null;
                        if (objReadOptionsByUserResponse != null && (objReadOptionsByUserResponse.ListOption != null && objReadOptionsByUserResponse.ListOption.Count > 0))
                        {
                            strPermission = new StringBuilder();
                            foreach (DashboardService.PaginaOption item in objReadOptionsByUserResponse.ListOption)
                            {
                                strPermission.Append(item.Clave);
                                strPermission.Append(",");
                            }
                        }
                        if (strPermission != null)
                        {
                            if (strPermission.ToString().IndexOf(ConfigurationManager.AppSettings(option)) != -1)
                            {
                                responseCode = 1;
                                UserValidator = user;
                            }
                            else
                            {
                                responseCode = 2;
                                UserValidator = "";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                objCheckingUserResponse = null;
                objReadOptionsByUserResponse = null;
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                responseCode = 3;
               
            }
            return Json(new { UserValidator = UserValidator, result = responseCode });
        }

        public JsonResult GetTraffictType(string strIdSession)
        {
            List<MODELS.PrepaidDataCall.GenericBagModel> listGenericBagModel = null;
            CommonService.GroupBagListResponseCommon objGroupBagListResponse;
            CommonService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            try
            {
                CommonService.GroupBagLisRequestCommon objRequest = new CommonService.GroupBagLisRequestCommon() { audit = objAuditRequest };
                objGroupBagListResponse = Claro.Web.Logging.ExecuteMethod<CommonService.GroupBagListResponseCommon>(() =>
                {
                    return new CommonService.CommonServiceClient().GetGroupBagList(objRequest);
                });
            }
            catch (Exception ex)
            {
                objGroupBagListResponse = null;
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                throw new Claro.MessageException(objAuditRequest.transaction);
            }

            if (objGroupBagListResponse != null)
            {
                listGenericBagModel = new List<MODELS.PrepaidDataCall.GenericBagModel>();
                foreach (ListItem item in objGroupBagListResponse.ListItem)
                {
                    listGenericBagModel.Add(new MODELS.PrepaidDataCall.GenericBagModel()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }
            return Json(new { data = listGenericBagModel });
        }

        public ActionResult InLineCall()
        {
            return PartialView();
        }

        public JsonResult GetEventType(string strIdSession, string strEventType)
        {
            CommonService.EventTypeResponseCommon objEventTypeResponseCommon;
            CommonService.EventTypeRequestCommon objEventTypeRequestCommon = new CommonService.EventTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                EventType = "listaTipoEventoCall"
            };

            try
            {
                objEventTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.EventTypeResponseCommon>(() => { return new CommonService.CommonServiceClient().GetEventType(objEventTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objEventTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objEventTypeRequestCommon.audit.transaction);
            }

            MODELS.PrepaidDataCall.OnlineCallModel objInLineCallModel = new MODELS.PrepaidDataCall.OnlineCallModel();

            if (objEventTypeResponseCommon.EventTypes != null)
            {
                List<HELPER_CALL.InLineHelper.EventType> listEventTypes = new List<HELPER_CALL.InLineHelper.EventType>();

                foreach (CommonService.ListItem item in objEventTypeResponseCommon.EventTypes)
                {
                    listEventTypes.Add(new HELPER_CALL.InLineHelper.EventType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objInLineCallModel.EventTypes = listEventTypes;
            }

            return Json(new { data = objInLineCallModel });
        }

        public JsonResult DetailVisualizeCall(string strIdSession, PrepaidService.CallsRequestPrePaid objCallsRequestPrePaid)
        {
            PrepaidService.CallsResponsePrePaid objCallsResponse;
            try
            {
                objCallsRequestPrePaid.TelephoneTipi = objCallsRequestPrePaid.Msisdn;
                objCallsRequestPrePaid.Msisdn = App_Code.Common.GetPhoneNumber(objCallsRequestPrePaid.Msisdn);
                objCallsRequestPrePaid.TypeQuery = Claro.Constants.NumberOneString;
                objCallsRequestPrePaid.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                objCallsResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.CallsResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetCalls(objCallsRequestPrePaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCallsRequestPrePaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCallsRequestPrePaid.audit.transaction);
            }

            List<HELPER_CALL.DataCall> listDataCallModel = null;

            if (objCallsResponse.listCall != null)
            {
                listDataCallModel = new List<HELPER_CALL.DataCall>();

                foreach (PrepaidService.CallPrePaid objCall in objCallsResponse.listCall)
                {
                    listDataCallModel.Add(new HELPER_CALL.DataCall()
                    {
                        CallDateTime = System.Convert.ToDateTime(objCall.CallDateTime).ToString("dd/MM/yyyy hh:mm tt"),
                        CallTelephoneDestination = objCall.CallTelephoneDestination,
                        CallTypeTraffic = objCall.CallTypeTraffic,
                        CallDuration = objCall.CallDuration,
                        CallUptake = objCall.CallUptake,
                        CallBoughtPresented = objCall.CallBoughtPresented,
                        CallBalance = objCall.CallBalance,
                        CallBagId = objCall.CallBagId,
                        CallDescription = objCall.CallDescription,
                        CallPlan = objCall.CallPlan,
                        CallPromotion = objCall.CallPromotion,
                        CallDestination = objCall.CallDestination,
                        CallOperator = objCall.CallOperator,
                        CallCobroGroup = objCall.CallCobroGroup,
                        CallNetworkType = objCall.CallNetworkType,
                        CallImei = objCall.CallImei,
                        CallRoaming = objCall.CallRoaming,
                        CallTariffArea = objCall.CallTariffArea
                    });
                }
            }
            return Json(new { data = listDataCallModel, headers = ConfigurationManager.AppSettings("strGridVisualizaLlamada") });
        }

        public JsonResult GetInLineCall(string strIdSession, PrepaidService.CallsRequestPrePaid objCallsRequestPrePaid)
        {
            PrepaidService.CallsResponsePrePaid objCallsResponse;

            try
            {
                objCallsRequestPrePaid.Msisdn = App_Code.Common.GetPhoneNumber(objCallsRequestPrePaid.Msisdn);
                objCallsRequestPrePaid.TypeQuery = Claro.Constants.NumberZeroString;
                objCallsRequestPrePaid.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                objCallsResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.CallsResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetCalls(objCallsRequestPrePaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCallsRequestPrePaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCallsRequestPrePaid.audit.transaction);
            }

            MODELS.Prepaid.DataCallModel objDataCallModel = new MODELS.Prepaid.DataCallModel();

            if (objCallsResponse.listCall != null)
            {
                List<HELPER_CALL.DataCall> listDataCall = new List<HELPER_CALL.DataCall>();

                foreach (PrepaidService.CallPrePaid objCall in objCallsResponse.listCall)
                {
                    listDataCall.Add(new HELPER_CALL.DataCall()
                    {
                        CallDateTime = objCall.CallDateTime,
                        CallTelephoneDestination = objCall.CallDestination,
                        CallDuration = objCall.CallDuration,
                        CallStart = objCall.CallStart,
                        CallEnd = objCall.CallEnd,
                        CallCost = objCall.CallCost,
                        CallBalance = objCall.CallBalance,
                        CallBag = objCall.CallBag,
                        CallPlan = objCall.CallPlan,
                        CallEventType = objCall.CallEventType
                    });
                }
                objDataCallModel.listDataCallModel = listDataCall;
            }

            return Json(new { data = objDataCallModel });
        }

        public MODELS.PrepaidDataCall.ExportExcelCallModel GetExportExcelDataCall(string strIdSession, PrepaidService.CallsRequestPrePaid objCallsRequestPrePaid)
        {
            PrepaidService.CallsResponsePrePaid objCallsResponse;
            try
            {
                objCallsRequestPrePaid.Msisdn = App_Code.Common.GetPhoneNumber(objCallsRequestPrePaid.Msisdn);
                objCallsRequestPrePaid.TypeQuery = Claro.Constants.NumberOneString;
                objCallsRequestPrePaid.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                objCallsResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.CallsResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetCalls(objCallsRequestPrePaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCallsRequestPrePaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCallsRequestPrePaid.audit.transaction);
            }

            string[] strTrafficDescription = { Claro.SIACU.Constants.PhoneTraffic, objCallsRequestPrePaid.StartDate, " al ", objCallsRequestPrePaid.EndDate };

            MODELS.PrepaidDataCall.ExportExcelCallModel objExportExcelCallModel = new MODELS.PrepaidDataCall.ExportExcelCallModel()
            {
                PhoneDescription = string.Concat(Claro.SIACU.Constants.Telephone, objCallsRequestPrePaid.Msisdn),
                TrafficDescription = string.Concat(strTrafficDescription)
            };

            if (objCallsResponse.listCall != null)
            {
                List<HELPER_CALL.DataCall> listDataCallModel = new List<HELPER_CALL.DataCall>();
                foreach (PrepaidService.CallPrePaid objCall in objCallsResponse.listCall)
                {
                    listDataCallModel.Add(new HELPER_CALL.DataCall()
                    {
                        CallDateTime = objCall.CallDateTime,
                        CallTelephoneDestination = objCall.CallTelephoneDestination,
                        CallTypeTraffic = objCall.CallTypeTraffic,
                        CallDuration = objCall.CallDuration,
                        CallUptake = objCall.CallUptake,
                        CallBoughtPresented = objCall.CallBoughtPresented,
                        CallBalance = objCall.CallBalance,
                        CallBagId = objCall.CallBagId,
                        CallDescription = objCall.CallDescription,
                        CallPlan = objCall.CallPlan,
                        CallPromotion = objCall.CallPromotion,
                        CallDestination = objCall.CallDestination,
                        CallOperator = objCall.CallOperator,
                        CallCobroGroup = objCall.CallCobroGroup,
                        CallNetworkType = objCall.CallNetworkType,
                        CallImei = objCall.CallImei,
                        CallRoaming = objCall.CallRoaming,
                        CallTariffArea = objCall.CallTariffArea
                    });
                }

                objExportExcelCallModel.DataCalls = listDataCallModel;
            }

            return objExportExcelCallModel;
        }

        public JsonResult GetExportExcelCall(string strIdSession, PrepaidService.CallsRequestPrePaid objCallsRequestPrePaid)
        {
            MODELS.PrepaidDataCall.ExportExcelCallModel objExportExcelCallModel = GetExportExcelDataCall(strIdSession, objCallsRequestPrePaid);
            Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();
            JsonResult objResult = null;

            string path = oExcelHelper.ExportExcel(objExportExcelCallModel, "~/Areas/Dashboard/Template/Prepaid/ExportExcelCall.xlsx", KEY.AppSettings("strGridExcelLlamada"));

            if (!path.Equals(""))
                objResult = Json(path);

            return objResult;
        }


        public MODELS.PrepaidDataCall.PrintCallModel getPrintCall(string strIdSession, string strTelephoneCustomer, string strStartDate, string strEndDate)
        {
            List<HELPER_CALL.DataCall> listDataCallModel = new List<HELPER_CALL.DataCall>();
            PrepaidService.CallsResponsePrePaid objCallsResponsePrePaid = new CallsResponsePrePaid();
            strTelephoneCustomer = App_Code.Common.GetPhoneNumber(strTelephoneCustomer);

            PrepaidService.CallsRequestPrePaid objCallsRequestPrePaid = new CallsRequestPrePaid()
            {
                Msisdn = App_Code.Common.GetPhoneNumber(strTelephoneCustomer),
                StartDate = strStartDate,
                EndDate = strEndDate,
                TrafficType = KEY.AppSettings("strtpWSDatosPrePost"),
                TypeQuery = "1",
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objCallsResponsePrePaid = Claro.Web.Logging.ExecuteMethod<PrepaidService.CallsResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetCalls(objCallsRequestPrePaid); });
            }
            catch (Exception ex)
            {
                objCallsResponsePrePaid = null;
                Claro.Web.Logging.Error(strIdSession, objCallsRequestPrePaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCallsRequestPrePaid.audit.transaction);
            }
            if (objCallsResponsePrePaid.listCall != null)
            {
                listDataCallModel = new List<HELPER_CALL.DataCall>();
                foreach (PrepaidService.CallPrePaid objCall in objCallsResponsePrePaid.listCall)
                {
                    listDataCallModel.Add(new HELPER_CALL.DataCall()
                    {
                        CallDateTime = objCall.CallDateTime,
                        CallTelephoneDestination = objCall.CallTelephoneDestination,
                        CallTypeTraffic = objCall.CallTypeTraffic,
                        CallDuration = objCall.CallDuration,
                        CallUptake = objCall.CallUptake,
                        CallBoughtPresented = objCall.CallBoughtPresented,
                        CallBalance = objCall.CallBalance,
                        CallBagId = objCall.CallBagId,
                        CallDescription = objCall.CallDescription,
                        CallPlan = objCall.CallPlan,
                        CallPromotion = objCall.CallPromotion,
                        CallDestination = objCall.CallDestination,
                        CallOperator = objCall.CallOperator,
                        CallCobroGroup = objCall.CallCobroGroup,
                        CallNetworkType = objCall.CallNetworkType,
                        CallImei = objCall.CallImei,
                        CallRoaming = objCall.CallRoaming,
                        CallTariffArea = objCall.CallTariffArea
                    });
                }
            }
            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strText = Claro.SIACU.Constants.ImpressionDetailCalls + strTelephoneCustomer + Claro.SIACU.Constants.DateStart + strStartDate + Claro.SIACU.Constants.DateFinish + strEndDate;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strTelephoneCustomer, KEY.AppSettings("strAudiTraCodImprDetalleLlamadas"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
                throw new Claro.MessageException(objaudit.transaction);
            }
            MODELS.PrepaidDataCall.PrintCallModel objPrintCallModel = new MODELS.PrepaidDataCall.PrintCallModel();
            objPrintCallModel.listCallDetailModel = listDataCallModel;
            objPrintCallModel.DateFrom = strStartDate;
            objPrintCallModel.DateTo = strEndDate;
            objPrintCallModel.DetCallTelephone = strTelephoneCustomer;
            objPrintCallModel.Headers = KEY.AppSettings("strGridImprimirLamada");
            return objPrintCallModel;
        }

        public ActionResult PrintCall(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strDateTo)
        {
            return View(getPrintCall(strIdSession, strTelephoneCustomer, strDateFrom, strDateTo));
        }
    }
}