using Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetService;
using Claro.SIACU.Entity.Dashboard.Postpaid;
using System.ServiceModel;
using System.Collections.Generic;

namespace Claro.SIACU.Web.Service.Dashboard
{

    [ServiceContract]
    public interface IPostpaidService
    {

        #region cmendez
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetServiceByCustomerCode(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetDataCustomer(CustomerRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress.OfficeAddressResponse GetAddressOfficce(OfficeAddressRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt.ReceiptResponse GetDataInvoice(ReceiptRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataServiceLine(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetTelephoneByContractCode(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetLineDisableByContractCode(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetInstallationAddress(CustomerRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetHistoryServiceLine(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays.ActiveDaysResponse GetActiveDisableDays(ActiveDaysRequest request);
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM.HistorySIMResponse GetHistorySIM(HistorySIMRequest request);
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoResponse GetHistoryEquipments(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse GetIMRConsult(Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRRequest request);


        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactResponse GetContact(Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse GetColumnsConfiguration(Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse ContactSave(Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveRequest request);



        #endregion

        [OperationContract]
        PaymentCollectionResponse GetPaymentCollection(PaymentCollectionRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeResponse GetDetailAmountDispute(Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeRequest objDetailAmountDisputeRequest);


        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse GetAffiliationToDebit(Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitRequest objAffiliationToDebitRequest);


        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse GetMonitoringCases(Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesRequest objMonitoringCasesRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse GetAnnotations(Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsRequest objAnnotationsRequest);

        #region LHV
        [OperationContract]
        Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse GetDetailAnnotation(Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationRequest objDetailAnnotationsRequest);

        [OperationContract]
        Entity.Common.GetAnnotationType.AnnotationTypeResponse GetAnnotationType(Entity.Common.GetAnnotationType.AnnotationTypeRequest objAnnotationTypeRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse GetWarranty(Entity.Dashboard.Postpaid.GetWarranty.WarrantyRequest objGetWarrantyRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse GetSharedBag(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objGetSharedBagRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse GetRelationPlans(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveRequest objMassiveRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse GetPinPuk(Entity.Dashboard.Postpaid.GetPinPuk.PinPukRequest objGetPinPukRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse GetSuspendedContract(Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractRequest objGetSuspendedContractRequest);

        [OperationContract]
        Entity.Common.GetSuspendedType.SuspendedTypeResponse GetSuspendedType(Entity.Common.GetSuspendedType.SuspendedTypeRequest objSuspendedTypeRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse GetSubAccount(Entity.Dashboard.Postpaid.GetSubAccount.SubAccountRequest objGetSubAccountRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse GetCreditLimit(Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitRequest objGetCreditLimitRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse GetRelationPlanHFCLTE(Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTERequest objGetRelationPlanHFCLTERequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse GetCreditLimitDetail(Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailRequest objCreditLimitDetailRequest);



        #endregion

        #region JHG
        [OperationContract]
        Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse GetDetailLongDistance(Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceRequest objGetGetDetailLongDistanceRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse GetInternationalRoamingDetail(Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailRequest objGetInternationalRoamingDetailRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse GetDebtDetail(Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailRequest objGetDebtDetailRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse GetAdditionalLocalTrafficDetail(Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailRequest objGetAdditionalLocalTrafficDetailRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse GetConsumeLocalTrafficDetail(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailRequest objGetConsumeLocalTrafficDetailRequest);
        #endregion

        #region JPR
        [OperationContract]
        Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse GetDetailsOtherConcepts(Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsRequest objGetDetailsOtherConceptsRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse GetTimServiceDetails(Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsRequest objGetTimServiceDetailsRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse GetHistoryInvoice(Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceRequest objGetTimServiceDetailsRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse SMSDetails(Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsRequest objGetSMSDetailsRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse GetAmountSMSDetails(Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsRequest objAmountSMSDetailsRequest);
        #endregion

        #region DCA
        [OperationContract]
        Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetPhoneContract(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetContractServices(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetServiceBSCS(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest);
        [OperationContract]
        Entity.Common.GetPetitionsType.PetitionsTypeResponse GetPetitionsType(Entity.Common.GetPetitionsType.PetitionsTypeRequest objPetitionsTypeRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse GetPetitions(Entity.Dashboard.Postpaid.GetPetitions.PetitionRequest objPetitionRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse GetTriaciones(Entity.Dashboard.Postpaid.GetTriaciones.StriationsRequest objTriacionRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse GetHistoricalStriations(Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsRequest objHistoricalStriationsRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetRecharges(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetBalance.BalanceResponse GetBalance(Entity.Dashboard.Postpaid.GetBalance.BalanceRequest objBalanceRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse GetComputerQuery(Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryRequest objComputerQueryRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse GetScheduledTransaction(Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionRequest objScheduledTransactionRequest);
        #endregion

        [OperationContract]
        Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse DueNumberDocumentOAC(Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberRequest objGetDueNumberDocumentOACRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse StatusAccount(Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountRequest objGetStatusAccountRequest);

        #region WS
        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataLineHistory(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR.HLRResponse GetHLR(HLRRequest request);
        #endregion

        #region [Acuerdos]

        [OperationContract]
        Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse GetExistsAgreementOld(Entity.Dashboard.Postpaid.GetAgreement.AgreementRequest Request);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse GetExistAgreementNew(Entity.Dashboard.Postpaid.GetAgreement.AgreementRequest Request);
        #endregion

        [OperationContract]
        Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse StatusAccountLDI(Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIRequest objGetStatusAccountLDIRequest);

        #region [Prestamo/alquiler]
        [OperationContract]
        Entity.Common.GetLoanRentalType.LoanRentalResponse GetLoanRentalType(Entity.Common.GetLoanRentalType.LoanRentalTypeRequest objLoanRentalTypeRequest);

        [OperationContract]
        Entity.Common.GetLoanRentalType.LoanRentalResponse GetLoanRentalListWarehouseArea(Entity.Common.GetLoanRentalType.LoanRentalTypeRequest objLoanRentalTypeRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse LoanRental(Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResquest ObjLoanRentalRequest);
        #endregion

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse GetHistoryActions(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsRequest objHistoryActionsRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse GetFixedChargeDetailTimPro(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProRequest objGetFixedChargeDetailTimProRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse GetFixedChargeDetailTimProOne(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneRequest objGetFixedChargeDetailTimProOneRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse GetFixedChargeDetailTimProTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoRequest objGetFixedChargeDetailTimProTwoRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse GetFixedChargeDetailTimMax(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxRequest objGetFixedChargeDetailTimMaxRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse GetFixedChargeDetailTimMaxTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoRequest objGetFixedChargeDetailTimMaxTwoRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse GetDiscountFixedChargeDetail(Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailRequest objGetDiscountFixedChargeDetailRequest);


        [OperationContract]
        Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse GetPaymentCommitment(Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentRequest objPaymentCommitmentRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse GetFixedChargeDetailTimProBag(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagRequest objGetFixedChargeDetailTimProBagRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse GetFixedChargeDetailTimProBagOne(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneRequest objGetFixedChargeDetailTimProBagOneRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse GetFixedChargeDetailTimProBagTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoRequest objGetFixedChargeDetailTimProBagTwoRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse GetFixedChargeDetailTimProBagThree(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeRequest objGetFixedChargeDetailTimProBagThreeRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery.HistoricDeliveryResponse GetHistoricDelivery(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricDelivery.HistoricDeliveryRequest objHistoricDeliveryRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse GetFixedChargeTimProBagDetail(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailRequest objGetFixedChargeTimProBagDetailRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse GetFixedChargeTimProBagDetailOne(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneRequest objGetFixedChargeTimProBagDetailOneRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse GetFixedChargeTimProBagDetailTwo(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoRequest objGetFixedChargeTimProBagDetailTwoRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse GetFixedChargeTimProBagDetailThree(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeRequest objGetFixedChargeTimProBagDetailThreeRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse FeeEquipment(Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentRequest objFeeEquipmentRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse LocalTrafficDetailCallTP(Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPRequest objLocalTrafficDetailCalltRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse LocalTrafficDetailCallTM(Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMRequest objLocalTrafficDetailCallTMRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse ConsumeLocalTrafficDetailCallTP(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPRequest objLocalTrafficDetailCalltRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse ConsumeLocalTrafficDetailCallTM(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMRequest objLocalTrafficDetailCallTMRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse GetHistoryHR(Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataLine(ServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceResponse ValidateTypeService(Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceRequest request);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse GetCustomerInformation(Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationRequest objCustomerInformationRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse GetStockWarehouse(Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseRequest objStockWarehouseRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse GetTrackingDetail(Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailRequest objTrackingDetailRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse GetManagementOfClosures(Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresRequest objManagementOfClosuresRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse GetReopenOfTheCase(Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseRequest objReopenOfTheCaseRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataServiceLineHFC(Entity.Dashboard.Postpaid.GetService.ServiceRequest objServiceRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse GetMaterials(Claro.SIACU.Entity.Dashboard.Postpaid.GetMaterials.MaterialsRequest objMaterialsRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse GetSubTableTracking(Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingRequest objSubTableTrackingRequest);

        [OperationContract]
        Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse GetThirdTableTracking(Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingRequest objThirdTableTrackingRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse GetBalanceCBIOS(Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSRequest objBalanceCBIOSRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse GetDataBalanceShared(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objSharedBagRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse GetHistoricalRecharge(Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeRequest objHistoricalRechargeRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid GetConsumptionHistoricalRecharge(Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeRequestPospaid objConsumptionHistoricalRechargeRequestPospaid);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse GetTypeConsumption(Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionRequest objGetTypeConsumptionRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetDataJanusPackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetDataOnePackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, string plataforma);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetHistoricalPackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, string plataforma,string flagConvivencia);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse GetTypePakage(Entity.Dashboard.Postpaid.GetTypePackage.TypePakageRequest objTypePakageRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse GetMbBag(Entity.Dashboard.Postpaid.GetMbBag.MbBagRequest objMbBagRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetTotalMbPurchasedPackageResponse(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse GetTypeOrder(Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderRequest objTypeOrderRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationResponse GetMotiveCancellation(Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationRequest objRequest);
        [OperationContract]
        Entity.Dashboard.StatusTechnologyVo GetStatusTechnologyVo(Claro.Entity.AuditRequest oAudit, string strSerie, string strTelefono);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIRequest objParameterTerminalTPIRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse GetSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeRequests objSolicitudeRequests);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse RegisterSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeRequests objRegisterSolicitudeRequests);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse GetDocumentSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeRequests objDocumentSolicitudeRequests);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse GetAnnotationWS(Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsRequest objAnnotationsRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse GetBSS_StatusAccount(Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountRequest objBSS_StatusAccountRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetInteraction.InteractionResponse GetInteraction(Entity.Dashboard.Postpaid.GetInteraction.InteractionRequest objInteractionRequest);
        [OperationContract]
        List<DataHistorical> getDataHistory(Claro.Entity.AuditRequest audit, string strIdSession, string strTransaction, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID, string plataforma,string flagconvivencia);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetTypeDocumentDeubt.TypeDocumentDeubtResponse GetTypeDocumentDeubt(Entity.Dashboard.Postpaid.GetTypeDocumentDeubt.TypeDocumentDeubtRequest objTypeDocumentDeubtRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse GetBonusStatusFullClaro(Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroRequest objRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetDataCustomer2(Entity.Dashboard.Postpaid.GetCustomer.CustomerRequest objRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse GetBonusStatusFullClaroClient(Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientRequest objRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse GetMiClaroApp(Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppRequest objRequest);
        [OperationContract]
        List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo> obtenerListaBloDesblo(Claro.Entity.AuditRequest auditoria, string ViCcontrato, out string Error);
        [OperationContract]
        Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse GetValidateCustomer
            (Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerRequest objRequest,
            Claro.Entity.AuditRequest auditRequest);
        [OperationContract]
        Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse GetValidateLine
            (Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineRequest objRequest,
            Claro.Entity.AuditRequest auditRequest);

        [OperationContract]
        List<Parameter> ObtenerBloqueosClaro(string strIdSession, string strTransaction, string nombre, out string mensaje);

        //[INICIATIVA-616 | ONE FIJA - Postventa. Integración SIAC con CBIOS] KGC
        //[INICIATIVA-616 | ONE FIJA - Postventa. Integración SIAC con CBIOS] KGC
        [OperationContract]
        Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse GetProductosXCustomer(Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerRequest objRequest);
    }
}
