using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;
using Claro.SIACU.Web.WebApplication.PostpaidService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid;
using HISTDELIVERY_HELPERS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoricDelivery;
using HISTORICDELIVERY_MODEL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling.BillingHistoricDeliveryModel;
using System.Globalization;
using System.IO;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PostpaidDataBillingController : Controller
    {
        readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();
        readonly PostpaidService.PostpaidServiceClient objPostpaidService = new PostpaidService.PostpaidServiceClient();

        public ActionResult BillingDetailLongDistance(string strTypeApplication, string strIndicator)
        {
            Models.PostpaidDataBilling.BillingDetailLongDistanceModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingDetailLongDistanceModel()
            {
                TypeApplication = strTypeApplication,
                DescriptionTypeCall = strIndicator
            };
            return PartialView(oPostpaidDataBillingModel);
        }

        public JsonResult GetBillingDetailLongDistance(string strIdSession, string strInvoiceNumber, string strTypeCall)
        {
            List<HELPER_DATA.LongDistance.CallDetail> listCallDetail = null;
            PostpaidService.DetailLongDistanceRequestPostPaidPostPaid objGetDetailLongDistanceRequest = new PostpaidService.DetailLongDistanceRequestPostPaidPostPaid
            {
                TypeCall = strTypeCall,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.DetailLongDistanceResponsePostPaid objGetDetailLongDistanceResponse = null;
            try
            {
                objGetDetailLongDistanceResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.DetailLongDistanceResponsePostPaid>(
                    () => { return new PostpaidService.PostpaidServiceClient().GetDetailLongDistance(objGetDetailLongDistanceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetDetailLongDistanceRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetDetailLongDistanceRequest.audit.transaction);
            }

            if (objGetDetailLongDistanceResponse != null && objGetDetailLongDistanceResponse.ListCallDetail != null && objGetDetailLongDistanceResponse.ListCallDetail.Count > 0)
            {
                listCallDetail = new List<HELPER_DATA.LongDistance.CallDetail>();
                foreach (PostpaidService.CallDetailPostPaid item in objGetDetailLongDistanceResponse.ListCallDetail)
                {
                    listCallDetail.Add(new HELPER_DATA.LongDistance.CallDetail()
                    {
                        Msisdn = item.MSISDN,
                        CallBefore = item.CALLANTES,
                        CallAfter = item.CALLDESPUES,
                        CallDestination = item.CALLDESTINATION,
                        CallDate = item.CALLDATE,
                        CallDuration = item.CALLDURATION,
                        CallNumber = item.CALLNUMBER,
                        CallOrigin = item.CALLORIGIN,
                        CallTime = item.CALLTIME,
                        CallTotal = item.CALLTOTAL

                    });
                }
            }

            Models.PostpaidDataBilling.BillingDetailLongDistanceModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingDetailLongDistanceModel()
            {
                listCallDetail = listCallDetail
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingInternationalRoamingDetail(string strIdSession, string strInvoiceNumber)
        {
            List<HELPER_DATA.LongDistance.CallDetail> listCallDetail = null;
            PostpaidService.InternationalRoamingDetailRequestPostPaid objInternationalRoamingDetailRequest = new PostpaidService.InternationalRoamingDetailRequestPostPaid
            {
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.InternationalRoamingDetailResponsePostPaid objInternationalRoamingDetailResponse = null;
            try
            {
                objInternationalRoamingDetailResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.InternationalRoamingDetailResponsePostPaid>(
                    () => { return objPostpaidService.GetInternationalRoamingDetail(objInternationalRoamingDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objInternationalRoamingDetailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objInternationalRoamingDetailRequest.audit.transaction);
            }


            if (objInternationalRoamingDetailResponse != null && objInternationalRoamingDetailResponse.ListCallDetail != null && objInternationalRoamingDetailResponse.ListCallDetail.Count > 0)
            {
                listCallDetail = new List<HELPER_DATA.LongDistance.CallDetail>();
                foreach (PostpaidService.CallDetailPostPaid item in objPostpaidService.GetInternationalRoamingDetail(objInternationalRoamingDetailRequest).ListCallDetail)
                {
                    listCallDetail.Add(new HELPER_DATA.LongDistance.CallDetail()
                    {
                        Msisdn = item.MSISDN,
                        CallOrigin = item.CALLORIGIN,
                        CallNumber = item.CALLNUMBER,
                        CallDate = item.CALLDATE,
                        CallTime = item.CALLTIME,
                        CallDuration = item.CALLDURATION,
                        CallType = item.TIPOLLAMADA,
                        CallTotal = item.CALLTOTAL
                    });
                }
            }
            Models.PostpaidDataBilling.BillingDetailLongDistanceModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingDetailLongDistanceModel()
            {
                listCallDetail = listCallDetail
            };
            return PartialView(oPostpaidDataBillingModel);
        }

        public ActionResult BillingDebtDetail()
        {
            return View();
        }

        public JsonResult GetBillingDebtDetail(string strIdSession, string strDocumentNumber)
        {
            int number = Claro.Constants.NumberOne;
            List<HELPER_DATA.BillingDebtDetail.ApadeceDebt> listApadeceDebt = null;
            PostpaidService.DebtDetailRequestPostPaid objDebtDetailRequest = new PostpaidService.DebtDetailRequestPostPaid
            {
                DocumentNumber = strDocumentNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.DebtDetailResponsePostPaid objDebtDetailResponsePostPaid = null;
            try
            {
                objDebtDetailResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.DebtDetailResponsePostPaid>(
                    () => { return objPostpaidService.GetDebtDetail(objDebtDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDebtDetailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDebtDetailRequest.audit.transaction);
            }

            if (objDebtDetailResponsePostPaid != null && objDebtDetailResponsePostPaid.ListDebtDetail != null)
            {
                listApadeceDebt = new List<HELPER_DATA.BillingDebtDetail.ApadeceDebt>();
                foreach (PostpaidService.ApadeceDebtPostPaid item in objDebtDetailResponsePostPaid.ListDebtDetail)
                {
                    listApadeceDebt.Add(new HELPER_DATA.BillingDebtDetail.ApadeceDebt()
                    {
                        CustomerDocument = item.DOCUMENTO_CLIENTE,
                        CustomerCode = item.CODIGO_CLIENTE,
                        CustomerName = item.NOMBRES_CLIENTE,
                        CustomerLastname = item.APELLIDOS_CLIENTE,
                        CustomerRepresentative = item.REPRESENTANTE_CLIENTE,
                        CustomerState = item.ESTADO_CLIENTE,
                        CustomerType = item.TIPO_CLIENTE,
                        CustomerDebt = item.DEUDA_CLIENTE,
                        Number = Convert.ToString(number)
                    });
                    number++;
                }
            }

            Models.PostpaidDataBilling.BillingDebtDetailModel oBillingDebtDetailModel = new Models.PostpaidDataBilling.BillingDebtDetailModel()
            {
                listApadeceDebt = listApadeceDebt,
                DocumentNumber = strDocumentNumber,
                DebtQuantity = Convert.ToString(listApadeceDebt.Count)
            };
            return Json(new { data = oBillingDebtDetailModel });
        }

        public ActionResult BillingAdditionalLocalTrafficDetail()
        {
            return PartialView();
        }

        public ActionResult BillingConsumeLocalTrafficDetail()
        {
            return PartialView();
        }

        public JsonResult GetBillingAdditionalLocalTrafficDetail(string strIdSession, string strInvoiceNumber)
        {
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail> listTimProLocalTrafficDetail = null;
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail> listTimMaxLocalTrafficDetail = null;

            PostpaidService.AdditionalLocalTrafficDetailRequestPostPaid objGetAdditionalLocalTrafficDetailRequest = new PostpaidService.AdditionalLocalTrafficDetailRequestPostPaid
            {
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.AdditionalLocalTrafficDetailResponsePostPaid objAdditionalLocalTrafficDetailResponsePostPaid = null;
            try
            {
                objAdditionalLocalTrafficDetailResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.AdditionalLocalTrafficDetailResponsePostPaid>(
                    () => { return objPostpaidService.GetAdditionalLocalTrafficDetail(objGetAdditionalLocalTrafficDetailRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetAdditionalLocalTrafficDetailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetAdditionalLocalTrafficDetailRequest.audit.transaction);
            }

            if (objAdditionalLocalTrafficDetailResponsePostPaid != null && objAdditionalLocalTrafficDetailResponsePostPaid.ListTimMaxLocalTrafficDetail != null)
            {
                List<PostpaidService.DetailLocalTrafficPostPaid> listTimMaxLocalTrafficDetailService = objAdditionalLocalTrafficDetailResponsePostPaid.ListTimMaxLocalTrafficDetail;
                if (listTimMaxLocalTrafficDetailService != null)
                {
                    listTimMaxLocalTrafficDetail = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail>();
                    foreach (PostpaidService.DetailLocalTrafficPostPaid item in listTimMaxLocalTrafficDetailService)
                    {

                        listTimMaxLocalTrafficDetail.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail()
                        {
                            Msisdn = item.MSISDN,
                            OnNet = item.ONNET,
                            OffNet = item.OFF_NET,
                            OffOnNetOffNet = item.OFF_ONNET_OFFNET,
                            Amount = item.IMPORTE
                        });
                    }
                }
            }

            if (objAdditionalLocalTrafficDetailResponsePostPaid != null && objAdditionalLocalTrafficDetailResponsePostPaid.ListTimProLocalTrafficDetail != null)
            {
                List<PostpaidService.DetailLocalTrafficPostPaid> listTimProLocalTrafficDetailService = objAdditionalLocalTrafficDetailResponsePostPaid.ListTimProLocalTrafficDetail;
                if (listTimProLocalTrafficDetailService != null)
                {
                    listTimProLocalTrafficDetail = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail>();
                    foreach (PostpaidService.DetailLocalTrafficPostPaid item in listTimProLocalTrafficDetailService)
                    {

                        listTimProLocalTrafficDetail.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail()
                        {
                            Msisdn = item.MSISDN,
                            Rtp = item.RTP,
                            OnNet = item.ONNET,
                            OffNetToTelephone = item.OFFNET_A_FIJO,
                            OffNetToCellphone = item.OFFNET_A_CELULAR,
                            Amount = item.IMPORTE
                        });
                    }
                }
            }

            HELPER_DATA.BillingLocalTrafficDetail.LocalTrafficDetailConstants objLocalTrafficDetailConstants = new HELPER_DATA.BillingLocalTrafficDetail.LocalTrafficDetailConstants()
            {
                Complete = Claro.SIACU.Constants.Complete,
                OffNet = Claro.SIACU.Constants.OffNet,
                OffNetToCellphone = Claro.SIACU.Constants.OffNetToCellphone,
                OffNetToTelephone = Claro.SIACU.Constants.OffNetToTelephone,
                OnNet = Claro.SIACU.Constants.OnNet,
                OnNetOffNet = Claro.SIACU.Constants.OnNetOffNet,
                RTP = Claro.SIACU.Constants.Rtp,
                RTPOnNet = Claro.SIACU.Constants.RtpOnNet,
                RTPOnNetOffNet = Claro.SIACU.Constants.RtpOnNetOffNet

            };

            HELPER_DATA.BillingLocalTrafficDetail.GeneralLocalTrafficDetailConstants objGeneralLocalTrafficDetailConstants = new HELPER_DATA.BillingLocalTrafficDetail.GeneralLocalTrafficDetailConstants()
            {
                ConsultTimMaxAdditionalLocalTraffic = Claro.SIACU.Constants.ConsultTimMaxAdditionalLocalTraffic,
                ConsultTimMaxConsumeLocalTraffic = Claro.SIACU.Constants.ConsultTimMaxConsumeLocalTraffic,
                ConsultTimProAdditionalLocalTraffic = Claro.SIACU.Constants.ConsultTimProAdditionalLocalTraffic,
                ConsultTimProConsumeLocalTraffic = Claro.SIACU.Constants.ConsultTimProConsumeLocalTraffic

            };
            Models.PostpaidDataBilling.BillingLocalTrafficDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingLocalTrafficDetailModel()
            {
                listTimMaxLocalTrafficDetail = listTimMaxLocalTrafficDetail,
                listTimProLocalTrafficDetail = listTimProLocalTrafficDetail,
                objGeneralLocalTrafficDetailConstants = objGeneralLocalTrafficDetailConstants,
                objLocalTrafficDetailConstants = objLocalTrafficDetailConstants
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }


        public JsonResult GetBillingConsumeLocalTrafficDetail(string strIdSession, string strInvoiceNumber)
        {
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail> listTimMaxLocalTrafficDetail = null;
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail> listTimProLocalTrafficDetail = null;
            PostpaidService.ConsumeLocalTrafficDetailRequestPostPaid objGetConsumeLocalTrafficDetailRequest = new PostpaidService.ConsumeLocalTrafficDetailRequestPostPaid
            {
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.ConsumeLocalTrafficDetailResponsePostPaid objConsumeLocalTrafficDetailResponsePostPaid = null;
            try
            {
                objConsumeLocalTrafficDetailResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ConsumeLocalTrafficDetailResponsePostPaid>(
                    () => { return objPostpaidService.GetConsumeLocalTrafficDetail(objGetConsumeLocalTrafficDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetConsumeLocalTrafficDetailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetConsumeLocalTrafficDetailRequest.audit.transaction);
            }

            List<PostpaidService.DetailLocalTrafficPostPaid> listConsumeTimProLocalTrafficDetailService = objConsumeLocalTrafficDetailResponsePostPaid.ListTimProLocalTrafficDetail;
            if (objConsumeLocalTrafficDetailResponsePostPaid != null && listConsumeTimProLocalTrafficDetailService != null && listConsumeTimProLocalTrafficDetailService.Count > 0)
            {
                listTimProLocalTrafficDetail = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail>();
                foreach (PostpaidService.DetailLocalTrafficPostPaid item in listConsumeTimProLocalTrafficDetailService)
                {
                    listTimProLocalTrafficDetail.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail()
                    {
                        Msisdn = item.MSISDN,
                        Rtp = item.RTP,
                        OnNet = item.ONNET,
                        OffNetToTelephone = item.OFFNET_A_FIJO,
                        OffNetToCellphone = item.OFFNET_A_CELULAR,
                        Amount = item.IMPORTE
                    });
                }
            }

            List<PostpaidService.DetailLocalTrafficPostPaid> listConsumeTimMaxLocalTrafficDetailService = objConsumeLocalTrafficDetailResponsePostPaid.ListTimMaxLocalTrafficDetail;
            if (objConsumeLocalTrafficDetailResponsePostPaid != null && listConsumeTimMaxLocalTrafficDetailService != null && listConsumeTimMaxLocalTrafficDetailService.Count > 0)
            {
                listTimMaxLocalTrafficDetail = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail>();
                foreach (PostpaidService.DetailLocalTrafficPostPaid item in listConsumeTimMaxLocalTrafficDetailService)
                {

                    listTimMaxLocalTrafficDetail.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetail()
                    {
                        Msisdn = item.MSISDN,
                        OnNet = item.ONNET,
                        OffNetToTelephone = item.OFFNET_A_FIJO,
                        OffNetToCellphone = item.OFFNET_A_CELULAR,
                        Amount = item.IMPORTE
                    });
                }
            }

            Models.PostpaidDataBilling.BillingLocalTrafficDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingLocalTrafficDetailModel()
            {
                listTimMaxLocalTrafficDetail = listTimMaxLocalTrafficDetail,
                listTimProLocalTrafficDetail = listTimProLocalTrafficDetail
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingOtherCharges(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.BillingOtherCharges.OtherDetails> listOtherDetails = null;
            PostpaidService.DetailsOtherConceptsRequestPostPaid objGetDetailsOtherConceptsRequest = new PostpaidService.DetailsOtherConceptsRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.DetailsOtherConceptsResponsePostPaid objDetailsOtherConceptsResponsePostPaid = null;
            try
            {
                objDetailsOtherConceptsResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.DetailsOtherConceptsResponsePostPaid>(
                    () => { return objPostpaidService.GetDetailsOtherConcepts(objGetDetailsOtherConceptsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetDetailsOtherConceptsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetDetailsOtherConceptsRequest.audit.transaction);
            }


            if (objDetailsOtherConceptsResponsePostPaid != null)
            {
                List<PostpaidService.OtherChargesDetailsPostPaid> listDetailsOtherConcepts = objDetailsOtherConceptsResponsePostPaid.ListOtherDetails;
                if (listDetailsOtherConcepts != null && listDetailsOtherConcepts.Count > 0)
                {
                    listOtherDetails = new List<HELPER_DATA.BillingOtherCharges.OtherDetails>();
                    foreach (PostpaidService.OtherChargesDetailsPostPaid item in listDetailsOtherConcepts)
                    {
                        listOtherDetails.Add(new HELPER_DATA.BillingOtherCharges.OtherDetails()
                        {
                            amount = item.amount,
                            description = item.description,
                            reason = item.reason
                        });
                    }
                }
            }

            BillingOtherChargesModel oBillingOtherChargesModel = new BillingOtherChargesModel()
            {
                listOtherDetails = listOtherDetails
            };
            return PartialView(oBillingOtherChargesModel);
        }

        public ActionResult BillingPrintTimServiceDetail()
        {
            return PartialView();
        }


        public JsonResult GetBillingPrintTimServiceDetail(string strIdSession, string strInvoiceNumber)
        {
            List<HELPER_DATA.LongDistance.CallDetailTimService> listCallDetailTimService = null;
            PostpaidService.TimServiceDetailsRequestPostPaid objGetTimServiceDetailsRequest = new PostpaidService.TimServiceDetailsRequestPostPaid()
            {
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.TimServiceDetailsResponsePostPaid objGetTimServiceDetailsResponse = null;
            try
            {
                objGetTimServiceDetailsResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.TimServiceDetailsResponsePostPaid>(
                    () => { return objPostpaidService.GetTimServiceDetails(objGetTimServiceDetailsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetTimServiceDetailsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetTimServiceDetailsRequest.audit.transaction);
            }

            if (objGetTimServiceDetailsResponse != null && objGetTimServiceDetailsResponse.ListCallDetail != null && objGetTimServiceDetailsResponse.ListCallDetail.Count > 0)
            {
                listCallDetailTimService = new List<HELPER_DATA.LongDistance.CallDetailTimService>();
                foreach (PostpaidService.CallDetailTimServicePostPaid item in objGetTimServiceDetailsResponse.ListCallDetail)
                {
                    listCallDetailTimService.Add(new HELPER_DATA.LongDistance.CallDetailTimService()
                    {
                        MSISDN = item.MSISDN,
                        AMOUNT = item.AMOUNT,
                        PERIODO = item.PERIODO,
                        RATEPLAN = item.RATEPLAN.CompareTo("Mensajes") >= 0 ? "<a style='cursor:pointer'>" + item.RATEPLAN + "</a>" : item.RATEPLAN
                    });
                }
            }

            Models.PostpaidDataBilling.BillingDetailLongDistanceModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingDetailLongDistanceModel()
            {
                listCallDetailTimService = listCallDetailTimService
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingSMSDetails()
        {

            return View();
        }

        public ActionResult BillingHistoryInvoice()
        {
            return PartialView();
        }


        public JsonResult getHistoryInvoice(string strIdSession, string strCustomerID)
        {
            List<HELPER_DATA.BillingHistoryInvoice.Invoice> listHistoryInvoice = null;
            PostpaidService.HistoryInvoiceRequestPostPaid objGetHistoryInvoiceRequest = new HistoryInvoiceRequestPostPaid()
            {
                CustomerID = strCustomerID,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            Claro.Web.Logging.Error("getHistoryInvoice", "strCustomerID: ", strCustomerID);
            //Claro.Web.Logging.Error("getHistoryInvoice", "strPlataforma: ", strPlataforma);
            PostpaidService.HistoryInvoiceResponsePostPaid objGetHistoryInvoiceResponse = null;
            try
            {
                Claro.Web.Logging.Error("getHistoryInvoice", "objGetHistoryInvoiceReqeust: ", "-----");
                objGetHistoryInvoiceResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoryInvoiceResponsePostPaid>(
                    () => { return objPostpaidService.GetHistoryInvoice(objGetHistoryInvoiceRequest, ""); });
                Claro.Web.Logging.Error("getHistoryInvoice", "objGetHistoryInvoiceResponse: ", "-----");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetHistoryInvoiceRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetHistoryInvoiceRequest.audit.transaction);
            }

            if (objGetHistoryInvoiceResponse != null && objGetHistoryInvoiceResponse.ListReceiptHistory != null && objGetHistoryInvoiceResponse.ListReceiptHistory.Count > 0)
            {
                listHistoryInvoice = new List<HELPER_DATA.BillingHistoryInvoice.Invoice>();
                foreach (PostpaidService.ReceiptHistory item in objGetHistoryInvoiceResponse.ListReceiptHistory)
                {
                    listHistoryInvoice.Add(new HELPER_DATA.BillingHistoryInvoice.Invoice()
                    {
                        FechaEmision = item.FechaEmision,
                        FechaVencimiento = item.FechaVencimiento,
                        TotalCurrentCharges = Convert.ToDecimal(item.TotalCurrentCharges),
                        InvoiceNumber = item.InvoiceNumber,
                        Periodo = item.Periodo
                    });


                }
            }
            Models.PostpaidDataBilling.BillingHistoryInvoiceModel oBillingHistoryInvoiceModel = new Models.PostpaidDataBilling.BillingHistoryInvoiceModel()
            {
                listInvoice = listHistoryInvoice
            };
            return Json(new { data = oBillingHistoryInvoiceModel });
        }

        public ActionResult BillingHistoricDelivery()
        {
            return PartialView();
        }

        public HISTORICDELIVERY_MODEL GetHistoricDelivery(string strIdSession, string strCustomerID, string flagPlataforma)
        {
            List<HISTDELIVERY_HELPERS.HisDelivery> listHisDelivery = null;
            HISTORICDELIVERY_MODEL ObjHistoricDeliveryModel = new HISTORICDELIVERY_MODEL();
            PostpaidService.HistoricDeliveryRequestPostPaid objGetReceiptHistoryRequest = new PostpaidService.HistoricDeliveryRequestPostPaid
            {
                strCustomer = strCustomerID,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                flagPlataforma = flagPlataforma
            };

            PostpaidService.HistoricDeliveryResponsePostpaid objHistoricDeliveryResponsePostpaid = null;
            try
            {
                objHistoricDeliveryResponsePostpaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoricDeliveryResponsePostpaid>(
                    () => { return objPostpaidService.GetHistoricDelivery(objGetReceiptHistoryRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetReceiptHistoryRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetReceiptHistoryRequest.audit.transaction);
            }


            try
            {
                HISTDELIVERY_HELPERS.HisDelivery oHisDelivery;

                if (objHistoricDeliveryResponsePostpaid != null)
                {
                    List<PostpaidService.HistoricDeliveryPostpaid> ListHistoricDeliveryService = objHistoricDeliveryResponsePostpaid.ListHistoricDelivery;
                    if (ListHistoricDeliveryService != null)
                    {
                        listHisDelivery = new List<HISTDELIVERY_HELPERS.HisDelivery>();
                        foreach (PostpaidService.HistoricDeliveryPostpaid item in ListHistoricDeliveryService)
                        {
                            oHisDelivery = new HISTDELIVERY_HELPERS.HisDelivery();

                            oHisDelivery.RECIBO = item._RECIBO;
                            oHisDelivery.FECEMISION = item._FECEMISION;

                            if (string.IsNullOrEmpty(item._TIPO))
                            {
                                oHisDelivery.TIPO = string.Empty;
                            }
                            else
                            {
                            oHisDelivery.TIPO = item._TIPO;
                            }

                            if (string.IsNullOrEmpty(item._COURIER))
                            {
                                oHisDelivery.COURIER = string.Empty;
                            }
                            else
                            {
                            oHisDelivery.COURIER = item._COURIER;
                            }

                            if (string.IsNullOrEmpty(item._ESTADO))
                            {
                                oHisDelivery.ESTADO = string.Empty;
                            }
                            else
                            {
                            oHisDelivery.ESTADO = item._ESTADO;
                            }

                            if (string.IsNullOrEmpty(item._MOTIVO))
                            {
                                oHisDelivery.MOTIVO = string.Empty;
                            }
                            else
                            {
                            oHisDelivery.MOTIVO = item._MOTIVO;
                            }

                            oHisDelivery.FECENTREGA = item._FECENTREGA;

                            if (string.IsNullOrEmpty(item._MOTIVO))
                            {
                                oHisDelivery.Url = string.Empty;
                            }
                            else
                            {
                            oHisDelivery.Url = GetUrlCourier(oHisDelivery.COURIER.Trim().ToUpper());
                            }
                            
                            listHisDelivery.Add(oHisDelivery);
                        }
                    }
                }

                ObjHistoricDeliveryModel.listHisDelivery = listHisDelivery;
            }
            catch (Exception ex)
            {
                ObjHistoricDeliveryModel.listHisDelivery = null;
                Claro.Web.Logging.Error(objGetReceiptHistoryRequest.audit.Session, objGetReceiptHistoryRequest.audit.transaction, ex.Message);
            }
            return ObjHistoricDeliveryModel;
        }
        public string GetUrlCourier(string courier)
        {
            string url = "";
            string cFlag = "N";
            string cadenaCourier = ConfigurationManager.AppSettings("strListaCourierSDF");
            string[] arrayCourier = cadenaCourier.Split(';');

            foreach (var strCourierWeb in arrayCourier)
            {
                if (courier.Equals(strCourierWeb, StringComparison.OrdinalIgnoreCase))
                {
                    cFlag = "S";
                    break;
                }
            }

            if (cFlag.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                if (courier.Equals("URBANO", StringComparison.OrdinalIgnoreCase))
                {
                    url = ConfigurationManager.AppSettings("strURLURBANOSDF");
                }
                else if (courier.Equals("SMP", StringComparison.OrdinalIgnoreCase))
                {
                    url = ConfigurationManager.AppSettings("strURLSMPSDF");
                }
                else if (courier.Equals("DATAIMAGENES", StringComparison.OrdinalIgnoreCase))
                {
                    url = ConfigurationManager.AppSettings("strURLDATAIMAGENESSDF").Replace("AMPERSAND", "&");
                }
            }

            return url;
        }


        public JsonResult GetBillingHistoricDelivery(string strIdSession, string strCustomerID, string flagPlataforma)
        {
            HISTORICDELIVERY_MODEL oPostpaidDataBillingModel = GetHistoricDelivery(strIdSession, strCustomerID, flagPlataforma);
            return Json(new { data = oPostpaidDataBillingModel });

        }

        public JsonResult BillingExportHistoricDelivery(string strIdSession, string strCustomerID, string strTelephone, string flagPlataforma)
        {
            HISTORICDELIVERY_MODEL oPostpaidDataBillingModel = GetHistoricDelivery(strIdSession, strCustomerID, flagPlataforma);
            oPostpaidDataBillingModel.TELEPHONE = strTelephone;
            string path = oExcelHelper.ExportExcel(oPostpaidDataBillingModel, TemplateExcel.CONST_HISTORICDELIVERY);
            return Json(path);
        }


        public ActionResult BillingFixedCharge()
        {
            return PartialView();
        }

        public JsonResult GetFixedChargeDetailTimPro(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimPro> listFixedChargeDetailTimPro = null;
            PostpaidService.FixedChargeDetailTimProRequestPostPaid objFixedChargeDetailTimProRequest = new PostpaidService.FixedChargeDetailTimProRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };
            PostpaidService.FixedChargeDetailTimProResponsePostPaid objFixedChargeDetailTimProResponse = null;
            try
            {
                objFixedChargeDetailTimProResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProResponsePostPaid>(
                    () => { return objPostpaidService.GetFixedChargeDetailTimPro(objFixedChargeDetailTimProRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProResponse != null && objFixedChargeDetailTimProResponse.ListFixedChargeDetailTimPro != null && objFixedChargeDetailTimProResponse.ListFixedChargeDetailTimPro.Count > 0)
            {
                listFixedChargeDetailTimPro = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimPro>();
                foreach (PostpaidService.FixedChargeDetailTimProPostPaid item in objFixedChargeDetailTimProResponse.ListFixedChargeDetailTimPro)
                {
                    listFixedChargeDetailTimPro.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimPro()
                    {

                        Msisdn = item.Msisdn,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Amount = item.Amount
                    });
                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProModel()
            {
                listFixedChargeDetailTimPro = listFixedChargeDetailTimPro
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimProOne(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProOne> listFixedChargeDetailTimProOne = null;
            PostpaidService.FixedChargeDetailTimProOneRequestPostPaid objFixedChargeDetailTimProOneRequest = new PostpaidService.FixedChargeDetailTimProOneRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };
            PostpaidService.FixedChargeDetailTimProOneResponsePostPaid objFixedChargeDetailTimProOneResponse = null;
            try
            {
                objFixedChargeDetailTimProOneResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProOneResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimProOne(objFixedChargeDetailTimProOneRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProOneRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProOneRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProOneResponse != null && objFixedChargeDetailTimProOneResponse.ListFixedChargeDetailTimProOne != null && objFixedChargeDetailTimProOneResponse.ListFixedChargeDetailTimProOne.Count > 0)
            {
                listFixedChargeDetailTimProOne = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProOne>();
                foreach (PostpaidService.FixedChargeDetailTimProOnePostPaid item in objFixedChargeDetailTimProOneResponse.ListFixedChargeDetailTimProOne)
                {
                    listFixedChargeDetailTimProOne.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProOne()
                    {

                        Msisdn = item.Msisdn,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Fu4 = item.Fu4,
                        Amount = item.Amount,
                        MsgTexto5 = item.MsgTexto5
                    });


                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProOneModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProOneModel()
            {
                listFixedChargeDetailTimProOne = listFixedChargeDetailTimProOne
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimProTwo(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProTwo> listFixedChargeDetailTimProTwo = null;
            PostpaidService.FixedChargeDetailTimProTwoRequestPostPaid objFixedChargeDetailTimProTwoRequest = new PostpaidService.FixedChargeDetailTimProTwoRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimProTwoResponsePostPaid objFixedChargeDetailTimProTwoResponse = null;
            try
            {
                objFixedChargeDetailTimProTwoResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProTwoResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimProTwo(objFixedChargeDetailTimProTwoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProTwoRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProTwoRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProTwoResponse != null && objFixedChargeDetailTimProTwoResponse.ListFixedChargeDetailTimProTwo != null && objFixedChargeDetailTimProTwoResponse.ListFixedChargeDetailTimProTwo.Count > 0)
            {
                listFixedChargeDetailTimProTwo = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProTwo>();
                foreach (PostpaidService.FixedChargeDetailTimProTwoPostPaid item in objFixedChargeDetailTimProTwoResponse.ListFixedChargeDetailTimProTwo)
                {

                    listFixedChargeDetailTimProTwo.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProTwo()
                    {

                        Msisdn = item.Msisdn,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Fu4 = item.Fu4,
                        Amount = item.Amount,
                        MsgTexto5 = item.MsgTexto5
                    });

                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProTwoModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProTwoModel()
            {
                listFixedChargeDetailTimProTwo = listFixedChargeDetailTimProTwo
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimMax(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimMax> listFixedChargeDetailTimMax = null;
            PostpaidService.FixedChargeDetailTimMaxRequestPostPaid objFixedChargeDetailTimMaxRequest = new PostpaidService.FixedChargeDetailTimMaxRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimMaxResponsePostPaid objFixedChargeDetailTimMaxResponse = null;
            try
            {
                objFixedChargeDetailTimMaxResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimMaxResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimMax(objFixedChargeDetailTimMaxRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimMaxRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimMaxRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimMaxResponse != null && objFixedChargeDetailTimMaxResponse.ListFixedChargeDetailTimMax != null && objFixedChargeDetailTimMaxResponse.ListFixedChargeDetailTimMax.Count > 0)
            {
                listFixedChargeDetailTimMax = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimMax>();
                foreach (PostpaidService.FixedChargeDetailTimMaxPostPaid item in objFixedChargeDetailTimMaxResponse.ListFixedChargeDetailTimMax)
                {
                    listFixedChargeDetailTimMax.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimMax()
                    {

                        Msisdn = item.Msisdn,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Fu4 = item.Fu4,
                        Amount = item.Amount,
                        MsgTexto4 = item.MsgTexto4
                    });


                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimMaxModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimMaxModel()
            {
                listFixedChargeDetailTimMax = listFixedChargeDetailTimMax
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimMaxTwo(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimMaxTwo> listFixedChargeDetailTimMaxTwo = null;
            PostpaidService.FixedChargeDetailTimMaxTwoRequestPostPaid objFixedChargeDetailTimMaxTwoRequest = new PostpaidService.FixedChargeDetailTimMaxTwoRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimMaxTwoResponsePostPaid objFixedChargeDetailTimMaxTwoResponse = null;
            try
            {
                objFixedChargeDetailTimMaxTwoResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimMaxTwoResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimMaxTwo(objFixedChargeDetailTimMaxTwoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimMaxTwoRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimMaxTwoRequest.audit.transaction);
            }


            if (objFixedChargeDetailTimMaxTwoResponse != null && objFixedChargeDetailTimMaxTwoResponse.ListFixedChargeDetailTimMaxTwo != null && objFixedChargeDetailTimMaxTwoResponse.ListFixedChargeDetailTimMaxTwo.Count > 0)
            {
                listFixedChargeDetailTimMaxTwo = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimMaxTwo>();
                foreach (PostpaidService.FixedChargeDetailTimMaxTwoPostPaid item in objFixedChargeDetailTimMaxTwoResponse.ListFixedChargeDetailTimMaxTwo)
                {
                    listFixedChargeDetailTimMaxTwo.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimMaxTwo()
                    {

                        Msisdn = item.Msisdn,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Amount = item.Amount,
                        MsgTexto4 = item.MsgTexto4
                    });

                }
            }
            Models.PostpaidDataBilling.BillingFixedChargeDetailTimMaxTwoModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimMaxTwoModel()
            {
                listFixedChargeDetailTimMaxTwo = listFixedChargeDetailTimMaxTwo
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetDiscountFixedChargeDetail(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.DiscountFixedChargeDetail> listDiscountFixedChargeDetail = null;
            PostpaidService.DiscountFixedChargeDetailRequestPostPaid objDiscountFixedChargeDetailRequest = new PostpaidService.DiscountFixedChargeDetailRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.DiscountFixedChargeDetailResponsePostPaid objDiscountFixedChargeDetailResponse = null;
            try
            {
                objDiscountFixedChargeDetailResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.DiscountFixedChargeDetailResponsePostPaid>(() => { return objPostpaidService.GetDiscountFixedChargeDetail(objDiscountFixedChargeDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDiscountFixedChargeDetailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDiscountFixedChargeDetailRequest.audit.transaction);
            }

            if (objDiscountFixedChargeDetailResponse != null && objDiscountFixedChargeDetailResponse.ListDiscountFixedChargeDetail != null && objDiscountFixedChargeDetailResponse.ListDiscountFixedChargeDetail.Count > 0)
            {
                listDiscountFixedChargeDetail = new List<HELPER_DATA.FixedChargeHelper.DiscountFixedChargeDetail>();
                foreach (PostpaidService.DiscountFixedChargeDetailPostPaid item in objDiscountFixedChargeDetailResponse.ListDiscountFixedChargeDetail)
                {
                    listDiscountFixedChargeDetail.Add(new HELPER_DATA.FixedChargeHelper.DiscountFixedChargeDetail()
                    {

                        Msisdn = item.Msisdn,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Amount = item.Amount,
                        MsgTexto4 = item.MsgTexto4
                    });


                }
            }
            Models.PostpaidDataBilling.BillingDiscountFixedChargeDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingDiscountFixedChargeDetailModel()
            {
                listDiscountFixedChargeDetail = listDiscountFixedChargeDetail
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimProBag(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBag> listFixedChargeDetailTimProBag = null;
            PostpaidService.FixedChargeDetailTimProBagRequestPostPaid objFixedChargeDetailTimProBagRequest = new PostpaidService.FixedChargeDetailTimProBagRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimProBagResponsePostPaid objFixedChargeDetailTimProBagResponse = null;
            try
            {
                objFixedChargeDetailTimProBagResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProBagResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimProBag(objFixedChargeDetailTimProBagRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProBagRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProBagRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProBagResponse != null && objFixedChargeDetailTimProBagResponse.ListFixedChargeDetailTimProBag != null && objFixedChargeDetailTimProBagResponse.ListFixedChargeDetailTimProBag.Count > 0)
            {
                listFixedChargeDetailTimProBag = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBag>();
                foreach (PostpaidService.FixedChargeDetailTimProBagPostPaid item in objFixedChargeDetailTimProBagResponse.ListFixedChargeDetailTimProBag)
                {
                    listFixedChargeDetailTimProBag.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBag()
                    {
                        Quantity = item.Quantity,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Amount = item.Amount,

                    });
                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagModel()
            {
                listFixedChargeDetailTimProBag = listFixedChargeDetailTimProBag
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimProBagOne(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagOne> listFixedChargeDetailTimProBagOne = null;
            PostpaidService.FixedChargeDetailTimProBagOneRequestPostPaid objFixedChargeDetailTimProBagOneRequest = new PostpaidService.FixedChargeDetailTimProBagOneRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimProBagOneResponsePostPaid objFixedChargeDetailTimProBagOneResponse = null;
            try
            {
                objFixedChargeDetailTimProBagOneResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProBagOneResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimProBagOne(objFixedChargeDetailTimProBagOneRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProBagOneRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProBagOneRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProBagOneResponse != null && objFixedChargeDetailTimProBagOneResponse.ListFixedChargeDetailTimProBagOne != null && objFixedChargeDetailTimProBagOneResponse.ListFixedChargeDetailTimProBagOne.Count > 0)
            {
                listFixedChargeDetailTimProBagOne = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagOne>();
                foreach (PostpaidService.FixedChargeDetailTimProBagOnePostPaid item in objFixedChargeDetailTimProBagOneResponse.ListFixedChargeDetailTimProBagOne)
                {
                    listFixedChargeDetailTimProBagOne.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagOne()
                    {

                        Quantity = item.Quantity,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Amount = item.Amount,

                    });
                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagOneModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagOneModel()
            {
                listFixedChargeDetailTimProBagOne = listFixedChargeDetailTimProBagOne
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimProBagTwo(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagTwo> listFixedChargeDetailTimProBagTwo = null;
            PostpaidService.FixedChargeDetailTimProBagTwoRequestPostPaid objFixedChargeDetailTimProBagTwoRequest = new PostpaidService.FixedChargeDetailTimProBagTwoRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimProBagTwoResponsePostPaid objFixedChargeDetailTimProBagTwoResponse = null;
            try
            {
                objFixedChargeDetailTimProBagTwoResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProBagTwoResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimProBagTwo(objFixedChargeDetailTimProBagTwoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProBagTwoRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProBagTwoRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProBagTwoResponse != null && objFixedChargeDetailTimProBagTwoResponse.ListFixedChargeDetailTimProBagTwo != null && objFixedChargeDetailTimProBagTwoResponse.ListFixedChargeDetailTimProBagTwo.Count > 0)
            {
                listFixedChargeDetailTimProBagTwo = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagTwo>();

                foreach (PostpaidService.FixedChargeDetailTimProBagTwoPostPaid item in objFixedChargeDetailTimProBagTwoResponse.ListFixedChargeDetailTimProBagTwo)
                {
                    listFixedChargeDetailTimProBagTwo.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagTwo()
                    {

                        Quantity = item.Quantity,
                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Amount = item.Amount,

                    });

                }
            }
            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagTwoModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagTwoModel()
            {
                listFixedChargeDetailTimProBagTwo = listFixedChargeDetailTimProBagTwo
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetFixedChargeDetailTimProBagThree(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagThree> listFixedChargeDetailTimProBagThree = null;
            PostpaidService.FixedChargeDetailTimProBagThreeRequestPostPaid objFixedChargeDetailTimProBagThreeRequest = new PostpaidService.FixedChargeDetailTimProBagThreeRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeDetailTimProBagThreeResponsePostPaid objFixedChargeDetailTimProBagThreeResponse = null;
            try
            {
                objFixedChargeDetailTimProBagThreeResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeDetailTimProBagThreeResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeDetailTimProBagThree(objFixedChargeDetailTimProBagThreeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeDetailTimProBagThreeRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeDetailTimProBagThreeRequest.audit.transaction);
            }

            if (objFixedChargeDetailTimProBagThreeResponse != null && objFixedChargeDetailTimProBagThreeResponse.ListFixedChargeDetailTimProBagThree != null && objFixedChargeDetailTimProBagThreeResponse.ListFixedChargeDetailTimProBagThree.Count > 0)
            {
                listFixedChargeDetailTimProBagThree = new List<HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagThree>();
                foreach (PostpaidService.FixedChargeDetailTimProBagThreePostPaid item in objFixedChargeDetailTimProBagThreeResponse.ListFixedChargeDetailTimProBagThree)
                {
                    listFixedChargeDetailTimProBagThree.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeDetailTimProBagThree()
                    {

                        RatePlan = item.RatePlan,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3,
                        Fu4 = "",
                        Amount = item.Amount,
                        Quantity = item.Quantity
                    });
                }
            }

            Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagThreeModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeDetailTimProBagThreeModel()
            {
                listFixedChargeDetailTimProBagThree = listFixedChargeDetailTimProBagThree
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingFixedChargeTimProBag()
        {
            return View();
        }

        public JsonResult GetFixedChargeTimProBagDetail(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetail> listFixedChargeTimProBagDetail = null;
            PostpaidService.FixedChargeTimProBagDetailRequestPostPaid objFixedChargeTimProBagDetailRequest = new PostpaidService.FixedChargeTimProBagDetailRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };


            PostpaidService.FixedChargeTimProBagDetailResponsePostPaid objFixedChargeTimProBagDetailResponse = null;
            try
            {
                objFixedChargeTimProBagDetailResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeTimProBagDetailResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeTimProBagDetail(objFixedChargeTimProBagDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeTimProBagDetailRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeTimProBagDetailRequest.audit.transaction);
            }

            if (objFixedChargeTimProBagDetailResponse != null && objFixedChargeTimProBagDetailResponse.ListFixedChargeTimProBagDetail != null && objFixedChargeTimProBagDetailResponse.ListFixedChargeTimProBagDetail.Count > 0)
            {
                listFixedChargeTimProBagDetail = new List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetail>();
                foreach (PostpaidService.FixedChargeTimProBagDetailPostPaid item in objFixedChargeTimProBagDetailResponse.ListFixedChargeTimProBagDetail)
                {

                    listFixedChargeTimProBagDetail.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetail()
                    {
                        Msisdn = item.Msisdn,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3
                    });

                }
            }
            Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailModel()
            {
                listFixedChargeTimProBagDetail = listFixedChargeTimProBagDetail
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingFixedChargeTimProBagOne()
        {
            return View();
        }

        public JsonResult GetFixedChargeTimProBagDetailOne(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailOne> listFixedChargeTimProBagDetailOne = null;
            PostpaidService.FixedChargeTimProBagDetailOneRequestPostPaid objFixedChargeTimProBagDetailOneRequest = new PostpaidService.FixedChargeTimProBagDetailOneRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeTimProBagDetailOneResponsePostPaid objFixedChargeTimProBagDetailOneResponse = null;
            try
            {
                objFixedChargeTimProBagDetailOneResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeTimProBagDetailOneResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeTimProBagDetailOne(objFixedChargeTimProBagDetailOneRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeTimProBagDetailOneRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeTimProBagDetailOneRequest.audit.transaction);
            }

            if (objFixedChargeTimProBagDetailOneResponse != null && objFixedChargeTimProBagDetailOneResponse.ListFixedChargeTimProBagDetailOne != null && objFixedChargeTimProBagDetailOneResponse.ListFixedChargeTimProBagDetailOne.Count > 0)
            {
                listFixedChargeTimProBagDetailOne = new List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailOne>();
                foreach (PostpaidService.FixedChargeTimProBagDetailOnePostPaid item in objFixedChargeTimProBagDetailOneResponse.ListFixedChargeTimProBagDetailOne)
                {
                    listFixedChargeTimProBagDetailOne.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailOne()
                    {
                        Msisdn = item.Msisdn,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1,
                        Fu2 = item.Fu2,
                        Fu3 = item.Fu3
                    });
                }
            }
            Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailOneModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailOneModel()
            {
                listFixedChargeTimProBagDetailOne = listFixedChargeTimProBagDetailOne
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingFixedChargeTimProBagTwo()
        {
            return View();
        }

        public JsonResult GetFixedChargeTimProBagDetailTwo(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailTwo> listFixedChargeTimProBagDetailTwo = null;
            PostpaidService.FixedChargeTimProBagDetailTwoRequestPostPaid objFixedChargeTimProBagDetailTwoRequest = new PostpaidService.FixedChargeTimProBagDetailTwoRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeTimProBagDetailTwoResponsePostPaid objFixedChargeTimProBagDetailTwoResponse = null;
            try
            {
                objFixedChargeTimProBagDetailTwoResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeTimProBagDetailTwoResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeTimProBagDetailTwo(objFixedChargeTimProBagDetailTwoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeTimProBagDetailTwoRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeTimProBagDetailTwoRequest.audit.transaction);
            }

            if (objFixedChargeTimProBagDetailTwoResponse != null && objFixedChargeTimProBagDetailTwoResponse.ListFixedChargeTimProBagDetailTwo != null && objFixedChargeTimProBagDetailTwoResponse.ListFixedChargeTimProBagDetailTwo.Count > 0)
            {
                listFixedChargeTimProBagDetailTwo = new List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailTwo>();
                foreach (PostpaidService.FixedChargeTimProBagDetailTwoPostPaid item in objFixedChargeTimProBagDetailTwoResponse.ListFixedChargeTimProBagDetailTwo)
                {

                    listFixedChargeTimProBagDetailTwo.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailTwo()
                    {
                        Msisdn = item.Msisdn,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu1 = item.Fu1

                    });

                }
            }
            Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailTwoModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailTwoModel()
            {
                listFixedChargeTimProBagDetailTwo = listFixedChargeTimProBagDetailTwo
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingFixedChargeTimProBagThree()
        {
            return View();
        }

        public JsonResult GetFixedChargeTimProBagDetailThree(string strIdSession, string intGroupBox, string strInvoiceNumber)
        {
            List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailThree> listFixedChargeTimProBagDetailThree = null;
            PostpaidService.FixedChargeTimProBagDetailThreeRequestPostPaid objFixedChargeTimProBagDetailThreeRequest = new PostpaidService.FixedChargeTimProBagDetailThreeRequestPostPaid()
            {
                GroupBox = intGroupBox,
                InvoiceNumber = strInvoiceNumber,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.FixedChargeTimProBagDetailThreeResponsePostPaid objFixedChargeTimProBagDetailThreeResponse = null;
            try
            {
                objFixedChargeTimProBagDetailThreeResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.FixedChargeTimProBagDetailThreeResponsePostPaid>(() => { return objPostpaidService.GetFixedChargeTimProBagDetailThree(objFixedChargeTimProBagDetailThreeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFixedChargeTimProBagDetailThreeRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFixedChargeTimProBagDetailThreeRequest.audit.transaction);
            }

            if (objFixedChargeTimProBagDetailThreeResponse != null && objFixedChargeTimProBagDetailThreeResponse.ListFixedChargeTimProBagDetailThree != null && objFixedChargeTimProBagDetailThreeResponse.ListFixedChargeTimProBagDetailThree.Count > 0)
            {
                listFixedChargeTimProBagDetailThree = new List<HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailThree>();
                foreach (PostpaidService.FixedChargeTimProBagDetailThreePostPaid item in objFixedChargeTimProBagDetailThreeResponse.ListFixedChargeTimProBagDetailThree)
                {
                    listFixedChargeTimProBagDetailThree.Add(new HELPER_DATA.FixedChargeHelper.FixedChargeTimProBagDetailThree()
                    {
                        Msisdn = item.Msisdn,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        Fu3 = item.Fu3

                    });

                }
            }
            Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailThreeModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingFixedChargeTimProBagDetailThreeModel()
            {
                listFixedChargeTimProBagDetailThree = listFixedChargeTimProBagDetailThree
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingTimProAdditionalLocalTrafficDetail()
        {
            return View();
        }

        public ActionResult BillingTimMaxAdditionalLocalTrafficDetail()
        {
            return View();
        }

        public JsonResult ExistFile(string strFilePath, string strFileName, string strIdSesion)
        {
           
            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSesion);
            bool ExistFile;
            FileStreamResult objFile;
            try
            {
                Claro.Web.Logging.Info(strIdSesion, objAudit.transaction, "Metodo ExistFile - Parametros de Entrada: strFilePath-" + strFilePath + ", strFileName-" + strFileName);
                objFile = ShowInvoice(strIdSesion, strFilePath, strFileName, "NO");
                ExistFile = true;
            }
            catch (Exception ex)
            {
                ExistFile = false;
                objFile = null;
                Claro.Web.Logging.Error(strIdSesion, objAudit.transaction, ex.Message);

            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExistFileTOBE(string strFilePath, string strFileName, string strIdSesion,string strcustomerId,string strNroInvoice)
        {

            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSesion);
            bool ExistFile;
            FileStreamResult objFile;
            try
            {
                Claro.Web.Logging.Info(strIdSesion, objAudit.transaction, "Metodo ExistFile - Parametros de Entrada: strFilePath-" + strFilePath + ", strFileName-" + strFileName);
                objFile = ShowInvoiceTOBE(strIdSesion, strFilePath, strFileName, strcustomerId,strNroInvoice);
                if (objFile==null)
                {
                    ExistFile = false;  
                }
                else
                {
                    ExistFile = true;   
                }
                
            }
            catch (Exception ex)
            {
                ExistFile = false;
                objFile = null;
                Claro.Web.Logging.Error(strIdSesion, objAudit.transaction, ex.Message);

            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }
        public FileStreamResult ShowInvoiceTOBE(string strIdSession, string strFilePath, string strFileName, string strcustomerId,string strNroInvoice)
        {
            string strExtension = "", idOnBase;
            byte[] arrBuffer = null;
            string strTypeMIME = "application/pdf", strPeriodo, numeroRecibo;

            
            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;

            try
            {
                DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                Claro.Web.Logging.Info(strIdSession, objAudit.transaction, "Metodo ShowInvoiceTOBE - Parametros de Entrada: strFilePath-" + strFilePath + ", strFileName-" + strFileName + ",strcustomerId: " + strcustomerId + ",strNroInvoice: " + strNroInvoice);
               
                DashboardService.InvoiceRequest request = new DashboardService.InvoiceRequest()
                {
                   customerId = strcustomerId,
                   nroRecibo = strNroInvoice,
                   audit=objAudit,
                   cantNroRecibo="1"
                };

                DashboardService.InvoiceResponse response = Claro.Web.Logging.ExecuteMethod<DashboardService.InvoiceResponse>(() =>
                { return new DashboardService.DashboardServiceClient().GetIdOnBase(request); });

                idOnBase = response.idonbase;
                strPeriodo = response.periodo;
                numeroRecibo = response.nroRecibo;

                strFileName = strFileName.Replace('|', '\\');
                strFilePath = strFilePath.Replace('|', '\\');
                strExtension = System.IO.Path.GetExtension(strFilePath + strFileName).Remove(0, 1);
                DashboardService.GetTypeMIMERequestDashboard objGetTypeMIMERequestDashboard = new DashboardService.GetTypeMIMERequestDashboard()
                {
                    audit = objAudit,
                    Extension = strExtension
                };
               
                DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                {                  
                    Path = strFileName,
                    DateRegister = strPeriodo,
                    audit = objAudit,
                    strIdOnBase = idOnBase,
                    strNumeroRecibo=numeroRecibo
                };

                objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjusteV3(objFileDefaultImpersonationRequestDashboard); });
                if (objFileDefaultImpersonationResponseDashboard != null)
                {
                    arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw;
            }
           
            //strFileName = strFileName.Replace("\\", "-");

            Stream stream = new MemoryStream(arrBuffer);
            Response.AppendHeader("content-disposition", "inline; filename=" + strFileName + ".pdf");
            return new FileStreamResult(stream, strTypeMIME);
        }


        public FileStreamResult ShowInvoice(string strIdSession, string strFilePath, string strFileName, string strNameForm)
        {
           
            string strExtension = "";
            byte[] arrBuffer = null;
            string strTypeMIME = "";
            DashboardService.FileInvoiceResponseDashboard objFileInvoiceResponseDashboard = null;
            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;
            FileService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseFile = null;

            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            FileService.AuditRequest objAuditFileService = App_Code.Common.CreateAuditRequest<FileService.AuditRequest>(strIdSession);

            strFilePath = strFilePath.Replace("|", @"\");
            strFileName = strFileName.Replace("|", @"\");

            Claro.Web.Logging.Info(strIdSession, objAudit.transaction, strFilePath + " " + strFileName);
            try
            {
                strExtension = System.IO.Path.GetExtension(strFilePath + strFileName).Remove(0, 1);
                DashboardService.GetTypeMIMERequestDashboard objGetTypeMIMERequestDashboard = new DashboardService.GetTypeMIMERequestDashboard()
                {
                    audit = objAudit,
                    Extension = strExtension
                };
                DashboardService.GetTypeMIMEResponseDashboard objGetTypeMIMEResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.GetTypeMIMEResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetTypeMIME(objGetTypeMIMERequestDashboard); });
                strTypeMIME = objGetTypeMIMEResponseDashboard.TypeMime;
             
                if (strNameForm == Claro.ConfigurationManager.AppSettings("strTransAjusteDA"))
                {

                    DashboardService.FileInvoiceRequestDashboard objDebtDetailRequest = new DashboardService.FileInvoiceRequestDashboard()
                    {
                        Path = strFilePath + strFileName,
                        audit = objAudit
                    };
                    objFileInvoiceResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileInvoiceResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetFileInvoice(objDebtDetailRequest); });

                }
                else
                {
                    FileService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new FileService.FileDefaultImpersonationRequestDashboard()
                    {
                        Path = strFilePath + strFileName,
                        audit = objAuditFileService
                    };
                 
                    objFileDefaultImpersonationResponseFile = Claro.Web.Logging.ExecuteMethod<FileService.FileDefaultImpersonationResponseDashboard>(1,() => { return new FileService.FileServiceClient().GetFileDefaultImpersonation(objFileDefaultImpersonationRequestDashboard); });

                }


                arrBuffer = objFileDefaultImpersonationResponseFile.ObjGlobalDocument.Documento;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAudit.transaction, ex.Message);

            }
            strFileName = strFileName.Replace("\\", "-");
            
            Stream stream = new MemoryStream(arrBuffer);
            Response.AppendHeader("content-disposition", "inline; filename=" + strFileName + ".pdf");
            return new FileStreamResult(stream, strTypeMIME);
        }

        public FileContentResult ShowInvoiceFtp(string strIdSession, string strFilePath, string strFileName)
        {
            string strExtension;
            byte[] arrBuffer = null;
            string strTypeMIME = "";
            strExtension = System.IO.Path.GetExtension(strFilePath + strFileName).Remove(0, 1);
            DashboardService.GetTypeMIMERequestDashboard objGetTypeMIMERequestDashboard = new DashboardService.GetTypeMIMERequestDashboard()
            {
                Extension = strExtension,
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
            };

            try
            {
                strExtension = System.IO.Path.GetExtension(strFilePath + strFileName).Remove(0, 1);
                DashboardService.GetTypeMIMEResponseDashboard objGetTypeMIMEResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.GetTypeMIMEResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetTypeMIME(objGetTypeMIMERequestDashboard); });
                strTypeMIME = objGetTypeMIMEResponseDashboard.TypeMime;

                DashboardService.InvoiceFtpRequestDashboard objInvoiceFtpRequestDashboard = new DashboardService.InvoiceFtpRequestDashboard()
                    {
                        filePath = strFilePath,
                        fileName = strFileName,
                        audit = objGetTypeMIMERequestDashboard.audit
                    };

                DashboardService.InvoiceFtpResponseDashboard objInvoiceFtpResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.InvoiceFtpResponseDashboard>(1,() => { return new DashboardService.DashboardServiceClient().GetInvoiceFtp(objInvoiceFtpRequestDashboard); });
                arrBuffer = objInvoiceFtpResponseDashboard.ObjGlobalDocument.Documento;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetTypeMIMERequestDashboard.audit.transaction, ex.Message);
            }

            
            return File(arrBuffer, strTypeMIME);
        }

        public JsonResult GetLocalTrafficDetailTP(string strIdSession, string strInvoiceNumber, string strMsisdn, string strTypeCallTPCol)
        {
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTP> listLocalTrafficDetailCallTP = null;
            PostpaidService.LocalTrafficDetailCallTPRequestPostPaid objLocalTrafficDetailCallTPRequest = new PostpaidService.LocalTrafficDetailCallTPRequestPostPaid()
            {
                InvoiceNumber = strInvoiceNumber,
                Msisdn = strMsisdn,
                TypeCallTP = strTypeCallTPCol,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.LocalTrafficDetailCallTPResponsePostPaid objLocalTrafficDetailCallTPResponse = null;
            try
            {
                objLocalTrafficDetailCallTPResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.LocalTrafficDetailCallTPResponsePostPaid>(() => { return objPostpaidService.LocalTrafficDetailCallTP(objLocalTrafficDetailCallTPRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objLocalTrafficDetailCallTPRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objLocalTrafficDetailCallTPRequest.audit.transaction);
            }

            if (objLocalTrafficDetailCallTPResponse != null && objLocalTrafficDetailCallTPResponse.ListLocalTrafficDetailCallTP != null && objLocalTrafficDetailCallTPResponse.ListLocalTrafficDetailCallTP.Count > 0)
            {
                listLocalTrafficDetailCallTP = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTP>();
                foreach (PostpaidService.LocalTrafficDetailCallTP item in objLocalTrafficDetailCallTPResponse.ListLocalTrafficDetailCallTP)
                {
                    listLocalTrafficDetailCallTP.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTP()
                    {
                        CALLDURATION = item.CALLDURATION,
                        CALLTIME = item.CALLTIME,
                        CALLTIMEFIN = item.CALLTIMEFIN,
                        CALLDATEFIN = item.CALLDATEFIN,
                        CALLNUMBER = item.CALLNUMBER,
                        CALLDATE = item.CALLDATE,
                        CALLDESTINATION = item.CALLDESTINATION,
                        CALLDURATIONMIN = item.CALLDURATIONMIN,
                        CALLTOTAL = item.CALLTOTAL,
                        CALLAMBITO = item.CALLAMBITO

                    });

                }
            }

            Models.PostpaidDataBilling.BillingLocalTrafficDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingLocalTrafficDetailModel()
            {
                LocalTrafficQuantityCallTP = objLocalTrafficDetailCallTPResponse.LocalTrafficQuantityCallTP,
                listLocalTrafficDetailCallTP = listLocalTrafficDetailCallTP
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetLocalTrafficDetailTM(string strIdSession, string strInvoiceNumber, string strMsisdn, string strTypeCallTMCol)
        {
            
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTM> listLocalTrafficDetailCallTM = null;

            PostpaidService.LocalTrafficDetailCallTMRequestPostPaid objLocalTrafficDetailCallTMRequest = new PostpaidService.LocalTrafficDetailCallTMRequestPostPaid()
            {
                InvoiceNumber = strInvoiceNumber,
                Msisdn = strMsisdn,
                TypeCallTM = strTypeCallTMCol,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.LocalTrafficDetailCallTMResponsePostPaid objLocalTrafficDetailCallTMResponse = null;
            try
            {
                objLocalTrafficDetailCallTMResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.LocalTrafficDetailCallTMResponsePostPaid>(() => { return objPostpaidService.LocalTrafficDetailCallTM(objLocalTrafficDetailCallTMRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objLocalTrafficDetailCallTMRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objLocalTrafficDetailCallTMRequest.audit.transaction);
            }

            if (objLocalTrafficDetailCallTMResponse != null && objLocalTrafficDetailCallTMResponse.ListLocalTrafficDetailCallTM != null && objLocalTrafficDetailCallTMResponse.ListLocalTrafficDetailCallTM.Count > 0)
            {
                listLocalTrafficDetailCallTM = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTM>();
                foreach (PostpaidService.LocalTrafficDetailCallTM item in objLocalTrafficDetailCallTMResponse.ListLocalTrafficDetailCallTM)
                {
                    listLocalTrafficDetailCallTM.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTM()
                    {
                        CALLDURATION = item.CALLDURATION,
                        CALLTIME = item.CALLTIME,
                        CALLTIMEFIN = item.CALLTIMEFIN,
                        CALLDATEFIN = item.CALLDATEFIN,
                        CALLNUMBER = item.CALLNUMBER,
                        CALLDATE = item.CALLDATE,
                        CALLDESTINATION = item.CALLDESTINATION,
                        CALLDURATIONMIN = item.CALLDURATIONMIN,
                        CALLTOTAL = item.CALLTOTAL,
                        CALLAMBITO = item.CALLAMBITO

                    });

                }
            }
            Models.PostpaidDataBilling.BillingLocalTrafficDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingLocalTrafficDetailModel()
            {
                LocalTrafficQuantityCallTM = objLocalTrafficDetailCallTMResponse.LocalTrafficQuantityCallTM,
                listLocalTrafficDetailCallTM = listLocalTrafficDetailCallTM
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingTimProConsumeLocalTrafficDetail()
        {
            return View();
        }

        public ActionResult BillingTimMaxConsumeLocalTrafficDetail()
        {
            return View();
        }

        public JsonResult GetConsumeLocalTrafficDetailTP(string strIdSession, string strInvoiceNumber, string strMsisdn, string strTypeCallTPCol)
        {
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTP> listConsumeLocalTrafficDetailCallTP = null;
            PostpaidService.ConsumeLocalTrafficDetailCallTPRequestPostPaid objConsumeLocalTrafficDetailCallTPRequest = new PostpaidService.ConsumeLocalTrafficDetailCallTPRequestPostPaid()
            {
                InvoiceNumber = strInvoiceNumber,
                Msisdn = strMsisdn,
                TypeCallTP = strTypeCallTPCol,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.ConsumeLocalTrafficDetailCallTPResponsePostPaid objConsumeLocalTrafficDetailCallTPResponse;
            try
            {
                objConsumeLocalTrafficDetailCallTPResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.ConsumeLocalTrafficDetailCallTPResponsePostPaid>(() => { return objPostpaidService.ConsumeLocalTrafficDetailCallTP(objConsumeLocalTrafficDetailCallTPRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objConsumeLocalTrafficDetailCallTPRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objConsumeLocalTrafficDetailCallTPRequest.audit.transaction);
            }

            if (objConsumeLocalTrafficDetailCallTPResponse != null && objConsumeLocalTrafficDetailCallTPResponse.ListConsumeLocalTrafficDetailCallTP != null && objConsumeLocalTrafficDetailCallTPResponse.ListConsumeLocalTrafficDetailCallTP.Count > 0)
            {
                listConsumeLocalTrafficDetailCallTP = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTP>();
                foreach (PostpaidService.ConsumeLocalTrafficDetailCallTP item in objConsumeLocalTrafficDetailCallTPResponse.ListConsumeLocalTrafficDetailCallTP)
                {
                    listConsumeLocalTrafficDetailCallTP.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTP()
                    {
                        CALLDURATION = item.CALLDURATION,
                        CALLTIME = item.CALLTIME,
                        CALLTIMEFIN = item.CALLTIMEFIN,
                        CALLDATEFIN = item.CALLDATEFIN,
                        CALLNUMBER = item.CALLNUMBER,
                        CALLDATE = item.CALLDATE,
                        CALLDESTINATION = item.CALLDESTINATION,
                        CALLDURATIONMIN = item.CALLDURATIONMIN,
                        CALLTOTAL = item.CALLTOTAL,
                        CALLAMBITO = item.CALLAMBITO
                    });
                }
            }

            Models.PostpaidDataBilling.BillingLocalTrafficDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingLocalTrafficDetailModel()
            {
                ConsumeLocalTrafficQuantityCallTP = objConsumeLocalTrafficDetailCallTPResponse.ConsumeLocalTrafficQuantityCallTP,
                listConsumeLocalTrafficDetailCallTP = listConsumeLocalTrafficDetailCallTP
            };

            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetConsumeLocalTrafficDetailTM(string strIdSession, string strInvoiceNumber, string strMsisdn, string strTypeCallTMCol)
        {
            List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTM> listConsumeLocalTrafficDetailCallTM = null;
            PostpaidService.ConsumeLocalTrafficDetailCallTMRequestPostPaid objConsumeLocalTrafficDetailCallTMRequest = new PostpaidService.ConsumeLocalTrafficDetailCallTMRequestPostPaid()
              {
                  InvoiceNumber = strInvoiceNumber,
                  Msisdn = strMsisdn,
                  TypeCallTM = strTypeCallTMCol,
                  audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
              };

            PostpaidService.ConsumeLocalTrafficDetailCallTMResponsePostPaid objConsumeLocalTrafficDetailCallTMResponse = null;
            try
            {
                objConsumeLocalTrafficDetailCallTMResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.ConsumeLocalTrafficDetailCallTMResponsePostPaid>(() => { return objPostpaidService.ConsumeLocalTrafficDetailCallTM(objConsumeLocalTrafficDetailCallTMRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objConsumeLocalTrafficDetailCallTMRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objConsumeLocalTrafficDetailCallTMRequest.audit.transaction);
            }

            if (objConsumeLocalTrafficDetailCallTMResponse != null && objConsumeLocalTrafficDetailCallTMResponse.ListConsumeLocalTrafficDetailCallTM != null && objConsumeLocalTrafficDetailCallTMResponse.ListConsumeLocalTrafficDetailCallTM.Count > 0)
            {
                listConsumeLocalTrafficDetailCallTM = new List<HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTM>();
                foreach (PostpaidService.ConsumeLocalTrafficDetailCallTM item in objConsumeLocalTrafficDetailCallTMResponse.ListConsumeLocalTrafficDetailCallTM)
                {
                    listConsumeLocalTrafficDetailCallTM.Add(new HELPER_DATA.LocalTrafficDetail.LocalTrafficDetailCallTM()
                    {
                        CALLDURATION = item.CALLDURATION,
                        CALLTIME = item.CALLTIME,
                        CALLTIMEFIN = item.CALLTIMEFIN,
                        CALLDATEFIN = item.CALLDATEFIN,
                        CALLNUMBER = item.CALLNUMBER,
                        CALLDATE = item.CALLDATE,
                        CALLDESTINATION = item.CALLDESTINATION,
                        CALLDURATIONMIN = item.CALLDURATIONMIN,
                        CALLTOTAL = item.CALLTOTAL,
                        CALLAMBITO = item.CALLAMBITO
                    });

                }
            }
            Models.PostpaidDataBilling.BillingLocalTrafficDetailModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingLocalTrafficDetailModel()
            {
                ConsumeLocalTrafficQuantityCallTM = objConsumeLocalTrafficDetailCallTMResponse.ConsumeLocalTrafficQuantityCallTM,
                listConsumeLocalTrafficDetailCallTM = listConsumeLocalTrafficDetailCallTM
            };

            return Json(new { data = oPostpaidDataBillingModel });
        }

        public ActionResult BillingHistoryHR()
        {
            return View();
        }

        public JsonResult GetHistoryHR(string strIdSession, string strCustomerId)
        {
            List<HELPER_DATA.BillingHistoryHR.HistoryHR> listHistoryHR = null;
            PostpaidService.HistoryHRRequestPostPaid objHistoryHRRequest = new PostpaidService.HistoryHRRequestPostPaid()
            {
                CustomerID = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.HistoryHRResponsePostPaid objHistoryHRResponsePostPaid = null;
            try
            {
                objHistoryHRResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.HistoryHRResponsePostPaid>(() => { return objPostpaidService.GetHistoryHR(objHistoryHRRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objHistoryHRRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objHistoryHRRequest.audit.transaction);
            }

            if (objHistoryHRResponsePostPaid != null && objHistoryHRResponsePostPaid.ListHistoryHR != null && objHistoryHRResponsePostPaid.ListHistoryHR.Count > 0)
            {
                listHistoryHR = new List<HELPER_DATA.BillingHistoryHR.HistoryHR>();
                foreach (PostpaidService.ReceiptHistory item in objHistoryHRResponsePostPaid.ListHistoryHR)
                {
                    listHistoryHR.Add(new HELPER_DATA.BillingHistoryHR.HistoryHR()
                    {
                        FechaEmision = item.FechaEmision,
                        FechaVencimiento = item.FechaVencimiento,
                        TotalCurrentCharges = Convert.ToDecimal(item.TotalCurrentCharges),
                        InvoiceNumber = item.InvoiceNumber,
                        Periodo = item.Periodo
                    });
                }
            }

            Models.PostpaidDataBilling.BillingHistoryHRModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingHistoryHRModel()
            {
                listHistoryHR = listHistoryHR
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetSMSDetail(string strIdSession, string strInvoiceNumber, string strMsisdn)
        {
            List<HELPER_DATA.LongDistance.CallDetailSMS> listCallDetailSMS = null;
            PostpaidService.SMSDetailsRequestPostPaid objSMSDetailsRequestPostPaid = new PostpaidService.SMSDetailsRequestPostPaid()
            {
                InvoiceNumber = strInvoiceNumber,
                Msisdn = strMsisdn,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            PostpaidService.SMSDetailsResponsePostPaid objSMSDetailsResponsePostPaid = null;
            try
            {
                objSMSDetailsResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.SMSDetailsResponsePostPaid>(() => { return objPostpaidService.SMSDetails(objSMSDetailsRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objSMSDetailsRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objSMSDetailsRequestPostPaid.audit.transaction);
            }

            if (objSMSDetailsResponsePostPaid != null && objSMSDetailsResponsePostPaid.ListCallDetail != null && objSMSDetailsResponsePostPaid.ListCallDetail.Count > 0)
            {
                listCallDetailSMS = new List<HELPER_DATA.LongDistance.CallDetailSMS>();
                foreach (PostpaidService.CallDetailSMSPostPaid item in objSMSDetailsResponsePostPaid.ListCallDetail)
                {
                    listCallDetailSMS.Add(new HELPER_DATA.LongDistance.CallDetailSMS()
                    {
                        SMSNUMBER = item.SMSNUMBER,
                        SMSDATE = item.SMSDATE,
                        SMSTIME = item.SMSTIME,
                        SMSTOTAL = item.SMSTOTAL,
                        SMSDESTINATION = item.SMSDESTINATION
                    });
                }
            }

            Models.PostpaidDataBilling.BillingDetailLongDistanceModel oPostpaidDataBillingModel = new Models.PostpaidDataBilling.BillingDetailLongDistanceModel()
            {
                decCantidadSMS = objSMSDetailsResponsePostPaid.DecCantidadSMS,
                listCallDetailSMS = listCallDetailSMS
            };
            return Json(new { data = oPostpaidDataBillingModel });
        }

        public JsonResult GetHistoryInvoiceFileName(string strIdSession, string strInvoiceNumber, string strFechaEmision, string strPeriodo)
        {
            Dashboard.Models.Postpaid.DataBillingModel objDataBillingModel = new Dashboard.Models.Postpaid.DataBillingModel();
            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            string strPathInvoice = Claro.ConfigurationManager.AppSettings("strDirectorioFacturas");

            if (strInvoiceNumber != null && strInvoiceNumber.Length > Claro.Constants.NumberZero)
            {

                objDataBillingModel.FlagBill = Claro.Constants.NumberOneString;
                objDataBillingModel.FilePath = strPathInvoice + "\\";


                DashboardService.ReceiptNumberRequestDashboard objReceiptNumberRequestDashboard = new DashboardService.ReceiptNumberRequestDashboard()
                {
                    InvoiceNumber = strInvoiceNumber,
                    EmissionDate = strFechaEmision,
                    audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
                };
                DashboardService.ReceiptNumberResponseDashboard objReceiptNumberResponseDashboard = null;
                try
                {
                    objReceiptNumberResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.ReceiptNumberResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetReceiptNumber(objReceiptNumberRequestDashboard); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objReceiptNumberRequestDashboard.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objReceiptNumberRequestDashboard.audit.transaction);
                }

                objDataBillingModel.FileName = strPeriodo.Trim() + "\\" + objReceiptNumberResponseDashboard.ReceiptNumber.Trim() + Claro.SIACU.Constants.PointPdf;

                FileStreamResult objFile;
                try
                {
                    objFile = ShowInvoice(strIdSession, objDataBillingModel.FilePath, objDataBillingModel.FileName, "NO");
                }
                catch (Exception ex)
                {
                    objDataBillingModel.FlagBill = Claro.Constants.NumberZeroString;
                    Claro.Web.Logging.Error(strIdSession, objAudit.transaction, ex.Message);

                }


            }
            else
            {
                objDataBillingModel.FlagBill = Claro.Constants.NumberZeroString;
            }

            return Json(new { data = objDataBillingModel });
        }


        public JsonResult GetHistoryHRFileName(string strIdSession, string strInvoiceNumber, string strFechaEmision, string strPeriodo)
        {
            Dashboard.Models.Postpaid.DataBillingModel objDataBillingModel = new Dashboard.Models.Postpaid.DataBillingModel();

            string pathInvoice = Claro.ConfigurationManager.AppSettings("strDirectorioHojasResumenes");
            DashboardService.ReceiptNumberResponseDashboard objReceiptNumberResponseDashboard = null;
            if (strInvoiceNumber != null && strInvoiceNumber.Length > Claro.Constants.NumberZero)
            {
                

                DashboardService.ReceiptNumberRequestDashboard objReceiptNumberRequestDashboard = new DashboardService.ReceiptNumberRequestDashboard()
                {
                    InvoiceNumber = strInvoiceNumber,
                    EmissionDate = strFechaEmision,
                    audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession)
                };


                try
                {
                    objReceiptNumberResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.ReceiptNumberResponseDashboard>(
                        () => { return new DashboardService.DashboardServiceClient().GetReceiptNumber(objReceiptNumberRequestDashboard); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objReceiptNumberRequestDashboard.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objReceiptNumberRequestDashboard.audit.transaction);
                }

            }
            else
            {
                objDataBillingModel.FlagBill = Claro.Constants.NumberZeroString;
                return Json(new { data = objDataBillingModel });
            }

            return Json( new PostPaidDataCollectionController().GetFileRute(strIdSession, objReceiptNumberResponseDashboard.ReceiptNumber, strPeriodo, "", true) );
        }

        public string PrintLinkHR(string strDocument, string strType)
        {
            string strDocumentEnd = "";
            string[] strTemDoc = strDocument.Split('-');

            if (strTemDoc[0].ToString().Trim() == ConfigurationManager.AppSettings("strNumeroDocumento")) strDocumentEnd = Claro.SIACU.Constants.HR_ + strTemDoc[1].ToString().Trim();
            strDocument = (strDocumentEnd.ToString().Trim() != "") ? strDocumentEnd : "";
            return strDocument;
        }
    }
}