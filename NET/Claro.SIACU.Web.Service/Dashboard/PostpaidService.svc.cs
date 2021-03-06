using Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetService;
using Claro.SIACU.Entity.Dashboard.Postpaid;
using System;
using System.ServiceModel;
using System.Collections.Generic;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetMiClaroApp;

namespace Claro.SIACU.Web.Service.Dashboard
{

    public class PostpaidService : IPostpaidService
    {
        public PostpaidService()
        {
            log4net.Config.XmlConfigurator.Configure();
        
        }

        /// <summary>
        /// Obtiene el listado de servicios Post/HFC/LTE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetServiceByCustomerCode(ServiceRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objServiceByCustomerCodeResponse;

            try
            {
                objServiceByCustomerCodeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetServiceByCustomerCode(request); });
            }
            catch (Exception ex)
            {
                objServiceByCustomerCodeResponse = null;
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objServiceByCustomerCodeResponse;
        }

        /// <summary>
        /// Obtine los datos del cliente por Nro de cuenta o nro Teléfono
        /// </summary>
        /// <param name="strAccountCustomer"></param> Nro de Cuenta del cliente
        /// <param name="strNroCellphone"></param>    Nro de Teléfono del cliente
        /// <returns></returns>
        /// 
        public Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetDataCustomer(CustomerRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse objSDataCustomerResponse = null;

            try
            {
                objSDataCustomerResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse>(() => { return Business.Dashboard.Postpaid.GetDataCustomer(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSDataCustomerResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress.OfficeAddressResponse GetAddressOfficce(OfficeAddressRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress.OfficeAddressResponse objSDataCustomerResponse;

            try
            {
                objSDataCustomerResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress.OfficeAddressResponse>(() => { return Business.Dashboard.Postpaid.GetAddressOfficce(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSDataCustomerResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt.ReceiptResponse GetDataInvoice(ReceiptRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt.ReceiptResponse objDataInvoiceResponse;
            bool isEnvioMail = false;
            try
            {
                objDataInvoiceResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt.ReceiptResponse>(() => { return Business.Dashboard.Postpaid.GetDataInvoice(request,out  isEnvioMail); });
                objDataInvoiceResponse.isEnvioEmail = isEnvioMail;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDataInvoiceResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataServiceLine(ServiceRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objDataServiceLineResponse;

            try
            {
                objDataServiceLineResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetDataServiceLine(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDataServiceLineResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetTelephoneByContractCode(ServiceRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objTelephoneByContractCodeResponse;

            try
            {
                objTelephoneByContractCodeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetTelephoneByContractCode(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTelephoneByContractCodeResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetLineDisableByContractCode(ServiceRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objLineDisableByContractCodeResponse;

            try
            {
                objLineDisableByContractCodeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetLineDisableByContractCode(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objLineDisableByContractCodeResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetInstallationAddress(CustomerRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse objInstallationAddressResponse;

            try
            {
                objInstallationAddressResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse>(() => { return Business.Dashboard.Postpaid.GetInstallationAddress(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objInstallationAddressResponse;

        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetHistoryServiceLine(ServiceRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objDataServiceLineResponse;

            try
            {
                objDataServiceLineResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetHistoryServiceLine(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDataServiceLineResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays.ActiveDaysResponse GetActiveDisableDays(ActiveDaysRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays.ActiveDaysResponse objActiveDaysResponse;

            try
            {
                objActiveDaysResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays.ActiveDaysResponse>(() => { return Business.Dashboard.Postpaid.GetActiveDisableDays(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objActiveDaysResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM.HistorySIMResponse GetHistorySIM(HistorySIMRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM.HistorySIMResponse objAHistorySIMResponse;

            try
            {
                objAHistorySIMResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM.HistorySIMResponse>(() => { return Business.Dashboard.Postpaid.GetHistorySIM(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objAHistorySIMResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoResponse GetHistoryEquipments(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoResponse objDecoResponse;

            try
            {
                objDecoResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoResponse>(() => { return Business.Dashboard.Postpaid.GetHistoryEquipments(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDecoResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse GetIMRConsult(Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse objIMRResponse;

            try
            {
                objIMRResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse>(() => { return Business.Dashboard.Postpaid.GetIMRConsult(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objIMRResponse;
        }


        public Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactResponse GetContact(Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactResponse objContactResponse;

            try
            {
                objContactResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactResponse>(() => { return Business.Dashboard.Postpaid.GetContact(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContactResponse;

        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse GetColumnsConfiguration(Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse objContactResponse;

            try
            {
                objContactResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse>(() => { return Business.Dashboard.Postpaid.GetColumnsConfiguration(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContactResponse;

        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse ContactSave(Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse objContactResponse;

            try
            {
                objContactResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse>(() => { return Business.Dashboard.Postpaid.ContactSave(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContactResponse;
        }


        /// <summary>
        /// Obtiene el monto disputa y el comportamiento de pago del cliente.
        /// </summary>
        /// <param name="strCustomerId">Id del Cliente</param>
        /// <returns>List List<Entity.Dashboard.Postpaid.Collection></returns>
        public Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection.PaymentCollectionResponse GetPaymentCollection(Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection.PaymentCollectionRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection.PaymentCollectionResponse objPaymentCollectionResponse = null;

            try
            {
                objPaymentCollectionResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection.PaymentCollectionResponse>(() => { return Business.Dashboard.Postpaid.GetPaymentCollection(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPaymentCollectionResponse;
        }

        /// <summary>
        /// Obtiene el detalle del monto de disputa.
        /// </summary>
        /// <param name="strCustomerId">Id del Cliente</param>
        /// <returns>List List<Entity.Dashboard.Postpaid.DetailAmountDispute></returns>
        public Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeResponse GetDetailAmountDispute(Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeRequest objDetailAmountDisputeRequest)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeResponse objDetailAmountDisputeResponse = null;

            try
            {
                objDetailAmountDisputeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeResponse>(() => { return Business.Dashboard.Postpaid.GetDetailAmountDispute(objDetailAmountDisputeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailAmountDisputeRequest.Audit.Session, objDetailAmountDisputeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDetailAmountDisputeResponse;
        }

        /// <summary>
        /// Obtiene la forma de pago del cliente.
        /// </summary>
        /// <param name="strCustumerId">Código del cliente</param>
        /// <returns>List List<Entity.Dashboard.Postpaid.MethodPayment></returns>

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse GetAffiliationToDebit(Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitRequest objAffiliationToDebitRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse objAffiliationToDebitResponse = null;

            try
            {
                objAffiliationToDebitResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse>(() => { return Business.Dashboard.Postpaid.GetAffiliationToDebit(objAffiliationToDebitRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAffiliationToDebitRequest.Audit.Session, objAffiliationToDebitRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objAffiliationToDebitResponse;
        }


        public Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse GetMonitoringCases(Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesRequest objMonitoringCasesRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse objMonitoringCasesResponse = null;

            try
            {
                objMonitoringCasesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse>(() => { return Business.Dashboard.Postpaid.GetMonitoringCases(objMonitoringCasesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMonitoringCasesRequest.Audit.Session, objMonitoringCasesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objMonitoringCasesResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse GetAnnotations(Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsRequest objAnnotationsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse objAnnotationsResponse = null;

            try
            {
                objAnnotationsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse>(() => { return Business.Dashboard.Postpaid.GetAnnotations(objAnnotationsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAnnotationsRequest.Audit.Session, objAnnotationsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objAnnotationsResponse;

        }

        public Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse GetDetailAnnotation(Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationRequest objDetailAnnotationsRequest)
        {
            Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse objDetailAnnotationResponse = null;
            try
            {
                objDetailAnnotationResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse>(() => { return Business.Dashboard.Postpaid.GetDetailAnnotation(objDetailAnnotationsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailAnnotationsRequest.Audit.Session, objDetailAnnotationsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objDetailAnnotationResponse;

        }

        public Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse GetWarranty(Entity.Dashboard.Postpaid.GetWarranty.WarrantyRequest objGetWarrantyRequest)
        {
            Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse objWarrantyResponse = null;

            try
            {
                objWarrantyResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse>(() => { return Business.Dashboard.Postpaid.GetWarranty(objGetWarrantyRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objWarrantyResponse;
        }

        public Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse GetSharedBag(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objGetSharedBagRequest)
        {
            Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse objSharedBagResponse = null;

            try
            {
                objSharedBagResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse>(() => { return Business.Dashboard.Postpaid.GetSharedBag(objGetSharedBagRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.InnerException.InnerException.Message);
            }

            return objSharedBagResponse;
        }

        public Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse GetRelationPlans(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveRequest objMassiveRequest)
        {
            Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse objRelationPlansResponse = null;

            try
            {
                objRelationPlansResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse>(() => { return Business.Dashboard.Postpaid.GetRelationPlans(objMassiveRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMassiveRequest.Audit.Session, objMassiveRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objRelationPlansResponse;
        }

        public Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse GetSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeRequests objSolicitudeRequests)
        {
            Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse objSolicitudeResponse = null;

            try
            {
                objSolicitudeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse>(() => { return Business.Dashboard.Postpaid.GetSolicitude(objSolicitudeRequests); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSolicitudeRequests.Audit.Session, objSolicitudeRequests.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSolicitudeResponse;
        }

        public Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse RegisterSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeRequests objRegisterSolicitudeRequests)
        {
            Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse objRegisterSolicitudeResponse;

            try
            {
                objRegisterSolicitudeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse>(() => { return Business.Dashboard.Postpaid.RegisterSolicitude(objRegisterSolicitudeRequests); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterSolicitudeRequests.Audit.Session, objRegisterSolicitudeRequests.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objRegisterSolicitudeResponse;
        }

        public Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse GetDocumentSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeRequests objDocumentSolicitudeRequests)
        {
            Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse objDocumentSolicitudeResponse = null;

            try
            {
                objDocumentSolicitudeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse>(() => { return Business.Dashboard.Postpaid.GetDocumentSolicitude(objDocumentSolicitudeRequests); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDocumentSolicitudeRequests.Audit.Session, objDocumentSolicitudeRequests.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDocumentSolicitudeResponse;
        }

        public Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse GetPinPuk(Entity.Dashboard.Postpaid.GetPinPuk.PinPukRequest objGetPinPukRequest)
        {
            Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse objPinPukResponse = null;

            try
            {
                objPinPukResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse>(() => { return Business.Dashboard.Postpaid.GetPinPuk(objGetPinPukRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetPinPukRequest.Audit.Session, objGetPinPukRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPinPukResponse;

        }

        public Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse GetSuspendedContract(Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractRequest objGetSuspendedContractRequest)
        {

            Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse objSuspendedContractResponse = null;

            try
            {
                objSuspendedContractResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse>(() => { return Business.Dashboard.Postpaid.GetSuspendedContract(objGetSuspendedContractRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetSuspendedContractRequest.Audit.Session, objGetSuspendedContractRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSuspendedContractResponse;
        }

        public Entity.Common.GetSuspendedType.SuspendedTypeResponse GetSuspendedType(Entity.Common.GetSuspendedType.SuspendedTypeRequest objSuspendedTypeRequest)
        {
            Entity.Common.GetSuspendedType.SuspendedTypeResponse objSuspendedTypeResponse;

            try
            {
                objSuspendedTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetSuspendedType.SuspendedTypeResponse>(() => { return Business.Dashboard.Postpaid.GetSuspendedType(objSuspendedTypeRequest); });

            }
            catch (Exception ex)
            {
                objSuspendedTypeResponse = null;
                Claro.Web.Logging.Error(objSuspendedTypeRequest.Audit.Session, objSuspendedTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSuspendedTypeResponse;
        }

        public Entity.Common.GetAnnotationType.AnnotationTypeResponse GetAnnotationType(Entity.Common.GetAnnotationType.AnnotationTypeRequest objAnnotationTypeRequest)
        {
            Entity.Common.GetAnnotationType.AnnotationTypeResponse objAnnotationTypeResponse;
            try
            {
                objAnnotationTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetAnnotationType.AnnotationTypeResponse>(() => { return Business.Dashboard.Postpaid.GetAnnotationType(objAnnotationTypeRequest); });
            }
            catch (Exception ex)
            {
                objAnnotationTypeResponse = null;
                Claro.Web.Logging.Error(objAnnotationTypeRequest.Audit.Session, objAnnotationTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objAnnotationTypeResponse;
        }

        public Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse GetSubAccount(Entity.Dashboard.Postpaid.GetSubAccount.SubAccountRequest objGetSubAccountRequest)
        {

            Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse objSubAccountResponse = null;

            try
            {
                objSubAccountResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse>(() => { return Business.Dashboard.Postpaid.GetSubAccount(objGetSubAccountRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetSubAccountRequest.Audit.Session, objGetSubAccountRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objSubAccountResponse;
        }

        public Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse GetCreditLimit(Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitRequest objGetCreditLimitRequest)
        {
            Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse objCreditLimitResponse = null;

            try
            {
                objCreditLimitResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse>(() => { return Business.Dashboard.Postpaid.GetCreditLimit(objGetCreditLimitRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetCreditLimitRequest.Audit.Session, objGetCreditLimitRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCreditLimitResponse;
        }

        public Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse GetRelationPlanHFCLTE(Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTERequest objGetRelationPlanHFCLTERequest)
        {
            Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse objGetRelationPlanHFCLTEResponse;
            try
            {
                objGetRelationPlanHFCLTEResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse>(() => { return Business.Dashboard.Postpaid.GetRelationPlanHFCLTE(objGetRelationPlanHFCLTERequest); });
            }
            catch (Exception ex)
            {
                objGetRelationPlanHFCLTEResponse = null;
                Claro.Web.Logging.Error(objGetRelationPlanHFCLTERequest.Audit.Session, objGetRelationPlanHFCLTERequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objGetRelationPlanHFCLTEResponse;
        }

        public Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse GetCreditLimitDetail(Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailRequest objCreditLimitDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse objCreditLimitDetailResponse = new Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse();
            try
            {
                objCreditLimitDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse>(() => { return Business.Dashboard.Postpaid.GetCreditLimitDetail(objCreditLimitDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCreditLimitDetailRequest.Audit.Session, objCreditLimitDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCreditLimitDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse GetDetailLongDistance(Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceRequest objGetDetailLongDistanceRequest)
        {
            Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse objDetailLongDistanceResponse = null;

            try
            {
                objDetailLongDistanceResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse>(() => { return Business.Dashboard.Postpaid.GetDetailLongDistance(objGetDetailLongDistanceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetDetailLongDistanceRequest.Audit.Session, objGetDetailLongDistanceRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDetailLongDistanceResponse;
        }

        public Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse GetInternationalRoamingDetail(Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailRequest objInternationalRoamingDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse objInternationalRoamingDetailResponse = null;

            try
            {
                objInternationalRoamingDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse>(() => { return Business.Dashboard.Postpaid.GetInternationalRoamingDetail(objInternationalRoamingDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInternationalRoamingDetailRequest.Audit.Session, objInternationalRoamingDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objInternationalRoamingDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse GetDebtDetail(Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailRequest objDebtDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse objDebtDetailResponse = null;

            try
            {
                objDebtDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse>(() => { return Business.Dashboard.Postpaid.GetDebtDetail(objDebtDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDebtDetailRequest.Audit.Session, objDebtDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDebtDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse GetAdditionalLocalTrafficDetail(Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailRequest objAdditionalLocalTrafficDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse objAdditionalLocalTrafficDetailResponse = null;

            try
            {
                objAdditionalLocalTrafficDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse>(() => { return Business.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail(objAdditionalLocalTrafficDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAdditionalLocalTrafficDetailRequest.Audit.Session, objAdditionalLocalTrafficDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objAdditionalLocalTrafficDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse GetConsumeLocalTrafficDetail(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailRequest objConsumeLocalTrafficDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse objConsumeLocalTrafficDetailResponse = null;

            try
            {
                objConsumeLocalTrafficDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse>(() => { return Business.Dashboard.Postpaid.GetConsumeLocalTrafficDetail(objConsumeLocalTrafficDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLocalTrafficDetailRequest.Audit.Session, objConsumeLocalTrafficDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objConsumeLocalTrafficDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse GetDetailsOtherConcepts(Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsRequest objDetailsOtherConceptsRequest)
        {
            Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse objDetailsOtherConceptsResponse = null;

            try
            {
                objDetailsOtherConceptsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse>(() => { return Business.Dashboard.Postpaid.GetDetailsOtherConcepts(objDetailsOtherConceptsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailsOtherConceptsRequest.Audit.Session, objDetailsOtherConceptsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDetailsOtherConceptsResponse;
        }

        public Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse GetTimServiceDetails(Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsRequest objTimServiceDetailsRequest)
        {
            Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse objTimServiceDetailsResponse = null;

            try
            {
                objTimServiceDetailsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse>(() => { return Business.Dashboard.Postpaid.GetTimServiceDetails(objTimServiceDetailsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTimServiceDetailsRequest.Audit.Session, objTimServiceDetailsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objTimServiceDetailsResponse;
        }

        public Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse GetHistoryInvoice(Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceRequest objHistoryInvoiceRequest)
        {
            Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse objHistoryInvoiceResponse = null;
           try
            {
                objHistoryInvoiceResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse>(() => { return Business.Dashboard.Postpaid.GetHistoryInvoice(objHistoryInvoiceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoryInvoiceRequest.Audit.Session, objHistoryInvoiceRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objHistoryInvoiceResponse;
        }

        public Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse SMSDetails(Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsRequest objSMSDetailsRequest)
        {
            Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse objSMSDetailsResponse = null;

            try
            {
                objSMSDetailsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse>(() => { return Business.Dashboard.Postpaid.SMSDetails(objSMSDetailsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSMSDetailsRequest.Audit.Session, objSMSDetailsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objSMSDetailsResponse;
        }

        public Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse GetAmountSMSDetails(Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsRequest objSMSDetailsRequest)
        {
            Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse objSMSDetailsResponse = null;
            return objSMSDetailsResponse;
        }

        public Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetPhoneContract(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest)
        {
            Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = null;

            try
            {
                objContractedBusinessServicesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse>(() => { return Business.Dashboard.Postpaid.GetPhoneContract(objContractedBusinessServicesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objContractedBusinessServicesRequest.Audit.Session, objContractedBusinessServicesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContractedBusinessServicesResponse;
        }
        public Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetContractServices(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest)
        {
            Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = null;

            try
            {
                objContractedBusinessServicesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse>(() => { return Business.Dashboard.Postpaid.GetContractServices(objContractedBusinessServicesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objContractedBusinessServicesRequest.Audit.Session, objContractedBusinessServicesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContractedBusinessServicesResponse;
        }
        public Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetServiceBSCS(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest)
        {
            Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = null;

            try
            {
                objContractedBusinessServicesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse>(() => { return Business.Dashboard.Postpaid.GetServiceBSCS(objContractedBusinessServicesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objContractedBusinessServicesRequest.Audit.Session, objContractedBusinessServicesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objContractedBusinessServicesResponse;
        }
        public Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse GetComputerQuery(Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryRequest objComputerQueryRequest)
        {

            Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse objComputerQueryResponse;

            try
            {
                objComputerQueryResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse>(() => { return Business.Dashboard.Postpaid.GetComputerQuery(objComputerQueryRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objComputerQueryRequest.Audit.Session, objComputerQueryRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objComputerQueryResponse;
        }
        public Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse GetScheduledTransaction(Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionRequest objScheduledTransactionRequest)
        {
            Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse objScheduledTransactionResponse;

            try
            {
                objScheduledTransactionResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse>(() => { return Business.Dashboard.Postpaid.GetScheduledTransaction(objScheduledTransactionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objScheduledTransactionRequest.Audit.Session, objScheduledTransactionRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objScheduledTransactionResponse;
        }
        public Entity.Common.GetPetitionsType.PetitionsTypeResponse GetPetitionsType(Entity.Common.GetPetitionsType.PetitionsTypeRequest objPetitionsTypeRequest)
        {
            Entity.Common.GetPetitionsType.PetitionsTypeResponse objPetitionsTypeResponse;

            try
            {
                objPetitionsTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetPetitionsType.PetitionsTypeResponse>(() => { return Business.Dashboard.Postpaid.GetPetitionsType(objPetitionsTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPetitionsTypeRequest.Audit.Session, objPetitionsTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPetitionsTypeResponse;
        }
        public Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse GetPetitions(Entity.Dashboard.Postpaid.GetPetitions.PetitionRequest objPetitionRequest)
        {
            Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse objPetitionResponse = null;

            try
            {
                objPetitionResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse>(() => { return Business.Dashboard.Postpaid.GetPetitions(objPetitionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                Claro.Web.Logging.Error(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objPetitionResponse;
        }
        public Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse GetTriaciones(Entity.Dashboard.Postpaid.GetTriaciones.StriationsRequest objTriacionRequest)
        {
            Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse objTriacionesResponse = null;

            try
            {
                objTriacionesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse>(() => { return Business.Dashboard.Postpaid.GetTriaciones(objTriacionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTriacionesResponse;
        }
        public Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse GetHistoricalStriations(Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsRequest objHistoricalStriationsRequest)
        {
            Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse objHistoricalStriationsResponse = null;

            try
            {
                objHistoricalStriationsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse>(() => { return Business.Dashboard.Postpaid.GetHistoricalStriations(objHistoricalStriationsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoricalStriationsRequest.Audit.Session, objHistoricalStriationsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objHistoricalStriationsResponse;
        }
        public Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetRecharges(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = null;

            try
            {
                objRechargesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse>(() => { return Business.Dashboard.Postpaid.GetRecharges(objRechargesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objRechargesResponse;
        }
        public Entity.Dashboard.Postpaid.GetBalance.BalanceResponse GetBalance(Entity.Dashboard.Postpaid.GetBalance.BalanceRequest objBalanceRequest)
        {
            Entity.Dashboard.Postpaid.GetBalance.BalanceResponse objBalanceResponse;

            try
            {
                objBalanceResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetBalance.BalanceResponse>(() => { return Business.Dashboard.Postpaid.GetBalance(objBalanceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBalanceRequest.Audit.Session, objBalanceRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objBalanceResponse;
        }

        public Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse DueNumberDocumentOAC(Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberRequest objGetDueNumberDocumentOACRequest)
        {
            Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse objDueNumberDocumentOACResponse = null;

            try
            {
                objDueNumberDocumentOACResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse>(() => { return Business.Dashboard.Postpaid.DueNumberDocumentOAC(objGetDueNumberDocumentOACRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetDueNumberDocumentOACRequest.Audit.Session, objGetDueNumberDocumentOACRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objDueNumberDocumentOACResponse;
        }
        public Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse StatusAccount(Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountRequest objStatusAccountRequest)
        {
            Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse objStatusAccountResponse = null;

            try
            {
                objStatusAccountResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse>(() => { return Business.Dashboard.Postpaid.GetStatusAccountDifferent(objStatusAccountRequest) ; });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objStatusAccountResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataLineHistory(ServiceRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objDataServiceLineHistoryResponse = null;

            try
            {
                objDataServiceLineHistoryResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Claro.SIACU.Business.Dashboard.Postpaid.GetDataLineHistory(request); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDataServiceLineHistoryResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR.HLRResponse GetHLR(HLRRequest request)
        {
            HLRResponse objHLRResponse = null;

            try
            {

                objHLRResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR.HLRResponse>(() => { return Business.Dashboard.Postpaid.GetHLR(request); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objHLRResponse;
        }

        public Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse GetExistsAgreementOld(Entity.Dashboard.Postpaid.GetAgreement.AgreementRequest Request)
        {
            Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse ObjAgreementResponse = null;

            try
            {
                ObjAgreementResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse>(() => { return Business.Dashboard.Postpaid.GetExistsAgreementOld(Request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return ObjAgreementResponse;
        }

        public Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse GetExistAgreementNew(Entity.Dashboard.Postpaid.GetAgreement.AgreementRequest Request)
        {
            Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse ObjAgreementResponse = null;

            try
            {
                ObjAgreementResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse>(() => { return Business.Dashboard.Postpaid.GetExistAgreementNew(Request); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return ObjAgreementResponse;
        }

        public Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse StatusAccountLDI(Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIRequest objStatusAccountLDIRequest)
        {
            Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse objStatusAccountLDIResponse = null;

            try
            {
                objStatusAccountLDIResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse>(() => { return Business.Dashboard.Postpaid.StatusAccountLDI(objStatusAccountLDIRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStatusAccountLDIRequest.Audit.Session, objStatusAccountLDIRequest.Audit.Transaction, ex.Message);

                throw new FaultException(ex.Message);
            }
            return objStatusAccountLDIResponse;
        }

        public Entity.Common.GetLoanRentalType.LoanRentalResponse GetLoanRentalType(Entity.Common.GetLoanRentalType.LoanRentalTypeRequest objLoanRentalTypeRequest)
        {
            Entity.Common.GetLoanRentalType.LoanRentalResponse objLoanRentalTypeResponse;

            try
            {
                objLoanRentalTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetLoanRentalType.LoanRentalResponse>(() => { return Business.Dashboard.Postpaid.GetLoanRentalType(objLoanRentalTypeRequest); });
            }
            catch (Exception ex)
            {
                objLoanRentalTypeResponse = null;
                Claro.Web.Logging.Error(objLoanRentalTypeRequest.Audit.Session, objLoanRentalTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objLoanRentalTypeResponse;
        }


        public Entity.Common.GetLoanRentalType.LoanRentalResponse GetLoanRentalListWarehouseArea(Entity.Common.GetLoanRentalType.LoanRentalTypeRequest objLoanRentalTypeRequest)
        {
            Entity.Common.GetLoanRentalType.LoanRentalResponse objLoanRentalWarehouseAreaTypeResponse;

            try
            {
                objLoanRentalWarehouseAreaTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetLoanRentalType.LoanRentalResponse>(() => { return Business.Dashboard.Postpaid.GetLoanRentalListwarehouseArea(objLoanRentalTypeRequest); });
            }
            catch (Exception ex)
            {
                objLoanRentalWarehouseAreaTypeResponse = null;
                Claro.Web.Logging.Error(objLoanRentalTypeRequest.Audit.Session, objLoanRentalTypeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objLoanRentalWarehouseAreaTypeResponse;
        }


        public Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse LoanRental(Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResquest ObjLoanRentalRequest)
        {

            Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse objLoanRentalResponse = null;

            try
            {
                objLoanRentalResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse>(() => { return Business.Dashboard.Postpaid.LoanRental(ObjLoanRentalRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(ObjLoanRentalRequest.Audit.Session, ObjLoanRentalRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objLoanRentalResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse GetHistoryActions(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsRequest objHistoryActionsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse objHistoryActionsResponse = null;

            try
            {
                objHistoryActionsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse>(() => { return Business.Dashboard.Postpaid.GetHistoryActions(objHistoryActionsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objHistoryActionsResponse;

        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse GetFixedChargeDetailTimPro(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProRequest objFixedChargeDetailsRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse objFixedChargeDetailsResponse = null;

            try
            {
                objFixedChargeDetailsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimPro(objFixedChargeDetailsRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailsRequest.Audit.Session, objFixedChargeDetailsRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailsResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse GetFixedChargeDetailTimProOne(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneRequest objFixedChargeDetailTimProOneRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse objFixedChargeDetailTimProOneResponse = null;

            try
            {
                objFixedChargeDetailTimProOneResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimProOne(objFixedChargeDetailTimProOneRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimProOneRequest.Audit.Session, objFixedChargeDetailTimProOneRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimProOneResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse GetFixedChargeDetailTimProTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoRequest objFixedChargeDetailTimProTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse objFixedChargeDetailTimProTwoResponse = null;

            try
            {
                objFixedChargeDetailTimProTwoResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo(objFixedChargeDetailTimProTwoRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimProTwoRequest.Audit.Session, objFixedChargeDetailTimProTwoRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimProTwoResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse GetFixedChargeDetailTimMax(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxRequest objFixedChargeDetailTimMaxRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse objFixedChargeDetailTimMaxResponse = null;

            try
            {
                objFixedChargeDetailTimMaxResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimMax(objFixedChargeDetailTimMaxRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimMaxRequest.Audit.Session, objFixedChargeDetailTimMaxRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimMaxResponse;
        }

        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse GetFixedChargeDetailTimMaxTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoRequest objFixedChargeDetailTimMaxTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse objFixedChargeDetailTimMaxTwoResponse = null;

            try
            {
                objFixedChargeDetailTimMaxTwoResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo(objFixedChargeDetailTimMaxTwoRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimMaxTwoRequest.Audit.Session, objFixedChargeDetailTimMaxTwoRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimMaxTwoResponse;
        }

        public Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse GetDiscountFixedChargeDetail(Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailRequest objDiscountFixedChargeDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse objDiscountFixedChargeDetailResponse = null;

            try
            {
                objDiscountFixedChargeDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse>(() => { return Business.Dashboard.Postpaid.GetDiscountFixedChargeDetail(objDiscountFixedChargeDetailRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDiscountFixedChargeDetailRequest.Audit.Session, objDiscountFixedChargeDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objDiscountFixedChargeDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse GetPaymentCommitment(Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentRequest objPaymentCommitmentRequest)
        {
            Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse objPaymentCommitmentResponse = null;
            try
            {
                objPaymentCommitmentResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse>(() => { return Business.Dashboard.Postpaid.GetPaymentCommitment(objPaymentCommitmentRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPaymentCommitmentRequest.Audit.Session, objPaymentCommitmentRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objPaymentCommitmentResponse;

        }

        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse GetFixedChargeDetailTimProBag(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagRequest objFixedChargeDetailTimProBagRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse objFixedChargeDetailTimProBagResponse = null;

            try
            {
                objFixedChargeDetailTimProBagResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimProBag(objFixedChargeDetailTimProBagRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimProBagRequest.Audit.Session, objFixedChargeDetailTimProBagRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimProBagResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse GetFixedChargeDetailTimProBagOne(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneRequest objFixedChargeDetailTimProBagOneRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse objFixedChargeDetailTimProBagOneResponse = null;

            try
            {
                objFixedChargeDetailTimProBagOneResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne(objFixedChargeDetailTimProBagOneRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimProBagOneRequest.Audit.Session, objFixedChargeDetailTimProBagOneRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimProBagOneResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse GetFixedChargeDetailTimProBagTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoRequest objFixedChargeDetailTimProBagTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse objFixedChargeDetailTimProBagTwoResponse = null;

            try
            {
                objFixedChargeDetailTimProBagTwoResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo(objFixedChargeDetailTimProBagTwoRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimProBagTwoRequest.Audit.Session, objFixedChargeDetailTimProBagTwoRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimProBagTwoResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse GetFixedChargeDetailTimProBagThree(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeRequest objFixedChargeDetailTimProBagThreeRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse objFixedChargeDetailTimProBagThreeResponse = null;

            try
            {
                objFixedChargeDetailTimProBagThreeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree(objFixedChargeDetailTimProBagThreeRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeDetailTimProBagThreeRequest.Audit.Session, objFixedChargeDetailTimProBagThreeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeDetailTimProBagThreeResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery.HistoricDeliveryResponse GetHistoricDelivery(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery.HistoricDeliveryRequest objHistoricDeliveryRequest)
        {

            HistoricDeliveryResponse objHistoricDeliveryResponse = null;

            try
            {
                objHistoricDeliveryResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery.HistoricDeliveryResponse>(() => { return Business.Dashboard.Postpaid.GetHistoricDelivery(objHistoricDeliveryRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoricDeliveryRequest.Audit.Session, objHistoricDeliveryRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objHistoricDeliveryResponse;

        }
        public Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse GetFixedChargeTimProBagDetail(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailRequest objFixedChargeTimProBagDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse objFixedChargeTimProBagDetailResponse = null;

            try
            {
                objFixedChargeTimProBagDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeTimProBagDetail(objFixedChargeTimProBagDetailRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeTimProBagDetailRequest.Audit.Session, objFixedChargeTimProBagDetailRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeTimProBagDetailResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse GetFixedChargeTimProBagDetailOne(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneRequest objFixedChargeTimProBagDetailOneRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse objFixedChargeTimProBagDetailOneResponse = null;

            try
            {
                objFixedChargeTimProBagDetailOneResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne(objFixedChargeTimProBagDetailOneRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeTimProBagDetailOneRequest.Audit.Session, objFixedChargeTimProBagDetailOneRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeTimProBagDetailOneResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse GetFixedChargeTimProBagDetailTwo(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoRequest objFixedChargeTimProBagDetailTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse objFixedChargeTimProBagDetailTwoResponse = null;

            try
            {
                objFixedChargeTimProBagDetailTwoResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo(objFixedChargeTimProBagDetailTwoRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeTimProBagDetailTwoRequest.Audit.Session, objFixedChargeTimProBagDetailTwoRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeTimProBagDetailTwoResponse;
        }
        public Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse GetFixedChargeTimProBagDetailThree(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeRequest objFixedChargeTimProBagDetailThreeRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse objFixedChargeTimProBagDetailThreeResponse = null;

            try
            {
                objFixedChargeTimProBagDetailThreeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse>(() => { return Business.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree(objFixedChargeTimProBagDetailThreeRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFixedChargeTimProBagDetailThreeRequest.Audit.Session, objFixedChargeTimProBagDetailThreeRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFixedChargeTimProBagDetailThreeResponse;
        }

        public Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse FeeEquipment(Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentRequest objFeeEquipmentRequest)
        {

            Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse ObjFeeEquipmentResponse = null;

            try
            {

                ObjFeeEquipmentResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse>(() => { return Business.Dashboard.Postpaid.FeeEquipment(objFeeEquipmentRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return ObjFeeEquipmentResponse;
        }

        public Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse LocalTrafficDetailCallTP(Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPRequest objLocalTrafficDetailCalltRequest)
        {

            Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse objLocalTrafficDetailCallTPResponse = null;

            try
            {
                objLocalTrafficDetailCallTPResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse>(() => { return Business.Dashboard.Postpaid.GetLocalTrafficDetailCallTP(objLocalTrafficDetailCalltRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLocalTrafficDetailCalltRequest.Audit.Session, objLocalTrafficDetailCalltRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objLocalTrafficDetailCallTPResponse;
        }
        public Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse LocalTrafficDetailCallTM(Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMRequest objLocalTrafficDetailCallTMRequest)
        {

            Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse objLocalTrafficDetailCallTMResponse = null;

            try
            {
                objLocalTrafficDetailCallTMResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse>(() => { return Business.Dashboard.Postpaid.GetLocalTrafficDetailCallTM(objLocalTrafficDetailCallTMRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLocalTrafficDetailCallTMRequest.Audit.Session, objLocalTrafficDetailCallTMRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objLocalTrafficDetailCallTMResponse;
        }
        public Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse ConsumeLocalTrafficDetailCallTP(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPRequest objConsumeLocalTrafficDetailCalltRequest)
        {

            Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse objConsumeLocalTrafficDetailCallTPResponse = null;

            try
            {
                objConsumeLocalTrafficDetailCallTPResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse>(() => { return Business.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP(objConsumeLocalTrafficDetailCalltRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLocalTrafficDetailCalltRequest.Audit.Session, objConsumeLocalTrafficDetailCalltRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objConsumeLocalTrafficDetailCallTPResponse;
        }
        public Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse ConsumeLocalTrafficDetailCallTM(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMRequest objConsumeLocalTrafficDetailCallTMRequest)
        {

            Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse objConsumeLocalTrafficDetailCallTMResponse = null;

            try
            {
                objConsumeLocalTrafficDetailCallTMResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse>(() => { return Business.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM(objConsumeLocalTrafficDetailCallTMRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objConsumeLocalTrafficDetailCallTMResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse GetHistoryHR(Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRRequest request)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse objHistoryHRResponse = null;

            try
            {
                objHistoryHRResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse>(() => { return Business.Dashboard.Postpaid.GetHistoryHR(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objHistoryHRResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataLine(ServiceRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objDataServiceLineResponse;
            try
            {
                objDataServiceLineResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetDataLine(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objDataServiceLineResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceResponse ValidateTypeService(Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceRequest request)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceResponse objTypeServiceResponse;
            try
            {
                objTypeServiceResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceResponse>(() => { return Business.Dashboard.Postpaid.ValidateTypeService(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objTypeServiceResponse;
        }

        public Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse GetCustomerInformation(Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationRequest objCustomerInformationRequest)
        {
            Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse objCustomerInformationResponse;

            try
            {
                objCustomerInformationResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse>(() => { return Business.Dashboard.Postpaid.GetCustomerInformation(objCustomerInformationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerInformationRequest.Audit.Session, objCustomerInformationRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCustomerInformationResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse GetStockWarehouse(Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseRequest objStockWarehouseRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse objStockWarehouseResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse();
            try
            {
                objStockWarehouseResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse>(() => { return Business.Dashboard.Postpaid.GetStockWarehouse(objStockWarehouseRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStockWarehouseRequest.Audit.Session, objStockWarehouseRequest.Audit.Transaction, ex.Message);
            }
            return objStockWarehouseResponse;
        }

        public Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse GetTrackingDetail(Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailRequest objTrackingDetailRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse objTrackingDetailResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse();
            try
            {
                objTrackingDetailResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse>(() => { return Business.Dashboard.Postpaid.GetTrackingDetail(objTrackingDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTrackingDetailRequest.Audit.Session, objTrackingDetailRequest.Audit.Transaction, ex.Message);
            }
            return objTrackingDetailResponse;
        }

        public Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse GetManagementOfClosures(Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresRequest objManagementOfClosuresRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse objManagementOfClosuresResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse();
            try
            {
                objManagementOfClosuresResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse>(() => { return Business.Dashboard.Postpaid.GetManagementOfClosures(objManagementOfClosuresRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objManagementOfClosuresRequest.Audit.Session, objManagementOfClosuresRequest.Audit.Transaction, ex.Message);
            }
            return objManagementOfClosuresResponse;
        }

        public Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse GetReopenOfTheCase(Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseRequest objReopenOfTheCaseRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse objReopenOfTheCaseResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse();
            try
            {
                objReopenOfTheCaseResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse>(() => { return Business.Dashboard.Postpaid.GetReopenOfTheCase(objReopenOfTheCaseRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objReopenOfTheCaseRequest.Audit.Session, objReopenOfTheCaseRequest.Audit.Transaction, ex.Message);
            }
            return objReopenOfTheCaseResponse;
        }

        public   Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataServiceLineHFC(Entity.Dashboard.Postpaid.GetService.ServiceRequest objServiceRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse objServiceResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse();
            try
            {
                objServiceResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse>(() => { return Business.Dashboard.Postpaid.GetServiceLineHFC(objServiceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, ex.Message);
            }
            return objServiceResponse;

        }
        
        public Claro.SIACU.Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse GetMaterials(Claro.SIACU.Entity.Dashboard.Postpaid.GetMaterials.MaterialsRequest objMaterialsRequest)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse objMaterialsResponse = new Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse();
            try
            {
                objMaterialsResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse>(() => { return Business.Dashboard.Postpaid.GetMaterials(objMaterialsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMaterialsRequest.Audit.Session, objMaterialsRequest.Audit.Transaction, ex.Message);
            }

            return objMaterialsResponse;
        }

        public Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse GetSubTableTracking(Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingRequest objSubTableTrackingRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse objSubTableTrackingResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse();
            try
            {
                objSubTableTrackingResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse>(() => { return Business.Dashboard.Postpaid.GetSubTableTracking(objSubTableTrackingRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSubTableTrackingRequest.Audit.Session, objSubTableTrackingRequest.Audit.Transaction, ex.Message);
            }
            return objSubTableTrackingResponse;
        }

        public Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse GetThirdTableTracking(Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingRequest objThirdTableTrackingRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse objThirdTableTrackingResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse();
            try
            {
                objThirdTableTrackingResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse>(() => { return Business.Dashboard.Postpaid.GetThirdTableTracking(objThirdTableTrackingRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objThirdTableTrackingRequest.Audit.Session, objThirdTableTrackingRequest.Audit.Transaction, ex.Message);
            }
            return objThirdTableTrackingResponse;
        }

        public Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse GetBalanceCBIOS(Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSRequest objBalanceCBIOSRequest)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse objBalanceCBIOSResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse();
            try
            {
                objBalanceCBIOSResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse>(() => { return Business.Dashboard.Postpaid.GetBalanceCBIOS(objBalanceCBIOSRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBalanceCBIOSRequest.Audit.Session, objBalanceCBIOSRequest.Audit.Transaction, ex.Message);
            }
            return objBalanceCBIOSResponse;
        }

        public Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse GetDataBalanceShared(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objSharedBagRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse objSharedBagResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse();
            try
            {
                objSharedBagResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse>(() => { return Business.Dashboard.Postpaid.GetDataBalanceShared(objSharedBagRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, ex.Message);
            }
            return objSharedBagResponse;
        }

        public Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse GetHistoricalRecharge(Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeRequest objHistoricalRechargeRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse objHistoricalRechargeResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse();
            try
            {
                objHistoricalRechargeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse>(() => { return Business.Dashboard.Postpaid.GetHistoricalRecharge(objHistoricalRechargeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoricalRechargeRequest.Audit.Session, objHistoricalRechargeRequest.Audit.Transaction, ex.Message);
            }
            return objHistoricalRechargeResponse;
        }
        public Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid GetConsumptionHistoricalRecharge(Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeRequestPospaid objConsumptionHistoricalRechargeRequestPospaid)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid objConsumptionHistoricalRechargeResponsePospaid = new Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid();
            try
            {
                objConsumptionHistoricalRechargeResponsePospaid = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid>(() => { return Business.Dashboard.Postpaid.GetConsumptionHistoricalRecharge(objConsumptionHistoricalRechargeRequestPospaid); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumptionHistoricalRechargeRequestPospaid.Audit.Session, objConsumptionHistoricalRechargeRequestPospaid.Audit.Transaction, ex.Message);
            }
            return objConsumptionHistoricalRechargeResponsePospaid;

        }
        public Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse GetTypeConsumption(Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionRequest objGetTypeConsumptionRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse objGetTypeConsumptionResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse();
            try
            {
                objGetTypeConsumptionResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse>(() => { return Business.Dashboard.Postpaid.GetTypeConsumption(objGetTypeConsumptionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetTypeConsumptionRequest.Audit.Session, objGetTypeConsumptionRequest.Audit.Transaction, ex.Message);
            }
            return objGetTypeConsumptionResponse;
        }

        public Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetDataJanusPackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            try
            {
                objTotalMbPurchasedPackageResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse>(() => { return Business.Dashboard.Postpaid.GetDataJanusPackage(objTotalMbPurchasedPackageRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTotalMbPurchasedPackageRequest.Audit.Session, objTotalMbPurchasedPackageRequest.Audit.Transaction, ex.Message);
            }
            return objTotalMbPurchasedPackageResponse;
        }


        public Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetDataOnePackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, string plataforma)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            try
            {
                objTotalMbPurchasedPackageResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse>(() => { return Business.Dashboard.Postpaid.GetDataOnePackage(objTotalMbPurchasedPackageRequest, plataforma); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTotalMbPurchasedPackageRequest.Audit.Session, objTotalMbPurchasedPackageRequest.Audit.Transaction, ex.Message);
            }
            return objTotalMbPurchasedPackageResponse;
        }
        public Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetHistoricalPackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, string plataforma, string flagConvivencia)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            try
            {
                objTotalMbPurchasedPackageResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse>(() => { return Business.Dashboard.Postpaid.GetHistoricalPackage(objTotalMbPurchasedPackageRequest, plataforma, flagConvivencia); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTotalMbPurchasedPackageRequest.Audit.Session, objTotalMbPurchasedPackageRequest.Audit.Transaction, ex.Message);
            }
            return objTotalMbPurchasedPackageResponse;
        }

        public Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse GetTypePakage(Entity.Dashboard.Postpaid.GetTypePackage.TypePakageRequest objTypePakageRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse objTypePakageResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse();
            try
            {
                objTypePakageResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse>(() => { return Business.Dashboard.Postpaid.GetTypePakage(objTypePakageRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypePakageRequest.Audit.Session, objTypePakageRequest.Audit.Transaction, ex.Message);
            }
            return objTypePakageResponse;

        }

        public Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse GetMbBag(Entity.Dashboard.Postpaid.GetMbBag.MbBagRequest objMbBagRequest)
        {

            Claro.SIACU.Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse objMbBagResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse();
            try
            {
                objMbBagResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse>(() => { return Business.Dashboard.Postpaid.GetMbBag(objMbBagRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMbBagRequest.Audit.Session, objMbBagRequest.Audit.Transaction, ex.Message);
            }
            return objMbBagResponse;
        }
        public Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetTotalMbPurchasedPackageResponse(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            try
            {
                objTotalMbPurchasedPackageResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse>(() => { return Business.Dashboard.Postpaid.GetTotalMbPurchasedPackageResponse(objTotalMbPurchasedPackageRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTotalMbPurchasedPackageRequest.Audit.Session, objTotalMbPurchasedPackageRequest.Audit.Transaction, ex.Message);
            }
            return objTotalMbPurchasedPackageResponse;
        }

        public Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse GetTypeOrder(Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderRequest objTypeOrderRequest)
        {

            Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse objTypeOrderResponse = new Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse();
            try
            {
                objTypeOrderResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse>(() => { return Business.Dashboard.Postpaid.GetTypeOrder(objTypeOrderRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeOrderRequest.Audit.Session, objTypeOrderRequest.Audit.Transaction, ex.Message);
            }
            return objTypeOrderResponse;
        }

        public Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationResponse GetMotiveCancellation(Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationRequest objRequest)
        {

            var objResponse = new Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationResponse();
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod<Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationResponse>(
                            objRequest.Audit.Session, objRequest.Audit.Transaction,
                            () => { return Business.Dashboard.Postpaid.GetMotiveCancellation(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, MessageException.GetOriginalExceptionMessage(ex));
            }
            return objResponse;
        }

        

 public  Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIRequest objParameterTerminalTPIRequest)
        {

            Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse objParameterTerminalTPIResponse = new Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse();
            try
            {
                objParameterTerminalTPIResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse>(() => { return Business.Dashboard.Postpaid.GetParameterTerminalTPI(objParameterTerminalTPIRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objParameterTerminalTPIRequest.Audit.Session, objParameterTerminalTPIRequest.Audit.Transaction, ex.Message);
            }
            return objParameterTerminalTPIResponse;

          
        }
        #region Servicios VoLTE y VoWiFi
        // PROY-31249
        public Entity.Dashboard.StatusTechnologyVo GetStatusTechnologyVo(Claro.Entity.AuditRequest oAudit, string strSerie, string strTelefono)
        {
            Entity.Dashboard.StatusTechnologyVo oStatusTechnologyVo;
            try
            {
                oStatusTechnologyVo = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.StatusTechnologyVo>(oAudit.Session, oAudit.Transaction, () => { return Business.Dashboard.Postpaid.GetStatusTechnologyVo(oAudit, strSerie, strTelefono); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oAudit.Session, oAudit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return oStatusTechnologyVo;

        }
        #endregion

        public List<DataHistorical> getDataHistory(Claro.Entity.AuditRequest audit, string strIdSession, string strTransaction, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID, string plataforma, string flagconvivencia)
        {
            List<DataHistorical> listItem = null;

            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<DataHistorical>>(() =>
                {
                    return Business.Dashboard.Postpaid.getDataHistory(audit, strIdSession, strTransaction, strIpAplicacion, strAplicacion, strUsrApp, strCustomerID, plataforma, flagconvivencia);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);                
            }

            return listItem;

        }

        public Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse GetAnnotationWS(Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsRequest objAnnotationsRequest)
        {

            Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse objAnnotationsResponse = new Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse();
            try
            {
                objAnnotationsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse>(() => { return Business.Dashboard.Postpaid.GetAnnotationWS(objAnnotationsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAnnotationsRequest.Audit.Session, objAnnotationsRequest.Audit.Transaction, ex.Message);
            }
            return objAnnotationsResponse;


        }
 public Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse GetBSS_StatusAccount(Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse objBSS_StatusAccountResponse = null;

            try
            {
                objBSS_StatusAccountResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse>(
                    objBSS_StatusAccountRequest.Audit.Session,
                    objBSS_StatusAccountRequest.Audit.Transaction,
                    () => { return Business.Dashboard.Postpaid.GetBSS_StatusAccount(objBSS_StatusAccountRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBSS_StatusAccountRequest.Audit.Session, objBSS_StatusAccountRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objBSS_StatusAccountResponse;
        }

        public Entity.Dashboard.Postpaid.GetInteraction.InteractionResponse GetInteraction(Entity.Dashboard.Postpaid.GetInteraction.InteractionRequest objInteractionRequest)
        {
            Entity.Dashboard.Postpaid.GetInteraction.InteractionResponse objInteractionResponse = null;

            try
            {
                objInteractionResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetInteraction.InteractionResponse>(() => { return Business.Dashboard.Postpaid.GetInteraction(objInteractionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionRequest.Audit.Session, objInteractionRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objInteractionResponse;

        }
        public Entity.Dashboard.Postpaid.GetTypeDocumentDeubt.TypeDocumentDeubtResponse GetTypeDocumentDeubt(Entity.Dashboard.Postpaid.GetTypeDocumentDeubt.TypeDocumentDeubtRequest objTypeDocumentDeubtRequest)
        {
            Entity.Dashboard.Postpaid.GetTypeDocumentDeubt.TypeDocumentDeubtResponse objTypeDocumentDeubtResponse = null;

            try
            {
                objTypeDocumentDeubtResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetTypeDocumentDeubt.TypeDocumentDeubtResponse>(() =>
                { return Business.Dashboard.Postpaid.GetTypeDocumentDeubt(objTypeDocumentDeubtRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeDocumentDeubtRequest.Audit.Session, objTypeDocumentDeubtRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objTypeDocumentDeubtResponse;

        }

        public Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse GetBonusStatusFullClaro(Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetBonusStatusFullClaro(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }



        public CustomerResponse GetDataCustomer2(CustomerRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse objResponse = new Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse();

            objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Business.Dashboard.Postpaid.GetDataCustomer2(objRequest);
            });


            return objResponse;
        }

        public Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse GetBonusStatusFullClaroClient(Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetBonusStatusFullClaroClient(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public MiClaroAppResponse GetMiClaroApp(MiClaroAppRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetMiClaroApp(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
       
            return objResponse;
        }
        public List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo> obtenerListaBloDesblo(Claro.Entity.AuditRequest auditoria, string ViCcontrato, out string Error)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo> listItem = null;
            string MsgError = string.Empty;
            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo>>(() =>
                {
                    return Business.Dashboard.Postpaid.obtenerListaBloDesblo(auditoria, ViCcontrato, out MsgError);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("obtenerListaBloDesblo", auditoria.Transaction, ex.Message);
            }
            Error = MsgError;
            return listItem;
        }

        public Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse GetValidateCustomer
            (Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerRequest objRequest,
            Claro.Entity.AuditRequest auditRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetValidateCustomer(objRequest, auditRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(auditRequest.Transaction, auditRequest.Session, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objResponse;
        
        }

        public Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse GetValidateLine
            (Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineRequest objRequest,
            Claro.Entity.AuditRequest auditRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse>(() =>
                {
                    return Business.Dashboard.Postpaid.GetValidateLine(objRequest, auditRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(auditRequest.Transaction, auditRequest.Session, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objResponse;

        }
        public List<Parameter> ObtenerBloqueosClaro(string strIdSession, string strTransaction, string nombre, out string mensaje)
        {
            List<Parameter> listItem = null;
            string Msg = string.Empty;
            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<Parameter>>(() =>
                {
                    return Business.Dashboard.Postpaid.ObtenerBloqueosClaro(strIdSession, strTransaction, nombre, out Msg);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("ObtenerBloqueosClaro", strTransaction, ex.Message);
            }
            mensaje = Msg;
            return listItem;
        }
        
        public Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse GetProductosXCustomer(Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerRequest objRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse objResponse = new Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                { return Business.Dashboard.Postpaid.GetProductosXCustomer(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }        
    }
}
