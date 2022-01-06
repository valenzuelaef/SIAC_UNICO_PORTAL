using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HELPER_PINPUK = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.PinkPukHelper;
using HELPER_TRIATION = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.HistoricalTriationRFAHerlper;
using HELPER_TRIATION_MODEL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.HistoricalTriationRFAModel;
using HELPER_BONDS_HISTORICAL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.ExportExcelHistoricalBondsHelper.HistoricalBondsHelper;
using KEY = Claro.ConfigurationManager;
using MODELS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;



namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PrepaidDataServiceController : Controller
    {
        readonly CommonService.CommonServiceClient oServiceCommon = new CommonService.CommonServiceClient();
        readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pinkpuk(string strIdSession, string strTelephone)
        {
            PrepaidService.PinPukResponsePrepaid objPinPukResponse;
            List<HELPER_PINPUK.PinPuk> listDataPinPuk = null;
            PrepaidService.PinPukRequestPrepaid objListPinPukRequest = new PrepaidService.PinPukRequestPrepaid
            {
                Key = strTelephone,
                StarDate = "",
                EndDate = "",
                Type = "",
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objPinPukResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.PinPukResponsePrepaid>(() => { return new PrepaidService.PrepaidServiceClient().GetPinPuk(objListPinPukRequest); });
            }
            catch (Exception ex)
            {
                objPinPukResponse = null;
                Claro.Web.Logging.Error(strIdSession, objListPinPukRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objListPinPukRequest.audit.transaction);
            }

            if (objPinPukResponse != null && objPinPukResponse.listPinPuk != null)
            {

                listDataPinPuk = new List<HELPER_PINPUK.PinPuk>();
                foreach (PrepaidService.PinkpukPrePaid item in objPinPukResponse.listPinPuk)
                {
                    listDataPinPuk.Add(new HELPER_PINPUK.PinPuk()
                    {
                        ICC_ID = item.ICC_ID,
                        IMSI = item.IMSI,
                        MSISDN = item.MSISDN,
                        PIN1 = item.PIN1,
                        PUK1 = item.PUK1,
                        PIN2 = item.PIN2,
                        PUK2 = item.PUK2
                    });
                }


            }

            MODELS.PrepaidDataService.PinkPukModel oPrepaidDataPinkPukModel = new MODELS.PrepaidDataService.PinkPukModel()
            {
                listPinpukData = listDataPinPuk,
                strTelephone = strTelephone,
                Answer = objPinPukResponse.Respuesta
            };
            return PartialView(oPrepaidDataPinkPukModel);
        }

        public ActionResult PinkpukSearch(string strIdSession, string strTelephone)
        {
            PrepaidService.PinPukResponsePrepaid objPinPukResponse;
            List<HELPER_PINPUK.PinPuk> listDataPinPuk = null;

            PrepaidService.PinPukRequestPrepaid objPinPukRequest = new PrepaidService.PinPukRequestPrepaid
            {
                Key = strTelephone,
                StarDate = "",
                EndDate = "",
                Type = Claro.Constants.NumberThreeString,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };
            try
            {
                objPinPukResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.PinPukResponsePrepaid>(() => { return new PrepaidService.PrepaidServiceClient().GetPinPuk(objPinPukRequest); });
            }
            catch (Exception ex)
            {
                objPinPukResponse = null;
                Claro.Web.Logging.Error(strIdSession, objPinPukRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPinPukRequest.audit.transaction);
            }

            if (objPinPukResponse != null && objPinPukResponse.listPinPuk != null)
            {

                listDataPinPuk = new List<HELPER_PINPUK.PinPuk>();
                foreach (PrepaidService.PinkpukPrePaid item in objPinPukResponse.listPinPuk)
                {
                    listDataPinPuk.Add(new HELPER_PINPUK.PinPuk()
                    {
                        ICC_ID = item.ICC_ID,
                        IMSI = item.IMSI,
                        MSISDN = item.MSISDN,
                        PIN1 = item.PIN1,
                        PUK1 = item.PUK1,
                        PIN2 = item.PIN2,
                        PUK2 = item.PUK2
                    });
                }

            }

            MODELS.PrepaidDataService.PinkPukModel objPinkPukModel = new MODELS.PrepaidDataService.PinkPukModel()
            {
                listPinpukData = listDataPinPuk,
                strTelephone = strTelephone,
                Answer = objPinPukResponse.Respuesta
            };
            return PartialView(objPinkPukModel);
        }

        public ActionResult Salesdues(string strIdSession, string strTelephone)
        {
            MODELS.PrepaidDataService.DebtSaleDuesModel objDebtSaleDuesModel = null;
            List<MODELS.PrepaidDataService.PendingSaleDues> listPendingSaleDues = null;
            List<MODELS.PrepaidDataService.CanceledSaleDues> listCanceledSaleDues = null;
            PrepaidService.DebtPrepaidResponse objDebtPrepaidResponse = new PrepaidService.DebtPrepaidResponse();
            PrepaidService.DebtPrepaidRequest objDebtPrepaidRequest = null;

            try
            {
                objDebtPrepaidRequest = new PrepaidService.DebtPrepaidRequest()
                {
                    Telephone = strTelephone,
                    Bukrs = KEY.AppSettings("strCuotaSAP"),
                    audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
                };
                objDebtPrepaidResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.DebtPrepaidResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetDebt(objDebtPrepaidRequest); });
            }
            catch (Exception ex)
            {
                objDebtPrepaidResponse = null;
                Claro.Web.Logging.Error(strIdSession, objDebtPrepaidRequest.audit.transaction, ex.Message);
            }

            if (objDebtPrepaidResponse != null && objDebtPrepaidResponse.objDebtSaleDues != null)
            {
                objDebtSaleDuesModel = new MODELS.PrepaidDataService.DebtSaleDuesModel();
                objDebtSaleDuesModel.Telephone = strTelephone;
                objDebtSaleDuesModel.Headline = objDebtPrepaidResponse.objDebtSaleDues.Name;
                objDebtSaleDuesModel.Total = objDebtPrepaidResponse.objDebtSaleDues.Total;

                listPendingSaleDues = new List<MODELS.PrepaidDataService.PendingSaleDues>();
                foreach (PrepaidService.PendingSaleDuesPrepaid item in objDebtPrepaidResponse.objDebtSaleDues.listPendingSaleDues)
                {
                    listPendingSaleDues.Add(new Models.PrepaidDataService.PendingSaleDues()
                    {
                        BKTXT = item.BKTXT,
                        FECHA = item.FECHA,
                        MONTO = item.MONTO
                    });
                }
                objDebtSaleDuesModel.listPendingSaleDues = listPendingSaleDues;

                listCanceledSaleDues = new List<MODELS.PrepaidDataService.CanceledSaleDues>();
                foreach (PrepaidService.CanceledSaleDuesPrepaid item in objDebtPrepaidResponse.objDebtSaleDues.listCanceledSaleDues)
                {
                    listCanceledSaleDues.Add(new Models.PrepaidDataService.CanceledSaleDues()
                    {
                        BKTXT = item.BKTXT,
                        FECHA = item.FECHA,
                        MONTO = item.MONTO,
                        FECHAPAGO = item.FECHAPAGO
                    });
                }
                objDebtSaleDuesModel.listCanceledSaleDues = listCanceledSaleDues;
            }
            else
            {
                objDebtSaleDuesModel = new MODELS.PrepaidDataService.DebtSaleDuesModel();
            }
            return View(objDebtSaleDuesModel);
        }


        public ActionResult SalesduesFormato(string strIdSession, string strTelephone)
        {
            MODELS.PrepaidDataService.DebtSaleDuesModel objDebtSaleDuesModel = null;
            List<MODELS.PrepaidDataService.PendingSaleDues> listPendingSaleDues = null;
            List<MODELS.PrepaidDataService.CanceledSaleDues> listCanceledSaleDues = null;
            PrepaidService.DebtPrepaidResponse objDebtPrepaidResponse = new PrepaidService.DebtPrepaidResponse();
            PrepaidService.DebtPrepaidRequest objDebtPrepaidRequest = null;

            try
            {
                objDebtPrepaidRequest = new PrepaidService.DebtPrepaidRequest()
                {
                    Telephone = strTelephone,
                    Bukrs = KEY.AppSettings("strCuotaSAP"),
                    audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
                };
                objDebtPrepaidResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.DebtPrepaidResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetDebt(objDebtPrepaidRequest); });
            }
            catch (Exception ex)
            {
                objDebtPrepaidResponse = null;
                Claro.Web.Logging.Error(strIdSession, objDebtPrepaidRequest.audit.transaction, ex.Message);
            }

            if (objDebtPrepaidResponse != null && objDebtPrepaidResponse.objDebtSaleDues != null)
            {
                objDebtSaleDuesModel = new MODELS.PrepaidDataService.DebtSaleDuesModel();
                objDebtSaleDuesModel.Telephone = strTelephone;
                objDebtSaleDuesModel.Headline = objDebtPrepaidResponse.objDebtSaleDues.Name;
                objDebtSaleDuesModel.Total = objDebtPrepaidResponse.objDebtSaleDues.Total;

                listPendingSaleDues = new List<MODELS.PrepaidDataService.PendingSaleDues>();
                foreach (PrepaidService.PendingSaleDuesPrepaid item in objDebtPrepaidResponse.objDebtSaleDues.listPendingSaleDues)
                {
                    listPendingSaleDues.Add(new Models.PrepaidDataService.PendingSaleDues()
                    {
                        BKTXT = item.BKTXT,
                        FECHA = item.FECHA,
                        MONTO = item.MONTO
                    });
                }
                objDebtSaleDuesModel.listPendingSaleDues = listPendingSaleDues;

                listCanceledSaleDues = new List<MODELS.PrepaidDataService.CanceledSaleDues>();
                foreach (PrepaidService.CanceledSaleDuesPrepaid item in objDebtPrepaidResponse.objDebtSaleDues.listCanceledSaleDues)
                {
                    listCanceledSaleDues.Add(new Models.PrepaidDataService.CanceledSaleDues()
                    {
                        BKTXT = item.BKTXT,
                        FECHA = item.FECHA,
                        MONTO = item.MONTO,
                        FECHAPAGO = item.FECHAPAGO
                    });
                }
                objDebtSaleDuesModel.listCanceledSaleDues = listCanceledSaleDues;
            }
            else
            {
                objDebtSaleDuesModel = new MODELS.PrepaidDataService.DebtSaleDuesModel();
            }
            return View(objDebtSaleDuesModel);
        }


        public ActionResult Datasale(string strIdSession, string strTelephone)
        {
            bool isDatosPEL = false;
            MODELS.PrepaidDataService.DataSaleModel objDataSaleModel = null;
            PrepaidService.PELRequestPrePaid objPELRequestPrePaid = new PrepaidService.PELRequestPrePaid()
            {
                Telephone = strTelephone,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };
            PrepaidService.PELResponsePrePaid objPELResponsePrePaid;

            try
            {
                objPELResponsePrePaid = Claro.Web.Logging.ExecuteMethod<PrepaidService.PELResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetPEL(objPELRequestPrePaid); });
            }
            catch (Exception ex)
            {
                objPELResponsePrePaid = null;
                Claro.Web.Logging.Error(strIdSession, objPELRequestPrePaid.audit.transaction,"Error -1: " + ex.Message);
            }
            try
            {
            if (objPELResponsePrePaid != null && objPELResponsePrePaid.objPEL != null)
            {

                objDataSaleModel = new MODELS.PrepaidDataService.DataSaleModel();
                isDatosPEL = true;
                objDataSaleModel.objDataSalePELModel = new MODELS.PrepaidDataService.DataSalePELModel()
                {
                    TelephonePEL = objPELResponsePrePaid.objPEL.P_TELEFONO.ToString(),
                    Iccid = objPELResponsePrePaid.objPEL.P_ICCID,
                    CodMatIccid = objPELResponsePrePaid.objPEL.P_ICCID_COD,
                    Imei = objPELResponsePrePaid.objPEL.P_IMEI,
                    CodMatImei = objPELResponsePrePaid.objPEL.P_IMEI_COD,
                    Office = objPELResponsePrePaid.objPEL.P_OFICINA,
                    Origin = objPELResponsePrePaid.objPEL.P_PROCEDENCIA,
                    DateActivation = objPELResponsePrePaid.objPEL.P_FECHA_ACT
                };

            }

            if (!string.IsNullOrEmpty(strTelephone) && !isDatosPEL)
            {
                PrepaidService.SalesDataResponse objSalesDataResponse = null;
                PrepaidService.SalesDataRequest objSalesDataRequest = new PrepaidService.SalesDataRequest()
                {
                    TransactionID = strIdSession,
                    ApplicationID = App_Code.Common.GetApplicationIp(),
                    ApplicationName = App_Code.Common.GetApplicationName(),
                    ApplicationUser = App_Code.Common.CurrentUser,
                    PhoneNumber = strTelephone,
                    Matter = "",
                    IssueSeries = "",
                    audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
                };

                try
                {
                    objSalesDataResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.SalesDataResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetSalesDataPEL(objSalesDataRequest); });
                }
                catch (Exception ex)
                {
                    objSalesDataResponse = null;
                        Claro.Web.Logging.Error(strIdSession, objSalesDataRequest.audit.transaction,"Error 1:" + ex.Message);
                }
                if (objSalesDataResponse != null && objSalesDataResponse.objDataSalePEL != null)
                {

                    objDataSaleModel = new MODELS.PrepaidDataService.DataSaleModel()
                    {
                        Telephone = objSalesDataResponse.objDataSalePEL.Telefono,
                        PointSale = objSalesDataResponse.objDataSalePEL.PuntoVenta,
                        Seller = objSalesDataResponse.objDataSalePEL.Vendedor,
                        Bell = objSalesDataResponse.objDataSalePEL.Campaña,
                        DatePurchase = objSalesDataResponse.objDataSalePEL.FechaCompra,
                        IMEI = objSalesDataResponse.objDataSalePEL.IMEI,
                        ItemCode = objSalesDataResponse.objDataSalePEL.CodigoArticulo,
                        BrandModel = objSalesDataResponse.objDataSalePEL.MarcaModelo,
                        DescriptionSale = objSalesDataResponse.objDataSalePEL.DescripcionVenta
                    };
                }
            }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objPELRequestPrePaid.audit.transaction, "Error 2 : " +ex.Message);
                
            }
            return View(objDataSaleModel);
        }

        public ActionResult HistoricalBonds()
        {
            return View();
        }

        public JsonResult HistoricalBondsSearch(string strIdSession, string strTelephone, string strPlanTariff, string strDateStartHB, string strDateEndHB)
        {
            List<MODELS.PrepaidDataService.HistoricalBondsModel> lstHistoricalBondsModel = null;
            int intAccountant = Claro.Constants.NumberZero;
            PrepaidService.HistoricalBondsResponse objHistoricalBondsResponse;
            PrepaidService.HistoricalBondsRequest objHistoricalBondsRequest = new PrepaidService.HistoricalBondsRequest()
            {
                Telephone = strTelephone,
                DateStart = strDateStartHB,
                DateEnd = strDateEndHB,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            if (!string.IsNullOrEmpty(strTelephone))
            {
                try
                {
                    objHistoricalBondsResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.HistoricalBondsResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetHistoricalBonds(objHistoricalBondsRequest); });
                }
                catch (Exception ex)
                {
                    objHistoricalBondsResponse = null;
                    Claro.Web.Logging.Error(strIdSession, objHistoricalBondsRequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objHistoricalBondsRequest.audit.transaction);
                }

                if (objHistoricalBondsResponse != null && objHistoricalBondsResponse.listHistoricalBonds != null)
                {

                    lstHistoricalBondsModel = new List<MODELS.PrepaidDataService.HistoricalBondsModel>();
                    foreach (PrepaidService.HistoricalBondsPrepaid item in objHistoricalBondsResponse.listHistoricalBonds)
                    {
                        lstHistoricalBondsModel.Add(new Models.PrepaidDataService.HistoricalBondsModel()
                        {
                            IdTransaction = item.ID_TRANSACCION == null ? "" : item.ID_TRANSACCION,
                            Transaction = item.TRANSACCION == null ? "" : item.TRANSACCION,
                            Promotion = item.PROMOCION == null ? "" : item.PROMOCION,
                            RegistrationDate = item.FECHA == null ? "" : item.FECHA,
                            Applicative = item.APLICATIVO == null ? "" : item.APLICATIVO,
                            Accountant = intAccountant
                        });
                        intAccountant += Claro.Constants.NumberOne;
                    }
                }

            }

            HELPER_BONDS_HISTORICAL objHistoricalBondsModel = new HELPER_BONDS_HISTORICAL()
            {
                listHistoricalBondsModel = lstHistoricalBondsModel
            };

            return Json(new { data = objHistoricalBondsModel });
        }

       
        public JsonResult ExportExcelHistoricalBonds(List<MODELS.PrepaidDataService.HistoricalBondsModel> listHistoricalBonds, string Telephone)
        {

            HELPER_BONDS_HISTORICAL objHistoricalBonds = new HELPER_BONDS_HISTORICAL()
            {
                listHistoricalBondsModel = listHistoricalBonds,
                Telephone = Telephone
            };

            string path = oExcelHelper.ExportExcel(objHistoricalBonds, TemplateExcel.CONST_HISTORICALBONDS);
            return Json(path);
        }

        public ActionResult HistoricalStriations()
        {
            return View();
        }

        public JsonResult HistoricalStriationsSearch(string strIdSession, string strTelephone, string strPlanTariff, string strDateStartHT, string strDateEndHT)
        {
            return Json(new { data = ListHistoricalStriationsSearch(strIdSession, strTelephone, strPlanTariff, strDateStartHT, strDateEndHT), strDateStartHT });
        }

        public HELPER_TRIATION_MODEL ListHistoricalStriationsSearch(string strIdSession, string strTelephone, string strPlanTariff, string strDateStartHT, string strDateEndHT)
        {

            HELPER_TRIATION_MODEL objHistoricalTriation = new HELPER_TRIATION_MODEL();

            int intAccountant = Claro.Constants.NumberZero;
            PrepaidService.RecordTriationRFAResponse objRecordTriationRFAResponse;
            PrepaidService.RecordTriationRFARequest objRecordTriationRFARequest = new PrepaidService.RecordTriationRFARequest()
            {
                Telephone = strTelephone,
                DateStart = strDateStartHT,
                DateEnd = strDateEndHT,
                PlanTariff = strPlanTariff
            };

            objHistoricalTriation.Telephone = strTelephone;
            objHistoricalTriation.DateFrom = strDateStartHT;
            objHistoricalTriation.DateTo = strDateEndHT;

            List<HELPER_TRIATION.HistoricalTriationRFA> lstHistoricalTriationRFA = null;

            if (!string.IsNullOrEmpty(strTelephone))
            {
                try
                {
                    objRecordTriationRFARequest.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                    objRecordTriationRFAResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.RecordTriationRFAResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetRecordTriationRFA(objRecordTriationRFARequest); });
                }
                catch (Exception ex)
                {
                    objRecordTriationRFAResponse = null;
                    Claro.Web.Logging.Error(strIdSession, objRecordTriationRFARequest.audit.transaction, ex.Message);
                }

                if (objRecordTriationRFAResponse.listHistoricalTriationRFA != null)
                {
                    lstHistoricalTriationRFA = new List<HELPER_TRIATION.HistoricalTriationRFA>();
                    foreach (PrepaidService.HistoricalTriationRFAPrepaid item in objRecordTriationRFAResponse.listHistoricalTriationRFA)
                    {
                        lstHistoricalTriationRFA.Add(new HELPER_TRIATION.HistoricalTriationRFA()
                        {
                            Option = "Opción " + item.Opcion,
                            Transaction = item.Transaccion,
                            Date = item.Fecha,
                            Applicative = item.Aplicativo,
                            Interaction = item.IdInteraccion,
                            Accountant = intAccountant
                        });
                        intAccountant += Claro.Constants.NumberOne;
                    }
                }
            }

            HELPER_TRIATION_MODEL objHistoricalTriationRFAModel = new HELPER_TRIATION_MODEL()
            {
                lstHistoricalTriationRFA = lstHistoricalTriationRFA,
                Telephone = strTelephone,
                DateFrom = strDateStartHT,
                DateTo = strDateEndHT,
                PlanTariff = strPlanTariff,

            };

            return objHistoricalTriationRFAModel;
        }

        public HELPER_TRIATION_MODEL DetailHistoricalTriation(Models.PrepaidDataService.HistoricalTriationRFAModel objHistoricalTriationRFA, string strOption, string strTransaction, string strDate, string strApplicative)
        {
            HELPER_TRIATION_MODEL objHistoricalTriation = new HELPER_TRIATION_MODEL()
            {
                lstNumbersTriationModel = objHistoricalTriationRFA.lstNumbersTriationModel
            };

            objHistoricalTriation.Applicative = strApplicative;
            objHistoricalTriation.Option = strOption;
            objHistoricalTriation.Date = strDate;
            objHistoricalTriation.Transaction = strTransaction;

            return objHistoricalTriation;
        }

        public JsonResult ExportHistoricalTriation(string strIdSession, string strTelephone, string strPlanTariff, string strDateStartHT, string strDateEndHT)
        {
            HELPER_TRIATION_MODEL objHistoricalTriation = ListHistoricalStriationsSearch(strIdSession, strTelephone, strPlanTariff, strDateStartHT, strDateEndHT);
            string path = oExcelHelper.ExportExcel(objHistoricalTriation, TemplateExcel.CONST_HISTORICALTRIATION);
            return Json(path);
        }

        public JsonResult ExportDetailHistoricalTriation(Models.PrepaidDataService.HistoricalTriationRFAModel objHistoricalTriationRFA, string strOption, string strTransaction, string strDate, string strApplicative)
        {
            HELPER_TRIATION_MODEL objHistoricalTriation = DetailHistoricalTriation(objHistoricalTriationRFA, strOption, strTransaction, strDate, strApplicative);

            string path = oExcelHelper.ExportExcel(objHistoricalTriation, TemplateExcel.CONST_DETAILHISTORICALTRIATION);
            return Json(path);
        }

        public JsonResult DetailsStriations(string strIdSession, string strTelephone, string strIdInteraction)
        {
            List<MODELS.PrepaidDataService.NumbersTriationModel> lstNumbersTriationModel = null;
            PrepaidService.DetailTriationRFAResponse objDetailTriationRFAResponse;
            PrepaidService.DetailTriationRFARequest objDetailTriationRFARequest = new PrepaidService.DetailTriationRFARequest()
            {
                Telephone = strTelephone,
                IdInteraction = strIdInteraction,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            if (!string.IsNullOrEmpty(strTelephone))
            {
                try
                {
                    objDetailTriationRFAResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.DetailTriationRFAResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetDetailTriationRFA(objDetailTriationRFARequest); });
                }
                catch (Exception ex)
                {
                    objDetailTriationRFAResponse = null;
                    Claro.Web.Logging.Error(strIdSession, objDetailTriationRFARequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objDetailTriationRFARequest.audit.transaction);
                }

                if (objDetailTriationRFAResponse != null)
                {
                    lstNumbersTriationModel = new List<Models.PrepaidDataService.NumbersTriationModel>();
                    foreach (PrepaidService.NumbersTriation item in objDetailTriationRFAResponse.listNumbersTriation)
                    {

                        lstNumbersTriationModel.Add(new Models.PrepaidDataService.NumbersTriationModel()
                        {

                            Description = item.Descripcion,
                            Description2 = (string.IsNullOrEmpty(item.Descripcion2) ? "" : item.Descripcion2),
                            Description3 = (string.IsNullOrEmpty(item.Descripcion3) ? "" : item.Descripcion3),
                            Description4 = (string.IsNullOrEmpty(item.Descripcion4) ? "" : item.Descripcion4),
                            Description5 = (string.IsNullOrEmpty(item.Descripcion5) ? "" : item.Descripcion5),
                            Description6 = (string.IsNullOrEmpty(item.Descripcion6) ? "" : item.Descripcion6),
                            Description7 = (string.IsNullOrEmpty(item.Descripcion7) ? "" : item.Descripcion7),
                            Description8 = (string.IsNullOrEmpty(item.Descripcion8) ? "" : item.Descripcion8),
                            Description9 = (string.IsNullOrEmpty(item.Descripcion9) ? "" : item.Descripcion9),
                            Description10 = (string.IsNullOrEmpty(item.Descripcion10) ? "" : item.Descripcion10),
                            Amount = item.Monto
                        });
                    }
                }
            }

            Areas.Dashboard.Models.PrepaidDataService.HistoricalTriationRFAModel objHistoricalTriationRFAModel = new Models.PrepaidDataService.HistoricalTriationRFAModel()
            {
                lstNumbersTriationModel = lstNumbersTriationModel
            };
            return Json(new { data = objHistoricalTriationRFAModel });
        }

        public ActionResult PortabilityConsultation(string strIdSession, string strTelephone)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.PortabilityModel objPortabilityModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.PortabilityModel();
            Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon objPortability;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            if (!string.IsNullOrEmpty(strTelephone))
            {
                try
                {
                    objPortability = GetPortability(strTelephone, audit);
                    objPortabilityModel = DataPortabilityModel(objPortability, strTelephone);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Claro.MessageException(audit.transaction);
                }

            }
            return PartialView(objPortabilityModel);
        }

        private Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon GetPortability(string strTelephone, CommonService.AuditRequest audit)
        {


            Claro.SIACU.Web.WebApplication.CommonService.PortabilityRequestCommon objRequest = new CommonService.PortabilityRequestCommon();
            objRequest.Telephone = strTelephone;
            objRequest.audit = audit;
            Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon objResponse = oServiceCommon.GetPortability(objRequest);

            return objResponse;
        }

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.PortabilityModel DataPortabilityModel(Claro.SIACU.Web.WebApplication.CommonService.PortabilityResponseCommon objPortability, string strTelephone)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.PortabilityModel objPortabilityModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.PortabilityModel();

            if (objPortability.ListPortability != null && objPortability.ListPortability.Count > 0)
            {
                objPortabilityModel.NumberRequest = objPortability.ListPortability[0].NUMERO_SOLICITUD;
                objPortabilityModel.ProcessStatus = objPortability.ListPortability[0].ESTADO_PROCESO;
                objPortabilityModel.RegistrationDate = objPortability.ListPortability[0].FECHA_REGISTRO.ToString();
                objPortabilityModel.ExecutionDate = objPortability.ListPortability[0].FECHA_EJECUCION.ToString();
                objPortabilityModel.Operator = objPortability.Operator;
                objPortabilityModel.Answer = objPortability.Respuesta;
                objPortabilityModel.TypeProcessDate = objPortability.TypeProcessDate;
                objPortabilityModel.TypeProcessOperator = objPortability.TypeProcessOperator;
                objPortabilityModel.ExecutionDate = objPortability.ExecutionDate.ToString();
                objPortabilityModel.DescriptionProcessStatus = objPortability.ListPortability[0].DESCRPCION_ESTADO_PROCESO;

            }

            return objPortabilityModel;
        }



        
        public ActionResult HistoricalBalance()
        {
            int intMesesHistorialSaldoBolsa = Convert.ToInt(ConfigurationManager.AppSettings("intMesesHistorialSaldoBolsa"));
            string fechaIni = DateTime.Now.AddMonths(intMesesHistorialSaldoBolsa).ToString("dd/MM/yyyy");
            string fechaFin = DateTime.Now.ToString("dd/MM/yyyy");
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.HistoricalBalanceModel objHistoricalBalanceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.HistoricalBalanceModel()
            {
                endDateHistoricalBalance = fechaFin,
                startDateHistoricalBalance = fechaIni


            };
            return PartialView(objHistoricalBalanceModel);
        }

        public JsonResult ExportExcelHistoricalBalance(List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.BalanceModel> arrlstBalanceHistorical)
        {


        if (arrlstBalanceHistorical == null)
        {
        arrlstBalanceHistorical = new List<MODELS.PrepaidDataService.BalanceModel>();
        }

        Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.HistoricalBalanceModel objHistoricalBalanceModel = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.HistoricalBalanceModel()
            {
                lstHistoricalBalance = arrlstBalanceHistorical
            };


            string path = oExcelHelper.ExportExcel(objHistoricalBalanceModel, TemplateExcel.CONST_HISTORICALBALANCE);
            return Json(path);

        }



    }
}