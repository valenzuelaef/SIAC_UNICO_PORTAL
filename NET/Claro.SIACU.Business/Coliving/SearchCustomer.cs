using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Coliving.getSearchCustomer;
using Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions;
using Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving;
using Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta;

using ENTITIES_OST = Claro.SIACU.Entity.Coliving.getListOST;

using KEY = Claro.ConfigurationManager;
using CONSULTALINEACUENTA = Claro.SIACU.ProxyService.SIAC.ConsultaLineaCuenta;
using Claro.SIACU.Entity.Coliving.getCustomerInfo;
using Claro.SIACU.Entity.Coliving.Common;
using System.ServiceModel;
using Claro.SIACU.Entity.Coliving.getListOST;

namespace Claro.SIACU.Business.Coliving
{
    public class SearchCustomer
    {
        public static string GetAccountTelephoneCustomer(string Session, string Transaction, string strValue, string strTypeSearch)
        {
            string strNumeroCuenta = string.Empty;
            string strOutTelefono = string.Empty;
            string strplataforma = string.Empty;
            try
            {
                if (strTypeSearch.Equals("9")) //ICCID
                {
                    strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(Session, Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetAccountTelephoneCustomer(
                                 Session,
                                 Transaction,
                                "", "", strValue, out strOutTelefono, out strplataforma);
                    });
                }
                else if (strTypeSearch.Equals("8")) //Recibo
                {
                    strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(Session, Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetAccountTelephoneCustomer(
                       Session,
                       Transaction,
                       strValue, out strOutTelefono, out strplataforma);

                    });
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Session, Transaction, ex.Message);
                throw;
            }
            return strOutTelefono;

        }


        #region ConsultarLineaCuenta
        /// <summary>
        /// Método para obtener el estado de la linea/cuenta si ha migrado o no.
        /// </summary>
        /// <param name="objConsultaLineaRequest">Contiene:Type y Value</param>
        /// <returns>Devuelve el objConsultaLineaResponse que contiene la descripción si ha migrado.</returns>
        public static ConsultaLineaResponse ConsultarLineaCuenta(ConsultaLineaRequest objConsultaLineaRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objConsultaLineaRequest.Audit.Session,
                "Transaccion: " + objConsultaLineaRequest.Audit.Transaction,
                string.Format("Capa Business-Metodo: {0}, Type:{1}, Value{2} ", "ConsultarLineaCuenta", objConsultaLineaRequest.Type, objConsultaLineaRequest.Value));

            ConsultaLineaResponse objConsultaLineaResponse = null;
            try
            {
                var migrateOne = KEY.AppSettings("strkeyMigrateOne").Split('|');
                objConsultaLineaResponse = Claro.Web.Logging.ExecuteMethod<ConsultaLineaResponse>(objConsultaLineaRequest.Audit.Session, objConsultaLineaRequest.Audit.Transaction,
               () =>
               {
                   return Data.Coliving.SearchCustomer.ConsultarLineaCuenta(objConsultaLineaRequest);
               });
                if (objConsultaLineaResponse != null)
                {
                    var result = migrateOne.Where(x => x.Split(';')[0] == objConsultaLineaResponse.ResponseValue).FirstOrDefault().Split(';')[1];
                    objConsultaLineaResponse.ResponseDescription = result;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsultaLineaRequest.Audit.Session, objConsultaLineaRequest.Audit.Transaction, ex.Message);
            }
            return objConsultaLineaResponse;
        }
        #endregion

        #region GetSearchCustomer

        /// <summary>
        /// Método que consulta los datos de una linea/cuenta que ha migrado
        /// </summary>
        /// <param name="objCustomerInfoRequest">objCustomerInfoRequest</param>
        /// <returns>Devuelve el objeto searchCustomerResponse que contiene:DocumentNumber,DocumentType,CustomerName,Address,AccountNumber </returns>
        public static SearchCustomerResponse GetSearchCustomer(SearchCustomerRequest objCustomerInfoRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objCustomerInfoRequest.Audit.Session,
              "Transaccion: " + objCustomerInfoRequest.Audit.Transaction,
              string.Format("Capa Data-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3} ,InvoiceNumber{4}",
                              "GetSearchCustomerDP", objCustomerInfoRequest.CustomerId, objCustomerInfoRequest.DocumentType, objCustomerInfoRequest.DocumentNumber, objCustomerInfoRequest.InvoiceNumber));


            SearchCustomerResponse searchCustomerResponse = null;
            try
            {
                var documentType = KEY.AppSettings("strkeyDocumentSearch").Split('|');
                var customerStatus = KEY.AppSettings("strCustomerStatus").Split('|');
                var prefix = KEY.AppSettings("strConstPrefijo");
                if (objCustomerInfoRequest != null && !string.IsNullOrEmpty(objCustomerInfoRequest.Msisdn))
                {
                    var NumberLine = objCustomerInfoRequest.Msisdn;
                    var validatePrefix = NumberLine.Substring(0, 2);
                    if (validatePrefix != prefix)
                    {
                        objCustomerInfoRequest.Msisdn = prefix + NumberLine;
                    }
                }
                searchCustomerResponse = Claro.Web.Logging.ExecuteMethod<SearchCustomerResponse>(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Coliving.SearchCustomer.GetSearchCustomerDP(objCustomerInfoRequest);
                    });

                if (searchCustomerResponse != null)
                {
                    searchCustomerResponse.DescriptionDocumentType = documentType.Where(x => x.Split(';')[1] == searchCustomerResponse.DocumentType).FirstOrDefault().Split(';')[0];
                    searchCustomerResponse.CustomerStatus = customerStatus.Where(x => x.Split(';')[0] == searchCustomerResponse.CustomerStatus).FirstOrDefault().Split(';')[1];
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction, "Capa: BUSINESS metodo:GetSearchCustomer" + ex.Message);
                searchCustomerResponse = null;
            }
            return searchCustomerResponse;
        }
        #endregion

        #region GetRetrieveSubscriptions
        /// <summary>
        /// Método que devuelve las susbcripcion del cliente de una linea/cuenta migrada.
        /// </summary>
        /// <param name="retrieveSubscriptions">retrieveSubscriptions</param>
        /// <returns>Devuele el objeto retrieveSubscriptionResponse: PoName,ServiceIdentifier,ProductType,SubscriptionStatus</returns> 
        public static RetrieveSubscriptionResponse GetRetrieveSubscriptions(RetrieveSubscriptionRequest retrieveSubscriptions)
        {
            Claro.Web.Logging.Info("IdSession: " + retrieveSubscriptions.Audit.Session,
            "Transaccion: " + retrieveSubscriptions.Audit.Transaction,
            string.Format("Capa Business-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3} ,serviceIdentifier{4}",
                            "GetRetrieveSubscriptionsDP", retrieveSubscriptions.customerId, retrieveSubscriptions.DocumentType, retrieveSubscriptions.DocumentNumber, retrieveSubscriptions.serviceIdentifier));

            RetrieveSubscriptionResponse retrieveSubscriptionResponse = null;
            try
            {
                var prefix = KEY.AppSettings("strConstPrefijo");
                if (retrieveSubscriptions != null && !string.IsNullOrEmpty(retrieveSubscriptions.serviceIdentifier))
                {
                    var NumberLine = retrieveSubscriptions.serviceIdentifier;
                    var validatePrefix = NumberLine.Substring(0, 2);
                    if (validatePrefix != prefix)
                    {
                        retrieveSubscriptions.serviceIdentifier = prefix + NumberLine;
                    }
                }
                //Verifica si es PostaPago/PrePago(MODALIDAD)
                var productType = KEY.AppSettings("strkeyProductType").Split('|');
                //Verfica el estado de la linea 
                var subscriptionStatus = KEY.AppSettings("strsubscriptionStatus").Split('|');

                retrieveSubscriptionResponse = new RetrieveSubscriptionResponse();
                retrieveSubscriptionResponse = Claro.Web.Logging.ExecuteMethod<RetrieveSubscriptionResponse>(retrieveSubscriptions.Audit.Session, retrieveSubscriptions.Audit.Transaction,
                     () =>
                     {
                         return Data.Coliving.SearchCustomer.GetRetrieveSubscriptionsDP(retrieveSubscriptions);
                     });
                if (retrieveSubscriptionResponse != null)
                {
                    foreach (var item in retrieveSubscriptionResponse.Subscriptions)
                    {
                        item.ProductType = productType.Where(x => x.Split(';')[0] == item.ProductType).FirstOrDefault().Split(';')[1];
                        item.SubscriptionStatus = subscriptionStatus.Where(x => x.Split(';')[0] == item.SubscriptionStatus).FirstOrDefault().Split(';')[1];
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(retrieveSubscriptions.Audit.Session, retrieveSubscriptions.Audit.Transaction, "Capa: BUSINESS metodo:GetRetrieveSubscriptions" + ex.Message);
                retrieveSubscriptionResponse = null;
            }
            return retrieveSubscriptionResponse;
        }
        #endregion

        #region GetObtenerDatosCliente
        ///<summary>
        /// Método que consulta los datos una linea/cuenta no migrada.
        /// </summary>
        /// <param name="objCustomerInfoRequest">Contiene: CustomerId|DocumentType,DocumentNumber|LineNumber</param>
        /// <returns>Devuele el objeto objObtenerDatosResponse.</returns> 
        public static ObtenerDatosClienteResponse GetObtenerDatosCliente(ObtenerDatosClienteRequest objObtenerDatosRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objObtenerDatosRequest.Audit.Session,
                "Transaccion: " + objObtenerDatosRequest.Audit.Transaction,
                string.Format("Capa Business-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3} ,LineNumber{4}",
                                "ConsultarLineaCuenta", objObtenerDatosRequest.CustomerId, objObtenerDatosRequest.DocumentType, objObtenerDatosRequest.DocumentNumber, objObtenerDatosRequest.LineNumber));

            ObtenerDatosClienteResponse objObtenerDatosResponse = null;
            try
            {
                var PrepagoOloStatus = KEY.AppSettings("strLinePrepagoOloStatus").Split('|'); 
                var subscriptionStatus = KEY.AppSettings("strsubscriptionStatus").Split('|');
                var documentType = KEY.AppSettings("strkeyDocumentSearch").Split('|');
                var strkeyOrigenInfo = KEY.AppSettings("strkeyOrigenInfo").Split('|').ToList();
                var strPrepagoOlo = KEY.AppSettings("strPrepagoOlo").ToString();

                objObtenerDatosResponse = Claro.Web.Logging.ExecuteMethod<ObtenerDatosClienteResponse>(objObtenerDatosRequest.Audit.Session, objObtenerDatosRequest.Audit.Transaction, () =>
                {
                    return Data.Coliving.SearchCustomer.GetObtenerDatosClienteDP(objObtenerDatosRequest);
                });
                if (objObtenerDatosResponse != null && ((objObtenerDatosResponse.SubscriptionsPostPaid != null && objObtenerDatosResponse.SubscriptionsPostPaid.Count > 0) || (objObtenerDatosResponse.SubscriptionsPrepaid != null && objObtenerDatosResponse.SubscriptionsPrepaid.Count > 0)))
                {
                    var strPrepago = Constants.PrepaidMajusculeSpanish;
                    var strPostPago = Constants.PostpaidMajusculeSpanish;


                    if (!string.IsNullOrEmpty(objObtenerDatosResponse.DocumentType))
                    {
                        //objObtenerDatosResponse.DocumentType = documentType.Where(X => X.Split(';')[0] == objObtenerDatosResponse.DescriptionDocumentType).FirstOrDefault().Split(';')[1];
                        objObtenerDatosResponse.DescriptionDocumentType = documentType.Where(X => X.Split(';')[2] == objObtenerDatosResponse.DocumentType).FirstOrDefault().Split(';')[0];
                        objObtenerDatosResponse.DocumentType = documentType.Where(X => X.Split(';')[2] == objObtenerDatosResponse.DocumentType).FirstOrDefault().Split(';')[1];
                    }
                    if (objObtenerDatosResponse.SubscriptionsPostPaid != null && objObtenerDatosResponse.SubscriptionsPostPaid.Count > 0)
                    {
                        objObtenerDatosResponse.ProductType = strPostPago;
                    }

                    if (objObtenerDatosResponse.SubscriptionsPrepaid != null && objObtenerDatosResponse.SubscriptionsPrepaid.Count > 0)
                    {

                        if (objObtenerDatosResponse.SubscriptionsPostPaid == null)
                        {
                            objObtenerDatosResponse.SubscriptionsPostPaid = new List<SubscriptionPostPaidResponse>();
                        }

                        foreach (var item in objObtenerDatosResponse.SubscriptionsPrepaid)
                        {
                            if (item.ProductType.ToUpper() == strPrepago)
                            {
                                objObtenerDatosResponse.ProductType = strPrepago;

                                if (item.origenInfoPre.ToUpper() == strkeyOrigenInfo[1].Split(';')[0])
                                {
                                    var objSubscriptionPostPaid = new SubscriptionPostPaidResponse()
                                    {
                                        AccountNumber = item.AccountId,
                                        Segment = item.Segment,
                                        LineNumber = item.LineNumber,
                                        ProductType = strPrepago,
                                        LineStatus = item.LineStatus,
                                        RatePlan = item.RatePlan,
                                        OrigenInfo = item.origenInfoPre,
                                    };
                                    objObtenerDatosResponse.SubscriptionsPostPaid.Add(objSubscriptionPostPaid);
                                }
                            }
                            else if (item.ProductType.ToUpper().EndsWith("OLO"))
                            {
                                //Varificar el estado de las lineas
                                item.LineStatus = PrepagoOloStatus.Where(x => x.Split(';')[0] == item.LineStatus).FirstOrDefault().Split(';')[1];

                                objObtenerDatosResponse.ProductType = strPrepagoOlo;

                                var objSubscriptionPostPaid = new SubscriptionPostPaidResponse()
                                {
                                    AccountNumber = item.AccountId,
                                    Segment = item.Segment,
                                    LineNumber = item.LineNumber,
                                    ProductType = strPrepagoOlo,
                                    LineStatus = item.LineStatus,
                                    RatePlan = item.RatePlan,
                                    OrigenInfo = item.origenInfoPre,
                                };
                                objObtenerDatosResponse.SubscriptionsPostPaid.Add(objSubscriptionPostPaid);
                            }
                        }
                        objObtenerDatosResponse.SubscriptionsPrepaid = objObtenerDatosResponse.SubscriptionsPrepaid.Where(x => x.ProductType.ToUpper() == strPrepago).ToList();
                    }
                }
                else
                {
                    objObtenerDatosResponse = null;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objObtenerDatosRequest.Audit.Session, objObtenerDatosRequest.Audit.Transaction, "Capa: Business, Método:GetObtenerDatosCliente" + ex.Message);
                objObtenerDatosResponse = null;
            }
            return objObtenerDatosResponse;
        }
        #endregion

        #region GetCustomerInfo
        /// <summary>
        /// Método que obtiene el tipo de la cuenta-tipo de cliente(Masivo-Corporativo).
        /// </summary>
        /// <param name="CustomerInfoRequest">objCustomerInfoRequests</param>
        /// <returns>objCustomerInfoResponse contiene: CustomerType.</returns> 
        public static CustomerInfoResponse GetCustomerInfo(CustomerInfoRequest objCustomerInfoRequest)
        {

            Web.Logging.Info(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction, "Capa=> Business, Método =>GetCustomerInfo objCustomerInfoRequest:  CustomerId: " + objCustomerInfoRequest.CustomerId);
            CustomerInfoResponse objCustomerInfoResponse = null;
            try
            {
                List<string> productType = KEY.AppSettings("strkeyCustomerType").Split('|').ToList();

                objCustomerInfoResponse = Claro.Web.Logging.ExecuteMethod<CustomerInfoResponse>(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction,
                () =>
                {
                    return Data.Coliving.SearchCustomer.GetCustomerInfoDP(objCustomerInfoRequest);
                });
                if (objCustomerInfoResponse != null)
                {
                    objCustomerInfoResponse.CustomerType = productType.Where(x => x.Split(';')[0] == objCustomerInfoResponse.CustomerType).FirstOrDefault().Split(';')[1];
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction, "Capa: Business Método:GetCustomerInfo" + ex.Message);
                objCustomerInfoResponse = null;
            }

            return objCustomerInfoResponse;
        }
        #endregion


        #region OST - Listar OST
        /// <summary>
        /// Método que obtiene la lista de OST de LEGADO
        /// </summary>
        /// <param name="objListOSTRequest">objListOSTRequest</param>
        /// <returns>ListOSTResponse contiene: Listado de OST de LEGADO.</returns> 
        public static ENTITIES_OST.ListOSTResponse getListOST_Legacy(ListOSTRequest objListOSTRequest)
        {

           Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction,
           string.Format("Capa Business: {0}, IdBusca:{1}, IdCAC:{2}, IdCriterio:{3}, IdEstado:{4}", "getListOST_Legacy", objListOSTRequest.IdBusca, objListOSTRequest.IdCAC, objListOSTRequest.IdCriterio, objListOSTRequest.IdEstado));

           ENTITIES_OST.ListOSTResponse objListOSTResponse = null;
           try
           {
               objListOSTResponse = Claro.Web.Logging.ExecuteMethod<ENTITIES_OST.ListOSTResponse>(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, () =>
               {
                   return Data.Coliving.SearchCustomer.getListOST_Legacy(objListOSTRequest);
               });
           }
           catch (Exception ex)
           {
               Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, ex.Message);
               throw ex;
           }
           return objListOSTResponse;
        }


        /// <summary>
        /// Método que obtiene la lista de OST de ONE
        /// </summary>
        /// <param name="objListOSTRequest">objListOSTRequest</param>
        /// <returns>ListOSTResponse contiene: lista de OST de ONE.</returns> 
        public static ENTITIES_OST.ListOSTResponse getListOST_One(ListOSTRequest objListOSTRequest)
        {

            Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction,
            string.Format("Capa Business: {0}, IdBusca:{1}, IdCAC:{2}, IdCriterio:{3}, IdEstado:{4}", "getListOST_One", objListOSTRequest.IdBusca, objListOSTRequest.IdCAC, objListOSTRequest.IdCriterio, objListOSTRequest.IdEstado));

            ENTITIES_OST.ListOSTResponse objListOSTResponse = null;
            try
            {
                objListOSTResponse = Claro.Web.Logging.ExecuteMethod<ENTITIES_OST.ListOSTResponse>(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, () =>
                {
                    return Data.Coliving.SearchCustomer.getListOST_One(objListOSTRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objListOSTResponse;
        }



        /// <summary>
        /// Método que obtiene el detalle de las OSTs de ONE
        /// </summary>
        /// <param name="objListOSTRequest">objListOSTRequest</param>
        /// <returns>ListOSTResponse contiene: el detalle de una OST.</returns> 
        public static ENTITIES_OST.ListOSTResponse getListOSTDetails_One(ListOSTRequest objListOSTRequest)
        {

            Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction,
            string.Format("Capa Business: {0}, IdBusca:{1}, IdCAC:{2}, IdCriterio:{3}, IdEstado:{4}", "getListOST_One", objListOSTRequest.IdBusca, objListOSTRequest.IdCAC, objListOSTRequest.IdCriterio, objListOSTRequest.IdEstado));

            ENTITIES_OST.ListOSTResponse objListOSTResponse = null;
            try
            {
                objListOSTResponse = Claro.Web.Logging.ExecuteMethod<ENTITIES_OST.ListOSTResponse>(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, () =>
                {
                    return Data.Coliving.SearchCustomer.getListOSTDetails_One(objListOSTRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objListOSTResponse;
        }

        #endregion

    }
}
