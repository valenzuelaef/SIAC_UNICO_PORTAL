using Claro.Data;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Dashboard;
using Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo;
using Claro.SIACU.Entity.Dashboard.Board.GetCustomerName;
using Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME;
using Claro.SIACU.ProxyService.SIAC.Segment;
using Claro.SIACU.ProxyService.SIACU.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using AUDIT_CREDENTIALS = Claro.SIACU.ProxyService.Audit.Redirect;
using COMMON_CONSULTCLIENT = Claro.SIACU.ProxyService.SIACU.Customers;
using COMMON_HLRUDB = Claro.SIACU.ProxyService.SIACPost.HLRUDB;
using ENTITYS = Claro.SIACU.Entity.Dashboard;
using KEY = Claro.ConfigurationManager;
using System.Collections;
using Claro.SIACU.ProxyService.SIAC.Seguridad;
using Claro.Data.Configuration;
using System.Configuration;
using AUDIT_SECURITY = Claro.SIACU.ProxyService.SIACSecurity.Permissions;
using CONSULTA_SECURITY = Claro.SIACU.ProxyService.SIAC.Seguridad;
using COMMON_ACTIVARVOLTEWS = Claro.SIACU.ProxyService.SIAC.ActivarVOLTE;
using EntityClarify = Claro.SIACU.Entity.Common.GetParamaterClarify.GetDescriptions;
namespace Claro.SIACU.Data.Dashboard
{
    public class Common
    {

        private System.Timers.Timer tmrController = null;
        private Thread thrProcess = null;
        private int intTimeTranscurridoFtp = Claro.Constants.NumberZero;
        private int intTimeTranscurridoFs = Claro.Constants.NumberZero;
        private int intTimeOutFtp = Claro.Constants.NumberZero;
        private int intTimeOutFs = Claro.Constants.NumberZero;

        /// <summary>
        /// Método que obtiene si el cliente tiene afiliacion de comprobante electrónico.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentType">Tipo de documento DNI, Carnet Extranjería, Pasaporte</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve objeto MembershipElectronic con información de afiliación de comprobante electrónico.</returns>
        public static ENTITYS.MembershipElectronic GetMembershipVoucherElectronic(string strIdSession, string strTransaction, string strDocumentType, string strDocumentNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_TIPO_DOCUMENTO", DbType.String, 255, ParameterDirection.Input, strDocumentType),
                new DbParameter("N_NRO_DOCUMENTO", DbType.String, 255, ParameterDirection.Input, strDocumentNumber),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("V_CODRES", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("V_DESRES", DbType.String, 255, ParameterDirection.Output)
            };

            ENTITYS.MembershipElectronic objMembershipElectronic = DbFactory.ExecuteReader<ENTITYS.MembershipElectronic>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBT_AFILIACION, parameters);

            return objMembershipElectronic;
        }

