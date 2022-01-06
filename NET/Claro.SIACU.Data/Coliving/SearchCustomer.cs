using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ENTITIES_COLIVING = Claro.SIACU.Entity.Coliving.getSearchCustomer;
using ENTITIES_RETRIEVE = Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions;
using ENTITTIES_CUSTOMERDATA = Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving;
using ENTITIES_CUSTOMERINFO = Claro.SIACU.Entity.Coliving.getCustomerInfo;
using ENTITIES_CONSULTALINEA = Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta;
using CONSULTALINEACUENTA = Claro.SIACU.ProxyService.SIAC.ConsultaLineaCuenta;
using AGREEMENT_MANAGEMENTDP = Claro.SIACU.ProxyService.SIACU.RetrieveSubscriptions;
using SEARCH_CUSTOMERLINE_DP = Claro.SIACU.ProxyService.SIACU.BusquedaLineaCliente;
using CUSTOMER_MANAGEMENT_DP = Claro.SIACU.ProxyService.SIACU.CustomerManagement;
using CUSTOMER_INFO_DP = Claro.SIACU.ProxyService.SIACU.CustomerInfo;

using TECHNICAL_SERVICES_BYCUSTOMER = Claro.SIACU.ProxyService.SIACU.TechnicalServicesByCustomer;
using TECHNICAL_SERVICES_DETAILS = Claro.SIACU.ProxyService.SIACU.TechnicalServiceDetails;
using ENTITIES_OST = Claro.SIACU.Entity.Coliving.getListOST;

using COLIVING_CONSULTCLAVE = Claro.SIACU.ProxyService.SIAC.Claves;
using COMMON_DATA = Claro.SIACU.Data;
using KEY = Claro.ConfigurationManager;
using System.ServiceModel;
using Claro.Entity;
using Claro.Data;
using System.Data;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Coliving.getListOST;
namespace Claro.SIACU.Data.Coliving
{
    public class SearchCustomer
    {
        #region ConsultarLineaCuenta
        /// <summary>
        /// Método para obtener el estado de la linea/cuenta si ha migrado o no.
        /// </summary>
        /// <param name="objConsultaLineaRequest">Contiene:Type y Value</param>
        /// <returns>Devuelve el objConsultaLineaResponse que contiene la descripción si ha migrado.</returns>
        public static ENTITIES_CONSULTALINEA.ConsultaLineaResponse ConsultarLineaCuenta(ENTITIES_CONSULTALINEA.ConsultaLineaRequest objConsultaLineaRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objConsultaLineaRequest.Audit.Session,
               "Transaccion: " + objConsultaLineaRequest.Audit.Transaction,
               string.Format("Capa Data-Metodo: {0}, Type:{1}, Value{2} ", "ConsultarLineaCuenta", objConsultaLineaRequest.Type, objConsultaLineaRequest.Value));


