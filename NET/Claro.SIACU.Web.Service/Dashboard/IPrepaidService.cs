using System.ServiceModel;

namespace Claro.SIACU.Web.Service.Dashboard
{

    [ServiceContract]
    public interface IPrepaidService
    {
        [OperationContract]
        Entity.Dashboard.Prepaid.GetCalls.CallsResponse GetCalls(Entity.Dashboard.Prepaid.GetCalls.CallsRequest objCallsRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse GetRecharges(Entity.Dashboard.Prepaid.GetRecharges.RechargesRequest objRechargesRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse GetPreviousCustomer(Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objGetPreviousCustomerRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse GetPreviousCustomers(Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersRequest objPreviousCustomersRequest);


        [OperationContract]
        Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse GetValidateTelephone(Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneRequest objValidateTelephoneRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetService.ServiceResponse GetService(Entity.Dashboard.Prepaid.GetService.ServiceRequest objGetServiceRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse GetRecordTriationRFA(Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFARequest objRecordTriationRFARequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse GetDetailTriationRFA(Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFARequest objDetailTriationRFARequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse GetHistoricalBonds(Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsRequest objHistoricalBondsRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse GetPinPuk(Entity.Dashboard.Prepaid.GetPinPuk.PinPukRequest objPinPukRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantResponse GetInstant(Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantRequest objInstantRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELResponse GetPEL(Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELRequest objPELRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse GetSalesDataPEL(Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataRequest objSalesDataRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtResponse GetDebt(Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtRequest objDebtRequest);

        [OperationContract]
        Claro.SIACU.Entity.Dashboard.Prepaid.CreateContactNotUSer.CreateContactNotUSerResponse CreateContactNotUSer(Claro.SIACU.Entity.Dashboard.Prepaid.CreateContactNotUSer.CreateContactNotUSerRequest objCreateContactNotUSerRequest);

        //[OperationContract]
        //Claro.SIACU.Entity.Dashboard.Prepaid.GetCallDetailPrint.CallDetailPrintResponse GetCallDetailPrint(Entity.Dashboard.Prepaid.GetCallDetailPrint.CallDetailPrintRequest objCallDetailPrintRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse GetConsumptionDataPacket(Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketRequest objConsumptionDataPacketRequest);

        [OperationContract]
        Entity.Dashboard.Prepaid.GetRateState.RateStateResponse GetRateState(Entity.Dashboard.Prepaid.GetRateState.RateStateRequest objRequest);
    
    }
}
