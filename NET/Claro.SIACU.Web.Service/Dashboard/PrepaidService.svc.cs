using System;
using System.ServiceModel;
using HELPER_CREATEUSER = Claro.SIACU.Entity.Dashboard.Prepaid.CreateContactNotUSer;

namespace Claro.SIACU.Web.Service.Dashboard
{

    public class PrepaidService : IPrepaidService
    {


        /// <summary>
        /// Listado de detalle de llamadas
        /// </summary>
        /// <param name="strMsisdn">Numero de telefono</param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetCalls.CallsResponse GetCalls(Entity.Dashboard.Prepaid.GetCalls.CallsRequest objCallsRequest)
        {
            Entity.Dashboard.Prepaid.GetCalls.CallsResponse objCallsResponse = null;

            try
            {
                objCallsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetCalls.CallsResponse>(objCallsRequest.Audit.Session,objCallsRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetCalls(objCallsRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCallsRequest.Audit.Session, objCallsRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }

            return objCallsResponse;
        }

        /// <summary>
        /// Listado de detalle de recargas
        /// </summary>
        /// <param name="strMsisdn"></param>
        /// <param name="strStartDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="strFlag"></param>
        /// <param name="intNumberRecords"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse GetRecharges(Entity.Dashboard.Prepaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse objRechargesResponse = null;
            try
            {
                objRechargesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse>(objRechargesRequest.Audit.Session,objRechargesRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetRecharges(objRechargesRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);

            }
            return objRechargesResponse;
        }

      
        /// <summary>
        /// Obtiene los datos del cliente en una Entidad
        /// </summary>
        /// <param name="strTelephone">Telefono del Cliente</param>
        /// <param name="strAccount">Numero de Cuenta</param>
        /// <param name="strContactId">ID del cliente</param>
        /// <param name="strFlagRegistry">Flag de Registro</param>
        /// <returns>Entity Customer</returns>
        public Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse GetPreviousCustomer(Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objGetPreviousCustomerRequest)
        {
            Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objPreviousCustomerResponse = null;
            try
            {
                objPreviousCustomerResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse>(objGetPreviousCustomerRequest.Audit.Session,objGetPreviousCustomerRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetPreviousCustomer(objGetPreviousCustomerRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetPreviousCustomerRequest.Audit.Session, objGetPreviousCustomerRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objPreviousCustomerResponse;
        }

        /// <summary>
        /// Obtiene una lista de clientes
        /// </summary>
        /// <param name="strTelephone">Telefono de Cliente</param>
        /// <param name="strAccount">Numero de Cuenta</param>
        /// <param name="strContactId">ID de Contacto</param>
        /// <param name="strFlagRegistry">Flag de Registro</param>
        /// <param name="strFlagGetAll">Flag para traer la data ordenada</param>
        /// <param name="strFlagGetAllData">Flag para traer todos los registros del cliente</param>
        /// <returns>List List<Entity.Dashboard.Prepaid.Customer></returns>
        public Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse GetPreviousCustomers(Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersRequest objPreviousCustomersRequest)
        {          
            Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse objPreviousCustomersResponse = null;
            try
            {
                objPreviousCustomersResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse>(objPreviousCustomersRequest.Audit.Session,objPreviousCustomersRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetPreviousCustomers(objPreviousCustomersRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPreviousCustomersRequest.Audit.Session, objPreviousCustomersRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objPreviousCustomersResponse;
        }

        /// <summary>
        /// Se obtiene los mensajes de validacion para los clientes prepago
        /// </summary>
        /// <param name="strTelephone">Numero de Telefono</param>
        /// <param name="isTFI">Indica si el numero es TFI</param>
        /// <param name="strProviderID"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse GetValidateTelephone(Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneRequest objValidateTelephoneRequest)
        {
            Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse objValidateTelephoneResponse = null;
            try
            {
                objValidateTelephoneResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse>(objValidateTelephoneRequest.Audit.Session,objValidateTelephoneRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetValidateTelephone(objValidateTelephoneRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objValidateTelephoneRequest.Audit.Session, objValidateTelephoneRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objValidateTelephoneResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oService"></param>
        /// <param name="strTransactionID"></param>
        /// <param name="strApplicationID"></param>
        /// <param name="strApplicationName"></param>
        /// <param name="strApplicationUser"></param>
        /// <param name="strMatter"></param>
        /// <param name="strIssueSeries"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetService.ServiceResponse GetService(Entity.Dashboard.Prepaid.GetService.ServiceRequest objServiceRequest)
        {
            Entity.Dashboard.Prepaid.GetService.ServiceResponse objServiceResponse = null;
            try
            {
                objServiceResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetService.ServiceResponse>(objServiceRequest.Audit.Session,objServiceRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetService(objServiceRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objServiceResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRecordTriationRFARequest"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse GetRecordTriationRFA(Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFARequest objRecordTriationRFARequest)
        {
            Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse objRecordTriationRFAResponse = null;
            try
            {
                objRecordTriationRFAResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse>(objRecordTriationRFARequest.Audit.Session,objRecordTriationRFARequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetRecordTriationRFA(objRecordTriationRFARequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRecordTriationRFARequest.Audit.Session, objRecordTriationRFARequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objRecordTriationRFAResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDetailTriationRFARequest"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse GetDetailTriationRFA(Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFARequest objDetailTriationRFARequest)
        {
            Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse objDetailTriationRFAResponse = null;
            try
            {
                objDetailTriationRFAResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse>(objDetailTriationRFARequest.Audit.Session,objDetailTriationRFARequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetDetailTriationRFA(objDetailTriationRFARequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailTriationRFARequest.Audit.Session, objDetailTriationRFARequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objDetailTriationRFAResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objHistoricalBondsRequest"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse GetHistoricalBonds(Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsRequest objHistoricalBondsRequest)
        {
            Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse objHistoricalBondsResponse = null;
            try
            {
                objHistoricalBondsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse>(objHistoricalBondsRequest.Audit.Session,objHistoricalBondsRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetHistoricalBonds(objHistoricalBondsRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoricalBondsRequest.Audit.Session, objHistoricalBondsRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objHistoricalBondsResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objPinPukRequest"></param>
        /// <returns></returns>
        public Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse GetPinPuk(Entity.Dashboard.Prepaid.GetPinPuk.PinPukRequest objPinPukRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse objPinPukResponse = null;
            try
            {
                objPinPukResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse>(objPinPukRequest.Audit.Session,objPinPukRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetPinPuk(objPinPukRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objPinPukResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInstantRequest"></param>
        /// <returns></returns>
        public Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantResponse GetInstant(Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantRequest objInstantRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantResponse objInstantResponse = null;
            try
            {
                objInstantResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetInstant.InstantResponse>(objInstantRequest.Audit.Session,objInstantRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetInstant(objInstantRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInstantRequest.Audit.Session, objInstantRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objInstantResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInstantRequest"></param>
        /// <returns></returns>
        public Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELResponse GetPEL(Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELRequest objPELRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELResponse objPELResponse = null;
            try
            {
                objPELResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetPEL.PELResponse>(objPELRequest.Audit.Session,objPELRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetPEL(objPELRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPELRequest.Audit.Session, objPELRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objPELResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse GetSalesDataPEL(Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataRequest objSalesDataRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse objSalesDataResponse = null;
            try
            {
                objSalesDataResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse>(objSalesDataRequest.Audit.Session,objSalesDataRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetSalesDataPEL(objSalesDataRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSalesDataRequest.Audit.Session, objSalesDataRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objSalesDataResponse;
        }

        public Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtResponse GetDebt(Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtRequest objDebtRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtResponse objDebtResponse = null;
            try
            {
                objDebtResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetDebt.DebtResponse>(objDebtRequest.Audit.Session,objDebtRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.GetDebt(objDebtRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDebtRequest.Audit.Session, objDebtRequest.Audit.Transaction,ex.Message);
                throw new FaultException(objDebtRequest.Audit.Transaction);
            }
            return objDebtResponse;
        }


        public HELPER_CREATEUSER.CreateContactNotUSerResponse CreateContactNotUSer(HELPER_CREATEUSER.CreateContactNotUSerRequest objCreateContactNotUSerRequest)
        {
            HELPER_CREATEUSER.CreateContactNotUSerResponse objCreateContactNotUSerResponse = null;
            try
            {
                objCreateContactNotUSerResponse = Claro.Web.Logging.ExecuteMethod<HELPER_CREATEUSER.CreateContactNotUSerResponse>(objCreateContactNotUSerRequest.Audit.Session,objCreateContactNotUSerRequest.Audit.Transaction,() => 
                { 
                    return Business.Dashboard.Prepaid.CreateContactNotUSer(objCreateContactNotUSerRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCreateContactNotUSerRequest.Audit.Session, objCreateContactNotUSerRequest.Audit.Transaction,ex.Message);
                throw new FaultException(ex.Message);
            }
            return objCreateContactNotUSerResponse;
        }
       
        //public Entity.Dashboard.Prepaid.GetCallDetailPrint.CallDetailPrintResponse GetCallDetailPrint(Entity.Dashboard.Prepaid.GetCallDetailPrint.CallDetailPrintRequest objCallDetailPrintRequest)
        //{
        //    Entity.Dashboard.Prepaid.GetCallDetailPrint.CallDetailPrintResponse objCallDetailPrintResponse = null;
        //    try
        //    {
        //        objCallDetailPrintResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetCallDetailPrint.CallDetailPrintResponse>(objCallDetailPrintRequest.Audit.Session,objCallDetailPrintRequest.Audit.Transaction,() => 
        //        { 
        //            return Business.Dashboard.Prepaid.GetCallDetailPrint(objCallDetailPrintRequest); 
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objCallDetailPrintRequest.Audit.Session, objCallDetailPrintRequest.Audit.Transaction,ex.Message);
        //        throw new FaultException(ex.Message);
        //    }
        //    return objCallDetailPrintResponse;
        //}

        public Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse GetConsumptionDataPacket(Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketRequest objConsumptionDataPacketRequest)
        {
            Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse objConsumptionDataPacketResponse = null;
            try
            {
                objConsumptionDataPacketResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse>(objConsumptionDataPacketRequest.Audit.Session, objConsumptionDataPacketRequest.Audit.Transaction, () =>
                {
                    return Business.Dashboard.Prepaid.GetConsumptionDataPacket(objConsumptionDataPacketRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumptionDataPacketRequest.Audit.Session, objConsumptionDataPacketRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objConsumptionDataPacketResponse;
        }

        public Entity.Dashboard.Prepaid.GetRateState.RateStateResponse GetRateState(Entity.Dashboard.Prepaid.GetRateState.RateStateRequest objRequest)
        {
            Entity.Dashboard.Prepaid.GetRateState.RateStateResponse objRateStateResponse = null;
            try
            {
                objRateStateResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.GetRateState.RateStateResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.Dashboard.Prepaid.GetRateState(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return objRateStateResponse;
        }

    }
}