            CONSULTALINEACUENTA.consultarLineaCuentaResponse consultarLineaCuentaResponse = null;
            ENTITIES_CONSULTALINEA.ConsultaLineaResponse objConsultaLineaResponse = null;
            CONSULTALINEACUENTA.consultarLineaCuentaRequest consultarLineaCuentaRequest = new CONSULTALINEACUENTA.consultarLineaCuentaRequest()
            {
                tipoConsulta = objConsultaLineaRequest.Type,
                valorConsulta = objConsultaLineaRequest.Value
            };
            try
            {


                consultarLineaCuentaResponse = Configuration.ServiceConfiguration.CONSULTALINEACUENTA.consultarLineaCuenta(consultarLineaCuentaRequest);
                if (consultarLineaCuentaResponse != null)
                {
                    objConsultaLineaResponse = new ENTITIES_CONSULTALINEA.ConsultaLineaResponse();
                    if (consultarLineaCuentaResponse.rptaConsulta == Constants.ZeroNumber || consultarLineaCuentaResponse.rptaConsulta == Claro.Constants.NumberOneString)
                    {

                        objConsultaLineaResponse.ResponseValue = consultarLineaCuentaResponse.rptaConsulta;
                    }
                    if (consultarLineaCuentaResponse.cursorDatos != null)
                    {
                        objConsultaLineaResponse.itm = new ENTITIES_CONSULTALINEA.Itm()
                        {

                            origenCuenta = consultarLineaCuentaResponse.cursorDatos[0].origenCuenta,
                            codCuenta = consultarLineaCuentaResponse.cursorDatos[0].codCuenta,
                            coId = consultarLineaCuentaResponse.cursorDatos[0].coId,
                            identificacion = consultarLineaCuentaResponse.cursorDatos[0].identificacion,
                            actCuentaProd = consultarLineaCuentaResponse.cursorDatos[0].actCuentaProd,
                            migCuentaProd = consultarLineaCuentaResponse.cursorDatos[0].migCuentaProd,
                            origenRegistro = consultarLineaCuentaResponse.cursorDatos[0].origenRegistro,
                            estado = consultarLineaCuentaResponse.cursorDatos[0].estado,
                            usrCrea = consultarLineaCuentaResponse.cursorDatos[0].usrCrea,
                            usrModif = consultarLineaCuentaResponse.cursorDatos[0].usrModif,
                            fchCreacion = consultarLineaCuentaResponse.cursorDatos[0].fchCreacion,
                            fchModif = consultarLineaCuentaResponse.cursorDatos[0].fchModif,
                            custCode = consultarLineaCuentaResponse.cursorDatos[0].custCode
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsultaLineaRequest.Audit.Session, objConsultaLineaRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objConsultaLineaResponse;
        }
        #endregion

        #region BusquedaLineaClienteDP - GetObtenerDatosClienteDP
        ///<summary>
        /// Método(consume servicio DataPower) que consulta los datos una linea/cuenta no migrada.
        /// </summary>
        /// <param name="objCustomerInfoRequest">Contiene: CustomerId|DocumentType,DocumentNumber|LineNumber</param>
        /// <returns>Devuele el objeto objObtenerDatosResponse.</returns> 
        public static Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving.ObtenerDatosClienteResponse GetObtenerDatosClienteDP(ENTITTIES_CUSTOMERDATA.ObtenerDatosClienteRequest objObtenerDatosClienteRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objObtenerDatosClienteRequest.Audit.Session,
                  "Transaccion: " + objObtenerDatosClienteRequest.Audit.Transaction,
                  string.Format("Capa Data-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3} ,LineNumber{4}",
                                  "GetObtenerDatosClienteDP", objObtenerDatosClienteRequest.CustomerId, objObtenerDatosClienteRequest.DocumentType, objObtenerDatosClienteRequest.DocumentNumber, objObtenerDatosClienteRequest.LineNumber));


            SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageResponseType responseCustomerData = new SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageResponseType();
            SEARCH_CUSTOMERLINE_DP.HeaderResponse objHeaderResponse = new SEARCH_CUSTOMERLINE_DP.HeaderResponse();
            ENTITTIES_CUSTOMERDATA.ObtenerDatosClienteResponse ObtenerDatosClienteResponse = null;

            string userAplicacion = "";
            string password = "";
            string strKey = KEY.AppSettings("strUserWSProd") + "|" + KEY.AppSettings("strPasswordWSProd");
            var audit = objObtenerDatosClienteRequest.Audit;
            try
            {
                var tipoDocumento = SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestTypeTipoDocumento.Item;
                if (objObtenerDatosClienteRequest.DocumentType != null)
                {
                    foreach (SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestTypeTipoDocumento x in (SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestTypeTipoDocumento[])Enum.GetValues(typeof(SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestTypeTipoDocumento)))
                    {
                        var _name = ((XmlEnumAttribute)typeof(SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestTypeTipoDocumento)
                            .GetMember(x.ToString())[0]
                            .GetCustomAttributes(typeof(XmlEnumAttribute), false)[0])
                            .Name;
                        if (_name == objObtenerDatosClienteRequest.DocumentType)
                        {
                            tipoDocumento = x;
                        }
                    }
                }

                SEARCH_CUSTOMERLINE_DP.HeaderRequestType objHeaderRequestType = new SEARCH_CUSTOMERLINE_DP.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSRestricTetheVeloc"),
                    country = KEY.AppSettings("strCountryWSRestricTetheVeloc"),
                    dispositivo = objObtenerDatosClienteRequest.Audit.IPAddress,
                    language = KEY.AppSettings("strLanguageWSRestricTetheVeloc"),
                    modulo = KEY.AppSettings("strModuloColiving"),
                    msgType = KEY.AppSettings("strMsgTypeWSRestricTetheVeloc"),
                    operation = KEY.AppSettings("strOperationObtenerDatos"),
                    pid = objObtenerDatosClienteRequest.Audit.Transaction,
                    system = KEY.AppSettings("NombreAplicacion"),
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    userId = objObtenerDatosClienteRequest.Audit.UserName,
                    VarArg = new SEARCH_CUSTOMERLINE_DP.ArgType[1],
                    wsIp = objObtenerDatosClienteRequest.Audit.IPAddress
                };
                objHeaderRequestType.VarArg[0] = new SEARCH_CUSTOMERLINE_DP.ArgType() { key = string.Empty, value = string.Empty };

                SEARCH_CUSTOMERLINE_DP.HeaderRequest objHeaderRequest = new SEARCH_CUSTOMERLINE_DP.HeaderRequest()
                {
                    channel = objObtenerDatosClienteRequest.Audit.ApplicationName,
                    idApplication = objObtenerDatosClienteRequest.Audit.IPAddress,
                    userApplication = objObtenerDatosClienteRequest.Audit.UserName,
                    userSession = objObtenerDatosClienteRequest.Audit.UserName,
                    idESBTransaction = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    idBusinessTransaction = objObtenerDatosClienteRequest.Audit.Transaction,
                    startDate = DateTime.Parse(string.Format("{0:s}", DateTime.Now)),
                    additionalNode = new object(),
                    startDateSpecified = true,

                };

                SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestType requestCustomerData = new SEARCH_CUSTOMERLINE_DP.obtenerDatosClienteColivingMessageRequestType()
                {
                    NroDocumento = objObtenerDatosClienteRequest.DocumentNumber,
                    numeroCuenta = objObtenerDatosClienteRequest.CustomerId,
                    numeroLinea = objObtenerDatosClienteRequest.LineNumber,
                    tipoDocumento = tipoDocumento,
                    tipoDocumentoSpecified = true
                };

                var result = COMMON_DATA.Common.IsOkGetKeyEncrypted(audit.Session, audit.Transaction, strKey, audit.IPAddress, audit.IPAddress, audit.UserName, audit.ApplicationName, out  userAplicacion, out password);

                if (result)
                {
                    using (new OperationContextScope(Configuration.ServiceConfiguration.OBTENER_DATOSCLIENTEDP.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(audit.Transaction, userAplicacion, password));

                        SEARCH_CUSTOMERLINE_DP.HeaderResponseType objHeaderResponseType = Claro.Web.Logging.ExecuteMethod<SEARCH_CUSTOMERLINE_DP.HeaderResponseType>
                        (audit.Session, audit.Transaction, () =>
                        {
                            return Configuration.ServiceConfiguration.OBTENER_DATOSCLIENTEDP.obtenerDatosClienteColiving(objHeaderRequestType,
                                objHeaderRequest, requestCustomerData, out objHeaderResponse, out responseCustomerData);
                        });

                        Claro.Web.Logging.Info(audit.Session, audit.Transaction,
                    String.Format("OBTENER_DATOSCLIENTEDP: HeaderResponse DataPower -> Status.code: {0}, Status.message: {1}", objHeaderResponseType != null ? objHeaderResponseType.Status.code : "NULO", objHeaderResponseType != null ? objHeaderResponseType.Status.message : "NULO"));

                    }
                    if (responseCustomerData != null)
                    {
                        if (responseCustomerData.status == Claro.Constants.NumberZeroString)
                        {
                            ENTITTIES_CUSTOMERDATA.SubscriptionPostPaidResponse objSubscriptionPostPaid = null;
                            ENTITTIES_CUSTOMERDATA.SubscriptionPrepaidResponse objSubscriptionPrepaid = null;

                            List<ENTITTIES_CUSTOMERDATA.SubscriptionPostPaidResponse> listSubscriptionPostPaid = new List<ENTITTIES_CUSTOMERDATA.SubscriptionPostPaidResponse>();
                            if (responseCustomerData.subscriptionsPostPago != null && responseCustomerData.subscriptionsPostPago.Any())
                            {
                                foreach (var xPostPaid in responseCustomerData.subscriptionsPostPago)
                                {
                                    objSubscriptionPostPaid = new ENTITTIES_CUSTOMERDATA.SubscriptionPostPaidResponse();
                                    objSubscriptionPostPaid.Code_Plan = xPostPaid.codigoPlan;
                                    objSubscriptionPostPaid.CustomerId = xPostPaid.customerId;
                                    objSubscriptionPostPaid.Description = xPostPaid.descripcion;
                                    objSubscriptionPostPaid.ContractNumber = xPostPaid.nroContrato;
                                    objSubscriptionPostPaid.Code_PlanType = xPostPaid.codigoTipoPlan;
                                    objSubscriptionPostPaid.Code_AccountType = xPostPaid.codigoTipoCuenta;
                                    objSubscriptionPostPaid.Segment = xPostPaid.tipoCuenta;
                                    objSubscriptionPostPaid.AccountNumber = xPostPaid.nroCuenta;
                                    objSubscriptionPostPaid.LineNumber = xPostPaid.nroLinea;
                                    objSubscriptionPostPaid.ProductType = xPostPaid.modalidad;
                                    objSubscriptionPostPaid.LineStatus = xPostPaid.estadoLinea;
                                    objSubscriptionPostPaid.RatePlan = xPostPaid.plan;
                                    objSubscriptionPostPaid.OrigenInfo = xPostPaid.OrigenInfoPost;
                                    listSubscriptionPostPaid.Add(objSubscriptionPostPaid);
                                }
                            }

                            List<ENTITTIES_CUSTOMERDATA.SubscriptionPrepaidResponse> listSubscriptionPrepaid = new List<ENTITTIES_CUSTOMERDATA.SubscriptionPrepaidResponse>();
                            if (responseCustomerData.subscriptionsPrepago != null && responseCustomerData.subscriptionsPrepago.Any())
                            {
                                foreach (var xPrepaid in responseCustomerData.subscriptionsPrepago)
                                {
                                    objSubscriptionPrepaid = new ENTITTIES_CUSTOMERDATA.SubscriptionPrepaidResponse();
                                    objSubscriptionPrepaid.ProductType = xPrepaid.modalidad;
                                    objSubscriptionPrepaid.Segment = xPrepaid.tipoCuenta;
                                    objSubscriptionPrepaid.AccountId = xPrepaid.idCuenta;
                                    objSubscriptionPrepaid.LineNumber = xPrepaid.NroLinea;
                                    objSubscriptionPrepaid.LineStatus = xPrepaid.EstadoLinea;
                                    objSubscriptionPrepaid.RatePlan = xPrepaid.plan;
                                    objSubscriptionPrepaid.origenInfoPre = xPrepaid.OrigenInfoPre;
                                    listSubscriptionPrepaid.Add(objSubscriptionPrepaid);
                                }
                            }

                            ObtenerDatosClienteResponse = new ENTITTIES_CUSTOMERDATA.ObtenerDatosClienteResponse()
                            {
                                Status = responseCustomerData.status,
                                CodeResponse = responseCustomerData.codeResponse,
                                DescriptionResponse = responseCustomerData.descriptionResponse,
                                Date = responseCustomerData.date,
                                CustomerName = responseCustomerData.nombreCliente,
                                CustomerAddress = responseCustomerData.direccionCliente,
                                //DescriptionDocumentType = responseCustomerData.tipoDcto,
                                DocumentType = responseCustomerData.tipoDcto,
                                DocumentNumber = responseCustomerData.nroDocumento,
                                SubscriptionsPostPaid = listSubscriptionPostPaid.Count > 0 ? listSubscriptionPostPaid : null,
                                SubscriptionsPrepaid = listSubscriptionPrepaid.Count > 0 ? listSubscriptionPrepaid : null
                            };
                            Claro.Web.Logging.Info("IdSession: " + objObtenerDatosClienteRequest.Audit.Session, "Transaccion: " + objObtenerDatosClienteRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseSearchCustomer: " + responseCustomerData.status + " -> 0 = Ok, 1=Error");
                        }
                        else
                        {
                            Claro.Web.Logging.Error("IdSession: " + objObtenerDatosClienteRequest.Audit.Session, "Transaccion: " + objObtenerDatosClienteRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseSearchCustomer: " + responseCustomerData.status + " -> 0 = Ok, 1=Error");

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objObtenerDatosClienteRequest.Audit.Session, objObtenerDatosClienteRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return ObtenerDatosClienteResponse;
        }
        #endregion

        #region CustomerManagementDP -  GetSearchCustomerDP

        /// <summary>
        /// Método(consume servicio DataPower) que consulta los datos de una linea/cuenta que ha migrado
        /// </summary>
        /// <param name="objCustomerInfoRequest">objCustomerInfoRequest</param>
        /// <returns>Devuelve el objeto searchCustomerResponse que contiene:DocumentNumber,DocumentType,CustomerName,Address,AccountNumber </returns>
        public static Claro.SIACU.Entity.Coliving.getSearchCustomer.SearchCustomerResponse GetSearchCustomerDP(ENTITIES_COLIVING.SearchCustomerRequest objsearchCustomerRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objsearchCustomerRequest.Audit.Session,
                 "Transaccion: " + objsearchCustomerRequest.Audit.Transaction,
                 string.Format("Capa Data-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3} ,InvoiceNumber{4}",
                                 "GetSearchCustomerDP", objsearchCustomerRequest.CustomerId, objsearchCustomerRequest.DocumentType, objsearchCustomerRequest.DocumentNumber, objsearchCustomerRequest.InvoiceNumber));


            CUSTOMER_MANAGEMENT_DP.SearchCustomerResponseType responseSearchCustomer = new CUSTOMER_MANAGEMENT_DP.SearchCustomerResponseType();
            CUSTOMER_MANAGEMENT_DP.HeaderResponseType objHeaderResponseType = new CUSTOMER_MANAGEMENT_DP.HeaderResponseType();
            ENTITIES_COLIVING.SearchCustomerResponse objsearchCustomerResponse = null;

            string userAplicacion = "";
            string password = "";
            var audit = objsearchCustomerRequest.Audit;
            string strKey = KEY.AppSettings("strUserWSProd") + "|" + KEY.AppSettings("strPasswordWSProd");
            try
            {

                CUSTOMER_MANAGEMENT_DP.SearchCustomerRequestType requestSearchCustomer = new CUSTOMER_MANAGEMENT_DP.SearchCustomerRequestType()
                {
                    _customer = new CUSTOMER_MANAGEMENT_DP.Customer()
                    {
                        ID = objsearchCustomerRequest.CustomerId,
                        _party = new CUSTOMER_MANAGEMENT_DP.Party()
                        {
                            _partyExtension = new CUSTOMER_MANAGEMENT_DP.PartyExtension()
                            {
                                identificationType = objsearchCustomerRequest.DocumentType
                            },
                            partyId = objsearchCustomerRequest.DocumentNumber,
                        },
                        _customerAccount = new CUSTOMER_MANAGEMENT_DP.CustomerAccount[] 
                        {
                            new CUSTOMER_MANAGEMENT_DP.CustomerAccount()
                            {
                              _customerBill = new CUSTOMER_MANAGEMENT_DP.CustomerBill[] 
                              {
                                new CUSTOMER_MANAGEMENT_DP.CustomerBill()
                                {
                                    _partyBill = new CUSTOMER_MANAGEMENT_DP.PartyBill()
                                    {
                                        billNo = objsearchCustomerRequest.InvoiceNumber
                                    }
                                }
                              }
                            }      
                        }
                    },
                    _telephoneNumber = new CUSTOMER_MANAGEMENT_DP.TelephoneNumber()
                    {
                        number = objsearchCustomerRequest.Msisdn
                    }
                };
                CUSTOMER_MANAGEMENT_DP.HeaderRequest objHeaderRequest = new CUSTOMER_MANAGEMENT_DP.HeaderRequest()
                {
                    channel = objsearchCustomerRequest.Audit.ApplicationName,
                    idApplication = objsearchCustomerRequest.Audit.IPAddress,
                    userApplication = objsearchCustomerRequest.Audit.UserName,
                    userSession = objsearchCustomerRequest.Audit.UserName,
                    idESBTransaction = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    idBusinessTransaction = objsearchCustomerRequest.Audit.Transaction,
                    startDate = DateTime.Parse(string.Format("{0:s}", DateTime.Now)),
                    additionalNode = new object(),
                    startDateSpecified = true
                };
                CUSTOMER_MANAGEMENT_DP.HeaderRequestType objHeaderRequestType = new CUSTOMER_MANAGEMENT_DP.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSRestricTetheVeloc"),
                    country = KEY.AppSettings("strCountryWSRestricTetheVeloc"),
                    dispositivo = audit.IPAddress,
                    language = KEY.AppSettings("strLanguageWSRestricTetheVeloc"),
                    modulo = KEY.AppSettings("strModuloColiving"),
                    msgType = KEY.AppSettings("strMsgTypeWSRestricTetheVeloc"),
                    operation = KEY.AppSettings("strOperationSearchCustomer"),
                    pid = audit.Transaction,
                    system = KEY.AppSettings("NombreAplicacion"),
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    userId = audit.UserName,
                    VarArg = new CUSTOMER_MANAGEMENT_DP.ArgType[1],
                    wsIp = audit.IPAddress
                };
                objHeaderRequestType.VarArg[0] = new CUSTOMER_MANAGEMENT_DP.ArgType() { key = string.Empty, value = string.Empty };

                var result = COMMON_DATA.Common.IsOkGetKeyEncrypted(audit.Session, audit.Transaction, strKey, audit.IPAddress, audit.IPAddress, audit.UserName, audit.ApplicationName, out  userAplicacion, out password);

                if (result)
                {
                    using (new OperationContextScope(Configuration.ServiceConfiguration.CUSTOMER_MANAGEMENTDP.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(audit.Transaction, userAplicacion, password));

                        Claro.Web.Logging.ExecuteMethod<CUSTOMER_MANAGEMENT_DP.HeaderResponse>
                         (audit.Session, audit.Transaction, () =>
                         {
                             return Configuration.ServiceConfiguration.CUSTOMER_MANAGEMENTDP.searchCustomer
                                 (objHeaderRequest,
                                 objHeaderRequestType,
                                 requestSearchCustomer,
                                 out objHeaderResponseType,
                                 out responseSearchCustomer);
                         });

                        Claro.Web.Logging.Info(audit.Session, audit.Transaction,
                            String.Format("CUSTOMER_MANAGEMENTDP: HeaderResponse DataPower -> Status.code: {0}, Status.message: {1}", objHeaderResponseType != null ? objHeaderResponseType.Status.code : "NULO", objHeaderResponseType != null ? objHeaderResponseType.Status.message : "NULO"));
                    }
                    if (responseSearchCustomer != null)
                    {
                        var responseStatus = responseSearchCustomer.responseStatus;

                        if (responseStatus != null)
                        {
                            if (responseStatus.status == Claro.Constants.NumberZero && responseSearchCustomer.responseData != null)
                            {

                                var data = responseSearchCustomer.responseData;
                                objsearchCustomerResponse = new ENTITIES_COLIVING.SearchCustomerResponse()
                                {
                                    CodeResponse = responseStatus.codeResponse,
                                    DescriptionResponse = responseStatus.descriptionResponse,
                                    Date = responseStatus.date,
                                    CustomerName = data._customer.name,
                                    Address = data._geographicAddress._localAddress[0].fullAddress != null ? data._geographicAddress._localAddress[0].fullAddress : "",//?
                                    AccountNumber = data._customer._customerAccount[0]._partyAccount.ID,
                                    DocumentType = responseSearchCustomer.responseData._customer._party._partyExtension.identificationType,
                                    DocumentNumber = responseSearchCustomer.responseData._customer._party.partyId,
                                    ParentAccount = data._customer._customerAccount[0] != null ? data._customer._customerAccount[0]._partyAccount.ID : "",//?
                                    CustomerStatus = data._customer.customerStatus
                                };

                            }
                            Claro.Web.Logging.Info("IdSession: " + objsearchCustomerRequest.Audit.Session, "Transaccion: " + objsearchCustomerRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data -> Parametro de salida: responseSearchCustomer.Status: " + responseSearchCustomer.responseStatus.status + " -> 0 = Ok, 1= Error");
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info("IdSession: " + objsearchCustomerRequest.Audit.Session, "Transaccion: " + objsearchCustomerRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data -> Parametro de salida: responseSearchCustomer: null");
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objsearchCustomerRequest.Audit.Session, objsearchCustomerRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objsearchCustomerResponse;
        }
        #endregion

        #region AgreementManagementDP - GetRetrieveSubscriptionsDP

        /// <summary>
        /// Método(consume servicio DataPower) que devuelve las susbcripcion del cliente de una linea/cuenta migrada.
        /// </summary>
        /// <param name="retrieveSubscriptions">retrieveSubscriptions</param>
        /// <returns>Devuele el objeto retrieveSubscriptionResponse: PoName,ServiceIdentifier,ProductType,SubscriptionStatus</returns> 
        public static ENTITIES_RETRIEVE.RetrieveSubscriptionResponse GetRetrieveSubscriptionsDP(ENTITIES_RETRIEVE.RetrieveSubscriptionRequest retrieveSubscriptionsRequest)
        {

            Claro.Web.Logging.Info("IdSession: " + retrieveSubscriptionsRequest.Audit.Session,
           "Transaccion: " + retrieveSubscriptionsRequest.Audit.Transaction,
           string.Format("Capa Data-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3} ,serviceIdentifier{4}",
                           "GetRetrieveSubscriptionsDP", retrieveSubscriptionsRequest.customerId, retrieveSubscriptionsRequest.DocumentType, retrieveSubscriptionsRequest.DocumentNumber, retrieveSubscriptionsRequest.serviceIdentifier));



            AGREEMENT_MANAGEMENTDP.RetrieveSubscriptionsResponseType retrieveSubscriptionsResponseType = new AGREEMENT_MANAGEMENTDP.RetrieveSubscriptionsResponseType();
            ENTITIES_RETRIEVE.RetrieveSubscriptionResponse retrieveSubscriptionsResponse = null;

            AGREEMENT_MANAGEMENTDP.HeaderResponseType headerResponseType = new AGREEMENT_MANAGEMENTDP.HeaderResponseType();


            var audit = retrieveSubscriptionsRequest.Audit;
            string userAplicacion = "";
            string password = "";
            string strKey = KEY.AppSettings("strUserWSProd") + "|" + KEY.AppSettings("strPasswordWSProd");
            try
            {
                AGREEMENT_MANAGEMENTDP.HeaderRequest headerRequest = new AGREEMENT_MANAGEMENTDP.HeaderRequest()
                {
                    channel = retrieveSubscriptionsRequest.Audit.ApplicationName,
                    idApplication = retrieveSubscriptionsRequest.Audit.IPAddress,
                    userApplication = retrieveSubscriptionsRequest.Audit.UserName,
                    userSession = retrieveSubscriptionsRequest.Audit.UserName,
                    idESBTransaction = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    idBusinessTransaction = retrieveSubscriptionsRequest.Audit.Transaction,
                    startDate = DateTime.Now,
                    startDateSpecified = true,
                    additionalNode = new object()
                };

                AGREEMENT_MANAGEMENTDP.HeaderRequestType headerRequestType = new AGREEMENT_MANAGEMENTDP.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSRestricTetheVeloc"),
                    country = KEY.AppSettings("strCountryWSRestricTetheVeloc"),
                    dispositivo = audit.IPAddress,
                    language = KEY.AppSettings("strLanguageWSRestricTetheVeloc"),
                    modulo = KEY.AppSettings("strModuloColiving"),
                    msgType = KEY.AppSettings("strMsgTypeWSRestricTetheVeloc"),
                    operation = KEY.AppSettings("strOperationRetrieveSubscription"),
                    pid = audit.Transaction,
                    system = KEY.AppSettings("NombreAplicacion"),
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    userId = audit.UserName,
                    VarArg = new AGREEMENT_MANAGEMENTDP.ArgType[1],
                    wsIp = audit.IPAddress
                };
                headerRequestType.VarArg[0] = new AGREEMENT_MANAGEMENTDP.ArgType() { key = string.Empty, value = string.Empty };

                #region valores
                AGREEMENT_MANAGEMENTDP.RetrieveSubscriptionsRequestType retrieveSubscriptionsRequestMessage = new AGREEMENT_MANAGEMENTDP.RetrieveSubscriptionsRequestType()
                {
                    _customer = new AGREEMENT_MANAGEMENTDP.Customer()
                    {
                        ID = retrieveSubscriptionsRequest.customerId,
                        _customerAccount = new AGREEMENT_MANAGEMENTDP.CustomerAccount[] 
                        { 
                            new AGREEMENT_MANAGEMENTDP.CustomerAccount()
                            {
                                _partyRole = new AGREEMENT_MANAGEMENTDP.PartyRole[] 
                                {  
                                    new AGREEMENT_MANAGEMENTDP.PartyRole()
                                    {
                                        _partyRoleProductOffering = new AGREEMENT_MANAGEMENTDP.PartyRoleProductOffering[] 
                                        { 
                                            new AGREEMENT_MANAGEMENTDP.PartyRoleProductOffering()
                                            {
                                                _agreementItem = new AGREEMENT_MANAGEMENTDP.AgreementItem[] 
                                                { 
                                                    new AGREEMENT_MANAGEMENTDP.AgreementItem()
                                                    {
                                                        _agreement = new AGREEMENT_MANAGEMENTDP.Agreement()
                                                        {
                                                            ID = retrieveSubscriptionsRequest.contractId,
                                                            interactionStatus = retrieveSubscriptionsRequest.subscriptionStatus
                                                        }
                                                    } 
                                                }
                                            } 
                                        }
                                    }
                                }
                            } 
                        },
                        _partyOrderItem = new AGREEMENT_MANAGEMENTDP.PartyOrderItem[] 
                        {
                             new AGREEMENT_MANAGEMENTDP.PartyOrderItem()
                            {
                                _product = new AGREEMENT_MANAGEMENTDP.Product()
                                {
                                    _productOffering = new AGREEMENT_MANAGEMENTDP.ProductOffering()
                                    {
                                        _partyProfileType = new AGREEMENT_MANAGEMENTDP.PartyProfileType[] 
                                        { 
                                            new AGREEMENT_MANAGEMENTDP.PartyProfileType()
                                            {
                                                ID = retrieveSubscriptionsRequest.serviceIdentifier
                                            }
                                        }
                                    }
                                }
                            }
                        },

                        _party = new AGREEMENT_MANAGEMENTDP.Party()
                        {
                            partyId = retrieveSubscriptionsRequest.DocumentNumber,
                            _partyUser = new AGREEMENT_MANAGEMENTDP.PartyUser[] 
                            {
                                new AGREEMENT_MANAGEMENTDP.PartyUser()
                                {
                                    _partyUserExtension = new AGREEMENT_MANAGEMENTDP.PartyUserExtension()
                                    {
                                        type = retrieveSubscriptionsRequest.DocumentType
                                    }
                                }
                            }
                        },
                        _customerOrderItem = new AGREEMENT_MANAGEMENTDP.CustomerOrderItem[] 
                        {
                            new AGREEMENT_MANAGEMENTDP.CustomerOrderItem()
                            {
                                _productSpecification = new AGREEMENT_MANAGEMENTDP.ProductSpecification()
                                {
                                    _product = new AGREEMENT_MANAGEMENTDP.Product()
                                    {
                                        
                                        _productBundle = new AGREEMENT_MANAGEMENTDP.ProductBundle[] 
                                        {
                                            new AGREEMENT_MANAGEMENTDP.ProductBundle()
                                            {
                                                ID = retrieveSubscriptionsRequest.bundleId
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
                #endregion

                var result = COMMON_DATA.Common.IsOkGetKeyEncrypted(audit.Session, audit.Transaction, strKey, audit.IPAddress, audit.IPAddress, audit.UserName, audit.ApplicationName, out  userAplicacion, out password);
                if (result)
                {
                    using (new OperationContextScope(Configuration.ServiceConfiguration.AGREEMENT_MANAGEMENTDP.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(audit.Transaction, userAplicacion, password));

                        Claro.Web.Logging.ExecuteMethod<AGREEMENT_MANAGEMENTDP.HeaderResponse>
                         (audit.Session, audit.Transaction, () =>
                         {
                             return Configuration.ServiceConfiguration.AGREEMENT_MANAGEMENTDP.retrieveSubscriptions(
                                 headerRequest,
                                 headerRequestType,
                                 retrieveSubscriptionsRequestMessage,
                                 out headerResponseType,
                                 out retrieveSubscriptionsResponseType);
                         });

                        Claro.Web.Logging.Info(audit.Session, audit.Transaction,
                       String.Format("AGREEMENT_MANAGEMENTDP: HeaderResponse DataPower -> Status.code: {0}, Status.message: {1}", headerResponseType != null ? headerResponseType.Status.code : "NULO", headerResponseType != null ? headerResponseType.Status.message : "NULO"));

                    }
                    var lista = new List<ENTITIES_RETRIEVE.Subscription>();
                    if (retrieveSubscriptionsResponseType != null)
                    {
                        if (retrieveSubscriptionsResponseType.responseStatus != null && retrieveSubscriptionsResponseType.responseData != null && retrieveSubscriptionsResponseType.responseStatus.status == Claro.Constants.NumberZero)
                        {
                            if (retrieveSubscriptionsResponseType.responseStatus.status == 0 && retrieveSubscriptionsResponseType.responseData.Any())
                            {
                                foreach (var item in retrieveSubscriptionsResponseType.responseData)
                                {
                                    var entidadRetrieve = new ENTITIES_RETRIEVE.Subscription()
                                    {
                                        BillingAccountId = item._customer._customerAccount[0]._partyRole[0].partyRoleId,
                                        BillingAccountStatus = item._customer._customerAccount[0]._partyRole[0].status != null ? item._customer._customerAccount[0]._partyRole[0].status : "",
                                        ProductType = item._productCategory.name,
                                        CustomerId = item._customer.ID,
                                        ServiceIdentifier = item._customer._partyRoleProductSpecification[0]._productSpecification._productSpecificationType[0].name,
                                        PoId = item._customer._customerAccount[0]._partyRole[0]._partyRoleProductOffering[0]._agreementItem[0]._productOffering.ID,
                                        PoName = item._customer._customerAccount[0]._partyRole[0]._partyRoleProductOffering[0]._agreementItem[0]._productOffering.name,
                                        SubscriptionStatus = item._customer._customerAccount[0]._partyRole[0]._partyRoleProductOffering[0]._agreementItem[0]._agreement.interactionStatus,
                                        DateOfLastSubscriptionStatus = item._customer._customerAccount[0]._partyRole[0]._partyRoleProductOffering[0]._agreementItem[0]._agreement.interactionDate.startDateTime,
                                        CoActivated = item._customer._customerAccount[0]._partyRole[0]._partyRoleProductOffering[0]._agreementItem[0]._agreement.interactionDate.endDateTime.ToString()
                                    };
                                    lista.Add(entidadRetrieve);
                                }
                                retrieveSubscriptionsResponse = new ENTITIES_RETRIEVE.RetrieveSubscriptionResponse()
                                {
                                    Subscriptions = lista
                                };
                            }
                            Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.Transaction, "End a RetrieveSubscription Capa Data, Parametro de salida: responseStatus.status: " + retrieveSubscriptionsResponseType.responseStatus.status + " -> 0 = Ok, 1=Error");
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.Transaction, "End a RetrieveSubscription Capa Data, Parametro de salida: retrieveSubscriptionsResponseType: null");
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(retrieveSubscriptionsRequest.Audit.Session, retrieveSubscriptionsRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return retrieveSubscriptionsResponse;
        }
        #endregion

        #region CustomerManagementDP - GetCustomerInfoDP
        /// <summary>
        /// Método(consume servicio DataPower) que obtiene el tipo de la cuenta-tipo de cliente(Masivo-Corporativo).
        /// </summary>
        /// <param name="CustomerInfoRequest">objCustomerInfoRequests</param>
        /// <returns>objCustomerInfoResponse contiene: CustomerType.</returns> 
        public static ENTITIES_CUSTOMERINFO.CustomerInfoResponse GetCustomerInfoDP(ENTITIES_CUSTOMERINFO.CustomerInfoRequest objCustomerInfoRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objCustomerInfoRequest.Audit.Session,
           "Transaccion: " + objCustomerInfoRequest.Audit.Transaction,
           string.Format("Capa Data-Metodo: {0}, CustomerId:{1},DocumentType{2}, DocumentNumber{3}",
                           "GetCustomerInfoDP", objCustomerInfoRequest.CustomerId, objCustomerInfoRequest.DocumentType, objCustomerInfoRequest.DocumentNumber));



            CUSTOMER_INFO_DP.GetCustomerInfoResponseType responseCustomerInfo = new CUSTOMER_INFO_DP.GetCustomerInfoResponseType();
            CUSTOMER_INFO_DP.HeaderResponseType objHeaderResponseType = new CUSTOMER_INFO_DP.HeaderResponseType();
            ENTITIES_CUSTOMERINFO.CustomerInfoResponse objCustomerInfoResponse = null;

            string userAplicacion = "";
            string password = "";
            var audit = objCustomerInfoRequest.Audit;
            string strKey = KEY.AppSettings("strUserWSProd") + "|" + KEY.AppSettings("strPasswordWSProd");
            try
            {

                CUSTOMER_INFO_DP.GetCustomerInfoRequestType requestCustomerInfo = new CUSTOMER_INFO_DP.GetCustomerInfoRequestType()
                {
                    _customerAccount = new CUSTOMER_INFO_DP.CustomerAccount()
                    {
                        _customer = new CUSTOMER_INFO_DP.Customer()
                        {
                            ID = objCustomerInfoRequest.CustomerId,
                            _party = new CUSTOMER_INFO_DP.Party()
                            {
                                partyId = objCustomerInfoRequest.DocumentNumber,
                                _partyExtension = new CUSTOMER_INFO_DP.PartyExtension()
                                {
                                    identificationType = objCustomerInfoRequest.DocumentType
                                }
                            }
                        }
                    }
                };
                CUSTOMER_INFO_DP.HeaderRequest objHeaderRequest = new CUSTOMER_INFO_DP.HeaderRequest()
                {
                    channel = objCustomerInfoRequest.Audit.ApplicationName,
                    idApplication = objCustomerInfoRequest.Audit.IPAddress,
                    userApplication = objCustomerInfoRequest.Audit.UserName,
                    userSession = objCustomerInfoRequest.Audit.UserName,
                    idESBTransaction = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    idBusinessTransaction = objCustomerInfoRequest.Audit.Transaction,
                    startDate = DateTime.Now,
                    additionalNode = new object(),
                    startDateSpecified = true
                };
                CUSTOMER_INFO_DP.HeaderRequestType objHeaderRequestType = new CUSTOMER_INFO_DP.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSRestricTetheVeloc"),
                    country = KEY.AppSettings("strCountryWSRestricTetheVeloc"),
                    dispositivo = audit.IPAddress,
                    language = KEY.AppSettings("strLanguageWSRestricTetheVeloc"),
                    modulo = KEY.AppSettings("strModuloColiving"),
                    msgType = KEY.AppSettings("strMsgTypeWSRestricTetheVeloc"),
                    operation = KEY.AppSettings("strOperationGerCustomerInfo"),
                    pid = audit.Transaction,
                    system = KEY.AppSettings("NombreAplicacion"),
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    userId = audit.UserName,
                    VarArg = new CUSTOMER_INFO_DP.ArgType[1],
                    wsIp = audit.IPAddress
                };
                objHeaderRequestType.VarArg[0] = new CUSTOMER_INFO_DP.ArgType() { key = string.Empty, value = string.Empty };

                var result = COMMON_DATA.Common.IsOkGetKeyEncrypted(audit.Session, audit.Transaction, strKey, audit.IPAddress, audit.IPAddress, audit.UserName, audit.ApplicationName, out  userAplicacion, out password);

                if (result)
                {
                    using (new OperationContextScope(Configuration.ServiceConfiguration.CUSTOMER_INFODP.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(audit.Transaction, userAplicacion, password));

                        Claro.Web.Logging.ExecuteMethod<CUSTOMER_INFO_DP.HeaderResponse>
                       (audit.Session, audit.Transaction, () =>
                       {
                           return Configuration.ServiceConfiguration.CUSTOMER_INFODP.getCustomerInfo(
                               objHeaderRequest,
                               objHeaderRequestType,
                               requestCustomerInfo,
                               out objHeaderResponseType,
                               out responseCustomerInfo);
                       });
                        Claro.Web.Logging.Info(audit.Session, audit.Transaction,
                      String.Format("CUSTOMER_INFODP: HeaderResponse DataPower -> Status.code: {0}, Status.message: {1}", objHeaderResponseType != null ? objHeaderResponseType.Status.code : "NULO", objHeaderResponseType != null ? objHeaderResponseType.Status.message : "NULO"));

                    }
                    if (responseCustomerInfo != null)
                    {
                        if (responseCustomerInfo.responseStatus != null)
                        {
                            if (responseCustomerInfo.responseStatus.status == Claro.Constants.NumberZero)
                            {
                                objCustomerInfoResponse = new ENTITIES_CUSTOMERINFO.CustomerInfoResponse();
                                objCustomerInfoResponse.Status = responseCustomerInfo.responseStatus.status;
                                objCustomerInfoResponse.CodeResponse = responseCustomerInfo.responseStatus.codeResponse;
                                objCustomerInfoResponse.DescriptionResponse = responseCustomerInfo.responseStatus.descriptionResponse;
                                objCustomerInfoResponse.DateTime = responseCustomerInfo.responseStatus.date;
                                if (responseCustomerInfo.responseData != null && responseCustomerInfo.responseData._customerAccount != null)
                                {
                                    objCustomerInfoResponse.CustomerType = responseCustomerInfo.responseData._customerAccount._customer._party._partyUser[0]._partyUserExtension.type;
                                }
                            }
                            Claro.Web.Logging.Info("IdSession: " + objCustomerInfoRequest.Audit.Session, "Transaccion: " + objCustomerInfoRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseCustomerInfo: " + responseCustomerInfo.responseStatus.status + " -> 0 = Ok, 1=Error");
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info("IdSession: " + objCustomerInfoRequest.Audit.Session, "Transaccion: " + objCustomerInfoRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseCustomerInfo: null");
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }

            return objCustomerInfoResponse;
        }

        #endregion

        #region OST - Listar OST
        /// <summary>
        /// Método que obtiene una lista de los OST de la línea
        /// </summary>
        /// <param name="ListOSTRequest">Objeto de consulta</param>
        /// <returns>Devuelve listado de OST</returns>
        public static ENTITIES_OST.ListOSTResponse getListOST_Legacy(ListOSTRequest objListOSTRequest)
        {
            Claro.SIACU.Entity.Coliving.getListOST.ListOSTResponse objListOSTResponse = new ListOSTResponse();
            List<Claro.SIACU.Entity.Coliving.getListOST.TechnicalServicesOSTType> objListTechnicalServicesOSTType = null;
            try
            {
                DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("P_ID_BUSCA", DbType.String, ParameterDirection.Input, objListOSTRequest.IdBusca),
                    new DbParameter("P_ID_CRITERIO", DbType.String, ParameterDirection.Input, objListOSTRequest.IdCriterio),
                    new DbParameter("P_ID_ESTADO", DbType.Int32, ParameterDirection.Input, objListOSTRequest.IdEstado),
                    new DbParameter("P_ID_CAC", DbType.String, ParameterDirection.Input, objListOSTRequest.IdCAC),
                    new DbParameter("P_RESULTADO", DbType.Object, ParameterDirection.Output)
                };
                objListTechnicalServicesOSTType = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Coliving.getListOST.TechnicalServicesOSTType>>(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_BUSCAR_OST, parameters);
                objListOSTResponse.TechnicalServicesOSTType = objListTechnicalServicesOSTType;
                objListOSTResponse.CodeResponse = Constants.ZeroNumber;
            }
            catch (Exception ex)
            {
                objListOSTResponse.CodeResponse = Constants.TwoNumber;
                objListOSTResponse.DescriptionResponse = ex.Message;
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
                throw;
            }

            return objListOSTResponse;
        }

        /// <summary>
        /// Método que obtiene una lista de los OST de ONE
        /// </summary>
        /// <param name="ListOSTRequest">Objeto de consulta</param>
        /// <returns>Devuelve listado de OST</returns>
        public static ENTITIES_OST.ListOSTResponse getListOST_One(ListOSTRequest objListOSTRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session,
           "Transaccion: " + objListOSTRequest.Audit.Transaction,
           string.Format("Capa Data-Metodo: {0}, IdBusca:{1}, IdCAC:{2}, IdCriterio:{3}, IdEstado:{4}",
                           "getListOST_One", objListOSTRequest.IdBusca, objListOSTRequest.IdCAC, objListOSTRequest.IdCriterio, objListOSTRequest.IdEstado));

            TECHNICAL_SERVICES_BYCUSTOMER.GetTechnicalServicesByCustomerResponseType responseTechnicalServices = new TECHNICAL_SERVICES_BYCUSTOMER.GetTechnicalServicesByCustomerResponseType();
            TECHNICAL_SERVICES_BYCUSTOMER.HeaderResponseType objHeaderResponseType = new TECHNICAL_SERVICES_BYCUSTOMER.HeaderResponseType();

            ENTITIES_OST.ListOSTResponse objTechnicalServicesResponse = new ENTITIES_OST.ListOSTResponse();

            string userAplicacion = "";
            string password = "";
            var audit = objListOSTRequest.Audit;
            string strKey = KEY.AppSettings("strRegeditWSEricsson");

            try
            {

                TECHNICAL_SERVICES_BYCUSTOMER.GetTechnicalServicesByCustomerRequestType requestTechnicalServices = new TECHNICAL_SERVICES_BYCUSTOMER.GetTechnicalServicesByCustomerRequestType()
                {
                    _individualIdentification = new TECHNICAL_SERVICES_BYCUSTOMER.IndividualIdentification()
                    {
                        _individual = new TECHNICAL_SERVICES_BYCUSTOMER.Individual()
                        {
                            _partyExtension = new TECHNICAL_SERVICES_BYCUSTOMER.PartyExtension()
                            {
                                identificationType = objListOSTRequest.IdCriterio,

                            },
                            partyId = objListOSTRequest.IdBusca
                        }
                    }
                };


                TECHNICAL_SERVICES_BYCUSTOMER.HeaderRequest objHeaderRequest = new TECHNICAL_SERVICES_BYCUSTOMER.HeaderRequest()
                {
                    channel = objListOSTRequest.Audit.ApplicationName,
                    idApplication = objListOSTRequest.Audit.IPAddress,
                    userApplication = objListOSTRequest.Audit.UserName,
                    userSession = objListOSTRequest.Audit.UserName,
                    idESBTransaction = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    idBusinessTransaction = objListOSTRequest.Audit.Transaction,
                    startDate = DateTime.Now,
                    additionalNode = new object(),
                    startDateSpecified = true
                };

                TECHNICAL_SERVICES_BYCUSTOMER.HeaderRequestType objHeaderRequestType = new TECHNICAL_SERVICES_BYCUSTOMER.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSTechnicalServices"),
                    country = KEY.AppSettings("strCountryWSTechnicalServices"),
                    dispositivo = audit.IPAddress,
                    language = KEY.AppSettings("strLanguageWSTechnicalServices"),
                    modulo = KEY.AppSettings("strModuloColiving"),
                    msgType = KEY.AppSettings("strMsgTypeWSTechnicalServices"),
                    operation = KEY.AppSettings("strOperationWSTechnicalServices"),
                    pid = audit.Transaction,
                    system = KEY.AppSettings("NombreAplicacion"),
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    userId = audit.UserName,
                    VarArg = new TECHNICAL_SERVICES_BYCUSTOMER.ArgType[1],
                    wsIp = audit.IPAddress
                };
                objHeaderRequestType.VarArg[0] = new TECHNICAL_SERVICES_BYCUSTOMER.ArgType() { key = string.Empty, value = string.Empty };

                var result = COMMON_DATA.Common.IsOkGetKey(audit.Session, audit.Transaction, strKey, audit.IPAddress, audit.IPAddress, audit.UserName, audit.ApplicationName, out  userAplicacion, out password);

                if (result)
                {
                    using (new OperationContextScope(Configuration.ServiceConfiguration.TECHNICAL_SERVICES_BYCUSTOMER.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(audit.Transaction, userAplicacion, password));

                        Claro.Web.Logging.ExecuteMethod<TECHNICAL_SERVICES_BYCUSTOMER.HeaderResponse>
                       (audit.Session, audit.Transaction, () =>
                       {
                           return Configuration.ServiceConfiguration.TECHNICAL_SERVICES_BYCUSTOMER.getTechnicalServicesByCustomer(
                               objHeaderRequest,
                               objHeaderRequestType,
                               requestTechnicalServices,
                               out objHeaderResponseType,
                               out responseTechnicalServices);
                       });
                        Claro.Web.Logging.Info(audit.Session, audit.Transaction,
                      String.Format("getListOST_One: HeaderResponse DataPower -> Status.code: {0}, Status.message: {1}", objHeaderResponseType != null ? objHeaderResponseType.Status.code : "NULO", objHeaderResponseType != null ? objHeaderResponseType.Status.message : "NULO"));

                    }
                    if (responseTechnicalServices != null)
                    {
                        if (responseTechnicalServices.responseStatus != null)
                        {
                            if (responseTechnicalServices.responseStatus.status == Claro.Constants.NumberZero)
                            {
                                objTechnicalServicesResponse.Status = responseTechnicalServices.responseStatus.status;
                                objTechnicalServicesResponse.CodeResponse = responseTechnicalServices.responseStatus.codeResponse;
                                objTechnicalServicesResponse.DescriptionResponse = responseTechnicalServices.responseStatus.descriptionResponse;
                                objTechnicalServicesResponse.Date = responseTechnicalServices.responseStatus.date;
                                objTechnicalServicesResponse.TechnicalServicesOSTType = new List<TechnicalServicesOSTType>();
                                if (responseTechnicalServices.responseData != null)
                                {
                                    foreach (var item in responseTechnicalServices.responseData)
                                    {
                                        TechnicalServicesOSTType objTechnicalServicesOSTType = new TechnicalServicesOSTType();

                                        objTechnicalServicesOSTType.Cac = item._organization._organizationExtension.ID;
                                        objTechnicalServicesOSTType.FechaGeneracion = "";
                                        objTechnicalServicesOSTType.idOst = item._customerOrder.ID;
                                        objTechnicalServicesOSTType.Imei = item._customerOrder._customerAccount[0]._partyRole[0]._service[0].ID; 
                                        objTechnicalServicesResponse.TechnicalServicesOSTType.Add(objTechnicalServicesOSTType);
                                    }
                                }
                            }
                            Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseCustomerInfo: " + responseTechnicalServices.responseStatus.status + " -> 0 = Ok, 1=Error");
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseCustomerInfo: null");
                    }
                }
            }
            catch (Exception ex)
            {
                objTechnicalServicesResponse.CodeResponse = Constants.TwoNumber;
                objTechnicalServicesResponse.DescriptionResponse = ex.Message;
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }

            return objTechnicalServicesResponse;
        }




        /// <summary>
        /// Método que obtiene el detalle de una OST de ONE
        /// </summary>
        /// <param name="ListOSTRequest">Objeto de consulta</param>
        /// <returns>Devuelve el detalle de una OST</returns>
        public static ENTITIES_OST.ListOSTResponse getListOSTDetails_One(ListOSTRequest objListOSTRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session,
           "Transaccion: " + objListOSTRequest.Audit.Transaction,
           string.Format("Capa Data-Metodo: {0}, IdBusca:{1}, IdCAC:{2}, IdCriterio:{3}, IdEstado:{4}",
                           "getListOSTDetails_One", objListOSTRequest.IdBusca, objListOSTRequest.IdCAC, objListOSTRequest.IdCriterio, objListOSTRequest.IdEstado));

            TECHNICAL_SERVICES_DETAILS.GetTechnicalServiceDetailsResponseType responseTechnicalServices = new TECHNICAL_SERVICES_DETAILS.GetTechnicalServiceDetailsResponseType();
            TECHNICAL_SERVICES_DETAILS.HeaderResponseType objHeaderResponseType = new TECHNICAL_SERVICES_DETAILS.HeaderResponseType();

            ENTITIES_OST.ListOSTResponse objTechnicalServicesResponse = new ENTITIES_OST.ListOSTResponse();

            string userAplicacion = "";
            string password = "";
            var audit = objListOSTRequest.Audit;
            string strKey = KEY.AppSettings("strRegeditWSEricsson");

            try
            {

                TECHNICAL_SERVICES_DETAILS.GetTechnicalServiceDetailsRequestType requestTechnicalServices = new TECHNICAL_SERVICES_DETAILS.GetTechnicalServiceDetailsRequestType()
                {
                    _customerOrder = new TECHNICAL_SERVICES_DETAILS.CustomerOrder()
                    {
                        ID = objListOSTRequest.IdBusca
                    }
                };


                TECHNICAL_SERVICES_DETAILS.HeaderRequest objHeaderRequest = new TECHNICAL_SERVICES_DETAILS.HeaderRequest()
                {
                    channel = objListOSTRequest.Audit.ApplicationName,
                    idApplication = objListOSTRequest.Audit.IPAddress,
                    userApplication = objListOSTRequest.Audit.UserName,
                    userSession = objListOSTRequest.Audit.UserName,
                    idESBTransaction = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    idBusinessTransaction = objListOSTRequest.Audit.Transaction,
                    startDate = DateTime.Now,
                    additionalNode = new object(),
                    startDateSpecified = true
                };

                TECHNICAL_SERVICES_DETAILS.HeaderRequestType objHeaderRequestType = new TECHNICAL_SERVICES_DETAILS.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSTechnicalServices"),
                    country = KEY.AppSettings("strCountryWSTechnicalServices"),
                    dispositivo = audit.IPAddress,
                    language = KEY.AppSettings("strLanguageWSTechnicalServices"),
                    modulo = KEY.AppSettings("strModuloColiving"),
                    msgType = KEY.AppSettings("strMsgTypeWSTechnicalServices"),
                    operation = KEY.AppSettings("strOperationWSTechnicalServicesDetails"),
                    pid = audit.Transaction,
                    system = KEY.AppSettings("NombreAplicacion"),
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    userId = audit.UserName,
                    VarArg = new TECHNICAL_SERVICES_DETAILS.ArgType[1],
                    wsIp = audit.IPAddress
                };
                objHeaderRequestType.VarArg[0] = new TECHNICAL_SERVICES_DETAILS.ArgType() { key = string.Empty, value = string.Empty };

                var result = COMMON_DATA.Common.IsOkGetKey(audit.Session, audit.Transaction, strKey, audit.IPAddress, audit.IPAddress, audit.UserName, audit.ApplicationName, out  userAplicacion, out password);

                if (result)
                {
                    using (new OperationContextScope(Configuration.ServiceConfiguration.TECHNICAL_SERVICES_DETAILS.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(audit.Transaction, userAplicacion, password));

                         Claro.Web.Logging.ExecuteMethod<TECHNICAL_SERVICES_DETAILS.HeaderResponse>
                        (audit.Session, audit.Transaction, () =>
                        {
                            return Configuration.ServiceConfiguration.TECHNICAL_SERVICES_DETAILS.getTechnicalServiceDetails(
                                objHeaderRequest,
                                objHeaderRequestType,
                                requestTechnicalServices,
                                out objHeaderResponseType,
                                out responseTechnicalServices);
                        });
                        Claro.Web.Logging.Info(audit.Session, audit.Transaction,
                      String.Format("getListOST_One: HeaderResponse DataPower -> Status.code: {0}, Status.message: {1}", objHeaderResponseType != null ? objHeaderResponseType.Status.code : "NULO", objHeaderResponseType != null ? objHeaderResponseType.Status.message : "NULO"));

                    }
                    if (responseTechnicalServices != null)
                    {
                        if (responseTechnicalServices.responseStatus != null)
                        {
                            if (responseTechnicalServices.responseStatus.status == Claro.Constants.NumberZero)
                            {
                                objTechnicalServicesResponse.Status = responseTechnicalServices.responseStatus.status;
                                objTechnicalServicesResponse.CodeResponse = responseTechnicalServices.responseStatus.codeResponse;
                                objTechnicalServicesResponse.DescriptionResponse = responseTechnicalServices.responseStatus.descriptionResponse;
                                objTechnicalServicesResponse.Date = responseTechnicalServices.responseStatus.date;
                                objTechnicalServicesResponse.TechnicalServicesOSTType = new List<TechnicalServicesOSTType>();
                                if (responseTechnicalServices.responseData != null)
                                {
                                    TechnicalServicesOSTType objTechnicalServicesOSTType = new TechnicalServicesOSTType();
                                    objTechnicalServicesOSTType.Marca = responseTechnicalServices.responseData._physicalDevice._resourceSpecification._productSpecification[0].brand; 
                                    objTechnicalServicesOSTType.Modelo = responseTechnicalServices.responseData._physicalDevice._physicalResourceRole[0]._physicalResourceSpec._physicalResourceSpecAttributes.modelNumber; 
                                    objTechnicalServicesResponse.TechnicalServicesOSTType.Add(objTechnicalServicesOSTType);
                                }
                            }
                            Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseCustomerInfo: " + responseTechnicalServices.responseStatus.status + " -> 0 = Ok, 1=Error");
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info("IdSession: " + objListOSTRequest.Audit.Session, "Transaccion: " + objListOSTRequest.Audit.Transaction, "End a GetSearchCustomer Capa Data Parametro de salida: responseCustomerInfo: null");
                    }
                }
            }
            catch (Exception ex)
            {
                objTechnicalServicesResponse.CodeResponse = Constants.TwoNumber;
                objTechnicalServicesResponse.DescriptionResponse = ex.Message;
                Claro.Web.Logging.Error(objListOSTRequest.Audit.Session, objListOSTRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }

            return objTechnicalServicesResponse;
        }

        #endregion

    }
}
