using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using MODELS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;
using POSTPAID_DATACOLLECTION = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection;
using POSTPAID_HELPER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid;
using System.Linq;
using Claro.SIACU.Web.WebApplication.DashboardService;
using System.IO;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PostPaidDataCollectionController : Controller
    {
        private readonly PostpaidService.PostpaidServiceClient objPostpaidService = new PostpaidService.PostpaidServiceClient();
        private readonly CommonService.CommonServiceClient oServiceCommon = new CommonService.CommonServiceClient();
        private readonly DashboardService.DashboardServiceClient oDashboardService = new DashboardService.DashboardServiceClient();
        private readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailAmountDispute(string strIdSession, PostpaidService.DetailAmountDisputeRequestPostPaid objDetailAmountDisputeRequestPostPaid)
        {
            PostpaidService.DetailAmountDisputeResponsePostPaid objDetailAmountDisputeResponsePostPaid;

            try
            {
                objDetailAmountDisputeRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objDetailAmountDisputeResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.DetailAmountDisputeResponsePostPaid>(() => { return objPostpaidService.GetDetailAmountDispute(objDetailAmountDisputeRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDetailAmountDisputeRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDetailAmountDisputeRequestPostPaid.audit.transaction);
            }

            POSTPAID_DATACOLLECTION.CollectionDetailAmountDisputeModel oPostpaidDataAccountModel = null;

            if (objDetailAmountDisputeResponsePostPaid.ListDetailAmountDispute != null)
            {
                oPostpaidDataAccountModel = new POSTPAID_DATACOLLECTION.CollectionDetailAmountDisputeModel();
                List<POSTPAID_HELPER.CollectionDetailAmountDispute.CollectionDetailAmountDispute> ListCollectionDetailAmountDispute = new List<POSTPAID_HELPER.CollectionDetailAmountDispute.CollectionDetailAmountDispute>();

                foreach (PostpaidService.DetailAmountDisputePostPaid item in objDetailAmountDisputeResponsePostPaid.ListDetailAmountDispute)
                {
                    ListCollectionDetailAmountDispute.Add(new POSTPAID_HELPER.CollectionDetailAmountDispute.CollectionDetailAmountDispute()
                    {
                        ReferenceDocument = item.NRO_RECIBO,
                        CreateDateReferenceDocument = item.FECHA_EMI_DOC,
                        ReferenceDocumentAmount = item.MONTO_TOTAL_DOC,
                        ClaimAmount = item.MONTO_RECLAMO,
                        ClaimDate = item.FECHA_REG_RECLAMO,
                        ClaimStatus = item.ESTADO_RECLAMO,
                        ClaimReason = item.MOTIVO,
                        CaseClosingDate = item.FECHA_CIERRE,
                        CaseNumber = item.CASO_CLARIFY,
                        LastDayExoneration = item.ULTIMO_DIA
                    });
                }
                oPostpaidDataAccountModel.listDetailAmountDisputePast = ListCollectionDetailAmountDispute;
            }

            return PartialView(oPostpaidDataAccountModel);
        }

        public ActionResult PaymentCommitment(string strIdSession, string strIdCustomer)
        {
            List<POSTPAID_HELPER.CollectionStatusAccountHelper.CollectionPaymentCommitment> listAccountPostPaid = null;
            PostpaidService.PaymentCommitmentResponsePostPaid objPaymentCollectionResponse = null;
            PostpaidService.PaymentCommitmentRequestPostPaid objPaymentCollectionRequest = new PostpaidService.PaymentCommitmentRequestPostPaid
            {
                IdCustomer = strIdCustomer,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
            };

            try
            {
                objPaymentCollectionResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.PaymentCommitmentResponsePostPaid>(() => { return objPostpaidService.GetPaymentCommitment(objPaymentCollectionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objPaymentCollectionRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPaymentCollectionRequest.audit.transaction);
            }


            if ((objPaymentCollectionResponse != null) && objPaymentCollectionResponse.ListAccount != null)
            {
                listAccountPostPaid = new List<POSTPAID_HELPER.CollectionStatusAccountHelper.CollectionPaymentCommitment>();

                foreach (PostpaidService.AccountPostPaid item in objPaymentCollectionResponse.ListAccount)
                {
                    listAccountPostPaid.Add(new POSTPAID_HELPER.CollectionStatusAccountHelper.CollectionPaymentCommitment()
                    {
                        Amount = item.SALDO,
                        Autorize = item.NOMBRE,
                        Date = item.FECHA_EXPIRACION
                    });
                }


            }
            MODELS.PostpaidDataCollection.CollectionPaymentCommitmentModel objCollectionPaymentCommitmentModel = new MODELS.PostpaidDataCollection.CollectionPaymentCommitmentModel()
            {
                listCollectionPaymentCommitment = listAccountPostPaid
            };
            return View(objCollectionPaymentCommitmentModel);
        }

        public ActionResult AffiliationToDebit(string strIdSession, string strCustomerId, string FlagPlat, string csIdPub)
        {
            PostpaidService.AffiliationToDebitResponsePostPaid objAffiliationToDebitResponse = null;
            PostpaidService.AffiliationToDebitRequestPostPaid objAffiliationToDebitRequestPostPaid = new PostpaidService.AffiliationToDebitRequestPostPaid
            {
                CustomerId = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                csIdPub = csIdPub,
                flagPlataforma = FlagPlat
            };

            try
            {
                objAffiliationToDebitResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.AffiliationToDebitResponsePostPaid>(() => { return objPostpaidService.GetAffiliationToDebit(objAffiliationToDebitRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAffiliationToDebitRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objAffiliationToDebitRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataCollection.CollectionAffiliationToDebitModel oAffiliationToDebit = new Models.PostpaidDataCollection.CollectionAffiliationToDebitModel();

            if (objAffiliationToDebitResponse.ObjMethodPayment != null)
            {
                oAffiliationToDebit.StatusDescription = objAffiliationToDebitResponse.ObjMethodPayment.DES_ESTADO_SOLICITUD;
                oAffiliationToDebit.DebitTypeDescription = objAffiliationToDebitResponse.ObjMethodPayment.TIPO_DEBITO;
                oAffiliationToDebit.CardAccountNumber = objAffiliationToDebitResponse.ObjMethodPayment.NUMERO_CUENTA_TARJETA;
                oAffiliationToDebit.CardExpirationDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_EXPIRACION_TARJETA;
                oAffiliationToDebit.TypeAccountDescription = objAffiliationToDebitResponse.ObjMethodPayment.DESC_TIPO_CUENTA;
                oAffiliationToDebit.CurrencyDescription = objAffiliationToDebitResponse.ObjMethodPayment.DESC_MONEDA;
                oAffiliationToDebit.EntityDescription = objAffiliationToDebitResponse.ObjMethodPayment.DESCRIPCION_LARGA;
                oAffiliationToDebit.MaximumDebitAmount = objAffiliationToDebitResponse.ObjMethodPayment.MONTO_MAXIMO;
                oAffiliationToDebit.RequestDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_REGISTRO;
                oAffiliationToDebit.ApprovalDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_APROBADO;
                oAffiliationToDebit.DisaffiliationDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_DESAFILIACION;
                oAffiliationToDebit.RejectionReason = objAffiliationToDebitResponse.ObjMethodPayment.MOTIVO;
                oAffiliationToDebit.RequestNumber = objAffiliationToDebitResponse.ObjMethodPayment.NUMERO_SOLICITUD;
                oAffiliationToDebit.RegistrationDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_REGISTRO;
                oAffiliationToDebit.RejectionDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_RECHAZO;
                oAffiliationToDebit.RequestReason = objAffiliationToDebitResponse.ObjMethodPayment.MOTIVO_SOLICITUD;
                oAffiliationToDebit.EntityCode = objAffiliationToDebitResponse.ObjMethodPayment.COD_ENTIDAD;
                oAffiliationToDebit.OwnerIdCard = objAffiliationToDebitResponse.ObjMethodPayment.DNI_TITULAR;
                oAffiliationToDebit.OwnerName = objAffiliationToDebitResponse.ObjMethodPayment.NOMBRE_TITULAR;
                oAffiliationToDebit.ProcessDate = objAffiliationToDebitResponse.ObjMethodPayment.FECHA_PROCESO;
                oAffiliationToDebit.TypeAccountCode = objAffiliationToDebitResponse.ObjMethodPayment.TIPO_CUENTA_BANCARIA;
                oAffiliationToDebit.CurrencyCode = objAffiliationToDebitResponse.ObjMethodPayment.MONEDA;
            }

            return PartialView(oAffiliationToDebit);
        }

        public ActionResult DetailMonitoringCases(string strIdSession, string strCaseId, string strCustomerAccount, string strDateFrom, string strDateTo)
        {
            PostpaidService.MonitoringCasesResponsePostPaid objMonitoringCasesResponsePostPaid = null;
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionMonitoringCases.CollectionMonitoringCases> listMonitoringCasesPast = null;
            PostpaidService.MonitoringCasesRequestPostPaid objMonitoringCasesRequestPostPaid = new PostpaidService.MonitoringCasesRequestPostPaid
            {
                CaseId = strCaseId,
                CustomerAccount = strCustomerAccount,
                DateFrom = strDateFrom,
                DateTo = strDateTo,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objMonitoringCasesResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.MonitoringCasesResponsePostPaid>(
                    () => { return objPostpaidService.GetMonitoringCases(objMonitoringCasesRequestPostPaid); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objMonitoringCasesRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objMonitoringCasesRequestPostPaid.audit.transaction);
            }

            if (objMonitoringCasesResponsePostPaid != null && objMonitoringCasesResponsePostPaid.ListMonitoringCases != null)
            {
                listMonitoringCasesPast = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionMonitoringCases.CollectionMonitoringCases>();
                foreach (PostpaidService.MonitoringCasesPostPaid item in objMonitoringCasesResponsePostPaid.ListMonitoringCases)
                {
                    listMonitoringCasesPast.Add(new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionMonitoringCases.CollectionMonitoringCases()
                    {
                        CaseId = item.IDCASO,
                        ClarifyCase = item.CASOCLARIFY,
                        CreationDate = item.FECREGISTRO,
                        CustomerId = item.CLIENTE,
                        Modality = item.TIPOCLIENTE,
                        Segment = item.SEGMENTO,
                        Status = item.ESTADO,
                        Priority = item.PRIORIDAD,
                        Type = item.TIPO,
                        Class = item.CLASE,
                        SubClass = (item.SUBCLASE == "null" ? "" : item.SUBCLASE),
                        Amount = item.MONTO,
                        Owner = item.DUENO,
                        OwnerName = item.NOMBREDUENO,
                        Recurrence = item.RECURRENCIA,
                        ElapsedTime = item.TIEMPOTRANSCURRIDO,
                        CreationDate2 = item.FECHAREGISTRO
                    });
                }
            }
            POSTPAID_DATACOLLECTION.CollectionMonitoringCasesModel objCollectionMonitoringCasesModel = new POSTPAID_DATACOLLECTION.CollectionMonitoringCasesModel()
            {
                listMonitoringCasesPast = listMonitoringCasesPast
            };

            return PartialView(objCollectionMonitoringCasesModel);
        }

        public ActionResult TrackingDetail()
        {
            return PartialView();
        }
        public JsonResult GetCustomerInformation(string strIdSession, PostpaidService.CustomerInformationRequestPostPaid objCustomerInformationRequestPostPaid)
        {
            PostpaidService.CustomerInformationResponsePostPaid objCustomerInformationResponsePostPaid = null;

            try
            {
                objCustomerInformationRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objCustomerInformationResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.CustomerInformationResponsePostPaid>(() => { return objPostpaidService.GetCustomerInformation(objCustomerInformationRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCustomerInformationRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCustomerInformationRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.QueryOACModel objQueryOACModel = new Models.PostpaidDataService.QueryOACModel();

            if (objCustomerInformationResponsePostPaid.QueryOAC != null)
            {
                POSTPAID_HELPER.QueryOAC.QueryOAC objQueryOAC = new POSTPAID_HELPER.QueryOAC.QueryOAC()
                {
                    ClarifyId = objCustomerInformationResponsePostPaid.QueryOAC.IdClarify,
                    CustomerCode = objCustomerInformationResponsePostPaid.QueryOAC.CodigoCliente,
                    CustomerContact = objCustomerInformationResponsePostPaid.QueryOAC.ContactoCliente,
                    Recurrence = objCustomerInformationResponsePostPaid.QueryOAC.Recurrencia,
                    CustomerName = objCustomerInformationResponsePostPaid.QueryOAC.NombreCliente
                };

                objQueryOACModel.QueryOAC = objQueryOAC;
            }

            return Json(new { data = objQueryOACModel });
        }

        public JsonResult GetTrackingDetail(string strIdSession, PostpaidService.TrackingDetailRequestPostPaid objTrackingDetailRequestPostPaid)
        {
            PostpaidService.TrackingDetailResponsePostPaid objTrackingDetailResponsePostPaid = null;

            try
            {
                objTrackingDetailRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objTrackingDetailResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.TrackingDetailResponsePostPaid>(() => { return objPostpaidService.GetTrackingDetail(objTrackingDetailRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objTrackingDetailRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objTrackingDetailRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.QueryOACModel objQueryOACModel = new Models.PostpaidDataService.QueryOACModel();

            if (objTrackingDetailResponsePostPaid.QueryOACs != null)
            {
                List<POSTPAID_HELPER.QueryOAC.QueryOAC> listQueryOACs = new List<POSTPAID_HELPER.QueryOAC.QueryOAC>();

                foreach (PostpaidService.QueryOACPostPaid objQueryOACPostPaid in objTrackingDetailResponsePostPaid.QueryOACs)
                {
                    listQueryOACs.Add(new POSTPAID_HELPER.QueryOAC.QueryOAC()
                    {
                        LocationCase = objQueryOACPostPaid.UbicacionCaso,
                        CaseLevel = objQueryOACPostPaid.NivelCaso,
                        StateCase = objQueryOACPostPaid.EstadoCaso,
                        DateCase = objQueryOACPostPaid.FechaCaso,
                        UserRegistrationCase = objQueryOACPostPaid.UsuarioRegistroCaso,
                        OwnerCase = objQueryOACPostPaid.PropietarioCaso,
                        CommentCase = objQueryOACPostPaid.ComentarioCaso,
                        DateClosingCase = objQueryOACPostPaid.FechaCierreCaso,
                        ReopeningCase = objQueryOACPostPaid.ReaperturaCaso,
                        UserName = objQueryOACPostPaid.UsuarioNombre
                    });
                }
                objQueryOACModel.QueryOACs = listQueryOACs;
            }

            return Json(new { data = objQueryOACModel });
        }

        public ActionResult ClosuresAndReclosures()
        {
            return PartialView();
        }

        public JsonResult GetManagementOfClosures(string strIdSession, PostpaidService.ManagementOfClosuresRequestPostPaid objManagementOfClosuresRequestPostPaid)
        {
            PostpaidService.ManagementOfClosuresResponsePostPaid objManagementOfClosuresResponsePostPaid = null;

            try
            {
                objManagementOfClosuresRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objManagementOfClosuresResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ManagementOfClosuresResponsePostPaid>(() => { return objPostpaidService.GetManagementOfClosures(objManagementOfClosuresRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objManagementOfClosuresRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objManagementOfClosuresRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.ReclosuresModel objReclosuresModel = new Models.PostpaidDataService.ReclosuresModel();

            if (objManagementOfClosuresResponsePostPaid.Reclosures != null)
            {
                List<POSTPAID_HELPER.Reclosures.Reclosures> listReclosures = new List<POSTPAID_HELPER.Reclosures.Reclosures>();

                foreach (PostpaidService.ReclosuresPostPaid objReclosuresPostPaid in objManagementOfClosuresResponsePostPaid.Reclosures)
                {
                    listReclosures.Add(new POSTPAID_HELPER.Reclosures.Reclosures()
                    {
                        ClosureCase = objReclosuresPostPaid.CasoCierre,
                        Resolution = objReclosuresPostPaid.Resolucion,
                        Result = objReclosuresPostPaid.Resultado,
                        State = objReclosuresPostPaid.Estado,
                        DateSentToTheBank = objReclosuresPostPaid.FechaEnvioBanco
                    });
                }
                objReclosuresModel.ManagementOfClosures = listReclosures;
            }

            return Json(new { data = objReclosuresModel });
        }

        public JsonResult GetReopenOfTheCase(string strIdSession, PostpaidService.ReopenOfTheCaseRequestPostPaid objReopenOfTheCaseRequestPostPaid)
        {
            PostpaidService.ReopenOfTheCaseResponsePostPaid objReopenOfTheCaseResponsePostPaid = null;

            try
            {
                objReopenOfTheCaseRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objReopenOfTheCaseResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ReopenOfTheCaseResponsePostPaid>(() => { return objPostpaidService.GetReopenOfTheCase(objReopenOfTheCaseRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objReopenOfTheCaseRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objReopenOfTheCaseRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.ReclosuresModel objReclosuresModel = new Models.PostpaidDataService.ReclosuresModel();

            if (objReopenOfTheCaseResponsePostPaid.Reclosures != null)
            {
                List<POSTPAID_HELPER.Reclosures.Reclosures> listReclosures = new List<POSTPAID_HELPER.Reclosures.Reclosures>();

                foreach (PostpaidService.ReclosuresPostPaid objReclosuresPostPaid in objReopenOfTheCaseResponsePostPaid.Reclosures)
                {
                    listReclosures.Add(new POSTPAID_HELPER.Reclosures.Reclosures()
                    {
                        ReopeningCase = objReclosuresPostPaid.CasoReapertura,
                        ReapvPhaseClarify = objReclosuresPostPaid.ReapvFaseClarify,
                        ReapdReopeningProcess = objReclosuresPostPaid.ReapdProcesoReapertura
                    });
                }
                objReclosuresModel.ReopenOfTheCases = listReclosures;
            }

            return Json(new { data = objReclosuresModel });
        }

        public JsonResult GetSubTableTracking(string strIdSession, PostpaidService.SubTableTrackingRequestPostPaid objSubTableTrackingRequestPostPaid)
        {
            PostpaidService.SubTableTrackingResponsePostPaid objSubTableTrackingResponsePostPaid = null;

            try
            {
                objSubTableTrackingRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objSubTableTrackingResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.SubTableTrackingResponsePostPaid>(() => { return objPostpaidService.GetSubTableTracking(objSubTableTrackingRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objSubTableTrackingRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objSubTableTrackingRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.QueryOACModel objQueryOACModel = new Models.PostpaidDataService.QueryOACModel();

            if (objSubTableTrackingResponsePostPaid.QueryOACs != null)
            {
                List<POSTPAID_HELPER.QueryOAC.QueryOAC> listQueryOACs = new List<POSTPAID_HELPER.QueryOAC.QueryOAC>();

                foreach (PostpaidService.QueryOACPostPaid objQueryOACPostPaid in objSubTableTrackingResponsePostPaid.QueryOACs)
                {
                    listQueryOACs.Add(new POSTPAID_HELPER.QueryOAC.QueryOAC()
                    {
                        DocumentNumberCase = objQueryOACPostPaid.NumeroDocumentoCaso,
                        CaseClaimId = objQueryOACPostPaid.IdReclamoCaso,
                        DocumentTypeCase = objQueryOACPostPaid.TipoDocumentoCaso,
                        VariableCaseClaim = objQueryOACPostPaid.VariableReclamoCaso,
                        CaseTransactionType = objQueryOACPostPaid.TipoTransaccionCaso,
                        AmountCase = objQueryOACPostPaid.MontoCaso,
                        StateCase = objQueryOACPostPaid.EstadoCaso
                    });
                }
                objQueryOACModel.QueryOACs = listQueryOACs;
            }

            return Json(new { data = objQueryOACModel });
        }

        public JsonResult GetThirdTableTracking(string strIdSession, PostpaidService.ThirdTableTrackingRequestPostPaid objThirdTableTrackingRequestPostPaid)
        {
            PostpaidService.ThirdTableTrackingResponsePostPaid objThirdTableTrackingResponsePostPaid = null;

            try
            {
                objThirdTableTrackingRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objThirdTableTrackingResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.ThirdTableTrackingResponsePostPaid>(() => { return objPostpaidService.GetThirdTableTracking(objThirdTableTrackingRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objThirdTableTrackingRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objThirdTableTrackingRequestPostPaid.audit.transaction);
            }

            Models.PostpaidDataService.QueryOACModel objQueryOACModel = new Models.PostpaidDataService.QueryOACModel();

            if (objThirdTableTrackingResponsePostPaid.QueryOACs != null)
            {
                List<POSTPAID_HELPER.QueryOAC.QueryOAC> listQueryOACs = new List<POSTPAID_HELPER.QueryOAC.QueryOAC>();

                foreach (PostpaidService.QueryOACPostPaid objQueryOACPostPaid in objThirdTableTrackingResponsePostPaid.QueryOACs)
                {
                    listQueryOACs.Add(new POSTPAID_HELPER.QueryOAC.QueryOAC()
                    {
                        IdCaseClarify = objQueryOACPostPaid.CasoIdClarify,
                        MemdvName = objQueryOACPostPaid.MemdvNombre,
                        EnvimdShipping = objQueryOACPostPaid.EnvimdEnvio,
                        EnvimdAmount = objQueryOACPostPaid.EnvimdMonto
                    });
                }
                objQueryOACModel.QueryOACs = listQueryOACs;
            }

            return Json(new { data = objQueryOACModel });
        }


        #region [EstadoCuenta]


        public ActionResult StatusAccount(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId)
        {
            return PartialView();
        }
        public JsonResult GetValuesConfig(string strIdSession, string strOriginType, string strIsLTE)
        {
            DashboardService.AuditRequest audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            string value;
            value = oDashboardService.GetParameterByCode(strIdSession, audit.transaction, int.Parse(ConfigurationManager.AppSettings("gConstAjusteSarCodParaConti")));
            return Json(new
            {
                FlagContigencia = value,
                Option = (strOriginType.Equals("POSTPAID") || strOriginType.Equals("DTH") || strOriginType.Equals("IFI") ? ConfigurationManager.AppSettings("gConstAjusteSarOpcRegAju") : (strOriginType.Equals("LTE") ? ConfigurationManager.AppSettings("gConstAjusteSarOpcRegAjuLTE") : (strOriginType.Equals("HFC") ? ConfigurationManager.AppSettings("gConstAjusteSarOpcRegAjuHFC") : ""))),
                MsjAlertNotPermission = ConfigurationManager.AppSettings("gConstAjusteSarMsjPermRA"),
                MsjAlertSiacNoDisponible = ConfigurationManager.AppSettings("gConstAjusteSarMsjConti"),
                Transaction = ((strOriginType.Equals("LTE") ? "TRANSACCION_REGISTRO_AJUSTES_LTE" : (strOriginType.Equals("HFC") ? "TRANSACCION_REGISTRO_AJUSTES_HFC" : (strOriginType.Equals("DTH") ? "TRANSACCION_DTH_AJUSTE_DA" : ""))))
            });
        }
        public JsonResult StatusAccounts(string strIdSession, string strCustomerId, string strCsIdPub, string strplataform, string strNameClient, string strNumberServices, string strContactId, string strOriginType)
        {
            POSTPAID_DATACOLLECTION.CollectionStatusAccountModel list = ListStatusAccount(strIdSession, strCustomerId, strCsIdPub, strplataform, strNameClient, strNumberServices, strContactId, strOriginType);
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);


            return Json(new
            {
                list = list
            });
        }

        #region GenerarConstanciaEstadoCuenta


        public JsonResult ExistFileTOBE(string strFilePath, string strFileName, string strIdSesion, string strcustomerId, string strNroInvoice)
        {

            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSesion);
            bool ExistFile;
            FileStreamResult objFile;
            try
            {
                Claro.Web.Logging.Info(strIdSesion, objAudit.transaction, "Metodo ExistFile - Parametros de Entrada: strFilePath-" + strFilePath + ", strFileName-" + strFileName);
                objFile = ShowInvoiceTOBE(strIdSesion, strFilePath, strFileName, strcustomerId, strNroInvoice);
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
        public FileStreamResult ShowInvoiceTOBE(string strIdSession, string strFilePath, string strFileName, string strcustomerId, string strNroInvoice)
        {
            string strExtension = "", idOnBase;
            byte[] arrBuffer = null;
            string strTypeMIME = "", strPeriodo, numeroRecibo;


            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;

            try
            {
                DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                Claro.Web.Logging.Info(strIdSession, objAudit.transaction, "Metodo ShowInvoiceTOBE - Parametros de Entrada: strFilePath-" + strFilePath + ", strFileName-" + strFileName + ",strcustomerId: " + strcustomerId + ",strNroInvoice: " + strNroInvoice);
                FileService.AuditRequest objAuditFileService = App_Code.Common.CreateAuditRequest<FileService.AuditRequest>(strIdSession);
                DashboardService.InvoiceRequest request = new DashboardService.InvoiceRequest()
                {
                    customerId = strcustomerId,
                    nroRecibo = strNroInvoice,
                    audit = objAudit,

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

                strTypeMIME = "application/pdf";
                DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                {

                    Path = strFileName,
                    DateRegister = strPeriodo,
                    audit = objAudit,
                    strIdOnBase = idOnBase,
                    strNumeroRecibo = numeroRecibo
                };

                objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjusteV3(objFileDefaultImpersonationRequestDashboard); });


                if (objFileDefaultImpersonationResponseDashboard != null)
                {
                    arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

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

                    objFileDefaultImpersonationResponseFile = Claro.Web.Logging.ExecuteMethod<FileService.FileDefaultImpersonationResponseDashboard>(1, () => { return new FileService.FileServiceClient().GetFileDefaultImpersonation(objFileDefaultImpersonationRequestDashboard); });

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

                DashboardService.InvoiceFtpResponseDashboard objInvoiceFtpResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.InvoiceFtpResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetInvoiceFtp(objInvoiceFtpRequestDashboard); });
                arrBuffer = objInvoiceFtpResponseDashboard.ObjGlobalDocument.Documento;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetTypeMIMERequestDashboard.audit.transaction, ex.Message);
            }


            return File(arrBuffer, strTypeMIME);
        }
        #endregion


        public JsonResult GetClaimByReceipt(string strIdSession, PostpaidService.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            objBSS_StatusAccountRequest.consultarEstadoCuenta = new PostpaidService.consultarEstadoCuenta();

            PostpaidService.BSS_StatusAccountResponse objBSS_StatusAccountResponse = null;
            try
            {
                objBSS_StatusAccountRequest.audit = Claro.SIACU.Web.WebApplication.App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

                objBSS_StatusAccountResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.BSS_StatusAccountResponse>(() =>
                {
                    return objPostpaidService.GetBSS_StatusAccount(objBSS_StatusAccountRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objBSS_StatusAccountRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objBSS_StatusAccountRequest.audit.transaction);
            }

            return Json(new { data = objBSS_StatusAccountResponse });
        }


        public POSTPAID_DATACOLLECTION.CollectionStatusAccountModel ListStatusAccount(string strIdSession, string strCustomerId, string strCsIdPub, string strplataform, string strNameClient, string strNumberServices, string strContactId, string strOriginType)
        {
            List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccount.CollectionStatusAccount> listCollectionStatusAccount = null;
            PostpaidService.StatusAccountResponsePostPaid objStatusAccountResponsePostPaid = null;
            POSTPAID_DATACOLLECTION.CollectionStatusAccountModel objCollectionStatusAccountModel = null;

            PostpaidService.StatusAccountRequestPostPaid objStatusAccountRequestPostPaid = new PostpaidService.StatusAccountRequestPostPaid
            {
                CustomerId = strCustomerId,
                csIdPub = strCsIdPub,
                plataformaAT = strplataform,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                UserName = App_Code.Common.CurrentUser,
                OriginType = strOriginType,
                isHR = false

            };


            try
            {
                objStatusAccountResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.StatusAccountResponsePostPaid>(
                    () => { return objPostpaidService.StatusAccount(objStatusAccountRequestPostPaid); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStatusAccountRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objStatusAccountRequestPostPaid.audit.transaction);
            }

            if (objStatusAccountResponsePostPaid != null && objStatusAccountResponsePostPaid.ListStatusAccount != null)
            {
                Claro.Web.Logging.Error(strIdSession, objStatusAccountRequestPostPaid.audit.transaction, "Seteado del estado de cuenta");
                listCollectionStatusAccount = new List<POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount>();
                var num = 0;
                try
                {

                    foreach (PostpaidService.StatusAccountAOCPostPaid item in objStatusAccountResponsePostPaid.ListStatusAccount)
                    {

                        num += 1;
                        listCollectionStatusAccount.Add(new POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount()
                        {
                            CorrelativeNumber = num,
                            Type = item.Tipo,
                            Document = item.Documento,
                            Ajuste = (strOriginType.Equals("POSTPAID", StringComparison.CurrentCultureIgnoreCase) || strOriginType.Equals("DTH") || strOriginType.Equals("IFI") ? GetLinkAjuste(strIdSession, item.Tipo, item.Documento) : (strOriginType.Equals("HFC") || strOriginType.Equals("LTE") ? GetLinkAjusteHFC(strIdSession, item.Tipo, item.Documento, double.Parse(item.Cargo), double.Parse(item.Abono)) : "")),
                            DescriptionPaid = item.DescripcionPago,
                            DateDue = (item.FechaVencimiento == null || item.FechaVencimiento == "") ? "" : DateTime.Parse(item.FechaVencimiento).ToString("dd/MM/yyyy"),
                            DateRegister = (item.FechaRegistro == null || item.FechaRegistro == "") ? "" : DateTime.Parse(item.FechaRegistro).ToString("dd/MM/yyyy"),
                            DateIssue = (item.FechaEmision == null || item.FechaEmision == "") ? "" : Convert.ToDate(item.FechaEmision).ToString("dd/MM/yyyy"),
                            DateRegisterTime = Convert.ToDate(item.FechaRegistro),
                            DateIssueTime = (item.FechaEmision == null || item.FechaEmision == "") ? "" : Convert.ToDate(item.FechaEmision).ToString("yyyyMM"),
                            DateDueTime = (item.FechaVencimiento == null || item.FechaVencimiento == "") ? "" : Convert.ToDate(item.FechaVencimiento).ToString("yyyyMM"),
                            Charge = String.IsNullOrEmpty(item.Cargo) ? "" : item.Cargo,
                            Payment = String.IsNullOrEmpty(item.Abono) ? "" : item.Abono,
                            ImportPending = String.IsNullOrEmpty(item.ImportePendiente) ? "" : item.ImportePendiente,
                            AmountClaimed = String.IsNullOrEmpty(item.MontoReclamo) ? "" : item.MontoReclamo,
                            Balance = String.IsNullOrEmpty(item.Saldo) ? "" : item.Saldo,
                            IdOnBase = string.IsNullOrEmpty(item.idOnbase) ? "" : item.idOnbase,
                            IsInvoceCancelable = item.flagBotonAnulacion, //proyAjustes
                            StatusDocument = item.EstadoDocumento
                        });
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objStatusAccountRequestPostPaid.audit.transaction, string.Format("Fila : {0}, Error:{1}", num.ToString(), ex.Message));

                }

                objCollectionStatusAccountModel = new POSTPAID_DATACOLLECTION.CollectionStatusAccountModel()
                {
                    listStatusAccountModel = listCollectionStatusAccount,
                    ClientId = strContactId,
                    ContratoId = strContactId,
                    NameClient = strNameClient,
                    NameClient2 = strNameClient,
                    NumberServices = strNumberServices,
                    IsEnabled = objStatusAccountResponsePostPaid.IsEnabled,
                    blMessageStatusAccountVisible = objStatusAccountResponsePostPaid.blMessageStatusAccountVisible,
                    strMessageStatusAccount = objStatusAccountResponsePostPaid.strMessageStatusAccount
                };

            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, objStatusAccountRequestPostPaid.audit.transaction, "Response Null del estado de cuenta");
                objCollectionStatusAccountModel = new POSTPAID_DATACOLLECTION.CollectionStatusAccountModel()
                {
                    listStatusAccountModel = new List<POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount>() { },
                    ClientId = strContactId,
                    ContratoId = strContactId,
                    NameClient = strNameClient,
                    NameClient2 = strNameClient,
                    NumberServices = strNumberServices,
                    IsEnabled = objStatusAccountResponsePostPaid.IsEnabled,
                    blMessageStatusAccountVisible = objStatusAccountResponsePostPaid.blMessageStatusAccountVisible,
                    strMessageStatusAccount = objStatusAccountResponsePostPaid.strMessageStatusAccount
                };
            }


            return objCollectionStatusAccountModel;
        }

        public string GetLinkAjuste(string strIdSession, string strType, string strDocument)
        {
            string link = "";
            DashboardService.AuditRequest audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            try
            {
                string StrResultado = string.Empty;
                char StrDelimitador = char.Parse(ConfigurationManager.AppSettings("gConstAjusteSarDelimitadorDocumento"));
                int StrDigitosAbrev = int.Parse(ConfigurationManager.AppSettings("gConstAjusteSarCantDigitosAbrev").ToString());

                string[] StrAbrev = strDocument.Split(StrDelimitador);

                if (StrAbrev.Length > 1)
                {
                    StrResultado = StrAbrev[0];
                }
                else
                {
                    StrResultado = strDocument.Substring(0, StrDigitosAbrev);
                }

                string strFlagAjuste = "1";
                DashboardService.FlagAjusteResponse objFlagAjusteResponse = oDashboardService.GetFlagAjuste(new DashboardService.FlagAjsuteRequest()
                {
                    audit = audit,
                    Document = StrResultado
                });
                if (objFlagAjusteResponse != null)
                    strFlagAjuste = (!string.IsNullOrWhiteSpace(objFlagAjusteResponse.AplicateAjsute) ? objFlagAjusteResponse.AplicateAjsute : "");

                string ValorLinkAjusteSar = ConfigurationManager.AppSettings("gConstAjusteSarValorLinkAjusteSar");
                if (strFlagAjuste.Equals(ValorLinkAjusteSar, StringComparison.CurrentCultureIgnoreCase))
                {

                    string StrResultadoTipoDoc = string.Empty;
                    string StrResultadoTipoDocRef = string.Empty;
                    if (Array.IndexOf(ConfigurationManager.AppSettings("gConstAjusteSarPrefTipDocReciboFactura").Split('|'), StrResultado) > -1)
                    {
                        StrResultadoTipoDoc = ConfigurationManager.AppSettings("gConstAjusteSarResulTipDocNCyND");
                        StrResultadoTipoDocRef = ConfigurationManager.AppSettings("gConstAjusteSarResulRecibo");
                    }
                    else if (Array.IndexOf(ConfigurationManager.AppSettings("gConstAjusteSarPrefTipDocNC").Split('|'), StrResultado) > -1)
                    {
                        StrResultadoTipoDoc = ConfigurationManager.AppSettings("gConstAjusteSarResulTipDocNC");
                        StrResultadoTipoDocRef = ConfigurationManager.AppSettings("gConstAjusteSarResulND");
                    }
                    else if (Array.IndexOf(ConfigurationManager.AppSettings("gConstAjusteSarPrefTipDocND").Split('|'), StrResultado) > -1)
                    {
                        StrResultadoTipoDoc = ConfigurationManager.AppSettings("gConstAjusteSarResulTipDocND");
                        StrResultadoTipoDocRef = ConfigurationManager.AppSettings("gConstAjusteSarResulNC");
                    }
                    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(Json(new { Type = strType, Document = strDocument, TypeDoc = StrResultadoTipoDoc, TypeDocRef = StrResultadoTipoDocRef }));
                    link = "<a style='cursor:pointer;' class='Ajuste' data-json='" + jsonstring + "'>Realizar Ajuste</a>";

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            return link;
        }
        public string GetLinkAjusteHFC(string strIdSession, string strType, string strDocument, double Cargo, double Abono)
        {
            string link = "";
            DashboardService.AuditRequest audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            try
            {
                char StrDelimitador = char.Parse(ConfigurationManager.AppSettings("gConstAjusteSarDelimitadorDocumento"));
                int StrDigitosAbrev = int.Parse(ConfigurationManager.AppSettings("gConstAjusteSarCantDigitosAbrev").ToString());
                string mstrAbreviatura = strDocument.Substring(0, strDocument.IndexOf(StrDelimitador));
                if ((!string.IsNullOrWhiteSpace(mstrAbreviatura) && mstrAbreviatura.Length != StrDigitosAbrev))
                {
                    mstrAbreviatura = strDocument.Substring(0, StrDigitosAbrev);
                }

                string strFlagAjuste = "0";
                DashboardService.FlagAjusteResponse objFlagAjusteResponse = oDashboardService.GetFlagAjuste(new DashboardService.FlagAjsuteRequest()
                {
                    audit = audit,
                    Document = mstrAbreviatura
                });
                if (objFlagAjusteResponse != null)
                    strFlagAjuste = (!string.IsNullOrWhiteSpace(objFlagAjusteResponse.AplicateAjsute) ? objFlagAjusteResponse.AplicateAjsute : "");
                if (strFlagAjuste.Equals("1"))
                {
                    string StrResultadoTipoDoc = "", StrResultadoTipoDocRef = "";
                    if (Array.IndexOf(ConfigurationManager.AppSettings("gConstAjusteSarPrefTipDocReciboFactura").Split('|'), mstrAbreviatura) > -1)
                    {
                        StrResultadoTipoDoc = ConfigurationManager.AppSettings("gConstAjusteSarResulTipDocNCyND");
                        StrResultadoTipoDocRef = ConfigurationManager.AppSettings("gConstAjusteSarResulRecibo");
                    }
                    else if (Array.IndexOf(ConfigurationManager.AppSettings("gConstAjusteSarPrefTipDocNC").Split('|'), mstrAbreviatura) > -1)
                    {
                        StrResultadoTipoDoc = ConfigurationManager.AppSettings("gConstAjusteSarResulTipDocNC");
                        StrResultadoTipoDocRef = ConfigurationManager.AppSettings("gConstAjusteSarResulND");
                    }
                    else if (Array.IndexOf(ConfigurationManager.AppSettings("gConstAjusteSarPrefTipDocND").Split('|'), mstrAbreviatura) > -1)
                    {
                        StrResultadoTipoDoc = ConfigurationManager.AppSettings("gConstAjusteSarResulTipDocND");
                        StrResultadoTipoDocRef = ConfigurationManager.AppSettings("gConstAjusteSarResulNC");
                    }
                    string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(Json(new { Type = strType, Document = strDocument, TypeDoc = StrResultadoTipoDoc, TypeDocRef = StrResultadoTipoDocRef }));
                    link = "<a style='cursor:pointer;' class='AjusteHFC' data-json='" + jsonstring + "'>Realizar Ajuste</a>";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            return link;
        }

        public JsonResult PostReportStatusAccount(string strIdSession, string strCustomerId, string strNameClient, string strCsIdPub, string strplataform, string strNumberServices, string strContactId, string strOriginType)
        {
            POSTPAID_DATACOLLECTION.CollectionStatusAccountModel objCollectionStatusAccountModel = ListStatusAccount(strIdSession, strCustomerId, strCsIdPub, strplataform, strNameClient, strNumberServices, strContactId, strOriginType);
            string path = oExcelHelper.ExportExcel(objCollectionStatusAccountModel, TemplateExcel.CONST_STATUSACCOUNT);

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strText = Claro.SIACU.Constants.ConsultStatusAccount + strNumberServices + Claro.SIACU.Constants.CustomerCode + strCustomerId + Claro.SIACU.Constants.WindowTypeStatusAccount;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strNumberServices, KEY.AppSettings("strAudiTraCodCuotaEquipo"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }

            return Json(path);
        }

        public JsonResult GetFileRute(string strIdSession, string strDocumenName, string DateIssue, string strType, bool isHistoryHR)
        {
            string strExtension = "";
            System.Text.StringBuilder strBCadena = new System.Text.StringBuilder();
            string cadena;
            int strNumbersMeses = Claro.Constants.NumberZero;
            string FilePath = KEY.AppSettings("strDirectorioFacturas");
            string strNameDocument = KEY.AppSettings("strNombreDocumento");
            string strNumberDocument = KEY.AppSettings("strNumeroDocumento");
            string strMonthAccountCount = KEY.AppSettings("strEstadoCuentaMeses");
            string strNameDocumentAjuste = KEY.AppSettings("strNombreDocumentoMGR");
            string strNumberCredit = KEY.AppSettings("strNumeroNotaCredito");
            string strNumberDebit = KEY.AppSettings("strNumeroNotaDebito");
            string sRutaHojasResumenes = KEY.AppSettings("strDirectorioHojasResumenes");
            string strNumberDocumentSB = KEY.AppSettings("strNumeroDocumentoSB");


            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;
            FileService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseFile = null;

            Dashboard.Models.Postpaid.DataBillingModel objDataBillingModel = new Dashboard.Models.Postpaid.DataBillingModel();

            for (int i = 0; i < strDocumenName.Length; i++)
            {
                if (strDocumenName[i].ToString() != "-")
                {
                    strBCadena.Append(strDocumenName[i].ToString());
                }
                else
                {
                    break;
                }
            }
            cadena = strBCadena.ToString();

            if ((strType == strNameDocument && (cadena == strNumberDocument || strNumberDocumentSB.Contains(cadena))) && (strNumbersMeses > Int32.Parse(strMonthAccountCount)) && (DateIssue != null && DateIssue != ""))
            {
                objDataBillingModel.FilePath = FilePath.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;
            }

            else if (strType == strNameDocumentAjuste && (cadena == strNumberCredit || cadena == strNumberDebit))
            {
                objDataBillingModel.FileName = strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;
                objDataBillingModel.EmissionDate = DateIssue.Trim().Substring(6, 4);
            }
            else if ((strType == "Hoja Resumen" && cadena == "HR") && (strNumbersMeses > Int32.Parse(strMonthAccountCount)) && (DateIssue != null && DateIssue != ""))
            {

                objDataBillingModel.FilePath = sRutaHojasResumenes.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;




            }
            else if ((strType == "Hoja Resumen" && (cadena == strNumberDocument || strNumberDocumentSB.Contains(cadena))) && (strNumbersMeses > Int32.Parse(strMonthAccountCount)) && (DateIssue != null && DateIssue != ""))
            {
                objDataBillingModel.FilePath = FilePath.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;
            }
            else if (isHistoryHR)
            {
                objDataBillingModel.FilePath = sRutaHojasResumenes.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + PrintHistoryHR(strDocumenName.Trim(), Claro.SIACU.Constants.T001) + Claro.SIACU.Constants.PointPdf;
            }

            try
            {
                objDataBillingModel.FlagBill = Claro.Constants.NumberZero.ToString();
                DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                FileService.AuditRequest objAuditFileService = App_Code.Common.CreateAuditRequest<FileService.AuditRequest>(strIdSession);
                strExtension = System.IO.Path.GetExtension(objDataBillingModel.FilePath + objDataBillingModel.FileName).Remove(0, 1);
                DashboardService.GetTypeMIMERequestDashboard objGetTypeMIMERequestDashboard = new DashboardService.GetTypeMIMERequestDashboard()
                {
                    audit = objAudit,
                    Extension = strExtension
                };

                DashboardService.GetTypeMIMEResponseDashboard objGetTypeMIMEResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.GetTypeMIMEResponseDashboard>(() => { return new DashboardService.DashboardServiceClient().GetTypeMIME(objGetTypeMIMERequestDashboard); });
                objDataBillingModel.TypeMIME = objGetTypeMIMEResponseDashboard.TypeMime;

                if (objDataBillingModel.TypeMIME != null && objDataBillingModel.TypeMIME != "")
                {
                    objDataBillingModel.arrBuffer = null;
                    if (objDataBillingModel.FilePath != null && objDataBillingModel.FilePath != "")
                    {

                        FileService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new FileService.FileDefaultImpersonationRequestDashboard()
                        {
                            Path = objDataBillingModel.FilePath + objDataBillingModel.FileName,
                            audit = objAuditFileService
                        };

                        objFileDefaultImpersonationResponseFile = Claro.Web.Logging.ExecuteMethod<FileService.FileDefaultImpersonationResponseDashboard>(1, () => { return new FileService.FileServiceClient().GetFileDefaultImpersonation(objFileDefaultImpersonationRequestDashboard); });
                        objDataBillingModel.FilePath = objDataBillingModel.FilePath.Replace('\\', '|');
                        objDataBillingModel.FileName = objDataBillingModel.FileName.Replace('\\', '|');
                    }
                    else
                    {
                        DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                        {
                            Path = objDataBillingModel.FileName,
                            DateRegister = objDataBillingModel.EmissionDate,
                            audit = objAudit
                        };


                        objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjuste(objFileDefaultImpersonationRequestDashboard); });

                    }
                    if (objFileDefaultImpersonationResponseFile != null)
                    {
                        objDataBillingModel.FlagBill = Claro.Constants.NumberOne.ToString();
                        objDataBillingModel.arrBuffer = objFileDefaultImpersonationResponseFile.ObjGlobalDocument.Documento;
                    }
                    else if (objFileDefaultImpersonationResponseDashboard != null)
                    {
                        objDataBillingModel.FlagBill = Claro.Constants.NumberOne.ToString();
                        objDataBillingModel.arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Json(new { data = objDataBillingModel });
        }

        public JsonResult GetFileRuteToBe(string strIdSession, string strDocumenName, string strPeriodo, string strPlataform, string strIdOnBase, string DateIssue, string strType, bool isHistoryHR)
        {
            string strExtension = "";
            System.Text.StringBuilder strBCadena = new System.Text.StringBuilder();
            string cadena;
            int strNumbersMeses = Claro.Constants.NumberZero;
            string FilePath = KEY.AppSettings("strDirectorioFacturas");
            string strNameDocument = KEY.AppSettings("strNombreDocumento");
            string strNumberDocument = KEY.AppSettings("strNumeroDocumento");

            string strNumberDocumentToBe = KEY.AppSettings("strNumeroDocumentoToBe");
            string[] arrNumeroDocumentoTobe = KEY.AppSettings("strNumeroDocumentoToBe2").ToString().Split(new char[] { '|' });
            //string strNumberDocumentToBe2 = KEY.AppSettings("strNumeroDocumentoToBe2");
            //string strNumeroDocumentoToBeAM02 = KEY.AppSettings("strNumeroDocumentoToBeAM02");

            string strMonthAccountCount = KEY.AppSettings("strEstadoCuentaMeses");
            string strNameDocumentAjuste = KEY.AppSettings("strNombreDocumentoMGR");
            string strNumberCredit = KEY.AppSettings("strNumeroNotaCredito");
            string strNumberDebit = KEY.AppSettings("strNumeroNotaDebito");
            string sRutaHojasResumenes = KEY.AppSettings("strDirectorioHojasResumenes");
            string strNumberDocumentSB = KEY.AppSettings("strNumeroDocumentoSB");


            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;

            Dashboard.Models.Postpaid.DataBillingModel objDataBillingModel = new Dashboard.Models.Postpaid.DataBillingModel();

            strIdOnBase = !string.IsNullOrWhiteSpace(strIdOnBase) ? strIdOnBase : "";

            if (strPlataform.Equals(Claro.SIACU.Constants.ASIS))
            {
                strIdOnBase = "";
            }

            for (int i = 0; i < strDocumenName.Length; i++)
            {
                if (strDocumenName[i].ToString() != "-")
                {
                    strBCadena.Append(strDocumenName[i].ToString());
                }
                else
                {
                    break;
                }
            }
            cadena = strBCadena.ToString();

            if (((strType == strNameDocument && (cadena == strNumberDocument || strNumberDocumentSB.Contains(cadena))) || (strType == strNameDocument && (cadena == strNumberDocumentToBe || strNumberDocumentSB.Contains(cadena))) ||
                (strType == strNameDocument && (cadena == arrNumeroDocumentoTobe[0] || strNumberDocumentSB.Contains(cadena))) || (strType == strNameDocument && (cadena == arrNumeroDocumentoTobe[1] || strNumberDocumentSB.Contains(cadena))) ||
                (strType == strNameDocument && (cadena == arrNumeroDocumentoTobe[2] || strNumberDocumentSB.Contains(cadena))) || (strType == strNameDocument && (cadena == arrNumeroDocumentoTobe[3] || strNumberDocumentSB.Contains(cadena)))) &&
                (strNumbersMeses > Int32.Parse(strMonthAccountCount)) && (DateIssue != null && DateIssue != ""))
            {
                objDataBillingModel.FilePath = FilePath.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;
            }

            else if (strType == strNameDocumentAjuste && (cadena == strNumberCredit || cadena == strNumberDebit))
            {
                objDataBillingModel.FileName = strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;
                objDataBillingModel.EmissionDate = DateIssue.Trim().Substring(6, 4);
            }
            else if ((strType == "Hoja Resumen" && cadena == "HR") && (strNumbersMeses > Int32.Parse(strMonthAccountCount)) && (DateIssue != null && DateIssue != ""))
            {

                objDataBillingModel.FilePath = sRutaHojasResumenes.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;




            }
            else if ((strType == "Hoja Resumen" && (cadena == strNumberDocument || strNumberDocumentSB.Contains(cadena))) && (strNumbersMeses > Int32.Parse(strMonthAccountCount)) && (DateIssue != null && DateIssue != ""))
            {
                objDataBillingModel.FilePath = FilePath.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + strDocumenName.Trim() + Claro.SIACU.Constants.PointPdf;
            }
            else if (isHistoryHR)
            {
                objDataBillingModel.FilePath = sRutaHojasResumenes.Replace(@"\", "\\") + "\\";
                objDataBillingModel.FileName = DateIssue.Trim() + "\\" + PrintHistoryHR(strDocumenName.Trim(), Claro.SIACU.Constants.T001) + Claro.SIACU.Constants.PointPdf;
            }

            try
            {
                objDataBillingModel.FlagBill = Claro.Constants.NumberZero.ToString();
                FileService.AuditRequest objAuditFileService = App_Code.Common.CreateAuditRequest<FileService.AuditRequest>(strIdSession);
                strExtension = System.IO.Path.GetExtension(objDataBillingModel.FilePath + objDataBillingModel.FileName).Remove(0, 1);

                objDataBillingModel.TypeMIME = "application/pdf";

                if (objDataBillingModel.TypeMIME != null && objDataBillingModel.TypeMIME != "")
                {
                    objDataBillingModel.arrBuffer = null;
                    if (objDataBillingModel.FilePath != null && objDataBillingModel.FilePath != "")
                    {

                        DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);

                        DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                        {
                            audit = objAudit,
                            strIdOnBase = strIdOnBase,
                            //strIdOnBase = "320619", //"320013",
                            DateRegister = strPeriodo,
                            strNumeroRecibo = strDocumenName
                        };

                        objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjusteV3(objFileDefaultImpersonationRequestDashboard); });

                        objDataBillingModel.FilePath = objDataBillingModel.FilePath.Replace('\\', '|');
                        objDataBillingModel.FileName = objDataBillingModel.FileName.Replace('\\', '|');
                    }
                    if (objFileDefaultImpersonationResponseDashboard != null)
                    {
                        objDataBillingModel.FlagBill = Claro.Constants.NumberOne.ToString();
                        objDataBillingModel.arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Json(new { data = objDataBillingModel });
        }

        private string PrintHistoryHR(string strDocument, string strType)
        {
            try
            {
                System.Text.StringBuilder cadena = new System.Text.StringBuilder();

                for (int i = 0; i < strDocument.Length; i++)
                {
                    if (strDocument[i].ToString() != "-")
                    {
                        cadena.Append(strDocument[i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }

                string strDocumentoFinal = string.Empty;

                if (strDocument.ToString().Split('-')[0].Trim().ToString() == KEY.AppSettings("strNumeroDocumento"))
                {
                    strDocumentoFinal = "HR-" + strDocument.ToString().Split('-')[1].Trim().ToString();
                }
                if (strDocumentoFinal.Trim().ToString() != "")
                {
                    strDocument = strDocumentoFinal;
                }
                else
                {
                    strDocument = "";
                }
                return strDocument;


            }
            catch (Exception)
            {

                throw;

            }
        }
        private string PrintLinkHR(string strDocument, string strType)
        {
            try
            {
                System.Text.StringBuilder cadena = new System.Text.StringBuilder();

                for (int i = 0; i < strDocument.Length; i++)
                {
                    if (strDocument[i].ToString() != "-")
                    {
                        cadena.Append(strDocument[i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
                if (strType == KEY.AppSettings("strNombreDocumento"))
                {
                    string strDocumentoFinal = string.Empty;

                    if (strDocument.ToString().Split('-')[0].Trim().ToString() == KEY.AppSettings("strNumeroDocumento"))
                    {
                        strDocumentoFinal = "HR-" + strDocument.ToString().Split('-')[1].Trim().ToString();
                    }
                    if (strDocumentoFinal.Trim().ToString() != "")
                    {
                        strDocument = strDocumentoFinal;
                    }
                    else
                    {
                        strDocument = "";
                    }
                    return strDocument;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                throw;

            }

        }
        public FileContentResult ShowFile(string strIdSession, string strFilePath, string strFileName, string strNameForm, string strEmissionDate)
        {
            string strExtension = "";
            byte[] arrBuffer = null;
            string strTypeMIME = "";
            DashboardService.FileInvoiceResponseDashboard objFileInvoiceResponseDashboard = null;
            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;
            FileService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseFile = null;

            DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            FileService.AuditRequest objAuditFileService = App_Code.Common.CreateAuditRequest<FileService.AuditRequest>(strIdSession);

            try
            {
                strFileName = strFileName.Replace('|', '\\');
                strFilePath = strFilePath.Replace('|', '\\');
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
                else if (strNameForm == Claro.ConfigurationManager.AppSettings("strTransAjusteTP"))
                {
                    DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                    {
                        Path = strFileName,
                        DateRegister = strEmissionDate,
                        audit = objAudit
                    };

                    objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjuste(objFileDefaultImpersonationRequestDashboard); });
                }
                else
                {
                    FileService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new FileService.FileDefaultImpersonationRequestDashboard()
                    {
                        Path = strFilePath + strFileName,
                        audit = objAuditFileService
                    };

                    objFileDefaultImpersonationResponseFile = Claro.Web.Logging.ExecuteMethod<FileService.FileDefaultImpersonationResponseDashboard>(1, () => { return new FileService.FileServiceClient().GetFileDefaultImpersonation(objFileDefaultImpersonationRequestDashboard); });
                }

                if (objFileDefaultImpersonationResponseDashboard != null)
                {
                    arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                }
                else
                {
                    arrBuffer = objFileDefaultImpersonationResponseFile.ObjGlobalDocument.Documento;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return File(arrBuffer, strTypeMIME);
        }

        public FileStreamResult ShowFileToBe(string strIdSession, string strFilePath, string strFileName, string strNameForm, string strEmissionDate, string strIdOnBase, string strDocumenName, string strPeriodo)
        {
            string strExtension = "";
            byte[] arrBuffer = null;
            string strTypeMIME = "";
            DashboardService.FileDefaultImpersonationResponseDashboard objFileDefaultImpersonationResponseDashboard = null;

            try
            {
                strFileName = strFileName.Replace('|', '\\');
                strFilePath = strFilePath.Replace('|', '\\');
                strExtension = System.IO.Path.GetExtension(strFilePath + strFileName).Remove(0, 1);

                strTypeMIME = "application/pdf";

                DashboardService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);

                DashboardService.FileDefaultImpersonationRequestDashboard objFileDefaultImpersonationRequestDashboard = new DashboardService.FileDefaultImpersonationRequestDashboard()
                {
                    audit = objAudit,
                    strIdOnBase = strIdOnBase,
                    //strIdOnBase = "320619", //"320013",
                    DateRegister = strPeriodo,
                    strNumeroRecibo = strDocumenName
                };

                objFileDefaultImpersonationResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.FileDefaultImpersonationResponseDashboard>(1, () => { return new DashboardService.DashboardServiceClient().GetFileAjusteV3(objFileDefaultImpersonationRequestDashboard); });

                if (objFileDefaultImpersonationResponseDashboard != null)
                {
                    arrBuffer = objFileDefaultImpersonationResponseDashboard.ObjGlobalDocument.Documento;
                }

            }
            catch (Exception)
            {
                throw;
            }

            Stream stream = new MemoryStream(arrBuffer);
            Response.AppendHeader("content-disposition", "inline; filename=" + strFileName + ".pdf");

            return new FileStreamResult(stream, strTypeMIME);
        }

        #endregion



        #region [NumerodeDocumento]

        public ActionResult DueNumberDocument()
        {
            return PartialView();
        }

        public POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel DueNumberDocuments(string strDocumentIdentity, string strNumberDocument, string strIdSession)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -INI");
            Claro.Web.Logging.Info(strIdSession, strIdSession, "strDocumentIdentity : " + strDocumentIdentity);
            Claro.Web.Logging.Info(strIdSession, strIdSession, "strNumberDocument : " + strNumberDocument);

            PostpaidService.DueDocumentNumberRequestPostPaid objDueDocumentNumberRequestPostPaid = new PostpaidService.DueDocumentNumberRequestPostPaid();
            PostpaidService.DueDocumentNumberResponsePostPaid objDueDocumentNumberResponsePostPaid;

            try
            {
                objDueDocumentNumberRequestPostPaid.DocumentIdentity = strDocumentIdentity;
                objDueDocumentNumberRequestPostPaid.NumberDocument = strNumberDocument;
                objDueDocumentNumberRequestPostPaid.UserAplication = App_Code.Common.CurrentUser;
                objDueDocumentNumberRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);

                objDueDocumentNumberResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.DueDocumentNumberResponsePostPaid>(() => { return objPostpaidService.DueNumberDocumentOAC(objDueDocumentNumberRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDueDocumentNumberRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objDueDocumentNumberRequestPostPaid.audit.transaction);
            }

            POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel objCollectionDueDocumentNumberModel = new POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel();
            Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber" + objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber);

            if (objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber != null)
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -objCollectionDueDocumentNumberModel.ObjCollectionDueDocumentNumberModel ini");

                objCollectionDueDocumentNumberModel.ObjCollectionDueDocumentNumberModel = new POSTPAID_HELPER.CollectionDueDocumentNumberModel.CollectionDueDocumentNumberModel()
                {
                    NameClient = objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.NombreCliente,
                    MobileDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DeudaMovil, 2),
                    FixedDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DeudaFija, 2),
                    OverdueMobileDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DeudaVencidaMovil, 2),
                    OverdueFixedDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DeudaVencidaFija, 2),
                    PunishedMobileDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DeudaCastigadaMovil, 2),
                    PunishedFixedDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DeudaCastigadaFija, 2),
                    ReferenceDocument = objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.DNIAsociado,
                    AntiquityDue = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.AntiguedadDeuda, 2),
                    AllServices = Decimal.Round((decimal)objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ObjDueDocumentNumberAccount.TotalServicios, 2)
                };
                Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -objCollectionDueDocumentNumberModel.ObjCollectionDueDocumentNumberModel fin");

                objCollectionDueDocumentNumberModel.ListCollectionDueDocumentNumberModelFija = new List<POSTPAID_HELPER.CollectionDueDocumentNumberModel.CollectionDueDocumentNumberModel>();
                objCollectionDueDocumentNumberModel.ListCollectionDueDocumentNumberModelMovil = new List<POSTPAID_HELPER.CollectionDueDocumentNumberModel.CollectionDueDocumentNumberModel>();
                POSTPAID_HELPER.CollectionDueDocumentNumberModel.CollectionDueDocumentNumberModel helCollectionDueDocumentNumberModel;
                Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -(objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountFijo != null)");

                if (objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountFijo != null)
                {

                    foreach (PostpaidService.DueDocumentNumberAccountPostPaid item in objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountFijo)
                    {
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountFijo" + item);

                        helCollectionDueDocumentNumberModel = new POSTPAID_HELPER.CollectionDueDocumentNumberModel.CollectionDueDocumentNumberModel();
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -item.CodCuenta" + item.CodCuenta);

                        helCollectionDueDocumentNumberModel.IdAccount = item.CodCuenta;
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -item.EstadoCuenta" + item.EstadoCuenta);

                        helCollectionDueDocumentNumberModel.StatusAccount = item.EstadoCuenta;
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -item.PromedioFact" + item.PromedioFact);

                        helCollectionDueDocumentNumberModel.AverageInvoiced = Convert.ToString(Decimal.Round((decimal)item.PromedioFact, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -item.DeudaCorriente" + item.DeudaCorriente);

                        helCollectionDueDocumentNumberModel.CurrentDue = Convert.ToString(Decimal.Round((decimal)item.DeudaCorriente, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -DeudaVencida" + item.DeudaVencida);

                        helCollectionDueDocumentNumberModel.ExpiredDue = Convert.ToString(Decimal.Round((decimal)(item.DeudaVencida == null ? 0 : item.DeudaVencida), 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -item.DeudaCastigada" + item.DeudaCastigada);

                        helCollectionDueDocumentNumberModel.PunishedDue = Convert.ToString(Decimal.Round((decimal)item.DeudaCastigada, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -CantServicios" + item.CantServicios);

                        helCollectionDueDocumentNumberModel.AccountServices = Convert.ToString(Convert.ToInt(item.CantServicios));
                        if (item.IndCentralRiesgo == "N")
                        {
                            helCollectionDueDocumentNumberModel.RiskCenter = "NO";
                        }
                        else if (item.IndCentralRiesgo == "Y" || item.IndCentralRiesgo == "S")
                        {
                            helCollectionDueDocumentNumberModel.RiskCenter = "SI";
                        }
                        else
                        {
                            helCollectionDueDocumentNumberModel.RiskCenter = item.IndCentralRiesgo;
                        }
                        objCollectionDueDocumentNumberModel.ListCollectionDueDocumentNumberModelFija.Add(helCollectionDueDocumentNumberModel);
                    }
                }
                Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -(objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountMovil != null)");
                if (objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountMovil != null)
                {
                    Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -(objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountMovil != null)");

                    foreach (PostpaidService.DueDocumentNumberAccountPostPaid item in objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountMovil)
                    {
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC -(objDueDocumentNumberResponsePostPaid.ObjDueDocumentNumber.ListDueDocumentNumberAccountMovil for)");

                        helCollectionDueDocumentNumberModel = new POSTPAID_HELPER.CollectionDueDocumentNumberModel.CollectionDueDocumentNumberModel();
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.CodCuenta)" + item.CodCuenta);
                        if (item.CodCuenta.IndexOf("-") >= 0)
                        {
                            string cuentaGuion = item.CodCuenta;
                            int posicion = cuentaGuion.IndexOf("-");
                            helCollectionDueDocumentNumberModel.IdAccount = cuentaGuion.Substring(0, posicion);
                            Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC cuentaGuion-IF )" + cuentaGuion);
                        }
                        else
                        {
                            helCollectionDueDocumentNumberModel.IdAccount = item.CodCuenta;
                        }
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.EstadoCuenta)" + item.EstadoCuenta);
                        helCollectionDueDocumentNumberModel.StatusAccount = item.EstadoCuenta;
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.PromedioFact)" + item.PromedioFact);
                        helCollectionDueDocumentNumberModel.AverageInvoiced = Convert.ToString(Decimal.Round((decimal)item.PromedioFact, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.DeudaCorriente)" + item.DeudaCorriente);
                        helCollectionDueDocumentNumberModel.CurrentDue = Convert.ToString(Decimal.Round((decimal)item.DeudaCorriente, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.DeudaVencida)" + item.DeudaVencida);
                        helCollectionDueDocumentNumberModel.ExpiredDue = Convert.ToString(Decimal.Round((decimal)item.DeudaVencida, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.DeudaCastigada)" + item.DeudaCastigada);
                        helCollectionDueDocumentNumberModel.PunishedDue = Convert.ToString(Decimal.Round((decimal)item.DeudaCastigada, 2));
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC item.CantServicios)" + item.CantServicios);
                        helCollectionDueDocumentNumberModel.AccountServices = Convert.ToString(Convert.ToInt(item.CantServicios));
                        if (item.IndCentralRiesgo == "N")
                        {
                            helCollectionDueDocumentNumberModel.RiskCenter = "NO";
                        }
                        else if (item.IndCentralRiesgo == "Y" || item.IndCentralRiesgo == "S")
                        {
                            helCollectionDueDocumentNumberModel.RiskCenter = "SI";
                        }
                        else
                        {
                            helCollectionDueDocumentNumberModel.RiskCenter = item.IndCentralRiesgo;
                        }
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC objCollectionDueDocumentNumberModel.ListCollectionDueDocumentNumberModelMovil.Add(helCollectionDueDocumentNumberModel);");

                        objCollectionDueDocumentNumberModel.ListCollectionDueDocumentNumberModelMovil.Add(helCollectionDueDocumentNumberModel);
                    }
                }

            }

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strConsultDate = DateTime.Now.ToString("yyyyMMdd");
                string strText = Claro.SIACU.Constants.ApadeceDue + Claro.SIACU.Constants.TypeDNI + strNumberDocument + Claro.SIACU.Constants.DateStart + strConsultDate;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strNumberDocument, KEY.AppSettings("strAudiTracodConsultaDeudaApadece"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, "Controller  DueNumberDocumentOAC objCollectionDueDocumentNumberModel");

            return objCollectionDueDocumentNumberModel;
        }

        public JsonResult GetDocumentType(string strIdSession)
        {
            CommonService.DocumentTypeResquestCommon objRequest = null;
            CommonService.DocumentTypeResponseCommon objDocumentTypeResponseCommon;

            try
            {
                objRequest = new CommonService.DocumentTypeResquestCommon() { audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession) };
                objDocumentTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.DocumentTypeResponseCommon>(() => { return oServiceCommon.GetDocumentType(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }

            POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel objCollectionDueDocumentNumberModel = new POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel();

            if (objDocumentTypeResponseCommon.DocumentTypes != null)
            {
                List<POSTPAID_HELPER.CollectionDueDocumentNumber.DocumentType> listDocumentTypes = new List<POSTPAID_HELPER.CollectionDueDocumentNumber.DocumentType>();

                foreach (CommonService.ListItem item in objDocumentTypeResponseCommon.DocumentTypes)
                {
                    listDocumentTypes.Add(new POSTPAID_HELPER.CollectionDueDocumentNumber.DocumentType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCollectionDueDocumentNumberModel.listDocumentType = listDocumentTypes;
            }

            return Json(new { data = objCollectionDueDocumentNumberModel });
        }

        public JsonResult ListsDueNumberDocument(string strDocumentIdentity, string strNumberDocument, string strIdSession)
        {
            POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel objCollectionDueDocumentNumberModel = DueNumberDocuments(strDocumentIdentity, strNumberDocument, strIdSession);
            return Json(new { data = objCollectionDueDocumentNumberModel });
        }
        public JsonResult ListsDueNumberDocument_EC(string strCod_consulta, string strDocLinea, string strIdSession, string strPlataform, string strDocNum, string strDocType)
        {
            POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel objCollectionDueDocumentNumberModel = null;
            if (strPlataform.Equals(Claro.SIACU.Constants.ASIS))
            {
                PostpaidService.TypeDocumentDeubtRequestPostPaid objTypeDocumentDeubtRequestPostPaid = new PostpaidService.TypeDocumentDeubtRequestPostPaid()
                {
                    audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                    strCod_consulta = strCod_consulta,
                    strDocLinea = strDocLinea
                };

                PostpaidService.TypeDocumentDeubtResponsePostPaid objTypeDocumentDeubtResponsePostPaid;

                objTypeDocumentDeubtResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.TypeDocumentDeubtResponsePostPaid>(() =>
                {
                    return objPostpaidService.GetTypeDocumentDeubt(objTypeDocumentDeubtRequestPostPaid);
                });

                PostpaidService.CustomerPostPaid objCustomerPostPaid = objTypeDocumentDeubtResponsePostPaid.lstCustomer.LastOrDefault();

                objCollectionDueDocumentNumberModel = DueNumberDocuments(
                objCustomerPostPaid.TIPO_DOC, objCustomerPostPaid.NRO_DOC, strIdSession);

            }
            else if (strPlataform.Equals(Claro.SIACU.Constants.TOBE))
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, "strDocType : " + strDocType);

                if (strDocType.Trim() == "2")
                {
                    strDocType = "1";
                }
                else if (strDocType.Trim() == "1")
                {
                    strDocType = "7";
                }

                Claro.Web.Logging.Info(strIdSession, strIdSession, "strDocType : " + strDocType);

                objCollectionDueDocumentNumberModel = DueNumberDocuments(strDocType.Trim(), strDocNum.Trim(), strIdSession);
            }
            return Json(new { data = objCollectionDueDocumentNumberModel });
        }

        public JsonResult PostReportDueNumberDocument(string strIdSession, string strDocumentIdentity, string strNumberDocument)
        {
            POSTPAID_DATACOLLECTION.CollectionDueDocumentNumberModel objDueNumberDocumentAccount = DueNumberDocuments(strDocumentIdentity, strNumberDocument, strIdSession);
            objDueNumberDocumentAccount.ObjCollectionDueDocumentNumberModel.NumberDocument = strNumberDocument;
            string path = oExcelHelper.ExportExcel(objDueNumberDocumentAccount, TemplateExcel.CONST_DUENUMBERDOCUMENT);
            return Json(path);
        }

        #endregion


        #region [EstdoCuentaLDI]

        public ActionResult StatusAccountLDI(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId)
        {
            return PartialView(ListStatusAccountLDI(strIdSession, strCustomerId, strNameClient, strNumberServices, strContactId));
        }

        public POSTPAID_DATACOLLECTION.CollectionStatusAccountLDIModel ListStatusAccountLDI(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId)
        {
            PostpaidService.StatusAccountLDIResponsePostPaid objGetStatusAccountLDIResponse = null;

            PostpaidService.StatusAccountLDIRequestPostPaid objGetStatusAccountLDIRequest = new PostpaidService.StatusAccountLDIRequestPostPaid()
            {
                CustomerId = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objGetStatusAccountLDIResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.StatusAccountLDIResponsePostPaid>(() => { return objPostpaidService.StatusAccountLDI(objGetStatusAccountLDIRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetStatusAccountLDIRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetStatusAccountLDIRequest.audit.transaction);
            }

            POSTPAID_DATACOLLECTION.CollectionStatusAccountLDIModel objCollectionStatusAccountLDIModel = new POSTPAID_DATACOLLECTION.CollectionStatusAccountLDIModel()
            {
                ClientId = strContactId,
                NameClient = strNameClient,
                NumberServices = strNumberServices
            };
            List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI> listCollectionStatusAccountLDI = new List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI>();

            if (objGetStatusAccountLDIResponse.Bills != null)
            {
                Claro.Web.Logging.Info(strIdSession, objGetStatusAccountLDIRequest.audit.transaction, "objGetStatusAccountLDIResponse.Bills != null");
                int cont = 0;
                foreach (PostpaidService.StatusAccountLDIPostPaid item in objGetStatusAccountLDIResponse.Bills)
                {
                    listCollectionStatusAccountLDI.Add(new POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI()
                    {
                        N = cont++,
                        Detalle = item.Detalle,
                        ClaroBillId = item.ClaroBillId,
                        LdiBillId = item.LdiBillId,
                        OperatorShort = item.OperatorShort,
                        RegistryDate = item.RegistryDate,
                        EmittedDate = item.EmittedDate,
                        MaturityDate = item.MaturityDate,
                        BillTotal = item.BillTotal,
                        ImportePend = item.ImportePend,
                        Status = item.Status,
                        AmountNotRequired = item.AmountNotRequired
                    });
                }
                Claro.Web.Logging.Info(strIdSession, objGetStatusAccountLDIRequest.audit.transaction, "objCollectionStatusAccountLDIModel.listBillStatusAccountLDIModel");
                objCollectionStatusAccountLDIModel.listBillStatusAccountLDIModel = listCollectionStatusAccountLDI;

            }
            List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDIPayment> listCollectionStatusAccountLDIPayment = new List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDIPayment>();
            if (objGetStatusAccountLDIResponse.Payments != null)
            {
                Claro.Web.Logging.Info(strIdSession, objGetStatusAccountLDIRequest.audit.transaction, "objGetStatusAccountLDIResponse.Payments != null");
                int cont = 0;
                foreach (PostpaidService.StatusAccountLDIPostPaid item in objGetStatusAccountLDIResponse.Payments)
                {
                    listCollectionStatusAccountLDIPayment.Add(new POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDIPayment()
                    {
                        N = cont++,
                        Payment = item.Payment,
                        ReferenceNumber = "",
                        ClaroBillId = item.ClaroBillId,
                        LdiBillId = item.LdiBillId,
                        OperatorShort = item.OperatorShort,
                        BankDesc = item.BankDesc,
                        RegistryDate = item.RegistryDate,
                        PaymentDate = item.PaymentDate,
                        AmountPen = item.AmountPen
                    });
                }
                Claro.Web.Logging.Info(strIdSession, objGetStatusAccountLDIRequest.audit.transaction, "objCollectionStatusAccountLDIModel.listPaymentStatusAccountLDIModel");
                objCollectionStatusAccountLDIModel.listPaymentStatusAccountLDIModel = listCollectionStatusAccountLDIPayment;
            }
            Claro.Web.Logging.Info(strIdSession, objGetStatusAccountLDIRequest.audit.transaction, "objCollectionStatusAccountLDIModel");
            return objCollectionStatusAccountLDIModel;
        }

        public JsonResult ExportStatusAccountLDI(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId, string strType)
        {
            string path;
            POSTPAID_DATACOLLECTION.CollectionStatusAccountLDIModel objStatusAccountLDI = ListStatusAccountLDI(strIdSession, strCustomerId, strNameClient, strNumberServices, strContactId);

            if (strType.Equals("B"))
            {
                List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI> listCollectionStatusAccountLDI = new List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI>();
                if (objStatusAccountLDI.listBillStatusAccountLDIModel == null)
                {
                    listCollectionStatusAccountLDI.Add(new POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI()
                    {
                        N = 0,
                        Detalle = "",
                        ClaroBillId = "",
                        LdiBillId = "",
                        OperatorShort = "",
                        RegistryDate = "",
                        EmittedDate = "",
                        MaturityDate = "",
                        BillTotal = 0,
                        ImportePend = 0,
                        Status = "",
                        AmountNotRequired = 0
                    });
                    objStatusAccountLDI.listBillStatusAccountLDIModel = listCollectionStatusAccountLDI;
                }

                path = oExcelHelper.ExportExcel(objStatusAccountLDI, TemplateExcel.CONST_STATUSACCOUNTLDI);
            }

            else
            {
                List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDIPayment> listCollectionStatusAccountLDIPayment = new List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDIPayment>();
                List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI> listCollectionStatusAccountLDI = new List<POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI>();
                listCollectionStatusAccountLDI.Add(new POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDI()
                {
                    N = 0,
                    Detalle = "",
                    ClaroBillId = "",
                    LdiBillId = "",
                    OperatorShort = "",
                    RegistryDate = "",
                    EmittedDate = "",
                    MaturityDate = "",
                    BillTotal = 0,
                    ImportePend = 0,
                    Status = "",
                    AmountNotRequired = 0
                });
                objStatusAccountLDI.listBillStatusAccountLDIModel = listCollectionStatusAccountLDI;
                if (objStatusAccountLDI.listPaymentStatusAccountLDIModel == null)
                {
                    listCollectionStatusAccountLDIPayment.Add(new POSTPAID_HELPER.CollectionStatusAccountLDI.CollectionStatusAccountLDIPayment()
                    {
                        N = 0,
                        Payment = "",
                        ReferenceNumber = "",
                        ClaroBillId = "",
                        LdiBillId = "",
                        OperatorShort = "",
                        BankDesc = "",
                        RegistryDate = "",
                        PaymentDate = "",
                        AmountPen = 0
                    });
                    objStatusAccountLDI.listPaymentStatusAccountLDIModel = listCollectionStatusAccountLDIPayment;
                }
                path = oExcelHelper.ExportExcel(objStatusAccountLDI, TemplateExcel.CONST_STATUSACCOUNTLDI_PAYMENT);
            }

            return Json(path);
        }

        #endregion


        #region [EstadoCuentaHR]
        ///se agrega strPlatAT csIdPub INC000003284774
        public ActionResult StatusAccountHR(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId, string strPlatAT, string strCsIdPub)
        {
            return PartialView(ListStatusAccountHR(strIdSession, strCustomerId, strNameClient, strNumberServices, strContactId, strPlatAT, strCsIdPub));
        }
        ///se agrega strPlatAT csIdPub INC000003284774
        public POSTPAID_DATACOLLECTION.CollectionStatusAccountModelHR ListStatusAccountHR(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId, string strPlatAT, string strCsIdPub)
        {
            PostpaidService.StatusAccountResponsePostPaid objStatusAccountResponsePostPaid = null;
            PostpaidService.StatusAccountRequestPostPaid objStatusAccountRequestPostPaid = new PostpaidService.StatusAccountRequestPostPaid
            {
                CustomerId = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                plataformaAT = strPlatAT,
                csIdPub = strCsIdPub,
                isHR = true
            };

            POSTPAID_DATACOLLECTION.CollectionStatusAccountModelHR objPinListStatusAccountHR = new POSTPAID_DATACOLLECTION.CollectionStatusAccountModelHR()
            {
                ClientId = strContactId,
                NameClient = strNameClient,
                NumberServices = strNumberServices
            };

            try
            {
                objStatusAccountResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.StatusAccountResponsePostPaid>(
                    () => { return objPostpaidService.StatusAccount(objStatusAccountRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStatusAccountRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objStatusAccountRequestPostPaid.audit.transaction);
            }

            if (objStatusAccountResponsePostPaid.ListStatusAccount != null && objStatusAccountResponsePostPaid.ListStatusAccount.Count > 0)
            {
                objPinListStatusAccountHR.listStatusAccountModel = new List<POSTPAID_HELPER.CollectionStatusAccountHR.CollectionStatusAccountHR>();
                foreach (PostpaidService.StatusAccountAOCPostPaid item in objStatusAccountResponsePostPaid.ListStatusAccount)
                {
                    objPinListStatusAccountHR.listStatusAccountModel.Add(new POSTPAID_HELPER.CollectionStatusAccountHR.CollectionStatusAccountHR()
                    {
                        Document = PrintWhithoutLinkHR(item.Documento, item.Tipo) + "|" + PrintLinkHR(item.Documento, item.Tipo),
                        InvoiceAssociated = PrintWhithoutLinkHR(item.Documento, item.Tipo) + "|" + PrintLink(item.Documento, item.Tipo),
                        DescriptionPaid = item.DescripcionPago,
                        Type = PrintType(item.Tipo),
                        DateRegister =
                        (item.FechaRegistro == null || item.FechaRegistro == "") ? "" : Convert.ToDate(item.FechaRegistro).ToString("dd/MM/yyyy"),
                        DateIssue = (item.FechaEmision == null || item.FechaEmision == "") ? "" : Convert.ToDate(item.FechaEmision).ToString("dd/MM/yyyy"),
                        DateDue =
                        (item.FechaVencimiento == null || item.FechaVencimiento == "") ? "" : Convert.ToDate(item.FechaVencimiento).ToString("dd/MM/yyyy"),
                        Charge = item.Cargo,
                        Payment = item.Abono,
                        ImportPending = item.ImportePendiente,
                        Balance = CalculateBalanceHR(item.Tipo, item.Cargo, item.Abono)
                    });

                }
            }


            return objPinListStatusAccountHR;
        }
        private string PrintLink(string strDocument, string strType)
        {
            try
            {
                System.Text.StringBuilder cadena = new System.Text.StringBuilder();

                for (int i = 0; i < strDocument.Length; i++)
                {
                    if (strDocument[i].ToString() != "-")
                    {
                        cadena.Append(strDocument[i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
                if (strType == KEY.AppSettings("strNombreDocumento") && cadena.ToString() == KEY.AppSettings("strNumeroDocumento"))
                {

                    return strDocument;
                }

                return "";



            }
            catch (Exception)
            {

                throw;

            }

        }
        private static string PrintType(string strType)
        {

            if (strType == KEY.AppSettings("strNombreDocumento"))
                return "Hoja Resumen";
            else return strType;


        }
        private string PrintWhithoutLinkHR(string strDocument, string strType)
        {
            try
            {
                System.Text.StringBuilder cadena = new System.Text.StringBuilder();

                for (int i = 0; i < strDocument.Length; i++)
                {
                    if (strDocument[i].ToString() != "-")
                    {
                        cadena.Append(strDocument[i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }

                string strDocumentFinal = string.Empty;

                if (strType == KEY.AppSettings("strNombreDocumento") && cadena.ToString() != KEY.AppSettings("strNumeroDocumento"))
                {
                    return strDocument;
                }
                if (strType != KEY.AppSettings("strNombreDocumento") && cadena.ToString() == KEY.AppSettings("strNumeroDocumento"))
                {
                    return strDocument;
                }
                if (strType == KEY.AppSettings("strNombreDocumento") && cadena.ToString() == KEY.AppSettings("strNumeroDocumento"))
                {
                    return strDocumentFinal;
                }
                if (strType != KEY.AppSettings("strNombreDocumento") && cadena.ToString() != KEY.AppSettings("strNumeroDocumento"))
                {
                    return strDocument;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "";

        }

        private string CalculateBalanceHR(string strType, string strCharge, string strPayment)
        {
            double PaymentStatusAccount = Claro.Constants.NumberZero;
            string strBalance = "";
            double dblBalance;
            string strChargeA = (strCharge.Trim() == "" ? Claro.Constants.NumberZeroString : strCharge.Trim());
            string strPaymentA = (strPayment.Trim() == "" ? Claro.Constants.NumberZeroString : strPayment.Trim());

            if (string.Equals(strType, Claro.SIACU.Constants.BillMajuscule, StringComparison.OrdinalIgnoreCase))
            {
                dblBalance = Convert.ToDouble(strChargeA) - Convert.ToDouble(strPaymentA);
                PaymentStatusAccount = PaymentStatusAccount + dblBalance;
                strBalance = dblBalance.ToString(Claro.Constants.FormatDouble);
            }

            if (((string.Equals(strType, Claro.SIACU.Constants.Payment, StringComparison.OrdinalIgnoreCase))
                        || ((string.Equals(strType, Claro.SIACU.Constants.AdjustmentMajuscule, StringComparison.OrdinalIgnoreCase))
                        || (string.Equals(strType, Claro.SIACU.Constants.TransferBad, StringComparison.OrdinalIgnoreCase)))))
            {
                dblBalance = Convert.ToDouble(PaymentStatusAccount);
                dblBalance = dblBalance + Convert.ToDouble(strChargeA) - Convert.ToDouble(strPayment);
                strBalance = dblBalance.ToString(Claro.Constants.FormatDouble);
            }

            return strBalance;
        }

        //Se agrega plataforma y csIdPub INC000003284774
        public JsonResult AccountExportStatusAccountHR(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId, string strPlatAT, string strCsIdPub)
        {
            POSTPAID_DATACOLLECTION.CollectionStatusAccountModelHR objCollectionStatusAccountModelHR = ListStatusAccountHR(strIdSession, strCustomerId, strNameClient, strNumberServices, strContactId, strPlatAT, strCsIdPub);
            if (objCollectionStatusAccountModelHR.listStatusAccountModel != null && objCollectionStatusAccountModelHR.listStatusAccountModel.Count > 0)
            {
                foreach (var item in objCollectionStatusAccountModelHR.listStatusAccountModel)
                {
                    if (!string.IsNullOrWhiteSpace(item.Document))
                    {
                        if (item.Document.Split('|')[0] == "")
                        {
                            item.Document = item.Document.Split('|')[1];
                        }
                        else
                        {
                            item.Document = item.Document.Split('|')[0];
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(item.InvoiceAssociated))
                    {
                        if (item.InvoiceAssociated.Split('|')[0] == "")
                        {
                            item.InvoiceAssociated = item.InvoiceAssociated.Split('|')[1];
                        }
                        else
                        {
                            item.InvoiceAssociated = item.InvoiceAssociated.Split('|')[0];
                        }
                    }

                }
            }

            string path = oExcelHelper.ExportExcel(objCollectionStatusAccountModelHR, TemplateExcel.CONST_STATUSACCOUNTHR);
            return Json(path);
        }

        #endregion

        #region [CuotaEquipo]

        public ActionResult FeeEquipment(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId)
        {
            return View(ListFeeEquipment(strIdSession, strCustomerId, strNameClient, strNumberServices, strContactId));
        }


        public POSTPAID_DATACOLLECTION.CollectionFeeEquipmentModel ListFeeEquipment(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId, bool isMovil = false)
        {

            PostpaidService.FeeEquipmentResponsePostPaid objFeeEquipmentResponsePostPaid = null;
            PostpaidService.FeeEquipmentRequestPostPaid objFeeEquipmentRequestPostPaid = new PostpaidService.FeeEquipmentRequestPostPaid()
            {
                CustomerId = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
            };

            POSTPAID_DATACOLLECTION.CollectionFeeEquipmentModel objListFeeEquipment = new POSTPAID_DATACOLLECTION.CollectionFeeEquipmentModel()
            {
                ClientId = strContactId,
                NameClient = strNameClient,
                NumberServices = strNumberServices,
                listFeeEquipmentModel = new List<POSTPAID_HELPER.CollectionFeeEquipment.CollectionFeeEquipment>(),
                listFeeEquipmentModelMovil = new List<POSTPAID_HELPER.CollectionFeeEquipment.CollectionFeeEquipmentMovil>(),
            };

            try
            {
                objFeeEquipmentResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.FeeEquipmentResponsePostPaid>(() => { return objPostpaidService.FeeEquipment(objFeeEquipmentRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objFeeEquipmentRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objFeeEquipmentRequestPostPaid.audit.transaction);
            }






            if (objFeeEquipmentResponsePostPaid.ListFeeEquipmentccount != null)
            {


                int num = 0;
                foreach (PostpaidService.FeeEquipmentPostPaid item in objFeeEquipmentResponsePostPaid.ListFeeEquipmentccount)
                {

                    num++;

                    if (isMovil == false)
                    {
                        objListFeeEquipment.listFeeEquipmentModel.Add(new POSTPAID_HELPER.CollectionFeeEquipment.CollectionFeeEquipment()
                        {
                            CorrelativeNumber = Claro.Convert.ToInt(num),
                            Type = Claro.Convert.ToString(item.Tipo_documento),
                            Document = Claro.Convert.ToString(item.numero_documento),
                            NumberSisact = Claro.Convert.ToString(item.numero_sisact),
                            NumberFunding = Claro.Convert.ToString(item.numero_cve_financiamiento),
                            NumberIMEI = Claro.Convert.ToString(item.numero_imei),
                            DateIssue = Claro.Convert.ToString(item.fecha_emision_cuota),
                            DateDue = Claro.Convert.ToString(item.fecha_vencimiento_cuota),
                            ImportOrigin = Claro.Convert.ToString(item.importe_original_cuota),
                            ImportPending = Claro.Convert.ToString(item.saldo_pendiente_cuota),
                            State = Claro.Convert.ToString(item.estado_cuota)
                        });
                    }
                    else
                    {
                        objListFeeEquipment.listFeeEquipmentModelMovil.Add(new POSTPAID_HELPER.CollectionFeeEquipment.CollectionFeeEquipmentMovil()
                        {
                            CorrelativeNumber = Claro.Convert.ToInt(num),
                            Document = Claro.Convert.ToString(item.numero_documento),
                            NumberSisact = Claro.Convert.ToString(item.numero_sisact),
                            NumberFunding = Claro.Convert.ToString(item.numero_cve_financiamiento),
                            NumberIMEI = Claro.Convert.ToString(item.numero_imei),
                            DateIssue = Claro.Convert.ToString(item.fecha_emision_cuota),
                            DateDue = Claro.Convert.ToString(item.fecha_vencimiento_cuota),
                            ImportOrigin = Claro.Convert.ToString(item.importe_original_cuota),
                            ImportPending = Claro.Convert.ToString(item.saldo_pendiente_cuota),
                            State = Claro.Convert.ToString(item.estado_cuota)
                        });
                    }
                }

            }


            if (objListFeeEquipment.listFeeEquipmentModel != null)
            {



                if (objListFeeEquipment.listFeeEquipmentModel.Count == 0)
                {


                    objListFeeEquipment.ImportOrigin = Claro.Constants.InitializesImporte;
                    objListFeeEquipment.ImportPending = Claro.Constants.InitializesImporte;
                }
                objListFeeEquipment.ImportOrigin = objFeeEquipmentResponsePostPaid.strSumImporteOriginal;
                objListFeeEquipment.ImportPending = objFeeEquipmentResponsePostPaid.strsumImportePendiente;
            }
            else
            {


                objListFeeEquipment.ImportOrigin = Claro.Constants.InitializesImporte;
                objListFeeEquipment.ImportPending = Claro.Constants.InitializesImporte;
            }


            return objListFeeEquipment;
        }


        public JsonResult CollectionExportFeeEquipment(string strIdSession, string strCustomerId, string strNameClient, string strNumberServices, string strContactId, bool boolIsMovil)
        {
            POSTPAID_DATACOLLECTION.CollectionFeeEquipmentModel objFeeEquipment = ListFeeEquipment(strIdSession, strCustomerId, strNameClient, strNumberServices, strContactId, boolIsMovil);

            string strPath = string.Empty;

            if (!boolIsMovil)
            {
                strPath = oExcelHelper.ExportExcel(objFeeEquipment, TemplateExcel.CONST_FEEEQUIPMENT);
            }
            else
            {
                strPath = oExcelHelper.ExportExcel(objFeeEquipment, KEY.AppSettings("CONST_FEEEQUIPMENT_MOVIL"));
            }

            SecurityAudit.AuditRequest objaudit = App_Code.Common.CreateAuditRequest<SecurityAudit.AuditRequest>(strIdSession);
            try
            {
                string strText = Claro.SIACU.Constants.ConsultStatusAccount + strNumberServices + Claro.SIACU.Constants.CustomerCode + strCustomerId + Claro.SIACU.Constants.WindowTypeFeeEquipment;
                Claro.Web.Logging.ExecuteMethod<string>(() => { return App_Code.Common.InsertAudit(objaudit, strNumberServices, KEY.AppSettings("strAudiTraCodCuotaEquipo"), strText); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }

            return Json(strPath);
        }

        #endregion

        public ActionResult AssociatedClaims()
        {
            return PartialView();
        }

        public ActionResult DeferredCollections()
        {
            return PartialView();
        }

        public JsonResult GetDeferredCollections(string strIdSession, string nameClient, string numberServices, string contratoId, PostpaidService.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            return Json(new { data = GetModelDeferredCollections(strIdSession, nameClient, numberServices, contratoId, objBSS_StatusAccountRequest) });
        }


        public JsonResult GetInteraction(string strIdSession, PostpaidService.InteractionRequestPostPaid objInteractionRequestPostPaid)
        {

            PostpaidService.InteractionResponsePostPaid objInteractionResponsePostPaid;

            try
            {
                objInteractionRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objInteractionResponsePostPaid = Claro.Web.Logging.ExecuteMethod<PostpaidService.InteractionResponsePostPaid>(() => { return objPostpaidService.GetInteraction(objInteractionRequestPostPaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objInteractionRequestPostPaid.audit.transaction, ex.Message);
                throw new Claro.MessageException(objInteractionRequestPostPaid.audit.transaction);
            }

            return Json(new { interaction = objInteractionResponsePostPaid.IdInteraction });
        }


        public Models.PostpaidDataCollection.DeferredCollectionsModel GetModelDeferredCollections(string strIdSession, string nameClient, string numberServices, string contratoId, PostpaidService.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            PostpaidService.BSS_StatusAccountResponse objBSS_StatusAccountResponse;

            try
            {
                objBSS_StatusAccountRequest.consultarEstadoCuenta = new PostpaidService.consultarEstadoCuenta();
                objBSS_StatusAccountRequest.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                objBSS_StatusAccountResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.BSS_StatusAccountResponse>(() => { return objPostpaidService.GetBSS_StatusAccount(objBSS_StatusAccountRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objBSS_StatusAccountRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objBSS_StatusAccountRequest.audit.transaction);
            }

            Models.PostpaidDataCollection.DeferredCollectionsModel objDeferredCollectionsModel = new POSTPAID_DATACOLLECTION.DeferredCollectionsModel();

            if (objBSS_StatusAccountResponse.responseDataConsultar.listaCobranzaDiferida != null)
            {
                List<Helpers.Postpaid.DeferredCollectionsHelper.DeferredCollections> listDeferredCollections = new List<POSTPAID_HELPER.DeferredCollectionsHelper.DeferredCollections>();

                int intNumero = Claro.Constants.NumberZero;
                foreach (PostpaidService.cobranzaDiferida item in objBSS_StatusAccountResponse.responseDataConsultar.listaCobranzaDiferida)
                {
                    intNumero++;
                    listDeferredCollections.Add(new Helpers.Postpaid.DeferredCollectionsHelper.DeferredCollections()
                    {
                        Nro = intNumero,
                        Cuenta = item.cuenta,
                        RazonSocial = item.razonSocial,
                        NroOCC = item.nroOcc,
                        FacturaSeleccionada = item.factSeleccionada,
                        Periodo = item.periodos == "0" ? "Facturadas" : item.periodos == "1" ? "No Facturadas" : item.periodos,
                        Importe = Convert.ToDecimal(item.importe),
                        NombreOCC = item.nombreOcc,
                        Comentario = item.comentario,
                        FechaIngreso = item.fechaIngreso,
                        CuentaContable = item.cuentaContable,
                        FechaValidez = item.fechaValidez
                    });
                }

                objDeferredCollectionsModel.DeferredCollections = listDeferredCollections;
                objDeferredCollectionsModel.TotalBilledAmount = listDeferredCollections.Where(x => x.Periodo == "Facturadas").Sum(x => x.Importe);
                objDeferredCollectionsModel.TotalAmountNotBilled = listDeferredCollections.Where(x => x.Periodo == "No Facturadas").Sum(x => x.Importe);
                objDeferredCollectionsModel.NameClient = nameClient;
                objDeferredCollectionsModel.NumberServices = numberServices;
                objDeferredCollectionsModel.ContratoId = contratoId;
            }

            return objDeferredCollectionsModel;
        }

        public JsonResult ExportDeferredCollections(string strIdSession, string nameClient, string numberServices, string contratoId, PostpaidService.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            Models.PostpaidDataCollection.DeferredCollectionsModel objDeferredCollectionsModel = GetModelDeferredCollections(strIdSession, nameClient, numberServices, contratoId, objBSS_StatusAccountRequest);
            string path = oExcelHelper.ExportExcel(objDeferredCollectionsModel, KEY.AppSettings("CONST_DEFERREDCOLLECTIONS"));
            return Json(path);
        }

        public ActionResult UnpaidDebt()
        {
            return PartialView();
        }
        public ActionResult ExpiredDebt()
        {
            return PartialView();
        }




        public JsonResult GetDescriptions(string strIdSession, CommonService.GetDescriptionsRequest objGetDescriptionsRequest)
        {
            CommonService.GetDescriptionsResponse objGetDescriptionsResponse = null;
            try
            {
                objGetDescriptionsRequest.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
                {

                    objGetDescriptionsResponse = Claro.Web.Logging.ExecuteMethod<CommonService.GetDescriptionsResponse>(() =>
                    {
                        return oServiceCommon.GetDescriptions(objGetDescriptionsRequest);
                    });
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetDescriptionsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objGetDescriptionsRequest.audit.transaction);
            }
            return Json(new { data = objGetDescriptionsResponse });
        }

        public JsonResult GetDueTooltip()
        {
            var DictionaryCaptions = new Dictionary<string, string>
            {
                { "strDueToolTip", KEY.AppSettings("strMensajeInfoDeudaTotal") }
            };

            return new JsonResult
            {
                Data = DictionaryCaptions,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult ListUnpaidDebt(string strIdSession, string strCustomerId)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "strCustomerId : " + strCustomerId);

            PostpaidService.consultarEstadoCuenta objConsultarEstadoCuenta = new PostpaidService.consultarEstadoCuenta();
            objConsultarEstadoCuenta.pCliNroCuenta = strCustomerId;

            PostpaidService.BSS_StatusAccountResponse objStatusAccountResponse = new PostpaidService.BSS_StatusAccountResponse();
            PostpaidService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.BSS_StatusAccountRequest objStatusAccountRequest = new PostpaidService.BSS_StatusAccountRequest
            {
                audit = objAudit,
                consultarEstadoCuenta = objConsultarEstadoCuenta,
                cuenta = "",
                numeroDocumentos = "",
                periodo = "",
                tipoConsulta = "1"   //Deuda No Vencida  
            };

            try
            {
                objStatusAccountResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.BSS_StatusAccountResponse>(
                    () => { return objPostpaidService.GetBSS_StatusAccount(objStatusAccountRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStatusAccountRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objStatusAccountRequest.audit.transaction);
            }

            var listCollectionStatusAccount = new List<POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount>();

            if (objStatusAccountResponse != null && objStatusAccountResponse.responseDataConsultar.deudaNoVencida.xEstadoCuenta != null)
            {
                var num = 0;
                try
                {
                    foreach (var item in objStatusAccountResponse.responseDataConsultar.deudaNoVencida.xEstadoCuenta)
                    {
                        foreach (var det in item.xDetalleTrx)
                        {
                            num += 1;
                            listCollectionStatusAccount.Add(new POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount()
                            {
                                CorrelativeNumber = num,
                                Document = det.xNroDocumento,
                                DateDue = (det.xFechaVencimiento == null || det.xFechaVencimiento == "") ? "" : DateTime.Parse(det.xFechaVencimiento).ToString("dd/MM/yyyy"),
                                DateIssue = (det.xFechaEmision == null || det.xFechaEmision == "") ? "" : Convert.ToDate(det.xFechaEmision).ToString("dd/MM/yyyy"),
                                ImportPending = String.IsNullOrEmpty(det.xSaldoDocumento) ? "" : det.xSaldoDocumento,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objStatusAccountRequest.audit.transaction, string.Format("Fila : {0}, Error:{1}", num.ToString(), ex.Message));

                }
            }

            return new JsonResult
            {
                Data = listCollectionStatusAccount,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public JsonResult ListExpiredDebt(string strIdSession, string strCustomerId)
        {
            PostpaidService.consultarEstadoCuenta objConsultarEstadoCuenta = new PostpaidService.consultarEstadoCuenta();
            objConsultarEstadoCuenta.pCliNroCuenta = strCustomerId;

            PostpaidService.BSS_StatusAccountResponse objStatusAccountResponse = new PostpaidService.BSS_StatusAccountResponse();
            PostpaidService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.BSS_StatusAccountRequest objStatusAccountRequest = new PostpaidService.BSS_StatusAccountRequest
            {
                audit = objAudit,
                consultarEstadoCuenta = objConsultarEstadoCuenta,
                cuenta = "",
                numeroDocumentos = "",
                periodo = "",
                tipoConsulta = "2"   //Deuda Vencida  
            };

            try
            {
                objStatusAccountResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.BSS_StatusAccountResponse>(
                    () => { return objPostpaidService.GetBSS_StatusAccount(objStatusAccountRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStatusAccountRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objStatusAccountRequest.audit.transaction);
            }

            var listCollectionStatusAccount = new List<POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount>();

            if (objStatusAccountResponse != null && objStatusAccountResponse.responseDataConsultar.deudaVencida.xEstadoCuenta != null)
            {
                var num = 0;
                try
                {
                    foreach (var item in objStatusAccountResponse.responseDataConsultar.deudaVencida.xEstadoCuenta)
                    {
                        foreach (var det in item.xDetalleTrx)
                        {
                            num += 1;
                            listCollectionStatusAccount.Add(new POSTPAID_HELPER.CollectionStatusAccount.CollectionStatusAccount()
                            {
                                CorrelativeNumber = num,
                                Document = det.xNroDocumento,
                                DateDue = (det.xFechaVencimiento == null || det.xFechaVencimiento == "") ? "" : DateTime.Parse(det.xFechaVencimiento).ToString("dd/MM/yyyy"),
                                DateIssue = (det.xFechaEmision == null || det.xFechaEmision == "") ? "" : Convert.ToDate(det.xFechaEmision).ToString("dd/MM/yyyy"),
                                ImportPending = String.IsNullOrEmpty(det.xSaldoDocumento) ? "" : det.xSaldoDocumento,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objStatusAccountRequest.audit.transaction, string.Format("Fila : {0}, Error:{1}", num.ToString(), ex.Message));

                }
            }

            return new JsonResult
            {
                Data = listCollectionStatusAccount,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //PROY-140464 Ajustes - INI

        public ActionResult CancelInvoice(string strIdSession, string strDocumentInvoice, string strCharge, string strPayment)
        {
            ViewData["nroDocumentInvoice"] = strDocumentInvoice;
            ViewData["charge"] = strCharge;
            ViewData["payment"] = strPayment;
            return PartialView();
        }

        public JsonResult ListReasonCancelInvoice(string strIdSession)
        {
            var objReasonCancelInvoiceResponse = new ReasonCancelInvoiceResponse();
            var objReasonCancelInvoiceRequest = new ReasonCancelInvoiceRequest();
            ItemGeneric ReasonCancelInvoice = null;
            var ListReasonCancelInvoice = new List<ItemGeneric>();

            try
            {
                objReasonCancelInvoiceRequest.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                objReasonCancelInvoiceRequest.AuditService = new Audit();
                objReasonCancelInvoiceRequest.MessageRequest = new ReasonCancelInvoiceMessageRequest();
                objReasonCancelInvoiceRequest.MessageRequest.Header = new HeadersRequest();
                objReasonCancelInvoiceRequest.MessageRequest.Header.HeaderRequest = getHeaderRequest(KEY.AppSettings("strOperacionListarMotivos"));
                objReasonCancelInvoiceRequest.MessageRequest.Body = new ReasonCancelInvoiceBodyRequest();

                objReasonCancelInvoiceResponse = oDashboardService.GetReasonCancelInvoice(objReasonCancelInvoiceRequest);

                if (objReasonCancelInvoiceResponse != null && objReasonCancelInvoiceResponse.MessageResponse.Body.codigoRespuesta == "0")
                {
                    foreach (var item in objReasonCancelInvoiceResponse.MessageResponse.Body.listaMotivos)
                    {
                        ReasonCancelInvoice = new Claro.ItemGeneric();

                        ReasonCancelInvoice.Code = item.codigo;
                        ReasonCancelInvoice.Description = item.descripcion;

                        ListReasonCancelInvoice.Add(ReasonCancelInvoice);
                    }
                }
                else
                {
                    Claro.Web.Logging.Error(objReasonCancelInvoiceRequest.audit.Session, objReasonCancelInvoiceRequest.audit.transaction, objReasonCancelInvoiceResponse.MessageResponse.Body.codigoRespuesta + ": " + objReasonCancelInvoiceResponse.MessageResponse.Body.mensajeRespuesta);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objReasonCancelInvoiceRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objReasonCancelInvoiceRequest.audit.transaction);
            }
            return Json(new { data = ListReasonCancelInvoice }); ;
        }

        public string ExecuteCancelInvoice(string strIdSession, string numeroDocumento, string codigoAnulacion, string codigoSistemaOrigen, string comentarios, string numeroTelefonico, string puntoAtencion, string codigoUsuarioAnulacion, string tipoServicio, string motivoAnulacion, string importeAnulacion, string nombreCompletoCliente, string idCliente, string tipoDocumento, string nombreCompletoUsuarioAnulacion)
        {
            var objCancelInvoiceResponse = new CancelInvoiceResponse();
            var objCancelInvoiceRequest = new CancelInvoiceRequest();
            var seEjecutoLaAnulacion = "99";
            var periodo = Claro.SIACU.Constants.PERIODO;
            var tipoTipificacion = string.Empty;
            var fechaContable = DateTime.Now.ToString("yyyy-MM-dd");

            objCancelInvoiceRequest.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            objCancelInvoiceRequest.AuditService = new Audit();
            objCancelInvoiceRequest.MessageRequest = new CancelInvoiceMessageRequest();
            objCancelInvoiceRequest.MessageRequest.Header = new HeadersRequest();
            objCancelInvoiceRequest.MessageRequest.Header.HeaderRequest = getHeaderRequest(KEY.AppSettings("strOperacionAnularDocumento"));
            objCancelInvoiceRequest.MessageRequest.Body = new CancelInvoiceBodyRequest();

            try
            {
                Claro.Web.Logging.Info(objCancelInvoiceRequest.audit.Session, objCancelInvoiceRequest.audit.transaction, "INICIO - Proceso de anulación.");

                var TipificacionValuesResponse = new TypificationResponse();
                var TipificacionValuesRequest = new TypificationRequest();

                tipoTipificacion = IdentificarTipoTipificacion(numeroDocumento, tipoServicio, objCancelInvoiceRequest.audit.Session, objCancelInvoiceRequest.audit.transaction);

                TipificacionValuesRequest.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                TipificacionValuesRequest.AuditService = new Audit();
                TipificacionValuesRequest.TRANSACTION_NAME = tipoTipificacion;
                TipificacionValuesRequest.TELEPHONE_NUMBER = numeroTelefonico;

                TipificacionValuesResponse = oDashboardService.GetTyficationList(TipificacionValuesRequest);

                objCancelInvoiceRequest.MessageRequest.Body.interaccionConNivelType = getInteractionLevelType(TipificacionValuesResponse, tipoServicio, codigoUsuarioAnulacion, objCancelInvoiceRequest.audit.Session, objCancelInvoiceRequest.audit.transaction);

                objCancelInvoiceRequest.MessageRequest.Body.interaccionPlusType = getInteractionPlusType(numeroTelefonico, codigoUsuarioAnulacion, numeroDocumento, motivoAnulacion, importeAnulacion, nombreCompletoCliente, tipoDocumento, nombreCompletoUsuarioAnulacion);

                objCancelInvoiceRequest.MessageRequest.Body.numeroDocumento = numeroDocumento;
                objCancelInvoiceRequest.MessageRequest.Body.cuenta = idCliente;
                objCancelInvoiceRequest.MessageRequest.Body.motivoAnulacion = codigoAnulacion;
                objCancelInvoiceRequest.MessageRequest.Body.codigoSistemaOrigen = codigoSistemaOrigen;
                objCancelInvoiceRequest.MessageRequest.Body.comentarios = comentarios;
                objCancelInvoiceRequest.MessageRequest.Body.periodo = periodo;
                objCancelInvoiceRequest.MessageRequest.Body.fechaContable = fechaContable;
                objCancelInvoiceRequest.MessageRequest.Body.puntoAtencion = puntoAtencion;
                objCancelInvoiceRequest.MessageRequest.Body.usuarioAtencion = codigoUsuarioAnulacion;

                objCancelInvoiceResponse = oDashboardService.CancelInvoice(objCancelInvoiceRequest);

                if (objCancelInvoiceResponse != null && objCancelInvoiceResponse.MessageResponse.Body.codigoRespuesta == "0")
                {
                    seEjecutoLaAnulacion = objCancelInvoiceResponse.MessageResponse.Body.codigoRespuesta;
                }
                else
                {
                    Claro.Web.Logging.Error(objCancelInvoiceRequest.audit.Session, objCancelInvoiceRequest.audit.transaction, objCancelInvoiceResponse.MessageResponse.Body.codigoRespuesta + ": " + objCancelInvoiceResponse.MessageResponse.Body.mensajeRespuesta);
                }
                Claro.Web.Logging.Info(objCancelInvoiceRequest.audit.Session, objCancelInvoiceRequest.audit.transaction, "FIN - Proceso de anulación.");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCancelInvoiceRequest.audit.transaction, "ERROR - Proceso de anulación. detalle: " + ex.Message);
                throw new Claro.MessageException(objCancelInvoiceRequest.audit.transaction);
            }
            return seEjecutoLaAnulacion;
        }

        private InteractionWithLevelType getInteractionLevelType(TypificationResponse objTypificationResponse, string tipoServicio, string codigoUsuarioAnulacion, string session, string transaccion)
        {
            InteractionWithLevelType objInteractionWithLevelType = null;

            Claro.Web.Logging.Info(session, transaccion, "INICIO - Obtener detalle interacción LevelType.");

            try
            {
                var typeTypification = objTypificationResponse.ListTypification.Where(x => x.TIPO.Equals(tipoServicio)).ToList().FirstOrDefault();

                Claro.Web.Logging.Info(session, transaccion, "Parametros de entrada - tipoServicio: " + tipoServicio +
                                                            "codigoUsuarioAnulacion: " + codigoUsuarioAnulacion +
                                                            "ContactSite " + objTypificationResponse.ContactSite +
                                                            ", ObjSite: " + objTypificationResponse.ObjSite +
                                                            ", TIPO: " + typeTypification.TIPO +
                                                            ", CLASE: " + typeTypification.CLASE +
                                                            ", SUBCLASE: " + typeTypification.SUBCLASE +
                                                            ", Account: " + objTypificationResponse.Account +
                                                            ",PhoneNumber." + objTypificationResponse.PhoneNumber);

                if (typeTypification.TIPO_CODE != "" && typeTypification.SUBCLASE != "" && typeTypification.CLASE_CODE != "")
                {
                    objInteractionWithLevelType = new InteractionWithLevelType();

                    objInteractionWithLevelType.contactObjId = Int32.Parse(objTypificationResponse.ContactSite);
                    objInteractionWithLevelType.siteObjId = long.Parse(objTypificationResponse.ObjSite);
                    objInteractionWithLevelType.account = objTypificationResponse.Account;
                    objInteractionWithLevelType.phone = objTypificationResponse.PhoneNumber;
                    objInteractionWithLevelType.tipo = typeTypification.TIPO;
                    objInteractionWithLevelType.clase = typeTypification.CLASE;
                    objInteractionWithLevelType.subclase = typeTypification.SUBCLASE;
                    objInteractionWithLevelType.metodoContacto = KEY.AppSettings("MetodoContactoTelefonoDefault");
                    objInteractionWithLevelType.tipoInter = KEY.AppSettings("AtencionDefault");
                    objInteractionWithLevelType.agente = codigoUsuarioAnulacion;
                    objInteractionWithLevelType.usrProceso = KEY.AppSettings("USRProcesoSU");
                    objInteractionWithLevelType.hechoEnUno = 0;
                    objInteractionWithLevelType.notas = "";
                    objInteractionWithLevelType.flagCaso = "0";
                    objInteractionWithLevelType.resultado = KEY.AppSettings("Ninguno");
                    objInteractionWithLevelType.servafect = "";
                    objInteractionWithLevelType.inconven = "";
                    objInteractionWithLevelType.servafectCode = "";
                    objInteractionWithLevelType.inconvenCode = "";
                    objInteractionWithLevelType.coId = "";
                    objInteractionWithLevelType.codPlano = "";
                    objInteractionWithLevelType.valor1 = "";
                    objInteractionWithLevelType.valor2 = "";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(session, transaccion, "Error en obtener Interaccion. Detalle: " + ex.Message);
            }
            Claro.Web.Logging.Info(session, transaccion, "FIN - Obtener detalle interacción LevelType.");
            return objInteractionWithLevelType;
        }

        private InteractionPlusType getInteractionPlusType(string numeroTelefonico, string codigoUsuarioAnulacion, string numeroDocumento, string motivoAnulacion, string importeAnulacion, string nombreCompletoCliente, string tipoDocumento, string nombreCompletoUsuarioAnulacion)
        {
            var objInteractionPlusType = new InteractionPlusType();
            var horaAnulacion = DateTime.Now.ToString("HH:mm");
            var fechaAnulacion = DateTime.Now.ToString("dd/MM/yyyy");

            objInteractionPlusType.pNroInteraccion = "";
            objInteractionPlusType.pInter1 = horaAnulacion;
            objInteractionPlusType.pInter2 = fechaAnulacion;
            objInteractionPlusType.pInter3 = tipoDocumento;
            objInteractionPlusType.pInter4 = "";
            objInteractionPlusType.pInter5 = "";
            objInteractionPlusType.pInter6 = "";
            objInteractionPlusType.pInter7 = "";
            objInteractionPlusType.pInter8 = "";
            objInteractionPlusType.pInter9 = "";
            objInteractionPlusType.pInter10 = "";
            objInteractionPlusType.pInter11 = "";
            objInteractionPlusType.pInter12 = "";
            objInteractionPlusType.pInter13 = "";
            objInteractionPlusType.pInter14 = "";
            objInteractionPlusType.pInter15 = "";
            objInteractionPlusType.pInter16 = "";
            objInteractionPlusType.pInter17 = "";
            objInteractionPlusType.pInter18 = "";
            objInteractionPlusType.pInter19 = "";
            objInteractionPlusType.pInter20 = "";
            objInteractionPlusType.pInter21 = "";
            objInteractionPlusType.pInter22 = "";
            objInteractionPlusType.pInter23 = "";
            objInteractionPlusType.pInter24 = "";
            objInteractionPlusType.pInter25 = "";
            objInteractionPlusType.pInter26 = "";
            objInteractionPlusType.pInter27 = "";
            objInteractionPlusType.pInter28 = "";
            objInteractionPlusType.pInter29 = "";
            objInteractionPlusType.pInter30 = "";
            objInteractionPlusType.pPlusInter2Interact = "";
            objInteractionPlusType.pAdjustmentAmount = importeAnulacion;
            objInteractionPlusType.pAdjustmentReason = motivoAnulacion;
            objInteractionPlusType.pAddress = "";
            objInteractionPlusType.pAmountUnit = "";
            objInteractionPlusType.pBirthday = "";
            objInteractionPlusType.pClarifyInteraction = "";
            objInteractionPlusType.pClaroLdn1 = "";
            objInteractionPlusType.pClaroLdn2 = "";
            objInteractionPlusType.pClaroLdn3 = "";
            objInteractionPlusType.pClaroLdn4 = "";
            objInteractionPlusType.pClarolocal1 = "";
            objInteractionPlusType.pClarolocal2 = "";
            objInteractionPlusType.pClarolocal3 = "";
            objInteractionPlusType.pClarolocal4 = "";
            objInteractionPlusType.pClarolocal5 = "";
            objInteractionPlusType.pClarolocal6 = "";
            objInteractionPlusType.pContactPhone = numeroTelefonico;
            objInteractionPlusType.pDniLegalRep = "";
            objInteractionPlusType.pDocumentNumber = numeroDocumento;
            objInteractionPlusType.pEmail = "";
            objInteractionPlusType.pFirstName = nombreCompletoCliente;
            objInteractionPlusType.pFixedNumber = "";
            objInteractionPlusType.pFlagChangeUser = "";
            objInteractionPlusType.pFlagLegalRep = "";
            objInteractionPlusType.pFlagOther = "";
            objInteractionPlusType.pFlagTitular = "";
            objInteractionPlusType.pImei = "";
            objInteractionPlusType.pLastName = "";
            objInteractionPlusType.pLastnameRep = "";
            objInteractionPlusType.pLdiNumber = "";
            objInteractionPlusType.pNameLegalRep = "";
            objInteractionPlusType.pOldClaroLdn1 = "";
            objInteractionPlusType.pOldClaroLdn2 = "";
            objInteractionPlusType.pOldClaroLdn3 = "";
            objInteractionPlusType.pOldClaroLdn4 = "";
            objInteractionPlusType.pOldClarolocal1 = "";
            objInteractionPlusType.pOldClarolocal2 = "";
            objInteractionPlusType.pOldClarolocal3 = "";
            objInteractionPlusType.pOldClarolocal4 = "";
            objInteractionPlusType.pOldClarolocal5 = "";
            objInteractionPlusType.pOldClarolocal6 = "";
            objInteractionPlusType.pOldDocNumber = "";
            objInteractionPlusType.pOldFirstName = "";
            objInteractionPlusType.pOldFixedPhone = "";
            objInteractionPlusType.pOldLastName = "";
            objInteractionPlusType.pOldLdiNumber = "";
            objInteractionPlusType.pOldFixedNumber = "";
            objInteractionPlusType.pOperationType = "";
            objInteractionPlusType.pOtherDocNumber = codigoUsuarioAnulacion;
            objInteractionPlusType.pOtherFirstName = nombreCompletoUsuarioAnulacion;
            objInteractionPlusType.pOtherLastName = "";
            objInteractionPlusType.pOtherPhone = "";
            objInteractionPlusType.pPhoneLegalRep = "";
            objInteractionPlusType.pReferencePhone = "";
            objInteractionPlusType.pReason = "";
            objInteractionPlusType.pModel = "";
            objInteractionPlusType.pLotCode = "";
            objInteractionPlusType.pFlagRegistered = "";
            objInteractionPlusType.pRegistrationReason = "";
            objInteractionPlusType.pClaroNumber = "";
            objInteractionPlusType.pMonth = "";
            objInteractionPlusType.pOstNumber = "";
            objInteractionPlusType.pBasket = "";
            objInteractionPlusType.pExpireDate = "";
            objInteractionPlusType.paddress5 = "";
            objInteractionPlusType.pchargeamount = "";
            objInteractionPlusType.pcity = "";
            objInteractionPlusType.pcontactsex = "";
            objInteractionPlusType.pdepartment = "";
            objInteractionPlusType.pdistrict = "";
            objInteractionPlusType.pemailconfirmation = "";
            objInteractionPlusType.pfax = "";
            objInteractionPlusType.pflagcharge = "";
            objInteractionPlusType.pmaritalstatus = "";
            objInteractionPlusType.poccupation = "";
            objInteractionPlusType.pposition = "";
            objInteractionPlusType.preferenceaddress = "";
            objInteractionPlusType.ptypedocument = "";
            objInteractionPlusType.pzipcode = "";
            objInteractionPlusType.pIccid = "";

            return objInteractionPlusType;
        }

        public string IdentificarTipoTipificacion(string numeroDocumento, string tipoServicio, string session, string transaccion)
        {
            var tipoDocumento = string.Empty;
            var tipificacion = string.Empty;

            Claro.Web.Logging.Info(session, transaccion, "INICIO - Identificar tipo de tipificación. Parametros de entrada - numeroDocumento: " + numeroDocumento + ", tipoServicio: " + tipoServicio);
            try
            {
                if (numeroDocumento.Contains(Claro.SIACU.Constants.DOCUMENTO_NC_ND))
                {
                    switch (tipoServicio)
                    {
                        case Claro.SIACU.Constants.SERVICIO_INTERNET_INALAMBRICO:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_NCND_IFI;
                            break;
                        case Claro.SIACU.Constants.SERVICIO_FIJO_POST:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_NCND_FIJOPOST;
                            break;
                        case Claro.SIACU.Constants.gstrTipoServDTH:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_NCND_DTHPOST;
                            break;
                        case Claro.SIACU.Constants.HFC:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_NCND_HFC;
                            break;
                        case Claro.SIACU.Constants.LTE:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_NCND_LTE;
                            break;
                        case Claro.SIACU.Constants.SERVICIO_POSTPAGO:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_NCND_POSTPAGO;
                            break;
                        default:
                            tipificacion = "";
                            break;
                    }
                }
                else
                {
                    switch (tipoServicio)
                    {
                        case Claro.SIACU.Constants.SERVICIO_INTERNET_INALAMBRICO:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_DCONTROL_IFI;
                            break;
                        case Claro.SIACU.Constants.SERVICIO_FIJO_POST:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_DCONTROL_FIJOPOST;
                            break;
                        case Claro.SIACU.Constants.gstrTipoServDTH:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_DCONTROL_DTHPOST;
                            break;
                        case Claro.SIACU.Constants.HFC:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_DCONTROL_HFC;
                            break;
                        case Claro.SIACU.Constants.LTE:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_DCONTROL_LTE;
                            break;
                        case Claro.SIACU.Constants.SERVICIO_POSTPAGO:
                            tipificacion = Claro.SIACU.Constants.TRANSACCION_ANULACION_DCONTROL_POSTPAGO;
                            break;
                        default:
                            tipificacion = "";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(session, transaccion, "ERROR - Identificar tipo de tipificación. Detalle: " + ex.Message);
            }
            Claro.Web.Logging.Info(session, transaccion, "FIN - Identificar tipo de tipificación. Parametros de salida - tipificacion: " + tipificacion);
            return tipificacion;
        }

        public string GetEvaluateAmount(string idSession, string perfilDelUsuario, string monto, string codigoModalidad, string transaccion)
        {
            var modalidad = string.Empty;
            var montoPermitido = 0.0;
            var esUnMontoPermitido = "99";
            var codigoConcepto = string.Empty;

            Claro.Web.Logging.Info(idSession, "", "INICIO - Evaluar monto. Parametros de entrada - perfilDelUsuario: " + perfilDelUsuario + ", monto: " + monto + ", codigoModalidad: " + codigoModalidad + ", transaccion: " + transaccion);

            try
            {
                var unidad = KEY.AppSettings("strUnidadEvaluacionMonto");
                var objEvaluateAmountRequest = new EvaluateAmountRequest();
                var objEvaluateAmountResponse = new EvaluateAmountResponse();

                codigoConcepto = IdentificarCodigoConcepto(idSession, transaccion);

                if (string.IsNullOrEmpty(codigoConcepto) || string.IsNullOrEmpty(codigoModalidad)) throw new ArgumentNullException();

                objEvaluateAmountRequest.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(idSession);
                objEvaluateAmountRequest.AuditService = new Audit();
                objEvaluateAmountRequest.MessageRequest = new EvaluateAmountMessageRequest();
                objEvaluateAmountRequest.MessageRequest.Header = new HeadersRequest();
                objEvaluateAmountRequest.MessageRequest.Header.HeaderRequest = getHeaderRequest(KEY.AppSettings("strOperacionEvaluacionMonto"));
                objEvaluateAmountRequest.MessageRequest.Body = new EvaluateAmountBodyRequest();

                objEvaluateAmountRequest.MessageRequest.Body.perfil = perfilDelUsuario;
                objEvaluateAmountRequest.MessageRequest.Body.unidad = unidad;
                objEvaluateAmountRequest.MessageRequest.Body.tipoTelefono = codigoConcepto + codigoModalidad;
                objEvaluateAmountRequest.MessageRequest.Body.modalidad = codigoModalidad;

                objEvaluateAmountResponse = oDashboardService.GetEvaluateAmount(objEvaluateAmountRequest);

                if (objEvaluateAmountResponse != null && objEvaluateAmountResponse.MessageResponse.Body.codigoRespuesta == "0")
                {
                    montoPermitido = objEvaluateAmountResponse.MessageResponse.Body.monto;
                    esUnMontoPermitido = (montoPermitido > double.Parse(monto)) ? "1" : "0";
                }
                else
                {
                    Claro.Web.Logging.Error(objEvaluateAmountRequest.audit.Session, objEvaluateAmountRequest.audit.transaction, objEvaluateAmountResponse.MessageResponse.Body.codigoRespuesta + ": " + objEvaluateAmountResponse.MessageResponse.Body.mensajeRespuesta);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, "", ex.Message);
                throw new Claro.MessageException();
            }
            return esUnMontoPermitido;
        }

        private string IdentificarCodigoConcepto(string idSession, string transaccionProceso)
        {
            var condigoTransaccion = string.Empty;
            try
            {
                Claro.Web.Logging.Info(idSession, "", "INICIO - Identificar codigo Concepto. Parametros de entrada - transaccionProceso: " + transaccionProceso);

                var llaveListaTransacciones = KEY.AppSettings("listaTransacciones_SIAPO");
                var listaTransacciones = llaveListaTransacciones.Split('|');

                foreach (var transaccion in listaTransacciones)
                {
                    var detalleTransaccion = transaccion.Split(';');
                    if (detalleTransaccion[0] == transaccionProceso)
                    {
                        condigoTransaccion = detalleTransaccion[1];
                        break;
                    }
                }
                Claro.Web.Logging.Info(idSession, "", "FIN - Identificar codigo Concepto. Parametros de salida - condigoTransaccion: " + condigoTransaccion);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, "", ex.Message);
                throw new Claro.MessageException();
            }
            return condigoTransaccion;
        }

        public string IsValidProfile(string strIdSession, string tipoProducto, string codigoUsuarioAnulacion)
        {
            var codigoPerfilValido = string.Empty;
            var listaDePerfilesValidos = string.Empty;
            string[] listaDePerfilesDelUsuario = new string[] { };

            Claro.Web.Logging.Info(strIdSession, "", "INICIO - Validar Perfil. Parametros de entrada - tipoProducto: " + tipoProducto + ", codigoUsuarioAnulacion: " + codigoUsuarioAnulacion);

            try
            {
                listaDePerfilesValidos = (tipoProducto == Claro.SIACU.Constants.SERVICIO_POSTPAGO) ? KEY.AppSettings("strPerfiles_SIACPO") : KEY.AppSettings("strPerfiles_SIACHFC");

                var codigoAplicacion = KEY.AppSettings("CodAplicacion_SIACPO");
                var ipAplicacion = KEY.AppSettings("strWebIpCod_SIACPO");
                var nombreAplicacion = KEY.AppSettings("NombreAplicacion_SIACPO");

                var objUserProfileRequest = new UserProfileRequest();
                var objUserProfileResponse = new UserProfileResponse();

                objUserProfileRequest.audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                objUserProfileRequest.AuditService = new Audit();
                objUserProfileRequest.MessageRequest = new UserProfileMessageRequest();
                objUserProfileRequest.MessageRequest.Header = new HeadersRequest();
                objUserProfileRequest.MessageRequest.Header.HeaderRequest = getHeaderRequest(KEY.AppSettings("strOperacionObtenerPerfilesUsuario"));
                objUserProfileRequest.MessageRequest.Body = new UserProfileBodyRequest();

                objUserProfileRequest.MessageRequest.Body.idTransaccion = string.Empty;
                objUserProfileRequest.MessageRequest.Body.ipAplicacion = ipAplicacion;
                objUserProfileRequest.MessageRequest.Body.aplicacion = nombreAplicacion;
                objUserProfileRequest.MessageRequest.Body.usuarioLogin = codigoUsuarioAnulacion;
                objUserProfileRequest.MessageRequest.Body.appCod = codigoAplicacion;


                objUserProfileResponse = oDashboardService.GetProfileUser(objUserProfileRequest);

                if (objUserProfileResponse != null && objUserProfileResponse.MessageResponse.Body.errorCode == "0")
                {
                    var listaPerfilesUsuario = objUserProfileResponse.MessageResponse.Body.cursorSeguridad;
                    foreach (var perfil in listaPerfilesUsuario)
                    {
                        if (listaDePerfilesValidos.Contains(perfil.PerfcCod))
                        {
                            codigoPerfilValido = perfil.PerfcCod;
                            break;
                        }
                    }
                }
                else
                {
                    Claro.Web.Logging.Error(objUserProfileRequest.audit.Session, objUserProfileRequest.audit.transaction, objUserProfileResponse.MessageResponse.Body.errorCode + ": " + objUserProfileResponse.MessageResponse.Body.errorMsg);
                }

                Claro.Web.Logging.Info(strIdSession, "", "FIN - Validar Perfil. Parametros de salida - codigoPerfilValido: " + codigoPerfilValido);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, "", "ERROR - IsValidProfile, detalle: " + ex.Message);
            }
            return codigoPerfilValido;
        }

        private HeaderRequest getHeaderRequest(string operacion)
        {
            return new DashboardService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivo"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("moduloSiac"),
                msgType = KEY.AppSettings("msgType"),
                operation = operacion,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = KEY.AppSettings("strWsIpAjustesDoc")
            };
        }
        //PROY-140464 Ajustes - FIN
    }
}