        /// <summary>
        /// Método que obtiene el segmento del cliente.
        /// </summary>
        /// <param name="oPerson">Entidad persona</param>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strUsrApplication">Usuario de aplicación</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strApplication">Aplicación</param>
        /// <returns>>Devuelve cadena con el segmento del cliente.</returns>
        public static string GetSegmentCustomerQuery(ENTITYS.Person oPerson, string strIdSession, string strUsrApplication, string strIpApplication, string strIdTransaction, string strApplication)
        {
            string strSegmentResponse;
            string strCodeResponse = "";
            string strMessageResponse = "";
            string strSegment = "";
            string strSegmentDefault;
            string strNameCustomer = "";
            string strMessage1 = "";
            string strMessage2 = "";
            string strMessage3 = "";
            string strMessage4 = "";

            try
            {
                strSegmentDefault = KEY.AppSettings("strSegment");
            }
            catch (Exception ex)
            {
                strSegmentDefault = "";
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            AuditType oAuditType = new AuditType();
            oAuditType.usrApp = string.IsNullOrEmpty(strUsrApplication) ? "0" : strUsrApplication;
            oAuditType.ipAplicacion = string.IsNullOrEmpty(strIpApplication) ? "0" : strIpApplication;
            oAuditType.aplicacion = string.IsNullOrEmpty(strApplication) ? "SIACUNICO" : strApplication;
            oAuditType.idTransaccion = string.IsNullOrEmpty(strIdTransaction) ? "0" : strIdTransaction;


            Claro.Web.Logging.Info(strIdSession, strIdTransaction, "Parametro de entrada consultaSegmento");
            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("TIPO_DOC_SEG {0} - NRO_DOC_SEG {1}", oPerson.TIPO_DOC, oPerson.NRO_DOC));
            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("LONG_NRO_DOC_SEG {0}", oPerson.LONG_NRO_DOC));

            Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, Configuration.ServiceConfiguration.PREPAID_SEGMENT, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_SEGMENT.consultaSegmento(oAuditType, oPerson.TIPO_DOC, oPerson.NRO_DOC, oPerson.LONG_NRO_DOC, out strCodeResponse, out strMessageResponse, out strSegment, out strNameCustomer, out strMessage1, out strMessage2, out strMessage3, out strMessage4);
            });
            Claro.Web.Logging.Info(strIdSession, strIdTransaction, "Parametro de salid consultaSegmento");
            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("STR_SEGMENTO_SEG {0}", strSegment));

            strSegmentResponse = (string.IsNullOrEmpty(strSegment) ? strSegmentDefault : strSegment);

            return strSegmentResponse;
        }

        /// <summary>
        /// Método que obtiene una lista de los clientes históricos por medio del número de documento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strDocumentType">Tipo de documento</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve listado de objetos Segment con información de segmento.</returns>
        public static List<ENTITYS.Segment> GetSegments(string strIdSession, string strIdTransaction, string strDocumentType, string strDocumentNumber, out string CodeResponse)
        {
            string strMessageResponse;
            string strCodeResponse = "0";

            List<ENTITYS.Segment> listSegment = null;
            AuditType oAuditType = new AuditType();
            ListaConsultaSegmHistorico[] oListaConsultaSegmHistorico = new ListaConsultaSegmHistorico[0];

            Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, Configuration.ServiceConfiguration.PREPAID_SEGMENT, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_SEGMENT.consultaSegmHistorico(oAuditType, strDocumentType, strDocumentNumber, out strCodeResponse, out strMessageResponse, out oListaConsultaSegmHistorico);
            });

            if (strCodeResponse == "0" && oListaConsultaSegmHistorico != null)
            {
                listSegment = new List<ENTITYS.Segment>();
                foreach (var item in oListaConsultaSegmHistorico)
                {
                    listSegment.Add(new ENTITYS.Segment()
                    {
                        NRO_DOC = item.nroDocumento,
                        SEGMENTO = item.segmento,
                        ULTIMAACTUALIZACION = item.fecInicio
                    });
                }
            }

            CodeResponse = strCodeResponse;

            return listSegment;
        }

        /// <summary>
        /// Método que obtiene los datos del cliente.
        /// </summary>
        /// <param name="oAudit">Objeto auditoría.</param>
        /// <param name="strSearchType">Tipo de búsqueda</param>
        /// <param name="strSearchValue">Valor de búsqueda</param>
        /// <returns>Devuelve objeto CustomerInfoResponse con información del cliente.</returns>
        public static CustomerInfoResponse GetCustomerInfo(Claro.Entity.AuditRequest oAudit, string strSearchType, string strSearchValue)
        {

            COMMON_CONSULTCLIENT.ConsultarClienteUnificadoResponse oConsultarClienteUnificadoResponse = Claro.Web.Logging.ExecuteMethod<COMMON_CONSULTCLIENT.ConsultarClienteUnificadoResponse>(oAudit.Session, oAudit.Transaction, Configuration.ServiceConfiguration.COMMON_CONSULTCLIENT, () =>
            {
                return Configuration.ServiceConfiguration.COMMON_CONSULTCLIENT.consultarClienteUnificado(new COMMON_CONSULTCLIENT.ConsultarClienteUnificadoRequest()
                {
                    tipoBusqueda = strSearchType,
                    valorBusqueda = strSearchValue,
                    auditRequest = new auditRequestType()
                    {
                        usuarioAplicacion = oAudit.UserName,
                        ipAplicacion = oAudit.IPAddress,
                        nombreAplicacion = oAudit.ApplicationName,
                        idTransaccion = oAudit.Transaction
                    },
                    listaRequestOpcional = new parametrosTypeObjetoOpcional[1]
                });
            });

            CustomerInfoResponse oGetCustomerInfoResponse = new CustomerInfoResponse()
            {
                ErrorMsg = oConsultarClienteUnificadoResponse.auditResponse.mensajeRespuesta,
                CodeError = oConsultarClienteUnificadoResponse.auditResponse.codigoRespuesta
            };

            if (Int32.Parse(oGetCustomerInfoResponse.CodeError).Equals(Claro.Constants.NumberTwo) || Int32.Parse(oGetCustomerInfoResponse.CodeError) < Claro.Constants.NumberZero)
            {
                throw new Claro.MessageException(Claro.SIACU.Constants.MessageNotQueryClientUnified);
            }
            if (oConsultarClienteUnificadoResponse.listaClientesResponse != null)
            {
                foreach (var item in oConsultarClienteUnificadoResponse.listaClientesResponse)
                {
                    oGetCustomerInfoResponse.ListPerson.Add(new Claro.SIACU.Entity.Dashboard.Person()
                    {
                        NRO_DOC = item.docIdentidad,
                        DESCRIPCION = item.descripcion,
                        TIPO_DOC = item.tipoDocIdentidad
                    });
                }


            }

            return oGetCustomerInfoResponse;
        }

        /// <summary>
        /// Método que obtiene el nombre del cliente.
        /// </summary>
        /// <param name="oAudit">Objeto auditoría</param>
        /// <param name="strSearchType">Tipo de búsqueda</param>
        /// <param name="strSearchValue">Valor de búsqueda</param>
        /// <returns>Devuelve objeto CustomerNameResponse con información de cliente.</returns>
        public static CustomerNameResponse GetCustomerName(Claro.Entity.AuditRequest oAudit, string strSearchType, string strSearchValue)
        {
            Claro.SIACU.ProxyService.SIACU.Customers.auditRequestType objAuditType = new ProxyService.SIACU.Customers.auditRequestType()
            {
                usuarioAplicacion = oAudit.UserName,
                ipAplicacion = oAudit.IPAddress,
                nombreAplicacion = oAudit.ApplicationName,
                idTransaccion = oAudit.Transaction
            };

            COMMON_CONSULTCLIENT.ConsultarClienteUnificadoResponse objConsultarClienteUnificadoResponse = Claro.Web.Logging.ExecuteMethod<COMMON_CONSULTCLIENT.ConsultarClienteUnificadoResponse>(oAudit.Session, oAudit.Transaction, Configuration.ServiceConfiguration.COMMON_CONSULTCLIENT, () =>
            {
                return Configuration.ServiceConfiguration.COMMON_CONSULTCLIENT.consultarClienteUnificado(new COMMON_CONSULTCLIENT.ConsultarClienteUnificadoRequest()
                    {
                        tipoBusqueda = strSearchType,
                        valorBusqueda = strSearchValue,
                        auditRequest = objAuditType,
                        listaRequestOpcional = new parametrosTypeObjetoOpcional[1]
                    });
            });

            if (objConsultarClienteUnificadoResponse.auditResponse.codigoRespuesta.Equals(Claro.Constants.NumberTwoString) || objConsultarClienteUnificadoResponse.auditResponse.codigoRespuesta.Equals(Claro.Constants.NumberOneNegativeString))
            {
                throw new Claro.MessageException(Claro.SIACU.Constants.MessageNotQueryDataClient);
            }
            CustomerNameResponse objCustomerNameResponse = new CustomerNameResponse()
            {
                ErrorMsg = objConsultarClienteUnificadoResponse.auditResponse.mensajeRespuesta,
                CodeError = objConsultarClienteUnificadoResponse.auditResponse.codigoRespuesta
            };

            if (objConsultarClienteUnificadoResponse.listaClientesResponse != null)
                foreach (var item in objConsultarClienteUnificadoResponse.listaClientesResponse)
                {
                    objCustomerNameResponse.ListPerson.Add(new ENTITYS.Person()
                    {
                        DESCRIPCION = item.descripcion,
                        TIPO_DOC = item.tipoDocIdentidad,
                        NRO_DOC = item.docIdentidad
                    });
                }

            return objCustomerNameResponse;

        }

        /// <summary>
        /// Método que obtiene una lista de los clientes Prepagos por nombre o razon social.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhone">Teléfono</param>
        /// <param name="strContract">Contrato</param>
        /// <param name="Straux">Valor auxiliar</param>
        /// <returns>Devuelve listado de objetos ProductType con información de tipos de productos.</returns>
        public static List<ENTITYS.ProductType> GetTypeProduct(string strIdSession, string strTransaction, string strPhone, string strContract, ref string Straux)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_NUMERO_TELEFONO", DbType.String, 50, ParameterDirection.Input, strPhone),
                new DbParameter("P_CONTRATO", DbType.String, 50, ParameterDirection.Input, strContract),
                new DbParameter("P_AUXILIAR", DbType.String, 500, ParameterDirection.Output),
                new DbParameter("P_CODIGO_SALIDA", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_MENSAJE_SALIDA", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
            List<ENTITYS.ProductType> lstProductType = DbFactory.ExecuteReader<List<ENTITYS.ProductType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SIUNSS_GET_TYPE_PRODUCT, parameters);
            if (parameters[2].Value != null)
            {
                Straux = parameters[2].Value.ToString();
            }

            return lstProductType;
        }
        public static List<ENTITYS.ProductType> GetTypeProductTobe(Claro.Entity.AuditRequest audit, Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Request.request request, ref string Straux)
        {

            List<ENTITYS.ProductType> lstProductType = null;

            Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_TIPO_PRODUCTO_TOBE, audit, request, false);

            if (response != null && response.obtenerTipoProductoResponse != null &&
                response.obtenerTipoProductoResponse.responseAudit != null &&
                response.obtenerTipoProductoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.obtenerTipoProductoResponse.responseData != null
                )
            {
                Straux = response.obtenerTipoProductoResponse.responseData.lineaContrato;
                lstProductType = new List<ProductType>();
                foreach (var item in response.obtenerTipoProductoResponse.responseData.listaTipoProducto)
                {
                    lstProductType.Add(new ENTITYS.ProductType()
                    {
                        CODIGO_PRODUCTO = item.codigoProducto,
                        TIPO_PRODUCTO = item.tipoProducto
                    });
                }
            }
            return lstProductType;
        }
        public static List<ENTITYS.ProductType> GetTypeProductDatTobe(Claro.Entity.AuditRequest audit, Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request request, string TypeSearch, ref string Straux, out Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional)
        {
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response.response.CaracteristicaAdicional CaractAdi = null;
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat oOptional = null;
            Claro.Web.Logging.Error(audit.Session, audit.Transaction, "Entro a capa Data metodo : GetTypeProductDatTobe ");
            Claro.Web.Logging.Error(audit.Session, audit.Transaction, string.Format("Entro a capa Data metodo : GetTypeProductDatTobe  , TypeSearch : {0}", TypeSearch));
            List<ENTITYS.ProductType> lstProductType = null;
            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_TIPO_PRODUCTO_DAT_TOBE, audit, request, false);

                if (response != null && response.responseStatus != null &&
                    response.responseStatus.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.responseData.contrato != null &&
                    response.responseData.contrato.Count > 0 && response.responseData.contrato[0].ofertaProducto != null && response.responseData.contrato[0].ofertaProducto[0].producto != null && response.responseData.contrato[0].ofertaProducto[0].producto.caracteristicaProducto != null && response.responseData.contrato[0].ofertaProducto[0].producto.caracteristicaProducto.tecnologia != null
                    )
                {

                    Straux = response.responseData.contrato[0].ofertaProducto[0].producto.recursoLogico[0].numeroLinea;
                    var idPublicoContrato = response.responseData.contrato[0].idPublicoContrato;
                    var nroCuenta = response.responseData.contrato[0].cuentaFacturacion.cuentaCliente.nroCuenta;
                    var idCliente = response.responseData.contrato[0].cuentaFacturacion.cuentaCliente.idCliente;
                    var coidDat = response.responseData.contrato[0].idContrato;
                    var idStateLine = response.responseData.contrato[0].estadoContrato;
                    CaractAdi = response.responseData.contrato[0].caracteristicaAdicional[0];
                    oOptional = new ENTITYS.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat(idPublicoContrato, idCliente, nroCuenta, coidDat, idStateLine, CaractAdi);

                    lstProductType = new List<ProductType>();
                    lstProductType.Add(new ENTITYS.ProductType()
                    {
                        CODIGO_PRODUCTO = Claro.Constants.NumberZeroString + Claro.Constants.NumberOneString,
                        TIPO_PRODUCTO = response.responseData.contrato[0].ofertaProducto[0].producto.caracteristicaProducto.tecnologia.ToUpper()
                    });



                }
            }
            catch (Exception ex)
            {
             
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw;
            }
            outOptional = oOptional;

            return lstProductType;
        }

        /// <summary>
        /// Método que obtiene una lista de los datos de las sesiones.
        /// </summary>
        /// <param name="audit">Objeto auditoría</param>
        /// <param name="strApplication">Aplicación</param>
        /// <param name="strOption">Opción</param>
        /// <param name="errorMsg">Mensaje de error</param>
        /// <param name="codError">Código de error</param>
        /// <returns>Devuelve listado de objetos Redirect con informacíón de las sesiones para redireccionar externamente.</returns>
        public static List<ENTITYS.Redirect> GetRedirectSession(Claro.Entity.AuditRequest audit, string strApplication, string strOption, out string errorMsg, out string codError)
        {
            List<ENTITYS.Redirect> listRedirect = null;

            AUDIT_CREDENTIALS.ListaSesionesObtenidas objSesiones = new AUDIT_CREDENTIALS.ListaSesionesObtenidas();
            AUDIT_CREDENTIALS.ListaResponseOpcional listResponseOpc = new AUDIT_CREDENTIALS.ListaResponseOpcional();
            AUDIT_CREDENTIALS.parametrosAuditRequest request = new AUDIT_CREDENTIALS.parametrosAuditRequest()
            {
                idTransaccion = audit.Transaction,
                ipAplicacion = audit.IPAddress,
                nombreAplicacion = audit.ApplicationName,
                usuarioAplicacion = audit.UserName
            };
            AUDIT_CREDENTIALS.ListaRequestOpcional OptionRequest = new AUDIT_CREDENTIALS.ListaRequestOpcional();

            try
            {
                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse = Claro.Web.Logging.ExecuteMethod<AUDIT_CREDENTIALS.parametrosAuditResponse>(audit.Session, audit.Transaction, Configuration.ServiceConfiguration.AUDIT_CREDENTIALS, () =>
                    {
                        return Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.obtenerSesiones(request, strOption, strApplication, OptionRequest, out objSesiones, out listResponseOpc);
                    }
                    );

                codError = objAuditResponse.codigoRespuesta;
                errorMsg = objAuditResponse.mensajeRespuesta;

                if (codError == Claro.Constants.NumberZeroString && objSesiones.Count > 0)
                {
                    ENTITYS.Redirect redirect;
                    listRedirect = new List<ENTITYS.Redirect>();
                    for (int i = 0; i < objSesiones.Count; i++)
                    {
                        redirect = new ENTITYS.Redirect();
                        redirect.option_key = objSesiones[i].opcionParametroId.ToString();
                        redirect.session_name = objSesiones[i].nombreSesion.ToString();
                        redirect.option_type = objSesiones[i].tipoOpcion.ToString();
                        if (!String.IsNullOrEmpty(objSesiones[i].propSession))
                        {
                            redirect.prop_session = objSesiones[i].propSession.ToString();
                        }
                        if (!String.IsNullOrEmpty(objSesiones[i].valorSession))
                        {
                            redirect.value_Session = objSesiones[i].valorSession.ToString();
                        }
                        listRedirect.Add(redirect);
                    }
                }
            }
            catch (TimeoutException ex)
            {
                codError = "";
                errorMsg = Claro.SIACU.Constants.MessageNotServicesLimitWait;
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (WebException ex)
            {
                codError = "";
                errorMsg = Claro.SIACU.Constants.MessageNotComunicationServerRemote;
                throw new Claro.MessageException(ex.Message.ToString());

            }

            catch (Exception ex)
            {
                codError = "";
                errorMsg = ex.Message;
            }

            return listRedirect;
        }

        /// <summary>
        /// Método que se genera con el servicio y registra la comunicación entre las páginas Redireccionadas, para ello se le agregará un parámetro de entrada que permita saber el nombre de la aplicación de destino.
        /// </summary>
        /// <param name="audit">Objeto auditoría</param>
        /// <param name="appDest">Aplicación destino</param>
        /// <param name="option">Opción</param>
        /// <param name="ipClient">Ip de cliente</param>
        /// <param name="ipServer">Ip de servidor</param>
        /// <param name="jsonParameters">Parámetros Json</param>
        /// <param name="Sequence">Secuencia</param>
        /// <param name="Url">Url</param>
        /// <returns>Devuelve secuencia y url para realizar la redirección.</returns>
        public static string InsertRedirectCommunication(Claro.Entity.AuditRequest audit, string appDest, string option, string ipClient, string ipServer, string jsonParameters, out string Sequence, out string Url)
        {
            string message = "";

            try
            {
                AUDIT_CREDENTIALS.ListaResponseOpcional objResponseOpc;
                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse =
                    Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.registrarComunicacion(new AUDIT_CREDENTIALS.parametrosAuditRequest()
                    {
                        idTransaccion = audit.Transaction,
                        ipAplicacion = audit.IPAddress,
                        nombreAplicacion = audit.ApplicationName,
                        usuarioAplicacion = audit.UserName,
                    },
                option,
                appDest,
                ipClient,
                ipServer,
                jsonParameters,
                new AUDIT_CREDENTIALS.ListaRequestOpcional(), out Sequence, out Url, out objResponseOpc);
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, string.Format("Metodo: {0} , Sequence: {1} , Url:{2}, option: {3}", "InsertRedirectCommunication", Sequence, Url, option));
                string codError = objAuditResponse.codigoRespuesta;
                string errorMsg = objAuditResponse.mensajeRespuesta;

                if (codError == Claro.Constants.NumberZeroString)
                {
                    if (Sequence != "" && Url != "")
                    {
                        message = Claro.SIACU.Constants.ok;
                    }
                    else
                    {
                        message = errorMsg;
                    }
                }
                else
                {
                    message = errorMsg;
                }
            }
            catch (TimeoutException ex)
            {
                message = Claro.SIACU.Constants.MessageNotServicesLimitWait;
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (WebException ex)
            {
                message = Claro.SIACU.Constants.MessageNotComunicationServerRemote;
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                message = ex.Message;
                throw;
            }

            return message;
        }

        /// <summary>
        /// Método que valida la comunicación entre las paginas redireccionadas devolviendo verdadero o falso según sea el caso.
        /// </summary>
        /// <param name="audit">Objeto auditoría</param>
        /// <param name="sequence">Secuencia</param>
        /// <param name="errorMsg">Mensaje de error</param>
        /// <param name="ipServer">Ip de servidor</param>
        /// <param name="urlDest">Url destino</param>
        /// <param name="availability">Disponibilidad</param>
        /// <param name="jsonParameters">Parámetros Json</param>
        /// <returns>Devuelve Url destino y parámetros Json para finalizar la redirección.</returns>
        public static Boolean ValidateRedirectCommunication(Claro.Entity.AuditRequest audit, string sequence, out string errorMsg, string ipServer, out string urlDest, out string availability, out string jsonParameters)
        {
            try
            {
                AUDIT_CREDENTIALS.ListaResponseOpcional objResponseOpc;
                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse = Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.validarComunicacion(new AUDIT_CREDENTIALS.parametrosAuditRequest()
                {
                    idTransaccion = audit.Transaction,
                    ipAplicacion = audit.IPAddress,
                    nombreAplicacion = audit.ApplicationName,
                    usuarioAplicacion = audit.UserName,
                },
                sequence,
                ipServer,
                new AUDIT_CREDENTIALS.ListaRequestOpcional(),
                out urlDest,
                out availability,
                out jsonParameters,
                out objResponseOpc);

                string codError = objAuditResponse.codigoRespuesta;

                if (codError == Claro.Constants.NumberZeroString)
                {
                    if (jsonParameters != "" && urlDest != "")
                    {
                        errorMsg = Claro.SIACU.Constants.ok;
                    }
                    else
                    {
                        errorMsg = objAuditResponse.mensajeRespuesta;
                    }
                }
                else
                {
                    errorMsg = objAuditResponse.mensajeRespuesta;
                }
            }
            catch (TimeoutException ex)
            {
                errorMsg = Claro.SIACU.Constants.MessageNotServicesLimitWait;
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (WebException ex)
            {
                errorMsg = Claro.SIACU.Constants.MessageNotComunicationServerRemote;
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                throw;
            }

            return true;
        }

        /// <summary>
        /// Método que obtiene una lista que se encuentra en un XML.
        /// </summary>
        /// <param name="strNameFunction">Nombre de la lista de valores</param>
        /// <returns>Devuelve lista genérica con la información de la lista solicitada.</returns>
        public static List<Entity.ListItem> GetListValuesXML(string strNameFunction)
        {
            List<Entity.ListItem> listItem = new List<Entity.ListItem>();

            string file = "Data.xml";
            string strApplicationPath;
            strApplicationPath = new Claro.Utils().GetApplicationPath();

            string fullPath = strApplicationPath + file;

            XmlDocument documento = new XmlDocument();
            documento.Load(fullPath);
            XmlNodeList nodos = documento.SelectNodes("descendant::" + strNameFunction + "/item");

            Entity.ListItem oListItem;
            foreach (XmlNode nodo in nodos)
            {
                oListItem = new Entity.ListItem();
                oListItem.Code = nodo["codigo"].InnerText;
                oListItem.Description = nodo["descripcion"].InnerText;
                listItem.Add(oListItem);
            }

            return listItem;
        }

        /// <summary>
        /// Método que obtiene la factura por FTP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="filePath">Ruta de carpeta FTP</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns>Devuelve objeto InvoiceFtpResponse con información de factura.</returns>
        public Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpResponse GetInvoiceFtp(string strIdSession, string strTransaction, string filePath, string fileName)
        {
            byte[] arrBuffer = null;
            Claro.SIACU.Entity.File.GlobalDocument objGlobalDoc = new Claro.SIACU.Entity.File.GlobalDocument();
            Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp.InvoiceFtpResponse objInvoiceFtpResponse;

            if (int.TryParse(KEY.AppSettings("timeOutElectronicBillsFtp"), out intTimeOutFtp))
            {
                intTimeTranscurridoFtp = Claro.Constants.NumberZero;

                thrProcess = new Thread(new ThreadStart(() =>
                {
                    arrBuffer = GetInvoiceFtpTime(strIdSession, strTransaction, filePath, fileName);
                }));
                thrProcess.Start();
                tmrController = new System.Timers.Timer(1000);
                tmrController.Elapsed += new System.Timers.ElapsedEventHandler(verificarTimeOutFtp);
                tmrController.Start();
                thrProcess.Join();
                objGlobalDoc.CodigoError = Claro.Constants.NumberZeroString;
                objGlobalDoc.Documento = arrBuffer;
                objGlobalDoc.MensajeError = "";

                if ((intTimeTranscurridoFtp >= intTimeOutFtp))
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, "Se excedió el tiempo de espera: " + intTimeOutFtp);
                    objGlobalDoc.MensajeError = Claro.SIACU.Constants.MessageTimeOut;
                    objGlobalDoc.CodigoError = Claro.Constants.NumberOneString;
                }
                else if (arrBuffer == null)
                {
                    objGlobalDoc.MensajeError = Claro.SIACU.Constants.MessageFileNotExist;
                    objGlobalDoc.CodigoError = Claro.Constants.NumberTwoString;
                }

            }
            else
            {
                throw new Claro.MessageException("No se ha definido el valor del TimeOut en el key: strValorTimeOutFacturasE");
            }

            objInvoiceFtpResponse = new ENTITYS.Board.GetInvoiceFtp.InvoiceFtpResponse()
            {
                ObjGlobalDocument = objGlobalDoc
            };
            return objInvoiceFtpResponse;
        }

        /// <summary>
        /// Método que obtiene los datos de la factura por FTP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="filePath">Ruta de carpeta ftp</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns>Devuelve arreglo de bytes con información de factura.</returns>
        public static byte[] GetInvoiceFtpTime(string strIdSession, string strTransaction, string filePath, string fileName)
        {
            IFtpConfiguration objIFtpConfiguration = FtpConfiguration.SIACU_FtpFacturas;

            byte[] arrBuffer = Claro.Data.Ftp.ConnectFtp(strTransaction, objIFtpConfiguration, filePath, fileName);

            return arrBuffer;
        }

        /// <summary>
        /// Método que obtiene la factura de una carpeta compartida en el servidor.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="ruta">Ruta de archivo</param>
        /// <returns>Devuelve objeto FileInvoiceResponse con información de factura.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceResponse GetFileInvoice(string strIdSession, string strTransaction, string ruta)
        {

            Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice.FileInvoiceResponse objFileInvoiceResponse = new ENTITYS.Board.GetFileInvoice.FileInvoiceResponse()
            {
                ObjArrBuffer = GetFileInternal(strIdSession, strTransaction, ruta)
            };

            return objFileInvoiceResponse;
        }

        /// <summary>
        /// Método que obtiene archivo con suplantacion predeterminada.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPath">Ruta de archivo</param>
        /// <returns>Devuelve objeto FileDefaultImpersonationResponse con archivo de suplantación predeterminada.</returns>
        public Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileDefaultImpersonation(string strIdSession, string strTransaction, string strPath)
        {
            byte[] arrBuffer = null;
            Claro.SIACU.Entity.File.GlobalDocument objGlobalDoc = new Claro.SIACU.Entity.File.GlobalDocument();
            INetworkConfiguration objINetworkConfiguration = NetworkConfiguration.SIAC_POST_DirFacturas;


            string strFileDefault = Claro.Data.Network.Connect(strIdSession, strTransaction, objINetworkConfiguration, System.IO.Path.GetPathRoot(strPath));

            Claro.Web.Logging.Error(strIdSession, strTransaction, " GetFileDefaultImpersonation Claro.Data.Network.Connect: " + strFileDefault);
            if (int.TryParse(KEY.AppSettings("timeOutElectronicBills"), out intTimeOutFs))
            {
                intTimeTranscurridoFs = Claro.Constants.NumberZero;
                tmrController = new System.Timers.Timer(1000);
                thrProcess = new Thread(new ThreadStart(() =>
                {
                    arrBuffer = GetFileDefaultImpersonationTime(strIdSession, strTransaction, strPath);
                }));


                thrProcess.Start();
                tmrController.Elapsed += new System.Timers.ElapsedEventHandler(verificarTimeOutFs);
                tmrController.Start();
                thrProcess.Join();

                objGlobalDoc.CodigoError = Claro.Constants.NumberZeroString;
                objGlobalDoc.Documento = arrBuffer;
                objGlobalDoc.MensajeError = "";

                if ((intTimeTranscurridoFs >= intTimeOutFs))
                {

                    Claro.Web.Logging.Error(strIdSession, strTransaction, "Se excedió el tiempo de espera: " + intTimeOutFs);
                    objGlobalDoc.MensajeError = Claro.SIACU.Constants.MessageTimeOut;
                    objGlobalDoc.CodigoError = Claro.Constants.NumberOneString;
                }
                else if (arrBuffer == null)
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, "El buffer del archivo es null");
                    objGlobalDoc.MensajeError = Claro.SIACU.Constants.MessageFileNotExist;
                    objGlobalDoc.CodigoError = Claro.Constants.NumberTwoString;
                }

            }
            else
            {
                throw new Claro.MessageException("No se ha definido el valor del TimeOut en el key: strValorTimeOutFacturasE");
            }

            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileInvoiceResponse = new Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse()
            {
                ObjGlobalDocument = objGlobalDoc
            };
            return objFileInvoiceResponse;
        }

        /// <summary>
        /// Método que conecta al archivo de suplantacion predeterminada.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="path">Ruta de archivo</param>
        /// <returns>Devuelve array de bytes con información de archivo.</returns>
        public static byte[] GetFileDefaultImpersonationTime(string strIdSession, string strTransaction, string path)
        {
            Claro.Web.Logging.Error(strIdSession, strTransaction, "entro GetFileDefaultImpersonationTime: " + path);
            ConectarUnidadRed(Path.GetPathRoot(path));
            byte[] arrBuffer = GetFileInternal(strIdSession, strTransaction, path);
            return arrBuffer;
        }

        /// <summary>
        /// Método que conecta a la unidad de red.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPath">Ruta de archivo</param>
        private static void ConectarUnidadRed(string strPath)
        {
            List<string> _arrConexionesRed = new List<string>();
            if (!_arrConexionesRed.Exists(item => item == strPath))
            {
                _arrConexionesRed.Add(strPath);
            }

        }

        /// <summary>
        /// Método que obtiene archivo internal.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="transaction">Id de transacción</param>
        /// <param name="path">Ruta de archivo</param>
        /// <returns>Devuelve array de bytes con información de archivo.</returns>
        private static byte[] GetFileInternal(string strIdSession, string transaction, string path)
        {
            byte[] buffer = null;

            if (File.Exists(path))
            {
                Claro.Web.Logging.Error(strIdSession, transaction, "Existe : " + path);
                Stream stream = null;
                try
                {
                    using (stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        Claro.Web.Logging.Error(strIdSession, transaction, "Error al leer el archivo : " + path);
                        long length = stream.Length;

                        if (length > 0L)
                        {

                            buffer = new byte[length];

                            stream.Read(buffer, 0, (int)length);
                            Claro.Web.Logging.Error(strIdSession, transaction, "File size : " + length.ToString());
                        }
                        else
                        {
                            Claro.Web.Logging.Error(strIdSession, transaction, "El size del archivo es invalido  size: " + length.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    Claro.Web.Logging.Error(strIdSession, transaction, "el stream se cierra correctamente luego de la excepcion - " + exception.Message);

                }
                finally
                {

                    if (stream != null)
                    {
                        stream.Close();

                    }
                }
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, transaction, "No Existe : " + path);

                Stream stream = null;
                try
                {
                    using (stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        Claro.Web.Logging.Error(strIdSession, transaction, "2Error al leer el archivo : " + path);
                        long length = stream.Length;

                        if (length > 0L)
                        {

                            buffer = new byte[length];

                            stream.Read(buffer, 0, (int)length);
                            Claro.Web.Logging.Error(strIdSession, transaction, "2File size : " + length.ToString());
                        }
                        else
                        {
                            Claro.Web.Logging.Error(strIdSession, transaction, "2El size del archivo es invalido  size: " + length.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    Claro.Web.Logging.Error(strIdSession, transaction, "2el stream se cierra correctamente luego de la excepcion - " + exception.Message);

                }
                finally
                {

                    if (stream != null)
                    {
                        stream.Close();

                    }
                }
            }

            return buffer;
        }

        /// <summary>
        /// Método que verifica TimeOut.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        private void verificarTimeOutFtp(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!object.ReferenceEquals(sender, null) && (!object.ReferenceEquals(e, null) && (intTimeTranscurridoFtp >= intTimeOutFtp)))
            {
                tmrController.Stop();
                if ((thrProcess != null))
                {
                    thrProcess.Abort();
                    thrProcess = null;
                }
            }
            intTimeTranscurridoFtp += Claro.Constants.NumberOne;
        }

        /// <summary>
        /// Método que verifica TimeOut.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        private void verificarTimeOutFs(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!object.ReferenceEquals(sender, null) && (!object.ReferenceEquals(e, null) && (intTimeTranscurridoFs >= intTimeOutFs)))
            {
                tmrController.Stop();
                if ((thrProcess != null))
                {
                    thrProcess.Abort();
                    thrProcess = null;
                }
            }
            intTimeTranscurridoFs += Claro.Constants.NumberOne;
        }

        /// <summary>
        /// Método que retorna tipo MIME.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="extension">Extensión del archivo</param>
        /// <returns>Devuelve objeto TypeMIMEResponse con información de tipo MIME.</returns>
        public static TypeMIMEResponse GetTypeMIME(string strIdSession, string strTransaction, string extension)
        {
            string TypeMime = "";

            DbParameter[] parameters = new DbParameter[] {

                new DbParameter("C_EXTENSION", DbType.String, ParameterDirection.Input, extension)

            };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.TIMGLOBAL, DbCommandConfiguration.SIACU_TIMGSS_GETTIPODOC, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    TypeMime = Convert.ToString(reader[0].ToString());
                    break;
                }
            });

            TypeMIMEResponse objGetTypeMIMEResponse = new TypeMIMEResponse() { TypeMime = TypeMime };
            return objGetTypeMIMEResponse;
        }

        /// <summary>
        /// Método que retorna lista con los datos de portabilidad.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="telefono">Teléfono</param>
        /// <param name="respuesta">Respuesta</param>
        /// <returns>Devuelve listado de objetos Portability con información de portabilidad.</returns>
        public static List<Entity.Dashboard.Portability> GetPortability(string strIdSession, string strTransaction, string telefono, out string respuesta)
        {
            List<Entity.Dashboard.Portability> objPortability = new List<Entity.Dashboard.Portability>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_TELEFONO", DbType.String, 20, ParameterDirection.Input, telefono),
                new DbParameter("RESPUESTA", DbType.String, 50 ,ParameterDirection.Output ),
                new DbParameter("K_RESULTADO", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CONSULTA_PORTABILIDAD, parameters, (IDataReader reader) =>
            {
                Entity.Dashboard.Portability item;

                while (reader.Read())
                {
                    item = new Entity.Dashboard.Portability();

                    item.NUMERO_SOLICITUD = Convert.ToString(reader["SOPOC_ID_SOLICITUD"]);
                    item.ESTADO_PROCESO = Convert.ToString(reader["SOPOC_ESTA_PROCESO"]);
                    item.DESCRPCION_ESTADO_PROCESO = Convert.ToString(reader["PARAV_DESCRIPCION"]);
                    item.TIPO_PORTABILIDAD = Convert.ToString(reader["SOPOC_TIPO_PORTA"]);
                    item.FECHA_REGISTRO = Convert.ToDate(reader["SOPOT_FECHA_REGISTRO"]);
                    if (item.ESTADO_PROCESO == KEY.AppSettings("PortabilidadEstadoFinOk"))
                    {
                        item.FECHA_EJECUCION = Convert.ToDate(reader["SOPOT_FECHA_EJECUCION"]);

                        if (item.TIPO_PORTABILIDAD == KEY.AppSettings("PortabilidadPortIN"))
                        {
                            item.CODIGO_OPERADOR_CEDENTE = Convert.ToString(reader["SOPOC_CODIGO_CEDENTE"]);
                        }
                        if (item.TIPO_PORTABILIDAD == KEY.AppSettings("PortabilidadPortOUT"))
                        {
                            item.CODIGO_OPERADOR_RECEPTOR = Convert.ToString(reader["SOPOC_CODIGO_RECEPTOR"]);
                        }
                    }
                    objPortability.Add(item);
                }
            });
            respuesta = Convert.ToString(parameters[1].Value);

            return objPortability;
        }

        /// <summary>
        /// Método que retorna tipo de documento del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de tipos de documentos de cliente.</returns>
        public static List<Entity.ListItem> GetTypeDocumentCustomer(string strIdSession, string strTransaction)
        {
            List<Entity.ListItem> listListItem = new List<Entity.ListItem>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("result", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_DOC_TYPE, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    listListItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["TIPO_DOC"])
                    });
                }
            });
            return listListItem;
        }

        /// <summary>
        /// Método que retorna el estado civil del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de estados civiles.</returns>
        public static List<Entity.ListItem> GetStateCivil(string strIdSession, string strTransaction)
        {
            List<Entity.ListItem> listListItem = new List<Entity.ListItem>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("result", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_MARITAL_STATUS, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    listListItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["ESTADO_CIVIL"])
                    });
                }
            });
            return listListItem;
        }

        /// <summary>
        /// Método que retorna la ocupación del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de ocupaciones.</returns>
        public static List<Entity.ListItem> GetOccupation(string strIdSession, string strTransaction)
        {
            List<Entity.ListItem> listListItem = new List<Entity.ListItem>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("result", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_OCCUPATION, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    listListItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["OCUPACION"])
                    });
                }
            });
            return listListItem;
        }

        /// <summary>
        /// Método que retorna el motivo de registro del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de motivos de registro</returns>
        public static List<Entity.ListItem> GetMotiveRegister(string strIdSession, string strTransaction)
        {
            List<Entity.ListItem> listListItem = new List<Entity.ListItem>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("result", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_PREPAID_REGISTRATION_REASON, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    listListItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["MOTIVO_REGISTRO"])
                    });
                }
            });
            return listListItem;
        }

        /// <summary>
        /// Método que retorna el tipo de estado de la solicitud.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="idList">Id de lista</param>
        /// <returns>Devuelve listado de tipos de estado de solicitud.</returns>
        public static List<Entity.ListItem> GetStateType(string strIdSession, string strTransaction, string idList)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_LISTA", DbType.String, 100, ParameterDirection.Input, idList),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                
            };

            List<Entity.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.ListItem()
                    {
                        Code = Convert.ToString(reader["VALOR"]),
                        Description = Convert.ToString(reader["DESCRIPCION"])
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// Método que retorna el tipo de transacción de la solicitud.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de tipos de transacción.</returns>
        public static List<Entity.ListItem> GetTransactionType(string strIdSession, string strTransaction)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
                
            };

            List<Entity.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.ListItem()
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["DESCRIPCION"])
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// Método que devuele el tipo de establecimiento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="code">Código</param>
        /// <returns>Devuelve listado de tipos de establecimiento.</returns>
        public static List<Entity.ListItem> GetCacDacType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_OBJID", DbType.Int64, ParameterDirection.Input, code),
                new DbParameter("P_FLAG_CONSULTA", DbType.String,225, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,225, ParameterDirection.Output),
                new DbParameter("P_LIST", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.ListItem()
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["NOMBRE"])
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// Método que devuelve el código de la lista.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="nameList">Nombre de lista</param>
        /// <returns>Devuelve código de lista.</returns>
        public static int GetCodeList(string strIdSession, string strTransaction, string nameList)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TITLE", DbType.String,3, ParameterDirection.Input, nameList),
                new DbParameter("P_OBJID", DbType.String,225, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_CODIGO, parameters);

            return Convert.ToInt(parameters[1].Value.ToString());
        }

        /// <summary>
        /// Método que devuelve el tipo de línea de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="contractId">Id de contrato</param>
        /// <returns>Devuelve listado de tipos de teléfono.</returns>
        public static List<Entity.ListItem> GetTelephoneType(string strIdSession, string strTransaction, int contractId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input, contractId),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,300, ParameterDirection.Output)
                
            };

            List<Entity.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTA_TELEFONO, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["DN_NUM"])
                    });
                }
            });

            return listItem;
        }
        /// <summary>
        /// Método que devuelve el tipo de línea de teléfono LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="contractId">Id de contrato</param>
        /// <returns>Devuelve listado de tipos de teléfono.</returns>
        public static List<Entity.ListItem> GetTelephoneTypeLTE(string strIdSession, string strTransaction, int contractId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input, contractId),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,300, ParameterDirection.Output)
                
            };

            List<Entity.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTA_TELEFONO_LTE, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["DN_NUM"])
                    });
                }
            });

            return listItem;
        }
        public static List<Entity.ListItem> GetDocumentType(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_id_lista", DbType.String,100, ParameterDirection.Input, strCodCargaDdl),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
                
            };

            List<Entity.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.ListItem()
                    {
                        Description = Convert.ToString(reader["DESCRIPCION"]),
                        Code = Convert.ToString(reader["VALOR"])
                    });
                }
            });

            return listItem;
        }


        /// <summary>
        /// Método que obtiene una lista de Consulta 4G por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strError">Error</param>
        /// <returns>Devuelve listado de objetos HLR UDB con información de la misma.</returns>
        public static List<Entity.Dashboard.HLR> GetHLRUDB(string strIdSession, string strTransaction, string strApplicationName, string strIpApplication, string strUserName, string strPhoneNumber, string strActionId, string strFieldName, string strListName, out string strCodeError)
        {
            List<Entity.Dashboard.HLR> listHLRUDB = new List<Entity.Dashboard.HLR>();
            HLR item;
            COMMON_HLRUDB.listaParametros[] objList = new COMMON_HLRUDB.listaParametros[1];
            COMMON_HLRUDB.listaParametros objParameterList = new COMMON_HLRUDB.listaParametros();
            COMMON_HLRUDB.listaParametrosParametro[] objParameterArray = new COMMON_HLRUDB.listaParametrosParametro[1];
            COMMON_HLRUDB.accionType action = new COMMON_HLRUDB.accionType();
            COMMON_HLRUDB.listaParametrosParametro parameter = new COMMON_HLRUDB.listaParametrosParametro();
            COMMON_HLRUDB.consultarRequest objConsultarRequest = new COMMON_HLRUDB.consultarRequest()
            {

                accionRequest = action,
                auditRequest = new COMMON_HLRUDB.parametrosAuditRequest()
                {
                    usuarioAplicacion = strUserName,
                    ipAplicacion = strIpApplication,
                    nombreAplicacion = strApplicationName,
                    idTransaccion = strTransaction
                }
            };

            parameter.campo = strFieldName;
            parameter.valor = strPhoneNumber;
            objParameterArray[0] = parameter;
            objParameterList.nombreLista = strListName;
            objParameterList.parametro = objParameterArray;
            objList[0] = objParameterList;
            action.idAccion = strActionId;
            action.listaParametros = objList;

            COMMON_HLRUDB.consultarResponse Response =
            Claro.Web.Logging.ExecuteMethod<COMMON_HLRUDB.consultarResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.COMMON_HLRUDB, () =>
            { return Configuration.ServiceConfiguration.COMMON_HLRUDB.consultar(objConsultarRequest); });

            strCodeError = Response.auditResponse.codRespuesta.ToString();

            if (Response.auditResponse.codRespuesta.Equals(SIACU.Constants.ZeroNumber))
            {
                foreach (COMMON_HLRUDB.listaParametros objListaSalida in Response.accionResponse.listaParametros)
                {
                    if (objListaSalida.parametro != null)
                    {
                        foreach (COMMON_HLRUDB.listaParametrosParametro objParametroSalida in objListaSalida.parametro)
                        {
                            item = new HLR();
                            item.Code = objParametroSalida.campo;
                            item.Description = objParametroSalida.valor;
                            listHLRUDB.Add(item);
                        }
                        if (objListaSalida.subListaParametros != null)
                        {
                            foreach (COMMON_HLRUDB.subListaParametros objSubLista in objListaSalida.subListaParametros)
                            {
                                foreach (COMMON_HLRUDB.subListaParametrosParametro objSubParametro in objSubLista.parametro)
                                {
                                    item = new HLR();
                                    item.Code = objSubParametro.campo;
                                    item.Description = objSubParametro.valor;
                                    listHLRUDB.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            return listHLRUDB;
        }


        public static List<Entity.Dashboard.Employee> GetEmployeByUser(string IdSession, string Transaction, string UserName)
        {

            AUDIT_SECURITY.EbsAuditoriaClient objEbsAuditoria = Configuration.ServiceConfiguration.SECURITY_PERMISSIONS;

            AUDIT_SECURITY.DatosEmpleadoRequest objRequest = new AUDIT_SECURITY.DatosEmpleadoRequest()
            {
                login = UserName
            };

            AUDIT_SECURITY.EmpleadoResponse objEmpleadoResponse = null;
            Claro.Web.Logging.Info(IdSession, Transaction,
            string.Format("El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerDatosEmpleado, Parametros de Entrada: login={0}", UserName));
            objEmpleadoResponse = objEbsAuditoria.leerDatosEmpleado(objRequest);
            List<Entity.Dashboard.Employee> lstEmploye = null;
            if (objEmpleadoResponse != null && (objEmpleadoResponse.empleados != null && objEmpleadoResponse.empleados.item != null && objEmpleadoResponse.empleados.item.Length > 0))
            {
                AUDIT_SECURITY.EmpleadoType[] objEmpleadoType = objEmpleadoResponse.empleados.item;
                lstEmploye = new List<Entity.Dashboard.Employee>();
                foreach (AUDIT_SECURITY.EmpleadoType item in objEmpleadoType)
                {
                    lstEmploye.Add(new Employee()
                    {
                        Login = item.login,
                        UserID = int.Parse(item.codigo),
                        FullName = string.Format("{0} {1} {2}", item.nombres, item.paterno, item.materno),
                        FirstName = item.nombres,
                        LastName1 = item.paterno,
                        LastName2 = item.materno,
                        SearchUser = "0"
                    });
                }
            }
            else
            {
                Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerDatosEmpleado , No devuelve resultados");
            }

            return lstEmploye;
        }

        public static List<Entity.Dashboard.Prepaid.PaginaOption> ReadOptionsByUser(string IdSession, string Transaction, int IdAplication, int IdUser)
        {

            AUDIT_SECURITY.EbsAuditoriaClient objEbsAuditoria = Configuration.ServiceConfiguration.SECURITY_PERMISSIONS;

            AUDIT_SECURITY.PaginaOpcionesUsuarioRequest objRequest = new AUDIT_SECURITY.PaginaOpcionesUsuarioRequest();
            objRequest.aplicCod = IdAplication;
            objRequest.user = IdUser;

            AUDIT_SECURITY.PaginaOpcionesUsuarioResponse objleerOpcionesPorUsuarioResponse = null;
            Claro.Web.Logging.Info(IdSession, Transaction,
            string.Format("El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario, Parametros de Entrada: aplicCod={0} ,user={1}", IdAplication, IdUser));
            objleerOpcionesPorUsuarioResponse = objEbsAuditoria.leerPaginaOpcionesPorUsuario(objRequest);
            List<Entity.Dashboard.Prepaid.PaginaOption> lstPaginaOption = null;
            if (objleerOpcionesPorUsuarioResponse != null && objleerOpcionesPorUsuarioResponse.listaOpciones.Length > 0)
            {
                lstPaginaOption = new List<Entity.Dashboard.Prepaid.PaginaOption>();
                AUDIT_SECURITY.PaginaOpcionType[] arrPaginaOpcionType = objleerOpcionesPorUsuarioResponse.listaOpciones;
                foreach (AUDIT_SECURITY.PaginaOpcionType item in arrPaginaOpcionType)
                {
                    lstPaginaOption.Add(new Entity.Dashboard.Prepaid.PaginaOption()
                    {
                        OptionCode = item.opcicCod,
                        OptionDescription = item.opcicDes,
                        Clave = item.clave
                    });
                }
            }
            else
            {
                Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario , No devuelve resultados");
            }

            return lstPaginaOption;
        }



        public static Entity.Dashboard.Board.CheckingUser.CheckingUserResponse CheckingUser(string IdTransaction, string IpAplication, string Aplication, string user, long AppCode)
        {

            Entity.Dashboard.Board.CheckingUser.CheckingUserResponse objCheckingUserResponse = new Entity.Dashboard.Board.CheckingUser.CheckingUserResponse();
            List<Entity.Dashboard.ConsulSeguridad> list = new List<ConsulSeguridad>();
            CONSULTA_SECURITY.ConsultaSeguridad consulta = Configuration.ServiceConfiguration.COMMON_CONSULTA_SEGURIDAD;
            CONSULTA_SECURITY.verificaUsuarioRequest request = new CONSULTA_SECURITY.verificaUsuarioRequest();
            CONSULTA_SECURITY.verificaUsuarioResponse response;

            request.idTransaccion = IdTransaction;
            request.ipAplicacion = IpAplication;
            request.aplicacion = Aplication;
            request.usuarioLogin = user;
            request.appCod = AppCode;

            CONSULTA_SECURITY.seguridadType[] seguridadType;
            response = consulta.verificaUsuario(request);
            seguridadType = response.cursorSeguridad;


            Claro.Web.Logging.Info("", IdTransaction,
            string.Format("El servicio ConsultaSeguridad(strWebServiceDBAUDIT) Metodo: verificaUsuario Parametros de Entrada: idTransaccion={0} ,IpAplication={1}, Aplication={2}, usuarioLogin={3} , AppCode={4}", IdTransaction, IpAplication, Aplication, user, AppCode));
            if (seguridadType != null && seguridadType.Length > 0)
            {
                int seguridadTypeCount = seguridadType.Length;
                for (int i = 0; i < seguridadTypeCount; i++)
                {
                    ConsulSeguridad item = new ConsulSeguridad();

                    item.USUACCOD = seguridadType[i].UsuacCod;
                    item.PERFCCOD = seguridadType[i].PerfcCod;
                    item.USUACCODVENSAP = seguridadType[i].UsuacCodVenSap;
                    list.Add(item);
                }
            }
            else
            {
                Claro.Web.Logging.Error("", IdTransaction, "El servicio ConsultaSeguridad(strWebServiceDBAUDIT) Metodo: verificaUsuario , No devuelve resultados");
            }

            objCheckingUserResponse.consultasSeguridad = list;
            objCheckingUserResponse.IdTransaccion = response.idTransaccion;
            objCheckingUserResponse.CodeErr = response.errorCode;
            objCheckingUserResponse.MsgErr = response.errorMsg;

            return objCheckingUserResponse;
        }

        /// <summary>
        /// Método que obtiene el Código Plano del cliente.
        /// </summary>
        /// <param name="strTransaction">Código de transacción</param>
        /// <param name="strContractId">Código de Contrato</param>
        /// <param name="strIdSession">Código de Sesión</param>
        /// <returns>>Devuelve  el Código Plano del cliente.</returns>
        public static string GetFlatCode(string strIdSession, string strTransaction, string strContractId)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("av_cod_id", DbType.Int64, ParameterDirection.Input, strContractId),
                new DbParameter("av_idplano", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int64, 255,ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String, 255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_IDPLANOXCOID, parameters);

            return parameters[1].Value.ToString();
        }

        /// <summary>
        /// Método que permite validar si el SIM soporta las tecnologías VoLTE y VoWIFI, es decir, su versión es > a 10.02.
        /// </summary>
        /// <param name="oBEConsultarTecVoLTERequest">Request del método consultarTecVoLTE/param>
        /// <returns>Devuelve el response del servicio consultarTecVoLTE.</returns>
        /// PROY-31249
        public static Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTEResponse ConsultarTecVoLTE(Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTERequest oBEConsultarTecVoLTERequest)
        {
            Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTEResponse oBEConsultarTecVoLTEResponse = new Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTEResponse();

            COMMON_ACTIVARVOLTEWS.consultarTecVoLTERequest oConsultarTecVoLTERequest = new COMMON_ACTIVARVOLTEWS.consultarTecVoLTERequest();
            COMMON_ACTIVARVOLTEWS.consultarTecVoLTEResponse oConsultarTecVoLTEResponse = null;

            oConsultarTecVoLTERequest.auditRequest = new COMMON_ACTIVARVOLTEWS.auditRequestType();

            try
            {
                Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oBEConsultarTecVoLTERequest.Audit.Transaction, "::INICIO - consultarTecVoLTE::");

                oConsultarTecVoLTERequest.auditRequest.idTransaccion = oBEConsultarTecVoLTERequest.Audit.Transaction;
                oConsultarTecVoLTERequest.auditRequest.ipAplicacion = oBEConsultarTecVoLTERequest.Audit.IPAddress;
                oConsultarTecVoLTERequest.auditRequest.nombreAplicacion = oBEConsultarTecVoLTERequest.Audit.ApplicationName;
                oConsultarTecVoLTERequest.auditRequest.usuarioAplicacion = oBEConsultarTecVoLTERequest.Audit.UserName;

                Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("IDTransaccion: {0}, IP: {1}, NombreAplicacion: {2}, Usuario: {3}", oConsultarTecVoLTERequest.auditRequest.idTransaccion, oConsultarTecVoLTERequest.auditRequest.ipAplicacion, oConsultarTecVoLTERequest.auditRequest.nombreAplicacion, oConsultarTecVoLTERequest.auditRequest.usuarioAplicacion));

                oConsultarTecVoLTERequest.serieVOLTE = oBEConsultarTecVoLTERequest.serieVOLTE;

                Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("serieVOLTE: {0}", oConsultarTecVoLTERequest.serieVOLTE));

                if (oBEConsultarTecVoLTERequest.lstRequestOpcional != null && oBEConsultarTecVoLTERequest.lstRequestOpcional.Count > 0)
                {
                    oConsultarTecVoLTERequest.listaRequestOpcional = new COMMON_ACTIVARVOLTEWS.parametrosTypeObjetoOpcional[oBEConsultarTecVoLTERequest.lstRequestOpcional.Count];
                    Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, string.Format("[INICIO] - listaRequestOpcional: {0}", oBEConsultarTecVoLTERequest.lstRequestOpcional.Count));
                    for (int i = 0; i < oBEConsultarTecVoLTERequest.lstRequestOpcional.Count; i++)
                    {
                        oConsultarTecVoLTERequest.listaRequestOpcional[i] = new COMMON_ACTIVARVOLTEWS.parametrosTypeObjetoOpcional();
                        oConsultarTecVoLTERequest.listaRequestOpcional[i].campo = oBEConsultarTecVoLTERequest.lstRequestOpcional[i].campo;
                        oConsultarTecVoLTERequest.listaRequestOpcional[i].valor = oBEConsultarTecVoLTERequest.lstRequestOpcional[i].valor;

                        Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("{0}: Campo: {1}, Valor: {2}", i, oConsultarTecVoLTERequest.listaRequestOpcional[i].campo, oConsultarTecVoLTERequest.listaRequestOpcional[i].valor));
                    }
                    Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, "[FIN] - listaRequestOpcional");
                }

                oConsultarTecVoLTEResponse = Claro.Web.Logging.ExecuteMethod<COMMON_ACTIVARVOLTEWS.consultarTecVoLTEResponse>
            (oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, Configuration.ServiceConfiguration.COMMON_ACTIVARVOLTEWS, () =>
            { return Configuration.ServiceConfiguration.COMMON_ACTIVARVOLTEWS.consultarTecVoLTE(oConsultarTecVoLTERequest); });


                if (oConsultarTecVoLTEResponse != null)
                {
                    oBEConsultarTecVoLTEResponse.codigoMaterial = oConsultarTecVoLTEResponse.codigoMaterial;
                    oBEConsultarTecVoLTEResponse.existeChip = oConsultarTecVoLTEResponse.existeChip;
                    oBEConsultarTecVoLTEResponse.autenticaVOLTE = oConsultarTecVoLTEResponse.autenticaVOLTE;
                    oBEConsultarTecVoLTEResponse.codigoResultado = oConsultarTecVoLTEResponse.codigoResultado;
                    oBEConsultarTecVoLTEResponse.mensajeResultado = oConsultarTecVoLTEResponse.mensajeResultado;
                    Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, "[INICIO] - Response");
                    Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("codigoMaterial: {0}, existeChip: {1}, autenticaVOLTE: {2}, codigoResultado: {3}, mensajeResultado: {4}", oBEConsultarTecVoLTEResponse.codigoMaterial, oBEConsultarTecVoLTEResponse.existeChip, oBEConsultarTecVoLTEResponse.autenticaVOLTE, oBEConsultarTecVoLTEResponse.codigoResultado, oBEConsultarTecVoLTEResponse.mensajeResultado));

                    if (oConsultarTecVoLTEResponse.auditResponse != null)
                    {
                        oBEConsultarTecVoLTEResponse.oAuditResponse = new Entity.AuditResponse();
                        oBEConsultarTecVoLTEResponse.oAuditResponse.codigoRespuesta = oConsultarTecVoLTEResponse.auditResponse.codigoRespuesta;
                        oBEConsultarTecVoLTEResponse.oAuditResponse.mensajeRespuesta = oConsultarTecVoLTEResponse.auditResponse.mensajeRespuesta;
                        Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("codigoRespuesta: {0}, mensajeRespuesta: {1}", oBEConsultarTecVoLTEResponse.oAuditResponse.codigoRespuesta, oBEConsultarTecVoLTEResponse.oAuditResponse.mensajeRespuesta));
                    }
                    if (oConsultarTecVoLTEResponse.listaResponseOpcional != null && oConsultarTecVoLTEResponse.listaResponseOpcional.Length > 0)
                    {
                        Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, string.Format("[INICIO] - listaResponseOpcional: {0}", oConsultarTecVoLTEResponse.listaResponseOpcional.Length));
                        for (int i = 0; i < oConsultarTecVoLTEResponse.listaResponseOpcional.Length; i++)
                        {
                            oBEConsultarTecVoLTEResponse.lstResponseOpcional[i] = new Entity.ItemOpcional();
                            oBEConsultarTecVoLTEResponse.lstResponseOpcional[i].campo = oConsultarTecVoLTEResponse.listaResponseOpcional[i].campo;
                            oBEConsultarTecVoLTEResponse.lstResponseOpcional[i].valor = oConsultarTecVoLTEResponse.listaResponseOpcional[i].valor;

                            Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("{0}: Campo: {1}, Valor: {2}", i, oConsultarTecVoLTERequest.listaRequestOpcional[i].campo, oConsultarTecVoLTERequest.listaRequestOpcional[i].valor));
                        }
                        Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, "[FIN] - listaResponseOpcional");
                    }

                    Claro.Web.Logging.Info(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, "[FIN] - consultarTecVoLTE Response");
                }

            }
            catch (Exception e)
            {
                oBEConsultarTecVoLTEResponse = null;
                Claro.Web.Logging.Error(oBEConsultarTecVoLTERequest.Audit.Session, oConsultarTecVoLTERequest.auditRequest.idTransaccion, String.Format("Error: {0}", e.Message));
            }

            return oBEConsultarTecVoLTEResponse;
        }


        /// <summary>
        /// Método que obtiene una lista de Consulta de UDBConnector
        /// </summary>
        /// <param name="oConsultaUDBRequest">oConsultaUDBRequest</param>
        /// <returns>Devuelve la respuesta del UDBConnector</returns>
        public static Entity.Common.GetConsultaUDB.ConsultaUDBResponse GetConsultaUDB(Entity.Common.GetConsultaUDB.ConsultaUDBRequest oConsultaUDBRequest)
        {
            COMMON_HLRUDB.consultarRequest oConsultarRequest = new COMMON_HLRUDB.consultarRequest();
            COMMON_HLRUDB.consultarResponse oconsultarResponse = null;
            Entity.Common.GetConsultaUDB.ConsultaUDBResponse oConsultaUDBResponse = new Entity.Common.GetConsultaUDB.ConsultaUDBResponse();

            oConsultarRequest.auditRequest = new COMMON_HLRUDB.parametrosAuditRequest()
            {
                idTransaccion = oConsultaUDBRequest.Audit.Transaction,
                ipAplicacion = oConsultaUDBRequest.Audit.IPAddress,
                nombreAplicacion = oConsultaUDBRequest.Audit.ApplicationName,
                usuarioAplicacion = oConsultaUDBRequest.Audit.UserName
            };

            oConsultarRequest.accionRequest = new COMMON_HLRUDB.accionType();
            oConsultarRequest.accionRequest.idAccion = oConsultaUDBRequest.oAccionRequest.idAccion;

            if (oConsultaUDBRequest.oAccionRequest.lstParametro != null && oConsultaUDBRequest.oAccionRequest.lstParametro.Count > 0)
            {
                oConsultarRequest.accionRequest.listaParametros = new COMMON_HLRUDB.listaParametros[oConsultaUDBRequest.oAccionRequest.lstParametro.Count];
                Claro.Web.Logging.Info(oConsultaUDBRequest.Audit.Session, oConsultaUDBRequest.Audit.Transaction, string.Format("[INICIO] - lstParametro: {0}", oConsultaUDBRequest.oAccionRequest.lstParametro.Count));
                for (int i = 0; i < oConsultaUDBRequest.oAccionRequest.lstParametro.Count; i++)
                {
                    oConsultarRequest.accionRequest.listaParametros[i] = new COMMON_HLRUDB.listaParametros();
                    oConsultarRequest.accionRequest.listaParametros[i].nombreLista = oConsultaUDBRequest.oAccionRequest.lstParametro[i].nombreLista;
                    if (oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstParametro != null && oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstParametro.Count > 0)
                    {
                        oConsultarRequest.accionRequest.listaParametros[i].parametro = new COMMON_HLRUDB.listaParametrosParametro[oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstParametro.Count];
                        for (int j = 0; j < oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstParametro.Count; j++)
                        {
                            oConsultarRequest.accionRequest.listaParametros[i].parametro[j] = new COMMON_HLRUDB.listaParametrosParametro();
                            oConsultarRequest.accionRequest.listaParametros[i].parametro[j].campo = oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstParametro[j].campo;
                            oConsultarRequest.accionRequest.listaParametros[i].parametro[j].valor = oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstParametro[j].valor;
                        }
                    }
                    if (oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstSubParametro != null && oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstSubParametro.Count > 0)
                    {
                        oConsultarRequest.accionRequest.listaParametros[i].subListaParametros = new COMMON_HLRUDB.subListaParametros[oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstSubParametro.Count];
                        for (int k = 0; k < oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstSubParametro.Count; k++)
                        {
                            oConsultarRequest.accionRequest.listaParametros[i].subListaParametros[k] = new COMMON_HLRUDB.subListaParametros();
                            oConsultarRequest.accionRequest.listaParametros[i].nombreLista = oConsultaUDBRequest.oAccionRequest.lstParametro[k].nombreLista;
                            for (int l = 0; l < oConsultaUDBRequest.oAccionRequest.lstParametro[l].lstParametro.Count; l++)
                            {
                                oConsultarRequest.accionRequest.listaParametros[i].subListaParametros[k].parametro[l] = new COMMON_HLRUDB.subListaParametrosParametro();
                                oConsultarRequest.accionRequest.listaParametros[i].subListaParametros[k].parametro[l].campo = oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstSubParametro[k].lstParametro[l].campo;
                                oConsultarRequest.accionRequest.listaParametros[i].subListaParametros[k].parametro[l].valor = oConsultaUDBRequest.oAccionRequest.lstParametro[i].lstSubParametro[k].lstParametro[l].valor;
                            }
                        }
                    }
                }
                Claro.Web.Logging.Info(oConsultaUDBRequest.Audit.Session, oConsultaUDBRequest.Audit.Transaction, "[FIN] - lstParametro");
            }

            try
            {
                oconsultarResponse = Claro.Web.Logging.ExecuteMethod<COMMON_HLRUDB.consultarResponse>
                    (oConsultaUDBRequest.Audit.Session, oConsultaUDBRequest.Audit.Transaction, Configuration.ServiceConfiguration.COMMON_HLRUDB, () =>
                    { return Configuration.ServiceConfiguration.COMMON_HLRUDB.consultar(oConsultarRequest); });

                if (oconsultarResponse != null)
                {
                    if (oconsultarResponse.auditResponse != null)
                    {
                        oConsultaUDBResponse.oAuditResponse = new Entity.AuditResponse();
                        oConsultaUDBResponse.oAuditResponse.idTransaccion = oconsultarResponse.auditResponse.idTransaccion;
                        oConsultaUDBResponse.oAuditResponse.codigoRespuesta = oconsultarResponse.auditResponse.codRespuesta;
                        oConsultaUDBResponse.oAuditResponse.mensajeRespuesta = oconsultarResponse.auditResponse.msjRespuesta;
                        Claro.Web.Logging.Info(oConsultaUDBRequest.Audit.Session, oConsultaUDBRequest.Audit.Transaction, String.Format("codigoRespuesta: {0}, mensajeRespuesta: {1}", oConsultaUDBResponse.oAuditResponse.codigoRespuesta, oConsultaUDBResponse.oAuditResponse.mensajeRespuesta));
                    }
                    if (oconsultarResponse.accionResponse != null)
                    {
                        oConsultaUDBResponse.oAccionResponse = new Entity.Common.GetConsultaUDB.AccionUDB();
                        oConsultaUDBResponse.oAccionResponse.idAccion = oconsultarResponse.accionResponse.idAccion;
                        if (oconsultarResponse.accionResponse.listaParametros != null && oconsultarResponse.accionResponse.listaParametros.Length > 0)
                        {
                            oConsultaUDBResponse.oAccionResponse.lstParametro = new List<Entity.Common.GetConsultaUDB.ListaParametro>();

                            Claro.Web.Logging.Info(oConsultaUDBRequest.Audit.Session, oConsultaUDBRequest.Audit.Transaction, string.Format("[INICIO] - lstParametro: {0}", oconsultarResponse.accionResponse.listaParametros.Length));

                            for (int i = 0; i < oconsultarResponse.accionResponse.listaParametros.Length; i++)
                            {
                                Entity.Common.GetConsultaUDB.ListaParametro oListaParametro = new Entity.Common.GetConsultaUDB.ListaParametro();
                                oListaParametro.nombreLista = oconsultarResponse.accionResponse.listaParametros[i].nombreLista;

                                if (oconsultarResponse.accionResponse.listaParametros[i].parametro != null && oconsultarResponse.accionResponse.listaParametros[i].parametro.Length > 0)
                                {
                                    List<Entity.ItemOpcional> oListItemOpcional = new List<Entity.ItemOpcional>(oconsultarResponse.accionResponse.listaParametros[i].parametro.Length);
                                    for (int j = 0; j < oconsultarResponse.accionResponse.listaParametros[i].parametro.Length; j++)
                                    {
                                        Entity.ItemOpcional oItemOpcional = new Entity.ItemOpcional();
                                        oItemOpcional.campo = oconsultarResponse.accionResponse.listaParametros[i].parametro[j].campo;
                                        oItemOpcional.valor = oconsultarResponse.accionResponse.listaParametros[i].parametro[j].valor;
                                        oListItemOpcional.Add(oItemOpcional);
                                    }
                                    oListaParametro.lstParametro = oListItemOpcional;
                                }


                                if (oconsultarResponse.accionResponse.listaParametros[i].subListaParametros != null && oconsultarResponse.accionResponse.listaParametros[i].subListaParametros.Length > 0)
                                {
                                    List<Entity.Common.GetConsultaUDB.SubListaParametro> oListaSubListaParametro = new List<Entity.Common.GetConsultaUDB.SubListaParametro>();
                                    for (int k = 0; k < oconsultarResponse.accionResponse.listaParametros[i].subListaParametros.Length; k++)
                                    {
                                        Entity.Common.GetConsultaUDB.SubListaParametro oSubListaParametro = new Entity.Common.GetConsultaUDB.SubListaParametro();
                                        oSubListaParametro.nombreLista = oconsultarResponse.accionResponse.listaParametros[i].subListaParametros[k].nombreLista;
                                        List<Entity.ItemOpcional> lstParametro = new List<Entity.ItemOpcional>();
                                        for (int l = 0; l < oconsultarResponse.accionResponse.listaParametros[i].subListaParametros[k].parametro.Length; l++)
                                        {
                                            Entity.ItemOpcional oItemOpcional = new Entity.ItemOpcional();
                                            oItemOpcional.campo = oconsultarResponse.accionResponse.listaParametros[i].subListaParametros[k].parametro[l].campo;
                                            oItemOpcional.valor = oconsultarResponse.accionResponse.listaParametros[i].subListaParametros[k].parametro[l].valor;
                                            lstParametro.Add(oItemOpcional);
                                        }
                                        oSubListaParametro.lstParametro = lstParametro;
                                        oListaSubListaParametro.Add(oSubListaParametro);
                                    }
                                    oListaParametro.lstSubParametro = oListaSubListaParametro;
                                }
                                oConsultaUDBResponse.oAccionResponse.lstParametro.Add(oListaParametro);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Claro.Web.Logging.Error(oConsultaUDBRequest.Audit.Session, oConsultaUDBRequest.Audit.Transaction, e.Message);
            }

            return oConsultaUDBResponse;
        }


        /// <summary>
        /// Método que obtiene los parametros de clarify.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentType">Tipo de documento DNI, Carnet Extranjería, Pasaporte</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve la lista por el tipo de proceso.</returns>
        public static EntityClarify.GetDescriptionsResponse GetDescriptions(EntityClarify.GetDescriptionsRequest objGetDescriptionsRequest)
        {
            EntityClarify.GetDescriptionsResponse objGetDescriptionsResponse = null;
            try
            {
                Claro.SIACU.ProxyService.SIAC.ParamaetrosClarify.AuditRequestType objAuditRequestType = new ProxyService.SIAC.ParamaetrosClarify.AuditRequestType()
                {
                    usuarioAplicacion = objGetDescriptionsRequest.Audit.UserName,
                    ipAplicacion = objGetDescriptionsRequest.Audit.IPAddress,
                    nombreAplicacion = objGetDescriptionsRequest.Audit.ApplicationName,
                    idTransaccion = objGetDescriptionsRequest.Audit.Transaction
                };

                Claro.SIACU.ProxyService.SIAC.ParamaetrosClarify.parametrosClarifyRequest objparametrosClarifyRequest = new ProxyService.SIAC.ParamaetrosClarify.parametrosClarifyRequest()
                {

                    auditRequest = objAuditRequestType,
                    listaRequestOpcional = new ProxyService.SIAC.ParamaetrosClarify.ListaCamposOpcionalesTypeCampoOpcional[1]
                    {
                        new ProxyService.SIAC.ParamaetrosClarify.ListaCamposOpcionalesTypeCampoOpcional()
                        {
                            campo = string.Empty,
                            valor = string.Empty
    }
                    },
                    tipoProceso = objGetDescriptionsRequest.tipoProceso
                };


                Claro.SIACU.ProxyService.SIAC.ParamaetrosClarify.parametrosClarifyResponse objparametrosClarifyResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.ProxyService.SIAC.ParamaetrosClarify.parametrosClarifyResponse>(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, Configuration.ServiceConfiguration.PARAMATERCLARIFY, () =>
                {
                    return Configuration.ServiceConfiguration.PARAMATERCLARIFY.obtenerDescripciones(objparametrosClarifyRequest);
                });

                if (objparametrosClarifyResponse != null)
                {
                    objGetDescriptionsResponse = new EntityClarify.GetDescriptionsResponse();
                    objGetDescriptionsResponse.auditResponse = new Entity.AuditResponse()
                    {
                        codigoRespuesta = objparametrosClarifyResponse.auditResponse.codigoRespuesta,
                        idTransaccion = objparametrosClarifyResponse.auditResponse.idTransaccion,
                        mensajeRespuesta = objparametrosClarifyResponse.auditResponse.mensajeRespuesta
                    };
                    objGetDescriptionsResponse.datosParametroClarify = new List<EntityClarify.DatosParametroSiacType>();
                    foreach (var item in objparametrosClarifyResponse.listaDatosParametroClarify)
                    {
                        EntityClarify.DatosParametroSiacType objDatosParametroSiacType = new EntityClarify.DatosParametroSiacType()
                        {
                            tipoProceso = item.tipoProceso,
                            datoEvalua = item.datoEvalua,
                            parametro1 = item.parametro1,
                            parametro2 = item.parametro2,
                            estado = item.estado

                        };

                        objGetDescriptionsResponse.datosParametroClarify.Add(objDatosParametroSiacType);

                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, ex.Message);
            }
            return objGetDescriptionsResponse;

        }



    }

}
