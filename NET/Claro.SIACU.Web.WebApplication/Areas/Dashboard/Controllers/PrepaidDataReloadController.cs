using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HELPER_RELOAD = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper;
using KEY = Claro.ConfigurationManager;
using MODELS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;
using System.Linq;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PrepaidDataReloadController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizeReload(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strDateTo, string strMovementType, string strcreditoDebito)
        {
            MODELS.PrepaidDataReload.VisualizeReloadModel objVisualizeReloadModel = new MODELS.PrepaidDataReload.VisualizeReloadModel()
            {
                strDetReloadTelephone = strTelephoneCustomer,
                strDateFrom = strDateFrom,
                strDateTo = strDateTo,
                strMovementType = strMovementType,
                strcreditoDebito = strcreditoDebito
            };
            return View(objVisualizeReloadModel);
        }

        public ActionResult DetailVisualizeReload(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strDateTo, string strMovementType, string strcreditoDebito)
        {
            strDateFrom = System.Convert.ToDateTime(strDateFrom.Trim()).ToString("dd/MM/yyyy");
            strDateTo = System.Convert.ToDateTime(strDateTo.Trim()).ToString("dd/MM/yyyy");
            MODELS.Prepaid.DataReloadModel objDataReloadModel = new MODELS.Prepaid.DataReloadModel();
            List<HELPER_RELOAD.DataReload> listReloadModel = null;
            PrepaidService.RechargesResponsePrePaid objRechargesResponse;

            PrepaidService.RechargesRequestPrePaid objRechargesRequest = new PrepaidService.RechargesRequestPrePaid
            {
                Msisdn = strTelephoneCustomer,
                StartDate = strDateFrom,
                EndDate = strDateTo,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession),
                strcreditoDebito = strcreditoDebito,
                strMovementType = strMovementType,
                strtypeQuery = Claro.Constants.NumberOneString
            };

            try
            {
                objRechargesResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.RechargesResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetRecharges(objRechargesRequest); });
            }
            catch (Exception ex)
            {
                objRechargesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRechargesRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRechargesRequest.audit.transaction);
            }

            if (objRechargesResponse != null)
            {
                listReloadModel = new List<HELPER_RELOAD.DataReload>();

                if (objRechargesResponse.listRecharge != null && objRechargesResponse.listRecharge.Count > 0)
                {
                    foreach (PrepaidService.RechargePrePaid objRecharge in objRechargesResponse.listRecharge)
                    {
                        listReloadModel.Add(new HELPER_RELOAD.DataReload()
                        {
                            DateTimeOperation = System.Convert.ToDateTime(objRecharge.FECHARECARGA).ToString("dd/MM/yyyy hh:mm tt"),
                            Kindmovement = objRecharge.TIPOMOVIMIENTO,
                            kindReload = objRecharge.TIPORECARGA,
                            NominalAmount = objRecharge.MONTOEFECTIVO,
                            Balance = objRecharge.SALDO,
                            Credit = objRecharge.CREDDEBI,
                            Bag = objRecharge.BOLSA,
                            KindBalance = objRecharge.TIPOSALDO,
                            Description = objRecharge.DESCRIPCION,
                            Detail = objRecharge.DETALLE
                        });
                    }
                }
            }
            objDataReloadModel.listDataReloadModel = listReloadModel;
            objDataReloadModel.Headers = KEY.AppSettings("strGridVisualiza");
            return View(objDataReloadModel);
        }

        public MODELS.PrepaidDataReload.ExportExcelReloadModel GetExportExcelReloadModel(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strDateTo)
        {
            string strDescriptionTelephone = string.Empty,
                   strDescriptionRecharge = string.Empty,
                   strTelephone = string.Empty;

            if (strTelephoneCustomer.StartsWith(Claro.Constants.NumberTwoString))
            {
                strTelephone = strTelephoneCustomer.Remove(0, 1);
                strDescriptionTelephone = Claro.SIACU.Constants.RechargeCode;
                strDescriptionRecharge = Claro.SIACU.Constants.CodeReloads;
            }
            else
            {
                strTelephone = strTelephoneCustomer.Substring(2, strTelephoneCustomer.Length - 2);
                strDescriptionTelephone = Claro.SIACU.Constants.Telephone;
                strDescriptionRecharge = Claro.SIACU.Constants.PhoneRecharges;
            }

            strDateFrom = System.Convert.ToDateTime(strDateFrom.Trim()).ToString("dd/MM/yyyy");
            strDateTo = System.Convert.ToDateTime(strDateTo.Trim()).ToString("dd/MM/yyyy");
            string[] strRechargeDescription = { strDescriptionRecharge, strDateFrom, " al ", strDateTo };

            MODELS.PrepaidDataReload.ExportExcelReloadModel objExportExcelReloadModel = new MODELS.PrepaidDataReload.ExportExcelReloadModel()
            {
                PhoneDescription = string.Concat(strDescriptionTelephone, strTelephone),
                RechargeDescription = string.Concat(strRechargeDescription)
            };

            List<HELPER_RELOAD.ExportExcelReloadDetailHelper> listExcelReloadDetailModel = null;
            PrepaidService.RechargesResponsePrePaid objRechargesResponse;

            PrepaidService.RechargesRequestPrePaid objRechargesRequest = new PrepaidService.RechargesRequestPrePaid
            {
                Msisdn = strTelephoneCustomer,
                StartDate = strDateFrom,
                EndDate = strDateTo,
                strtypeQuery = Claro.Constants.NumberOneString,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objRechargesResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.RechargesResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetRecharges(objRechargesRequest); });
            }
            catch (Exception ex)
            {
                objRechargesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRechargesRequest.audit.transaction, ex.Message);
            }

            if (objRechargesResponse != null)
            {
                listExcelReloadDetailModel = new List<HELPER_RELOAD.ExportExcelReloadDetailHelper>();
                if (objRechargesResponse.listRecharge != null && objRechargesResponse.listRecharge.Count > 0)
                {
                    foreach (PrepaidService.RechargePrePaid objRecharge in objRechargesResponse.listRecharge)
                    {
                        listExcelReloadDetailModel.Add(new HELPER_RELOAD.ExportExcelReloadDetailHelper()
                        {
                            DateTimeOperation = System.Convert.ToDateTime(objRecharge.FECHARECARGA).ToString("dd/MM/yyyy hh:mm tt"),
                            MovementType = objRecharge.TIPOMOVIMIENTO,
                            kindReload = objRecharge.TIPORECARGA,
                            CashAmount = objRecharge.MONTOEFECTIVO,
                            Balance = objRecharge.SALDO,
                            Credit = objRecharge.CREDDEBI,
                            Bag = objRecharge.BOLSA,
                            BalanceType = objRecharge.TIPOSALDO,
                            Description = objRecharge.DESCRIPCION,
                            Detail = objRecharge.DETALLE
                        });
                    }
                }

                objExportExcelReloadModel.ExportExcelReloadDetailHelpers = listExcelReloadDetailModel;
            }

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strText = Claro.SIACU.Constants.DataRecharge + strTelephoneCustomer + Claro.SIACU.Constants.DateStart + strDateFrom + Claro.SIACU.Constants.DateFinish + strDateTo;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strTelephoneCustomer, KEY.AppSettings("strAudiTraCodExporDetalleLlamadas"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }

            return objExportExcelReloadModel;

        }

        public JsonResult GetExportExcelReload(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strDateTo)
        {
            MODELS.PrepaidDataReload.ExportExcelReloadModel objExportExcelReloadModel = GetExportExcelReloadModel(strIdSession, strTelephoneCustomer, strDateFrom, strDateTo);

            JsonResult objResult = null;

            Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();
            string path = oExcelHelper.ExportExcel(objExportExcelReloadModel, "~/Areas/Dashboard/Template/Prepaid/ExportExcelReload.xlsx", KEY.AppSettings("strGridExcel"));

            if (!path.Equals("")) objResult = Json(path);

            return objResult;
        }

        public ActionResult OnlineReload()
        {
            return PartialView();
        }

        public ActionResult ConsumptionDataPacket()
        {
            return PartialView();
        }

        public JsonResult GetEventType(string strIdSession)
        {
            CommonService.EventTypeResponseCommon objEventTypeResponseCommon;
            CommonService.EventTypeRequestCommon objEventTypeRequestCommon = new CommonService.EventTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                EventType = "listaTipoEventoReload"
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

            MODELS.PrepaidDataReload.OnlineReloadModel objOnlineReloadModel = new MODELS.PrepaidDataReload.OnlineReloadModel();

            if (objEventTypeResponseCommon.EventTypes != null)
            {
                List<HELPER_RELOAD.OnlineHelper.EventType> listEventTypes = new List<HELPER_RELOAD.OnlineHelper.EventType>();

                foreach (CommonService.ListItem item in objEventTypeResponseCommon.EventTypes)
                {
                    listEventTypes.Add(new HELPER_RELOAD.OnlineHelper.EventType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objOnlineReloadModel.EventTypes = listEventTypes;
            }

            return Json(new { data = objOnlineReloadModel });
        }

        private List<PrepaidService.RechargePrePaid> GetFilterOnlineReload(PrepaidService.RechargesResponsePrePaid objRechargesResponsePrePaid, string strTypeEvent)
        {
            return (from PrepaidService.RechargePrePaid p in objRechargesResponsePrePaid.listRecharge
                    where p.TIPOEVENTO == strTypeEvent
                    select p).ToList();
        }

        public JsonResult GetOnlineReload(string strIdSession, PrepaidService.RechargesRequestPrePaid objRechargesRequestPrePaid)
        {
            PrepaidService.RechargesResponsePrePaid objRechargesResponsePrePaid;

            try
            {
                objRechargesRequestPrePaid.Msisdn = App_Code.Common.GetPhoneNumber(objRechargesRequestPrePaid.Msisdn);
                objRechargesRequestPrePaid.strtypeQuery = Claro.Constants.NumberZeroString;
                objRechargesRequestPrePaid.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                objRechargesResponsePrePaid = Claro.Web.Logging.ExecuteMethod<PrepaidService.RechargesResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetRecharges(objRechargesRequestPrePaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRechargesRequestPrePaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRechargesRequestPrePaid.audit.transaction);
            }

            MODELS.Prepaid.DataReloadModel objDataReloadModel = new MODELS.Prepaid.DataReloadModel();

            if (objRechargesResponsePrePaid.listRecharge != null)
            {
                if (!string.IsNullOrEmpty(objRechargesRequestPrePaid.TrafficType))
                    objRechargesResponsePrePaid.listRecharge = GetFilterOnlineReload(objRechargesResponsePrePaid, objRechargesRequestPrePaid.TrafficType);

                List<HELPER_RELOAD.DataReload> listDataReload = new List<HELPER_RELOAD.DataReload>();

                foreach (PrepaidService.RechargePrePaid objCall in objRechargesResponsePrePaid.listRecharge)
                {
                    listDataReload.Add(new HELPER_RELOAD.DataReload()
                    {
                        DateTimeOperation = objCall.FECHARECARGA,
                        TypeEvent = objCall.TIPOEVENTO,
                        NominalAmount = objCall.MONTOEFECTIVO,
                        FinalBalance = objCall.SALDO,
                        Bag = objCall.BOLSA,
                        Detail = objCall.DETALLE,
                        ExpirationDate = objCall.FECHAEXPIRACION
                    });
                }
                objDataReloadModel.listDataReloadModel = listDataReload;
            }

            return Json(new { data = objDataReloadModel });
        }

        public JsonResult GetMovementType(string strIdSession)
        {
            CommonService.EventTypeResponseCommon objEventTypeResponseCommon;
            CommonService.EventTypeRequestCommon objEventTypeRequestCommon = new CommonService.EventTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                EventType = "ListaTipoMovimiento"
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

            MODELS.PrepaidDataReload.OnlineReloadModel objOnlineReloadModel = new MODELS.PrepaidDataReload.OnlineReloadModel();

            if (objEventTypeResponseCommon.EventTypes != null)
            {
                List<HELPER_RELOAD.OnlineHelper.EventType> listEventTypes = new List<HELPER_RELOAD.OnlineHelper.EventType>();

                foreach (CommonService.ListItem item in objEventTypeResponseCommon.EventTypes)
                {
                    listEventTypes.Add(new HELPER_RELOAD.OnlineHelper.EventType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objOnlineReloadModel.EventTypes = listEventTypes;
            }

            return Json(new { data = objOnlineReloadModel });
        }

        public JsonResult GetCredDevType(string strIdSession)
        {
            CommonService.EventTypeResponseCommon objEventTypeResponseCommon;
            CommonService.EventTypeRequestCommon objEventTypeRequestCommon = new CommonService.EventTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                EventType = "ListaCreditoDebito"
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

            MODELS.PrepaidDataReload.OnlineReloadModel objOnlineReloadModel = new MODELS.PrepaidDataReload.OnlineReloadModel();

            if (objEventTypeResponseCommon.EventTypes != null)
            {
                List<HELPER_RELOAD.OnlineHelper.EventType> listEventTypes = new List<HELPER_RELOAD.OnlineHelper.EventType>();

                foreach (CommonService.ListItem item in objEventTypeResponseCommon.EventTypes)
                {
                    listEventTypes.Add(new HELPER_RELOAD.OnlineHelper.EventType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objOnlineReloadModel.EventTypes = listEventTypes;
            }

            return Json(new { data = objOnlineReloadModel });
        }

        private Models.PrepaidDataReload.ConsumptionDataPacketModel GetConsumptionDataPacketModel(string strIdSession, PrepaidService.ConsumptionDataPacketRequestPrePaid objConsumptionDataPacketRequestPrePaid)
        {
            PrepaidService.ConsumptionDataPacketResponsePrePaid objConsumptionDataPacketResponsePrePaid;

            try
            {
                objConsumptionDataPacketRequestPrePaid.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                objConsumptionDataPacketResponsePrePaid = Claro.Web.Logging.ExecuteMethod<PrepaidService.ConsumptionDataPacketResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetConsumptionDataPacket(objConsumptionDataPacketRequestPrePaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objConsumptionDataPacketRequestPrePaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objConsumptionDataPacketRequestPrePaid.audit.transaction);
            }

            Models.PrepaidDataReload.ConsumptionDataPacketModel objConsumptionDataPacketModel = new MODELS.PrepaidDataReload.ConsumptionDataPacketModel();

            if (objConsumptionDataPacketResponsePrePaid.PackageODCSs != null)
            {
                List<HELPER_RELOAD.ConsumptionDataPacketHelper.ConsumptionDataPacket> listConsumptionDataPacket = new List<HELPER_RELOAD.ConsumptionDataPacketHelper.ConsumptionDataPacket>();
                int NumberNineteen = Claro.Constants.NumberNineteen;

                foreach (PrepaidService.PackageODCSPrePaid item in objConsumptionDataPacketResponsePrePaid.PackageODCSs)
                {
                    HELPER_RELOAD.ConsumptionDataPacketHelper.ConsumptionDataPacket objConsumptionDataPacket = new HELPER_RELOAD.ConsumptionDataPacketHelper.ConsumptionDataPacket();

                    objConsumptionDataPacket.DescriptionPackage = item.DescriptionPackage;

                    if (string.IsNullOrEmpty(item.ActivationDate))
                        objConsumptionDataPacket.ActivationDate = string.Empty;
                    else if (item.PackageCode.Equals(KEY.AppSettings("strCodigoPaqueteTarifaDelDia")))
                        objConsumptionDataPacket.ActivationDate = item.ActivationDate;
                    else
                        objConsumptionDataPacket.ActivationDate = item.ActivationDate.Substring(0, NumberNineteen);

                    if (string.IsNullOrEmpty(item.ExpirationDate))
                        objConsumptionDataPacket.ExpirationDate = string.Empty;
                    else if (item.PackageCode.Equals(KEY.AppSettings("strCodigoPaqueteTarifaDelDia")))
                        objConsumptionDataPacket.ExpirationDate = string.Concat(item.ExpirationDate.Substring(0, item.ActivationDate.Length - 2), KEY.AppSettings("strSegundosFechaExpiracionPaqueteDelDia"));
                    else
                        objConsumptionDataPacket.ExpirationDate = item.ExpirationDate.Substring(0, NumberNineteen);

                    if (string.IsNullOrEmpty(item.MBAvailable))
                        objConsumptionDataPacket.MBAvailable = Claro.Constants.NumberZeroString;
                    else
                        objConsumptionDataPacket.MBAvailable = Convert.ToString(Math.Truncate(Convert.ToDouble(item.MBAvailable)));

                    if (string.IsNullOrEmpty(item.MBConsumed))
                        objConsumptionDataPacket.MBConsumed = Claro.Constants.NumberZeroString;
                    else
                        objConsumptionDataPacket.MBConsumed = Convert.ToString(Math.Truncate(Convert.ToDouble(item.MBConsumed)));

                    if (string.IsNullOrEmpty(item.MBAvailable) && string.IsNullOrEmpty(item.MBConsumed))
                        objConsumptionDataPacket.MBGranted = Claro.Constants.NumberZeroString;
                    else
                        objConsumptionDataPacket.MBGranted = string.Concat((Convert.ToInt(Convert.ToDouble(item.MBAvailable)) + Convert.ToInt(Convert.ToDouble(item.MBConsumed))).ToString(), "Mb");

                    if (item.MBAvailable.Equals(KEY.AppSettings("strIndPaqueteIlimEnMBDisp")))
                    {
                        objConsumptionDataPacket.MBConsumed = KEY.AppSettings("strMBConsBolsaIlim");
                    }
                    else if (item.MBAvailable.Equals(KEY.AppSettings("strKeyIlim2")) && item.MBTotal.Equals(KEY.AppSettings("strKeyIlim3")))
                    {
                        objConsumptionDataPacket.MBConsumed = KEY.AppSettings("strMBConsBolsaIlim");
                        objConsumptionDataPacket.MBAvailable = KEY.AppSettings("strPalabraIlimitado");
                        objConsumptionDataPacket.MBGranted = KEY.AppSettings("strPalabraIlimitado");
                    }
                    else
                    {
                        objConsumptionDataPacket.MBConsumed = string.Concat(objConsumptionDataPacket.MBConsumed, KEY.AppSettings("strConstMbODCS"));
                        objConsumptionDataPacket.MBAvailable = string.Concat(objConsumptionDataPacket.MBAvailable, KEY.AppSettings("strConstMbODCS"));
                    }

                    objConsumptionDataPacket.State = StateDescription(item.State);

                    if (KEY.AppSettings("strFlagIlimitado") == "1")
                    {
                        string strPackage = App_Code.Common.GetUnlimitedPackageCode(item.PackageCode, "strCodigoPaqueteIlimitado");

                        if (!string.IsNullOrEmpty(strPackage))
                        {
                            objConsumptionDataPacket.MBConsumed = strPackage;
                            objConsumptionDataPacket.MBAvailable = strPackage;
                            objConsumptionDataPacket.MBGranted = strPackage;
                        }
                    }

                    listConsumptionDataPacket.Add(objConsumptionDataPacket);
                }
                objConsumptionDataPacketModel.ConsumptionDataPackets = listConsumptionDataPacket.OrderByDescending(x => Convert.ToDate(x.ActivationDate)).ToList();
                objConsumptionDataPacketModel.Phone = string.Concat(Claro.SIACU.Constants.Telephone, objConsumptionDataPacketRequestPrePaid.PackageODCS.Msisdn);
            }

            return objConsumptionDataPacketModel;
        }

        public JsonResult GetConsumptionDataPacket(string strIdSession, PrepaidService.ConsumptionDataPacketRequestPrePaid objConsumptionDataPacketRequestPrePaid)
        {
            return Json(new { data = GetConsumptionDataPacketModel(strIdSession, objConsumptionDataPacketRequestPrePaid) });
        }

        private string StateDescription(string state)
        {
            switch (state)
            {
                case "ACT":
                    return "Activo";
                case "NEST":
                    return "Anidado";
                case "EXP":
                    return "Expirado";
                case "CAN":
                    return "Cancelado";
                default:
                    return "";
            }
        }

        public JsonResult GetExcelConsumptionDataPacket(string strIdSession, PrepaidService.ConsumptionDataPacketRequestPrePaid objConsumptionDataPacketRequestPrePaid)
        {
            Models.PrepaidDataReload.ConsumptionDataPacketModel objConsumptionDataPacketModel = GetConsumptionDataPacketModel(strIdSession, objConsumptionDataPacketRequestPrePaid);
            Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();
            JsonResult objResult = null;

            string path = oExcelHelper.ExportExcel(objConsumptionDataPacketModel, "~/Areas/Dashboard/Template/Prepaid/ExportExcelConsumptionDataPacket.xlsx");

            if (!path.Equals(""))
                objResult = Json(path);

            return objResult;
        }


    }
}