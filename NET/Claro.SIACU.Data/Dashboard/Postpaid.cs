using Claro.Data;
using Claro.SAP;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Dashboard;
using Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines;
using Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts;
using Claro.SIACU.Entity.Dashboard.Postpaid;
using INVOICE = Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetInvoice;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount;
using Claro.SIACU.ProxyService.SIACPost.StateAccount;
using System;
using System.Collections.Generic;
using System.Data;
using KEY = Claro.ConfigurationManager;
using POSTPAID_ACCOUNT = Claro.SIACU.ProxyService.SIACPost.AccountDue;
using POSTPAID_BALANCE = Claro.SIACU.ProxyService.SIACPost.CreditBalance;
using POSTPAID_CONSULTCLIENT = Claro.SIACU.ProxyService.SIACPost.Customer;
using POSTPAID_FINANCE = Claro.SIACU.ProxyService.SIACPost.FinanceManagement;
using POSTPAID_HFC = Claro.SIACU.ProxyService.SIACPost.CustomerHFC;
using POSTPAID_IMRCONSULT = Claro.SIACU.ProxyService.SIACPost.IMR;
using POSTPAID_LTE = Claro.SIACU.ProxyService.SIACPost.CustomerLTE;
using POSTPAID_PRODUCTDETAIL = Claro.SIACU.ProxyService.SIACU.Post.Lines;
using POSTPAID_PRODUCTS = Claro.SIACU.ProxyService.SIACU.Post.Products;
using POSTPAID_RECHARGE = Claro.SIACU.ProxyService.SIACPost.Recharges;
using POSTPAID_SAP = Claro.SIACU.ProxyService.SIACPost.DatosSAP;
using POSTPAID_SERVICES = Claro.SIACU.ProxyService.SIACPost.PostpaidService;
using POSTPAID_STATE = Claro.SIACU.ProxyService.SIACPost.StateAccount;
using PREPAID_SERVICE = Claro.SIACU.ProxyService.SIACPre.Service;
using POSTPAID_CUSTOMERCONTACT = Claro.SIACU.ProxyService.SIACPost.CustomerContact;
using POSTPAID_VALIDATE4GLTE = Claro.SIACU.ProxyService.SIACPost.Validate4GLTE;
using POSTPAID_PACKAGE_ROAMING = Claro.SIACU.ProxyService.SIACPost.PackageRoaming;
using POSTPAID_HFC_IGV = Claro.SIACU.ProxyService.SIACPost.IGVHFC;
using POSTPAID_CUSTOMER_MG_FS = Claro.SIACU.ProxyService.SIACPost.GetCustomerManagementFS;
using POSTPAID_DOCUMENT = Claro.SIACU.ProxyService.SIACPost.DocumentoFisico;
using POSTPAID_DOCUMENTV3 = Claro.SIACU.ProxyService.SIACPost.DocumentoFisicoV3;
using POSTPAID_M2M = Claro.SIACU.ProxyService.SIACPost.RechargesM2M;
using POSTPAID_DETAILAJUSTE = Claro.SIACU.ProxyService.SIACPost.DetalleAjuste;
using POSTPREDATA = Claro.SIACU.ProxyService.SIAC.Post.DatosPrePost_V2;
using POSTPAID_PAQUETE = Claro.SIACU.ProxyService.SIACPre.PaqueteDatos;
using SECURITY_SERVICE = Claro.SIACU.ProxyService.SecurityService;
using SIACU_INTERAC = Claro.SIACU.ProxyService.SIACU.Interacciones;
using POSTPAID_RESTRICTVELOC = Claro.SIACU.ProxyService.SIACPost.RestricTethecVeloc;
using POSTPAID_CONSULTCLAVE = Claro.SIACU.ProxyService.SIAC.Claves;
using POSTPAID_STATUS_ACCOUNT = Claro.SIACU.ProxyService.SIACU.Post.BSS_StatusAccount;
using System.Globalization;
using System.ServiceModel;
using Claro.Entity;
using POSTPAID_CAMBIODATOS = Claro.SIACU.ProxyService.CambioDatosSiacWS;
using POSTPAID_ANNOTATION = Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion;
using Newtonsoft.Json;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetDataInvoiceAndDetailTobe;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryActions;
using System.Xml.Serialization;
using System.IO;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response;
using IMSILIST = Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI;
using System.Linq;
using ServiciosCatalogoTobe = Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Request;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections;
using POSTPAID_BLOQDESBLOQLINEA = Claro.SIACU.ProxyService.SIACPost.BloqDesbloqLineaPostpago;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountDP;
using Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetReasonCancelInvoice;
using Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile;
using RequestListaBloqueos = Claro.SIACU.Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Request;
using ResponseListaBloqueos = Claro.SIACU.Entity.Dashboard.Postpaid.GetListaBloqueoDesbloqueo.Response;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoceDetails;

namespace Claro.SIACU.Data.Dashboard
{
    public static class Postpaid
    {

        /// <summary>
        /// Método que obtiene el Número de cuenta o Telefono de servicio buscando por id del cliente, Número de Recibo o Número de chip.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strReceiptNumber">Número de recibo</param>
        /// <param name="strIccidNumber">Número de ICCID</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns>Devuelve cadena con valor de la cuenta.</returns>
        public static string GetAccountTelephoneCustomer(string strIdSession, string strTransaction, string strCustomerId, string strReceiptNumber, string strIccidNumber, out string strTelephone, out string strplataforma)
        {
            string strAccountCustomer;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AC_CUSTOMER_ID", DbType.String,255, ParameterDirection.Input, strCustomerId),
                new DbParameter("AC_NUM_RECIBO", DbType.String,255, ParameterDirection.Input,strReceiptNumber),
                new DbParameter("AC_ICCID", DbType.String,255, ParameterDirection.Input,strIccidNumber),
                new DbParameter("CURSOR_DATA_CLI", DbType.Object, ParameterDirection.Output),
                new DbParameter("AC_DN_NUM", DbType.String,255, ParameterDirection.Output),
                new DbParameter("AN_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("AC_MSG", DbType.String,255, ParameterDirection.Output)
            };

            strAccountCustomer = DbFactory.ExecuteScalarToString(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_DATCLI_X_CUST_NUMREC_ICCID, parameters, "CUENTA");
            strTelephone = parameters[4].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[4].Value.ToString());


            if (string.IsNullOrEmpty(strAccountCustomer) && string.IsNullOrEmpty(strTelephone))
            {
                strplataforma = string.Empty;
            }
            else
            {
                strplataforma = Claro.SIACU.Constants.ASIS;
            }
            return strAccountCustomer;
        }
        public static string GetAccountTelephoneCustomer(string strIdSession, string strTransaction, string strReceiptNumber, out string strTelephone, out string strplataforma)
        {
            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Metodo GetAccountTelephoneCustomer capa Data"));
            string strAccountCustomer = string.Empty, strCustomerId = string.Empty;


            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AC_CUSTOMER_ID", DbType.String,255, ParameterDirection.Input, string.Empty),
                new DbParameter("AC_NUM_RECIBO", DbType.String,255, ParameterDirection.Input,strReceiptNumber),
                new DbParameter("AC_ICCID", DbType.String,255, ParameterDirection.Input,string.Empty),
                new DbParameter("CURSOR_DATA_CLI", DbType.Object, ParameterDirection.Output),
                new DbParameter("AC_DN_NUM", DbType.String,255, ParameterDirection.Output),
                new DbParameter("AN_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("AC_MSG", DbType.String,255, ParameterDirection.Output)
            };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_DATCLI_X_CUST_NUMREC_ICCID, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    strAccountCustomer = Convert.ToString(reader["CUENTA"]);
                    strCustomerId = Convert.ToString(reader["CUSTOMER_ID"]);
                    Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("CUENTA: {0},CUSTOMER_ID : {1} ", strAccountCustomer, strCustomerId));
                }
            });
            strTelephone = strCustomerId;

            if (string.IsNullOrEmpty(strAccountCustomer) && string.IsNullOrEmpty(strTelephone))
            {
                strplataforma = string.Empty;
            }
            else
            {
                strplataforma = Claro.SIACU.Constants.ASIS;
            }
            return strAccountCustomer;
        }

        public static string GetAccountTelephoneCustomerTobe(Claro.Entity.AuditRequest audit, Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request request, out string strTelephone, out string strplataforma)
        {
            string strAccountCustomer = "";


            Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_CUENTA_TEL_CUST_TOBE, audit, request, false);

            if (response != null &&
                response.obtenerLineaPorIccidNroReciboResponse != null &&
                response.obtenerLineaPorIccidNroReciboResponse.responseAudit != null &&
                response.obtenerLineaPorIccidNroReciboResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.obtenerLineaPorIccidNroReciboResponse.responseData != null)
            {
                strAccountCustomer = response.obtenerLineaPorIccidNroReciboResponse.responseData.cuenta;
                strTelephone = response.obtenerLineaPorIccidNroReciboResponse.responseData.linea;
                strplataforma = Claro.SIACU.Constants.TOBE;

            }
            else
            {
                strTelephone = "";
                strplataforma = string.Empty;
            }
            return strAccountCustomer;
        }


        /// <summary>
        /// Método que  obtiene los datos del cliente por código de contrato en HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContract">Contrato</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomerByContractCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContract)
        {
            Entity.Dashboard.Postpaid.Customer oCustomer;
            POSTPAID_HFC.consultarDatosPorCodigoContratoEAIResponse oConsultationCustomerOut = Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarDatosPorCodigoContratoEAIResponse>
                 (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
                 {
                     return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarDatosPorCodigoContrato(new POSTPAID_HFC.consultarDatosPorCodigoContratoEAIRequest()
                     {
                         consultarDatosPorCodigoContratoEaiRequest = new POSTPAID_HFC.ConsultarDatosPorCodigoContratoEAIInput()
                         {
                             cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                             {
                                 idTransaccion = strIdTransaction,
                                 ipAplicacion = strIpApplication,
                                 nombreAplicacion = strApplicationName,
                                 usuarioAplicacion = strUserApplication
                             },
                             cuerpoRequest = new POSTPAID_HFC.CuerpoDCORequest()
                             {
                                 codigoContrato = strContract
                             }
                         }
                     });
                 });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationCustomerOut.consultarDatosPorCodigoContratoEaiResponse.cabeceraResponse;
            POSTPAID_HFC.CuerpoDCOResponse oBodyOutput = oConsultationCustomerOut.consultarDatosPorCodigoContratoEaiResponse.cuerpoResponse;
            POSTPAID_HFC.DatosContratoType[] oTempCustomer = oBodyOutput.listaDatosPorCodigoContrato;

            if (!oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                oCustomer = null;
            }
            else
            {
                oCustomer = new Entity.Dashboard.Postpaid.Customer();
                int lastIndex = oTempCustomer.Length - 1;

                if (oTempCustomer[lastIndex] != null && oTempCustomer.Length > 0)
                {
                    oCustomer.COD_TIPO_CLIENTE = Convert.ToString(oTempCustomer[lastIndex].codigoTipoCliente);
                    oCustomer.TIPO_CLIENTE = Convert.ToString(oTempCustomer[lastIndex].tipoCliente);
                    oCustomer.NOMBRES = Convert.ToString(oTempCustomer[lastIndex].nombre);
                    oCustomer.APELLIDOS = Convert.ToString(oTempCustomer[lastIndex].apellido);
                    oCustomer.NOMBRE_COMPLETO = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.NOMBRE_COMERCIAL = Convert.ToString(oTempCustomer[lastIndex].nombreComercial);
                    oCustomer.CUENTA = Convert.ToString(oTempCustomer[lastIndex].cuenta);
                    oCustomer.CUSTOMER_ID = Convert.ToString(oTempCustomer[lastIndex].customerId);
                    oCustomer.RAZON_SOCIAL = Convert.ToString(oTempCustomer[lastIndex].razonSocial);
                    oCustomer.TIPO_DOC = Convert.ToString(oTempCustomer[lastIndex].tipDocumento);
                    oCustomer.NRO_DOC = Convert.ToString(oTempCustomer[lastIndex].nroDocumento);
                    oCustomer.SEXO = Convert.ToString(oTempCustomer[lastIndex].sexo);
                    oCustomer.TELEFONO = Convert.ToString(oTempCustomer[lastIndex].telefonoref);
                    oCustomer.TELEF_REFERENCIA = Convert.ToString(oTempCustomer[lastIndex].telefonoref);
                    oCustomer.TELEFONO_CONTACTO = Convert.ToString(oTempCustomer[lastIndex].telefonoref2);
                    oCustomer.FAX = Convert.ToString(oTempCustomer[lastIndex].fax);
                    oCustomer.ESTADO_CIVIL = Convert.ToString(oTempCustomer[lastIndex].estadoCivil);
                    oCustomer.ESTADO_CIVIL_ID = Convert.ToString(oTempCustomer[lastIndex].estadoCivilId);
                    oCustomer.FECHA_NAC = Convert.ToString(oTempCustomer[lastIndex].fechaNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(oTempCustomer[lastIndex].lugarNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(oTempCustomer[lastIndex].nacionalidad);
                    oCustomer.DNI_RUC = Convert.ToString(oTempCustomer[lastIndex].rucDni);
                    oCustomer.REPRESENTANTE_LEGAL = Convert.ToString(oTempCustomer[lastIndex].repLegal);
                    oCustomer.EMAIL = Convert.ToString(oTempCustomer[lastIndex].email);
                    oCustomer.CARGO = Convert.ToString(oTempCustomer[lastIndex].cargo);
                    oCustomer.DOMICILIO_FAC = Convert.ToString(oTempCustomer[lastIndex].direccionFact);
                    oCustomer.REFERENCIA = Convert.ToString(oTempCustomer[lastIndex].urbanizacionFact);
                    oCustomer.URBANIZACION_FAC = Convert.ToString(oTempCustomer[lastIndex].urbanizacionFact);
                    oCustomer.DISTRITO = Convert.ToString(oTempCustomer[lastIndex].distritoFact);
                    oCustomer.DISTRITO_FAC = Convert.ToString(oTempCustomer[lastIndex].distritoFact);
                    oCustomer.PROVINCIA = Convert.ToString(oTempCustomer[lastIndex].provinciaFact);
                    oCustomer.PROVINCIA_FAC = Convert.ToString(oTempCustomer[lastIndex].provinciaFact);
                    oCustomer.POSTAL_FAC = Convert.ToString(oTempCustomer[lastIndex].codigoPostalFact);
                    oCustomer.DEPARTAMENTO = Convert.ToString(oTempCustomer[lastIndex].departamentoFact);
                    oCustomer.DEPARTEMENTO_FAC = Convert.ToString(oTempCustomer[lastIndex].departamentoFact);
                    oCustomer.PAIS_FAC = Convert.ToString(oTempCustomer[lastIndex].paisFact);
                    oCustomer.DIRECCION_DESPACHO = Convert.ToString(oTempCustomer[lastIndex].direccionInst);
                    oCustomer.URBANIZACION_LEGAL = Convert.ToString(oTempCustomer[lastIndex].urbanizacionInst);
                    oCustomer.DISTRITO_LEGAL = Convert.ToString(oTempCustomer[lastIndex].distritoInst);
                    oCustomer.PROVINCIA_LEGAL = Convert.ToString(oTempCustomer[lastIndex].provinciaInst);
                    oCustomer.POSTAL_LEGAL = Convert.ToString(oTempCustomer[lastIndex].codigoPostalInst);
                    oCustomer.PAIS_LEGAL = Convert.ToString(oTempCustomer[lastIndex].paisInst);
                    oCustomer.DEPARTEMENTO_LEGAL = Convert.ToString(oTempCustomer[lastIndex].departamentoInst);
                    oCustomer.FECHA_ACT = Convert.ToDate(oTempCustomer[lastIndex].fechaAct);
                    oCustomer.CICLO_FACTURACION = Convert.ToString(oTempCustomer[lastIndex].cicloFact);
                    oCustomer.FORMA_PAGO = Convert.ToString(oTempCustomer[lastIndex].formaPago);
                    oCustomer.CONTRATO_ID = Convert.ToString(strContract);
                    oCustomer.UBIGEO_FACT = Convert.ToString(oTempCustomer[lastIndex].ubigeoFact);
                    oCustomer.UBIGEO_INST = Convert.ToString(oTempCustomer[lastIndex].ubigeoInst);
                    oCustomer.CODIGO_PLANO_FACT = Convert.ToString(oTempCustomer[lastIndex].codigoPlanoFact);
                    oCustomer.CODIGO_PLANO_INST = Convert.ToString(oTempCustomer[lastIndex].codigoPlanoInst);
                    oCustomer.ASESOR = Convert.ToString(oTempCustomer[lastIndex].asesor);

                    oCustomer.origen = Claro.ConfigurationManager.AppSettings("ModalityBSCS7");

                    oCustomer.oCUENTA = new Account()
                    {
                        TIPO_CLIENTE = oTempCustomer[lastIndex].tipoCliente,
                        SEGMENTO = oTempCustomer[lastIndex].segmento,
                        CICLO_FACTURACION = oTempCustomer[lastIndex].cicloFact,
                        NICHO = Convert.ToString(oTempCustomer[lastIndex].nichoId),
                        ESTADO_CUENTA = oTempCustomer[lastIndex].estadoCuenta,
                        RESPONSABLE_PAGO = oTempCustomer[lastIndex].responPago,
                        LIMITE_CREDITO = Convert.ToString(oTempCustomer[lastIndex].limiteCredito),
                        FECHA_ACT = Convert.ToDate(oTempCustomer[lastIndex].fechaAct).ToString(),
                        CONSULTOR = Convert.ToString(oTempCustomer[lastIndex].consultor)
                    };

                    //Logica Segmento PROY-SEGMENTOVALOR 140351
                    string strDocumentType = KEY.AppSettings("strDocumentType");
                    string strDocumentLong = KEY.AppSettings("strDocumentLong");
                    string strCaracterRellenoNroDoc = KEY.AppSettings("strCaracterRellenoNroDoc");
                    string strSplitDNIRUC = KEY.AppSettings("strSplitDNIRUC");
                    string oTipoDoc = oCustomer.TIPO_DOC;

                    string[] strSplitSegmentoDocumentoF = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[0].Split(',');

                    string[] strSplitSegmentoDocumentoV = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[1].Split(',');

                    for (int i = 0; i < strSplitSegmentoDocumentoF.Length; i++)
                    {
                        if (oTipoDoc == strSplitSegmentoDocumentoF[i])
                        {
                            oTipoDoc = strSplitSegmentoDocumentoV[i];
                        }
                    }

                    Entity.Dashboard.Postpaid.Customer oCustomers = new Entity.Dashboard.Postpaid.Customer()
                    {
                        TIPO_DOC = strDocumentType,
                        NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)),
                        LONG_NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)).Length.ToString()
                    };
                    oCustomer.SEGMENTO2 = Claro.Web.Logging.ExecuteMethod<String>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetSegmentCustomerQuery(oCustomers, strIdSession, strUserApplication, strIpApplication, strIdTransaction, strApplicationName); });

                    //FIN PROY-SEGMENTOVALOR 140351
                }
            }

            return oCustomer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="audit"></param>
        /// <param name="intContratoID"></param>
        /// <returns></returns>
        public static Entity.Dashboard.Postpaid.Service GetDataLineTobeHFC(AuditRequest audit, int intContratoID, string coidPub)
        {
            Entity.Dashboard.Postpaid.Service objLine = null;
            Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Response.response response = null;
            List<Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Common.listaOpcional> listOptional = null;

            try
            {
                listOptional = new List<Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Common.listaOpcional>();
                listOptional.Add(new Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Common.listaOpcional()
                {
                    clave = "proceso",
                    valor = "fija",
                });


                Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Request.request()
                {
                    obtenerDatosContratoRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Request.obtenerDatosContratoRequest()
                    {
                        coId = Convert.ToString(intContratoID),
                        listaOpcional = listOptional
                    }
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataLineTobeHFC.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DATOS_LINEA_TOBE, audit, request, false);



            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (response != null &&
               response.obtenerDatosContratoResponse != null &&
               response.obtenerDatosContratoResponse.responseAudit != null &&
               response.obtenerDatosContratoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
               response.obtenerDatosContratoResponse.responseData != null
               )
            {
                objLine = new Entity.Dashboard.Postpaid.Service();
                objLine.FECHA_ESTADO = System.Convert.ToDateTime(response.obtenerDatosContratoResponse.responseData.fechaEstado);
                objLine.ESTADO_LINEA = response.obtenerDatosContratoResponse.responseData.descEstado;
                objLine.MOTIVO = response.obtenerDatosContratoResponse.responseData.descMotivo;
                objLine.PLAN = response.obtenerDatosContratoResponse.responseData.plan;
                objLine.PLAZO_CONTRATO = response.obtenerDatosContratoResponse.responseData.plazoContrato;
                objLine.NROICCID = response.obtenerDatosContratoResponse.responseData.iccid;
                objLine.NROIMSI = response.obtenerDatosContratoResponse.responseData.imsi;
                objLine.VENDEDOR = response.obtenerDatosContratoResponse.responseData.vendedor;
                objLine.CAMPANIA = response.obtenerDatosContratoResponse.responseData.campania;
                objLine.FEC_ACTIVACION = response.obtenerDatosContratoResponse.responseData.fechaAct.ToString();
                objLine.FLAG_PLATAFORMA = response.obtenerDatosContratoResponse.responseData.flagPlataforma;
                objLine.PIN1 = response.obtenerDatosContratoResponse.responseData.pin1;
                objLine.PIN2 = response.obtenerDatosContratoResponse.responseData.pin2;
                objLine.PUK1 = response.obtenerDatosContratoResponse.responseData.puk1;
                objLine.PUK2 = response.obtenerDatosContratoResponse.responseData.puk2;
                objLine.COD_PLAN_TARIFARIO = response.obtenerDatosContratoResponse.responseData.codplanTarifario.ToString();
                objLine.NRO_CELULAR = response.obtenerDatosContratoResponse.responseData.telefono;
                objLine.CONTRATO_ID = response.obtenerDatosContratoResponse.responseData.coId.ToString();
                objLine.topeConsumo = response.obtenerDatosContratoResponse.responseData.topeConsumo.ToString();
                objLine.bolsasAdicionales = response.obtenerDatosContratoResponse.responseData.bolsasAdicionales.ToString();
                objLine.COD_RETORNO = Claro.Constants.NumberOneString;
            }

            return objLine;




        }




        /// <summary>
        /// Método que obtiene los datos del servicio para HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="intContratoID">Id de contrato</param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        public static Entity.Dashboard.Postpaid.Service GetDataLineHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, int intContratoID)
        {
            Entity.Dashboard.Postpaid.Service objLine = new Entity.Dashboard.Postpaid.Service();

            POSTPAID_HFC.consultarServicioPorCodigoContratoEAIResponse oConsultationServicioOut =
             Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarServicioPorCodigoContratoEAIResponse>
             (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
             {
                 return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarServicioPorCodigoContrato(new POSTPAID_HFC.consultarServicioPorCodigoContratoEAIRequest()
                 {
                     consultarServicioPorCodigoContratoEaiRequest = new POSTPAID_HFC.ConsultarServicioPorCodigoContratoEAIInput()
                     {
                         cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                         {

                             idTransaccion = strIdTransaction,
                             ipAplicacion = strIpApplication,
                             nombreAplicacion = strApplicationName,
                             usuarioAplicacion = strUserApplication
                         },
                         cuerpoRequest = new POSTPAID_HFC.CuerpoCSCORequest()
                         {
                             codigoContrato = intContratoID.ToString()
                         }
                     }
                 });
             });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationServicioOut.consultarServicioPorCodigoContratoEaiResponse.cabeceraResponse;
            POSTPAID_HFC.ServicioPorCodigoContratoType[] oTempServicio = oConsultationServicioOut.consultarServicioPorCodigoContratoEaiResponse.cuerpoResponse.listaServicioPorCodigoContrato;

            if (oHeaderOutput.codigoRespuesta == Claro.Constants.NumberZeroString)
            {
                if (oTempServicio != null && oTempServicio.Length >= 1)
                {

                    objLine.CONTRATO_ID = Convert.ToString(oTempServicio[0].coid);
                    objLine.FECHA_ESTADO = Convert.ToDate(oTempServicio[0].fechaEstado);
                    objLine.ESTADO_LINEA = Convert.ToString(oTempServicio[0].estado);
                    objLine.MOTIVO = Convert.ToString(oTempServicio[0].motivoEstado);
                    objLine.PLAN = Convert.ToString(oTempServicio[0].planTarifario);
                    objLine.COD_PLAN_TARIFARIO = Convert.ToString(oTempServicio[0].codPlan);
                    objLine.PLAZO_CONTRATO = Convert.ToString(oTempServicio[0].plazoAcuerdo);
                    objLine.VENDEDOR = Convert.ToString(oTempServicio[0].vendedor);
                    objLine.CAMPANIA = Convert.ToString(oTempServicio[0].campana);
                    objLine.FEC_ACTIVACION = Convert.ToString(oTempServicio[0].fechaActivacion);
                    objLine.TELEFONIA = Convert.ToString(oTempServicio[0].telefonia);
                    objLine.INTERNET = Convert.ToString(oTempServicio[0].internet);
                    objLine.CABLETV = Convert.ToString(oTempServicio[0].cableTv);
                    objLine.NoEs3Play = Convert.ToString(Claro.Constants.NumberZeroString);
                    objLine.MSISDN = Convert.ToString(oTempServicio[0].msisdn);
                }
            }
            else
            {
                objLine.NoEs3Play = Convert.ToString(Claro.Constants.NumberOneString);
            }

            return objLine;
        }

        /// <summary>
        /// Método que obtiene el código del cliente por linea HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strFlag">Flag</param>
        /// <param name="strValue">Valor</param>
        /// <returns>Devuelve cadena con valor de código de cliente.</returns>
        public static string GetCodeByAccountLineHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strFlag, string strValue)
        {
            string strCode;

            POSTPAID_HFC.consultarCodigoClientePorLineaCuentaEAIResponse oConsultationCustomerOut =
             Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarCodigoClientePorLineaCuentaEAIResponse>
             (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
             {
                 return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarCodigoClientePorLineaCuenta(new POSTPAID_HFC.consultarCodigoClientePorLineaCuentaEAIRequest()
                 {
                     consultarCodigoClientePorLineaCuentaEaiRequest = new POSTPAID_HFC.ConsultarCodigoClientePorLineaCuentaEAIInput()
                     {
                         cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                         {
                             idTransaccion = strIdTransaction,
                             ipAplicacion = strIpApplication,
                             nombreAplicacion = strApplicationName,
                             usuarioAplicacion = strUserApplication
                         },
                         cuerpoRequest = new POSTPAID_HFC.CuerpoCCLCRequest()
                         {
                             flag = strFlag,
                             valor = strValue
                         },
                     }
                 });
             });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationCustomerOut.consultarCodigoClientePorLineaCuentaEaiResponse.cabeceraResponse;
            POSTPAID_HFC.CuerpoCCLCResponse oBodyOutput = oConsultationCustomerOut.consultarCodigoClientePorLineaCuentaEaiResponse.cuerpoResponse;

            if (oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                strCode = oBodyOutput.codigoCliente;
            }
            else
            {
                strCode = "";
            }

            return strCode;
        }

        /// <summary>
        /// Método que obtiene si es satisfactoria o no la consulta al sevicio EAI por codigo de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strCodeCliente">Código de cliente</param>
        /// <param name="lstServiceGSM">Listado de servicio GSM</param>
        /// <param name="lstServiceISDN">Listado de servicio ISDN</param>
        /// <returns>Devuelve verdadero, si se obtuvo la información solicitada.</returns>
        public static bool GetServicesByCustomerCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strCodeCliente, ref List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstServiceGSM, ref List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstServiceISDN)
        {
            POSTPAID_HFC.consultarServicioPorCodigoClienteEAIResponse oConsultationServicioOut =
             Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarServicioPorCodigoClienteEAIResponse>
             (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
             {
                 return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarServicioPorCodigoCliente(new POSTPAID_HFC.consultarServicioPorCodigoClienteEAIRequest()
                 {
                     consultarServicioPorCodigoClienteEaiRequest = new POSTPAID_HFC.ConsultarServicioPorCodigoClienteEAIInput()
                     {
                         cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                         {
                             idTransaccion = strIdTransaction,
                             ipAplicacion = strIpApplication,
                             nombreAplicacion = strApplicationName,
                             usuarioAplicacion = strUserApplication
                         },
                         cuerpoRequest = new POSTPAID_HFC.CuerpoCSCLRequest()
                         {
                             codigoCliente = strCodeCliente
                         },
                     }
                 });
             });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationServicioOut.consultarServicioPorCodigoClienteEaiResponse.cabeceraResponse;
            POSTPAID_HFC.DatosGsmType[] oTempServicioGSM = oConsultationServicioOut.consultarServicioPorCodigoClienteEaiResponse.cuerpoResponse.listaServicioPorCodigoCliente.listaDatosGsm;
            POSTPAID_HFC.DatosIsdnType[] oTempServicioISDN = oConsultationServicioOut.consultarServicioPorCodigoClienteEaiResponse.cuerpoResponse.listaServicioPorCodigoCliente.listaDatosIsdn;

            bool esCorrecto;

            if (oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (oTempServicioGSM != null)
                {
                    foreach (var item in oTempServicioGSM)
                    {
                        lstServiceGSM.Add(new Claro.SIACU.Entity.Dashboard.Postpaid.Service
                            {
                                CODID = Convert.ToString(item.coId)
                            });
                    }
                }

                if (oTempServicioISDN != null)
                {
                    foreach (var item in oTempServicioISDN)
                    {
                        lstServiceISDN.Add(new Claro.SIACU.Entity.Dashboard.Postpaid.Service
                            {
                                CODID = Convert.ToString(item.coId)
                            });
                    }
                }

                esCorrecto = true;
            }
            else
            {
                esCorrecto = false;
            }

            return esCorrecto;
        }

        /// <summary>
        /// Método que obtiene los datos del cliente por el código de contrato en HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomerByCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strCustomerId)
        {
            Entity.Dashboard.Postpaid.Customer oCustomer = new Entity.Dashboard.Postpaid.Customer();
            POSTPAID_HFC.consultarDatosPorCodigoClienteEAIResponse oConsultationCustomerOut =
               Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarDatosPorCodigoClienteEAIResponse>
               (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
               {
                   return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarDatosPorCodigoCliente(new POSTPAID_HFC.consultarDatosPorCodigoClienteEAIRequest()
                   {
                       consultarDatosPorCodigoClienteEaiRequest = new POSTPAID_HFC.ConsultarDatosPorCodigoClienteEAIInput()
                       {
                           cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                           {
                               idTransaccion = strIdTransaction,
                               ipAplicacion = strIpApplication,
                               nombreAplicacion = strApplicationName,
                               usuarioAplicacion = strUserApplication
                           },
                           cuerpoRequest = new POSTPAID_HFC.CuerpoDCLRequest()
                           {
                               codigoCliente = strCustomerId
                           }
                       }
                   });
               });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationCustomerOut.consultarDatosPorCodigoClienteEaiResponse.cabeceraResponse;
            POSTPAID_HFC.CuerpoDCLResponse oBodyOutput = oConsultationCustomerOut.consultarDatosPorCodigoClienteEaiResponse.cuerpoResponse;
            POSTPAID_HFC.DatosClienteType[] oTempCustomer = oBodyOutput.listaDatosPorCodigoCliente;

            if (!oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                return null;
            }
            else
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {

                    oCustomer.NOMBRES = Convert.ToString(oTempCustomer[i].nombre);
                    oCustomer.APELLIDOS = Convert.ToString(oTempCustomer[i].apellido);
                    oCustomer.NOMBRE_COMPLETO = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.CUENTA = Convert.ToString(oTempCustomer[i].cuenta);
                    oCustomer.CUSTOMER_ID = Convert.ToString(oTempCustomer[i].customerId);
                    oCustomer.RAZON_SOCIAL = Convert.ToString(oTempCustomer[i].razonSocial);
                    oCustomer.TIPO_DOC = Convert.ToString(oTempCustomer[i].tipDocumento);
                    oCustomer.NRO_DOC = Convert.ToString(oTempCustomer[i].nroDocumento);
                    oCustomer.SEXO = Convert.ToString(oTempCustomer[i].sexo);
                    oCustomer.TELEFONO = Convert.ToString(oTempCustomer[i].telefonoref);
                    oCustomer.TELEF_REFERENCIA = Convert.ToString(oTempCustomer[i].telefonoref);
                    oCustomer.FAX = Convert.ToString(oTempCustomer[i].fax);
                    oCustomer.ESTADO_CIVIL = Convert.ToString(oTempCustomer[i].estadoCivil);
                    oCustomer.ESTADO_CIVIL_ID = Convert.ToString(oTempCustomer[i].estadoCivilId);
                    oCustomer.FECHA_NAC = Convert.ToString(oTempCustomer[i].fechaNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(oTempCustomer[i].lugarNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(oTempCustomer[i].nacionalidad);
                    oCustomer.DNI_RUC = Convert.ToString(oTempCustomer[i].rucDni);
                    oCustomer.REPRESENTANTE_LEGAL = Convert.ToString(oTempCustomer[i].repLegal);
                    oCustomer.EMAIL = Convert.ToString(oTempCustomer[i].email);
                    oCustomer.CARGO = Convert.ToString(oTempCustomer[i].cargo);
                    oCustomer.DOMICILIO_FAC = Convert.ToString(oTempCustomer[i].direccionFact);
                    oCustomer.REFERENCIA = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                    oCustomer.URBANIZACION_FAC = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                    oCustomer.DISTRITO = Convert.ToString(oTempCustomer[i].distritoFact);
                    oCustomer.DISTRITO_FAC = Convert.ToString(oTempCustomer[i].distritoFact);
                    oCustomer.PROVINCIA = Convert.ToString(oTempCustomer[i].provinciaFact);
                    oCustomer.PROVINCIA_FAC = Convert.ToString(oTempCustomer[i].provinciaFact);
                    oCustomer.POSTAL_FAC = Convert.ToString(oTempCustomer[i].codigoPostalFact);
                    oCustomer.DEPARTAMENTO = Convert.ToString(oTempCustomer[i].departamentoFact);
                    oCustomer.DEPARTEMENTO_FAC = Convert.ToString(oTempCustomer[i].departamentoFact);
                    oCustomer.PAIS_FAC = Convert.ToString(oTempCustomer[i].paisFact);
                    oCustomer.FORMA_PAGO = Convert.ToString(oTempCustomer[i].formaPago);
                    oCustomer.ASESOR = Convert.ToString(oTempCustomer[i].asesor);
                    oCustomer.UBIGEO_FACT = Convert.ToString(oTempCustomer[i].ubigeoFact);
                    oCustomer.UBIGEO_INST = Convert.ToString(oTempCustomer[i].ubigeoInst);
                    oCustomer.CODIGO_PLANO_FACT = Convert.ToString(oTempCustomer[i].codigoPlanoFact);
                    oCustomer.CODIGO_PLANO_INST = Convert.ToString(oTempCustomer[i].codigoPlanoInst);
                    oCustomer.FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct);
                    oCustomer.oCUENTA = new Account()
                    {
                        TIPO_CLIENTE = oTempCustomer[i].tipoCliente,
                        SEGMENTO = oTempCustomer[i].segmento,
                        CICLO_FACTURACION = oTempCustomer[i].cicloFact,
                        NICHO = Convert.ToString(oTempCustomer[i].nichoId),
                        ESTADO_CUENTA = oTempCustomer[i].estadoCuenta,
                        RESPONSABLE_PAGO = oTempCustomer[i].responPago,
                        LIMITE_CREDITO = Convert.ToString(oTempCustomer[i].limiteCredito),
                        FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct).ToString(),
                        CONSULTOR = Convert.ToString(oTempCustomer[i].consultor)
                    };
                }
                return oCustomer;
            }
        }

        /// <summary>
        /// Método que obtiene una lista con los datos del servicio por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intCustomerId">Id de customer</param>
        /// <returns>Devuelve listado de objetos Service con información del servicio.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetServiceByCustomerCode(string strIdSession, string strTransaction, int intCustomerId)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService;


            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.Int32, ParameterDirection.Input, intCustomerId),
                new DbParameter("P_CURSOR_ISDN", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,255, ParameterDirection.Output)
            };

            lstService = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SIUNSS_DATOS_SERVICIOS, parameters);
            return lstService;

        }
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetServiceByCustomerCodeTobe(AuditRequest audit, string csIdPub)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = new List<Entity.Dashboard.Postpaid.Service>();
            Entity.Dashboard.Postpaid.Legacy.GetServiceByCustomerCode.Response.response response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetServiceByCustomerCode.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetServiceByCustomerCode.Request.request()
                {
                    consultarDetProductoRequest = new Entity.Dashboard.Postpaid.Legacy.GetServiceByCustomerCode.Request.ConsultarDetProductoRequest()
                    {
                        csIdPub = csIdPub
                    }

                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetServiceByCustomerCode.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_SERVICIOS_POR_CSIDPUB, audit, request, false);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
                throw;

            }

            if (response != null &&
                response.consultarDetProductoResponse != null &&
                response.consultarDetProductoResponse.responseAudit != null &&
                response.consultarDetProductoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.consultarDetProductoResponse.responseData != null
                )
            {
                var listaLineas = response.consultarDetProductoResponse.responseData.listaLineas;
                listaLineas.ForEach(x =>
                {

                    lstService.Add(new Claro.SIACU.Entity.Dashboard.Postpaid.Service()
                    {
                        ESTADO_LINEA = x.coStatusDescription,
                        PLAN = x.plan,
                        NRO_CELULAR = x.dirNum,
                        CONTRATO_ID = x.coId,
                        TIPO_PRODUCTO = x.typeProduct,
                        COID_PUB = x.coIdPub



                    });
                });
            }

            return lstService;

        }

        /// <summary>
        /// Método que obtiene una lista con los datos del teléfono  por código de contrato HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información del servicio.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetTelephoneByContractCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = new List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>();

            POSTPAID_HFC.consultarListaTelefonoPorCodigoContratoEAIResponse oConsultationListOut =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarListaTelefonoPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarListaTelefonoPorCodigoContrato(new POSTPAID_HFC.consultarListaTelefonoPorCodigoContratoEAIRequest()
                {
                    consultarListaTelefonoPorCodigoContratoEaiRequest = new POSTPAID_HFC.ConsultarListaTelefonoPorCodigoContratoEAIInput()
                    {
                        cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                        {
                            idTransaccion = strIdTransaction,
                            ipAplicacion = strIpApplication,
                            nombreAplicacion = strApplicationName,
                            usuarioAplicacion = strUserApplication
                        },
                        cuerpoRequest = new POSTPAID_HFC.CuerpoCLTCRequest()
                        {
                            codigoContrato = strContractId
                        }
                    }
                });

            });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationListOut.consultarListaTelefonoPorCodigoContratoEaiResponse.cabeceraResponse;
            POSTPAID_HFC.TelefonoPorCodigoContratoType[] oTempCustomer = oConsultationListOut.consultarListaTelefonoPorCodigoContratoEaiResponse.cuerpoResponse.listaTelefonoPorCodigoContrato;

            if (oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    Claro.SIACU.Entity.Dashboard.Postpaid.Service objService = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                    objService.NRO_CELULAR = Convert.ToString(oTempCustomer[i].dnNum);
                    objService.ESTADO_LINEA = Convert.ToString(oTempCustomer[i].estadoLinea);
                    lstService.Add(objService);
                }
            }

            return lstService;
        }

        /// <summary>
        /// Método que obtiene una lista con los datos del teléfono  por código de contrato HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información del servicio.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetTelephoneByContractCodeHFCTOBE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = new List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>();

            try
            {
                Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLines.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataLines.Request.request();
                request.listarLineasRequestType = new Entity.Dashboard.Postpaid.Legacy.GetDataLines.Request.listarLineasRequestType
                {
                    coId = strContractId
                };

                Claro.Entity.AuditRequest audit = new AuditRequest();
                audit.Transaction = strIdTransaction;
                audit.ApplicationName = strApplicationName;
                audit.IPAddress = strIpApplication;
                audit.Session = strIdSession;
                audit.UserName = strUserApplication;

                Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLines.Response.response response = RestService.PostInvoque<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetDataLines.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.GET_LISTAR_LINEAS, audit, request, false);


                Claro.Web.Logging.Info(strIdSession, strIdTransaction, "GetTelephoneByContractCodeHFCTOBE response:" + response.listarLineasResponseType.responseStatus.codigoRespuesta);

                 if (response != null &&
                    response.listarLineasResponseType != null &&
                    response.listarLineasResponseType.responseStatus != null &&
                    response.listarLineasResponseType.responseStatus.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.listarLineasResponseType.responseData != null &&
                    response.listarLineasResponseType.responseData.listaTelefonoType != null)
                 {
                     foreach (var oTelefonoType in response.listarLineasResponseType.responseData.listaTelefonoType)
                     {
                         Claro.SIACU.Entity.Dashboard.Postpaid.Service objService = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                         objService.NRO_CELULAR = oTelefonoType.numero;
                         objService.ESTADO_LINEA = oTelefonoType.estado;
                         lstService.Add(objService);
                     }
                 }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, "GetTelephoneByContractCodeHFCTOBE error: " + ex.Message);
                return lstService;
            }

            return lstService;
        }



        /// <summary>
        /// Método que obtiene una lista por la consulta de Lineas Desactivas por Codigo de Contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de servicio.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetLineByContractCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = new List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>();

            POSTPAID_HFC.consultarLineasDesactivasPorCodigoContratoEAIResponse oConsultationListOut =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarLineasDesactivasPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarLineasDesactivasPorCodigoContrato(new POSTPAID_HFC.consultarLineasDesactivasPorCodigoContratoEAIRequest()
                {
                    consultarLineasDesactivasPorCodigoContratoEaiRequest = new POSTPAID_HFC.ConsultarLineasDesactivasPorCodigoContratoEAIInput()
                    {
                        cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                        {
                            idTransaccion = strIdTransaction,
                            ipAplicacion = strIpApplication,
                            nombreAplicacion = strApplicationName,
                            usuarioAplicacion = strUserApplication
                        },
                        cuerpoRequest = new POSTPAID_HFC.CuerpoCLDCRequest()
                        {
                            codigoContrato = strContractId
                        }
                    }
                });
            });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationListOut.consultarLineasDesactivasPorCodigoContratoEaiResponse.cabeceraResponse;
            POSTPAID_HFC.LineasDesactivasPorCodigoContratoType[] oTempCustomer = oConsultationListOut.consultarLineasDesactivasPorCodigoContratoEaiResponse.cuerpoResponse.listaLineasDesactivasPorCodigoContrato;

            if (oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                Claro.SIACU.Entity.Dashboard.Postpaid.Service objService;

                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    objService = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                    objService.MSISDN = Convert.ToString(oTempCustomer[i].msisdn);
                    objService.PROVIDER_ID = Convert.ToString(oTempCustomer[i].idProducto);
                    objService.FEC_ACTIVACION = Convert.ToString(oTempCustomer[i].fechaActivacion);
                    objService.FEC_DESACTIVACION = Convert.ToString(oTempCustomer[i].fechaDesactivacion);

                    lstService.Add(objService);
                }
            }

            return lstService;
        }

        /// <summary>
        /// Método que obtiene los datos del cliente por Nro de cuenta o nro Teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strAccountCustomer">Cuenta de cliente</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomer(string strIdSession, string strTransaction, string strAccountCustomer, string strTelephone)
        {
            Entity.Dashboard.Postpaid.Customer oCustomer;

            try
            {
                POSTPAID_CONSULTCLIENT.datosClienteResponse objClienteResponse =
               Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.datosClienteResponse>(strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () => { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.datosCliente(strAccountCustomer, strTelephone); });


                if (objClienteResponse != null && (objClienteResponse.cliente != null && objClienteResponse.cliente.Length > 0))
                {
                    ProxyService.SIACPost.Customer.cliente objCliente = objClienteResponse.cliente[0];

                    oCustomer = new Entity.Dashboard.Postpaid.Customer();

                    oCustomer.RAZON_SOCIAL = objCliente.razonSocial;
                    oCustomer.NOMBRE_COMERCIAL = objCliente.nomb_comercial;
                    oCustomer.DNI_RUC = objCliente.ruc_dni;
                    oCustomer.NRO_DOC = objCliente.num_doc;
                    oCustomer.CUENTA = Convert.ToString(objCliente.cuenta);
                    oCustomer.REPRESENTANTE_LEGAL = Convert.ToString(objCliente.rep_legal);
                    oCustomer.TELEF_REFERENCIA = Convert.ToString(objCliente.telef_principal);
                    oCustomer.EMAIL = Convert.ToString(objCliente.email);
                    oCustomer.SEGMENTO = Convert.ToString(objCliente.segmento);
                    oCustomer.NOMBRES = Convert.ToString(objCliente.nombre);
                    oCustomer.APELLIDOS = Convert.ToString(objCliente.apellidos);
                    oCustomer.NOMBRE_COMPLETO = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.CUSTOMER_ID = objCliente.customerId.ToString();
                    oCustomer.CARGO = objCliente.cargo;
                    oCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(objCliente.lug_nac);
                    oCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(objCliente.nacionalidad);
                    oCustomer.FECHA_NAC = objCliente.fecha_nac.ToString() != "" ? Convert.ToDate(objCliente.fecha_nac).ToString("dd/MM/yyyy") : "";
                    oCustomer.ESTADO_CIVIL = Convert.ToString(objCliente.estado_civil);
                    oCustomer.ESTADO_CIVIL_ID = Convert.ToString(objCliente.estado_civil_id);
                    oCustomer.CONTACTO_CLIENTE = objCliente.contacto_cliente;
                    oCustomer.TIPO_DOC = objCliente.tip_doc;
                    oCustomer.FAX = objCliente.fax;
                    oCustomer.TELEFONO_CONTACTO = objCliente.telef_contacto;
                    oCustomer.REFERENCIA = objCliente.urbanizacion_fac;
                    oCustomer.PAIS = objCliente.pais_fac;
                    oCustomer.DEPARTAMENTO = objCliente.departamento_fac;
                    oCustomer.SEXO = objCliente.sexo;
                    oCustomer.PROVINCIA = objCliente.provincia_fac;
                    oCustomer.DISTRITO = objCliente.distrito_fac;
                    oCustomer.PAIS_FAC = objCliente.pais_fac;
                    oCustomer.DOMICILIO_FAC = objCliente.direccion_fac;
                    oCustomer.URBANIZACION_FAC = objCliente.urbanizacion_fac;
                    oCustomer.DISTRITO_FAC = objCliente.distrito_fac;
                    oCustomer.PROVINCIA_FAC = objCliente.provincia_fac;
                    oCustomer.POSTAL_FAC = objCliente.cod_postal_fac;
                    oCustomer.DEPARTEMENTO_FAC = objCliente.departamento_fac;
                    oCustomer.PAIS_LEGAL = objCliente.pais_leg;
                    oCustomer.DOMICILIO_LEGAL = objCliente.direccion_leg;
                    oCustomer.URBANIZACION_LEGAL = objCliente.urbanizacion_leg;
                    oCustomer.DISTRITO_LEGAL = objCliente.distrito_leg;
                    oCustomer.PROVINCIA_LEGAL = objCliente.provincia_leg;
                    oCustomer.POSTAL_LEGAL = objCliente.cod_postal_leg;
                    oCustomer.DEPARTEMENTO_LEGAL = objCliente.departamento_leg;
                    oCustomer.CONTRATO_ID = Convert.ToString(objCliente.co_id);
                    oCustomer.FORMA_PAGO = Convert.ToString(objCliente.forma_pago);
                    oCustomer.TIPO_CLIENTE = objCliente.tipo_cliente;
                    oCustomer.COD_TIPO_CLIENTE = Convert.ToString(objCliente.codigo_tipo_cliente);
                    oCustomer.ASESOR = objCliente.asesor;
                    oCustomer.FECHA_ACT = Convert.ToDate(objCliente.fecha_act);
                    oCustomer.oCUENTA = new Account();
                    oCustomer.oCUENTA.TIPO_CLIENTE = objCliente.tipo_cliente;
                    oCustomer.oCUENTA.SEGMENTO = objCliente.segmento;
                    oCustomer.oCUENTA.CICLO_FACTURACION = objCliente.ciclo_fac;
                    oCustomer.oCUENTA.NICHO = Convert.ToString(objCliente.nicho_id);
                    oCustomer.oCUENTA.ESTADO_CUENTA = objCliente.status_cuenta;
                    oCustomer.oCUENTA.RESPONSABLE_PAGO = objCliente.respon_pago;
                    oCustomer.oCUENTA.CONSULTOR = Convert.ToString(objCliente.consultor);
                    oCustomer.oCUENTA.CONSULTOR_ACCOUNT = Convert.ToString(objCliente.consultor);
                    oCustomer.oCUENTA.LIMITE_CREDITO = Convert.ToString(objCliente.limite_credito);
                    oCustomer.oCUENTA.TOTAL_LINEAS = Convert.ToString(objCliente.num_lineas);
                    oCustomer.oCUENTA.TOTAL_CUENTAS = Convert.ToString(objCliente.num_cuentas);
                    oCustomer.oCUENTA.FECHA_ACT = objCliente.fecha_act.ToString();
                    oCustomer.MODALIDAD = objCliente.modalidad.ToString();
                    oCustomer.oCUENTA.MODALIDAD = objCliente.modalidad.ToString();
                    oCustomer.origen = Claro.ConfigurationManager.AppSettings("ModalityBSCS7");
                    oCustomer.oCUENTA.plataformaAT = Claro.SIACU.Constants.ASIS;

                }
                else
                {
                    oCustomer = null;
                }


            }
            catch (Exception ex)
            {
                oCustomer = null;
            }

            return oCustomer;
        }
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomerTobe(Claro.Entity.AuditRequest audit, Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.request request, string strAccountCustomer, string strTelephone)
        {
            Entity.Dashboard.Postpaid.Customer oCustomer;

            try
            {

                Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DATOS_CLIENTE_TOBE, audit, request, false);

                if (response != null &&
                    response.obtenerDatosClienteResponse != null &&
                    response.obtenerDatosClienteResponse.responseAudit != null &&
                    response.obtenerDatosClienteResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.obtenerDatosClienteResponse.responseData != null &&
                    response.obtenerDatosClienteResponse.responseData.clienteData != null &&
                    response.obtenerDatosClienteResponse.responseData.cuentaData != null
                    )
                {
                    oCustomer = new Entity.Dashboard.Postpaid.Customer();

                    oCustomer.RAZON_SOCIAL = response.obtenerDatosClienteResponse.responseData.clienteData.razonSocial;
                    oCustomer.NOMBRE_COMERCIAL = response.obtenerDatosClienteResponse.responseData.clienteData.nombComercial;
                    oCustomer.DNI_RUC = response.obtenerDatosClienteResponse.responseData.clienteData.rucDni;
                    oCustomer.NRO_DOC = response.obtenerDatosClienteResponse.responseData.clienteData.numDoc;
                    oCustomer.CUENTA = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.cuenta);
                    oCustomer.TELEF_REFERENCIA = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.telefPrincipal);
                    oCustomer.EMAIL = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.email);
                    oCustomer.SEGMENTO = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.segmento);
                    oCustomer.NOMBRES = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.nombre);
                    oCustomer.APELLIDOS = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.apellidos);
                    oCustomer.NOMBRE_COMPLETO = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.REPRESENTANTE_LEGAL = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.CUSTOMER_ID = response.obtenerDatosClienteResponse.responseData.clienteData.customerId.ToString();
                    oCustomer.CARGO = response.obtenerDatosClienteResponse.responseData.clienteData.cargo;
                    oCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.lugNac);
                    oCustomer.FECHA_NAC = response.obtenerDatosClienteResponse.responseData.clienteData.fechaNac.ToString() != "" ? Convert.ToDate(response.obtenerDatosClienteResponse.responseData.clienteData.fechaNac).ToString("dd/MM/yyyy") : "";
                    oCustomer.ESTADO_CIVIL = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.estadoCivil);
                    oCustomer.ESTADO_CIVIL_ID = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.estadoCivilId);
                    oCustomer.CONTACTO_CLIENTE = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.TIPO_DOC = response.obtenerDatosClienteResponse.responseData.clienteData.tipDoc;
                    oCustomer.TIPO_DOC_ID = response.obtenerDatosClienteResponse.responseData.clienteData.tipDocId;
                    oCustomer.SEXO = response.obtenerDatosClienteResponse.responseData.clienteData.sexo;
                    oCustomer.FAX = response.obtenerDatosClienteResponse.responseData.clienteData.fax;
                    oCustomer.TELEFONO_CONTACTO = response.obtenerDatosClienteResponse.responseData.clienteData.telefContacto;
                    oCustomer.REFERENCIA = response.obtenerDatosClienteResponse.responseData.clienteData.dirReferenciaFac;
                    oCustomer.PAIS = response.obtenerDatosClienteResponse.responseData.clienteData.paisFac;
                    oCustomer.DEPARTAMENTO = response.obtenerDatosClienteResponse.responseData.clienteData.departamentoFac;
                    oCustomer.PROVINCIA = response.obtenerDatosClienteResponse.responseData.clienteData.provinciaFac;
                    oCustomer.DISTRITO = response.obtenerDatosClienteResponse.responseData.clienteData.distritoFac;
                    oCustomer.PAIS_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.paisFac;
                    oCustomer.DOMICILIO_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.direccionFac;
                    oCustomer.URBANIZACION_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.dirReferenciaFac;
                    oCustomer.DISTRITO_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.distritoFac;
                    oCustomer.PROVINCIA_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.provinciaFac;
                    oCustomer.POSTAL_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.codPostalFac;
                    oCustomer.DEPARTEMENTO_FAC = response.obtenerDatosClienteResponse.responseData.clienteData.departamentoFac;
                    oCustomer.PAIS_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.paisLegal;
                    oCustomer.DOMICILIO_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.direccionLegal;
                    oCustomer.URBANIZACION_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.direccionReferenciaLegal;
                    oCustomer.DISTRITO_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.distritoLegal;
                    oCustomer.PROVINCIA_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.provinciaLegal;
                    oCustomer.POSTAL_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.codigoPostalLegal;
                    oCustomer.DEPARTEMENTO_LEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.departamentoLegal;
                    oCustomer.CONTRATO_ID = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.coId);
                    oCustomer.FORMA_PAGO = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.formaPago);
                    oCustomer.TIPO_CLIENTE = response.obtenerDatosClienteResponse.responseData.cuentaData.tipoCliente;
                    oCustomer.ASESOR = response.obtenerDatosClienteResponse.responseData.clienteData.asesor;
                    oCustomer.FECHA_ACT = Convert.ToDate(response.obtenerDatosClienteResponse.responseData.clienteData.fechaAct);
                    oCustomer.coIdPub = response.obtenerDatosClienteResponse.responseData.clienteData.coIdPub;
                    oCustomer.csIdPub = response.obtenerDatosClienteResponse.responseData.clienteData.csIdPub;
                    oCustomer.flagConvivencia = response.obtenerDatosClienteResponse.responseData.clienteData.flagConvivencia;
                    oCustomer.DIRECCIONREFERENCIALLEGAL = response.obtenerDatosClienteResponse.responseData.clienteData.direccionReferenciaLegal;
                    oCustomer.APELLIDO_PAT_TOBE = response.obtenerDatosClienteResponse.responseData.clienteData.apellidoPaterno;
                    oCustomer.APELLIDO_MAT_TOBE = response.obtenerDatosClienteResponse.responseData.clienteData.apellidoMaterno;
                    oCustomer.indicadorCambioNumero = response.obtenerDatosClienteResponse.responseData.clienteData.indicadorCambioNumero;
                    oCustomer.LUGAR_NACIMIENTO_ID = response.obtenerDatosClienteResponse.responseData.clienteData.nacionalidadId;
                    oCustomer.oCUENTA = new Account();
                    oCustomer.COD_TIPO_CLIENTE = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.codigoTipoCliente);
                    oCustomer.oCUENTA.TIPO_CLIENTE = response.obtenerDatosClienteResponse.responseData.cuentaData.tipoCliente;
                    oCustomer.oCUENTA.SEGMENTO = response.obtenerDatosClienteResponse.responseData.cuentaData.segmento;
                    oCustomer.oCUENTA.CICLO_FACTURACION = response.obtenerDatosClienteResponse.responseData.cuentaData.cicloFac;
                    oCustomer.oCUENTA.NICHO = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.nichoId);
                    oCustomer.oCUENTA.ESTADO_CUENTA = response.obtenerDatosClienteResponse.responseData.cuentaData.descStatusCuenta;
                    oCustomer.oCUENTA.RESPONSABLE_PAGO = response.obtenerDatosClienteResponse.responseData.cuentaData.responPago;
                    oCustomer.oCUENTA.CONSULTOR = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.consultor);
                    oCustomer.oCUENTA.CONSULTOR_ACCOUNT = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.consultor);
                    oCustomer.oCUENTA.LIMITE_CREDITO = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.limiteCredito);
                    oCustomer.oCUENTA.SALDO_LIMITE_CREDITO = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.saldoLimiteCredito);
                    oCustomer.oCUENTA.TOTAL_LINEAS = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.numLineas);
                    oCustomer.oCUENTA.TOTAL_CUENTAS = Convert.ToString(response.obtenerDatosClienteResponse.responseData.cuentaData.numCuentas);
                    oCustomer.oCUENTA.FECHA_ACT = response.obtenerDatosClienteResponse.responseData.clienteData.fechaAct.ToString();//prueba
                    oCustomer.oCUENTA.plataformaAT = Claro.SIACU.Constants.TOBE;
                    oCustomer.oCUENTA.billingAccountId = response.obtenerDatosClienteResponse.responseData.cuentaData.billingAccountId;
                    oCustomer.oCUENTA.bmIdPub = response.obtenerDatosClienteResponse.responseData.cuentaData.bmIdPub;
                    oCustomer.oCUENTA.MODALIDAD = Claro.ConfigurationManager.AppSettings("ModalityBSCS9");
                    oCustomer.MODALIDAD = Claro.ConfigurationManager.AppSettings("ModalityBSCS9");
                    oCustomer.origen = Claro.ConfigurationManager.AppSettings("ModalityBSCS9");
                    oCustomer.oCUENTA.contactSeqno = response.obtenerDatosClienteResponse.responseData.cuentaData.contactSeqno;

                    //INICIATIVA 616
                    oCustomer.nombreCalle = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.nombreCalle);
                    oCustomer.tipoVia = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.tipoVia);
                    oCustomer.numeroCuadra = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.numeroCuadra);
                    oCustomer.tipoPredio = Convert.ToString(response.obtenerDatosClienteResponse.responseData.clienteData.tipoPredio);
                    //INICIATIVA 616

                    if (oCustomer.oCUENTA.bmIdPub != null && (oCustomer.oCUENTA.bmIdPub.Equals(Claro.SIACU.Constants.MREC, StringComparison.InvariantCultureIgnoreCase) || oCustomer.oCUENTA.bmIdPub.Equals(Claro.SIACU.Constants.PMREC, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        oCustomer.oCUENTA.IsSendEmail = "A";
                    }
                    else
                    {
                        oCustomer.oCUENTA.IsSendEmail = "D";
                    }
                }
                else
                {
                    oCustomer = null;
                }



            }
            catch (Exception ex)
            {
                oCustomer = null;
            }

            return oCustomer;
        }
        /// <summary>
        /// Método que retorna la modalidad de un cliente por número de documento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve valor de modalidad de cliente.</returns>
        public static string GetCustomerJanus(string strIdSession, string strTransaction, string strDocumentNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_MSISDN", DbType.String,255, ParameterDirection.Input, strDocumentNumber),
                new DbParameter("P_RESULTADO", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_CLIENTE_JANUS, parameters);
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetCustomerJanus : " + parameters[1].Value.ToString());
            return parameters[1].Value.ToString();
        }

        public static string GetEstadoContrato(string strIdSession, string strTransaction, string strContract)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("v_estado", DbType.String,1,ParameterDirection.ReturnValue),
                new DbParameter("p_co_id", DbType.Int64, ParameterDirection.Input, Convert.ToInt(strContract))                
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_TFUN007_GET_STATUS_COID, parameters);
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetEstadoContrato : " + parameters[0].Value.ToString());
            return parameters[0].Value.ToString();
        }

        public static string GetStatusControlPlan(string strIdSession, string strTransaction, int intContract, int intSnCode)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_ESTADO", DbType.String, ParameterDirection.ReturnValue),
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input, intContract),
                new DbParameter("P_SNCODE", DbType.Int64, ParameterDirection.Input, intSnCode)                
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TFUN015_ESTADO_SERVICIO, parameters);
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetStatusControlPlan : " + parameters[0].Value.ToString());
            return parameters[0].Value.ToString();
        }


        /// <summary>
        /// Método que obtiene las direcciones del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentType">Tipo de documento</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve objeto OfficeAddress con información de dirección de cliente.</returns>
        public static OfficeAddress GetAddressOfficce(string strIdSession, string strTransaction, string strDocumentType, string strDocumentNumber)
        {
            OfficeAddress oAddress;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPO_DOC", DbType.String, ParameterDirection.Input,strDocumentType),
                new DbParameter("P_NRO_DOC", DbType.String,255, ParameterDirection.Input, strDocumentNumber),
                new DbParameter("C_DIRECCION", DbType.Object, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String,255, ParameterDirection.Output)
            };

            oAddress = DbFactory.ExecuteReader<OfficeAddress>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_DIRECCION_DESPACHO, parameters);

            return oAddress;
        }




        /// <summary>
        /// Método que obtiene las direcciones del cliente RFC.
        /// </summary>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve objeto OfficeAddress con información de dirección de cliente.</returns>
        public static OfficeAddress GetAddressOfficeRfc(string strDocumentNumber)
        {
            OfficeAddress oAddress = new OfficeAddress();

            DataSet dsData = SAPMethods.GetAddressOfficeRfc(strDocumentNumber);

            if (dsData != null)
            {
                foreach (DataRow dr in dsData.Tables[0].Rows)
                {
                    oAddress.DIRECCION_FACT = Convert.ToString(dr["Direccion"]);
                }
            }

            return oAddress;
        }

        /// <summary>
        /// Método que obtiene datos de la factura del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerCode">Código de cliente</param>
        /// <returns>Devuelve objeto Receipt con información de recibo.</returns>
        public static Entity.Dashboard.Postpaid.Receipt GetDataInvoice(string strIdSession, string strTransaction, string strCustomerCode)
        {
            Receipt objReceipt;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_CODIGOCLIENTE", DbType.String,24, ParameterDirection.Input, strCustomerCode),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };
            Claro.Web.Logging.Info(strIdSession, strTransaction, "Metodo: GetDataInvoice - Parametros de Entrada: strCustomerCode-" + strCustomerCode);
            objReceipt = DbFactory.ExecuteReader<Receipt>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters);
            if (objReceipt == null)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, "GetDataInvoice: El valor retornado es null");
            }
            return objReceipt;
        }

        /// <summary>
        /// Método que obtiene datos de detalle de la factura del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve objeto DetailReceipt con información de detalle de recibo.</returns>
        public static Entity.Dashboard.Postpaid.DetailReceipt GetDetailInvoice(string strIdSession, string strTransaction, string strInvoiceNumber)
        {
            DetailReceipt objDetaileReceipt;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output),
            };

            objDetaileReceipt = DbFactory.ExecuteReader<DetailReceipt>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1220, parameters);

            return objDetaileReceipt;
        }

        public static DataInvoiceAndDetailTobe GetDataInvoiceAndDetailTobe(AuditRequest audit, string strCustomerCode, string strInvoiceNumber)
        {

            Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Response.response response = null;
            DataInvoiceAndDetailTobe dataInvoiceAndDetailTobe = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Request.request()
                {
                    consultarUltimaFacturaRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Request.consultarUltimaFacturaRequest()
                    {
                        codigoCliente = strCustomerCode,
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Common.listaOpcional>(){
                            new Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Common.listaOpcional(){
                                clave=string.Empty,
                                valor=string.Empty
                            }
                        }
                    }

                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataInvoiceAndDetail.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DATOS_INVOICE_TOBE, audit, request, false);

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }



            if (response != null &&
                response.consultarUltimaFacturaResponse != null &&
                response.consultarUltimaFacturaResponse.responseAudit != null &&
                response.consultarUltimaFacturaResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.consultarUltimaFacturaResponse.responseData != null
                )
            {
                dataInvoiceAndDetailTobe = new DataInvoiceAndDetailTobe()
               {
                   receipt = new Receipt()
                   {
                       NUMERO_RECIBO = response.consultarUltimaFacturaResponse.responseData.numeroRecibo,
                       FECHA_EMISION = response.consultarUltimaFacturaResponse.responseData.fechaEmision,
                       FECHA_VENCIMIENTO = response.consultarUltimaFacturaResponse.responseData.fechaVencimiento,
                       PERIODO = response.consultarUltimaFacturaResponse.responseData.fechaVencimiento,
                       INVOICENUMBER = response.consultarUltimaFacturaResponse.responseData.numeroRecibo

                   },

               };
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TOTALACCESS = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.totalAccess);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.LARGA_DISTANCIA_INTERNACIONAL = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.largaDistanciaInternacional);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.LARGA_DISTANCIA_NACIONAL = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.largaDistanciaNacional);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TOTALOCCNIGV = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.totalOCCnIGV);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TRAFICO_LOCAL_ADICIONAL = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.traficoLocalAdicional);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TRAFICO_LOCAL_A_CONSUMO = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.traficoLocalAConsumo);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.ROAMING_INTERNACIONAL = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.roamingInternacional);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TOTALSUBSCRIPTION = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.totalSubscription);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TOTALOCCS = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.totalOCCnIGV);
                dataInvoiceAndDetailTobe.receipt.RECIBO_DETALLADO.TOTAL_CARGOS_DEL_MES = Convert.ToDecimal(response.consultarUltimaFacturaResponse.responseData.totalCargosDelMes);



            }

            return dataInvoiceAndDetailTobe;




        }
        /// <summary>
        /// Método que obtiene si el cliente recibe o no por correo su recibo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerCode">Código de cliente</param>
        /// <returns>Devuelve confirmación de envío</returns>
        public static string ValidateMail(string strIdSession, string strTransaction, string strCustomerCode)
        {
            string strServicio;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.String, ParameterDirection.Input, strCustomerCode),
                new DbParameter("P_STATUS", DbType.String,1, ParameterDirection.Output),
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_VALIDA_MAIL, parameters);
            strServicio = parameters[1].Value.ToString();

            return strServicio;
        }

        /// <summary>
        /// Método que obtiene los datos del servicio para Postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="intContratoID">Id de contrato</param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        public static Entity.Dashboard.Postpaid.Service GetDataLine(string strIdSession, string strIdTransaction, int intContratoID)
        {
            Entity.Dashboard.Postpaid.Service objLine = null;

            POSTPAID_CONSULTCLIENT.contrato[] objDataContract = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.contrato[]>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.datosContrato(intContratoID); });

            if (objDataContract.Length >= 1)
            {
                objLine = new Entity.Dashboard.Postpaid.Service();
                objLine.FECHA_ESTADO = objDataContract[0].fec_estado;
                objLine.ESTADO_LINEA = objDataContract[0].estado;
                objLine.MOTIVO = objDataContract[0].motivo;
                objLine.PLAN = objDataContract[0].plan;
                objLine.PLAZO_CONTRATO = objDataContract[0].plazo_contrato;
                objLine.NROICCID = objDataContract[0].iccid;
                objLine.NROIMSI = objDataContract[0].imsi;
                objLine.VENDEDOR = objDataContract[0].vendedor;
                objLine.CAMPANIA = objDataContract[0].campania;
                objLine.FEC_ACTIVACION = objDataContract[0].fecha_act.ToString();
                objLine.FLAG_PLATAFORMA = objDataContract[0].flag_plataforma;
                objLine.PIN1 = objDataContract[0].pin1;
                objLine.PIN2 = objDataContract[0].pin2;
                objLine.PUK1 = objDataContract[0].puk1;
                objLine.PUK2 = objDataContract[0].puk2;
                objLine.COD_PLAN_TARIFARIO = objDataContract[0].codigo_plan_tarifario.ToString();
                objLine.NRO_CELULAR = objDataContract[0].telefono;
                objLine.CONTRATO_ID = objDataContract[0].co_id.ToString();
                objLine.COD_RETORNO = Claro.Constants.NumberOneString;
            }

            return objLine;
        }
        /// <summary>
        /// Metodo para obtener el codigo de OnBase
        /// </summary>
        /// <param name="audit"></param>
        /// <param name="intContratoID"></param>
        /// <returns></returns>
        public static Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse GetIdOnBase(AuditRequest audit, string strCustomerId, string strNroRecibo, string cantidad)
        {
            INVOICE.Response.response oResponse = null;
            Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse objInvoice = null;
            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetInvoice.Request.request request = new INVOICE.Request.request()
                {
                    obtenerIdOnbaseRequest = new INVOICE.Request.ObtenerIdOnbaseRequest()
                    {
                        customerId = strCustomerId,
                        nroRecibo = strNroRecibo,
                        cantidadRecibos = cantidad
                    }
                };
                oResponse = RestService.PostInvoque<INVOICE.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_FACTURA, audit, request, false);


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
                throw;
            }

            if (oResponse != null &&
               oResponse.obtenerIdOnbaseResponse != null &&
               oResponse.obtenerIdOnbaseResponse.responseData != null &&
               oResponse.obtenerIdOnbaseResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
               oResponse.obtenerIdOnbaseResponse.responseData != null
               )
            {
                objInvoice = new Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse();
                objInvoice.idonbase = oResponse.obtenerIdOnbaseResponse.responseData.listaRecibos[0].idonbase;
                objInvoice.nroRecibo = oResponse.obtenerIdOnbaseResponse.responseData.listaRecibos[0].nroRecibo;
                objInvoice.fechaemision = oResponse.obtenerIdOnbaseResponse.responseData.listaRecibos[0].fechaemision;
                objInvoice.fecharegistro = oResponse.obtenerIdOnbaseResponse.responseData.listaRecibos[0].fecharegistro;
                objInvoice.fechavencimiento = oResponse.obtenerIdOnbaseResponse.responseData.listaRecibos[0].fechavencimiento;
                objInvoice.periodo = oResponse.obtenerIdOnbaseResponse.responseData.listaRecibos[0].periodo;
            }
            else
            {
                objInvoice = null;
            }
            return objInvoice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="audit"></param>
        /// <param name="intContratoID"></param>
        /// <returns></returns>
        public static Entity.Dashboard.Postpaid.Service GetDataLineTobe(AuditRequest audit, int intContratoID,string coidPub)
        {
            Entity.Dashboard.Postpaid.Service objLine = null;
            Entity.Dashboard.Postpaid.Legacy.GetDataLine.Response.response response = null;
            Entity.Dashboard.Postpaid.Legacy.GetDataLine.Common.listaOpcional objOpcional = new Entity.Dashboard.Postpaid.Legacy.GetDataLine.Common.listaOpcional();

            List<Entity.Dashboard.Postpaid.Legacy.GetDataLine.Common.listaOpcional> List = new List<Entity.Dashboard.Postpaid.Legacy.GetDataLine.Common.listaOpcional>();
            objOpcional.clave = KEY.AppSettings("contractPubIdKey");
            objOpcional.valor = coidPub;
            List.Add(objOpcional);

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetDataLine.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataLine.Request.request()
            {
                obtenerDatosContratoRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataLine.Request.obtenerDatosContratoRequest()
                {
                    coId = Convert.ToString(intContratoID),
                    listaOpcional = List

                }
            };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataLine.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DATOS_LINEA_TOBE, audit, request, false);



            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (response != null &&
               response.obtenerDatosContratoResponse != null &&
               response.obtenerDatosContratoResponse.responseAudit != null &&
               response.obtenerDatosContratoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
               response.obtenerDatosContratoResponse.responseData != null
               )
            {
                objLine = new Entity.Dashboard.Postpaid.Service();
                objLine.FECHA_ESTADO = System.Convert.ToDateTime(response.obtenerDatosContratoResponse.responseData.fechaEstado);
                objLine.ESTADO_LINEA = response.obtenerDatosContratoResponse.responseData.descEstado;
                objLine.MOTIVO = response.obtenerDatosContratoResponse.responseData.descMotivo;
                objLine.PLAN = response.obtenerDatosContratoResponse.responseData.plan;
                objLine.PLAZO_CONTRATO = response.obtenerDatosContratoResponse.responseData.plazoContrato;
                objLine.NROICCID = response.obtenerDatosContratoResponse.responseData.iccid;
                objLine.NROIMSI = response.obtenerDatosContratoResponse.responseData.imsi;
                objLine.VENDEDOR = response.obtenerDatosContratoResponse.responseData.vendedor;
                objLine.CAMPANIA = response.obtenerDatosContratoResponse.responseData.campania;
                objLine.FEC_ACTIVACION = response.obtenerDatosContratoResponse.responseData.fechaAct.ToString();
                objLine.FLAG_PLATAFORMA = response.obtenerDatosContratoResponse.responseData.flagPlataforma;
                objLine.PIN1 = response.obtenerDatosContratoResponse.responseData.pin1;
                objLine.PIN2 = response.obtenerDatosContratoResponse.responseData.pin2;
                objLine.PUK1 = response.obtenerDatosContratoResponse.responseData.puk1;
                objLine.PUK2 = response.obtenerDatosContratoResponse.responseData.puk2;
                objLine.COD_PLAN_TARIFARIO = response.obtenerDatosContratoResponse.responseData.codplanTarifario.ToString();
                objLine.NRO_CELULAR = response.obtenerDatosContratoResponse.responseData.telefono;
                objLine.CONTRATO_ID = response.obtenerDatosContratoResponse.responseData.coId.ToString();
                objLine.topeConsumo = response.obtenerDatosContratoResponse.responseData.topeConsumo.ToString();
                objLine.bolsasAdicionales = response.obtenerDatosContratoResponse.responseData.bolsasAdicionales.ToString();
                objLine.COD_RETORNO = Claro.Constants.NumberOneString;
            }

            return objLine;




        }
        /// <summary>
        /// Método que obtiene SVA del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContratoId">Id de contrato</param>
        /// <returns>Devuelve cadena con información de cliente.</returns>
        public static string GetTypeSolutionLine(string strIdSession, string strTransaction, string strContratoId)
        {
            string strtext20;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.String, ParameterDirection.Input, strContratoId),
                new DbParameter("p_text20", DbType.String,100, ParameterDirection.Output),
                new DbParameter("p_resultado", DbType.String,100, ParameterDirection.Output),
                new DbParameter("p_msgerr", DbType.String,100, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TIM112_SP_GETINFO_CONTR_TEXT, parameters);
            strtext20 = parameters[1].Value.ToString();

            return strtext20;
        }

        /// <summary>
        /// Método que obtiene si el límite de consumo es activo o no.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelefono">Teléfono</param>
        /// <param name="strContratoId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos ConsumeLimit con información de limite de consumo.</returns>
        public static List<ConsumeLimit> GetConsumeLimit(string strIdSession, string strTransaction, string strTelefono, int strContratoId)
        {
            List<ConsumeLimit> lstConsumeLimit;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_msisdn", DbType.String, ParameterDirection.Input, strTelefono),
                new DbParameter("p_co_id", DbType.String, ParameterDirection.Input,strContratoId),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("p_resultado", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_msgerr", DbType.String, 255, ParameterDirection.Output)
            };

            lstConsumeLimit = DbFactory.ExecuteReader<List<ConsumeLimit>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TIM100_CONSULTA_TOPE_CONSUMO, parameters);

            return lstConsumeLimit;
        }

        /// <summary>
        /// Método que valida paquete de empresa por número y tipo de documento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentType">Tipo de documento</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve indicador de validación.</returns>
        public static string GetValidateCompanyPackage(string strIdSession, string strTransaction, int strDocumentType, string strDocumentNumber)
        {
            string strResultado;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_tipo_doc", DbType.Int64,10, ParameterDirection.Input, strDocumentType), 
                new DbParameter("p_num_doc", DbType.String,20, ParameterDirection.Input,strDocumentNumber), 
                new DbParameter("p_flag", DbType.String,10, ParameterDirection.Output), 
                new DbParameter("p_result", DbType.String,10, ParameterDirection.Output), 
                new DbParameter("p_desc_result", DbType.String,100, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_VALIDA_CLIENTE, parameters);
            strResultado = parameters[2].Value.ToString();

            return strResultado;
        }

        /// <summary>
        /// Método que obtiene una cadena de redireccionamiento de acuerdo al  perfil del usuario.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strName">Nombre</param>
        /// <returns>Devuelve cadena con valor solicitado.</returns>
        public static string GetParameter(string strIdSession, string strTransaction, string strName)
        {
            string objGenericItem;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_NOMBRE", DbType.String, ParameterDirection.Input, strName),
                new DbParameter("P_MENSAJE", DbType.String, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
            };

            objGenericItem = DbFactory.ExecuteScalarToString(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_DATO, parameters, "VALOR_C");

            return objGenericItem;
        }


        public static List<Parameter> GetListParameters(string strIdSession, string strTransaction, string strName)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_NOMBRE", DbType.String, ParameterDirection.Input, strName),
                new DbParameter("P_MENSAJE", DbType.String, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
            };

            List<Parameter> lstParameter = null;


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
            {

                lstParameter = new List<Parameter>();

                while (reader.Read())
                {


                    lstParameter.Add(

                       new Parameter()
                    {
                        DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]),
                        VALOR_C = Convert.ToString(reader["VALOR_C"]),


                    }

                        );
                }
            });

            return lstParameter;
        }
        public static List<Parameter> ObtenerBloqueosClaro(string strIdSession, string strTransaction, string nombre, out string mensaje)
        {
            List<Parameter> lstParameter = new List<Parameter>();
            Parameter objParameter;
            string msg = "";

            try
            {
             DbParameter[] parameters = {new DbParameter("P_NOMBRE", DbType.String,ParameterDirection.Input, nombre),
												   new DbParameter("P_MENSAJE", DbType.String,ParameterDirection.Output),
												   new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
											   };


             DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
             {
                 while (reader.Read())
                 {
                     objParameter = new Parameter()
                     {
                         DESCRIPCION = Claro.Convert.ToString(reader["DESCRIPCION"]),
                         VALOR_C = Claro.Convert.ToString(reader["VALOR_C"]).ToString(),
                         VALOR_N = Claro.Convert.ToDecimal(reader["VALOR_N"]),
                         VALOR_L = Claro.Convert.ToString(reader["VALOR_L"]).ToString()
                     };

                     lstParameter.Add(objParameter);
                 }

             });
             msg = parameters[2].Value.ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                mensaje = msg;
            }
            return lstParameter;
        }


        /// <summary>
        /// Método que retorna parametro TPI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intParameterId">Id de parámetro</param>
        /// <returns>Devuelve valor de parametro TPI.</returns>
        public static string GetParameterTerminalTPI(string strIdSession, string strTransaction, int intParameterId)
        {
            string objGenericItem;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PARAMETRO_ID", DbType.Int32, ParameterDirection.Input, intParameterId),
                new DbParameter("P_MENSAJE", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            objGenericItem = DbFactory.ExecuteScalarToString(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_PARAMETRO, parameters, "VALOR_C");

            return objGenericItem;
        }


        /// <summary>
        /// Método que retorna los datos del terminal TPI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <param name="strTerminalId">Id de terminal</param>
        /// <returns>Devuelve valor de terminal TPI.</returns>
        public static string GetSearchTerminalTPI(string strIdSession, string strTransaction, string strTelephone, string strTerminalId)
        {
            string objGenericItem;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TELEFONO", DbType.String,30, ParameterDirection.Input, strTelephone),
                new DbParameter("P_ID_TERMINAL", DbType.String,30, ParameterDirection.Input,strTerminalId),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MENSAJE_ERROR", DbType.String,500, ParameterDirection.Output)
            };

            objGenericItem = DbFactory.ExecuteScalarToString(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SEARCH_DATA_TERMINAL_TPI, parameters, "ID_TERMINAL");

            return objGenericItem;
        }

        /// <summary>
        /// Método que retorna parametro TFI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intParameterId">Id de parámetro</param>
        /// <returns>Devuelve valor de parámetro TFI.</returns>
        public static string GetParameterTerminalTFI(string strIdSession, string strTransaction, int intParameterId)
        {
            string objGenericItem;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PARAMETRO_ID", DbType.Int32, ParameterDirection.Input, intParameterId),
                new DbParameter("P_MENSAJE", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            objGenericItem = DbFactory.ExecuteScalarToString(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_PARAMETRO, parameters, "VALOR_C");

            return objGenericItem;

        }

        /// <summary>
        /// Método que devuelve una cadena con el dato del plan por id de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContratoID">Id de contrato</param>
        /// <param name="strCodeError">Código de error</param>
        /// <returns>Devuelve cadena con dato del plan</returns>
        public static string GetPlanServiceCombo(string strIdSession, string strTransaction, string strContratoID, ref string strCodeError)
        {
            string strResultado;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.Int64, ParameterDirection.Input, strContratoID),
                new DbParameter("p_tmcode", DbType.String,20, ParameterDirection.Output),
                new DbParameter("p_tmdes", DbType.String,30, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.Int32, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String,500, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_LISTA_SERVICIOS, parameters);
            strResultado = parameters[2].Value.ToString();
            strCodeError = parameters[4].Value.ToString();
            return strResultado;
        }

        /// <summary>
        /// Método que retorna el vendedor para los datos de la cuenta del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve cadena con dato del vendedor.</returns>
        public static string GetSeller(string strIdSession, string strTransaction, string strDocumentNumber)
        {
            string strSeller = "";

            DbParameter[] parameters = new[] {
                new DbParameter("@K_COD_EMP", DbType.String, ParameterDirection.Input, strDocumentNumber)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SACE, DbCommandConfiguration.SIACU_CONBSS_CUENTAXRUC, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    strSeller = Convert.ToString(reader["Consultor"].ToString());
                    break;
                }
            });

            return strSeller;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos Banner por fecha, estado y número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="dtDate">Fecha</param>
        /// <param name="strState">Estado</param>
        /// <param name="strTelephoneType">Tipo de teléfono</param>
        /// <returns>Devuelve listado de objetos Banner con información de la misma.</returns>
        public static List<Banner> GetBanner(string strIdSession, string strTransaction, DateTime dtDate, string strState, string strTelephoneType)
        {
            List<Banner> lstBanner;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_FECHA_PROCESO", DbType.Date, ParameterDirection.Input, dtDate),
                new DbParameter("P_ESTADO", DbType.String, ParameterDirection.Input,strState),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, ParameterDirection.Input,strTelephoneType),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            lstBanner = DbFactory.ExecuteReader<List<Banner>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_BANNER, parameters);

            return lstBanner;
        }

        /// <summary>
        /// Método que obtiene la dirección de la instalación del cliente por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetInstallationAddress(string strIdSession, string strTransaction, string strTelephone)
        {
            Entity.Dashboard.Postpaid.Customer objCustomer = new Customer();

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_NRO_TELEFONO", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("P_VIA_PREFIJO_COD", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_VIA_PREFIJO", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_VIA_DESCRIP", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_VIA_NRO", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_URBAN_COD", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_URBAN_DESC", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_MANZANA", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_LOTE", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_DEPARTAMENTO", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_PROVINCIA", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_DISTRITO", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_UBIGEO", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_REFERENCIA", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_COD_RESP", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_MSG_RESP", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_TIP_DOMICILIO", DbType.String,255, ParameterDirection.Output)

            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_DIRECCION_INSTAL, parameters);
            objCustomer.TELEFONO = strTelephone;
            objCustomer.DIRECCION = String.Format("{0} {1} {2} {3} {4}",
                parameters[2].Value.ToString().Equals("null") ? string.Empty : parameters[2].Value.ToString(),
                parameters[3].Value.ToString().Equals("null") ? string.Empty : parameters[3].Value.ToString(),
                parameters[4].Value.ToString().Equals("null") ? string.Empty : parameters[4].Value.ToString(),
                parameters[5].Value.ToString().Equals("null") ? string.Empty : parameters[5].Value.ToString(),
                parameters[6].Value.ToString().Equals("null") ? string.Empty : parameters[6].Value.ToString());
            objCustomer.NOTA_DIRECCION = parameters[13].Value.ToString().Equals("null") ? string.Empty : parameters[13].Value.ToString();
            objCustomer.DISTRITO = parameters[11].Value.ToString().Equals("null") ? string.Empty : parameters[11].Value.ToString();
            objCustomer.PROVINCIA = parameters[10].Value.ToString().Equals("null") ? string.Empty : parameters[10].Value.ToString();
            objCustomer.DEPARTAMENTO = parameters[9].Value.ToString().Equals("null") ? string.Empty : parameters[9].Value.ToString();


            return objCustomer;
        }

        /// <summary>
        /// Método que obtiene los datos del cliente por código de contrato en LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContract">Contrato</param>
        /// <returns>Devuelve objeto Customer con informción del cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomerByContractCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContract)
        {
            Entity.Dashboard.Postpaid.Customer oCustomer = null;
            POSTPAID_LTE.consultarDatosPorCodigoContratoEAIResponse oConsultationCustomerOut =
              Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarDatosPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarDatosPorCodigoContrato(new POSTPAID_LTE.consultarDatosPorCodigoContratoEAIRequest()
                {
                    auditRequest = new POSTPAID_LTE.AuditRequestType()
                    {
                        idTransaccion = strIdTransaction,
                        ipAplicacion = strIpApplication,
                        nombreAplicacion = strApplicationName,
                        usuarioAplicacion = strUserApplication
                    },
                    codigoContrato = strContract
                });
            });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationCustomerOut.auditResponse;
            POSTPAID_LTE.DatosContratoType[] oTempCustomer = oConsultationCustomerOut.listaDatosPorCodigoContrato;

            if (objAuditResponse.codigoRespuesta != Claro.Constants.NumberZeroString)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    oCustomer = new Entity.Dashboard.Postpaid.Customer();
                    oCustomer.COD_TIPO_CLIENTE = Convert.ToString(oTempCustomer[i].codigoTipoCliente);
                    oCustomer.TIPO_PRODUCTO = Convert.ToString(oTempCustomer[i].tipoProd);
                    oCustomer.TIPO_CLIENTE = Convert.ToString(oTempCustomer[i].tipoCliente);
                    oCustomer.NOMBRES = Convert.ToString(oTempCustomer[i].nombre);
                    oCustomer.APELLIDOS = Convert.ToString(oTempCustomer[i].apellido);
                    oCustomer.NOMBRE_COMPLETO = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.CUENTA = Convert.ToString(oTempCustomer[i].cuenta);
                    oCustomer.CUSTOMER_ID = Convert.ToString(oTempCustomer[i].customerId);
                    oCustomer.RAZON_SOCIAL = Convert.ToString(oTempCustomer[i].razonSocial);
                    oCustomer.TIPO_DOC = Convert.ToString(oTempCustomer[i].tipDocumento);
                    oCustomer.NRO_DOC = Convert.ToString(oTempCustomer[i].nroDocumento);
                    oCustomer.SEXO = Convert.ToString(oTempCustomer[i].sexo);
                    oCustomer.TELEFONO = Convert.ToString(oTempCustomer[i].telefonoref);
                    oCustomer.TELEF_REFERENCIA = Convert.ToString(oTempCustomer[i].telefonoref);
                    oCustomer.TELEFONO_CONTACTO = Convert.ToString(oTempCustomer[i].telefonoref2);
                    oCustomer.FAX = Convert.ToString(oTempCustomer[i].fax);
                    oCustomer.ESTADO_CIVIL = Convert.ToString(oTempCustomer[i].estadoCivil);
                    oCustomer.ESTADO_CIVIL_ID = Convert.ToString(oTempCustomer[i].estadoCivilId);
                    oCustomer.FECHA_NAC = Convert.ToString(oTempCustomer[i].fechaNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(oTempCustomer[i].lugarNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(oTempCustomer[i].nacionalidad);
                    oCustomer.DNI_RUC = Convert.ToString(oTempCustomer[i].rucDni);
                    oCustomer.REPRESENTANTE_LEGAL = Convert.ToString(oTempCustomer[i].repLegal);
                    oCustomer.EMAIL = Convert.ToString(oTempCustomer[i].email);
                    oCustomer.CARGO = Convert.ToString(oTempCustomer[i].cargo);
                    oCustomer.DOMICILIO_FAC = Convert.ToString(oTempCustomer[i].direccionFact);
                    oCustomer.REFERENCIA = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                    oCustomer.URBANIZACION_FAC = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                    oCustomer.DISTRITO = Convert.ToString(oTempCustomer[i].distritoFact);
                    oCustomer.DISTRITO_FAC = Convert.ToString(oTempCustomer[i].distritoFact);
                    oCustomer.PROVINCIA = Convert.ToString(oTempCustomer[i].provinciaFact);
                    oCustomer.PROVINCIA_FAC = Convert.ToString(oTempCustomer[i].provinciaFact);
                    oCustomer.POSTAL_FAC = Convert.ToString(oTempCustomer[i].codigoPostalFact);
                    oCustomer.DEPARTAMENTO = Convert.ToString(oTempCustomer[i].departamentoFact);
                    oCustomer.DEPARTEMENTO_FAC = Convert.ToString(oTempCustomer[i].departamentoFact);
                    oCustomer.DIRECCION_DESPACHO = Convert.ToString(oTempCustomer[i].direccionInst);
                    oCustomer.URBANIZACION_LEGAL = Convert.ToString(oTempCustomer[i].urbanizacionInst);
                    oCustomer.DISTRITO_LEGAL = Convert.ToString(oTempCustomer[i].distritoInst);
                    oCustomer.PROVINCIA_LEGAL = Convert.ToString(oTempCustomer[i].provinciaInst);
                    oCustomer.POSTAL_LEGAL = Convert.ToString(oTempCustomer[i].codigoPostalInst);
                    oCustomer.PAIS_LEGAL = Convert.ToString(oTempCustomer[i].paisInst);
                    oCustomer.PAIS_FAC = oTempCustomer[i].paisFact == null ? "" : Convert.ToString(oTempCustomer[i].paisFact);
                    oCustomer.DEPARTEMENTO_LEGAL = Convert.ToString(oTempCustomer[i].departamentoInst);
                    oCustomer.FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct);
                    oCustomer.CICLO_FACTURACION = Convert.ToString(oTempCustomer[i].cicloFact);
                    oCustomer.COD_CENTRO_POBLADO = Convert.ToString(oTempCustomer[i].codCentPoblado);
                    oCustomer.DES_CENTRO_POBLADO = Convert.ToString(oTempCustomer[i].nomCentPoblado);
                    oCustomer.ASESOR = Convert.ToString(oTempCustomer[i].asesor);
                    oCustomer.CONTRATO_ID = Convert.ToString(strContract);
                    oCustomer.FORMA_PAGO = Convert.ToString(oTempCustomer[i].formaPago);
                    oCustomer.UBIGEO_FACT = Convert.ToString(oTempCustomer[i].ubigeoFact);
                    oCustomer.UBIGEO_INST = Convert.ToString(oTempCustomer[i].ubigeoInst);
                    oCustomer.CODIGO_PLANO_FACT = Convert.ToString(oTempCustomer[i].codigoPlanoFact);
                    oCustomer.CODIGO_PLANO_INST = Convert.ToString(oTempCustomer[i].codigoPlanoInst);

                    oCustomer.oCUENTA = new Account()
                    {
                        TIPO_CLIENTE = oTempCustomer[i].tipoCliente,
                        SEGMENTO = oTempCustomer[i].segmento,
                        CICLO_FACTURACION = oTempCustomer[i].cicloFact,
                        NICHO = Convert.ToString(oTempCustomer[i].nichoId),
                        ESTADO_CUENTA = oTempCustomer[i].estadoCuenta,
                        RESPONSABLE_PAGO = oTempCustomer[i].responPago,
                        LIMITE_CREDITO = Convert.ToString(oTempCustomer[i].limiteCredito),
                        FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct).ToString(),
                        CONSULTOR = Convert.ToString(oTempCustomer[i].consultor)
                    };
                    //Logica Segmento PROY-SEGMENTOVALOR 140351
                    string strDocumentType = KEY.AppSettings("strDocumentType");
                    string strDocumentLong = KEY.AppSettings("strDocumentLong");
                    string strCaracterRellenoNroDoc = KEY.AppSettings("strCaracterRellenoNroDoc");
                    string strSplitDNIRUC = KEY.AppSettings("strSplitDNIRUC");
                    string oTipoDoc = oCustomer.TIPO_DOC;

                    string[] strSplitSegmentoDocumentoF = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[0].Split(',');

                    string[] strSplitSegmentoDocumentoV = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[1].Split(',');

                    for (int ii = 0; ii < strSplitSegmentoDocumentoF.Length; ii++)
                    {
                        if (oTipoDoc == strSplitSegmentoDocumentoF[ii])
                        {
                            oTipoDoc = strSplitSegmentoDocumentoV[ii];
                        }
                    }


                    Entity.Dashboard.Postpaid.Customer oCustomers = new Entity.Dashboard.Postpaid.Customer()
                    {
                        TIPO_DOC = strDocumentType,
                        NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)),
                        LONG_NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)).Length.ToString()
                    };
                    oCustomer.SEGMENTO2 = Claro.Web.Logging.ExecuteMethod<String>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetSegmentCustomerQuery(oCustomers, strIdSession, strUserApplication, strIpApplication, strIdTransaction, strApplicationName); });
                    //FIN PROY-SEGMENTOVALOR 140351

                }
                return oCustomer;
            }
        }

        /// <summary>
        /// Método que obtiene información de hub.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strContratoID">Id de contrato</param>
        /// <returns>Devuelve objeto Hub con información de la misma.</returns>
        public static Hub GetHub(string strIdSession, string strTransaction, string strCustomerId, string strContratoID)
        {

            List<Hub> lstHub;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_customer_id", DbType.String,255, ParameterDirection.Input, strCustomerId),
                new DbParameter("av_cod_id", DbType.String,255, ParameterDirection.Input,strContratoID),
                new DbParameter("an_resultado", DbType.Object, ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output),
                 new DbParameter("ac_equ_cur", DbType.String,255, ParameterDirection.Output)
            };

            lstHub = DbFactory.ExecuteReader<List<Hub>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_HUB, parameters);
            return ((lstHub != null) && lstHub.Count > 0) ? lstHub[0] : null;
        }


        //INICIATIVA 616
        /// <summary>
        /// Método que obtiene información de hub del tobe.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strContratoID">Id de contrato</param>
        /// <returns>Devuelve objeto Hub con información de la misma.</returns>
        public static Hub GetHubTobe(string strIdSession, string strTransaction, int strCustomerId, int strContratoID)
        {

            List<Hub> lstHub;
            Claro.Web.Logging.Info(strIdSession, strTransaction, "Ejecutando GetHubTobe");
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetHubTobe AV_CUSTOMER_ID : " + strCustomerId.ToString());
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetHubTobe AV_COD_ID : " + strContratoID.ToString());

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AV_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input, strCustomerId),
                new DbParameter("AV_COD_ID", DbType.Int64, ParameterDirection.Input, strContratoID),
                new DbParameter("AC_EQU_CUR", DbType.Object, ParameterDirection.Output),
                new DbParameter("AN_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("AV_MENSAJE", DbType.String,255, ParameterDirection.Output)
            };

            lstHub = DbFactory.ExecuteReader<List<Hub>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_HUB_TOBE, parameters);
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetHubTobe AV_RESUTADO : " + parameters[3].Value.ToString());
            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetHubTobe AV_MENSAJE : " + parameters[4].Value.ToString());
            return ((lstHub != null) && lstHub.Count > 0) ? lstHub[0] : null;
        }
        //INICIATIVA 616

        /// <summary>
        /// Método que obtiene los datos de la línea por código de contrato en LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="intContratoID">Id de contrato</param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        public static Entity.Dashboard.Postpaid.Service GetDataLineLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, int intContratoID)
        {
            Entity.Dashboard.Postpaid.Service objLine = new Entity.Dashboard.Postpaid.Service();

            POSTPAID_LTE.consultarServicioPorCodigoContratoEAIResponse oConsultationServicioOut =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarServicioPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarServicioPorCodigoContrato(new POSTPAID_LTE.consultarServicioPorCodigoContratoEAIRequest()
                {
                    auditRequest = new POSTPAID_LTE.AuditRequestType()
                    {
                        idTransaccion = strIdTransaction,
                        ipAplicacion = strIpApplication,
                        nombreAplicacion = strApplicationName,
                        usuarioAplicacion = strUserApplication
                    },
                    codigoContrato = intContratoID.ToString()
                });
            });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationServicioOut.auditResponse;
            POSTPAID_LTE.ServicioPorCodigoContratoType[] oTempServicio = oConsultationServicioOut.listaServicioPorCodigoContrato;

            if (objAuditResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
            {
                if (oTempServicio.Length >= 1)
                {
                    for (int i = 0; i < oTempServicio.Length; i++)
                    {
                        objLine.CONTRATO_ID = Convert.ToString(oTempServicio[0].coid);
                        objLine.FECHA_ESTADO = Convert.ToDate(oTempServicio[0].fechaEstado);
                        objLine.ESTADO_LINEA = Convert.ToString(oTempServicio[0].estado);
                        objLine.MOTIVO = Convert.ToString(oTempServicio[0].motivoEstado);

                        objLine.PLAN = Convert.ToString(oTempServicio[0].tipoServ);
                        objLine.PLAN_TARIFARIO = Convert.ToString(oTempServicio[0].planTarifario);
                        objLine.COD_PLAN_TARIFARIO = Convert.ToString(oTempServicio[0].codPlan);
                        objLine.PLAZO_CONTRATO = Convert.ToString(oTempServicio[0].plazoAcuerdo);
                        objLine.VENDEDOR = Convert.ToString(oTempServicio[0].vendedor);
                        objLine.CAMPANIA = Convert.ToString(oTempServicio[0].campana);
                        objLine.FEC_ACTIVACION = Convert.ToString(oTempServicio[0].fechaActivacion);

                        objLine.TELEFONIA = Convert.ToString(oTempServicio[0].telefonia);
                        objLine.INTERNET = Convert.ToString(oTempServicio[0].internet);
                        objLine.CABLETV = Convert.ToString(oTempServicio[0].cableTv);
                        objLine.NoEs3Play = Convert.ToString(Claro.Constants.NumberZeroString);


                    }
                }
            }
            else
            {
                objLine.NoEs3Play = Convert.ToString(Claro.Constants.NumberOneString);
            }
            return objLine;
        }

        /// <summary>
        /// Método que obtiene una cadena con el código por cuenta LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strFlag">Flag</param>
        /// <param name="strValue">Valor</param>
        /// <returns>Devuelve cadena con código de cuenta.</returns>
        public static string GetCodeByAccountLineLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strFlag, string strValue)
        {
            string strCode;
            POSTPAID_LTE.consultarCodigoClientePorLineaCuentaEAIResponse oConsultationCustomerOut =
               Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarCodigoClientePorLineaCuentaEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarCodigoClientePorLineaCuenta(new POSTPAID_LTE.consultarCodigoClientePorLineaCuentaEAIRequest()
                {
                    auditRequest = new POSTPAID_LTE.AuditRequestType()
                    {
                        idTransaccion = strIdTransaction,
                        ipAplicacion = strIpApplication,
                        nombreAplicacion = strApplicationName,
                        usuarioAplicacion = strUserApplication
                    },
                    flag = strFlag,
                    valor = strValue,
                });
            });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationCustomerOut.auditResponse;

            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                strCode = oConsultationCustomerOut.codigoCliente;
            }
            else
            {
                strCode = "";
            }

            return strCode;
        }

        /// <summary>
        /// Método que obtiene verdadero o falso al consultar los servicios por código de cliente LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strCodeCliente">Código de cliente</param>
        /// <param name="lstServiceGSM">Listado de servicio GSM</param>
        /// <param name="lstServiceISDN">Listado de servicio ISDN</param>
        /// <returns>Devuelve valor booleano.</returns>
        public static bool GetServicesByCustomerCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strCodeCliente, ref List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstServiceGSM, ref List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstServiceISDN)
        {
            POSTPAID_LTE.consultarServicioPorCodigoClienteEAIResponse oConsultationServicioOut =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarServicioPorCodigoClienteEAIResponse>
           (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
           {
               return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarServicioPorCodigoCliente(new POSTPAID_LTE.consultarServicioPorCodigoClienteEAIRequest()
               {
                   codigoCliente = strCodeCliente,
                   auditRequest = new POSTPAID_LTE.AuditRequestType()
                   {
                       idTransaccion = strIdTransaction,
                       ipAplicacion = strIpApplication,
                       nombreAplicacion = strApplicationName,
                       usuarioAplicacion = strUserApplication
                   }
               });
           });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationServicioOut.auditResponse;

            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (oConsultationServicioOut.listaServicioPorCodigoCliente != null)
                {
                    foreach (var item in oConsultationServicioOut.listaServicioPorCodigoCliente)
                    {
                        Claro.SIACU.Entity.Dashboard.Postpaid.Service objService = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                        objService.CODID = Convert.ToString(item.coid);
                        lstServiceISDN.Add(objService);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método que obtiene los datos de un cliente por código LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomerByCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strCustomerId)
        {
            Entity.Dashboard.Postpaid.Customer oCustomer = null;

            POSTPAID_LTE.consultarDatosPorCodigoClienteEAIResponse oConsultationCustomerOut =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarDatosPorCodigoClienteEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarDatosPorCodigoCliente(new POSTPAID_LTE.consultarDatosPorCodigoClienteEAIRequest()
                {
                    auditRequest = new POSTPAID_LTE.AuditRequestType()
                    {
                        idTransaccion = strIdTransaction,
                        ipAplicacion = strIpApplication,
                        nombreAplicacion = strApplicationName,
                        usuarioAplicacion = strUserApplication
                    },
                    codigoCliente = strCustomerId
                });
            });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationCustomerOut.auditResponse;
            POSTPAID_LTE.DatosClienteType[] oTempCustomer = oConsultationCustomerOut.listaDatosPorCodigoCliente;

            if (objAuditResponse.codigoRespuesta != Claro.Constants.NumberZeroString)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    oCustomer = new Entity.Dashboard.Postpaid.Customer();
                    oCustomer.TIPO_CLIENTE = Convert.ToString(oTempCustomer[i].tipoCliente);
                    oCustomer.TIPO_PRODUCTO = Convert.ToString(oTempCustomer[i].tipoProd);
                    oCustomer.NOMBRES = Convert.ToString(oTempCustomer[i].nombre);
                    oCustomer.APELLIDOS = Convert.ToString(oTempCustomer[i].apellido);
                    oCustomer.NOMBRE_COMPLETO = oCustomer.NOMBRES + " " + oCustomer.APELLIDOS;
                    oCustomer.CUENTA = Convert.ToString(oTempCustomer[i].cuenta);
                    oCustomer.CUSTOMER_ID = Convert.ToString(oTempCustomer[i].customerId);
                    oCustomer.RAZON_SOCIAL = Convert.ToString(oTempCustomer[i].razonSocial);
                    oCustomer.TIPO_DOC = Convert.ToString(oTempCustomer[i].tipDocumento);
                    oCustomer.NRO_DOC = Convert.ToString(oTempCustomer[i].nroDocumento);
                    oCustomer.SEXO = Convert.ToString(oTempCustomer[i].sexo);
                    oCustomer.TELEFONO = Convert.ToString(oTempCustomer[i].telefonoref);
                    oCustomer.TELEF_REFERENCIA = Convert.ToString(oTempCustomer[i].telefonoref);
                    oCustomer.FAX = Convert.ToString(oTempCustomer[i].fax);
                    oCustomer.ESTADO_CIVIL = Convert.ToString(oTempCustomer[i].estadoCivil);
                    oCustomer.ESTADO_CIVIL_ID = Convert.ToString(oTempCustomer[i].estadoCivilId);
                    oCustomer.FECHA_NAC = Convert.ToString(oTempCustomer[i].fechaNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(oTempCustomer[i].lugarNacimiento);
                    oCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(oTempCustomer[i].nacionalidad);
                    oCustomer.DNI_RUC = Convert.ToString(oTempCustomer[i].rucDni);
                    oCustomer.REPRESENTANTE_LEGAL = Convert.ToString(oTempCustomer[i].repLegal);
                    oCustomer.EMAIL = Convert.ToString(oTempCustomer[i].email);
                    oCustomer.CARGO = Convert.ToString(oTempCustomer[i].cargo);
                    oCustomer.DOMICILIO_FAC = Convert.ToString(oTempCustomer[i].direccionFact);
                    oCustomer.REFERENCIA = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                    oCustomer.URBANIZACION_FAC = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                    oCustomer.DISTRITO = Convert.ToString(oTempCustomer[i].distritoFact);
                    oCustomer.DISTRITO_FAC = Convert.ToString(oTempCustomer[i].distritoFact);
                    oCustomer.PROVINCIA = Convert.ToString(oTempCustomer[i].provinciaFact);
                    oCustomer.PROVINCIA_FAC = Convert.ToString(oTempCustomer[i].provinciaFact);
                    oCustomer.POSTAL_FAC = Convert.ToString(oTempCustomer[i].codigoPostalFact);
                    oCustomer.DEPARTAMENTO = Convert.ToString(oTempCustomer[i].departamentoFact);
                    oCustomer.DEPARTEMENTO_FAC = Convert.ToString(oTempCustomer[i].departamentoFact);
                    oCustomer.UBIGEO_FACT = Convert.ToString(oTempCustomer[i].ubigeoFact);
                    oCustomer.UBIGEO_INST = Convert.ToString(oTempCustomer[i].ubigeoInst);
                    oCustomer.CODIGO_PLANO_FACT = Convert.ToString(oTempCustomer[i].codigoPlanoFact);
                    oCustomer.CODIGO_PLANO_INST = Convert.ToString(oTempCustomer[i].codigoPlanoInst);
                    oCustomer.ASESOR = Convert.ToString(oTempCustomer[i].asesor);
                    oCustomer.FORMA_PAGO = Convert.ToString(oTempCustomer[i].formaPago);
                    oCustomer.FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct);
                    oCustomer.oCUENTA = new Account()
                    {
                        TIPO_CLIENTE = oTempCustomer[i].tipoCliente,

                        SEGMENTO = oTempCustomer[i].segmento,
                        CICLO_FACTURACION = oTempCustomer[i].cicloFact,
                        NICHO = Convert.ToString(oTempCustomer[i].nichoId),
                        ESTADO_CUENTA = oTempCustomer[i].estadoCuenta,
                        RESPONSABLE_PAGO = oTempCustomer[i].responPago,
                        LIMITE_CREDITO = Convert.ToString(oTempCustomer[i].limiteCredito),

                        FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct).ToString(),
                        CONSULTOR = Convert.ToString(oTempCustomer[i].consultor)
                    };

                }
                return oCustomer;
            }

        }

        /// <summary>
        /// Método que obtiene una lista con la consulta de teléfono por código de contrato LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de la misma.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetTelephoneByContractCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = new List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>();
            POSTPAID_LTE.consultarListaTelefonoPorCodigoContratoEAIResponse oConsultationListOut =
               Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarListaTelefonoPorCodigoContratoEAIResponse>
           (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
           {
               return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarListaTelefonoPorCodigoContrato(new POSTPAID_LTE.consultarListaTelefonoPorCodigoContratoEAIRequest()
               {
                   codigoContrato = strContractId,
                   auditRequest = new POSTPAID_LTE.AuditRequestType()
                   {
                       idTransaccion = strIdTransaction,
                       ipAplicacion = strIpApplication,
                       nombreAplicacion = strApplicationName,
                       usuarioAplicacion = strUserApplication
                   }
               });
           });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationListOut.auditResponse;
            POSTPAID_LTE.TelefonoPorCodigoContratoType[] oTempCustomer = oConsultationListOut.listaTelefonoPorCodigoContrato;

            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    Claro.SIACU.Entity.Dashboard.Postpaid.Service objService = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                    objService.NRO_CELULAR = Convert.ToString(oTempCustomer[i].dnNum);
                    objService.ESTADO_LINEA = Convert.ToString(oTempCustomer[i].estadoLinea);
                    lstService.Add(objService);
                }
            }
            return lstService;

        }

        /// <summary>
        /// Método que obtiene una lista de líneas desactivas por código de contrato LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de la misma.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetLineByContractCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = new List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>();

            POSTPAID_LTE.consultarLineasDesactivasPorCodigoContratoEAIResponse oConsultationCodeOutput =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_LTE.consultarLineasDesactivasPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_LTE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_LTE.consultarLineasDesactivasPorCodigoContrato(new POSTPAID_LTE.consultarLineasDesactivasPorCodigoContratoEAIRequest()
                {
                    codigoContrato = strContractId,
                    auditRequest = new POSTPAID_LTE.AuditRequestType()
                    {
                        idTransaccion = strIdTransaction,
                        ipAplicacion = strIpApplication,
                        nombreAplicacion = strApplicationName,
                        usuarioAplicacion = strUserApplication
                    }
                });
            });

            POSTPAID_LTE.AuditResponseType objAuditResponse = oConsultationCodeOutput.auditResponse;
            POSTPAID_LTE.LineasDesactivasPorCodigoContratoType[] oTempDisableLine = oConsultationCodeOutput.listaLineasDesactivasPorCodigoContrato;

            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempDisableLine.Length; i++)
                {
                    Claro.SIACU.Entity.Dashboard.Postpaid.Service objService = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                    objService.MSISDN = Convert.ToString(oTempDisableLine[i].msisdn);
                    objService.PROVIDER_ID = Convert.ToString(oTempDisableLine[i].idProducto);
                    objService.FEC_ACTIVACION = Convert.ToString(oTempDisableLine[i].fechaActivacion);
                    objService.FEC_DESACTIVACION = Convert.ToString(oTempDisableLine[i].fechaDesactivacion);

                    lstService.Add(objService);
                }
            }

            return lstService;
        }

        /// <summary>
        /// Método que obtiene parámetro terminal TPI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="decParameterId">Id de parámetro</param>
        /// <returns>Devuelve objeto Parameter con información de parámetro.</returns>
        public static Entity.Dashboard.Postpaid.Parameter GetTPIParameterTerminal(string strIdSession, string strTransaction, decimal decParameterId)
        {
            Entity.Dashboard.Postpaid.Parameter oParameter;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PARAMETRO_ID", DbType.Decimal, ParameterDirection.Input, decParameterId),
                new DbParameter("P_MENSAJE", DbType.String, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            oParameter = DbFactory.ExecuteReader<Parameter>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_PARAMETRO, parameters);
            return oParameter;
        }

        /// <summary>
        /// Método que obtiene si existe o no días activos en datos del servicio.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContratoId">Id de contrato</param>
        /// <param name="intDaysActive">Cantidad días activos.</param>
        /// <param name="intDaysDisable">Cantidad días desactivos</param>
        /// <param name="intResul">Valor de resultado</param>
        /// <param name="strMessageError">Mensaje de error</param>
        /// <returns>Devuelve valor booleano.</returns>
        public static bool GetActiveDisableDays(string strIdSession, string strTransaction, string strContratoId, out int intDaysActive, out int intDaysDisable, out int intResul, out string strMessageError)
        {
            bool boolOut;
            intDaysActive = Claro.Constants.NumberZero;
            intDaysDisable = Claro.Constants.NumberZero;
            intResul = Claro.Constants.NumberZero;
            strMessageError = "";

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CO_ID", DbType.String,255, ParameterDirection.Input, strContratoId),
                new DbParameter("p_nro_dias_act", DbType.Int64, ParameterDirection.Output),
                new DbParameter("p_nro_dias_desact", DbType.Int64, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,255, ParameterDirection.Output),
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_NRO_DIAS_ACT_DES, parameters);
            intDaysActive = Convert.ToInt(parameters[1].Value.ToString());
            intDaysDisable = Convert.ToInt(parameters[2].Value.ToString());
            intResul = Convert.ToInt(parameters[3].Value.ToString());
            strMessageError = Convert.ToString(parameters[4].Value);
            boolOut = true;

            return boolOut;
        }


        public static List<HistorySIM> GetHistorySIMTobe(Claro.Entity.AuditRequest audit, string strCoId, string strNroLinea, string strPlataforma, string strFechaMigracion, string flagconvivencia, out string strResponseCode, out string strResponse)
        {

            IMSILIST.Response.response oResponse = null;
            HistorySIM HistorySIMTobe = null;
            List<HistorySIM> Lista = new List<HistorySIM>();
            strResponseCode = Claro.Constants.NumberOneNegativeString;
            strResponse = "";
            try
            {
                IMSILIST.Common.listaOpcional objOpcional = new IMSILIST.Common.listaOpcional();

                List<IMSILIST.Common.listaOpcional> List = new List<IMSILIST.Common.listaOpcional>();
                List.Add(objOpcional);
                IMSILIST.Request.request request = new IMSILIST.Request.request()

                {
                    consultarIMSIHistoricoRequest = new IMSILIST.Request.consultarIMSIHistoricoRequest()
        {
            coid = strCoId,
            numeroLinea = strNroLinea,
            plataforma = strPlataforma.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase) ? Claro.Constants.NumberZeroString : flagconvivencia.Equals(Claro.Constants.NumberZeroString) ? Claro.Constants.NumberOneString : Claro.Constants.NumberTwoString,
            fechaMigracion = strFechaMigracion,
            listaOpcional = List
        }

                };

                oResponse = RestService.PostInvoque<IMSILIST.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_LISTADO_HISTORICO_IMSI, audit, request, false);

                if (oResponse != null && oResponse.consultarIMSIHistoricoResponse != null && oResponse.consultarIMSIHistoricoResponse.responseAudit != null)
                {
                    strResponseCode = oResponse.consultarIMSIHistoricoResponse.responseAudit.codigoRespuesta;
                    strResponse = oResponse.consultarIMSIHistoricoResponse.responseAudit.mensajeRespuesta;
                    if (
                        oResponse.consultarIMSIHistoricoResponse.responseData != null &&
                        oResponse.consultarIMSIHistoricoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString
                   )
                    {
                        foreach (var item in oResponse.consultarIMSIHistoricoResponse.responseData.listaHistoricoDatos)
                        {
                            HistorySIMTobe = new HistorySIM();

                            HistorySIMTobe.cd_id = item.cd_id;
                            HistorySIMTobe.cd_seqno = item.cd_seqno;
                            HistorySIMTobe.Estado = item.estado;
                            HistorySIMTobe.Motivo = item.motivo;
                            HistorySIMTobe.IntroducidoAl = item.introducido_al;
                            HistorySIMTobe.Valido_Desde = DateTime.Parse(item.valido_desde).ToString("dd/MM/yyyy hh:mm:ss tt");
                            HistorySIMTobe.IntroducidoPor = item.introducido_por;
                            HistorySIMTobe.ICCID = item.iccid;
                            HistorySIMTobe.Modificado = item.modificado;
                            HistorySIMTobe.IMSI = item.imsi;
                            HistorySIMTobe.cd_rs_id = item.cd_rs_id;

                            Lista.Add(HistorySIMTobe);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
                throw;
            }
            return Lista;
        }

        /// <summary>
        /// Método que devuelve una lista de servicios de línea por id de contrato y id de cliente LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContratoId">Id de contrato</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Deco con información de equipos decodificadores.</returns>
        public static List<Deco> GetServiceLineLTE(string strIdSession, string strTransaction, string strContratoId, string strCustomerId)
        {
            List<Deco> listDeco = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_customer_id", DbType.String,255, ParameterDirection.Input, strCustomerId),
                new DbParameter("av_cod_id", DbType.String,255, ParameterDirection.Input, strContratoId),
                new DbParameter("ac_equ_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int32, ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_EQU, parameters, (IDataReader reader) =>
            {
                listDeco = new List<Deco>();

                while (reader.Read())
                {
                    listDeco.Add(new Deco()
                    {
                        tipo_servicio = Convert.ToString(reader["tipo_servicio"]),
                        codigo_material = Convert.ToString(reader["codigo_material"]),
                        codigo_sap = Convert.ToString(reader["codigo_sap"]),
                        numero_serie = Convert.ToString(reader["numero_serie"]),
                        macadress = Convert.ToString(reader["macaddress"]),
                        descripcion_material = Convert.ToString(reader["descripcion_material"]),
                        tipo_equipo = Convert.ToString(reader["tipo_equipo"]),
                        centro = Convert.ToString(reader["centro"]),
                        modelo = Convert.ToString(reader["modelo"]),
                        numero = Convert.ToString(reader["numero"]),
                        sim_card = Convert.ToString(reader["sim_card"]),
                        Tipo = Convert.ToString(reader["tipo"]),
                        Estado = Convert.ToString(reader["estado"])
                    });
                }
            });

            return listDeco;
        }

        /// <summary>
        /// Método que devuelve una lista de servicios de línea DTH por id de contrato y id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContratoId">Id de contrato</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Deco con información de equipos decodificadores.<returns>
        public static List<Deco> GetServiceLineDTH(string strIdSession, string strTransaction, string strContratoId, string strCustomerId)
        {
            List<Deco> listDeco = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_customer_id", DbType.String,255, ParameterDirection.Input, strCustomerId),
                new DbParameter("av_cod_id", DbType.String,255, ParameterDirection.Input, strContratoId),
                 new DbParameter("ac_equ_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int32, ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
               
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_P_CONSULTA_EQU, parameters, (IDataReader reader) =>
            {
                listDeco = new List<Deco>();

                while (reader.Read())
                {
                    listDeco.Add(new Deco()
                    {
                        id_transaccion = Convert.ToString(reader["idtransaccion"]),
                        codigo_material = Convert.ToString(reader["codigo_material"]),
                        codigo_sap = Convert.ToString(reader["codigo_sap"]),
                        numero_serie = Convert.ToString(reader["numero_serie"]),
                        macadress = Convert.ToString(reader["macaddress"]),
                        descripcion_material = Convert.ToString(reader["descripcion_material"]),
                        abrev_material = Convert.ToString(reader["abrev_material"]),
                        estado_material = Convert.ToString(reader["estado_material"]),
                        precio_almacen = Convert.ToString(reader["precio_almacen"]),
                        codigo_cuenta = Convert.ToString(reader["codigo_cuenta"]),
                        componente = Convert.ToString(reader["componente"]),
                        centro = Convert.ToString(reader["centro"]),
                        idalm = Convert.ToString(reader["idalm"]),
                        almacen = Convert.ToString(reader["almacen"]),
                        tipo_equipo = Convert.ToString(reader["tipo_equipo"]),
                        id_producto = Convert.ToString(reader["idproducto"]),
                        id_cliente = Convert.ToString(reader["id_cliente"]),
                        modelo = Convert.ToString(reader["modelo"]),
                        fecusu = Convert.ToString(reader["fecusu"]),
                        codusu = Convert.ToString(reader["codusu"]),
                        convertertype = Convert.ToString(reader["convertertype"]),
                        servicio_principal = Convert.ToString(reader["servicio_principal"]),
                        headend = Convert.ToString(reader["headend"]),
                        ephomeexchange = Convert.ToString(reader["ephomeexchange"]),
                        numero = Convert.ToString(reader["numero"])
                    });
                }
            });

            return listDeco;
        }

        /// <summary>
        /// Método que retorna verdadero o falso la consulta IMR.
        /// </summary>
        /// <param name="objAudit">Objeto auditoría</param>
        /// <param name="strMsisdn">Valor msisdn</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strAccount">Cuenta</param>
        /// <param name="intBillingCycle">Ciclo de facturación</param>
        /// <param name="strProductType">Tipo de producto</param>
        /// <param name="strCustomerType">Tipo de cliente</param>
        /// <param name="strSegment">Segmento</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <param name="strIMRTotalAmount">Monto total IMR</param>
        /// <param name="strIMRAmount">Monto IMR</param>
        /// <param name="strExpirationDate">Fecha de expiración</param>
        /// <returns>Devuelve valor booleano.</returns>
        public static bool GetIMRConsult(Claro.Entity.AuditRequest objAudit, string strMsisdn, string strCustomerId, string strAccount, int intBillingCycle, string strProductType, string strCustomerType, string strSegment, string strContractId, out string strIMRTotalAmount, out string strIMRAmount, out string strExpirationDate)
        {
            string strIMRAmounts = null;
            string strIMRTotalAmounts = null;
            string strExpirationDates = null;
            bool boolResponse;
            objAudit.ApplicationName = "SIACPOST";
            POSTPAID_IMRCONSULT.audiTypeResponse objIMRResponse;
            POSTPAID_IMRCONSULT.audiTypeRequest objIMRRequest = new POSTPAID_IMRCONSULT.audiTypeRequest()
            {
                idTransaccion = objAudit.Transaction,
                usrAplicacion = objAudit.UserName,
                ipAplicacion = objAudit.IPAddress,
                aplicacion = objAudit.ApplicationName
            };
            objIMRResponse =
            Claro.Web.Logging.ExecuteMethod<Claro.SIACU.ProxyService.SIACPost.IMR.audiTypeResponse>
            (objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.POSTPAID_IMR, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_IMR.consultaIMR(objIMRRequest, strMsisdn, strCustomerId, strAccount, intBillingCycle, strProductType,
                                                                                           strCustomerType, strSegment, strContractId, out strIMRTotalAmounts,
                                                                                           out strIMRAmounts, out strExpirationDates);
            });
            strIMRAmount = strIMRAmounts;
            strIMRTotalAmount = strIMRTotalAmounts;
            strExpirationDate = strExpirationDates;
            if (!objIMRResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                boolResponse = false;
            }
            else
            {
                boolResponse = true;
            }
            return boolResponse;
        }

        /// <summary>
        /// Método que retorna los tipos de contacto de un cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de objetos ContactType con información de tipo de contacto.</returns>
        public static List<ContactType> GetContactType(string strIdSession, string strTransaction)
        {
            List<ContactType> lstContactType = new List<ContactType>();
            ContactType objContactType;
            DbParameter[] parameters = new DbParameter[] {
                 new DbParameter("P_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACTSS_TCCT_LISTACFG, parameters, (IDataReader reader) =>
            {
                objContactType = new Entity.Dashboard.Postpaid.ContactType();

                while (reader.Read())
                {
                    objContactType = new Entity.Dashboard.Postpaid.ContactType()
                    {
                        TCCTN_CODIGO = Convert.ToInt(reader["TCCTN_CODIGO"]),
                        TCCTV_NOMBRE = Convert.ToString(reader["TCCTV_NOMBRE"]),
                        TCCTC_COPIABLE = (Convert.ToString(reader["TCCTC_COPIABLE"]) == Claro.Constants.NumberOneString),
                        TCCTC_OBLIGATORIO = (Convert.ToString(reader["TCCTC_OBLIGATORIO"]) == Claro.Constants.NumberOneString),
                        TCCTI_MINREGISTROS = Convert.ToInt(reader["TCCTI_MINREGISTROS"]),
                        TCCTI_MAXREGISTROS = Convert.ToInt(reader["TCCTI_MAXREGISTROS"]),
                        TCCTC_VISIBLESIACPOST = (Convert.ToString(reader["TCCTC_VISIBLESIACPOST"]) == Claro.Constants.NumberOneString),
                        TCCTC_VISIBLESISACT = (Convert.ToString(reader["TCCTC_VISIBLESISACT"]) == Claro.Constants.NumberOneString),
                    };
                    lstContactType.Add(objContactType);
                }
            });

            return lstContactType;
        }

        /// <summary>
        /// Método que obtiene una lista con el tipo de parámetro que reciben las columnas de contacto.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de objetos ContactTypeField con información de tipo de contacto.</returns>
        public static List<ContactTypeField> GetContactTypeField(string strIdSession, string strTransaction)
        {
            List<ContactTypeField> lstContactTypeField;
            DbParameter[] parameters = new DbParameter[] {
                 new DbParameter("P_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
            };
            lstContactTypeField = DbFactory.ExecuteReader<List<ContactTypeField>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACTSS_TCCC_LISTACFG, parameters);

            return lstContactTypeField;
        }

        /// <summary>
        /// Método que obtiene una lista con los tipos de documento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentCode">Código de documento</param>
        /// <returns>Devuelve listado de objetos DocumentType con información de tipo de documento.</returns>
        public static List<DocumentType> GetDocumentType(string strIdSession, string strTransaction, string strDocumentCode)
        {
            List<DocumentType> lstDocumentType;
            DbParameter[] parameters = new DbParameter[] {
               new DbParameter("P_FLAG_CON", DbType.String,255, ParameterDirection.Input, strDocumentCode),
                 new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output),
         
            };
            lstDocumentType = DbFactory.ExecuteReader<List<DocumentType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACT_CON_TIPO_DOC, parameters);

            return lstDocumentType;
        }

        /// <summary>
        /// Método que obtiene una lista con los tipos de datos de la tabla de contactos de un cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strType">Tipo</param>
        /// <returns>Devuelve lista de objetos DataType con información del tipo de datos.</returns>
        public static List<DataType> GetDataTypeList(string strIdSession, string strTransaction, string strType)
        {
            List<DataType> lstDataType;
            DbParameter[] parameters = new DbParameter[] {
               new DbParameter("P_PARAN_CODIGO", DbType.Int64, ParameterDirection.Input, strType),
                 new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
            lstDataType = DbFactory.ExecuteReader<List<DataType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACTSS_DATOS_LISTA, parameters);

            return lstDataType;
        }

        /// <summary>
        /// Método que obtiene la lista de contactos de un cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intCustomerId">Id de cliente</param>
        /// <param name="strCustomerCode">Código de cliente</param>
        /// <param name="intSolinCode">Código de solicitud</param>
        /// <param name="intCSCTNCode">Código CSCTN</param>
        /// <returns>Devuelve listado de objetos Contact con información de contactos.</returns>
        public static List<Contact> GetContactList(string strIdSession, string strTransaction, int intCustomerId, string strCustomerCode, int intSolinCode, int intCSCTNCode)
        {
            List<Contact> lstContact;
            DbParameter[] parameters = {
            new DbParameter("P_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input,intCustomerId),		
			new DbParameter("P_CUSTCODE", DbType.String, 24, ParameterDirection.Input,strCustomerCode),	
			new DbParameter("P_SOLIN_CODIGO", DbType.Int64,ParameterDirection.Input,intSolinCode),		
			new DbParameter("P_CSCTN_CODIGO", DbType.Int64, ParameterDirection.Input,intCSCTNCode),	 											   
			new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)};

            lstContact = DbFactory.ExecuteReader<List<Contact>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACTSS_CSCT_LISTA, parameters);

            return lstContact;
        }

        /// <summary>
        /// Método que obtiene la configuración de las columnas de la tabla de contactos de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intCode">Código</param>
        /// <returns>Devuelve listado de objetos ContactTypeByField con información de columnas de tipo de contacto.</returns>
        public static List<ContactTypeByField> GetColumnsConfiguration(string strIdSession, string strTransaction, int intCode)
        {
            List<ContactTypeByField> lst;
            DbParameter[] parameters = {
            new DbParameter("P_TCCTN_CODIGO", DbType.Int64, ParameterDirection.Input,intCode),		
			new DbParameter("P_CUR_SALIDA", DbType.Object, ParameterDirection.Output)};

            lst = DbFactory.ExecuteReader<List<ContactTypeByField>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACTSS_TCXC_LISTACFG, parameters);

            return lst;
        }

        /// <summary>
        /// Método que obtiene una cadena con los datos de un nuevo contacto de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strAccount">Cuenta</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <param name="strSave">Indicador para guardar</param>
        /// <param name="strDelete">Indicador para eliminar</param>
        /// <param name="strResponseCode">Código de respuesta</param>
        /// <returns>Devuelve confirmación de correcto registro de nuevo contacto.</returns>
        public static string ContactSave(string strIdSession, string strTransaction, string strCustomerId, string strAccount, string strTelephone, string strSave, string strDelete, out string strResponseCode)
        {
            string strSalida;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_RESP_CODIGO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("P_RESP_MENSAJE", DbType.String,120, ParameterDirection.Output),
                new DbParameter("P_USER", DbType.String,255, ParameterDirection.Input,strCustomerId),
                new DbParameter("P_TELEFONO", DbType.String,255, ParameterDirection.Input,strTelephone),
                new DbParameter("P_CUSTCODE", DbType.String,255, ParameterDirection.Input,strAccount),
                new DbParameter("P_LIST_CUST_CONTACTO", DbType.String,32000, ParameterDirection.Input,strSave),
                new DbParameter("P_LIST_CUST_CONTACTO_ELIM", DbType.String,32000, ParameterDirection.Input,strDelete)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SISACTSU_GUARDAR_CONTACTOS, parameters);

            strSalida = Convert.ToString(parameters[1].Value);
            strResponseCode = Convert.ToString(parameters[0].Value);
            return strSalida;
        }

        /// <summary>
        /// Método que devuelve una cadena con los dominios de email validos. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intParameter">Parámetro</param>
        /// <returns>Devuelve cadena con dominios de email.</returns>
        public static string AvailableEmails(string strIdSession, string strTransaction, int intParameter)
        {
            string strSalida;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CODPARAMETRO", DbType.Int64, ParameterDirection.Input,intParameter),
                new DbParameter("K_VALOR", DbType.String,120, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_MANTSS_OBTENER_VALOR_PARAMETRO, parameters);

            strSalida = Convert.ToString(parameters[1].Value);

            return strSalida;
        }

        /// <summary>
        /// Método que devuelve los productos postpago. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strUserName">Nombre de usuario</param>
        /// <param name="strIPAddress">Dirección IP</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentType">Tipo de documento</param>
        /// <param name="strDocumentIdentity">Documento de identidad</param>
        /// <returns>Devuelve objeto PostpaidProductsResponse con información de productos postpago.</returns>
        public static PostpaidProductsResponse GetPostpaidProducts(string strIdSession, string strUserName, string strIPAddress, string strApplicationName, string strTransaction, string strDocumentType, string strDocumentIdentity)
        {
            POSTPAID_PRODUCTS.ConsultarProductosPostpagoResponse oConsultarProductosPostpagoResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_PRODUCTS.ConsultarProductosPostpagoResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_PRODUCTS, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_PRODUCTS.consultarProductosPostpago(new POSTPAID_PRODUCTS.ConsultarProductosPostpagoRequest()
                {
                    tipoDocIdentidad = strDocumentType,
                    docIdentidad = strDocumentIdentity,
                    auditRequest = new ProxyService.SIACU.Post.Products.auditRequestType()
                    {
                        usuarioAplicacion = strUserName,
                        ipAplicacion = strIPAddress,
                        nombreAplicacion = strApplicationName,
                        idTransaccion = strTransaction
                    },
                    listaRequestOpcional = new Claro.SIACU.ProxyService.SIACU.Post.Products.parametrosTypeObjetoOpcional[2] { 
                        new Claro.SIACU.ProxyService.SIACU.Post.Products.parametrosTypeObjetoOpcional(){
                            campo=KEY.AppSettings( "keyplataforma"),
                            valor=KEY.AppSettings("keyTOBE")
                         },
                         new Claro.SIACU.ProxyService.SIACU.Post.Products.parametrosTypeObjetoOpcional(){
                            campo="proceso",
                            valor="fija"
                         }
                    }

                });
            });


            if (oConsultarProductosPostpagoResponse.auditResponse.codigoRespuesta.Equals(Claro.Constants.NumberTwoString) || Convert.ToInt(oConsultarProductosPostpagoResponse.auditResponse.codigoRespuesta) < Claro.Constants.NumberZero)
            {
                throw new Claro.MessageException("No se pudo realizar la consulta de productos Postpago");
            }

            PostpaidProductsResponse oGetPostpaidProductsResponse = new PostpaidProductsResponse()
            {
                ErrorMsg = oConsultarProductosPostpagoResponse.auditResponse.mensajeRespuesta,
                CodeError = oConsultarProductosPostpagoResponse.auditResponse.codigoRespuesta,
                DataOrigin = oConsultarProductosPostpagoResponse.datosPostpagoResponse.origenDatos
            };

            Product oServicioPost;

            foreach (var item in oConsultarProductosPostpagoResponse.datosPostpagoResponse.listaServicios)
            {
                oServicioPost = new Product();
                oServicioPost.IdPlan = item.idPlan;
                oServicioPost.TipoProducto = item.tipoProducto;
                oServicioPost.CodigoProducto = item.codigoProducto;
                oServicioPost.Producto = item.producto;
                oServicioPost.Cuenta = item.cuenta;
                oServicioPost.CodigoCliente = item.codigoCliente;
                oServicioPost.TipoCliente = item.tipoCliente;
                oServicioPost.FechaActivacionCuenta = item.fechaActivacionCuenta;
                oServicioPost.NroServiciosActivos = item.nroServiciosActivos;
                oServicioPost.NroServiciosNoActivos = item.nroServiciosNoActivos;
                oGetPostpaidProductsResponse.ListProduct.Add(oServicioPost);
            }

            return oGetPostpaidProductsResponse;
        }


        /// <summary>
        /// Método que devuelve el detalle de productos postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strUserName">Nombre de usuario</param>
        /// <param name="strIPAddress">Dirección Ip</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strState">Estado</param>
        /// <param name="strCodeProduct">Código de producto</param>
        /// <param name="strOrigin">Origen</param>
        /// <param name="strIdPlan">Id de plan</param>
        /// <param name="strProductType">Tipo de producto</param>
        /// <returns>Devuelve objeto PostpaidLinesResponse con información de productos postpago.</returns>
        public static PostpaidLinesResponse GetPostpaidLines(string strIdSession, string strUserName, string strIPAddress, string strApplicationName, string strTransaction, string strCustomerId, string strState, string strCodeProduct, string strOrigin, string strIdPlan, string strProductType)
        {


            POSTPAID_PRODUCTDETAIL.ConsultarDetalleProductoPostpagoRequest objRequest = new POSTPAID_PRODUCTDETAIL.ConsultarDetalleProductoPostpagoRequest()
            {
                customerId = strCustomerId,
                estado = strState,
                codigoProducto = strCodeProduct,
                origenDatos = strOrigin,
                idPlan = strIdPlan,
                tipoProducto = strProductType,
                auditRequest = new ProxyService.SIACU.Post.Lines.auditRequestType()
                {
                    usuarioAplicacion = strUserName,
                    ipAplicacion = strIPAddress,
                    nombreAplicacion = strApplicationName,
                    idTransaccion = strTransaction
                },
                listaRequestOpcional = new Claro.SIACU.ProxyService.SIACU.Post.Lines.parametrosTypeObjetoOpcional[2] { 
                    new Claro.SIACU.ProxyService.SIACU.Post.Lines.parametrosTypeObjetoOpcional(){
                        campo=KEY.AppSettings( "keyplataforma"),
                        valor=KEY.AppSettings("keyTOBE")
                    },
                    new Claro.SIACU.ProxyService.SIACU.Post.Lines.parametrosTypeObjetoOpcional(){
                        campo="proceso",
                        valor="fija"
                    }
                }
            };


            POSTPAID_PRODUCTDETAIL.ConsultarDetalleProductoPostpagoResponse oConsultarDetalleProductoPostpagoResponse =
                Claro.Web.Logging.ExecuteMethod<POSTPAID_PRODUCTDETAIL.ConsultarDetalleProductoPostpagoResponse>
        (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_PRODUCTDETAIL, () =>
        {
            return Configuration.ServiceConfiguration.POSTPAID_PRODUCTDETAIL.consultarDetalleProductoPostpago(objRequest);
        });


            if (Int32.Parse(oConsultarDetalleProductoPostpagoResponse.auditResponse.codigoRespuesta) > Claro.Constants.NumberOne || Int32.Parse(oConsultarDetalleProductoPostpagoResponse.auditResponse.codigoRespuesta) < Claro.Constants.NumberZero)
            {
                throw new Claro.MessageException(Claro.SIACU.Constants.MessageNotConsultationProductsPostpaid);
            }

            PostpaidLinesResponse oGetPostpaidLinesResponse = new PostpaidLinesResponse()
            {
                ErrorMsg = oConsultarDetalleProductoPostpagoResponse.auditResponse.mensajeRespuesta,
                CodeError = oConsultarDetalleProductoPostpagoResponse.auditResponse.codigoRespuesta
            };

            DetailProductPost oDetalleServicio;

            foreach (var item in oConsultarDetalleProductoPostpagoResponse.datosPostpagoResponse.listaServicios)
            {
                oDetalleServicio = new DetailProductPost();
                oDetalleServicio.CoId = item.coId;
                oDetalleServicio.Estado = item.estado;
                oDetalleServicio.FechaActivacion = item.fechaActivacion;
                oDetalleServicio.Telefono = item.telefono;
                oGetPostpaidLinesResponse.ListDetailProduct.Add(oDetalleServicio);
            }

            return oGetPostpaidLinesResponse;

        }

        /// <summary>
        /// Método que obtiene el monto en disputa del cliente por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="decCustomerId">Id de cliente</param>
        /// <returns>Devuelve valor de monto en disputa.</returns>
        public static string GetAmountDispute(string strIdSession, string strTransaction, decimal decCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_customer_id", DbType.Decimal,30, ParameterDirection.Input, decCustomerId),
                new DbParameter("p_montodisputa", DbType.Decimal,30, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_GETMONTODISPUTA, parameters);

            return Convert.ToString(parameters[1].Value);
        }

        /// <summary>
        /// Método que obtiene el comportamiento de pago del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="lngCustomerId">Id de cliente</param>
        /// <returns>Devuelve valor de comportamiento de pago.</returns>
        public static string GetPaymentBehavior(string strIdSession, string strTransaction, long lngCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_CustomerID", DbType.Int64, ParameterDirection.Input, lngCustomerId),
                new DbParameter("p_Comportamiento", DbType.String, 12, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_COMPORTAMIENTO_PAGO, parameters);

            return Convert.ToString(parameters[1].Value);
        }

        public static string GetPaymentBehaviorTobe(Claro.Entity.AuditRequest audit, string lngCustomerId)
        {
            string comportamientoPago = string.Empty;
            Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Request.request()
            {
                consultarComportamientoPagoHistRequest = new Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Request.consultarComportamientoPagoHistRequest()
                {
                    customerId = lngCustomerId,
                    nroDocumento = string.Empty,
                    tipoDocumento = string.Empty,
                    listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Common.listaOpcional>()
                    {
                        new Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Common.listaOpcional(){
                            clave=string.Empty,
                            valor= string.Empty
}
                    }
                }
            };
            Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetPaymentBehavior.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_COMPORTAMIENTO_PAGO_TOBE, audit, request, false);
            if (response != null &&
                   response.consultarComportamientoPagoHistResponse != null &&
                   response.consultarComportamientoPagoHistResponse.responseAudit != null &&
                   response.consultarComportamientoPagoHistResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                   response.consultarComportamientoPagoHistResponse.responseData != null
                   )
            {
                comportamientoPago = response.consultarComportamientoPagoHistResponse.responseData.comportamientoPago;
            }
            return comportamientoPago;
        }


        public static string GetPaymentMethodTobe(Claro.Entity.AuditRequest audit, string lngCustomerId)
        {
            string formaPago = string.Empty;
            Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Request.request()
            {

                consultarFormaPagoRequest = new Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Request.consultarFormaPagoRequest()
                {
                    customerId = lngCustomerId,
                    linea = string.Empty,
                    listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Common.listaOpcional>()
                    {
                        new Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Common.listaOpcional(){
                            clave=string.Empty,
                            valor= string.Empty
}
                    }
                }
            };
            Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetPaymentMethod.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_FORMA_PAGO_TOBE, audit, request, false);
            if (response != null &&
                   response.consultarFormaPagoResponse != null &&
                   response.consultarFormaPagoResponse.responseAudit != null &&
                   response.consultarFormaPagoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                   response.consultarFormaPagoResponse.responseData != null
                   )
            {
                formaPago = response.consultarFormaPagoResponse.responseData.formaDePago;
            }
            return formaPago;
        }
        /// <summary>
        /// Método que obtiene el detalle del monto de disputa del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="decCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos DetailAmountDispute con información de monto en disputa.</returns>
        public static List<DetailAmountDispute> GetDetailAmountDispute(string strIdSession, string strTransaction, decimal decCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_customer_id", DbType.Decimal,20, ParameterDirection.Input, decCustomerId),
                new DbParameter("p_cur_montosd", DbType.Object, ParameterDirection.Output)
             
            };

            return DbFactory.ExecuteReader<List<DetailAmountDispute>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_GETDETMONTODISPUTA, parameters);
        }

        /// <summary>
        /// Método que obtiene la forma de pago del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="decCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto AffiliationToDebit con información de forma de pago.</returns>
        public static Entity.Dashboard.Postpaid.AffiliationToDebit GetAffiliationToDebit(string strIdSession, string strTransaction, decimal decCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_Customer_id", DbType.Decimal,20, ParameterDirection.Input, decCustomerId),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            
            };

            return DbFactory.ExecuteReader<AffiliationToDebit>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTAR_AFILIACION_DEBITO, parameters);
        }
        /// <remarks>GetPaymentMethodDetail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>26/11/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>02/12/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Metodo que devuelve el detalle de forma de pago de afiliacion de debito</Mot></item></list>
        public static Entity.Dashboard.Postpaid.AffiliationToDebit GetPaymentMethodDetail(AuditRequest audit, string strTransaction, string CustomerId, string FlagPlat, string csIdPub)
        {
            Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response.response response = null;
            Entity.Dashboard.Postpaid.AffiliationToDebit objAfiliacionDebito = null;
            List<Entity.Dashboard.Postpaid.AffiliationToDebit> lstAffiliationToDebit = new List<AffiliationToDebit>();


            try
            {

                Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Request.request()
                {
                    consultarDetalleFormaPagoRequest = new Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Request.consultarDetalleFormaPagoRequest()
                    {
                        customerId = FlagPlat.Equals(Claro.SIACU.Constants.ASIS) ? Claro.Convert.ToString(CustomerId) : Claro.Convert.ToString(csIdPub),
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Common.listaOpcional>()
                        {
                            new Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Common.listaOpcional(){
                                clave=string.Empty,
                                valor= string.Empty
                            }
                        }
                    }

                };


                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetPaymentMethodDetail.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DETALLE_PAGO, audit, request, false);

                if (response != null && response.consultarDetalleFormaPagoResponse != null && response.consultarDetalleFormaPagoResponse.responseAudit != null &&
                   response.consultarDetalleFormaPagoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString && response.consultarDetalleFormaPagoResponse.responseData != null && response.consultarDetalleFormaPagoResponse.responseData.listaDetalleFormaPago != null)
                {


                    foreach (listaAfiliacionDebito item in response.consultarDetalleFormaPagoResponse.responseData.listaDetalleFormaPago)
                    {
                        objAfiliacionDebito = new AffiliationToDebit();
                        objAfiliacionDebito.DES_ESTADO_SOLICITUD = item.desEstadoSolicitud;
                        objAfiliacionDebito.TIPO_DEBITO = item.tipoDebito;
                        objAfiliacionDebito.NUMERO_CUENTA_TARJETA = item.numeroCuentaTarjeta;
                        objAfiliacionDebito.FECHA_EXPIRACION_TARJETA = item.fechaExpiracionTarjeta;
                        objAfiliacionDebito.DESC_TIPO_CUENTA = item.descTipoCuenta;
                        objAfiliacionDebito.DESC_MONEDA = item.descMoneda;
                        objAfiliacionDebito.DESCRIPCION_LARGA = item.descripcionLarga;
                        objAfiliacionDebito.MONTO_MAXIMO = item.montoMaximo;
                        objAfiliacionDebito.FECHA_REGISTRO = item.fechaRegistro;
                        objAfiliacionDebito.FECHA_APROBADO = item.fechaAprobado;
                        objAfiliacionDebito.FECHA_DESAFILIACION = item.fechaDesafiliacion;
                        objAfiliacionDebito.MOTIVO = item.motivo;
                        objAfiliacionDebito.NUMERO_SOLICITUD = item.numeroSolicitud;
                        objAfiliacionDebito.FECHA_RECHAZO = item.fechaRechazo;
                        objAfiliacionDebito.MOTIVO_SOLICITUD = item.motivoSolicitud;
                        objAfiliacionDebito.COD_ENTIDAD = item.codEntidad;
                        objAfiliacionDebito.DNI_TITULAR = item.dniTitular;
                        objAfiliacionDebito.NOMBRE_TITULAR = item.nombreTitular;
                        objAfiliacionDebito.FECHA_PROCESO = item.fechaProceso;
                        objAfiliacionDebito.TIPO_CUENTA_BANCARIA = item.tipoCuentaBancaria;
                        objAfiliacionDebito.MONEDA = item.moneda;
                        lstAffiliationToDebit.Add(objAfiliacionDebito);
                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }
            return objAfiliacionDebito;

        }

        /// <summary>
        /// Método que obtiene la lista de monitoreo, cierres y reaperturas de casos.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="decStartRow">Fila inicial</param>
        /// <param name="decEndRow">Fila final</param>
        /// <param name="strCaseId">Id de caso</param>
        /// <param name="strCustomerAccount">Cuenta de cliente</param>
        /// <param name="strDateFrom">Fecha desde</param>
        /// <param name="strDateTo">Fecha hasta</param>
        /// <returns>Devuelve listado de objetos MonitoringCases con información de monitoreo de casos.</returns>
        public static List<MonitoringCases> GetMonitoringCases(string strIdSession, string strTransaction, decimal decStartRow, decimal decEndRow, string strCaseId, string strCustomerAccount, string strDateFrom, string strDateTo)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_inicio", DbType.Decimal,20, ParameterDirection.Input, decStartRow),
                new DbParameter("k_fin", DbType.Decimal,20, ParameterDirection.Input, decEndRow),
                new DbParameter("k_idcaso", DbType.String,255, ParameterDirection.Input, strCaseId),
                new DbParameter("k_idcliente", DbType.String,255, ParameterDirection.Input, strCustomerAccount),
                new DbParameter("k_fecdesde", DbType.String,255, ParameterDirection.Input, strDateFrom),
                new DbParameter("k_fechasta", DbType.String,255, ParameterDirection.Input, strDateTo),
                new DbParameter("k_cur_caso", DbType.Object, ParameterDirection.Output)
            
            };

            return DbFactory.ExecuteReader<List<MonitoringCases>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_LISTCASOSCLARIFY, parameters);
        }

        /// <summary>
        /// Método que obtiene la lista del historial del plan por número de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intContratoId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de servicio.</returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetHistoryServiceLine(string strIdSession, string strTransaction, int intContratoId)
        {
            List<Entity.Dashboard.Postpaid.Service> lstService = new List<Entity.Dashboard.Postpaid.Service>();
            Entity.Dashboard.Postpaid.Service objLine;

            POSTPAID_CONSULTCLIENT.HistPlanLinea[] objHistPlanLinea =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.HistPlanLinea[]>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.historicoPlanLinea(intContratoId); });


            if (objHistPlanLinea.Length >= 1)
            {
                foreach (var item in objHistPlanLinea)
                {
                    objLine = new Entity.Dashboard.Postpaid.Service();
                    objLine.VALIDO_DESDE = item.valido_desde;
                    objLine.PLAN_TARIFARIO = item.plan_tarifa;
                    objLine.CAMBIADO_POR = item.cambiado_por;
                    lstService.Add(objLine);

                }

            }
            return lstService;

        }

        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetHistoryServiceLineTobe(AuditRequest audit, string coId, List<Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Common.listaOpcional> listaOpcional)
        {
            List<Entity.Dashboard.Postpaid.Service> lstService = new List<Entity.Dashboard.Postpaid.Service>();

            Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Response.response response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Request.request()
                {
                    consultarHistPlanRequest = new Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Request.consultarHistPlanRequest()
                    {
                        coId = coId,
                        listaOpcional = listaOpcional
                    }

                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_HISTORIAL_PLAN_POR_COID, audit, request, false);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
                throw;

            }



            if (response != null &&
                response.consultarHistPlanResponse != null &&
                response.consultarHistPlanResponse.responseAudit != null &&
                response.consultarHistPlanResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.consultarHistPlanResponse.responseData != null
                )
            {
                var listaHistPlan = response.consultarHistPlanResponse.responseData.listaHistPlan;

                listaHistPlan.ForEach(x =>
                {

                    lstService.Add(new Claro.SIACU.Entity.Dashboard.Postpaid.Service()
                    {
                        VALIDO_DESDE = Convert.ToDate(x.fecha),
                        PLAN_TARIFARIO = x.tarifa,
                        CAMBIADO_POR = x.usuario,
                    });
                });

            }

            return lstService;

        }


        /// <summary>
        /// Método que devuelve los datos de Detalle Anotaciones por id de cliente y numero tickler.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strNumberTickler">Número de tickler</param>
        /// <returns>Devuelve objeto DetailAnnotation con la información de detalle de anotaciones.</returns>
        public static DetailAnnotation GetDetailAnnotation(string strIdSession, string strTransaction, string strCustomerId, string strNumberTickler)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_customer_id", DbType.Int64,30, ParameterDirection.Input, strCustomerId),
                new DbParameter("p_tickler_number", DbType.String,30, ParameterDirection.Input, strNumberTickler),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            
            };

            return DbFactory.ExecuteReader<DetailAnnotation>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_DETALLE_ANOTACION, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de anotaciones por id de cliente, id de contrato, estado, tipo y numero de tickler.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strContract">Contrato</param>
        /// <param name="strStatus">Estado</param>
        /// <param name="strNumberTickler">Número de tickler</param>
        /// <param name="strType">Tipo</param>
        /// <returns>Devuelve listado de objetos Annotation con información de anotaciones.</returns>
        public static List<Annotation> GetAnnotations(string strIdSession, string strTransaction, string strCustomerId, string strContract, string strStatus, string strNumberTickler, string strType)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_customer_id", DbType.Int64,30, ParameterDirection.Input, strCustomerId),
                new DbParameter("p_co_id", DbType.Int64,30, ParameterDirection.Input, strContract),
                new DbParameter("p_cstype", DbType.String,30, ParameterDirection.Input, strStatus),
                new DbParameter("p_tickler_number", DbType.String,30, ParameterDirection.Input, strNumberTickler),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            
            };

            return DbFactory.ExecuteReader<List<Annotation>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_CONSULTA_ANOTACIONES, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de garantías.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCodigoAplicacion">Código de aplicación</param>
        /// <param name="strUsuarioAplicacion">Usuario de aplicacion</param>
        /// <param name="strTipoConsulta">Tipo de consulta</param>
        /// <param name="strTipoDocumento">Tipo de documento</param>
        /// <param name="strNroDocumento">Número de documento</param>
        /// <param name="strOrigen">Origen</param>
        /// <param name="strNroCuenta">Número de cuenta</param>
        /// <param name="strDgTotal">Total</param>
        /// <returns>Devuelve objeto Warranty con información de garantía.</returns>
        public static Entity.Dashboard.Postpaid.Warranty GetWarranty(string strIdSession, string strTransaction, string strCodigoAplicacion, string strUsuarioAplicacion, string strTipoConsulta,
                                                                     string strTipoDocumento, string strNroDocumento, string strOrigen, string strNroCuenta, string strDgTotal)
        {
            Warranty objWarranty = null;
            Warranty objWarrantyItem = null;

            DbParameter[] parameters = new DbParameter[] { 
                new DbParameter("cod_aplicacion", DbType.String,250, ParameterDirection.Input, strCodigoAplicacion), 
                new DbParameter("usuario_aplicacion", DbType.String,250, ParameterDirection.Input, strUsuarioAplicacion),
                new DbParameter("origen_cuenta", DbType.String,250, ParameterDirection.Input, strOrigen), 
                new DbParameter("cod_cuenta", DbType.String,250, ParameterDirection.Input, strNroCuenta), 
                new DbParameter("detalle_dra", DbType.Object, ParameterDirection.Output), 
                new DbParameter("detalle_dg", DbType.Object, ParameterDirection.Output), 
                new DbParameter("codigo_rpta", DbType.String, 250, ParameterDirection.Output), 
                new DbParameter("msg_rpta", DbType.String, 250, ParameterDirection.Output) 
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_OAC, DbCommandConfiguration.SIACU_PR_CONSULTA_RA, parameters, (IDataReader reader) =>
            {
                objWarranty = new Warranty();

                while (reader.Read())
                {
                    objWarrantyItem = new Warranty()
                    {
                        NUMERO = reader["NUMERO_DRA"].ToString(),
                        FECHA = reader["FECHA_IN"].ToString(),
                        ESTADO = reader["ESTADO"].ToString(),
                        MONTO = reader["TOTAL_AMOUNT"].ToString(),
                        SALDO = reader["OPEN_AMOUNT"].ToString()

                    };
                    objWarranty.lstCabAccountWarranty.Add(objWarrantyItem);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        objWarrantyItem = new Warranty()
                        {
                            NUMERO = reader["NUMERO_DEP"].ToString(),
                            FECHA = reader["FECHA_IN"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            MONTO = reader["TOTAL_AMOUNT"].ToString(),
                            SALDO = reader["OPEN_AMOUNT"].ToString()
                        };
                        objWarranty.lstDebAccountWarranty.Add(objWarrantyItem);
                    }
                }
            });

            return objWarranty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerId"></param>
        /// <returns></returns>
        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> GetTelephoneNumbersPost(string strIdSession, string strTransaction, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_customer_id", DbType.String,255, ParameterDirection.Input, strCustomerId),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            
            };
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstTelephoneNumbersPost = null;
            Claro.SIACU.Entity.Dashboard.Postpaid.Service objTelephoneNumbers;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LINEAS_CLIENTE, parameters, (IDataReader reader) =>
            {
                objTelephoneNumbers = new Claro.SIACU.Entity.Dashboard.Postpaid.Service();
                lstTelephoneNumbersPost = new List<Entity.Dashboard.Postpaid.Service>();

                while (reader.Read())
                {
                    objTelephoneNumbers = new Claro.SIACU.Entity.Dashboard.Postpaid.Service()
                    {
                        NRO_CELULAR = Convert.ToString(reader["TELEFONO"]),
                        CONTRATO_ID = Convert.ToString(reader["CO_ID"]),
                        ESTADO_LINEA = Convert.ToString(reader["ESTADO"]),
                        PLAN = Convert.ToString(reader["PLAN"]),

                    };
                    lstTelephoneNumbersPost.Add(objTelephoneNumbers);
                }
            });

            return lstTelephoneNumbersPost;
        }

        /// <summary>
        /// Método que obtiene los datos de bolsa compartida.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strCuenta">Cuenta</param>
        /// <param name="strAplicacion">Aplicación</param>
        /// <param name="strIdTransaccion">Id de transacción</param>
        /// <param name="strIPAplicacion">Ip de aplicación</param>
        /// <param name="strUsrAplicacion">Usuario de aplicación</param>
        /// <param name="strCustomerid">Id de cliente</param>
        /// <param name="strtelephone">>Teléfono</param>
        /// <returns>Devuelve objeto SharedBag con información de bolsa compartida.</returns>
        public static Entity.Dashboard.Postpaid.SharedBag GetSharedBag(string strIdSession, string strCuenta, string strAplicacion, string strIdTransaccion, string strIPAplicacion, string strUsrAplicacion,
                                                                             string strCustomerid, string strtelephone)
        {
            string strMensaje;
            string strTipoLinea;
            string strTipoCliente;
            string strPlataforma;

            SharedBag objSharedBagPostpaid = new SharedBag();

            POSTPAID_BALANCE.PostPagoCorportaivoType[] objPostPagoCorportaivoType = new POSTPAID_BALANCE.PostPagoCorportaivoType[0];
            POSTPAID_BALANCE.PostpagoDatosConsultaType objPostpagoDatosConsultaType = new POSTPAID_BALANCE.PostpagoDatosConsultaType();
            POSTPAID_BALANCE.PostPagoType[] objPostPagoType = new POSTPAID_BALANCE.PostPagoType[0];
            POSTPAID_BALANCE.PrepagoDatosConsultaType objPrepagoDatosConsultaType = new POSTPAID_BALANCE.PrepagoDatosConsultaType();
            POSTPAID_BALANCE.audiType objaudiType =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_BALANCE.audiType>
            (strIdSession, strIdTransaccion, Configuration.ServiceConfiguration.POSTPAID_BALANCE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_BALANCE.consultaSaldo(new POSTPAID_BALANCE.audiTypeRequest()
                {
                    aplicacion = strAplicacion,
                    idTransaccion = strIdTransaccion,
                    ipAplicacion = strIPAplicacion,
                    usrAplicacion = strUsrAplicacion
                }, ref strtelephone, strCustomerid, out strMensaje, out strTipoLinea, out strTipoCliente, out strPlataforma, out objPrepagoDatosConsultaType, out objPostpagoDatosConsultaType, out objPostPagoType, out objPostPagoCorportaivoType);
            });

            if (!string.Equals(objaudiType.codigoRespuesta, Claro.Constants.NumberZeroString, StringComparison.OrdinalIgnoreCase))
            {

                throw new Claro.MessageException(objaudiType.mensajeRespuesta);
            }


            List<SharedBag> listSharedBag = new List<SharedBag>();

            if (objPostPagoType != null && objPostPagoType.Length >= 1)
            {
                foreach (var itemPostPago in objPostPagoType)
                {
                    listSharedBag.Add(new SharedBag()
                    {
                        GRUPO_BOLSA = strCuenta,
                        BOLSA = itemPostPago.bolsa,
                        UNIDAD = itemPostPago.unidad,
                        TOPE = itemPostPago.topeConsumo,
                        CONSUMO = itemPostPago.consumo,
                        SALDO = itemPostPago.saldo,
                        FECHAVIGENCIA = itemPostPago.fechaExpiracion,
                        OPCIONAL1 = itemPostPago.opcional1,
                        OPCIONAL2 = itemPostPago.opcional2
                    });
                }
            }

            objSharedBagPostpaid.lstAccountSharedBag = listSharedBag;

            return objSharedBagPostpaid;
        }

        /// <summary>
        /// Método que obtiene los datos de bolsa compartida para clientes que no tienen líneas asociadas.
        /// </summary>
        /// <param name="strCuenta">Cuenta</param>
        /// <param name="strAplicacion">Aplicación</param>
        /// <param name="strIdTransaccion">Id de transacción</param>
        /// <param name="strIPAplicacion">Ip de aplicación</param>
        /// <param name="strUsrAplicacion">Usuario de aplicación</param>
        /// <param name="strtelephone">Teléfono</param>
        /// <returns>Devuelve listado de objetos SharedBag con información de bolsa compartido.</returns>
        public static List<Entity.Dashboard.Postpaid.SharedBag> GetSharedBagLine(string strCuenta, string strAplicacion, string strIdTransaccion, string strIPAplicacion, string strUsrAplicacion,
                                                                                 string strtelephone)
        {
            string strMensaje = "";
            string strTipoLinea = "";
            string strTipoCliente = "";
            string strPlataforma = "";

            SharedBag objSharedBag;
            List<SharedBag> listSharedBag = new List<SharedBag>();

            POSTPAID_BALANCE.PostPagoCorportaivoType[] objPostPagoCorportativoType = new POSTPAID_BALANCE.PostPagoCorportaivoType[0];
            POSTPAID_BALANCE.PostpagoDatosConsultaType objPostpagoDatosConsultaType = new POSTPAID_BALANCE.PostpagoDatosConsultaType();
            POSTPAID_BALANCE.PostPagoType[] objPostPagoType = new POSTPAID_BALANCE.PostPagoType[0];
            POSTPAID_BALANCE.PrepagoDatosConsultaType objPrepagoDatosConsultaType = new POSTPAID_BALANCE.PrepagoDatosConsultaType();

            POSTPAID_BALANCE.audiType objaudiType = Configuration.ServiceConfiguration.POSTPAID_BALANCE.consultaSaldo(new POSTPAID_BALANCE.audiTypeRequest()
            {
                aplicacion = strAplicacion,
                idTransaccion = strIdTransaccion,
                ipAplicacion = strIPAplicacion,
                usrAplicacion = strUsrAplicacion
            }, ref strtelephone, string.Empty, out strMensaje, out strTipoLinea, out strTipoCliente, out strPlataforma, out objPrepagoDatosConsultaType, out objPostpagoDatosConsultaType, out objPostPagoType, out objPostPagoCorportativoType);

            if (string.Equals(objaudiType.codigoRespuesta, Claro.Constants.NumberOneNegativeString, StringComparison.OrdinalIgnoreCase))
            {
                throw new Claro.MessageException(objaudiType.mensajeRespuesta);
            }

            if (objPostPagoCorportativoType != null && objPostPagoCorportativoType.Length >= 1)
            {
                decimal dblTope;
                decimal dblConsumo;

                foreach (POSTPAID_BALANCE.PostPagoCorportaivoType itemPostPago in objPostPagoCorportativoType)
                {
                    objSharedBag = new SharedBag();
                    dblTope = Convert.ToDecimal(itemPostPago.topeLinea);
                    dblConsumo = Convert.ToDecimal(itemPostPago.topeConsumo);
                    objSharedBag.LINEA = strtelephone;
                    objSharedBag.BOLSA = Convert.ToString(itemPostPago.bolsa);
                    objSharedBag.UNIDAD = Convert.ToString(itemPostPago.unidad);
                    objSharedBag.TOPE = (dblTope > dblConsumo ? Convert.ToString(itemPostPago.topeConsumo) : Convert.ToString(itemPostPago.topeLinea));
                    objSharedBag.CONSUMO = Convert.ToString(itemPostPago.consumoLinea);
                    objSharedBag.SALDO = Convert.ToString(itemPostPago.saldo);
                    objSharedBag.FECHAVIGENCIA = Convert.ToString(itemPostPago.fechaExpiracion);
                    listSharedBag.Add(objSharedBag);
                }

                foreach (SharedBag item in listSharedBag)
                {
                    item.GRUPO_BOLSA = strCuenta;
                    switch (item.UNIDAD.Trim())
                    {
                        case Claro.SIACU.Constants.MB:
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString("###,###,###,##0.00");
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToDecimal(item.CONSUMO).ToString("###,###,###,##0.00");
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToDecimal(item.SALDO).ToString("###,###,###,##0.00");
                            }
                            break;
                        case Claro.SIACU.Constants.MMS:
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToString(Math.Round(Convert.ToDecimal(item.TOPE), Convert.ToInt(Claro.Constants.NumberZeroString)));
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToString(Math.Round(Convert.ToDecimal(item.CONSUMO), Convert.ToInt(Claro.Constants.NumberZeroString)));
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToString(Math.Round(Convert.ToDecimal(item.SALDO), Convert.ToInt(Claro.Constants.NumberZeroString)));
                            }
                            break;
                    }
                }
            }
            return listSharedBag;

        }

        /// <summary>
        /// Método que devuelve una lista con los datos de Relación de Planes.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTipoBusqueda">Tipo de búsqueda</param>
        /// <param name="strAccount">Cuenta</param>
        /// <param name="strOrden">Orden</param>
        /// <param name="strAscDes">-</param>
        /// <param name="strCiclo">Ciclo</param>
        /// <returns>Devuelve listado de objetos Bag con información de bolsas.</returns>
        public static List<Entity.Dashboard.Postpaid.Bag> GetBag(string strIdSession, string strTransaction, string strTipoBusqueda, string strAccount, string strOrden, string strAscDes, string strCiclo)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_TipBusq", DbType.String,1, ParameterDirection.Input, strTipoBusqueda),
                new DbParameter("p_ValBusq", DbType.String,24, ParameterDirection.Input, strAccount),
                new DbParameter("p_Orden", DbType.String,1, ParameterDirection.Input, strOrden),
                new DbParameter("p_AscDes", DbType.String,1, ParameterDirection.Input, strAscDes),
                new DbParameter("s_Ciclo", DbType.String,2, ParameterDirection.Input, strCiclo),
                new DbParameter("c_ref_cusor", DbType.Object, ParameterDirection.Output)

            };

            return DbFactory.ExecuteReader<List<Bag>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_GET_BOLSAS, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el detalle de Relación de Planes.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTipoBusqueda">Tipo de búsqueda</param>
        /// <param name="strAccount">Cuenta</param>
        /// <param name="strOrden">Orden</param>
        /// <param name="strAscDes">-</param>
        /// <param name="strShbolsa">Bolsa</param>
        /// <param name="strCiclo">Ciclo</param>
        /// <returns>Devuelve listado de objetos BagDetail con información de bolsa.</returns>
        public static List<Entity.Dashboard.Postpaid.BagDetail> GetDetailBag(string strIdSession, string strTransaction, string strTipoBusqueda, string strAccount, string strOrden, string strAscDes, string strShbolsa, string strCiclo)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_TipBusq", DbType.String,1, ParameterDirection.Input, strTipoBusqueda),
                new DbParameter("p_ValBusq", DbType.String,24, ParameterDirection.Input, strAccount),
                new DbParameter("p_Orden", DbType.String,1, ParameterDirection.Input, strOrden),
                new DbParameter("p_AscDes", DbType.String,1, ParameterDirection.Input, strAscDes),
                new DbParameter("p_Shbolsa", DbType.String,5, ParameterDirection.Input, strShbolsa),
                new DbParameter("s_Ciclo", DbType.String,2, ParameterDirection.Input, strCiclo),
                new DbParameter("c_ref_cusor", DbType.Object, ParameterDirection.Output)
            
            };

            return DbFactory.ExecuteReader<List<BagDetail>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_GET_BOLSAS_SERVICIOS, parameters);
        }

        /// <summary>
        /// Método que retorna la nomenclatura de planes.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Plan con información de planes.</returns>
        public static Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveResponse GetPlan(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveRequest objPlanMasivoRequest)
        {
            return RestService.PostInvoque<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.PLAN_MASIVO, objPlanMasivoRequest.Audit, objPlanMasivoRequest, true);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de Relación de Planes Corporativo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strAccount">Cuenta</param>
        /// <returns>Devuelve listado de objetos Bag con información de bolsas corporativas.</returns>
        public static Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse GetSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeRequests objSolicitudeRequests)
        {
            return RestService.PostInvoque<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_SOLICITUD, objSolicitudeRequests.Audit, objSolicitudeRequests, true);
        }

        public static Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse RegisterSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeRequests objRegisterSolicitudeRequests)
        {
            return RestService.PostInvoque<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.REGISTER_SOLICITUD, objRegisterSolicitudeRequests.Audit, objRegisterSolicitudeRequests, true);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de Contrato Suspendido por rango de fechas y motivo de suspensión. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strFechaIni">Fecha de inicio</param>
        /// <param name="strFechaFin">Fecha de fin</param>
        /// <param name="strReasonID">Id de razón</param>
        /// <returns>Devuelve listado de objetos Contract con información de contratos.</returns>
        public static List<Entity.Dashboard.Postpaid.Contract> GetSuspendedContract(string strIdSession, string strTransaction, string strFechaIni, string strFechaFin, string strReasonID)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_fechaini", DbType.String,30, ParameterDirection.Input, strFechaIni),
                new DbParameter("p_fechafin", DbType.String,30, ParameterDirection.Input, strFechaFin),
                new DbParameter("p_rs_id", DbType.String,30, ParameterDirection.Input, strReasonID),
                new DbParameter("cursor_salida_campos", DbType.Object, ParameterDirection.Output)
            };
            return DbFactory.ExecuteReader<List<Entity.Dashboard.Postpaid.Contract>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_CONTRATOS_SUSPENDIDOS, parameters);

        }

        /// <summary>
        /// Método que retorna una lista con los datos Pin y Puk Business por número de cuenta y tipo. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCuenta">Cuenta</param>
        /// <param name="strTipo">Tipo</param>
        /// <returns>Devuelve listado de objetos PinPuk con información de la misma.</returns>
        public static List<Entity.Dashboard.Postpaid.PinPuk> GetPinPuk(string strIdSession, string strTransaction, string strCuenta, string strTipo)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("svalor", DbType.String,30, ParameterDirection.Input, strCuenta),
                new DbParameter("stipo", DbType.String,30, ParameterDirection.Input, strTipo),
                new DbParameter("c_ref_cusor", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Postpaid.PinPuk>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_GET_PIN_PUK, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de las cuentas de ciente por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Account con información de cuenta.</returns>
        public static List<Entity.Dashboard.Postpaid.Account> GetSubAccount(string strIdSession, string strTransaction, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_customer_id", DbType.String,30, ParameterDirection.Input, strCustomerId),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Postpaid.Account>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_CONSULTA_SUBCUENTAS, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del Límite de Credito.
        /// </summary>
        /// <param name="strUser">Usuario</param>
        /// <param name="strNroCuenta">Número de cuenta.</param>
        /// <param name="strTipoConsultaOAC">Tipo de consulta OAC</param>
        /// <param name="sTipoConsultaDetalleOAC">Tipo de consulta detalle OAC</param>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de objetos CreditLimit con información de límite de crédito.</returns>
        public static List<Entity.Dashboard.Postpaid.CreditLimit> GetCreditLimit(string strUser, string strNroCuenta, string strTipoConsultaOAC, string sTipoConsultaDetalleOAC, string strIdSession, string strIdTransaction)
        {
            CreditLimit objCreditLimit;
            List<CreditLimit> listCreditLimit = null;
            POSTPAID_STATE.DetalleEstadoCuentaCabType[] objDECCabType = new POSTPAID_STATE.DetalleEstadoCuentaCabType[0];
            POSTPAID_STATE.DetalleEstadoCtaType[] objDECtype = new POSTPAID_STATE.DetalleEstadoCtaType[0];

            string strCodApliOAC = "";
            string strConsTipoServAjuste = "";
            string strFlagSaldoOAC = "";
            string sTipoConsultaPAGOAC = "";
            string strTamPaginaOAC = "";
            string strNroPagina1 = "";
            string strTxIDWSConsEstCtaOAC1 = "";
            string strNroTelfWSConsEstCtaOAC1 = "";
            string strFlgSoloDisputaWSConsEstCtaOAC1 = "";

            try
            {
                strCodApliOAC = KEY.AppSettings("strCodApliOAC");
                strConsTipoServAjuste = KEY.AppSettings("strConsTipoServAjuste");
                strFlagSaldoOAC = KEY.AppSettings("strFlagSaldoOAC", "");
                sTipoConsultaPAGOAC = KEY.AppSettings("sTipoConsultaPACOAC");
                strTamPaginaOAC = KEY.AppSettings("strTamPaginaOAC");
                strNroPagina1 = KEY.AppSettings("strNroPagina");
                strTxIDWSConsEstCtaOAC1 = KEY.AppSettings("strTxIDWSConsEstCtaOAC", "");
                strNroTelfWSConsEstCtaOAC1 = KEY.AppSettings("strNroTelfWSConsEstCtaOAC", "");
                strFlgSoloDisputaWSConsEstCtaOAC1 = KEY.AppSettings("strFlgSoloDisputaWSConsEstCtaOAC", "");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            string strCodApli = strCodApliOAC;
            string strTipServicio = strConsTipoServAjuste;
            string strFlagSaldo = strFlagSaldoOAC;
            string strTipoConsultaPAGO = sTipoConsultaPAGOAC;
            decimal decTamPagina = Convert.ToDecimal(strTamPaginaOAC);
            decimal decNroPagina = Convert.ToDecimal(strNroPagina1);
            string strTxIDWSConsEstCtaOAC = strTxIDWSConsEstCtaOAC1;
            string strNroTelfWSConsEstCtaOAC = strNroTelfWSConsEstCtaOAC1;
            string strFlgSoloDisputaWSConsEstCtaOAC = strFlgSoloDisputaWSConsEstCtaOAC1;


            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo(Claro.SIACU.Constants.CulturejaJp, true);
            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;


            try
            {

                Claro.Web.Logging.ExecuteMethod<AuditType>
                (strIdSession, strIdTransaction, Configuration.WebServiceConfiguration.POSTPAID_STATE, () =>
                {
                    return Configuration.WebServiceConfiguration.POSTPAID_STATE.consultaEstadoCuenta(
                                                                                              strTxIDWSConsEstCtaOAC,
                                                                                              strCodApli,
                                                                                              strUser,
                                                                                              strTipoConsultaOAC,
                                                                                              strTipServicio,
                                                                                              strNroCuenta,
                                                                                              strNroTelfWSConsEstCtaOAC,
                                                                                              strFlagSaldo,
                                                                                              strFlgSoloDisputaWSConsEstCtaOAC,
                                                                                              "",
                                                                                              "",
                                                                                              decTamPagina,
                                                                                              decNroPagina,
                                                                                              out objDECCabType);
                });

            }
            catch (Exception ex)
            {
                objDECCabType = new POSTPAID_STATE.DetalleEstadoCuentaCabType[0];
            }

            if (objDECCabType.Length == 1)
            {
                listCreditLimit = new List<CreditLimit>();
                objDECtype = objDECCabType[0].xDetalleTrx;
                if (objDECtype.Length > 0)
                {
                    for (int i = 0; i < objDECtype.Length; i++)
                    {
                        objCreditLimit = new CreditLimit();
                        if ((objDECtype[i].xTipoDocumento == Claro.SIACU.Constants.REC) || (objDECtype[i].xTipoDocumento == Claro.SIACU.Constants.FAC))
                        {
                            objCreditLimit.TIPO = Claro.SIACU.Constants.BillMajuscule;
                        }
                        else
                        {
                            objCreditLimit.TIPO = objDECtype[i].xTipoDocumento;
                        }
                        if (objDECtype[i].xTipoDocumento == Claro.SIACU.Constants.Payment)
                        {
                            objCreditLimit.DOCUMENTO = objDECtype[i].xNroOperacionPago;
                        }
                        else
                        {
                            objCreditLimit.DOCUMENTO = objDECtype[i].xNroDocumento;
                        }
                        switch (strTipoConsultaPAGO)
                        {
                            case Claro.SIACU.Constants.PAC:
                                if (Convert.ToString(objDECtype[i].xFechaRegistro).Length == 0)
                                {
                                    objCreditLimit.FECHA_MARCA_SMS1 = Convert.ToString(objDECtype[i].xFechaRegistro);
                                }
                                else
                                {
                                    objCreditLimit.FECHA_MARCA_SMS1 = Convert.ToDate(objDECtype[i].xFechaRegistro).ToString("dd/MM/yyyy");
                                }
                                objCreditLimit.FECHA_MARCA_SMS2 = "";
                                objCreditLimit.TIENE_PAC = objDECtype[i].xTipoDocumento;
                                objCreditLimit.MONTO_PAC = Convert.ToDecimal(objDECtype[i].xMontoDocumento);
                                objCreditLimit.NUMERO_PAC = objDECtype[i].xNroDocumento;
                                if (Convert.ToString(objDECtype[i].xFechaEmision).Length == 0)
                                {
                                    objCreditLimit.FECHA_BLOQ_EJECUCION = Convert.ToString(objDECtype[i].xFechaEmision);
                                }
                                else
                                {
                                    objCreditLimit.FECHA_BLOQ_EJECUCION = Convert.ToDate(objDECtype[i].xFechaEmision).ToString("dd/MM/yyyy");
                                }
                                if (Convert.ToString(objDECtype[i].xFechaVencimiento).Length == 0)
                                {
                                    objCreditLimit.FECHA_BLOQ_PROGRAMADA = Convert.ToString(objDECtype[i].xFechaVencimiento);
                                }
                                else
                                {
                                    objCreditLimit.FECHA_BLOQ_PROGRAMADA = Convert.ToDate(objDECtype[i].xFechaVencimiento).ToString("yyyy/MM/dd");
                                }
                                objCreditLimit.MONTO_CONSUMIDO = Convert.ToDecimal(objDECtype[i].xSaldoDocumento);
                                objCreditLimit.CARGO_FIJO = Convert.ToDecimal(objDECtype[i].xCargo);
                                objCreditLimit.CARGO_ADICIONAL = Convert.ToDecimal(objDECtype[i].xMontoFco);
                                objCreditLimit.CARGO_PRORRATEO = Convert.ToDecimal(objDECtype[i].xMontoFinan);
                                objCreditLimit.FECHA_DESBLOQ_EJECUCION = "";
                                objCreditLimit.DESCRIPCION_PAGO = Convert.ToString(objDECtype[i].xDescripDocumento);
                                objCreditLimit.IMPORTE_PENDIENTE = Convert.ToString(0);
                                if (Convert.ToDate(objCreditLimit.FECHA_BLOQ_PROGRAMADA.Replace("/", "-")) >= DateTime.Now)
                                {
                                    listCreditLimit.Add(objCreditLimit);
                                }
                                break;
                            case Claro.SIACU.Constants.Payment:
                                if (sTipoConsultaDetalleOAC == objDECtype[i].xNroDocumento)
                                {
                                    objCreditLimit.FECHA_MARCA_SMS1 = Convert.ToString(objDECtype[i].xFechaRegistro);
                                    objCreditLimit.FECHA_MARCA_SMS2 = Convert.ToString(objDECtype[i].xFechaRegistro);
                                    objCreditLimit.TIENE_PAC = objDECtype[i].xTipoDocumento;
                                    objCreditLimit.MONTO_PAC = Convert.ToDecimal(objDECtype[i].xMontoDocumento);
                                    objCreditLimit.NUMERO_PAC = objDECtype[i].xNroDocumento;
                                    objCreditLimit.FECHA_BLOQ_EJECUCION = Convert.ToString(objDECtype[i].xFechaEmision);
                                    objCreditLimit.FECHA_BLOQ_PROGRAMADA = Convert.ToString(objDECtype[i].xFechaVencimiento);
                                    objCreditLimit.MONTO_CONSUMIDO = Convert.ToDecimal(objDECtype[i].xSaldoDocumento);
                                    objCreditLimit.CARGO_FIJO = Convert.ToDecimal(objDECtype[i].xCargo);
                                    objCreditLimit.CARGO_ADICIONAL = Convert.ToDecimal(objDECtype[i].xMontoFco);
                                    objCreditLimit.CARGO_PRORRATEO = Convert.ToDecimal(objDECtype[i].xMontoFinan);
                                    objCreditLimit.FECHA_DESBLOQ_EJECUCION = Convert.ToString(objDECtype[i].xFechaEmision);
                                    objCreditLimit.DESCRIPCION_PAGO = Convert.ToString(objDECtype[i].xDescripDocumento);
                                    objCreditLimit.IMPORTE_PENDIENTE = Convert.ToString(0);

                                    listCreditLimit.Add(objCreditLimit);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return listCreditLimit;
        }

        /// <summary>
        /// Método que obtiene los datos del Limite de crédito por id de cliente y periodo. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPeriodo">Período</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto CreditLimit con información de límite de crédito.</returns>
        public static Entity.Dashboard.Postpaid.CreditLimit GetCharge(string strIdSession, string strTransaction, string strPeriodo, string strCustomerId)
        {
            Entity.Dashboard.Postpaid.CreditLimit objCreditLimit;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_PERIODO", DbType.String,6, ParameterDirection.Input, strPeriodo),
                new DbParameter("V_CUSTOMER_ID", DbType.String,15, ParameterDirection.Input, strCustomerId),
                new DbParameter("N_CARGO_FIJO", DbType.Decimal, ParameterDirection.Output),
                new DbParameter("N_CARGO_ADICIONAL", DbType.Decimal, ParameterDirection.Output),
                new DbParameter("I_FLAG", DbType.Int32, ParameterDirection.Output),
                new DbParameter("V_MSG_TEXT", DbType.String,250, ParameterDirection.Output)            
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_OAC, DbCommandConfiguration.SIACU_CSPI_OAC_P_CONS_CF_ADIC_X_CTA, parameters);

            objCreditLimit = new Entity.Dashboard.Postpaid.CreditLimit()
            {
                CARGO_FIJO = Convert.ToDecimal(parameters[2].Value),
                CARGO_ADICIONAL = Convert.ToDecimal(parameters[3].Value),
                FLAG = Convert.ToString(parameters[4].Value)
            };

            return objCreditLimit;
        }

        /// <summary>
        /// Método que obtiene una cadena con el id del tipo de documento por tipo de documento de identidad.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTypeDocIdentity">Tipo de documento de identidad</param>
        /// <returns>Devuelve cadena con código de tipo de documento.</returns>
        public static string GetCodeIdentityCard(string strIdSession, string strTransaction, string strTypeDocIdentity)
        {
            string strTypeDoc = "";

            List<Entity.Dashboard.Postpaid.Warranty> listWarranty;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)    
            };

            listWarranty = DbFactory.ExecuteReader<List<Entity.Dashboard.Postpaid.Warranty>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_TIPO_DOCIDENTIDAD, parameters);

            if (listWarranty != null && listWarranty.Count > 0)
            {
                foreach (var item in listWarranty)
                {
                    if (string.Equals(item.TIPO_DOC, strTypeDocIdentity, StringComparison.OrdinalIgnoreCase))
                    {
                        strTypeDoc = item.TIPO_COD_DOC;
                        break;
                    }
                }
            }

            return strTypeDoc;
        }

        /// <summary>
        /// Método que obtiene una lista con los datos de Renta Adelantada / Garantía.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTipoDocumento">Tipo de documento</param>
        /// <param name="strNroDocumento">Número de documento</param>
        /// <param name="strClass">Clase</param>
        /// <param name="strFechaIni">Fecha de inicio</param>
        /// <param name="strFechaFin">Fecha de fin</param>
        /// <returns>Devuelve listado de objetos Warranty con información de garantía.</returns>
        public static List<Entity.Dashboard.Postpaid.Warranty> GetWarrantyWS(string strIdSession, string strTransaction, string strTipoDocumento, string strNroDocumento, string strClass, string strFechaIni, string strFechaFin)
        {
            List<Entity.Dashboard.Postpaid.Warranty> listWarranty = new List<Warranty>();
            POSTPAID_SAP.ZSIAC_RFC_CON_DEV_GARANResponse objResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_SAP.ZSIAC_RFC_CON_DEV_GARANResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SAP, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_SAP.consultaDEPORenta(strFechaIni, strFechaFin, strClass, strTipoDocumento, strNroDocumento); });

            POSTPAID_SAP.ZDEPOSITOS[] objDeposito = objResponse.TI_DEPOSITOS;

            if (objDeposito != null)
            {
                foreach (var item in objDeposito)
                {
                    listWarranty.Add(new Warranty()
                    {
                        FECHA = item.FECHA_DEPOSITO,
                        MONTO = item.MONTO_DEPOSITO,
                    });
                }
            }

            return listWarranty;
        }

        /// <summary>
        /// Método que obtiene los datos de Planes por cuenta por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto ServiceAccount con información del servicio.</returns>
        public static Entity.Dashboard.Postpaid.ServiceAccount GetProductAccount(string strIdSession, string strTransaction, string strCustomerId)
        {

            ServiceAccount objServiceAccount = null;
            ServiceGSMAccount objServiceAccountGSMItem = null;
            ServiceHFCAccount objServiceAccountHFCtItem = null;

            DbParameter[] parameters = new DbParameter[] { 
                new DbParameter("P_CUSTOMER_ID", DbType.String,30, ParameterDirection.Input, strCustomerId), 
                new DbParameter("P_CUR_GSM", DbType.Object, ParameterDirection.Output), 
                new DbParameter("P_CUR_HFC", DbType.Object, ParameterDirection.Output), 
                new DbParameter("P_COD_ERR", DbType.Int32, ParameterDirection.Output), 
                new DbParameter("P_DES_ERR", DbType.String,30, ParameterDirection.Output)        
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TIM163_VENTA_CONVERG, parameters, (IDataReader reader) =>
            {
                objServiceAccount = new ServiceAccount();
                objServiceAccountGSMItem = new ServiceGSMAccount();
                objServiceAccountHFCtItem = new ServiceHFCAccount();
                while (reader.Read())
                {
                    objServiceAccountGSMItem = new ServiceGSMAccount()
                    {
                        NRO_SERV = Convert.ToString(reader["NroServ"]),
                        CONTRATO = Convert.ToString(reader["CO_ID"]),
                        ESTADO = Convert.ToString(reader["ESTADO"]),
                        COD_PLAN = "",
                        PLAN_TARIFARIO = Convert.ToString(reader["PLAN_TARIFARIO"]),
                        COD_COMBO = "",
                        COMBO = Convert.ToString(reader["COMBO"]),
                        DESCUENTO = Convert.ToString(reader["DESCUENTO"]),
                        DIR_INSTALACION = Convert.ToString(reader["DirecInstalac"])
                    };

                    objServiceAccount.ListServiceAccountGSM.Add(objServiceAccountGSMItem);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        objServiceAccountHFCtItem = new ServiceHFCAccount()
                        {
                            CONTRATO = Convert.ToString(reader["CO_ID"]),
                            ESTADO = Convert.ToString(reader["ESTADO"]),
                            COD_PLAN = "",
                            PLAN_TARIFARIO = Convert.ToString(reader["PLAN_TARIFARIO"]),
                            DIR_INSTALACION = Convert.ToString(reader["DireccionInstalacion"]),
                            COD_COMBO = "",
                            COMBO = Convert.ToString(reader["COMBO"]),
                            DESCUENTO = Convert.ToString(reader["DESCUENTO"]),
                            TELEFONIA_FIJA = Convert.ToString(reader["TELEFONIA"]),
                            INTERNET_FIJO = Convert.ToString(reader["INTERNET"]),
                            CLAROTV = Convert.ToString(reader["CABLE_TV"])
                        };

                        objServiceAccount.ListServiceAccountHFC.Add(objServiceAccountHFCtItem);
                    }
                }
            });
            return objServiceAccount;
        }

        /// <summary>
        /// Método que obtiene un Listado Servicios con por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos ServiceLTEAccount con información del servicio LTE.</returns>
        public static List<Entity.Dashboard.Postpaid.ServiceLTEAccount> GetServiceCustomerId(string strIdSession, string strTransaction, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.String,30, ParameterDirection.Input, strCustomerId),
                new DbParameter("P_CURSOR_ISDN", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,50, ParameterDirection.Output)
            
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Postpaid.ServiceLTEAccount>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_DATOS_SERVICIO, parameters);
        }

        /// <summary>
        /// Método que obtiene los datos de Planes por cuenta por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Equipment con información de equipos.</returns>
        public static List<Entity.Dashboard.Postpaid.Equipment> GetEquipmentAccount(string strIdSession, string strTransaction, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.String,30, ParameterDirection.Input, strCustomerId),
                new DbParameter("CUR_EQUIPOS", DbType.Object, ParameterDirection.Output),
                new DbParameter("V_ERRNUM", DbType.String,50, ParameterDirection.Output),
                new DbParameter("V_ERRMSJ", DbType.String,50, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Postpaid.Equipment>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_EQUIPOS, parameters);
        }

        /// <summary>
        /// Método que retorna los datos del límite de crédito.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCodAplicacion">Código de aplicación</param>
        /// <param name="strUsuario">Usuario</param>
        /// <param name="strTipoServicio">Tipo de servicio</param>
        /// <param name="strCodCuenta">Código de cuenta</param>
        /// <param name="strTipoDocumento">Tipo de documento</param>
        /// <param name="strNroDocumento">Número de documento</param>
        /// <returns>Devuelve objeto CreditLimitDetail con información de límite de crédito.</returns>
        public static Entity.Dashboard.Postpaid.CreditLimitDetail GetDetailPAC(string strIdSession, string strTransaction, string strCodAplicacion, string strUsuario, string strTipoServicio, string strCodCuenta, string strTipoDocumento, string strNroDocumento)
        {
            Entity.Dashboard.Postpaid.CreditLimitDetail objCreditLimit;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("pv_codaplicacion", DbType.String,100, ParameterDirection.Input, strCodAplicacion),
                new DbParameter("pv_usuario", DbType.String,12, ParameterDirection.Input, strUsuario),
                new DbParameter("pv_tipo_servicio", DbType.String,150, ParameterDirection.Input, strTipoServicio),
                new DbParameter("pv_cod_cuenta", DbType.String,150, ParameterDirection.Input, strCodCuenta),
                new DbParameter("pv_tipo_documento", DbType.String,20, ParameterDirection.Input, strTipoDocumento),
                new DbParameter("pv_nro_documento", DbType.String,20, ParameterDirection.Input, strNroDocumento),
                new DbParameter("xv_id_doc_oac", DbType.String, 100, ParameterDirection.Output),
                new DbParameter("xv_estado_documento", DbType.String,400, ParameterDirection.Output),
                new DbParameter("xv_fec_emision", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_fec_vencimiento", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_fec_pago", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_tipo_moneda", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_importe_original", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_importe_pagado", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_saldo", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_importe_reclamado", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_importe_fco", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_importe_financiamiento", DbType.String,40, ParameterDirection.Output),             
                new DbParameter("xv_saldo_fco", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_saldo_financiamiento", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_nro_doc_financiamiento", DbType.String,150, ParameterDirection.Output),
                new DbParameter("xv_cod_rpta", DbType.String,40, ParameterDirection.Output),
                new DbParameter("xv_msg_rpta", DbType.String,400, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_OAC, DbCommandConfiguration.SIACU_SP_CON_EQUIPOS, parameters);



            objCreditLimit = new Entity.Dashboard.Postpaid.CreditLimitDetail()
            {
                FEC_PAGO = Convert.ToString(parameters[10].Value),
                MONTO_PAGO = Convert.ToDecimal(parameters[13].Value),
                NRO_PAC = Convert.ToString(parameters[20].Value)

            };
            return objCreditLimit;

        }

        /// <summary>
        /// Método que obtiene el id del cliente por el numero de cintillo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCintillo">Cintillo</param>
        /// <returns>Devuelve cadena con id de cliente.</returns>
        public static string GetCustomerIDxCintillo(string strIdSession, string strTransaction, string strCintillo)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_cintillo", DbType.String, ParameterDirection.Input, strCintillo),
                new DbParameter("ac_cli_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int32, ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String, 255, ParameterDirection.Output)
            };

            return DbFactory.ExecuteScalarToString(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_CLIENTEXCINTILLO, parameters, "CUSTOMER_ID");
        }

        /// <summary>
        /// Método que retorna los datos de cliente HFC por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto Customer con información de cliente.</returns>
        public static Entity.Dashboard.Postpaid.Customer GetDataCustomerByCustomerIdHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strCustomerId)
        {
            Entity.Dashboard.Postpaid.Customer objCustomer = new Entity.Dashboard.Postpaid.Customer();
            POSTPAID_HFC.consultarDatosPorCodigoClienteEAIResponse oConsultationCustomerOut =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC.consultarDatosPorCodigoClienteEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.POSTPAID_HFC, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_HFC.consultarDatosPorCodigoCliente(new POSTPAID_HFC.consultarDatosPorCodigoClienteEAIRequest()
                {
                    consultarDatosPorCodigoClienteEaiRequest = new POSTPAID_HFC.ConsultarDatosPorCodigoClienteEAIInput()
                    {
                        cabeceraRequest = new POSTPAID_HFC.CabeceraRequest()
                        {
                            idTransaccion = strIdTransaction,
                            ipAplicacion = strIpApplication,
                            nombreAplicacion = strApplicationName,
                            usuarioAplicacion = strUserApplication
                        },
                        cuerpoRequest = new POSTPAID_HFC.CuerpoDCLRequest()
                        {
                            codigoCliente = strCustomerId
                        }
                    }
                });
            });

            POSTPAID_HFC.CabeceraResponse oHeaderOutput = oConsultationCustomerOut.consultarDatosPorCodigoClienteEaiResponse.cabeceraResponse;
            POSTPAID_HFC.CuerpoDCLResponse oBodyOutput = oConsultationCustomerOut.consultarDatosPorCodigoClienteEaiResponse.cuerpoResponse;
            POSTPAID_HFC.DatosClienteType[] oTempCustomer = oBodyOutput.listaDatosPorCodigoCliente;

            objCustomer.NOMBRES = String.Empty;
            objCustomer.APELLIDOS = String.Empty;
            objCustomer.CUENTA = String.Empty;
            objCustomer.CUSTOMER_ID = String.Empty;
            objCustomer.RAZON_SOCIAL = String.Empty;
            objCustomer.TIPO_DOC = String.Empty;
            objCustomer.NRO_DOC = String.Empty;
            objCustomer.SEXO = String.Empty;
            objCustomer.TELEFONO = String.Empty;
            objCustomer.TELEFONO_CONTACTO = String.Empty;
            objCustomer.FAX = String.Empty;
            objCustomer.ESTADO_CIVIL = String.Empty;
            objCustomer.ESTADO_CIVIL_ID = String.Empty;
            objCustomer.FECHA_NAC = String.Empty;
            objCustomer.LUGAR_NACIMIENTO_DES = String.Empty;
            objCustomer.DNI_RUC = String.Empty;
            objCustomer.DOMICILIO = String.Empty;
            objCustomer.NOMBRE_COMERCIAL = String.Empty;
            objCustomer.CONTACTO_CLIENTE = String.Empty;
            objCustomer.REPRESENTANTE_LEGAL = String.Empty;
            objCustomer.EMAIL = String.Empty;
            objCustomer.CARGO = String.Empty;
            objCustomer.ASESOR = String.Empty;
            objCustomer.URBANIZACION_FAC = String.Empty;
            objCustomer.DISTRITO_FAC = String.Empty;
            objCustomer.DISTRITO = String.Empty;
            objCustomer.PROVINCIA_FAC = String.Empty;
            objCustomer.POSTAL_FAC = String.Empty;
            objCustomer.DEPARTEMENTO_FAC = String.Empty;
            objCustomer.DEPARTAMENTO = String.Empty;
            objCustomer.PAIS_FAC = String.Empty;
            objCustomer.URBANIZACION_LEGAL = String.Empty;
            objCustomer.DISTRITO_LEGAL = String.Empty;
            objCustomer.PROVINCIA_LEGAL = String.Empty;
            objCustomer.PROVINCIA = String.Empty;
            objCustomer.POSTAL_LEGAL = String.Empty;
            objCustomer.DEPARTEMENTO_LEGAL = String.Empty;
            objCustomer.PAIS_LEGAL = String.Empty;
            objCustomer.LUGAR_NACIMIENTO_ID = String.Empty;
            objCustomer.TIPO_CLIENTE = String.Empty;
            objCustomer.SEGMENTO = String.Empty;
            objCustomer.REFERENCIA = String.Empty;
            objCustomer.CODIGO_PLANO_INST = String.Empty;

            if (oHeaderOutput.codigoRespuesta != Claro.Constants.NumberZeroString)
            {
                return null;
            }

            for (int i = 0; i < oTempCustomer.Length; i++)
            {
                objCustomer.NOMBRES = Convert.ToString(oTempCustomer[i].nombre);
                objCustomer.APELLIDOS = Convert.ToString(oTempCustomer[i].apellido);
                objCustomer.CODIGO_PLANO_INST = Convert.ToString(oTempCustomer[i].codigoPlanoInst);
                objCustomer.CUENTA = Convert.ToString(oTempCustomer[i].cuenta);
                objCustomer.CUSTOMER_ID = Convert.ToString(oTempCustomer[i].customerId);
                objCustomer.RAZON_SOCIAL = Convert.ToString(oTempCustomer[i].razonSocial);
                objCustomer.TIPO_DOC = Convert.ToString(oTempCustomer[i].tipDocumento);
                objCustomer.NRO_DOC = Convert.ToString(oTempCustomer[i].nroDocumento);
                objCustomer.SEXO = Convert.ToString(oTempCustomer[i].sexo);
                objCustomer.TELEFONO = Convert.ToString(oTempCustomer[i].telefonoref);
                objCustomer.TELEF_REFERENCIA = Convert.ToString(oTempCustomer[i].telefonoref);
                objCustomer.TELEFONO_CONTACTO = Convert.ToString(oTempCustomer[i].telefonoref2);
                objCustomer.FAX = Convert.ToString(oTempCustomer[i].fax);
                objCustomer.ESTADO_CIVIL = Convert.ToString(oTempCustomer[i].estadoCivil);
                objCustomer.ESTADO_CIVIL_ID = Convert.ToString(oTempCustomer[i].estadoCivilId);
                objCustomer.FECHA_NAC = Convert.ToString(oTempCustomer[i].fechaNacimiento);
                objCustomer.DOMICILIO_FAC = Convert.ToString(oTempCustomer[i].direccionFact);
                objCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(oTempCustomer[i].lugarNacimiento);
                objCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(oTempCustomer[i].nacionalidad);
                objCustomer.DNI_RUC = Convert.ToString(oTempCustomer[i].rucDni);
                objCustomer.NOMBRE_COMERCIAL = Convert.ToString(oTempCustomer[i].nombreComercial);
                objCustomer.CONTACTO_CLIENTE = Convert.ToString(oTempCustomer[i].contactoCliente);
                objCustomer.REPRESENTANTE_LEGAL = Convert.ToString(oTempCustomer[i].repLegal);
                objCustomer.EMAIL = Convert.ToString(oTempCustomer[i].email);
                objCustomer.CARGO = Convert.ToString(oTempCustomer[i].cargo);
                objCustomer.ASESOR = Convert.ToString(oTempCustomer[i].asesor);
                objCustomer.DOMICILIO = Convert.ToString(oTempCustomer[i].direccionFact);
                objCustomer.URBANIZACION_FAC = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                objCustomer.REFERENCIA = Convert.ToString(oTempCustomer[i].urbanizacionFact);
                objCustomer.DISTRITO_FAC = Convert.ToString(oTempCustomer[i].distritoFact);
                objCustomer.DISTRITO = Convert.ToString(oTempCustomer[i].distritoFact);
                objCustomer.PROVINCIA_FAC = Convert.ToString(oTempCustomer[i].provinciaFact);
                objCustomer.PROVINCIA = Convert.ToString(oTempCustomer[i].provinciaFact);
                objCustomer.POSTAL_FAC = Convert.ToString(oTempCustomer[i].codigoPostalFact);
                objCustomer.DEPARTEMENTO_FAC = Convert.ToString(oTempCustomer[i].departamentoFact);
                objCustomer.DEPARTAMENTO = Convert.ToString(oTempCustomer[i].departamentoFact);
                objCustomer.PAIS_FAC = Convert.ToString(oTempCustomer[i].paisFact);
                objCustomer.URBANIZACION_LEGAL = Convert.ToString(oTempCustomer[i].urbanizacionInst);
                objCustomer.DISTRITO_LEGAL = Convert.ToString(oTempCustomer[i].distritoInst);
                objCustomer.PROVINCIA_LEGAL = Convert.ToString(oTempCustomer[i].provinciaInst);
                objCustomer.POSTAL_LEGAL = Convert.ToString(oTempCustomer[i].codigoPostalInst);
                objCustomer.DEPARTEMENTO_LEGAL = Convert.ToString(oTempCustomer[i].departamentoInst);
                objCustomer.PAIS_LEGAL = Convert.ToString(oTempCustomer[i].paisInst);
                objCustomer.LUGAR_NACIMIENTO_ID = Convert.ToString(oTempCustomer[i].nacionalidad);
                objCustomer.TIPO_CLIENTE = Convert.ToString(oTempCustomer[i].tipoCliente);
                objCustomer.SEGMENTO = Convert.ToString(oTempCustomer[i].segmento);
                objCustomer.NOMBRE_COMPLETO = objCustomer.NOMBRES + " " + objCustomer.APELLIDOS;
                objCustomer.FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct);

                objCustomer.origen = Claro.ConfigurationManager.AppSettings("ModalityBSCS7");

                objCustomer.oCUENTA = new Account()
                {
                    TIPO_CLIENTE = oTempCustomer[i].tipoCliente,
                    SEGMENTO = oTempCustomer[i].segmento,
                    CICLO_FACTURACION = oTempCustomer[i].cicloFact,
                    NICHO = Convert.ToString(oTempCustomer[i].nichoId),
                    ESTADO_CUENTA = oTempCustomer[i].estadoCuenta,
                    RESPONSABLE_PAGO = oTempCustomer[i].responPago,
                    LIMITE_CREDITO = Convert.ToString(oTempCustomer[i].limiteCredito),
                    FECHA_ACT = Convert.ToDate(oTempCustomer[i].fechaAct).ToString(),
                    CONSULTOR = Convert.ToString(oTempCustomer[i].consultor)
                };
                //Logica Segmento PROY-SEGMENTOVALOR 140351
                string strDocumentType = KEY.AppSettings("strDocumentType");
                string strDocumentLong = KEY.AppSettings("strDocumentLong");
                string strCaracterRellenoNroDoc = KEY.AppSettings("strCaracterRellenoNroDoc");
                string strSplitDNIRUC = KEY.AppSettings("strSplitDNIRUC");
                string oTipoDoc = objCustomer.TIPO_DOC;

                string[] strSplitSegmentoDocumentoF = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[0].Split(',');

                string[] strSplitSegmentoDocumentoV = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[1].Split(',');

                for (int ii = 0; ii < strSplitSegmentoDocumentoF.Length; ii++)
                {
                    if (oTipoDoc == strSplitSegmentoDocumentoF[ii])
                    {
                        oTipoDoc = strSplitSegmentoDocumentoV[ii];
                    }
                }

                Entity.Dashboard.Postpaid.Customer oCustomers = new Entity.Dashboard.Postpaid.Customer()
                {
                    TIPO_DOC = strDocumentType,
                    NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + objCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)),
                    LONG_NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + objCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)).Length.ToString()
                };
                objCustomer.SEGMENTO2 = Claro.Web.Logging.ExecuteMethod<String>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetSegmentCustomerQuery(oCustomers, strIdSession, strUserApplication, strIpApplication, strIdTransaction, strApplicationName); });

                //FIN PROY-SEGMENTOVALOR 140351
            }
            return objCustomer;
        }

        /// <summary>
        /// Método que obtiene una lista con las notificaciones.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDataSearch">Dato de búsqueda</param>
        /// <param name="strTypePhone">Tipo de teléfono</param>
        /// <returns>Devuelve listado de objetos Instant con información de instantáneas.</returns>
        public static List<Instant> GetListInstant(string strIdSession, string strTransaction, string strDataSearch, string strTypePhone)
        {
            //*PENDIENTE UNIFICAR EN LA VISTA LA PROPIEDAD DESCRIPCIÓN
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_DATO_BUSQUEDA", DbType.String, ParameterDirection.Input, strDataSearch),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, ParameterDirection.Input, strTypePhone),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Instant>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_INSTANTANEA_VIGENTE, parameters);
        }

        /// <summary>
        /// Método que obtiene una lista con las notificaciones para HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDataSearch">Dato de búsqueda</param>
        /// <param name="strTypePhone">Tipo de teléfono</param>
        /// <returns>Devuelve listado de objetos Instant con información de instantáneas.</returns>
        public static List<Instant> GetListInstantByContract(string strIdSession, string strTransaction, string strDataSearch, string strTypePhone)
        {
            //*PENDIENTE UNIFICAR EN LA VISTA LA PROPIEDAD DESCRIPCIÓN
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_DATO_BUSQUEDA", DbType.String, ParameterDirection.Input, strDataSearch),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, ParameterDirection.Input, strTypePhone),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Instant>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_INSTANTANEA_VIGENTE_HFC, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el detale de llamada de Larga Distancia Internacional por número de factura.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intTypeCall">Tipo de llamada</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos CallDetail con información de detalle de llamadas.</returns>
        public static List<CallDetail> GetDetailLongDistance(string strIdSession, string strTransaction, int intTypeCall, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_TIPOLLAMADA", DbType.Int32, ParameterDirection.Input, intTypeCall),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output), 
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<CallDetail>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1460, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el Detalle de Roaming Internacional por número de factura.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos CallDetail con información de detalle de llamadas.</returns>
        public static List<Entity.Dashboard.Postpaid.CallDetail> GetInternationalRoamingDetail(string strIdSession, string strTransaction, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output), 
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<CallDetail>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1470, parameters);
        }

        /// <summary>
        /// Método que obtiene una lista de Consultas de Deudas de Cuenta Cliente por Documento de Identidad.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve listado de objetos ApadeceDebt con información de deuda.</returns>
        public static List<Entity.Dashboard.Postpaid.ApadeceDebt> GetDebtDetail(string strIdSession, string strTransaction, string strDocumentNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_NRODOCUM", DbType.String, ParameterDirection.Input, strDocumentNumber),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output), 
                new DbParameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_ERROR", DbType.String, ParameterDirection.Output) 
            };

            return DbFactory.ExecuteReader<List<ApadeceDebt>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_OBTENER_CTA_DEUDA, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del Tráfico Local Adicional TIM PRO.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intCallType">Tipo de llamada</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos LocalTrafficDetail con información de detalle de tráfico local.</returns>
        public static List<Entity.Dashboard.Postpaid.LocalTrafficDetail> GetDetailLocalTrafficTimPro(string strIdSession, string strTransaction, int intCallType, string strInvoiceNumber)
        {
            List<Entity.Dashboard.Postpaid.LocalTrafficDetail> listLocalTrafficDetail = new List<LocalTrafficDetail>();
            Entity.Dashboard.Postpaid.LocalTrafficDetail objLocalTrafficDetail = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32, ParameterDirection.Input, intCallType), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1405_PRO, parameters, (IDataReader reader) =>
           {
               objLocalTrafficDetail = new Entity.Dashboard.Postpaid.LocalTrafficDetail();

               while (reader.Read())
               {
                   objLocalTrafficDetail = new Entity.Dashboard.Postpaid.LocalTrafficDetail()
                   {
                       MSISDN = Convert.ToString(reader["MSISDN"]),
                       RTP = Convert.ToString(reader["RTP"]),
                       ONNET = Convert.ToString(reader["ONNET"]),
                       OFFNET_A_FIJO = Convert.ToString(reader["OFFNET_A_FIJO"]),
                       OFFNET_A_CELULAR = Convert.ToString(reader["OFFNET_A_CELULAR"]),
                       IMPORTE = Convert.ToString(reader["IMPORTE"]),
                       IMPORTE2 = Convert.ToString(reader["IMPORTE2"])
                   };
                   listLocalTrafficDetail.Add(objLocalTrafficDetail);
               }
           });

            return listLocalTrafficDetail;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del Tráfico Local Adicional TIM MAX.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intCallType">Tipo de llamada</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos LocalTrafficDetail con información de detalle de tráfico local.</returns>
        public static List<Entity.Dashboard.Postpaid.LocalTrafficDetail> GetDetailLocalTrafficTimMax(string strIdSession, string strTransaction, int intCallType, string strInvoiceNumber)
        {
            List<Entity.Dashboard.Postpaid.LocalTrafficDetail> listLocalTrafficTimMax = new List<LocalTrafficDetail>();
            Entity.Dashboard.Postpaid.LocalTrafficDetail objLocalTrafficDetailTimMax = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32, ParameterDirection.Input, intCallType), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_Tols_Consultartemptag1405_Max, parameters, (IDataReader reader) =>
            {
                objLocalTrafficDetailTimMax = new Entity.Dashboard.Postpaid.LocalTrafficDetail();

                while (reader.Read())
                {
                    objLocalTrafficDetailTimMax = new Entity.Dashboard.Postpaid.LocalTrafficDetail()
                    {
                        MSISDN = Convert.ToString(reader["MSISDN"]),
                        ONNET = Convert.ToString(reader["ONNET"]),
                        OFF_NET = Convert.ToString(reader["OFF_NET"]),
                        OFF_ONNET_OFFNET = Convert.ToString(reader["OFF_ONNET_OFFNET"]),
                        IMPORTE = Convert.ToString(reader["IMPORTE"])

                    };
                    listLocalTrafficTimMax.Add(objLocalTrafficDetailTimMax);
                }
            });
            return listLocalTrafficTimMax;
        }

        /// <summary>
        /// Método que devuelve una lista de Detalle de Otros Cargos del servicio Postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos OtherDetails con información de otros detalles del servicio.</returns>
        public static List<Entity.Dashboard.Postpaid.OtherDetails> GetDetailsOtherConcepts(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32, ParameterDirection.Input, strGroupBox), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<OtherDetails>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1225, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista de Detalle Servicios TIM por número de factura. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos CallDetailTimService con información de detalle de servicio llamadas.</returns>
        public static List<Entity.Dashboard.Postpaid.CallDetailTimService> GetTimServiceDetails(string strIdSession, string strTransaction, string strInvoiceNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<CallDetailTimService>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1425, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el histórico de facturas por id de cliente y cantidad de registros.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intQuantityRegist">Cantidad de registro</param>
        /// <param name="strCustomerID">Id de cliente</param>
        /// <returns>Devuelve listado de objetos ReceiptHistory con información de historial de recibos.</returns>
        public static List<Entity.Dashboard.Postpaid.ReceiptHistory> GetHistoryInvoice(string strIdSession, string strTransaction, int intQuantityRegist, string strCustomerID)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_CODIGOCLIENTE", DbType.String, ParameterDirection.Input, strCustomerID),
                new DbParameter("K_PageSize", DbType.String, ParameterDirection.Input, intQuantityRegist),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<ReceiptHistory>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENER_RECIBOS, parameters);
        }

        /// <summary>
        /// Método que retorna una lista con los detalles de mensajes por número de factura y Msisdn.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <returns>Devuelve listado de objetos CallDetailSMS con información de detalle de llamadas y sms.</returns>
        public static List<Entity.Dashboard.Postpaid.CallDetailSMS> SMSDetails(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String, ParameterDirection.Input, strMsisdn),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<CallDetailSMS>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1480, parameters);
        }

        /// <summary>
        /// Método que retorna la cantidad de mensajes enviados por número de factura y Msisdn.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <returns>Devuelve cantidad de mensajes enviados.</returns>
        public static decimal GetAmountSMSDetails(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn)
        {
            decimal decCantidadSMS = 0;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String, ParameterDirection.Input, strMsisdn),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARCANTIDADSMS, parameters, (IDataReader reader) =>
            {

                if (reader.Read())
                {
                    decCantidadSMS = Convert.ToDecimal(reader["AddUsage4"]);
                }
            });
            return decCantidadSMS;
        }

        /// <summary>
        /// Método que retorna el listado de los contratos por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strUser">Usuario</param>
        /// <param name="strSystem">Sistema</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <returns>Devuelve listado de objetos PhoneContract con información de contrato de teléfono.</returns>
        public static List<PhoneContract> GetPhoneContract(string strIdSession, string strTransaction, string strUser, string strSystem, string strTelephone)
        {
            List<PhoneContract> listPhoneContract = null;

            POSTPAID_SERVICES.contratosTelefonoResponse objResponse =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_SERVICES.contratosTelefonoResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SERVICES, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_SERVICES.contratosTelefono(new POSTPAID_SERVICES.contratosTelefonoRequest()
                {
                    login = strUser,
                    msisdn = strTelephone
                });
            });


            if (Convert.ToInt(objResponse.errNum) == 0)
            {
                listPhoneContract = new List<PhoneContract>();

                foreach (var item in objResponse.contrato)
                {
                    listPhoneContract.Add(new PhoneContract()
                    {
                        CUSTCODE = item.custCode,
                        COID = item.coId,
                        NOMBRE = item.nombre,
                        ESTADO = item.estado,
                        FECHA = item.fecha,
                        RAZON = item.razon,
                    });
                }
            }
            else
            {
                throw new Claro.MessageException(Claro.SIACU.Constants.MessageErrorConsumingData);
            }

            return listPhoneContract;

        }

        // GetPhone Tobe

        public static List<PhoneContract> GetPhoneContractTobe(AuditRequest audit, string strTransaction, string strUser, string strSystem, string strTelephone, string plataformaAT, string flagMigrado)
        {
            Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Response.response response = null;
            List<PhoneContract> listPhoneContract = new List<PhoneContract>();
            PhoneContract objPhoneContract = null;
            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Request.request
                {
                    consultarContratosLineaRequest = new Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Request.ConsultarContratosLineaRequest()
                    {
                        linea = ConfigurationManager.AppSettings("strConstPrefijo") + strTelephone,
                        flagMigrado = flagMigrado,          /*
                                                           Flag para indicar situación de línea
                                                                    0: No migrado - BSCS70
                                                                    1: Migrado - BSCS70 -> BSCSIX
                                                                    2: No Migrado - BSCSIX
                                                           */
                    }
                };


                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_TELEFONO_CONTRATO, audit, request, false);


                if (response != null &&
                   response.consultarContratosLineaResponse != null &&
                   response.consultarContratosLineaResponse.responseAudit != null &&
                   response.consultarContratosLineaResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                   response.consultarContratosLineaResponse.responseDataContrato != null)

                    foreach (var contrato in response.consultarContratosLineaResponse.responseDataContrato.contratos)
                    {
                        objPhoneContract = new PhoneContract();
                        objPhoneContract.CUSTCODE = contrato.csCode;
                        objPhoneContract.COID = contrato.coId;
                        objPhoneContract.COID_PUB = contrato.contractIdPub;
                        objPhoneContract.NOMBRE = contrato.adrFullname;
                        objPhoneContract.ESTADO = contrato.desCoStatus;
                        objPhoneContract.FECHA = Convert.ToDate(contrato.coLastStatusChangeDate).ToString("yyyy-MM-dd HH:mm:ss");
                        objPhoneContract.origen = contrato.origen;
                        foreach (var reason in contrato.reason)
                        {
                            objPhoneContract.rsCode = reason.rsCode;
                            objPhoneContract.RAZON = reason.rsDes;
                            objPhoneContract.rsState = reason.rsState;
                        }

                        listPhoneContract.Add(objPhoneContract);

                    }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            return listPhoneContract;
        }


        public static List<Entity.Dashboard.Postpaid.HistoricDelivery> GetHistoricDeliveryToBe(string strIdSession, string strTransaction, string CustomerID, Int32 intPageSize)
        {
            Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response.ConsultarHistoricoEntregasResponse responseHDT = null;
            List<Entity.Dashboard.Postpaid.HistoricDelivery> listHistoricoEntrega = new List<Entity.Dashboard.Postpaid.HistoricDelivery>();
            HistoricDelivery objHD = null;

            AuditRequest audit = new AuditRequest();

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Request.ConsultarHistoricoEntregasRequest request = new Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Request.ConsultarHistoricoEntregasRequest();
                request.PV_CUSTOMER_ID = CustomerID;
                request.PV_NRO_RECIBO = "";
                request.PN_CANT_RECIBOS = intPageSize.ToString();

                responseHDT = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response.ConsultarHistoricoEntregasResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_HISTORICO_ENTREGAS, audit, request, false);


                if (responseHDT != null &&
                    responseHDT.P_CURSOR != null &&
                    responseHDT.XV_CODIGORESPUESTA == Claro.Constants.NumberZeroString)
                    foreach (var item in responseHDT.P_CURSOR.P_CURSOR_Row)
                    {
                        objHD = new HistoricDelivery();
                        objHD._RECIBO = item.nro_Recibo;
                        objHD._FECHAINI = item.fechaRegistro;
                        objHD._FECEMISION = item.fechaEmision;
                        objHD._FECHAFIN = item.fechaVencimiento;
                        objHD._FECENTREGA = item.periodo;
                        listHistoricoEntrega.Add(objHD);
                    }

            }
            catch (Exception ex)
            {
                throw new Claro.MessageException(ex.Message);
                throw;
            }

            return listHistoricoEntrega;
        }
        /// <summary>
        /// Método que obtiene una lista con la consulta Servicio por id de contrato. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos ContractServices con información de servicios contratados.</returns>
        public static List<ContractServices> GetContractServices(string strIdSession, string strTransaction, string strContractId)
        {
            List<ContractServices> listContractServices = null;

            POSTPAID_SERVICES.consultaServicioEstadoResponse objResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_SERVICES.consultaServicioEstadoResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SERVICES, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_SERVICES.consultaServicioEstado(new POSTPAID_SERVICES.consultaServicioEstadoRequest()
                {
                    coId = strContractId
                });
            });


            if (Convert.ToInt(objResponse.errNum) == 0)
            {
                if (objResponse.servicioEstado != null && objResponse.servicioEstado.Length > 0)
                {
                    listContractServices = new List<ContractServices>();
                    ContractServices objContractServices;

                    foreach (var item in objResponse.servicioEstado)
                    {
                        objContractServices = new ContractServices();
                        objContractServices.COD_GRUPO = item.coGrp;
                        objContractServices.DES_GRUPO = item.deGrp;
                        objContractServices.POS_GRUPO = item.noGrp;
                        objContractServices.COD_SERV = item.coSer;
                        objContractServices.DES_SERV = item.deSer;
                        objContractServices.POS_SERV = item.noSer;
                        objContractServices.COD_EXCLUYENTE = item.coExcl;
                        objContractServices.DES_EXCLUYENTE = item.deExcl;
                        objContractServices.ESTADO = item.estado;
                        objContractServices.FECHA_VALIDEZ = item.valido;
                        objContractServices.MONTO_CARGO_SUS = item.suscrip;
                        objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;
                        objContractServices.CUOTA_MODIF = item.cuota;
                        objContractServices.MONTO_FINAL = item.final;
                        objContractServices.PERIODOS_VALIDOS = item.period;
                        objContractServices.BLOQUEO_DESACT = item.BloqDes;
                        objContractServices.BLOQUEO_ACT = item.BloqAct;
                        listContractServices.Add(objContractServices);
                    }

                }
            }
            else
            {
                throw new Claro.MessageException(Claro.SIACU.Constants.MessageErrorConsumingData);
            }

            return listContractServices;
        }

        /// <summary>
        /// Método que obtiene una lista con la consulta Servicio por id de contrato. 
        /// </summary>
        /// <param name="audit">Auditoria</param>
        /// <param name="coIdPub">Id de contrato</param>
        /// <returns>Devuelve listado de objetos ContractServices con información de servicios contratados.</returns>
        public static List<ContractServices> GetContractServicesToBe(AuditRequest audit, string coIdPub, string phone)
        {
            List<ContractServices> listContractServices = new List<ContractServices>();
            ContractServices objContractServices;
            ServiciosCatalogoTobe.CatalogoResponse response = null;
            string fechaServicioBasico = "";

            try
            {
                ServiciosCatalogoTobe.CatalogoRequest request = new ServiciosCatalogoTobe.CatalogoRequest()
                {
                    obtenerServiciosPlanPorContratoRequest = new Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe.ObtenerServiciosPlanPorContratoRequest()
                    {
                        contractIdPub = coIdPub,
                        flagConsulta = "A",
                        validaExcluyente = "",
                        linea = phone
                    }
                };
                response = RestService.PostInvoque<ServiciosCatalogoTobe.CatalogoResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_LISTA_SERVICIOS_CONTRATO, audit, request, false);

                foreach (var item in response.obtenerServiciosPlanPorContratoResponse.responseData.serviciosAsociados)
                {

                    if (!string.IsNullOrEmpty(item.numeroGrupo) && !string.IsNullOrEmpty(item.descripcionGrupo)
                    && !string.IsNullOrEmpty(item.bloqueoActivacion) && item.bloqueoActivacion != "X" &&
                    !string.IsNullOrEmpty(item.bloqueoDesactivacion) && item.bloqueoDesactivacion != "X"
                    && !string.IsNullOrEmpty(item.tipoServicio) || item.itemCargo.ToUpper() == ConfigurationManager.AppSettings("strItemCargo").ToUpper())
                    {
                        objContractServices = new ContractServices();
                        objContractServices.TIPOSERVICIO = item.tipoServicio;
                        objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;
                        objContractServices.MONTO_CARGO_SUS = item.suscripcion;
                        if (string.IsNullOrEmpty(item.numeroGrupo) && item.descripcionServicio.ToUpper() == ConfigurationManager.AppSettings("strDescripcionServicio").ToUpper() && item.itemCargo.ToUpper() == ConfigurationManager.AppSettings("strItemCargo").ToUpper())
                        {
                            objContractServices.POS_GRUPO = Claro.Constants.NumberOneString;
                            objContractServices.DES_GRUPO = ConfigurationManager.AppSettings("strGroupServicioBasico");

                        }
                        else
                        {
                            if (objContractServices.TIPOSERVICIO.ToUpper() == ConfigurationManager.AppSettings("strTipoSerOPT").ToUpper())
                            {
                                objContractServices.MONTO_CARGO_SUS = Claro.Constants.NumberZeroString;
                            }
                            else
                            {
                                objContractServices.MONTO_CARGO_SUS = item.suscripcion;
                            }

                            if (objContractServices.TIPOSERVICIO.ToUpper() == ConfigurationManager.AppSettings("strTipoSerAON").ToUpper())
                            {
                                objContractServices.MONTO_CARGO_FIJO = Claro.Constants.NumberZeroString;
                            }
                            else
                            {
                                objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;
                            }

                            if (item.numeroGrupo == Claro.Constants.NumberOneString && item.descripcionGrupo == ConfigurationManager.AppSettings("strGroupServicioBasico"))
                            {
                                fechaServicioBasico = item.validoDesde;
                            }
                            objContractServices.DES_GRUPO = item.descripcionGrupo;
                            objContractServices.POS_GRUPO = item.numeroGrupo;
                        }
                        objContractServices.FECHA_VALIDEZ = item.validoDesde;
                        objContractServices.COD_SERV = item.codigoServicio;
                        objContractServices.DES_SERV = item.descripcionServicio;
                        objContractServices.POS_SERV = item.codigoServicio;
                        objContractServices.COD_EXCLUYENTE = item.codigoExcluyente;
                        objContractServices.DES_EXCLUYENTE = item.descripcionExcluyente;
                        objContractServices.ESTADO = item.estado;
                        objContractServices.CUOTA_MODIF = item.cuota;
                        objContractServices.PERIODOS_VALIDOS = item.periodo;
                        if (!string.IsNullOrEmpty(objContractServices.PERIODOS_VALIDOS))
                        {
                            objContractServices.PERIODOS_VALIDOS = item.periodo + " " + item.periodoTipo;
                        }
                        else
                        {
                            objContractServices.PERIODOS_VALIDOS = string.Empty;
                        }

                        objContractServices.BLOQUEO_DESACT = item.bloqueoDesactivacion;
                        objContractServices.BLOQUEO_ACT = item.bloqueoActivacion;
                        objContractServices.DAPO = item.daPo;
                        objContractServices.SERVICIO = item.valorServicio;
                        objContractServices.CARGO = item.itemCargo;
                        objContractServices.CATSERVICIO = item.categoriaServicio;

                        listContractServices.Add(objContractServices);

                    }
                }
                foreach (var item in listContractServices)
                {
                    if (item.POS_GRUPO.ToUpper() == Claro.Constants.NumberOneString
                         && item.DES_SERV.ToUpper() == ConfigurationManager.AppSettings("strDescripcionServicio").ToUpper())
                    {
                        item.FECHA_VALIDEZ = fechaServicioBasico;
                    }

                }

                Claro.Web.Logging.Info("", "requestStringMODIFICADO ", string.Format("LLAVE: {0} -- {1}", ConfigurationManager.AppSettings("strDescripcionServicio").ToUpper(), ConfigurationManager.AppSettings("strItemCargo").ToUpper()));
                


                listContractServices = listContractServices
                    .OrderBy(x => x.POS_GRUPO)
                    .ThenBy(z => z.DES_SERV)
                    .ToList();
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            return listContractServices;
        }


        /// <summary>
        /// Método que obtiene una lista de Servicios Comerciales Contratados para HFC por id de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos ContractServices con información de servicios contratados.</returns>
        public static List<ContractServices> GetContractServicesHFCLTE(string strIdSession, string strTransaction, string strContractId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.Int32, ParameterDirection.Input, strContractId),
                new DbParameter("p_tmdes", DbType.String,100, ParameterDirection.Output),
                new DbParameter("p_tmcode", DbType.Int32, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.Int32, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String,200, ParameterDirection.Output)                
            };

            List<ContractServices> listContractServices = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTA_SERVICIOS_USADOS_HFC, parameters, (IDataReader reader) =>
            {
                listContractServices = new List<ContractServices>();

                while (reader.Read())
                {
                    listContractServices.Add(new ContractServices()
                    {
                        DES_GRUPO = Convert.ToString(reader["de_grp"]),
                        POS_GRUPO = Convert.ToString(reader["no_grp"]),
                        COD_SERV = Convert.ToString(reader["co_ser"]),
                        DES_SERV = Convert.ToString(reader["de_ser"]),
                        POS_SERV = Convert.ToString(reader["no_ser"]),
                        COD_EXCLUYENTE = Convert.ToString(reader["co_excl"]),
                        DES_EXCLUYENTE = Convert.ToString(reader["de_excl"]),
                        ESTADO = Convert.ToString(reader["estado"]),
                        FECHA_VALIDEZ = Convert.ToString(reader["valido_desde"]),
                        MONTO_CARGO_SUS = Convert.ToString(reader["suscrip"]),
                        MONTO_CARGO_FIJO = Convert.ToString(reader["cargofijo"]),
                        CUOTA_MODIF = Convert.ToString(reader["cuota"]),
                        PERIODOS_VALIDOS = Convert.ToString(reader["periodos"]),
                        BLOQUEO_ACT = Convert.ToString(reader["bloq_act"]),
                        BLOQUEO_DESACT = Convert.ToString(reader["bloq_des"]),
                        SPCODE = Convert.ToString(reader["spcode"]),
                        SNCODE = Convert.ToString(reader["sncode"])
                    });
                }
            });

            return listContractServices;
        }

        /// <summary>
        /// Método que obtiene una lista de Consulta de Equipos por id de cliente y id de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Deco con información de equipos decodificadores.</returns>
        public static List<Deco> GetComputerQuery(string strIdSession, string strTransaction, string strContractId, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_customer_id", DbType.String,255, ParameterDirection.Input, strCustomerId),
                new DbParameter("av_cod_id", DbType.String,255, ParameterDirection.Input,strContractId),
                new DbParameter("ac_equ_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int32, ParameterDirection.Output),
                 new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
            };

            List<Deco> listDeco = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_P_CONSULTA_EQU, parameters, (IDataReader reader) =>
            {
                listDeco = new List<Deco>();

                while (reader.Read())
                {
                    listDeco.Add(new Deco()
                    {
                        id_transaccion = Convert.ToString(reader["idtransaccion"]),
                        codigo_material = Convert.ToString(reader["codigo_material"]),
                        codigo_sap = Convert.ToString(reader["codigo_sap"]),
                        numero_serie = Convert.ToString(reader["numero_serie"]),
                        macadress = Convert.ToString(reader["macaddress"]),
                        descripcion_material = Convert.ToString(reader["descripcion_material"]),
                        abrev_material = Convert.ToString(reader["abrev_material"]),
                        estado_material = Convert.ToString(reader["estado_material"]),
                        precio_almacen = Convert.ToString(reader["precio_almacen"]),
                        codigo_cuenta = Convert.ToString(reader["codigo_cuenta"]),
                        componente = Convert.ToString(reader["componente"]),
                        centro = Convert.ToString(reader["centro"]),
                        idalm = Convert.ToString(reader["idalm"]),
                        almacen = Convert.ToString(reader["almacen"]),
                        tipo_equipo = Convert.ToString(reader["tipo_equipo"]),
                        id_producto = Convert.ToString(reader["idproducto"]),
                        id_cliente = Convert.ToString(reader["id_cliente"]),
                        modelo = Convert.ToString(reader["modelo"]),
                        fecusu = Convert.ToString(reader["fecusu"]),
                        codusu = Convert.ToString(reader["codusu"]),
                        convertertype = Convert.ToString(reader["convertertype"]),
                        servicio_principal = Convert.ToString(reader["servicio_principal"]),
                        headend = Convert.ToString(reader["headend"]),
                        ephomeexchange = Convert.ToString(reader["ephomeexchange"]),
                        numero = Convert.ToString(reader["numero"])
                    });
                }
            });

            return listDeco;

        }

        /// <summary>
        /// Método que obtiene una lista con los datos de la consulta de Tareas Programadas.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strIdCode">Código Id</param>
        /// <param name="strAccount">Cuenta</param>
        /// <param name="strStartDate">Fecha inicial</param>
        /// <param name="strEndDate">Fecha final</param>
        /// <param name="strState">Estado</param>
        /// <param name="strAdviser">Asesor</param>
        /// <param name="strTransactionType">Tipo de transacción</param>
        /// <param name="strInteractionCode">Código de interacción</param>
        /// <param name="strCacDac">Cac Dac</param>
        /// <returns>Devuelve listado de objetos ScheduledTransaction con información de tareas programadas.</returns>
        public static List<ScheduledTransaction> GetScheduledTransaction(string strIdSession, string strTransaction, string strIdCode, string strAccount, string strStartDate, string strEndDate, string strState, string strAdviser, string strTransactionType, string strInteractionCode, string strCacDac)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_servi_coid", DbType.String,24, ParameterDirection.Input, strIdCode),
                new DbParameter("p_fecha_desde", DbType.Date, ParameterDirection.Input,Convert.ToDate(strStartDate)),
                new DbParameter("p_fecha_hasta", DbType.Date, ParameterDirection.Input,Convert.ToDate(strEndDate)),
                new DbParameter("p_estado", DbType.String,20, ParameterDirection.Input,strState),
                new DbParameter("p_asesor", DbType.String,100, ParameterDirection.Input, strAdviser),
                new DbParameter("p_cuenta", DbType.String,100, ParameterDirection.Input,strAccount),
                new DbParameter("p_tipotransaccion", DbType.String,100, ParameterDirection.Input, strTransactionType),
                new DbParameter("p_codinteraccion", DbType.String,100, ParameterDirection.Input,strInteractionCode),
                new DbParameter("p_caddac", DbType.String,100, ParameterDirection.Input,strCacDac),
                new DbParameter("p_cursor", DbType.Object,255, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<ScheduledTransaction>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_SP_CONSULTA_POSTT_SERVICIOPROG, parameters);
        }

        /// <summary>
        /// Método que obtiene una lista de servicios comerciales por código de servicio.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strServiceCode">Código de servicio</param>
        /// <returns>Devuelve listado de objetos ServiceBSCS con información de servicio BSCS.</returns>
        public static List<ServiceBSCS> GetServiceBSCS(string strIdSession, string strTransaction, string strServiceCode)
        {
            List<ServiceBSCS> listServiceBSCS = null;
            POSTPAID_SERVICES.serviciosBSCSServComercialResponse objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_SERVICES.serviciosBSCSServComercialResponse>(strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SERVICES, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_SERVICES.serviciosBSCSServComercial(new POSTPAID_SERVICES.serviciosBSCSServComercialRequest()
                {
                    coSer = strServiceCode
                });
            });

            if (objResponse != null)
            {
                if (Convert.ToInt(objResponse.errNum) == 0)
                {
                    if (objResponse.servicioBscs.Length > 0)
                    {
                        listServiceBSCS = new List<ServiceBSCS>();

                        foreach (var item in objResponse.servicioBscs)
                        {
                            listServiceBSCS.Add(new ServiceBSCS()
                            {
                                SERVICIO = item.servicio,
                                PAQUETE = item.paquete,
                                ESTADO = item.estado
                            });
                        }
                    }
                }
                else
                {
                    throw new Claro.MessageException(Claro.SIACU.Constants.MessageErrorConsumingData);
                }
            }

            return listServiceBSCS;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de las peticiones por número de contrato y un rango de fechas. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <param name="strStartDate">Fecha inicial</param>
        /// <param name="strEndDate">Fecha final</param>
        /// <returns>Devuelve listado de objetos Petitions con información de peticiones.</returns>
        public static List<Petitions> GetPetitions(string strIdSession, string strTransaction, string strContractId, string strStartDate, string strEndDate)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_id_contrato", DbType.Int64, ParameterDirection.Input, strContractId),
                new DbParameter("p_fechaini", DbType.Date, ParameterDirection.Input, Convert.ToDate(strStartDate)),
                new DbParameter("p_fechafin", DbType.Date, ParameterDirection.Input, Convert.ToDate(strEndDate)),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Petitions>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTAR_PETICIONES, parameters);

        }

        /// <remarks>GetPetitionsLegacy</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>10/12/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Devuelve listado de objetos Petitions con información de peticiones To Be </Mot></item></list>


        public static List<Petitions> GetPetitionsLegacy(AuditRequest audit, string strContractId, string coIdPub, string migrado, string strStartDate, string strEndDate)
        {
            List<Petitions> listPeticion = new List<Petitions>();
            Petitions ObjPetition = null;
            Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response.response response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request.Request request = new Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request.Request()
                {
                    obtenerPeticionesRequest = new Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request.obtenerPeticionesRequest()
                    {
                        coId = string.IsNullOrEmpty(strContractId) ? string.Empty : strContractId,
                        coIdPub = string.IsNullOrEmpty(coIdPub) ? string.Empty : coIdPub,
                        fechaInicio = strStartDate,
                        fechaFin = strEndDate,

                        migrado = migrado,
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon.listaOpcional>()
                    {
                        new Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon.listaOpcional()
                        {
                        clave = string.Empty,
                        valor = string.Empty
                        }
                      
                    },
                    }
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_PETICIONES_LEGACY, audit, request, false);



            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (response != null && response.obtenerPeticionesResponse != null &&
                response.obtenerPeticionesResponse.responseAudit != null &&
                response.obtenerPeticionesResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.obtenerPeticionesResponse.responseData != null &&
                response.obtenerPeticionesResponse.responseData.listaPeticiones != null)
            {
                foreach (var item in response.obtenerPeticionesResponse.responseData.listaPeticiones)
                {
                    {
                        ObjPetition = new Petitions();
                        ObjPetition.Fecha_Peticion = System.Convert.ToDateTime(item.fechaPeticion);
                        ObjPetition.Fecha_Vencimiento = System.Convert.ToDateTime(item.fechaVencimiento);
                        ObjPetition.Estado = item.estadoPeticion;
                        ObjPetition.Accion = item.accionSolicitada;
                        ObjPetition.Usuario = item.usuario;
                        ObjPetition.OrdenId = item.ordenId;
                        listPeticion.Add(ObjPetition);

                    }

                }

            }

            return listPeticion;

        }

        /// <remarks>GetPetitionsTobeFija</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>10/12/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Devuelve listado de objetos Petitions con información de peticiones To Be </Mot></item></list>


        public static List<Petitions> GetPetitionsTobeFija(AuditRequest audit, string strContractId, string coIdPub, string migrado, string strStartDate, string strEndDate)
        {
            List<Petitions> listPeticion = new List<Petitions>();
            Petitions ObjPetition = null;
            Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response.responseFija response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request.Request request = new Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request.Request()
                {
                    obtenerPeticionesRequest = new Entity.Dashboard.Postpaid.Legacy.GetPetitions.Request.obtenerPeticionesRequest()
                    {
                        coId = string.IsNullOrEmpty(strContractId) ? string.Empty : strContractId,
                        coIdPub = string.IsNullOrEmpty(coIdPub) ? string.Empty : coIdPub,
                        fechaInicio = strStartDate,
                        fechaFin = strEndDate,

                        migrado = migrado,
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon.listaOpcional>()
                    {
                        new Entity.Dashboard.Postpaid.Legacy.GetPetitions.Conmon.listaOpcional()
                        {
                        clave = "flagCuentaPrepSinContrato",
                        valor = "true"
                        }
                      
                    },
                    }
                };
                response = RestService.PostInvoqueRest<Entity.Dashboard.Postpaid.Legacy.GetPetitions.Response.responseFija>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_PETICIONES_FIJA, audit, request, false);



            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }


            if (response != null &&
                response.obtenerPeticionesResponseType.auditResponse != null &&
                response.obtenerPeticionesResponseType.auditResponse.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.obtenerPeticionesResponseType.listaPeticiones != null)
            {
                foreach (var item in response.obtenerPeticionesResponseType.listaPeticiones)
                {
                    {
                        ObjPetition = new Petitions();
                        ObjPetition.Fecha_Peticion = System.Convert.ToDateTime(item.fechaPeticion);
                        ObjPetition.Fecha_Vencimiento = System.Convert.ToDateTime(item.fechaVencimiento);
                        ObjPetition.Estado = item.estadoPeticion;
                        ObjPetition.Accion = item.accionSolicitada;
                        ObjPetition.Usuario = item.usuario;
                        ObjPetition.OrdenId = item.ordenId;
                        listPeticion.Add(ObjPetition);
                    }

                }

            }

            return listPeticion;

        }


        /// <summary>
        /// Método que devuelve una lista con los datos de los trios por número de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Striations con información de triación.</returns>
        public static List<Striations> GetTriaciones(string strIdSession, string strTransaction, int intContractId)
        {
            List<Striations> listStriations = null;
            POSTPAID_CONSULTCLIENT.ConsultaTriados[] objTriados = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.ConsultaTriados[]>(strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () => { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.consultaTriados(intContractId); });

            if (objTriados.Length >= 1)
            {
                listStriations = new List<Striations>();

                foreach (var item in objTriados)
                {
                    listStriations.Add(new Striations()
                    {
                        NUM_TRIO = item.num_trio + Claro.Constants.NumberOne,
                        TIPO_DESTINO = item.Tipo_Destino,
                        FACTOR = item.factor,
                        TELEFONO = item.telefono,
                        TIPO_TRIADO = item.tipo_triado,
                        DESTINO_TRIO = item.dest_trio,
                        CODIGO_TIPO_DESTINO = item.Cod_Tipo_Destino
                    });
                }
            }

            return listStriations;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del historial de triaciones por número de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <returns>Devuelve listado de objetos HistoricalStriations con información del historial de triaciones.</returns>
        public static List<HistoricalStriations> GetHistoricalStriations(string strIdSession, string strTransaction, string strTelephone)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_numero_telefonico", DbType.String, ParameterDirection.Input, strTelephone),
                new DbParameter("cursor_salida_campos", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<HistoricalStriations>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_TRIACIONES_HISTORICO, parameters);
        }

        /// <summary>
        /// Método que obtiene una lista con los datos de Consumo/Saldo.
        /// </summary>
        /// <param name="objRecharge">Objeto Recarga</param>
        /// <param name="objAudit">Objeto Auditoria</param>
        /// <param name="strResponseCode">Código de respuesta</param>
        /// <param name="strPostReply">Envío de respuesta</param>
        /// <param name="strPlatform">Plataforma</param>
        /// <returns>Devuelve listado de objetos Recharge con información de recarga.</returns>
        public static List<Recharge> GetBagBalance(Recharge objRecharge, Claro.Entity.AuditRequest objAudit, out string strResponseCode, out string strPostReply, out string strPlatform)
        {
            string gConstValor00 = "";
            string gConstLineaJanusBroker = "";
            string gConstPlataformaJanusBroker = "";
            string gConstConsumoGuionBroker = "";
            string gConstIdBolsaBroker = "";
            string gConstPlataformaOdcsBroker = "";
            string strPlatform1 = "";
            try
            {
                gConstValor00 = KEY.AppSettings("gConstValor00");
                gConstLineaJanusBroker = KEY.AppSettings("gConstLineaJanusBroker");
                gConstPlataformaJanusBroker = KEY.AppSettings("gConstPlataformaJanusBroker");
                gConstConsumoGuionBroker = KEY.AppSettings("gConstConsumoGuionBroker");
                gConstIdBolsaBroker = KEY.AppSettings("gConstIdBolsaBroker");
                gConstPlataformaOdcsBroker = KEY.AppSettings("gConstPlataformaOdcsBroker");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            POSTPAID_RECHARGE.SaldoBolsasType[] objSaldoBolsasType = new POSTPAID_RECHARGE.SaldoBolsasType[0];
            POSTPAID_RECHARGE.SaldoPaqueteDatosType[] objSaldoPaqueteDatosType = new POSTPAID_RECHARGE.SaldoPaqueteDatosType[0];
            POSTPAID_RECHARGE.listaCamposOpcionalesCampoOpcional[] objListaCamposOpcionales = new POSTPAID_RECHARGE.listaCamposOpcionalesCampoOpcional[0];

            POSTPAID_RECHARGE.audiTypeRequest objAuditRequest = new POSTPAID_RECHARGE.audiTypeRequest()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = objAudit.ApplicationName,
                    usuarioAplicacion = objAudit.UserName
                };

            POSTPAID_RECHARGE.audiTypeResponse objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_RECHARGE.audiTypeResponse>
            (objAudit.Session, objAudit.Transaction, Configuration.WebServiceConfiguration.POSTPAID_RECHARGE, () =>
            {
                return Configuration.WebServiceConfiguration.POSTPAID_RECHARGE.consultarSaldoBolsas(objAuditRequest, objRecharge.MSISDN, objRecharge.ID_CLIENTE, out strPlatform1, out objSaldoBolsasType, out objSaldoPaqueteDatosType, out objListaCamposOpcionales);
            });

            strPlatform = strPlatform1;

            List<Recharge> listRecharge = null;
            strResponseCode = objResponse.errorCode;
            strPostReply = objResponse.errorMsg;
            string dato = string.Empty;
            int numberZero = Convert.ToInt(gConstValor00);

            if (strResponseCode == gConstValor00)
            {
                if (objSaldoBolsasType != null && objSaldoBolsasType.Length > numberZero)
                {
                    listRecharge = new List<Recharge>();
                    for (int i = numberZero; i < objSaldoBolsasType.Length; i++)
                    {
                        objRecharge = new Recharge();

                        if (strPlatform == gConstLineaJanusBroker)
                        {
                            if (objSaldoBolsasType[i].opcional1 == gConstPlataformaJanusBroker)
                            {
                                objRecharge.BOLSA = objSaldoBolsasType[i].bolsa;
                                objRecharge.FECHA_EXPIRACION = objSaldoBolsasType[i].fechaExpiracion;
                                objRecharge.NOMBRE = objRecharge.BOLSA;
                                objRecharge.CONSUMO = objSaldoBolsasType[i].consumo.ToString();
                                objRecharge.UNIDAD = objSaldoBolsasType[i].unidad;

                                if (string.IsNullOrEmpty(objRecharge.CONSUMO))
                                    objRecharge.CONSUMO = gConstConsumoGuionBroker;
                                else
                                    objRecharge.CONSUMO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objRecharge.CONSUMO, "SaldoBolsas-Consumo");

                                objRecharge.SALDO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objSaldoBolsasType[i].saldo, "SaldoBolsas-Saldo");

                                if (objSaldoBolsasType[i].idBolsa == gConstIdBolsaBroker)
                                {
                                    dato = objRecharge.CONSUMO;
                                    objRecharge.CONSUMO = objRecharge.SALDO;
                                    objRecharge.SALDO = dato;
                                }

                                listRecharge.Add(objRecharge);
                            }
                        }
                        else
                        {

                            if (objSaldoBolsasType[i].opcional1 != gConstPlataformaOdcsBroker)
                            {
                                objRecharge.BOLSA = objSaldoBolsasType[i].bolsa;
                                objRecharge.FECHA_EXPIRACION = objSaldoBolsasType[i].fechaExpiracion;
                                objRecharge.NOMBRE = objRecharge.BOLSA;
                                objRecharge.CONSUMO = objSaldoBolsasType[i].consumo.ToString();
                                objRecharge.UNIDAD = objSaldoBolsasType[i].unidad;

                                if (string.IsNullOrEmpty(objRecharge.CONSUMO))
                                    objRecharge.CONSUMO = gConstConsumoGuionBroker;
                                else
                                    objRecharge.CONSUMO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objRecharge.CONSUMO, "SaldoBolsas-Consumo");

                                objRecharge.SALDO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objSaldoBolsasType[i].saldo, "SaldoBolsas-Saldo");

                                if (objSaldoBolsasType[i].idBolsa == gConstIdBolsaBroker)
                                {
                                    dato = objRecharge.CONSUMO;
                                    objRecharge.CONSUMO = objRecharge.SALDO;
                                    objRecharge.SALDO = dato;
                                }

                                listRecharge.Add(objRecharge);
                            }
                        }
                    }
                }

                if (objSaldoPaqueteDatosType != null && objSaldoPaqueteDatosType.Length > numberZero)
                {

                    for (int i = numberZero; i < objSaldoPaqueteDatosType.Length; i++)
                    {
                        objRecharge = new Recharge();

                        if (strPlatform == gConstLineaJanusBroker)
                        {

                            if (objSaldoPaqueteDatosType[i].opcional1 == gConstPlataformaJanusBroker)
                            {
                                objRecharge.BOLSA = objSaldoPaqueteDatosType[i].bolsa;
                                objRecharge.FECHA_EXPIRACION = objSaldoPaqueteDatosType[i].fechaExpiracion;
                                objRecharge.NOMBRE = objRecharge.BOLSA;
                                objRecharge.CONSUMO = objSaldoPaqueteDatosType[i].consumo.ToString();
                                objRecharge.UNIDAD = objSaldoPaqueteDatosType[i].unidad;

                                if (string.IsNullOrEmpty(objRecharge.CONSUMO))
                                    objRecharge.CONSUMO = gConstConsumoGuionBroker;
                                else
                                    objRecharge.CONSUMO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objRecharge.CONSUMO, "SaldoPaqueteDatos-Consumo");

                                objRecharge.SALDO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objSaldoPaqueteDatosType[i].saldo, "SaldoPaqueteDatos-Saldo");

                                if (objSaldoPaqueteDatosType[i].idBolsa == gConstIdBolsaBroker)
                                {
                                    dato = objRecharge.CONSUMO;
                                    objRecharge.CONSUMO = objRecharge.SALDO;
                                    objRecharge.SALDO = dato;
                                }

                                listRecharge.Add(objRecharge);
                            }
                        }
                        else
                        {
                            if (objSaldoPaqueteDatosType[i].opcional1 != gConstPlataformaOdcsBroker)
                            {
                                objRecharge.BOLSA = objSaldoPaqueteDatosType[i].bolsa;
                                objRecharge.FECHA_EXPIRACION = objSaldoPaqueteDatosType[i].fechaExpiracion;
                                objRecharge.NOMBRE = objRecharge.BOLSA;
                                objRecharge.CONSUMO = objSaldoPaqueteDatosType[i].consumo.ToString();
                                objRecharge.UNIDAD = objSaldoPaqueteDatosType[i].unidad;

                                if (string.IsNullOrEmpty(objRecharge.CONSUMO))
                                    objRecharge.CONSUMO = gConstConsumoGuionBroker;
                                else
                                    objRecharge.CONSUMO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objRecharge.CONSUMO, "SaldoBolsas-Consumo");

                                objRecharge.SALDO = CalculateData(objAudit.Session, objAudit.Transaction, objRecharge.UNIDAD, objSaldoPaqueteDatosType[i].saldo, "SaldoBolsas-Saldo");

                                if (objSaldoPaqueteDatosType[i].idBolsa == gConstIdBolsaBroker)
                                {
                                    dato = objRecharge.CONSUMO;
                                    objRecharge.CONSUMO = objRecharge.SALDO;
                                    objRecharge.SALDO = dato;
                                }

                                listRecharge.Add(objRecharge);
                            }
                        }
                    }
                }
            }

            return listRecharge;
        }

        /// <summary>
        /// Método que obtiene una lista con los datos de la recarga.
        /// </summary>
        /// <param name="objRecharge">Objeto Recarga</param>
        /// <param name="objAudit">Objeto Auditoría</param>
        /// <param name="strResponseCode">Código de respuesta</param>
        /// <param name="strPostReply">Envío de respuesta</param>
        /// <param name="strPlatform">Plataforma</param>
        /// <returns>Devuelve listado de objetos Recharge con información de recarga.</returns>
        public static List<Recharge> GetBag(Recharge objRecharge, Claro.Entity.AuditRequest objAudit, out string strResponseCode, out string strPostReply, out string strPlatform)
        {
            string gConstValor00 = "";
            string gConstConsumoGuionBroker = "";
            string strPlatforms = null;
            try
            {
                gConstValor00 = KEY.AppSettings("gConstValor00");
                gConstConsumoGuionBroker = KEY.AppSettings("gConstConsumoGuionBroker");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            POSTPAID_RECHARGE.SaldoBolsasType[] objSaldoBolsasType = new POSTPAID_RECHARGE.SaldoBolsasType[0];
            POSTPAID_RECHARGE.listaCamposOpcionalesCampoOpcional[] objListaCamposOpcionales = new POSTPAID_RECHARGE.listaCamposOpcionalesCampoOpcional[0];
            POSTPAID_RECHARGE.audiTypeResponse objResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_RECHARGE.audiTypeResponse>
            (objAudit.Session, objAudit.Transaction, Configuration.WebServiceConfiguration.POSTPAID_RECHARGE, () =>
            {
                return Configuration.WebServiceConfiguration.POSTPAID_RECHARGE.consultarBolsas(new POSTPAID_RECHARGE.audiTypeRequest()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = objAudit.ApplicationName,
                    usuarioAplicacion = objAudit.UserName
                }, objRecharge.MSISDN, out strPlatforms, out objSaldoBolsasType, out objListaCamposOpcionales);
            });

            strPlatform = strPlatforms;
            List<Recharge> listRecharge = null;
            strResponseCode = objResponse.errorCode;
            strPostReply = objResponse.errorMsg;

            int numberZero = Convert.ToInt(gConstValor00);

            if (strResponseCode == gConstValor00 && objSaldoBolsasType != null && objSaldoBolsasType.Length > numberZero)
            {
                listRecharge = new List<Recharge>();

                for (int i = numberZero; i < objSaldoBolsasType.Length; i++)
                {
                    objRecharge = new Recharge();

                    objRecharge.BOLSA = objSaldoBolsasType[i].bolsa;
                    objRecharge.FECHA_EXPIRACION = objSaldoBolsasType[i].fechaExpiracion;
                    objRecharge.NOMBRE = objRecharge.BOLSA;
                    objRecharge.CONSUMO = objSaldoBolsasType[i].consumo.ToString();

                    if (string.IsNullOrEmpty(objRecharge.CONSUMO))
                        objRecharge.CONSUMO = gConstConsumoGuionBroker;

                    objRecharge.SALDO = objSaldoBolsasType[i].saldo;

                    listRecharge.Add(objRecharge);
                }

            }

            return listRecharge;
        }

        /// <summary>
        /// Método que devuelve una cadena con la información del Consumo/Saldo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strUnity">Unidad</param>
        /// <param name="strDato">Dato</param>
        /// <param name="strDescription">Descripción</param>
        /// <returns>Devuelve cadena con valor del consumo.</returns>
        public static string CalculateData(string strIdSession, string strIdTransaction, string strUnity, string strDato, string strDescription)
        {
            string gConstBytesValorBroker = "";
            string gConstSegundoValorBroker = "";
            string gConstOtroValorBroker = "";

            try
            {
                gConstBytesValorBroker = KEY.AppSettings("gConstBytesValorBroker");
                gConstSegundoValorBroker = KEY.AppSettings("gConstSegundoValorBroker");
                gConstOtroValorBroker = KEY.AppSettings("gConstOtroValorBroker");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            int valueBytes, valueSeconds, valueOthers;
            int operationValue;
            string strValue;

            try
            {
                valueBytes = Convert.ToInt(gConstBytesValorBroker);
                valueSeconds = Convert.ToInt(gConstSegundoValorBroker);
                valueOthers = Convert.ToInt(gConstOtroValorBroker);

                if (!string.IsNullOrEmpty(strDato))
                {
                    if (Convert.ToDouble(strDato) != Convert.ToDouble(Claro.Constants.NumberZeroString))
                    {
                        switch (strUnity.ToUpperInvariant())
                        {
                            case Claro.SIACU.Constants.BytesMajuscule:
                                operationValue = valueBytes;
                                break;
                            case Claro.SIACU.Constants.SecondsMajuscule:
                                operationValue = valueSeconds;
                                break;
                            default:
                                operationValue = valueOthers;
                                break;
                        }

                        strValue = Convert.ToString(Convert.ToDouble(strDato) / operationValue);
                        strDato = String.Format("{0:0.00}", Convert.ToDouble(strValue)).ToString();
                    }
                    else
                        strDato = String.Format("{0:0.00}", Convert.ToDouble(strDato)).ToString();
                }
                else
                    strDato = String.Format("{0:0.00}", Convert.ToDouble(strDato)).ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            return strDato;
        }

        /// <summary>
        /// Método que devuelve un entero con la carga de saldo por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <returns>Devuelve valor de la carga de saldo.</returns>
        public static int LoadBalancePostpaid(string strIdSession, string strTransaction, string strTelephone)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_dn_num", DbType.String,50, ParameterDirection.Input, strTelephone),
                new DbParameter("p_iRetorno", DbType.Int32, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_CARGA_SALDO, parameters);

            return Convert.ToInt(parameters[1].Value.ToString());
        }

        /// <summary>
        /// Método que obtiene una cadena con el saldo por número de teléfono y fecha de activación.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <param name="strActivationDate">Fecha de activación</param>
        /// <returns>Devuelve cadena con el saldo.</returns>
        public static string GetBalancesPostpaid(string strIdSession, string strTransaction, string strTelephone, out string strActivationDate)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_numero", DbType.String,50, ParameterDirection.Input, strTelephone),
                new DbParameter("p_msg", DbType.String,500, ParameterDirection.Output),
                new DbParameter("p_fecha_act", DbType.String,255, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Int32, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_SALDO_IVR, parameters);

            strActivationDate = parameters[2].Value.ToString();

            return parameters[1].Value.ToString();
        }

        /// <summary>
        /// Método que obtiene una cadena con un correlativo de transacción de saldo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve valor correlativo de transacción.</returns>
        public static string GetCorrelativeRiceBalances(string strIdSession, string strTransaction)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_CORR", DbType.Int64, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_CORRELATIVO_TRAN_SALDO, parameters);

            return parameters[0].Value.ToString();
        }

        /// <summary>
        /// Método que obtiene el tiempo consumido por la transacción. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTransactionId">Id de transacción</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <param name="strExternalOrigen">Origen externo</param>
        /// <param name="strCurrentUser">Usuario actual</param>
        /// <param name="strConsumerBagIdIn">Id de bolsa de consumo</param>
        /// <returns>Devuelve el valor del tiempo consumido.</returns>
        public static string GetBalancesRiceServices(string strIdSession, string strTransaction, string strTransactionId, string strTelephone, string strExternalOrigen, string strCurrentUser, string strConsumerBagIdIn)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("i_trid", DbType.String,18, ParameterDirection.Input, strTransactionId),
                new DbParameter("i_phone_number", DbType.String,32, ParameterDirection.Input, strTelephone),
                new DbParameter("o_amount", DbType.String,2000, ParameterDirection.Output),
                new DbParameter("i_external_origen", DbType.String,20, ParameterDirection.Input, strExternalOrigen),
                new DbParameter("i_user_origen", DbType.String,20, ParameterDirection.Input, strCurrentUser),
                new DbParameter("o_status", DbType.Int32, ParameterDirection.Output),
                new DbParameter("i_consume_bag_id_in", DbType.Int32, ParameterDirection.Input, strConsumerBagIdIn),
                new DbParameter("o_exit_code", DbType.Int32, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_RICE, DbCommandConfiguration.SIACU_PR_CONSUMED_TIME_API, parameters);

            return parameters[2].Value.ToString();
        }

        /// <summary>
        /// Método que obtiene los datos del servicio por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strTransactionId">Id de transacción</param>
        /// <param name="strApplicationId">Id de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        public static Entity.Dashboard.Postpaid.Service GetDataService(string strIdSession, string strPhoneNumber, string strTransactionId, string strApplicationId, string strApplicationName)
        {
            Entity.Dashboard.Postpaid.Service oService = new Entity.Dashboard.Postpaid.Service();
            List<Trio> oListTrio = null;
            List<Account> oListAccount;

            string strFlagNuevoDatosPrepago = "";
            string strCounterChangeTariffForFree = "";

            try
            {
                strFlagNuevoDatosPrepago = KEY.AppSettings("FlagNuevoDatosPrepago");
                strCounterChangeTariffForFree = KEY.AppSettings("strCounterChangeTariffForFree");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionId, ex.Message);
            }

            int i = Claro.Constants.NumberOne;
            if (string.Equals(strFlagNuevoDatosPrepago, Claro.Constants.NumberOneString, StringComparison.OrdinalIgnoreCase))
            {
                PREPAID_SERVICE.INDatosPrepagoResponse oPrepaidRespons = Claro.Web.Logging.ExecuteMethod<PREPAID_SERVICE.INDatosPrepagoResponse>(strIdSession, strTransactionId, Configuration.WebServiceConfiguration.PREPAID_SERVICE, () =>
                {
                    return Configuration.WebServiceConfiguration.PREPAID_SERVICE.leerDatosPrepago(new PREPAID_SERVICE.INDatosPrepagoRequest()
                    {
                        telefono = strPhoneNumber
                    });
                });


                PREPAID_SERVICE.DatosPrepago oPrepaidResult;

                if (string.Equals(oPrepaidRespons.resultado.Trim(), Claro.Constants.NumberZeroString, StringComparison.OrdinalIgnoreCase))
                {
                    oPrepaidResult = oPrepaidRespons.datosPrePago;
                    oService.NRO_CELULAR = strPhoneNumber;
                    oService.SALDO_PRINCIPAL = Convert.ToString(oPrepaidResult.onPeakAccountIDBalance);
                    oService.FECHA_EXPIRACION_SALDO = Convert.ToDate(oPrepaidResult.expiryDate).ToString("dd/MM/yyyy HH:mm:ss");
                    oService.ESTADO_LINEA = Convert.ToString(oPrepaidResult.subscriberLifeCycleStatus);
                    oService.CAMBIOS_TRIOS_GRATIS = Convert.ToString(oPrepaidResult.voucherRchFraudCounter);
                    oService.CAMBIOS_TARIFA_GRATIS = KEY.AppSettings("strCounterChangeTariffForFree");
                    oService.PLAN_TARIFARIO = Convert.ToString(oPrepaidResult.tariffModelNumber);
                    oService.PROVIDER_ID = Convert.ToString(oPrepaidResult.providerID);
                    oService.FEC_ACTIVACION = Convert.ToString(oPrepaidResult.firstCallDate);
                    oService.FECHA_EXPIRACION_LINEA = Convert.ToString(oPrepaidResult.deletionDate);
                    oService.FECHA_DOL = "";
                    oService.SUBSCRIBIR_ESTADO = Convert.ToString(oPrepaidResult.subscriberStatus);
                    oService.CNT_NUMERO = Convert.ToString(oPrepaidResult.cNTNumber);
                    oService.CNT_POSIBLE = Convert.ToString(oPrepaidResult.isCNTPossible);
                    oService.NROIMSI = Convert.ToString(oPrepaidResult.imsi);
                    oService.ESTADO_IMSI = Convert.ToString(oPrepaidResult.isLocked);


                    oListAccount = new List<Account>();

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceSms,
                        SALDO = Convert.ToString(oPrepaidResult.sMSPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.sMSPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.Voice1PromoAccount,
                        SALDO = Convert.ToString(oPrepaidResult.voice1PromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.voice1PromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.Voice2PromoAccount,
                        SALDO = Convert.ToString(oPrepaidResult.voice2PromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.voice2PromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceGprs,
                        SALDO = Convert.ToString(oPrepaidResult.gPRSPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.gPRSPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceMms,
                        SALDO = Convert.ToString(oPrepaidResult.mMSPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.mMSPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalancePromo,
                        SALDO = Convert.ToString(oPrepaidResult.bonusPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.bonusPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceSmsLoyalty,
                        SALDO = Convert.ToString(oPrepaidResult.sMSLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.sMSLoyaltyAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalancePromo1,
                        SALDO = Convert.ToString(oPrepaidResult.gPRSLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.gPRSLoyaltyAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalancePromo2,
                        SALDO = Convert.ToString(oPrepaidResult.mMSLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResult.mMSLoyaltyAccountIDExpiryDate)
                    });

                    oService.LISTA_ACCOUNT = oListAccount;

                    oListTrio = new List<Trio>();

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber0.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber0)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber1.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber1)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber2.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber2)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber3.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber3)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber4.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber4)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber5.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber5)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber6.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber6)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber7.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber7)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber8.Trim()))
                    {
                        oListTrio.Add(new Trio()
                        {
                            CODIGO = "Nro Triado " + i.ToString(),
                            DESCRIPCION = Convert.ToString(oPrepaidResult.fnFNumber8)
                        });
                        i++;
                    }

                    oService.TIPO_TRIACION = Convert.ToString(oPrepaidResult.activeFnFOption);
                    oService.NUMERO_FAMILIA_AMIGOS = oListTrio.Count.ToString();
                    oService.LISTA_TRIO = oListTrio;
                }
                else
                    oService.CODIGO_RESPUESTA = oPrepaidRespons.resultado.Trim();

            }
            else
            {
                PREPAID_SERVICE.INDatosPrepagoResponse oPrepaidResponsV2 =
                 Claro.Web.Logging.ExecuteMethod<PREPAID_SERVICE.INDatosPrepagoResponse>
                (strIdSession, strTransactionId, Configuration.WebServiceConfiguration.PREPAID_SERVICE, () =>
                {
                    return Configuration.WebServiceConfiguration.PREPAID_SERVICE.leerDatosPrepago(new PREPAID_SERVICE.INDatosPrepagoRequest()
                    {
                        telefono = strPhoneNumber
                    });
                });


                PREPAID_SERVICE.DatosPrepago oPrepaidResultV2;
                if (string.Equals(oPrepaidResponsV2.resultado.Trim(), "1", StringComparison.OrdinalIgnoreCase))
                {
                    oService = new Entity.Dashboard.Postpaid.Service();
                    oPrepaidResultV2 = oPrepaidResponsV2.datosPrePago;
                    oService.NRO_CELULAR = strPhoneNumber;
                    oService.SALDO_PRINCIPAL = Convert.ToString(oPrepaidResultV2.onPeakAccountIDBalance);
                    oService.FECHA_EXPIRACION_SALDO = Convert.ToDate(oPrepaidResultV2.expiryDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.ESTADO_LINEA = Convert.ToString(oPrepaidResultV2.subscriberLifeCycleStatus);
                    oService.CAMBIOS_TRIOS_GRATIS = Convert.ToString(oPrepaidResultV2.voucherRchFraudCounter);
                    oService.CAMBIOS_TARIFA_GRATIS = strCounterChangeTariffForFree;
                    oService.PLAN_TARIFARIO = Convert.ToString(oPrepaidResultV2.tariffModelNumber);
                    oService.PROVIDER_ID = Convert.ToString(oPrepaidResultV2.providerID);
                    oService.FEC_ACTIVACION = Convert.ToDate(oPrepaidResultV2.firstCallDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.FECHA_EXPIRACION_LINEA = Convert.ToDate(oPrepaidResultV2.deletionDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.FECHA_DOL = " ";
                    oService.SUBSCRIBIR_ESTADO = Convert.ToString(oPrepaidResultV2.subscriberStatus);
                    oService.CNT_NUMERO = Convert.ToString(oPrepaidResultV2.cNTNumber);
                    oService.CNT_POSIBLE = Convert.ToString(oPrepaidResultV2.isCNTPossible);
                    oService.NROIMSI = Convert.ToString(oPrepaidResultV2.imsi);
                    oService.ESTADO_IMSI = Convert.ToString(oPrepaidResultV2.isLocked);

                    oListAccount = new List<Account>();

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceSms,
                        SALDO = Convert.ToString(oPrepaidResultV2.sMSPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.sMSPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.Voice1PromoAccount,
                        SALDO = Convert.ToString(oPrepaidResultV2.voice1PromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.voice1PromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.Voice2PromoAccount,
                        SALDO = Convert.ToString(oPrepaidResultV2.voice2PromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.voice2PromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceGprs,
                        SALDO = Convert.ToString(oPrepaidResultV2.gPRSPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.gPRSPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceMms,
                        SALDO = Convert.ToString(oPrepaidResultV2.mMSPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.mMSPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalancePromo,
                        SALDO = Convert.ToString(oPrepaidResultV2.bonusPromoAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.bonusPromoAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceSmsLoyalty,
                        SALDO = Convert.ToString(oPrepaidResultV2.sMSLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.sMSLoyaltyAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalanceVoiceLoyalty,
                        SALDO = Convert.ToString(oPrepaidResultV2.voiceLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.voiceLoyaltyAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalancePromo1,
                        SALDO = Convert.ToString(oPrepaidResultV2.gPRSLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.gPRSLoyaltyAccountIDExpiryDate)
                    });

                    oListAccount.Add(new Account()
                    {
                        NOMBRE = Claro.SIACU.Constants.BalancePromo2,
                        SALDO = Convert.ToString(oPrepaidResultV2.mMSLoyaltyAccountIDBalance),
                        FECHA_EXPIRACION = Convert.ToString(oPrepaidResultV2.mMSLoyaltyAccountIDExpiryDate)
                    });

                    oService.LISTA_ACCOUNT = oListAccount;

                    oService.TIPO_TRIACION = Convert.ToString(oPrepaidResultV2.activeFnFOption);
                    oService.NUMERO_FAMILIA_AMIGOS = oListTrio.Count.ToString();
                    oService.LISTA_TRIO = oListTrio;
                }
                else
                    oService.CODIGO_RESPUESTA = oPrepaidResponsV2.resultado.Trim();

            }

            return oService;
        }

        /// <summary>
        /// Método que obtiene una lista con los datos del saldo por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTelephone">Teléfono</param>
        /// <param name="strTransactionId">Id de transacción</param>
        /// <param name="intRelationShipType">Tipo de relación</param>
        /// <param name="strMessage">Mensaje</param>
        /// <returns>Devuelve listado de objetos Balance con información de balance.</returns>
        public static List<Balance> GetBalance(string strIdSession, string strTelephone, string strTransactionId, int intRelationShipType, out string strMessage)
        {
            double consumption = Convert.ToDouble(Claro.Constants.NumberZeroString);
            double balance = Convert.ToDouble(Claro.Constants.NumberZeroString);
            string consumptions = string.Empty;
            List<Balance> listBalance = null;

            string strExternalOrigen = "";

            try
            {
                strExternalOrigen = KEY.AppSettings("strExternalOrigen");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionId, ex.Message);
            }

            POSTPAID_FINANCE.getBalanceEventDataType objGetBalanceEventDataType = new POSTPAID_FINANCE.getBalanceEventDataType()
                    {
                        entityId = new POSTPAID_FINANCE.getBalanceEventDataTypeEntityId()
                        {
                            value = Claro.Constants.FormatDoubleZero + strTelephone
                        },
                        entityType = new POSTPAID_FINANCE.getBalanceEventDataTypeEntityType()
                        {
                            value = POSTPAID_FINANCE.getBalanceEventDataTypeEntityTypeValue.C
                        },
                        referenceNumber = new POSTPAID_FINANCE.getBalanceEventDataTypeReferenceNumber()
                        {
                            value = (strTransactionId == string.Empty) ? Claro.Constants.NumberOneString : strTransactionId
                        },
                        eventSource = new POSTPAID_FINANCE.getBalanceEventDataTypeEventSource()
                        {
                            value = strExternalOrigen
                        },
                        relationShipType = new POSTPAID_FINANCE.getBalanceEventDataTypeRelationShipType()
                        {
                            value = (ushort)intRelationShipType
                        },
                        spendGroupDetailsFlag = new POSTPAID_FINANCE.getBalanceEventDataTypeSpendGroupDetailsFlag()
                        {
                            value = 1
                        }
                    };

            POSTPAID_FINANCE.getBalance objGetBalance = new POSTPAID_FINANCE.getBalance();
            Security.Registry registry = new Security.Registry();

            objGetBalance.loginName = registry.RegistryReadDecryptValue(Claro.SIACU.Constants.KEY_SIACU_JANUS, "User");
            objGetBalance.proxiedLoginName = registry.RegistryReadDecryptValue(Claro.SIACU.Constants.KEY_SIACU_JANUS, "User");
            objGetBalance.loginPass = registry.RegistryReadDecryptValue(Claro.SIACU.Constants.KEY_SIACU_JANUS, "Password");
            objGetBalance.mvnoId = (ulong)Convert.ToInt(Claro.Constants.NumberZeroString);
            objGetBalance.@event = objGetBalanceEventDataType;

            POSTPAID_FINANCE.balanceResult objResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_FINANCE.balanceResult>
            (strIdSession, strTransactionId, Configuration.ServiceConfiguration.POSTPAID_FINANCE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_FINANCE.getBalance(objGetBalance);
            });

            strMessage = objResponse.invocationStatus.errorMessage;


            if (strMessage == Claro.SIACU.Constants.Success)
            {
                POSTPAID_FINANCE.getBalanceEventOutputTypeEventPayerInfo walletDetails = new POSTPAID_FINANCE.getBalanceEventOutputTypeEventPayerInfo();
                walletDetails.walletDetails = objResponse.eventData.@event.payerInfo.walletDetails;

                if (walletDetails.walletDetails.Length > 0)
                {

                    listBalance = new List<Balance>();
                    Balance objBalance = null;

                    for (int i = 0; i < walletDetails.walletDetails.Length; i++)
                    {
                        objBalance = new Balance();
                        objBalance.EXPIRY_DATE = Claro.Constants.NumberZeroString;

                        if (intRelationShipType == Convert.ToInt(Claro.Constants.NumberThreeString))
                        {
                            objBalance.EXPIRY_DATE = Claro.Constants.NumberThreeString;

                            if (walletDetails.walletDetails[i].spendGroup != null)
                            {
                                walletDetails.walletDetails[i].balanceUnits = walletDetails.walletDetails[i].spendGroup[0].balanceUnits;
                                walletDetails.walletDetails[i].minimumBalance = walletDetails.walletDetails[i].spendGroup[0].minimumBalance;
                            }
                            else
                                continue;
                        }

                        #region RelationShiperType
                        consumption = Math.Abs(walletDetails.walletDetails[i].balanceUnits) - Math.Abs(walletDetails.walletDetails[i].minimumBalance);
                        consumption = Math.Abs(consumption);
                        if (walletDetails.walletDetails[i].minimumBalance == Convert.ToDouble(Claro.Constants.NumberZeroString))
                        {
                            objBalance.CONSUMO = Claro.SIACU.Constants.DoubleScript;
                            if (walletDetails.walletDetails[i].balanceUnits != Convert.ToInt(Claro.Constants.NumberZeroString))
                            {
                                if ((walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Bytes) || (walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Seconds))
                                {
                                    if (walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Bytes)
                                    {
                                        balance = Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits)) / Claro.Constants.NumberTenMillionTwoHundredFortyThousand;
                                        objBalance.BALANCE = balance.ToString(Claro.Constants.FormatDecimal);
                                    }
                                    if (walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Seconds)
                                    {
                                        balance = Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits)) / Claro.Constants.NumberSixHundredThousand;
                                        objBalance.BALANCE = balance.ToString(Claro.Constants.FormatDecimal);
                                    }
                                }
                                else
                                {
                                    objBalance.BALANCE = (Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits)) / Claro.Constants.NumberTenThousand).ToString(Claro.Constants.FormatDecimal);
                                }
                            }
                            else
                            {
                                objBalance.BALANCE = (Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits) / Claro.Constants.NumberTenThousand)).ToString(Claro.Constants.FormatDecimal);
                            }
                        }
                        else
                        {
                            if ((walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Bytes) || (walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Seconds))
                            {
                                if (walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Bytes)
                                {
                                    consumption = consumption / Claro.Constants.NumberTenMillionTwoHundredFortyThousand;
                                    balance = Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits)) / Claro.Constants.NumberTenMillionTwoHundredFortyThousand;
                                    objBalance.CONSUMO = consumption.ToString(Claro.Constants.FormatDecimal);
                                    objBalance.BALANCE = balance.ToString(Claro.Constants.FormatDecimal);
                                }
                                if (walletDetails.walletDetails[i].unitDescription == Claro.SIACU.Constants.Seconds)
                                {
                                    consumption = consumption / Claro.Constants.NumberSixHundredThousand;
                                    balance = Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits)) / Claro.Constants.NumberSixHundredThousand;
                                    objBalance.CONSUMO = consumption.ToString(Claro.Constants.FormatDecimal);
                                    objBalance.BALANCE = balance.ToString(Claro.Constants.FormatDecimal);
                                }
                            }
                            else
                            {
                                objBalance.CONSUMO = (consumption / Claro.Constants.NumberTenThousand).ToString(Claro.Constants.FormatDecimal);
                                objBalance.BALANCE = (Convert.ToDouble(Math.Abs(walletDetails.walletDetails[i].balanceUnits)) / Claro.Constants.NumberTenThousand).ToString(Claro.Constants.FormatDecimal);
                            }
                        }
                        if (intRelationShipType == Convert.ToInt(Claro.Constants.NumberZeroString))
                        {
                            if (walletDetails.walletDetails[i].walletId == Convert.ToDouble(Claro.Constants.NumberSixString))
                            {
                                consumptions = objBalance.CONSUMO;
                                objBalance.CONSUMO = objBalance.BALANCE;
                                objBalance.BALANCE = consumptions;
                            }
                        }
                        else if (intRelationShipType == Convert.ToInt(Claro.Constants.NumberThreeString))
                        {
                            consumptions = objBalance.CONSUMO;
                            objBalance.CONSUMO = objBalance.BALANCE;
                            objBalance.BALANCE = consumptions;
                        }
                        #endregion

                        objBalance.CREDIT_DESCRIPTION = walletDetails.walletDetails[i].creditDescription;
                        objBalance.TARIF_ID = walletDetails.walletDetails[i].tariffId;
                        objBalance.UNIT_DESCRIPTION = walletDetails.walletDetails[i].unitDescription;
                        objBalance.WALLET_DESCRIPTION = walletDetails.walletDetails[i].walletDescription;
                        objBalance.WALLET_ID = walletDetails.walletDetails[i].walletId;

                        listBalance.Add(objBalance);
                    }
                }
            }

            return listBalance;
        }

        /// <summary>
        /// Método que obtiene los datos de la deuda por número y tipo de documento de identidad.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransacionID">Id de transacción</param>
        /// <param name="strTxId">Id de tx</param>
        /// <param name="strIdAplication">Id de aplicación</param>
        /// <param name="strUserAplication">Usuario de aplicación</param>
        /// <param name="strDocumentIdentity">Documento de identidad</param>
        /// <param name="strNumberDocument">Número de documento</param>
        /// <returns>Devuelve listado de objetos DueDocumentNumber con información de deuda.</returns>
        public static Entity.Dashboard.Postpaid.DueDocumentNumber DueNumberDocumentOAC(string strIdSession, string strTransaction, string strUserAplication, string strDocumentIdentity, string strNumberDocument)
        {
            string strCodFlagFijo = "";
            string strCodFlagMovil = "";

            try
            {
                strCodFlagFijo = KEY.AppSettings("strCodFlagFijo");
                strCodFlagMovil = KEY.AppSettings("strCodFlagMovil");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            DueDocumentNumber objDueDocumentNumber = null;
            POSTPAID_ACCOUNT.ClienteType objClienteType = null;
            POSTPAID_ACCOUNT.CuentaType objDetalleCuentaType;

            Claro.Web.Logging.ExecuteMethod<POSTPAID_ACCOUNT.AuditType>(strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_ACCOUNT, () => { return Configuration.ServiceConfiguration.POSTPAID_ACCOUNT.consultaDeuda(null, null, strUserAplication, strDocumentIdentity, strNumberDocument, out objClienteType); });
            if (objClienteType.Count == 1)
            {
                objDueDocumentNumber = new DueDocumentNumber();
                objDueDocumentNumber.ObjDueDocumentNumberAccount = new DueDocumentNumberAccount()
                {
                    NombreCliente = objClienteType[0].xNombreCliente,
                    DeudaMovil = objClienteType[0].xDeudaMovil == null ? 0 : objClienteType[0].xDeudaMovil,
                    DeudaFija = objClienteType[0].xDeudaFija == null ? 0 : objClienteType[0].xDeudaFija,
                    DeudaVencidaMovil = objClienteType[0].xDeudaVencidaMovil == null ? 0 : objClienteType[0].xDeudaVencidaMovil,
                    DeudaVencidaFija = objClienteType[0].xDeudaVencidaFija == null ? 0 : objClienteType[0].xDeudaVencidaFija,
                    DeudaCastigadaMovil = objClienteType[0].xDeudaCastigadaMovil == null ? 0 : objClienteType[0].xDeudaCastigadaMovil,
                    DeudaCastigadaFija = objClienteType[0].xDeudaCastigadaFija == null ? 0 : objClienteType[0].xDeudaCastigadaFija,
                    DNIAsociado = objClienteType[0].xDniAsociado,
                    AntiguedadDeuda = objClienteType[0].xAntiguedadDeuda == null ? 0 : objClienteType[0].xAntiguedadDeuda,
                    TotalServicios = objClienteType[0].xTotalServicios == null ? 0 : objClienteType[0].xTotalServicios
                };

                objDetalleCuentaType = objClienteType[0].xCuentas;
                DueDocumentNumberAccount objDueDocumentNumberAccount;

                if (objDetalleCuentaType.Count > 0)
                {

                    objDueDocumentNumber.ListDueDocumentNumberAccountFijo = new List<DueDocumentNumberAccount>();
                    objDueDocumentNumber.ListDueDocumentNumberAccountMovil = new List<DueDocumentNumberAccount>();
                    foreach (var item in objDetalleCuentaType)
                    {
                        objDueDocumentNumberAccount = new DueDocumentNumberAccount();
                        objDueDocumentNumberAccount.TipoServicio = item.xTipoServicio;
                        objDueDocumentNumberAccount.CodCuenta = item.xCodCuenta;
                        objDueDocumentNumberAccount.EstadoCuenta = item.xEstadoCuenta;
                        objDueDocumentNumberAccount.PromedioFact = item.xPromedioFact == null ? 0 : item.xPromedioFact;
                        objDueDocumentNumberAccount.DeudaCorriente = item.xDeudaCorriente == null ? 0 : item.xDeudaCorriente;
                        objDueDocumentNumberAccount.DeudaVencida = item.xDeudaVencida == null ? 0 : item.xDeudaVencida;
                        objDueDocumentNumberAccount.DeudaCastigada = item.xDeudaCastigada == null ? 0 : item.xDeudaCastigada;
                        objDueDocumentNumberAccount.CantServicios = item.xCantServicios == null ? 0 : item.xCantServicios;
                        objDueDocumentNumberAccount.IndCentralRiesgo = item.xIndCentralRiesgo;
                        try
                        {
                            if (item.xTipoServicio == strCodFlagFijo)
                            {
                                objDueDocumentNumber.ListDueDocumentNumberAccountFijo.Add(objDueDocumentNumberAccount);
                            }

                            if (item.xTipoServicio == strCodFlagMovil)
                            {
                                objDueDocumentNumber.ListDueDocumentNumberAccountMovil.Add(objDueDocumentNumberAccount);
                            }

                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

                        }
                    }
                }
            }
            return objDueDocumentNumber;
        }

        /// <summary>
        /// Método que obtiene un lista de Estado de Cuenta por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos statusAccount con información de estado de cuenta.</returns>
        public static List<Entity.Dashboard.Postpaid.StatusAccount> StatusAccount(string strIdSession, string strTransaction, Int64 strCustomerId)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_CustomerID", DbType.Int64, ParameterDirection.Input, strCustomerId),
                new DbParameter("p_Cursor", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<StatusAccount>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_ESTADO_CUENTA, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el histórico Estado Linea por id de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intContratoID">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de servicios.</returns>
        public static List<Entity.Dashboard.Postpaid.Service> LineHistory(string strIdSession, string strTransaction, int intContratoID)
        {
            List<Entity.Dashboard.Postpaid.Service> listService = new List<Entity.Dashboard.Postpaid.Service>();
            Entity.Dashboard.Postpaid.Service objLine;
            POSTPAID_CONSULTCLIENT.HistEstadosLinea[] objHistEstadosLinea =
             Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.HistEstadosLinea[]>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.historicoEstadoLinea(intContratoID); });


            if (objHistEstadosLinea.Length >= 1)
            {
                foreach (var item in objHistEstadosLinea)
                {
                    objLine = new Entity.Dashboard.Postpaid.Service();
                    objLine.ESTADO_LINEA = item.estado;
                    objLine.MOTIVO = item.motivo;
                    objLine.INTRODUCIDO_EL = item.introducido_el;
                    objLine.INTRODUCIDO_POR = item.introducido_por;
                    listService.Add(objLine);
                }
            }

            return listService;
        }



        /// <remarks>LineHistoryTobe</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/12/2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>11/12/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Metodo que devuleve el historico de Estado</Mot></item></list>
        public static List<Entity.Dashboard.Postpaid.Service> LineHistoryTobe(AuditRequest audit, string strTransaction, int intContratoID, string flagConvivencia, string coIdPub)
        {
            List<Entity.Dashboard.Postpaid.Service> listService = new List<Entity.Dashboard.Postpaid.Service>();
            Entity.Dashboard.Postpaid.Service objHistoryLine = null;
            Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Response.response response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Request.request()
                {
                    consultarHistoricoLineaRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Request.consultarHistoricoLineaRequest()
                    {
                        coid = coIdPub,
                        listaOpcional = new Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Common.listaOpcional()
                        {
                            clave = Claro.ConfigurationManager.AppSettings("strFlagConvivencia"),
                            valor = flagConvivencia
                        },
                    }
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataLineHistory.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_HISTORICO_ESTADO_LINEA_TOBE, audit, request, false);


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (response != null &&
               response.consultarHistoricoLineaResponse != null &&
               response.consultarHistoricoLineaResponse.responseAudit != null &&
               response.consultarHistoricoLineaResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
               response.consultarHistoricoLineaResponse.responseData != null)

                foreach (var item in response.consultarHistoricoLineaResponse.responseData.listaHistoricoMotivo)
                {
                    objHistoryLine = new Entity.Dashboard.Postpaid.Service();
                    objHistoryLine.FECHA_VALIDO_DESDE = Convert.ToDate(item.fechaValidoDesde);
                    objHistoryLine.INTRODUCIDO_EL = Convert.ToDate(item.fechaRegistro);
                    objHistoryLine.ESTADO_LINEA = item.estado;
                    objHistoryLine.MOTIVO = item.motivo;
                    objHistoryLine.INTRODUCIDO_POR = item.usuarioRegistro;
                    listService.Add(objHistoryLine);
                }

            return listService;

        }

        /// <summary>
        /// Método que retorna una cadena con el numero EAI por numero de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve número EAI.</returns>
        public static string ObtenerNumeroEAI(string strIdSession, string strTransaction, string strPhoneNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input, strPhoneNumber),
                new DbParameter("P_NUMERO", DbType.String, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_SP_OBTENER_NUMERO, parameters);

            return Convert.ToString(parameters[1]);
        }

        /// <summary>
        /// Método que retorna una cadena con el valor EAI por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve valor EAI.</returns>
        public static string ObtenerValorEAI(string strIdSession, string strTransaction, string strPhoneNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input, strPhoneNumber),
                new DbParameter("P_NUMERO", DbType.String, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_SP_OBTENER_VALOR, parameters);

            return Convert.ToString(parameters[1]);
        }

        /// <summary>
        /// Método que obtiene una cadena con el numero GWP por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve número GWP.</returns>
        public static string ObtenerNumeroGWP(string strIdSession, string strTransaction, string strPhoneNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_strPhoneNumber", DbType.String, ParameterDirection.Input, strPhoneNumber),
                new DbParameter("P_NUMERO", DbType.String, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_GWP, DbCommandConfiguration.SIACU_SP_OBTENER_NUMERO_PORT, parameters);


            return Convert.ToString(parameters[1]);
        }

        /// <summary>
        /// Método que obtiene una cadena con el valor GWP por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve valor GWP.</returns>
        public static string ObtenerValorGWP(string strIdSession, string strTransaction, string strPhoneNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_strPhoneNumber", DbType.String, ParameterDirection.Input, strPhoneNumber),
                new DbParameter("P_NUMERO", DbType.String, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_GWP, DbCommandConfiguration.SIACU_SP_OBTENER_VALOR_PORT, parameters);

            return Convert.ToString(parameters[1]);
        }

        /// <summary>
        /// Método que si existe o no acuerdo postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strType">Tipo</param>
        /// <param name="strValue">Valor</param>
        /// <returns>Devuelve valor booleano</returns>
        public static bool GetExistsAgreementOld(string strIdSession, string strTransaction, string strType, string strValue)
        {
            bool Result;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPO", DbType.String, ParameterDirection.Input, strType),
                new DbParameter("P_VALOR", DbType.String, ParameterDirection.Input, strValue),
                new DbParameter("PO_EXISTE", DbType.String, ParameterDirection.Output)
            };

            Result = DbFactory.ExecuteReader<bool>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA, DbCommandConfiguration.SIACU_SSSIGA_EXISTE_ACUERDO_SIACPO, parameters);

            return Result;

        }

        /// <summary>
        /// Método que si existe o no acuerdo postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strType">Tipo</param>
        /// <param name="strValue">Valor</param>
        /// <returns>Devuelve valor booleano.</returns>
        public static bool GetExistAgreementNew(string strIdSession, string strTransaction, string strType, string strValue)
        {
            bool result;

            DbParameter[] parameters = new DbParameter[]
            {
                new DbParameter("P_TIPO", DbType.String, ParameterDirection.Input, strType),
                new DbParameter("P_VALOR", DbType.String, ParameterDirection.Input, strValue),
                new DbParameter("PO_EXISTE", DbType.Double, ParameterDirection.Output)
            };


            result = DbFactory.ExecuteReader<bool>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA_NEW, DbCommandConfiguration.SIACU_SP_EXISTE_ACUERDO_SIACPO, parameters);

            return result;
        }

        /// <summary>
        /// Método que retorna una lista con la consulta de Productos Cliente IMEI
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strIMEI">IMEI</param>
        /// <param name="strStarDate">Fecha inicial</param>
        /// <param name="strEndDate">Fecha final</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve listado de objetos LoanRental con información de alquiler de préstamo de equipos.</returns>
        public static List<Entity.Dashboard.Postpaid.LoanRental> GetLoanRentalEquipmentListIMEI(string strIdSession, string strTransaction, string strIMEI, string strStarDate, string strEndDate, string strDocumentNumber)
        {
            List<Entity.Dashboard.Postpaid.LoanRental> listLoanRental = new List<LoanRental>();
            POSTPAID_SAP.ZEIA_RFC_PROD_CLTE_IMEI_OCSResponse objconsultaProductosClienteResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_SAP.ZEIA_RFC_PROD_CLTE_IMEI_OCSResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SAP, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_SAP.consultaProductosClienteIMEI(strStarDate, strEndDate, strDocumentNumber, strIMEI); });

            POSTPAID_SAP.ZENTREGAS_IMEI[] objEntrega = objconsultaProductosClienteResponse.TI_ENTREGAS;

            if (objEntrega != null)
            {
                foreach (var item in objEntrega)
                {
                    listLoanRental.Add(new LoanRental()
                    {
                        RAZON_SOCIAL = item.RSOC,
                        RUC = item.RUC,
                        DIRECCION = item.DIREC,
                        MODELO = item.MAKTG,
                        IMEI = item.SERNR,
                        MOTIVO_SAP = item.MTPED,
                        MODALIDAD_SAP = item.CLPED,
                        FECHAS = item.LFDAT,
                        NUMERO_CLARIFY = item.ZZCLARIF,
                        NUMERO_PEDIDO = item.VBELN,
                        DENOMINACION = item.OTRO_MTPED,
                        VALOR_NETO = item.NETPR,
                    });
                }

            }

            return listLoanRental;
        }

        /// <summary>
        /// Método que obtiene una lista de Préstamo Alquiler de Equipo por número de documento del cliente y rango de fechas.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strStarDate">Fecha inicial</param>
        /// <param name="strEndDate">Fecha final</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve listado de objetos LoanRental con información de alquiler de préstamo de equipos.</returns>
        public static List<Entity.Dashboard.Postpaid.LoanRental> GetLoanRentalEquipmentListWS(string strIdSession, string strTransaction, string strStarDate, string strEndDate, string strDocumentNumber)
        {
            List<Entity.Dashboard.Postpaid.LoanRental> listLoanRental = new List<LoanRental>();
            POSTPAID_SAP.ZEIA_RFC_PRODUCTOS_CLIENTE_OCSResponse objconsultaProductosClienteResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_SAP.ZEIA_RFC_PRODUCTOS_CLIENTE_OCSResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SAP, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_SAP.consultaProductosCliente(strStarDate, strEndDate, strDocumentNumber); });

            POSTPAID_SAP.ZENTREGAS_OCS[] objEntrega = objconsultaProductosClienteResponse.TI_ENTREGAS;

            if (objEntrega != null)
            {
                foreach (var item in objEntrega)
                {
                    listLoanRental.Add(new LoanRental()
                    {
                        RAZON_SOCIAL = item.RSOC,
                        RUC = item.RUC,
                        DIRECCION = item.DIREC,
                        MODELO = item.MAKTG,
                        IMEI = item.SERNR,
                        MOTIVO_SAP = item.MTPED,
                        MODALIDAD_SAP = item.CLPED,
                        FECHAS = item.LFDAT,
                        NUMERO_CLARIFY = item.ZZCLARIF,
                        NUMERO_PEDIDO = item.VBELN,
                        DENOMINACION = item.OTRO_MTPED,
                        VALOR_NETO = item.NETPR,
                    });
                }

            }
            return listLoanRental;

        }

        /// <summary>
        /// Método que obtiene una lista con la consulta de Productos Cliente ICCID.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strSIM">SIM</param>
        /// <param name="strStarDate">Fecha inicial</param>
        /// <param name="strEndDate">Fecha final</param>
        /// <param name="strDocumentNumber">Número de documento</param>
        /// <returns>Devuelve listado de objetos LoanRental con información de alquiler de préstamo de equipos.</returns>
        public static List<Entity.Dashboard.Postpaid.LoanRental> GetLoanRentalEquipmentListSIM(string strIdSession, string strTransaction, string strSIM, string strStarDate, string strEndDate, string strDocumentNumber)
        {
            List<Entity.Dashboard.Postpaid.LoanRental> listLoanRental = new List<LoanRental>();
            POSTPAID_SAP.ZEIA_RFC_PROD_CLTE_ICCID_OCSResponse objconsultaProductosClienteResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_SAP.ZEIA_RFC_PROD_CLTE_ICCID_OCSResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SAP, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_SAP.consultaProductosClienteICCID(strStarDate, strEndDate, strDocumentNumber, strSIM); });

            POSTPAID_SAP.ZENTREGAS_ICCID[] objEntrega = objconsultaProductosClienteResponse.TI_ENTREGAS;

            if (objEntrega != null)
            {

                foreach (var item in objEntrega)
                {
                    listLoanRental.Add(new LoanRental()
                    {
                        RAZON_SOCIAL = item.RSOC,
                        RUC = item.RUC,
                        DIRECCION = item.DIREC,
                        MODELO = item.MAKTG,
                        IMEI = item.SERNR,
                        MOTIVO_SAP = item.MTPED,
                        MODALIDAD_SAP = item.CLPED,
                        FECHAS = item.LFDAT,
                        NUMERO_CLARIFY = item.ZZCLARIF,
                        NUMERO_PEDIDO = item.VBELN,
                        DENOMINACION = item.OTRO_MTPED,
                        VALOR_NETO = item.NETPR,
                    });
                }

            }
            return listLoanRental;
        }

        /// <summary>
        /// Método que obtiene una lista con los datos de la búsqueda de Préstamo Alquiler de Equipo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strFechaInicio">Fecha inicial</param>
        /// <param name="strFechaFin">Fecha final</param>
        /// <param name="strDocCliente">Documento de cliente</param>
        /// <param name="strSIM">SIM</param>
        /// <param name="strIMEI">IMEI</param>
        /// <returns>Devuelve listado de objetos LoanRental con información de préstamo de alquiler de equipo.</returns>
        public static List<Entity.Dashboard.Postpaid.LoanRental> GetloanRentalEquipmentBD(string strIdSession, string strTransaction, string strFechaInicio, string strFechaFin, string strDocCliente, string strSIM, string strIMEI)
        {
            DbParameter[] parameters = new DbParameter[]{
                 new DbParameter("K_FECHAINICIO", DbType.String, ParameterDirection.Input, strFechaInicio),
                 new DbParameter("K_FECHAFIN", DbType.String, ParameterDirection.Input, strFechaFin),
                 new DbParameter("K_DOCCLIENTE", DbType.String, ParameterDirection.Input, strDocCliente),
                 new DbParameter("K_IMEI", DbType.String, ParameterDirection.Input, strIMEI),
                 new DbParameter("K_SIM", DbType.String, ParameterDirection.Input, strSIM),
                 new DbParameter("K_RESPUESTA", DbType.Object, ParameterDirection.Output),
                 new DbParameter("CU_PRODUCTOSCLIENTE", DbType.Object, ParameterDirection.Output),

             };

            return DbFactory.ExecuteReader<List<LoanRental>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BD_MSSAP, DbCommandConfiguration.SIACU_SP_SSAPSS_PRODUCTOS_CLIENTE, parameters);
        }

        /// <summary>
        /// Método que obtiene los datos del estado de la cuenta LDI por id y tipo de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <param name="strTipo">Tipo</param>
        /// <returns>Devuelve objeto StatusAccountLDI con información de estado de cuenta LDI.</returns>
        public static List<StatusAccountLDI> StatusAccountLDI(string strIdSession, string strTransaction, string strCustomerId, string strTipo)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_FLAG_BUSQ", DbType.String, ParameterDirection.Input, strTipo),
                new DbParameter("P_CUSTOMER_ID", DbType.String, ParameterDirection.Input,strCustomerId ),
                new DbParameter("P_CURSOR_SAL", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULT", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,255, ParameterDirection.Output)
            };
            Claro.Web.Logging.Info(strIdSession, strTransaction, "StatusAccountLDI Data strCustomerId:  " + strCustomerId);
            Claro.Web.Logging.Info(strIdSession, strTransaction, "StatusAccountLDI Data strTipo:  " + strTipo);
            List<StatusAccountLDI> listStatusAccountLDI = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_COBIL, DbCommandConfiguration.SIACU_SP_QUERY_BILLS_SUMMARY, parameters, (IDataReader reader) =>
            {
                listStatusAccountLDI = new List<StatusAccountLDI>();

                while (reader.Read())
                {
                    StatusAccountLDI objStatusAccountLDI = new StatusAccountLDI();

                    if (strTipo == "1")
                    {
                        objStatusAccountLDI.Detalle = Convert.ToString(reader["DETALLE"]);
                        objStatusAccountLDI.ClaroBillId = Convert.ToString(reader["CLARO_BILL_ID"]);
                        objStatusAccountLDI.LdiBillId = Convert.ToString(reader["LDI_BILL_ID"]);
                        objStatusAccountLDI.RegistryDate = Convert.ToString(reader["REGISTRY_DATE"]);
                        objStatusAccountLDI.EmittedDate = Convert.ToString(reader["EMITTED_DATE"]);
                        objStatusAccountLDI.MaturityDate = Convert.ToString(reader["MATURITY_DATE"]);
                        objStatusAccountLDI.BillTotal = Convert.ToDouble(reader["BILL_TOTAL"]);
                        objStatusAccountLDI.ImportePend = Convert.ToDouble(reader["IMPORTE_PEND"]);
                        objStatusAccountLDI.Status = Convert.ToString(reader["STATUS"]);
                        objStatusAccountLDI.AmountNotRequired = Convert.ToDouble(reader["MONTO_NOEXIGIBLE"]);
                        switch (objStatusAccountLDI.LdiBillId.Substring(0, 2))
                        {
                            case Claro.SIACU.Constants.COBILOperador1:
                                objStatusAccountLDI.OperatorShort = KEY.AppSettings("Operador1");
                                break;
                            case Claro.SIACU.Constants.COBILOperador2:
                                objStatusAccountLDI.OperatorShort = KEY.AppSettings("Operador2");
                                break;
                            case Claro.SIACU.Constants.COBILOperador3:
                                objStatusAccountLDI.OperatorShort = KEY.AppSettings("Operador3");
                                break;
                            default:
                                objStatusAccountLDI.OperatorShort = "";
                                break;
                        }
                    }
                    else
                    {
                        objStatusAccountLDI.Payment = Convert.ToString(reader["PAGO"]);
                        objStatusAccountLDI.ClaroBillId = Convert.ToString(reader["CLARO_BILL_ID"]);
                        objStatusAccountLDI.LdiBillId = Convert.ToString(reader["LDI_BILL_ID"]);
                        objStatusAccountLDI.BankDesc = Convert.ToString(reader["BANK_DESC"]);
                        objStatusAccountLDI.RegistryDate = Convert.ToString(reader["REGISTRY_DATE"]);
                        objStatusAccountLDI.PaymentDate = Convert.ToString(reader["PAYMENT_DATE"]);
                        objStatusAccountLDI.AmountPen = Convert.ToDouble(reader["AMOUNT_PEN"]);
                        switch (objStatusAccountLDI.LdiBillId.Substring(0, 2))
                        {
                            case Claro.SIACU.Constants.COBILOperador1:
                                objStatusAccountLDI.OperatorShort = KEY.AppSettings("Operador1");
                                break;
                            case Claro.SIACU.Constants.COBILOperador2:
                                objStatusAccountLDI.OperatorShort = KEY.AppSettings("Operador2");
                                break;
                            case Claro.SIACU.Constants.COBILOperador3:
                                objStatusAccountLDI.OperatorShort = KEY.AppSettings("Operador3");
                                break;
                            default:
                                objStatusAccountLDI.OperatorShort = "";
                                break;
                        }
                    }

                    listStatusAccountLDI.Add(objStatusAccountLDI);
                }
            });
            Claro.Web.Logging.Info(strIdSession, strTransaction, "StatusAccountLDI Data listStatusAccountLDI :  " + listStatusAccountLDI);
            return listStatusAccountLDI;
        }

        /// <summary>
        /// Método que obtiene una lista de Historial Acciones HFC por número de contrato y rango de fechas. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strContrato">Teléfono</param>
        /// <param name="strFechaIni">Fecha inicial</param>
        /// <param name="strFechaFin">Fecha final</param>
        /// <returns>Devuelve listado de objetos HistoryActions con información de historial de acciones.</returns>
        public static List<HistoryActions> GetHistoryActionsHFC(string strIdSession, string strTransaction, int intContract, string strStarDate, string strEndDate)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.Int64, ParameterDirection.Input, intContract),
                new DbParameter("p_fecha_ini", DbType.String, 30, ParameterDirection.Input,strStarDate ),
                new DbParameter("p_fecha_fin", DbType.String, 30, ParameterDirection.Input, strEndDate),
                new DbParameter("cursor_salida_campos", DbType.Object, ParameterDirection.Output)
            };

            List<HistoryActions> listHistoryActions = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_HIST_ACCIONS_HFC, parameters, (IDataReader reader) =>
            {
                listHistoryActions = new List<HistoryActions>();

                while (reader.Read())
                {
                    listHistoryActions.Add(new HistoryActions()
                    {
                        CONTRATO = Convert.ToInt(reader["CONTRATO"]),
                        DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]),
                        FECH_ORDE = Convert.ToDate(reader["FECH_ORDE"]),
                        FECHA = Convert.ToString(reader["FECHA"]),
                        HORA = Convert.ToString(reader["HORA"]),
                        SERVICIO = Convert.ToString(reader["SERVICIO"]),
                        USUARIO = Convert.ToString(reader["USUARIO"])
                    });
                }
            });

            return listHistoryActions;
        }

        /// <summary>
        /// Método que obtiene una lista de Historial Acciones por número de teléfono y rango de fechas. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelefono">Teléfono</param>
        /// <param name="strFechaIni">Fecha inicial</param>
        /// <param name="strFechaFin">Fecha final</param>
        /// <returns>Devuelve listado de objetos HistoryActions con información de historial de acciones.</returns>
        public static List<HistoryActions> GetHistoryActions(string strIdSession, string strTransaction, string strTelefono, string strFechaIni, string strFechaFin)
        {
            List<HistoryActions> listHistoryActions = new List<HistoryActions>();
            POSTPAID_CONSULTCLIENT.historicoAcciones[] objHistoricoAcciones =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.historicoAcciones[]>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.historicoAcciones(strTelefono, strFechaIni, strFechaFin); });

            foreach (var item in objHistoricoAcciones)
            {
                listHistoryActions.Add(new HistoryActions()
                {
                    CONTRATO = item.contrato,
                    DESCRIPCION = item.descripcion,
                    FECH_ORDE = item.fech_orde,
                    FECHA = item.fecha,
                    HORA = item.hora,
                    SERVICIO = item.servicio,
                    USUARIO = item.usuario,
                });
            }

            return listHistoryActions;
        }

        /// <summary>
        /// Metodo que devuelve lista de Historico de Acciones TOBE
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTelefono"></param>
        /// <param name="strFechaIni"></param>
        /// <param name="strFechaFin"></param>
        /// <param name="flagConvivencia"></param>
        /// <returns></returns>
        public static List<HistoryActions> GetHistoryActionsTobe(AuditRequest audit, string strTransaction, string strTelefono, string strFechaIni, string strFechaFin, string flagConvivencia)
        {
            Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Response.response response = null;
            HistoryActions HistoryActionsTobe = null;
            List<HistoryActions> lstHistoryActionsTobe = new List<HistoryActions>();

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Request.request()
                {
                    consultarHistoricoAccionesRequest = new Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Request.consultarHistoricoAccionesRequest()
                    {
                        msisdn = strTelefono,
                        fechaInicio = strFechaIni,
                        fechaFin = strFechaFin,
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Common.listaOpcional>()
                        {
                            new Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Common.listaOpcional()
                            {
                                clave = Claro.ConfigurationManager.AppSettings("strFlagConvivencia"),
                                valor= flagConvivencia
                            }
                        }
                    }
                };

                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetHistoryActions.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_HISTORICO_ACCIONES_TOBE, audit, request, false);



            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (response != null && response.consultarHistoricoAccionesResponse != null && response.consultarHistoricoAccionesResponse.responseAudit != null &&
                response.consultarHistoricoAccionesResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString && response.consultarHistoricoAccionesResponse.responseData != null && response.consultarHistoricoAccionesResponse.responseData.listaHistoricoAcciones != null)
            {


                foreach (var obj in response.consultarHistoricoAccionesResponse.responseData.listaHistoricoAcciones)
                {
                    HistoryActionsTobe = new HistoryActions();
                    HistoryActionsTobe.CONTRATO = obj.contrato;
                    HistoryActionsTobe.DESCRIPCION = obj.descripcion;
                    HistoryActionsTobe.FECHA = obj.fecha; 
                    HistoryActionsTobe.FECH_ORDE = obj.fechaOrden;
                    HistoryActionsTobe.HORA = obj.hora;
                    HistoryActionsTobe.SERVICIO = obj.servicio;
                    HistoryActionsTobe.USUARIO = obj.usuario;
                    lstHistoryActionsTobe.Add(HistoryActionsTobe);
                }

            }

            lstHistoryActionsTobe = (from lista in lstHistoryActionsTobe orderby Convert.ToDate(lista.FECHA + " " +lista.HORA) descending select lista).ToList();

            return lstHistoryActionsTobe;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Pro.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeDetailTimPro con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimPro> GetFixedChargeDetailTimPro(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimPro>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1410, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Pro 1.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeDetailTimProOne con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimProOne> GetFixedChargeDetailTimProOne(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimProOne>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1410, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Pro 2.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeDetailTimProTwo con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimProTwo> GetFixedChargeDetailTimProTwo(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimProTwo>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1410, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del Detalle Cargo Fijo TIM Max.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeDetailTimMax con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimMax> GetFixedChargeDetailTimMax(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimMax>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1410, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Max 2.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeDetailTimMaxTwo con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimMaxTwo> GetFixedChargeDetailTimMaxTwo(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimMaxTwo>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1410, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del Detalle Descuento Cargo Fijo.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos DiscountFixedChargeDetail con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.DiscountFixedChargeDetail> GetDiscountFixedChargeDetail(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<DiscountFixedChargeDetail>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1410, parameters);
        }

        /// <summary>
        /// Método que obtiene los datos de compromiso de pago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strIdCustomer">Id de cliente</param>
        /// <returns>Devuelve listado de objetos Account con información de la cuenta.</returns>
        public static List<Entity.Dashboard.Postpaid.Account> GetPaymentCommitment(string strIdSession, string strTransaction, string strIdCustomer)
        {
            List<Entity.Dashboard.Postpaid.Account> listAccount = new List<Entity.Dashboard.Postpaid.Account>();

            POSTPAID_CONSULTCLIENT.compromisoPago[] arrCompromisoPago =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.compromisoPago[]>
           (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () =>
           { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.compromisoPago(strIdCustomer); });


            if (arrCompromisoPago != null && arrCompromisoPago.Length > 0)
            {
                foreach (POSTPAID_CONSULTCLIENT.compromisoPago objCompromisoPago in arrCompromisoPago)
                {
                    listAccount.Add(new Account()
                    {
                        NOMBRE = objCompromisoPago.usuario,
                        FECHA_EXPIRACION = objCompromisoPago.fecha_expiracion.ToString(),
                        SALDO = objCompromisoPago.importe.ToString()
                    });
                }
            }

            return listAccount;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Pro Bolsa.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeDetailTimProBag con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimProBag> GetFixedChargeDetailTimProBag(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimProBag>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1315, parameters);

        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Pro Bolsa 1.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objeto FixedChargeDetailTimProBagOne con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimProBagOne> GetFixedChargeDetailTimProBagOne(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimProBagOne>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1315, parameters);

        }

        /// <summary>
        /// Método que devuelve una lista con los datos de facturación Detalle Cargo Fijo TIM Pro Bolsa 2.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objeto FixedChargeDetailTimProBagTwo con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimProBagTwo> GetFixedChargeDetailTimProBagTwo(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimProBagTwo>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1315, parameters);

        }

        /// <summary>
        /// Método que devuelve una lista con los datos del Detalle Cargo Fijo TIM Pro Bolsa 3.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objeto FixedChargeDetailTimProBagThree con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeDetailTimProBagThree> GetFixedChargeDetailTimProBagThree(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeDetailTimProBagThree>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1315, parameters);

        }

        /// <summary>
        /// Método que devuelve una lista con las Consultas de Histórico de entrega por recibo de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strReceipt">Recibo</param>
        /// <param name="strFlag">Flag</param>
        /// <returns>Devuelve listado de objetos HistoricDelivery con información histórica de delivery.</returns>
        public static List<Entity.Dashboard.Postpaid.HistoricDelivery> GetHistoricDelivery(string strIdSession, string strTransaction, string strReceipt, string strFlag)
        {

            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("inRECIBOS", DbType.String, 1000,ParameterDirection.Input, strReceipt),
                new DbParameter("inFLG", DbType.String, 1,ParameterDirection.Input, strFlag),
                new DbParameter("ouCURRECIBOS", DbType.Object, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.Int32, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String, 250, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<HistoricDelivery>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_CONSULTA_RECIBOHIS, parameters);
        }

        /// <summary>
        /// Método que obtiene una lista con los datos de facturación Cargo Fijo TIM Pro Bolsa.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">G>roupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeTimProBagDetail con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeTimProBagDetail> GetFixedChargeTimProBagDetail(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeTimProBagDetail>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1455, parameters);

        }

        /// <summary>
        /// Método que obtiene una lista con los datos de facturación Cargo Fijo TIM Pro Bolsa 1.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">Groupbox</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeTimProBagDetailOne con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeTimProBagDetailOne> GetFixedChargeTimProBagDetailOne(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeTimProBagDetailOne>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1455, parameters);

        }

        /// <summary>
        /// Método que obtiene una lista con los datos de facturación Cargo Fijo TIM Pro Bolsa 2.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">-</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeTimProBagDetailTwo con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeTimProBagDetailTwo> GetFixedChargeTimProBagDetailTwo(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeTimProBagDetailTwo>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1455, parameters);

        }

        /// <summary>
        /// Método que obtiene una lista con los datos del Cargo Fijo TIM Pro Bolsa 3. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strGroupBox">-</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <returns>Devuelve listado de objetos FixedChargeTimProBagDetailThree con información de cargo fijo.</returns>
        public static List<Entity.Dashboard.Postpaid.FixedChargeTimProBagDetailThree> GetFixedChargeTimProBagDetailThree(string strIdSession, string strTransaction, string strGroupBox, string strInvoiceNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,18, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_GROUPBOX", DbType.Int32,10, ParameterDirection.Input, strGroupBox),
                new DbParameter("K_ERRMSG", DbType.String,100, ParameterDirection.Output),
                 new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<FixedChargeTimProBagDetailThree>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1455, parameters);

        }

        /// <summary>
        /// Método que obtiene una lista de Consultas de Histórico de entrega por número de contrato y cantidad de registros.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intQuantityRegist">Cantidad de registros</param>
        /// <param name="strCustomerAcc">Cliente ACC</param>
        /// <returns>Devuelve listado de objetos ReceiptHistory con información histórica de recibo.</returns>
        public static List<Entity.Dashboard.Postpaid.ReceiptHistory> GetTolsReceipt(string strIdSession, string strTransaction, Int32 intQuantityRegist, string strCustomerAcc)
        {
            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("K_CODIGOCLIENTE", DbType.String, 24,ParameterDirection.Input, strCustomerAcc),
                new DbParameter("K_PageSize", DbType.Int32, 4,ParameterDirection.Input, intQuantityRegist),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<ReceiptHistory>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENER_RECIBOS, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista de Cuota de equipo por id de cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intCodigoAplicacion">Código de aplicación</param>
        /// <param name="strUsuarioAplicacion">Usuario de aplicación</param>
        /// <param name="strOrigen">Origen</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos FeeEquipment con información de pago de equipo.</returns>
        public static List<Entity.Dashboard.Postpaid.FeeEquipment> GetFeeEquipment(string strIdSession, string strTransaction, int intCodigoAplicacion, string strUsuarioAplicacion, string strOrigen, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("pv_cod_aplicacion", DbType.Int64, ParameterDirection.Input, intCodigoAplicacion),
                new DbParameter("pv_usu_aplicacion", DbType.String, ParameterDirection.Input, strUsuarioAplicacion),
                new DbParameter("pv_origen", DbType.String, ParameterDirection.Input, strOrigen),
                new DbParameter("pv_customer_id", DbType.String, ParameterDirection.Input, strCustomerId),
                new DbParameter("xv_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("xv_cod_err", DbType.Int32, ParameterDirection.Output),
                new DbParameter("xv_des_err", DbType.String, 250, ParameterDirection.Output)
             };

            return DbFactory.ExecuteReader<List<FeeEquipment>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_OAC, DbCommandConfiguration.SIACU_SP_CONSULTA_DOC_CVE_CSR, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con los datos del estado de cuenta AOC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strUser">Usuario</param>
        /// <param name="strNroCuenta">Número de cuenta</param>
        /// <param name="strErrCod">Código de error</param>
        /// <returns>Devuelve listado de objetos StatusAccountAOC con información de estado de cuenta AOC.</returns>
        public static List<Entity.Dashboard.Postpaid.StatusAccountAOC> GetStatusAccountAOC(string strIdSession, string strTransactionID, string strUser, string strNroCuenta, ref string strErrCod)
        {

            StatusAccountAOC objStatusAccountAOC;
            List<StatusAccountAOC> listStatusAccountAOC = null;

            string strCodApliOAC = "";
            string strConsTipoServAjuste = "";
            string strFlagSaldoOAC = "";
            string sTipoConsultaPAGOAC = "";
            string strTamPaginaOAC = "";
            string strNroPagina1 = "";
            string strTxIDWSConsEstCtaOAC1 = "";
            string strNroTelfWSConsEstCtaOAC1 = "";
            string strFlgSoloDisputaWSConsEstCtaOAC1 = "";
            string strTipoConsultaOAC = "";

            try
            {
                strCodApliOAC = KEY.AppSettings("strCodApliOAC");
                strConsTipoServAjuste = KEY.AppSettings("strConsTipoServAjuste");
                strFlagSaldoOAC = KEY.AppSettings("strFlagSaldoOAC", "");
                sTipoConsultaPAGOAC = KEY.AppSettings("sTipoConsultaPAGOAC");
                strTamPaginaOAC = KEY.AppSettings("strTamPaginaOAC");
                strNroPagina1 = KEY.AppSettings("strNroPagina");
                strTxIDWSConsEstCtaOAC1 = KEY.AppSettings("strTxIDWSConsEstCtaOAC", "");
                strNroTelfWSConsEstCtaOAC1 = KEY.AppSettings("strNroTelfWSConsEstCtaOAC", "");
                strFlgSoloDisputaWSConsEstCtaOAC1 = KEY.AppSettings("strFlgSoloDisputaWSConsEstCtaOAC", "");
                strTipoConsultaOAC = KEY.AppSettings("strTipConsultaWSConsEstCtaOAC", "");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, "Error:" + ex.Message);
            }

            try
            {
            string strCodApli = strCodApliOAC;
            string strTipServicio = strConsTipoServAjuste;
            string strFlagSaldo = strFlagSaldoOAC;
            decimal strTamPagina = Convert.ToDecimal(strTamPaginaOAC);
            decimal strNroPagina = Convert.ToDecimal(strNroPagina1);
            string strTxIDWSConsEstCtaOAC = strTxIDWSConsEstCtaOAC1;
            string strNroTelfWSConsEstCtaOAC = strNroTelfWSConsEstCtaOAC1;
            string strFlgSoloDisputaWSConsEstCtaOAC = strFlgSoloDisputaWSConsEstCtaOAC1;

            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo(Claro.SIACU.Constants.CulturejaJp, true);
            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;
                DateTime dateFrom = Convert.ToDate(Claro.SIACU.Constants.DateDefautlCreditLimit);
                DateTime dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0);
            string strdateFrom = KEY.AppSettings("strFechaDesdeWSConsEstCtaOAC", ""), strdateTo = ConfigurationManager.AppSettings("strFechaHastaWSConsEstCtaOAC", "");
           
                //PROY-140464 Ajustes - INI
                var objStatusAccountDPResponse = new StatusAccountDPResponse();
                var objStatusAccountDPRequest = new StatusAccountDPRequest();
                var objEstadoCuenta = new estadoCuenta();
                objStatusAccountDPRequest.AuditService = new Audit();
                objStatusAccountDPRequest.Audit = new AuditRequest()
                {
                    Transaction = strTransactionID,
                    ApplicationName = "",
                    UserName = "SIACU",
                    IPAddress = KEY.AppSettings("strWsIpAjustesDoc"),
                    Session = ""
                };
                objStatusAccountDPRequest.MessageRequest = new StatusAccountDPMessageRequest();
                objStatusAccountDPRequest.MessageRequest.Header = new Entity.Common.GetDataPower.HeadersRequest();
                objStatusAccountDPRequest.MessageRequest.Header.HeaderRequest = new Entity.Common.GetDataPower.HeaderRequest()
                {
                    consumer = "SIACU",
                    country = "PE",
                    dispositivo = "MOVIL",
                    language = "ES",
                    modulo = "",
                    msgType = "Request",
                    operation = "Listar estado de cuenta",
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = "SIACU",
                    timestamp = DateTime.Now.ToString("o"),
                    userId = "SIACU",
                    wsIp = KEY.AppSettings("strWsIpAjustesDoc")
                };

                Claro.Web.Logging.Info(strIdSession, strTransactionID, "Inicio - Cambio Ajustes. nroCta: " + strNroCuenta);

                objStatusAccountDPRequest.MessageRequest.Body = new StatusAccountDPBodyRequest()
                {
                    txId = strTxIDWSConsEstCtaOAC,
                    codAplicacion = strCodApli,
                    usuarioAplic = strUser,
                    tipoConsulta = strTipoConsultaOAC,
                    tipoServicio = strTipServicio,
                    cliNroCuenta = strNroCuenta,
                    nroTelefono = strNroTelfWSConsEstCtaOAC,
                    flagSoloSaldo = strFlagSaldo,
                    flagSoloDisputa = strFlgSoloDisputaWSConsEstCtaOAC,
                    fechaDesde = strdateFrom,
                    fechaHasta = strdateTo,
                    tamanoPagina = strTamPagina,
                    nroPagina = strNroPagina
                };
           
            try
            {
                    objStatusAccountDPResponse = GetStatusAccountDP(objStatusAccountDPRequest, strIdSession, strTransactionID);
                    if (objStatusAccountDPResponse != null && objStatusAccountDPResponse.MessageResponse.Body.codigoRespuesta == "0")
                    {
                        objEstadoCuenta = objStatusAccountDPResponse.MessageResponse.Body.estadoCuenta;
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
                }

                strErrCod = Claro.Constants.NumberZeroString;
                listStatusAccountAOC = new List<StatusAccountAOC>();

                if (objStatusAccountDPResponse != null && objStatusAccountDPResponse.MessageResponse.Body.codigoRespuesta == "0")
                {
                    Claro.Web.Logging.Info(strIdSession, strTransactionID, "Inicio : Llenado detalle de Cuenta.");
                    var objDetalleEstadoCuenta = objEstadoCuenta.detalleEstadoCuentaCab[0].detalle.detalleEstadoCuenta;

                    if (objDetalleEstadoCuenta.Count > 0)
                    {
                        for (int i = 0; i < objDetalleEstadoCuenta.Count; i++)
                        {
                            objStatusAccountAOC = new StatusAccountAOC();
                            if ((objDetalleEstadoCuenta[i].tipoDocumento == Claro.SIACU.Constants.REC) || (objDetalleEstadoCuenta[i].tipoDocumento == Claro.SIACU.Constants.FAC))
                            {
                                objStatusAccountAOC.Tipo = Claro.SIACU.Constants.Bill;
                            }
                            else
                            {
                                if ((objDetalleEstadoCuenta[i].tipoDocumento == Claro.SIACU.Constants.ND) || (objDetalleEstadoCuenta[i].tipoDocumento == Claro.SIACU.Constants.NC))
                                {
                                    objStatusAccountAOC.Tipo = Claro.SIACU.Constants.Adjustment;
                                }
                                else
                                {
                                    objStatusAccountAOC.Tipo = objDetalleEstadoCuenta[i].tipoDocumento;
                                }
                            }

                            if (objDetalleEstadoCuenta[i].tipoDocumento == Claro.SIACU.Constants.Payment)
                            {
                                objStatusAccountAOC.Documento = objDetalleEstadoCuenta[i].nroOperacionPago;
                            }
                            else
                            {
                                objStatusAccountAOC.Documento = objDetalleEstadoCuenta[i].nroDocumento;
                            }
                            objStatusAccountAOC.DescripcionPago = Convert.ToString(objDetalleEstadoCuenta[i].descripcionDocumento);
                            objStatusAccountAOC.FechaRegistro = String.IsNullOrEmpty(objDetalleEstadoCuenta[i].fechaRegistro) ? "" : DateTime.Parse(objDetalleEstadoCuenta[i].fechaRegistro).ToString("dd/MM/yyyy");
                            objStatusAccountAOC.FechaEmision = String.IsNullOrEmpty(objDetalleEstadoCuenta[i].fechaEmision) ? "" : DateTime.Parse(objDetalleEstadoCuenta[i].fechaEmision).ToString("dd/MM/yyyy");
                            objStatusAccountAOC.FechaVencimiento = String.IsNullOrEmpty(objDetalleEstadoCuenta[i].fechaVencimiento) ? "" : DateTime.Parse(objDetalleEstadoCuenta[i].fechaVencimiento).ToString("dd/MM/yyyy");
                            objStatusAccountAOC.Cargo = Convert.ToString(objDetalleEstadoCuenta[i].cargo);
                            objStatusAccountAOC.Abono = Convert.ToString(objDetalleEstadoCuenta[i].abono);
                            objStatusAccountAOC.ImportePendiente = Convert.ToString(objDetalleEstadoCuenta[i].saldoDocumento);
                            objStatusAccountAOC.MontoReclamo = Convert.ToString(objDetalleEstadoCuenta[i].montoReclamado);
                            objStatusAccountAOC.DescripcionDetalle = Convert.ToString(objDetalleEstadoCuenta[i].descripcionExtend);
                            objStatusAccountAOC.flagBotonAnulacion = objDetalleEstadoCuenta[i].flagBotonAnulacion;
                            objStatusAccountAOC.EstadoDocumento = objDetalleEstadoCuenta[i].estadoDocumento;

                            listStatusAccountAOC.Add(objStatusAccountAOC);
                        }
                    }
                    else
                    {
                        strErrCod = Claro.Constants.NumberOneNegativeString;
                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, "Error:" + ex.Message);
            }
            return listStatusAccountAOC;
        }
 
        public static List<Entity.Dashboard.Postpaid.StatusAccountAOC> GetStatusAccountAOCToBe(AuditRequest audit, string strTransactionID, string strUser, string strNroCuenta)
        {
            List<Entity.Dashboard.Postpaid.StatusAccountAOC> listStatusAccountAOC = new List<StatusAccountAOC>();
            Entity.Dashboard.Postpaid.StatusAccountAOC objStatusAccountAOC = null;
            Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Response.response objResponse = null;

            string strCodApliOAC = "";
            string strConsTipoServAjuste = "";
            string strFlagSaldoOAC = "";
            string strTamPaginaOAC = "";
            string strNroPagina1 = "";
            string strTxIDWSConsEstCtaOAC1 = "";
            string strNroTelfWSConsEstCtaOAC1 = "";
            string strFlgSoloDisputaWSConsEstCtaOAC1 = "";

            try
            {
                strCodApliOAC = KEY.AppSettings("strCodApliOAC");
                strConsTipoServAjuste = KEY.AppSettings("strConsTipoServAjuste");
                strFlagSaldoOAC = KEY.AppSettings("strFlagSaldoOAC", "");
                strTamPaginaOAC = KEY.AppSettings("strTamPaginaOAC");
                strNroPagina1 = KEY.AppSettings("strNroPagina");
                strTxIDWSConsEstCtaOAC1 = KEY.AppSettings("strTxIDWSConsEstCtaOAC", "");
                strNroTelfWSConsEstCtaOAC1 = KEY.AppSettings("strNroTelfWSConsEstCtaOAC", "");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, strTransactionID, "Error:" + ex.Message);
            }

            string strCodApli = strCodApliOAC;
            decimal strTamPagina = Convert.ToDecimal(strTamPaginaOAC);
            decimal strNroPagina = Convert.ToDecimal(strNroPagina1);
            string strNroTelfWSConsEstCtaOAC = strNroTelfWSConsEstCtaOAC1;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Request.request objRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Request.request()
                {
                    consultarEstadoCuentaRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Request.consultarEstadoCuentaRequest()
                    {
                        nroPag = strNroPagina.ToString(),
                        tamanoPag = strTamPagina.ToString(),
                        codAplicacion = strCodApli,
                        nroCuentaCli = strNroCuenta,
                        nroTelefono = strNroTelfWSConsEstCtaOAC,
                        usuario = strUser,
                    }
                };
                objResponse = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataAccountState.Response.response>(RestServiceConfiguration.OBTENER_ESTADO_CUENTA_LINEA_TOBE, audit, objRequest, false);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (objResponse != null &&
                objResponse.consultarEstadoCuentaResponse != null &&
                objResponse.consultarEstadoCuentaResponse.responseAudit != null &&
                objResponse.consultarEstadoCuentaResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                objResponse.consultarEstadoCuentaResponse.responseData != null)
            {
                foreach (var objListaDetalleEecc in objResponse.consultarEstadoCuentaResponse.responseData.estadoCuenta.listaDetalleEecc)
                {
                    objStatusAccountAOC = new Entity.Dashboard.Postpaid.StatusAccountAOC();

                    if ((objListaDetalleEecc.tipoDocumento == Claro.SIACU.Constants.REC) || (objListaDetalleEecc.tipoDocumento == Claro.SIACU.Constants.FAC))
                    {
                        objStatusAccountAOC.Tipo = Claro.SIACU.Constants.Bill;
                    }
                    else
                    {
                        if ((objListaDetalleEecc.tipoDocumento == Claro.SIACU.Constants.ND) || (objListaDetalleEecc.tipoDocumento == Claro.SIACU.Constants.NC))
                        {
                            objStatusAccountAOC.Tipo = Claro.SIACU.Constants.Adjustment;
                        }
                        else
                        {
                            objStatusAccountAOC.Tipo = objListaDetalleEecc.tipoDocumento;
                        }
                    }

                    if (objListaDetalleEecc.tipoDocumento == Claro.SIACU.Constants.Payment)
                    {
                        objStatusAccountAOC.Documento = objListaDetalleEecc.nroOperacionPago;
                    }
                    else
                    {
                        objStatusAccountAOC.Documento = objListaDetalleEecc.nroDocumento;
                    }
                    objStatusAccountAOC.DescripcionPago = Convert.ToString(objListaDetalleEecc.descripDocumento);
                    objStatusAccountAOC.FechaRegistro = String.IsNullOrEmpty(objListaDetalleEecc.fechaRegistro) ? "" : DateTime.Parse(objListaDetalleEecc.fechaRegistro).ToString("dd/MM/yyyy");
                    objStatusAccountAOC.FechaEmision = String.IsNullOrEmpty(objListaDetalleEecc.fechaEmision) ? "" : DateTime.Parse(objListaDetalleEecc.fechaEmision).ToString("dd/MM/yyyy");
                    objStatusAccountAOC.FechaVencimiento = String.IsNullOrEmpty(objListaDetalleEecc.fechaVencimiento) ? "" : DateTime.Parse(objListaDetalleEecc.fechaVencimiento).ToString("dd/MM/yyyy");
                    objStatusAccountAOC.Cargo = Convert.ToString(objListaDetalleEecc.cargo);
                    objStatusAccountAOC.Abono = Convert.ToString(objListaDetalleEecc.abono);
                    objStatusAccountAOC.ImportePendiente = Convert.ToString(objListaDetalleEecc.saldoDocumento);
                    objStatusAccountAOC.MontoReclamo = Convert.ToString(objListaDetalleEecc.montoReclamado);
                    objStatusAccountAOC.DescripcionDetalle = Convert.ToString(objListaDetalleEecc.descripExtend);
                    objStatusAccountAOC.idOnbase = Convert.ToString(objListaDetalleEecc.idOnbase);

                    listStatusAccountAOC.Add(objStatusAccountAOC);
                }
            }

            return listStatusAccountAOC;
        }

        /// <summary>
        /// Método que retorna una lista con el detalle de tráfico local en llamadas TP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <returns>Devuelve listado de objetos LocalTrafficDetailCallTP con información de tráfico local de llamadas.</returns>
        public static List<Entity.Dashboard.Postpaid.LocalTrafficDetailCallTP> GetDetailLocalTrafficCallTP(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall)
        {
            List<Entity.Dashboard.Postpaid.LocalTrafficDetailCallTP> listLocalTrafficDetailCallTP = new List<LocalTrafficDetailCallTP>();
            Entity.Dashboard.Postpaid.LocalTrafficDetailCallTP objLocalTrafficDetailCallTP = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARDETTP, parameters, (IDataReader reader) =>
            {
                objLocalTrafficDetailCallTP = new Entity.Dashboard.Postpaid.LocalTrafficDetailCallTP();

                while (reader.Read())
                {
                    string strCallTimeEnd = "";
                    string strCallDuration = Convert.ToString(reader["callDuration"]);
                    string strCallTime = Convert.ToString(reader["calltime"]);
                    string strCallDateFin = Convert.ToString(reader["calldatefin"]);


                    if ((strCallDuration != null) && (strCallDuration.Length > 0) && (strCallTime != null) && (strCallTime.Length > 0))
                    {
                        int lIntCallDuration = Convert.ToInt(strCallDuration);
                        string[] strHora = Convert.ToString(strCallTime).Split(char.Parse(":"));

                        int lIntHora = Convert.ToInt(strHora[0]);
                        int lIntMin = Convert.ToInt(strHora[1]);
                        int lIntSeg = Convert.ToInt(strHora[2]);
                        int lIntcSeg = (lIntSeg + lIntCallDuration) / Claro.Constants.NumberSixty;
                        int lIntrSeg = (lIntSeg + lIntCallDuration) % Claro.Constants.NumberSixty;
                        lIntSeg = lIntrSeg;
                        int lIntcMin = (lIntMin + lIntcSeg) / Claro.Constants.NumberSixty;
                        int lIntrMin = (lIntMin + lIntcSeg) % Claro.Constants.NumberSixty;
                        lIntMin = lIntrMin;

                        if ((lIntHora + lIntcMin) > 24)
                        {
                            lIntHora = lIntHora + lIntcMin - Claro.Constants.NumberTwentyFour;
                            strCallTimeEnd = strCallDateFin + lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                        else
                        {
                            lIntHora = lIntHora + lIntcMin;
                            strCallTimeEnd = lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                    }

                    objLocalTrafficDetailCallTP = new Entity.Dashboard.Postpaid.LocalTrafficDetailCallTP()
                    {
                        CALLDURATION = Convert.ToString(reader["callDuration"]),
                        CALLTIME = Convert.ToString(reader["calltime"]),
                        CALLTIMEFIN = Convert.ToString(strCallTimeEnd),
                        CALLDATEFIN = Convert.ToString(reader["calldatefin"]),
                        CALLNUMBER = Convert.ToString(reader["callnumber"]),
                        CALLDATE = Convert.ToString(reader["calldate"]),
                        CALLDESTINATION = Convert.ToString(reader["calldestination"]),
                        CALLDURATIONMIN = Convert.ToString(reader["calldurationmin"]),
                        CALLTOTAL = Convert.ToString(reader["calltotal"]),
                        CALLAMBITO = Convert.ToString(reader["ambito"])
                    };

                    listLocalTrafficDetailCallTP.Add(objLocalTrafficDetailCallTP);
                }
            });

            return listLocalTrafficDetailCallTP;
        }

        /// <summary>
        /// Método que retorna una lista con el detalle de tráfico local en llamadas TM.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <param name="strTariffTime">Tiempo de tarifa</param>
        /// <returns>Devuelve listado de objetos LocalTrafficDetailCallTM con información de tráfico local de llamadas.</returns>
        public static List<Entity.Dashboard.Postpaid.LocalTrafficDetailCallTM> GetDetailLocalTrafficCallTM(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall, string strTariffTime)
        {
            List<Entity.Dashboard.Postpaid.LocalTrafficDetailCallTM> listLocalTrafficDetailCallTM = new List<LocalTrafficDetailCallTM>();
            Entity.Dashboard.Postpaid.LocalTrafficDetailCallTM objLocalTrafficDetailCallTM = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_TARIFFTIME", DbType.String,1000, ParameterDirection.Input, strTariffTime), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARDETTM, parameters, (IDataReader reader) =>
            {
                objLocalTrafficDetailCallTM = new Entity.Dashboard.Postpaid.LocalTrafficDetailCallTM();

                while (reader.Read())
                {
                    string strCallTimeEnd = "";
                    string strCallDuration = Convert.ToString(reader["callDuration"]);
                    string strCallTime = Convert.ToString(reader["calltime"]);
                    string strCallDateFin = Convert.ToString(reader["calldatefin"]);


                    if ((strCallDuration != null) && (strCallDuration.Length > 0) && (strCallTime != null) && (strCallTime.Length > 0))
                    {
                        int lIntCallDuration = Convert.ToInt(strCallDuration);
                        string[] strHora = Convert.ToString(strCallTime).Split(char.Parse(":"));

                        int lIntHora = Convert.ToInt(strHora[0]);
                        int lIntMin = Convert.ToInt(strHora[1]);
                        int lIntSeg = Convert.ToInt(strHora[2]);
                        int lIntcSeg = (lIntSeg + lIntCallDuration) / Claro.Constants.NumberSixty;
                        int lIntrSeg = (lIntSeg + lIntCallDuration) % Claro.Constants.NumberSixty;
                        lIntSeg = lIntrSeg;
                        int lIntcMin = (lIntMin + lIntcSeg) / Claro.Constants.NumberSixty;
                        int lIntrMin = (lIntMin + lIntcSeg) % Claro.Constants.NumberSixty;
                        lIntMin = lIntrMin;

                        if ((lIntHora + lIntcMin) > 24)
                        {
                            lIntHora = lIntHora + lIntcMin - Claro.Constants.NumberTwentyFour;
                            strCallTimeEnd = strCallDateFin + lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                        else
                        {
                            lIntHora = lIntHora + lIntcMin;
                            strCallTimeEnd = lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                    }

                    objLocalTrafficDetailCallTM = new Entity.Dashboard.Postpaid.LocalTrafficDetailCallTM()
                    {
                        CALLDURATION = Convert.ToString(reader["callDuration"]),
                        CALLTIME = Convert.ToString(reader["calltime"]),
                        CALLTIMEFIN = Convert.ToString(strCallTimeEnd),
                        CALLDATEFIN = Convert.ToString(reader["calldatefin"]),
                        CALLNUMBER = Convert.ToString(reader["callnumber"]),
                        CALLDATE = Convert.ToString(reader["calldate"]),
                        CALLDESTINATION = Convert.ToString(reader["calldestination"]),
                        CALLDURATIONMIN = Convert.ToString(reader["calldurationmin"]),
                        CALLTOTAL = Convert.ToString(reader["calltotal"]),
                        CALLAMBITO = Convert.ToString(reader["ambito"])
                    };
                    listLocalTrafficDetailCallTM.Add(objLocalTrafficDetailCallTM);
                }
            });

            return listLocalTrafficDetailCallTM;
        }

        /// <summary>
        /// Método que devuelve una cadena con la cantidad de tráfico local de llamadas TP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <returns>Devuelve cantidad de llamadas de tráfico local.</returns>
        public static string GetQuantityLocalTrafficCallTP(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall)
        {
            string strQuantityTP = "";
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARCANTP, parameters, (IDataReader reader) =>
            {
                reader.Read();
                strQuantityTP = Convert.ToString(reader["AddUsage"]);

            });

            return strQuantityTP;
        }

        /// <summary>
        /// Método que devuelve una cadena con la cantidad de tráfico local de llamadas TM.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Númer de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <param name="strTariffTime">Tiempo de tarifa</param>
        /// <returns>Devuelve cantidad de llamadas de tráficp local.</returns>
        public static string GetQuantityLocalTrafficCallTM(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall, string strTariffTime)
        {
            string strQuantityTM = "";

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_TARIFFTIME", DbType.String,1000, ParameterDirection.Input, strTariffTime), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARCANTM, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    strQuantityTM = Convert.ToString(reader["AddUsage"]);
                    break;
                }
            });

            return strQuantityTM;
        }

        /// <summary>
        /// Método que obtiene una lista con el detalle de trafico local consumo en llamadas TP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <returns>Devuelve listado de objetos ConsumeLocalTrafficDetailCallTP con información de tráfico local de consumo de llamadas.</returns>
        public static List<Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTP> GetDetailConsumeLocalTrafficCallTP(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall)
        {
            List<Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTP> listConsumeLocalTrafficDetailCallTP = new List<ConsumeLocalTrafficDetailCallTP>();
            Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTP objConsumeLocalTrafficDetailCallTP = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARDETTPE, parameters, (IDataReader reader) =>
            {
                objConsumeLocalTrafficDetailCallTP = new Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTP();

                while (reader.Read())
                {
                    string strCallTimeEnd = "";
                    string strCallDuration = Convert.ToString(reader["callDuration"]);
                    string strCallTime = Convert.ToString(reader["calltime"]);
                    string strCallDateFin = Convert.ToString(reader["calldatefin"]);


                    if ((strCallDuration != null) && (strCallDuration.Length > 0) && (strCallTime != null) && (strCallTime.Length > 0))
                    {
                        int lIntCallDuration = Convert.ToInt(strCallDuration);
                        string[] strHora = Convert.ToString(strCallTime).Split(char.Parse(":"));

                        int lIntHora = Convert.ToInt(strHora[0]);
                        int lIntMin = Convert.ToInt(strHora[1]);
                        int lIntSeg = Convert.ToInt(strHora[2]);
                        int lIntcSeg = (lIntSeg + lIntCallDuration) / Claro.Constants.NumberSixty;
                        int lIntrSeg = (lIntSeg + lIntCallDuration) % Claro.Constants.NumberSixty;
                        lIntSeg = lIntrSeg;
                        int lIntcMin = (lIntMin + lIntcSeg) / Claro.Constants.NumberSixty;
                        int lIntrMin = (lIntMin + lIntcSeg) % Claro.Constants.NumberSixty;
                        lIntMin = lIntrMin;

                        if ((lIntHora + lIntcMin) > 24)
                        {
                            lIntHora = lIntHora + lIntcMin - Claro.Constants.NumberSixty;
                            strCallTimeEnd = strCallDateFin + lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                        else
                        {
                            lIntHora = lIntHora + lIntcMin;
                            strCallTimeEnd = lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                    }

                    objConsumeLocalTrafficDetailCallTP = new Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTP()
                    {
                        CALLDURATION = Convert.ToString(reader["callDuration"]),
                        CALLTIME = Convert.ToString(reader["calltime"]),
                        CALLTIMEFIN = Convert.ToString(strCallTimeEnd),
                        CALLDATEFIN = Convert.ToString(reader["calldatefin"]),
                        CALLNUMBER = Convert.ToString(reader["callnumber"]),
                        CALLDATE = Convert.ToString(reader["calldate"]),
                        CALLDESTINATION = Convert.ToString(reader["calldestination"]),
                        CALLDURATIONMIN = Convert.ToString(reader["calldurationmin"]),
                        CALLTOTAL = Convert.ToString(reader["calltotal"]),
                        CALLAMBITO = Convert.ToString(reader["ambito"])
                    };
                    listConsumeLocalTrafficDetailCallTP.Add(objConsumeLocalTrafficDetailCallTP);
                }
            });

            return listConsumeLocalTrafficDetailCallTP;
        }

        /// <summary>
        /// Método que obtiene una lista con el detalle de trafico local consumo en llamadas TM.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <returns>Devuelve listado de objetos ConsumeLocalTrafficDetailCallTM con información de tráfico local de consumo de llamadas.</returns>
        public static List<Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTM> GetDetailConsumeLocalTrafficCallTM(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall)
        {
            List<Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTM> listConsumeLocalTrafficDetailCallTM = new List<ConsumeLocalTrafficDetailCallTM>();
            Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTM objConsumeLocalTrafficDetailCallTM = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARDETTME, parameters, (IDataReader reader) =>
            {
                objConsumeLocalTrafficDetailCallTM = new Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTM();

                while (reader.Read())
                {
                    string strCallTimeEnd = "";
                    string strCallDuration = Convert.ToString(reader["callDuration"]);
                    string strCallTime = Convert.ToString(reader["calltime"]);
                    string strCallDateFin = Convert.ToString(reader["calldatefin"]);


                    if ((strCallDuration != null) && (strCallDuration.Length > 0) && (strCallTime != null) && (strCallTime.Length > 0))
                    {
                        int lIntCallDuration = Convert.ToInt(strCallDuration);
                        string[] strHora = Convert.ToString(strCallTime).Split(char.Parse(":"));

                        int lIntHora = Convert.ToInt(strHora[0]);
                        int lIntMin = Convert.ToInt(strHora[1]);
                        int lIntSeg = Convert.ToInt(strHora[2]);
                        int lIntcSeg = (lIntSeg + lIntCallDuration) / Claro.Constants.NumberSixty;
                        int lIntrSeg = (lIntSeg + lIntCallDuration) % Claro.Constants.NumberSixty;
                        lIntSeg = lIntrSeg;
                        int lIntcMin = (lIntMin + lIntcSeg) / Claro.Constants.NumberSixty;
                        int lIntrMin = (lIntMin + lIntcSeg) % Claro.Constants.NumberSixty;
                        lIntMin = lIntrMin;

                        if ((lIntHora + lIntcMin) > 24)
                        {
                            lIntHora = lIntHora + lIntcMin - Claro.Constants.NumberTwentyFour;
                            strCallTimeEnd = strCallDateFin + lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                        else
                        {
                            lIntHora = lIntHora + lIntcMin;
                            strCallTimeEnd = lIntHora + ":" + lIntMin + ":" + lIntSeg;
                        }
                    }

                    objConsumeLocalTrafficDetailCallTM = new Entity.Dashboard.Postpaid.ConsumeLocalTrafficDetailCallTM()
                    {
                        CALLDURATION = Convert.ToString(reader["callDuration"]),
                        CALLTIME = Convert.ToString(reader["calltime"]),
                        CALLTIMEFIN = Convert.ToString(strCallTimeEnd),
                        CALLDATEFIN = Convert.ToString(reader["calldatefin"]),
                        CALLNUMBER = Convert.ToString(reader["callnumber"]),
                        CALLDATE = Convert.ToString(reader["calldate"]),
                        CALLDESTINATION = Convert.ToString(reader["calldestination"]),
                        CALLDURATIONMIN = Convert.ToString(reader["calldurationmin"]),
                        CALLTOTAL = Convert.ToString(reader["calltotal"]),
                        CALLAMBITO = Convert.ToString(reader["ambito"])
                    };
                    listConsumeLocalTrafficDetailCallTM.Add(objConsumeLocalTrafficDetailCallTM);
                }
            });

            return listConsumeLocalTrafficDetailCallTM;
        }

        /// <summary>
        /// Método que devuelve una cadena con la cantidad de consumo de tráfico local de llamadas TP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <returns>Devuelve cadena con la cantidad de consumo.</returns>
        public static string GetConsumeQuantityLocalTrafficCallTP(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall)
        {
            string strConsumeQuantityTP = "";
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARCANTPE, parameters, (IDataReader reader) =>
            {
                reader.Read();
                strConsumeQuantityTP = Convert.ToString(reader["AddUsage"]);

            });

            return strConsumeQuantityTP;
        }

        /// <summary>
        /// Método que devuelve una cadena con la cantidad de consumo de tráfico local de llamadas TM.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strInvoiceNumber">Número de factura</param>
        /// <param name="strMsisdn">Valor de msisdn</param>
        /// <param name="strTypeCall">Tipo de llamada</param>
        /// <returns>devuelve cadena con la cantidad de consumo.</returns>
        public static string GetConsumeQuantityLocalTrafficCallTM(string strIdSession, string strTransaction, string strInvoiceNumber, string strMsisdn, string strTypeCall)
        {
            string strConsumeQuantityTM = "";

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_MSISDN", DbType.String,20, ParameterDirection.Input, strMsisdn), 
                new DbParameter("K_TYPECALL", DbType.String,1000, ParameterDirection.Input, strTypeCall), 
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARCANTME, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    strConsumeQuantityTM = Convert.ToString(reader["AddUsage"]);
                    break;
                }
            });

            return strConsumeQuantityTM;
        }

        /// <summary>
        /// Método que consulta y retorna lista de clientes VIP
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhone">Número de Teléfono</param>
        /// <param name="MsgText">Mensaje de Respuesta</param>
        /// <param name="FlagQuery">TFlag de consulta</param>
        /// <returns>devuelve lista de Cliente VIP</returns>
        public static List<Entity.Dashboard.Postpaid.ClientVIP> QueryClientVIP(string strIdSession, string strTransaction, string strPhone, ref string MsgText, ref string FlagQuery)
        {
            List<Entity.Dashboard.Postpaid.ClientVIP> listClientVIP = null;
            Entity.Dashboard.Postpaid.ClientVIP objClientVIP;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_MSISDN", DbType.String, 500, ParameterDirection.Input, strPhone),
                new DbParameter("P_FLAG_CONSULTA", DbType.String, 500, ParameterDirection.Output), 
                new DbParameter("p_MSG_TEXT", DbType.String, 500, ParameterDirection.Output), 
                new DbParameter("P_LIST", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_LISTA_USUARIO_VIP, parameters, (IDataReader reader) =>
            {
                objClientVIP = new Claro.SIACU.Entity.Dashboard.Postpaid.ClientVIP();
                listClientVIP = new List<Entity.Dashboard.Postpaid.ClientVIP>();

                while (reader.Read())
                {
                    objClientVIP = new Claro.SIACU.Entity.Dashboard.Postpaid.ClientVIP()
                    {
                        MSISDN = Convert.ToString(reader["MSISDN"]),
                        ESTADO = Convert.ToString(reader["ESTADO"]),
                        FECHA_REGISTRO = Convert.ToDate(reader["FECHA_REGISTRO"]),
                        FECHA_BAJA = Convert.ToDate(reader["FECHA_BAJA"]),
                        NOTAS = Convert.ToString(reader["NOTAS"])
                    };
                    listClientVIP.Add(objClientVIP);
                }
            });

            FlagQuery = Convert.ToString(parameters[1].Value);
            MsgText = Convert.ToString(parameters[2].Value);

            return listClientVIP;
        }

        /// <summary>
        /// Método que busca cuotas Roaming
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTypeClient">Tipo de Cliente</param>
        /// <param name="CodAplication">Codigo Aplicación</param>
        /// <param name="CodExecution">Código Ejecución</param>
        /// <param name="strZonaExc">Zona Exc</param>
        /// <param name="strZonaRM">Zona RM</param>
        /// <param name="CodError">Codigo Error</param>
        /// <param name="DesError">Descripción Error</param>
        /// <returns>devuelve Lista de cuotas Roaming</returns>
        public static List<Entity.Dashboard.Postpaid.ShareRoaming> GetShareRoaming(string strIdSession, string strTransaction, string strTypeClient, int CodAplication, int CodExecution, ref string strZonaExc, ref string strZonaRM, ref int CodError, ref string DesError)
        {
            List<Entity.Dashboard.Postpaid.ShareRoaming> listShareRoaming = null;
            Entity.Dashboard.Postpaid.ShareRoaming objShareRoaming;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPO_CLIENTE", DbType.String, 20, ParameterDirection.Input, strTypeClient),
                new DbParameter("P_COD_APLIC", DbType.Int32, 22, ParameterDirection.Input, CodAplication), 
                new DbParameter("P_COD_EJEC", DbType.Int32, 22, ParameterDirection.Input, CodExecution), 
                new DbParameter("P_ZONA_EXC", DbType.String, 200, ParameterDirection.Output),
                new DbParameter("P_ZONA_RM", DbType.String, 200, ParameterDirection.Output),
                new DbParameter("V_ERRNUM", DbType.Int32, 22, ParameterDirection.Output),
                new DbParameter("V_ERRMSJ", DbType.String, 1000, ParameterDirection.Output),
                new DbParameter("V_CURSOR", DbType.Object, ParameterDirection.Output)
            };


            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_BUSCA_CUOTAS_ROAMING, parameters, (IDataReader reader) =>
            {
                objShareRoaming = new Claro.SIACU.Entity.Dashboard.Postpaid.ShareRoaming();
                listShareRoaming = new List<Entity.Dashboard.Postpaid.ShareRoaming>();
                while (reader.Read())
                {
                    objShareRoaming = new Claro.SIACU.Entity.Dashboard.Postpaid.ShareRoaming()
                    {
                        CTRON_COD_APLIC = Convert.ToInt(reader["CTRON_COD_APLIC"]),
                        CTROV_TIPO_CLIENTE = Convert.ToString(reader["CTROV_TIPO_CLIENTE"]),
                        CTROV_ZONA_EXC = Convert.ToString(reader["CTROV_ZONA_EXC"]),
                        CTROV_ZONA_RM = Convert.ToString(reader["CTROV_ZONA_RM"]),
                        CTROV_USU_REGISTRO = Convert.ToString(reader["CTROV_USU_REGISTRO"]),
                        CTROV_USU_MODIFICO = Convert.ToString(reader["CTROV_USU_MODIFICO"]),
                        CTROD_FEC_REGISTRO = Convert.ToString(reader["CTROD_FEC_REGISTRO"]),
                        CTROD_FEC_ACTUALIZO = Convert.ToString(reader["CTROD_FEC_ACTUALIZO"]),
                        CTROV_PCRF_CE = Convert.ToString(reader["CTROV_PCRF_CE"])
                    };

                    if (CodExecution == 1)
                    {
                        objShareRoaming = new Claro.SIACU.Entity.Dashboard.Postpaid.ShareRoaming()
                        {
                            CTROV_PCRF = Convert.ToString(reader["CTROV_PCRF"]),
                            CTROV_ACT_CUOTA = Convert.ToString(reader["CTROV_ACT_CUOTA"]),
                            CTROV_ACT_MASIVO = Convert.ToString(reader["CTROV_ACT_MASIVO"]),
                            CTROV_ACT_CORP = Convert.ToString(reader["CTROV_ACT_CORP"]),
                            CTROV_ZNEXC_CAP = Convert.ToString(reader["CTROV_ZNEXC_CAP"]),
                            CTROV_ZNRM_CAP = Convert.ToString(reader["CTROV_ZNRM_CAP"])
                        };
                    }
                }
                listShareRoaming.Add(objShareRoaming);
            });

            strZonaExc = Convert.ToString(parameters[3].Value);
            strZonaRM = Convert.ToString(parameters[4].Value);
            CodError = Convert.ToInt(parameters[5].Value.ToString());
            DesError = Convert.ToString(parameters[6].Value);

            return listShareRoaming;
        }

        /// <summary>
        /// Método que obtiene verdadero o falso al consultar servicio comercial
        /// </summary>
        /// <param name="strContract">Contrato</param>
        /// <param name="strCodService">Codigo de Servicio Roaming</param>
        /// <param name="result">Numero de resultado</param>
        /// <param name="message">Mensaje de resultado</param>
        /// <returns>Devuelve valor booleano.</returns>
        public static bool GetServicesCommercial(string strIdSession, string strTransaction, string strContract, string strCodService, ref int result, ref string message)
        {
            string state;
            bool boolOut = false;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.Int32, 22, ParameterDirection.Input, strContract),
                new DbParameter("p_co_ser", DbType.Int32, 22, ParameterDirection.Input, strCodService),
                new DbParameter("v_estado", DbType.String, 10, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.Int32, 22, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String, 1000, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_CONSULTA_SERVICIO_COMERCIAL, parameters);
            state = Convert.ToString(parameters[2].Value.ToString());
            result = Convert.ToInt(parameters[3].Value.ToString());
            message = Convert.ToString(parameters[4].Value.ToString());

            if (state == "A")
            {
                boolOut = true;
            }

            return boolOut;
        }

        public static QueryOAC GetCustomerInformation(string strIdSession, string strTransaction, double IdCaso)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_idcaso", DbType.Double, ParameterDirection.Input, IdCaso),
                 new DbParameter("k_cur_caso", DbType.Object, ParameterDirection.Output)
            };

            QueryOAC objQueryOAC = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_DATCASO, parameters, (IDataReader reader) =>
            {
                reader.Read();

                objQueryOAC = new QueryOAC()
                {
                    IdClarify = Convert.ToString(reader["IDCLARIFY"]),
                    CodigoCliente = Convert.ToString(reader["CODCLIENTE"]),
                    ContactoCliente = Convert.ToString(reader["CONTACTO"]),
                    Recurrencia = Convert.ToString(reader["RECURRENCIA"]),
                    NombreCliente = Convert.ToString(reader["NOMBCLIENTE"])
                };
            });

            return objQueryOAC;
        }

        /// <summary>
        /// Permite obtener el listado de stocks en almacen
        /// </summary>
        /// <param name="vEjercicio"></param>
        /// <param name="vNumMaterial"></param>
        /// <returns>ArrayList</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>JAM - TSF</CreadoPor></item>
        /// <item><FecCrea>2007-07-17</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>07/10/2015.</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        public static List<Entity.Dashboard.Postpaid.StockWarehouse> GetStockWarehouse(string strSerie, string strDescripcion, string strRegion)
        {
            List<StockWarehouse> ListStockWarehouse = new List<StockWarehouse>();
            int i;
            DataSet dsData = SAPMethods.GetStockWarehouse(strSerie, strDescripcion, strRegion);
            for (i = 0; i < dsData.Tables[0].Rows.Count; i++)
            {
                StockWarehouse item = new StockWarehouse();
                item.Codigo = SAPMethods.GetmATNRField();
                item.Descripcion = SAPMethods.GetmAKTXField();
                item.Warehouse = SAPMethods.GetlGOBEField();
                item.Quantity = SAPMethods.GetlABSTField();
                ListStockWarehouse.Add(item);
            }
            return ListStockWarehouse;

        }

        public static List<Materials> GetMaterials(string strIdSession, string strTransaction)
        {
            List<Materials> ListMaterials = new List<Materials>();
            POSTPAID_SAP.ZEIA_PACKS_HANDSETS_OCSResponse objResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_SAP.ZEIA_PACKS_HANDSETS_OCSResponse>
            (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_SAP, () =>
            { return Configuration.ServiceConfiguration.POSTPAID_SAP.listaMateriales(); });
            POSTPAID_SAP.ZST_PV_ARTICULO[] objDeposito = objResponse.ARTICULOS;

            if (objDeposito != null)
            {
                foreach (var item in objDeposito)
                {
                    ListMaterials.Add(new Materials()
                    {
                        Id = item.MATNR,
                        Description = item.MAKTX
                    });
                }
            }
            return ListMaterials;
        }

        public static List<QueryOAC> GetTrackingDetail(string strIdSession, string strTransaction, double IdCaso)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_idcaso", DbType.Double, ParameterDirection.Input, IdCaso),
                new DbParameter("k_cur_caso", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_TXTERR", DbType.String,200, ParameterDirection.Output),
                new DbParameter("P_CODERR", DbType.String,200, ParameterDirection.Output)
            };

            List<QueryOAC> listQueryOAC = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_HISTCASO_USU, parameters, (IDataReader reader) =>
            {
                listQueryOAC = new List<QueryOAC>();

                while (reader.Read())
                {
                    listQueryOAC.Add(new QueryOAC()
                    {
                        UbicacionCaso = Convert.ToString(reader["UBICACION"]),
                        NivelCaso = Convert.ToString(reader["NIVEL"]),
                        EstadoCaso = Convert.ToString(reader["ESTADO"]),
                        FechaCaso = Convert.ToString(reader["FECHA"]),
                        UsuarioRegistroCaso = Convert.ToString(reader["USUARIOREGISTRO"]),
                        PropietarioCaso = Convert.ToString(reader["PROPIETARIO"]),
                        ComentarioCaso = Convert.ToString(reader["COMENTARIO"]),
                        FechaCierreCaso = Convert.ToString(reader["FECHACIERRE"]),
                        ReaperturaCaso = Convert.ToString(reader["REAPERTURA"]),
                        UsuarioNombre = Convert.ToString(reader["NOM_USUARIO"])
                    });
                }
            });

            return listQueryOAC;
        }

        public static List<Reclosures> GetManagementOfClosures(string strIdSession, string strTransaction, double IdCaso)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_idcaso", DbType.Double, ParameterDirection.Input, IdCaso),
                new DbParameter("k_cur_datos", DbType.Object, ParameterDirection.Output)
            };

            List<Reclosures> listReclosures = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_CIERREXCASO, parameters, (IDataReader reader) =>
            {
                listReclosures = new List<Reclosures>();

                while (reader.Read())
                {
                    listReclosures.Add(new Reclosures()
                    {
                        CasoCierre = Convert.ToString(reader["CASOD_CIERRE"]),
                        Resolucion = Convert.ToString(reader["RESOLUCION"]),
                        Resultado = Convert.ToString(reader["RESULTADO"]),
                        Estado = Convert.ToString(reader["ESTADO"]),
                        FechaEnvioBanco = Convert.ToString(reader["FECHAENVIOBANCO"])
                    });
                }
            });

            return listReclosures;
        }

        public static List<Reclosures> GetReopenOfTheCase(string strIdSession, string strTransaction, double IdCaso)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_idcaso", DbType.Double, ParameterDirection.Input, IdCaso),
                new DbParameter("k_cur_datos", DbType.Object, ParameterDirection.Output)
            };

            List<Reclosures> listReclosures = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_REAPERTURASXCASO, parameters, (IDataReader reader) =>
            {
                listReclosures = new List<Reclosures>();

                while (reader.Read())
                {
                    listReclosures.Add(new Reclosures()
                    {
                        CasoReapertura = Convert.ToString(reader["CASOD_REAPERTURA"]),
                        ReapvFaseClarify = Convert.ToString(reader["REAPV_FASECLARIFY"]),
                        ReapdProcesoReapertura = Convert.ToString(reader["REAPD_PROCESOREAPERTURA"])
                    });
                }
            });

            return listReclosures;
        }

        public static List<QueryOAC> GetSubTableTracking(string strIdSession, string strTransaction, double IdCaso)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_idcaso", DbType.Double, ParameterDirection.Input, IdCaso),
                new DbParameter("k_cur_caso", DbType.Object, ParameterDirection.Output)
            };

            List<QueryOAC> listQueryOAC = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_HISTRPTCASO, parameters, (IDataReader reader) =>
            {
                listQueryOAC = new List<QueryOAC>();

                while (reader.Read())
                {
                    listQueryOAC.Add(new QueryOAC()
                    {
                        NumeroDocumentoCaso = Convert.ToString(reader["NUMERODOC"]),
                        IdReclamoCaso = Convert.ToString(reader["IDRECLAMO"]),
                        TipoDocumentoCaso = Convert.ToString(reader["TIPODOCUMENTO"]),
                        VariableReclamoCaso = Convert.ToString(reader["VAR_RECLA"]),
                        TipoTransaccionCaso = Convert.ToString(reader["TIPOTRANSACCION"]),
                        MontoCaso = Convert.ToString(reader["MONTO"]),
                        EstadoCaso = Convert.ToString(reader["ESTADO"])
                    });
                }
            });

            return listQueryOAC;
        }

        public static List<QueryOAC> GetThirdTableTrackingPrevious(string strIdSession, string strTransaction, double CaseClaimId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_idreclamo", DbType.Double, ParameterDirection.Input, CaseClaimId),
                new DbParameter("k_cur_casos", DbType.Object, ParameterDirection.Output)
            };

            List<QueryOAC> listQueryOAC = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_DATOSRECLAMO, parameters, (IDataReader reader) =>
            {
                listQueryOAC = new List<QueryOAC>();

                while (reader.Read())
                {
                    listQueryOAC.Add(new QueryOAC()
                    {
                        ReclamoOhxactCaso = Convert.ToString(reader["RECLN_OHXACT"])
                    });
                }
            });

            return listQueryOAC;
        }

        public static List<QueryOAC> GetThirdTableTracking(string strIdSession, string strTransaction, string IdCaso)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_ohxact", DbType.String, ParameterDirection.Input, IdCaso),
                new DbParameter("k_cur_datos", DbType.Object, ParameterDirection.Output)
            };

            List<QueryOAC> listQueryOAC = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_MGR, DbCommandConfiguration.SIACU_MGRSS_ENVIOMD, parameters, (IDataReader reader) =>
            {
                listQueryOAC = new List<QueryOAC>();

                while (reader.Read())
                {
                    listQueryOAC.Add(new QueryOAC()
                    {
                        CasoIdClarify = Convert.ToString(reader["CASOV_IDCLARIFY"]),
                        MemdvNombre = Convert.ToString(reader["MEMDV_NOMBRE"]),
                        EnvimdEnvio = Convert.ToString(reader["ENVID_ENVIO"]),
                        EnvimdMonto = Convert.ToString(reader["ENVIN_MONTO"])
                    });
                }
            });

            return listQueryOAC;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strContract"></param>
        /// <param name="strCodService"></param>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetCustomerCBIOS(string strIdSession, string strTransaction, string strEntityType, string strEntityValue, out Int64 intCustomerId, out string strMsisdn, out string strCustomerType, out string strCodeError, out string strMessageError)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPO_ENTIDAD", DbType.String, 22, ParameterDirection.Input, strEntityType),
                new DbParameter("P_VALOR_ENTIDAD", DbType.String, 22, ParameterDirection.Input, strEntityValue),
                new DbParameter("P_CUSTOMER_ID", DbType.Int64, ParameterDirection.Output), 
                new DbParameter("P_MSISDN", DbType.String, 63, ParameterDirection.Output),
                new DbParameter("P_TIPO_CLIENTE", DbType.String, 20, ParameterDirection.Output),
                new DbParameter("P_COD_ERROR", DbType.Int32, 22, ParameterDirection.Output),
                new DbParameter("P_MSG_ERROR", DbType.String, 1000, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_CLIENTE_CBIOS, parameters);

            intCustomerId = parameters[2].Value.ToString().Equals("null") ? Claro.Constants.NumberZero : Convert.ToInt64(parameters[2].Value.ToString());
            strMsisdn = Convert.ToString(parameters[3].Value);
            strCustomerType = Convert.ToString(parameters[4].Value);
            strCodeError = Convert.ToString(parameters[5].Value);
            strMessageError = Convert.ToString(parameters[6].Value);

            return strCodeError;
        }
        public static bool ConsultationValidityPackage4G(string strTelephone, Claro.Entity.AuditRequest audit,
          out int strFlagResult, out string strcodigoResponse, out string strMessage)
        {
            bool Respuesta = false;


            POSTPAID_VALIDATE4GLTE.validarLineaResponse objvalidarLineaResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_VALIDATE4GLTE.validarLineaResponse>

            (audit.UserName, audit.ApplicationName, Configuration.ServiceConfiguration.POSTPAID_VALIDATE4GLTE, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_VALIDATE4GLTE.validarLinea(new POSTPAID_VALIDATE4GLTE.validarLineaRequest()
                {

                    auditRequest = new POSTPAID_VALIDATE4GLTE.auditRequestType()
                    {
                        idTransaccion = audit.Transaction,
                        ipAplicacion = audit.IPAddress,
                        nombreAplicacion = audit.ApplicationName,
                        usuarioAplicacion = audit.UserName,

                    },
                    numero = strTelephone
                });
            });

            try
            {

                strcodigoResponse = objvalidarLineaResponse.auditResponse.codigoRespuesta;
                strMessage = objvalidarLineaResponse.auditResponse.mensajeRespuesta;

                if (objvalidarLineaResponse.flag)
                {
                    strFlagResult = Claro.Constants.NumberZero;
                    Respuesta = true;
                }
                else
                {
                    strFlagResult = Claro.Constants.NumberOne;
                    Respuesta = false;
                }

                Claro.Web.Logging.Info("Resultado - codigoRespuesta: {0}, mensajeRespuesta: {1}", strcodigoResponse, strMessage);
            }
            catch (Exception ex)
            {
                strFlagResult = 1;
                Claro.Web.Logging.Error(audit.UserName, audit.Transaction, ex.Message);
                throw;
            }


            return Respuesta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strAplicacion"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="strIPAplicacion"></param>
        /// <param name="strUsrAplicacion"></param>
        /// <param name="strCustomerId"></param>
        /// <param name="strContactNumberReference1"></param>
        /// <param name="strContactNumberReference2"></param>
        /// <param name="strContactCntCode"></param>
        /// <returns></returns>
        public static string GetNumberCustomerContact(string strIdSession, string strAplicacion, string strIdTransaccion, string strIPAplicacion, string strUsrAplicacion, string strCustomerId, out string strContactNumberReference1, out string strContactNumberReference2, out string strContactCntCode)
        {
            string strContactCustomerCode = string.Empty;
            strContactCntCode = string.Empty;
            strContactNumberReference1 = string.Empty;
            strContactNumberReference2 = string.Empty;

            POSTPAID_CUSTOMERCONTACT.consultarNumerosRequest objRequest = new POSTPAID_CUSTOMERCONTACT.consultarNumerosRequest();
            POSTPAID_CUSTOMERCONTACT.auditRequestType objAuditRequest = new POSTPAID_CUSTOMERCONTACT.auditRequestType();

            objAuditRequest.nombreAplicacion = strAplicacion;
            objAuditRequest.idTransaccion = strIdTransaccion;
            objAuditRequest.ipAplicacion = strIPAplicacion;
            objAuditRequest.usuarioAplicacion = strUsrAplicacion;

            POSTPAID_CUSTOMERCONTACT.auditResponseType objAuditResponse;

            objRequest.auditRequest = objAuditRequest;
            objRequest.costumerid = strCustomerId;

            POSTPAID_CUSTOMERCONTACT.consultarNumerosResponse objResponse =
            Claro.Web.Logging.ExecuteMethod<POSTPAID_CUSTOMERCONTACT.consultarNumerosResponse>
            (strIdSession, strIdTransaccion, Configuration.ServiceConfiguration.POSTPAID_CUSTOMERCONTACT, () =>
            {
                return Configuration.ServiceConfiguration.POSTPAID_CUSTOMERCONTACT.consultarNumeros(objRequest);
            });

            objAuditResponse = objResponse.auditResponse;
            strContactCustomerCode = objResponse.codcli;
            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < objResponse.listaTelefonos.Length; i++)
                {

                    if (objResponse.listaTelefonos[i].idmedcom.Equals(KEY.AppSettings("NumerosTelefonoRef1")))
                    {

                        strContactNumberReference1 = objResponse.listaTelefonos[i].numcom;
                        strContactCntCode = objResponse.listaTelefonos[i].codcnt;
                    }

                    if (objResponse.listaTelefonos[i].idmedcom.Equals(KEY.AppSettings("NumerosTelefonoRef2")))
                    {

                        strContactNumberReference2 = objResponse.listaTelefonos[i].numcom;
                        strContactCntCode = objResponse.listaTelefonos[i].codcnt;
                    }
                }
            }

            return strContactCustomerCode;
        }


        public static List<Recharge> GetServicesContractZone(Recharge objRecharge, Claro.Entity.AuditRequest objAudit, out string strResponseCode, out string strMessage)
        {
            string gConstValor00 = "";

            try
            {
                gConstValor00 = KEY.AppSettings("gConstValor00");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }
            POSTPAID_PACKAGE_ROAMING.AuditRequestType objAuditRequest = new POSTPAID_PACKAGE_ROAMING.AuditRequestType()
            {
                idTransaccion = objAudit.Transaction,
                ipAplicacion = objAudit.IPAddress,
                nombreAplicacion = objAudit.ApplicationName,
                usuarioAplicacion = objAudit.UserName
            };
            POSTPAID_PACKAGE_ROAMING.serviciosContratoZonaRequest objserviciosContratoZonaRequest = new POSTPAID_PACKAGE_ROAMING.serviciosContratoZonaRequest()
            {
                auditRequest = objAuditRequest,
                numeroLinea = objRecharge.MSISDN
            };
            POSTPAID_PACKAGE_ROAMING.serviciosContratoZonaResponse objserviciosContratoZonaResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_PACKAGE_ROAMING.serviciosContratoZonaResponse>
            (objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.PAQUETE_ROAMING, () =>
            {
                return Configuration.ServiceConfiguration.PAQUETE_ROAMING.serviciosContratoZona(objserviciosContratoZonaRequest);
            });


            List<Recharge> listRecharge = null;
            strResponseCode = objserviciosContratoZonaResponse.auditResponse.codigoRespuesta;
            strMessage = objserviciosContratoZonaResponse.auditResponse.mensajeRespuesta;

            int numberZero = Convert.ToInt(gConstValor00);

            if (strResponseCode == gConstValor00 && (objserviciosContratoZonaResponse.listaServiciosZona != null && objserviciosContratoZonaResponse.listaServiciosZona.Length > numberZero))
            {
                listRecharge = new List<Recharge>();
                Parameter objParameter;
                string rMsgText = "";
                double consumo, saldo;
                objParameter = GetBSCSParameter("COMSUMPTION_LIMITS", objAudit.Session, objAudit.Transaction, out rMsgText).Find(x => x.VALOR.Contains("MEGASILIMITADO"));


                for (int i = numberZero; i < objserviciosContratoZonaResponse.listaServiciosZona.Length; i++)
                {
                    objRecharge = new Recharge();
                    objRecharge.TIPO_PAQUETE = Claro.SIACU.Constants.ConstRoaming + objserviciosContratoZonaResponse.listaServiciosZona[i].tipoPaquete;
                    objRecharge.BOLSA = objserviciosContratoZonaResponse.listaServiciosZona[i].descPaquete;
                    objRecharge.DESC_ZONA = objserviciosContratoZonaResponse.listaServiciosZona[i].descZona;
                    objRecharge.TIPO_UNIDAD = Claro.SIACU.Constants.ConstMegabytes;

                    consumo = double.Parse(objserviciosContratoZonaResponse.listaServiciosZona[i].consumoPaquete.Replace("MB", ""));
                    saldo = double.Parse(objserviciosContratoZonaResponse.listaServiciosZona[i].saldoPaquete.Replace("MB", ""));

                    if (consumo + saldo > double.Parse(objParameter.VALOR2))
                    {
                        objRecharge.UNIDAD = KEY.AppSettings("gConstkeyILIMITADO");
                        objRecharge.SALDO = KEY.AppSettings("gConstkeyILIMITADO");
                    }
                    else
                    {
                        double strTotalSaldo = consumo + saldo;
                        objRecharge.UNIDAD = strTotalSaldo + "MB";
                        objRecharge.SALDO = objserviciosContratoZonaResponse.listaServiciosZona[i].saldoPaquete;
                    }
                    objRecharge.CONSUMO = objserviciosContratoZonaResponse.listaServiciosZona[i].consumoPaquete;
                    objRecharge.FECHA_EXPIRACION = objserviciosContratoZonaResponse.listaServiciosZona[i].fechaExpiracion;


                    listRecharge.Add(objRecharge);
                }
            }

            return listRecharge;
        }


        public static List<IndicatorIGV> GetIGV(Claro.Entity.AuditRequest objAudit, out string strResponseCode, out string strMessage)
        {
            List<IndicatorIGV> lstIndicatorIGV = new List<IndicatorIGV>();
            string strxMessage = "";
            string strxResponseCode = "";

            try
            {

                POSTPAID_HFC_IGV.AuditRequestType objAuditRequest = new POSTPAID_HFC_IGV.AuditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = objAudit.ApplicationName,
                    usuarioAplicacion = objAudit.UserName
                };
                POSTPAID_HFC_IGV.consultarIGVRequest objconsultarIGVRequest = new POSTPAID_HFC_IGV.consultarIGVRequest()
                {
                    auditoria = objAuditRequest,
                };
                POSTPAID_HFC_IGV.consultarIGVResponse objconsultarIGVResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_HFC_IGV.consultarIGVResponse>
               (objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.IGV_HFC, () =>
               {
                   return Configuration.ServiceConfiguration.IGV_HFC.consultarIGV(objconsultarIGVRequest);
               });
                strxMessage = objconsultarIGVResponse.defaultServiceResponse.mensaje;
                strxResponseCode = objconsultarIGVResponse.defaultServiceResponse.idRespuesta;

                if (Convert.ToInt(strxResponseCode) == 0)
                {
                    int cantidad = objconsultarIGVResponse.listaIGVS.Length;

                    for (int i = 0; i < cantidad; i++)
                    {
                        IndicatorIGV objIndicatorIGV = new IndicatorIGV();

                        objIndicatorIGV.ID_INDICADOR = objconsultarIGVResponse.listaIGVS[i].imputId;
                        objIndicatorIGV.INDICADOR = objconsultarIGVResponse.listaIGVS[i].impuvDes;
                        objIndicatorIGV.PORCENTAJE = objconsultarIGVResponse.listaIGVS[i].igv;
                        objIndicatorIGV.PORCENTAJED = objconsultarIGVResponse.listaIGVS[i].igvD;
                        objIndicatorIGV.TIPO_DOCUMENTO = objconsultarIGVResponse.listaIGVS[i].impunTipDoc;
                        objIndicatorIGV.FEC_CREATE = DateTime.Parse(objconsultarIGVResponse.listaIGVS[i].impudFecRegistro);
                        objIndicatorIGV.FEC_INI_VIGENCIA = DateTime.Parse(objconsultarIGVResponse.listaIGVS[i].impudFecIniVigencia);
                        objIndicatorIGV.FEC_FIN_VIGENCIA = DateTime.Parse(objconsultarIGVResponse.listaIGVS[i].impudFecFinVigencia);
                        lstIndicatorIGV.Add(objIndicatorIGV);

                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                strMessage = strxMessage;
                strResponseCode = strxResponseCode;
            }
            strMessage = strxMessage;
            strResponseCode = strxResponseCode;
            return lstIndicatorIGV;

        }


        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.BagBalanceCBIOS> GetBalanceCBIOS(string CustomerId, string Host, string Telephone, Claro.Entity.AuditRequest objAudit, out string strResponseCode, out string strMessage)
        {

            string strxMessage = "";
            string strxResponseCode = "";
            string balance = ConfigurationManager.AppSettings("balance").ToString();
            string consumption = ConfigurationManager.AppSettings("consumption").ToString();
            string lineLimit = ConfigurationManager.AppSettings("lineLimit").ToString();
            string lineConsumption = ConfigurationManager.AppSettings("lineConsumption").ToString();
            string SharedProduct = ConfigurationManager.AppSettings("SharedProduct").ToString();
            string type = ConfigurationManager.AppSettings("type").ToString();

            List<Claro.SIACU.Entity.Dashboard.Postpaid.BagBalanceCBIOS> lstBagBalanceCBIOS = new List<Claro.SIACU.Entity.Dashboard.Postpaid.BagBalanceCBIOS>();
            try
            {

                POSTPAID_CUSTOMER_MG_FS.CustomerOrder oType = new POSTPAID_CUSTOMER_MG_FS.CustomerOrder();
                POSTPAID_CUSTOMER_MG_FS.URL url = new POSTPAID_CUSTOMER_MG_FS.URL();
                POSTPAID_CUSTOMER_MG_FS.Customer customer = new POSTPAID_CUSTOMER_MG_FS.Customer();
                POSTPAID_CUSTOMER_MG_FS.PartyRole partyRole = new POSTPAID_CUSTOMER_MG_FS.PartyRole();
                POSTPAID_CUSTOMER_MG_FS.ProductOffering[] productOffering = new POSTPAID_CUSTOMER_MG_FS.ProductOffering[1];
                POSTPAID_CUSTOMER_MG_FS.Product[] product = new POSTPAID_CUSTOMER_MG_FS.Product[1];
                POSTPAID_CUSTOMER_MG_FS.GetCustomerConsumptionResponseType objGetCustomerConsumptionResponseType = null;


                url.host = Host;
                product[0] = new POSTPAID_CUSTOMER_MG_FS.Product();
                product[0].externalIDs = Telephone;
                productOffering[0] = new POSTPAID_CUSTOMER_MG_FS.ProductOffering();
                productOffering[0]._product = product;
                partyRole._productOffering = productOffering;
                oType.id = CustomerId;
                oType._url = url;
                oType._customer = customer;
                oType._customer._partyRole = partyRole;



                POSTPAID_CUSTOMER_MG_FS.GetCustomerConsumptionRequestType objGetCustomerConsumptionRequestType = new POSTPAID_CUSTOMER_MG_FS.GetCustomerConsumptionRequestType()
               {
                   _customerOrder = oType
               };

                objGetCustomerConsumptionResponseType = Claro.Web.Logging.ExecuteMethod<POSTPAID_CUSTOMER_MG_FS.GetCustomerConsumptionResponseType>
               (objAudit.Session, objAudit.Transaction, Configuration.WebServiceConfiguration.POSTPAID_CUSTOMER_MG_FS, () =>
               {
                   return Configuration.WebServiceConfiguration.POSTPAID_CUSTOMER_MG_FS.GetCustomerConsumption(objGetCustomerConsumptionRequestType);
               });


                if (objGetCustomerConsumptionResponseType != null)
                {
                    strxResponseCode = Claro.Constants.NumberOneString;
                    string name = "";

                    Claro.SIACU.Entity.Dashboard.Postpaid.BagBalanceCBIOS itemBolsaSaldoCBIOS = null;
                    for (int i = 0; i < objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering.Length; i++)
                    {
                        itemBolsaSaldoCBIOS = new Claro.SIACU.Entity.Dashboard.Postpaid.BagBalanceCBIOS();
                        itemBolsaSaldoCBIOS.CodigoConsumo = objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i].name.ToString();
                        for (int x = 0; x < objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product.Length; x++)
                        {

                            itemBolsaSaldoCBIOS.ChargeTypeCode = objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x].chargeTypeCode;

                            itemBolsaSaldoCBIOS.Bolsa = objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x].popId;
                            for (int y = 0; y < objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue.Length; y++)
                            {
                                name = objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].name;

                                if (name == balance)
                                {

                                    if (objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value == "")
                                    {
                                        itemBolsaSaldoCBIOS.Saldo = 0;
                                    }
                                    else
                                    {
                                        itemBolsaSaldoCBIOS.Saldo = Convert.ToDecimal(objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value);
                                    }

                                }
                                if (name == consumption)
                                {
                                    itemBolsaSaldoCBIOS.Consumo = Convert.ToDecimal(objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value);
                                }

                                if (name == SharedProduct)
                                {
                                    itemBolsaSaldoCBIOS.SharedProduct = objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value;
                                }

                                if (name == lineLimit)
                                {
                                    itemBolsaSaldoCBIOS.lineLimit = Convert.ToDecimal(objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value);
                                }

                                if (name == lineConsumption)
                                {
                                    itemBolsaSaldoCBIOS.lineConsumption = Convert.ToDecimal(objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value);
                                }


                                if (name == type)
                                {
                                    itemBolsaSaldoCBIOS.Type = Convert.ToString(objGetCustomerConsumptionResponseType._customerOrder._customer._partyRole._productOffering[i]._product[x]._productCharacteristicsValue[y].value);
                                }


                            }
                            lstBagBalanceCBIOS.Add(itemBolsaSaldoCBIOS);
                        }
                    }

                }
                else
                {
                    strxResponseCode = Claro.Constants.NumberZeroString;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                strMessage = strxMessage;
                strResponseCode = strxResponseCode;
            }
            strMessage = strxMessage;
            strResponseCode = strxResponseCode;
            return lstBagBalanceCBIOS;

        }
        /// <summary>
        /// Visualizar la factura enviando el CodIdBase.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns>Devuelve objeto FileDefaultImpersonationResponse con archivo de suplantación predeterminada.</returns>
        public static Entity.File.GlobalDocument GetFileAjusteV3(string strIdSession, string strTransaction, string strFileName, string strNumeroRecibo, string strDateIssue, string stridReciboOnBase)
        {
            int posicion = strNumeroRecibo.IndexOf("-");
            string strPrefijo = strNumeroRecibo.Remove(posicion);
            string NUMERORECIBO = strNumeroRecibo.Substring(posicion + 1, strNumeroRecibo.Length - posicion - 1);

            Entity.File.GlobalDocument objGlobalDoc = new Entity.File.GlobalDocument();
            try
            {
                POSTPAID_DOCUMENTV3.AuditRequestType objAuditRequest = new POSTPAID_DOCUMENTV3.AuditRequestType()
                {
                    idTransaccional = strTransaction,
                    ipOrigen = "",
                    idAplicacionSolicitante = "",
                    usuarioLogueado = "",
                    fecha = DateTime.Now,
                    hora = DateTime.Now,
                    nombrePC = "",
                    usuarioVinculado = "",
                    usuarioDueno = "",
                };
                POSTPAID_DOCUMENTV3.obtenerDocumentoFisicoRequest objDocumentoFisicoRequest = new POSTPAID_DOCUMENTV3.obtenerDocumentoFisicoRequest()
                {
                    auditoria = objAuditRequest,
                    numeroRecibo = NUMERORECIBO,
                    periodoRecibo = strDateIssue,
                    categoria = Claro.Constants.NumberOneString,
                    idProductoServicio = Claro.Constants.NumberZeroString,
                    idReciboProducto = Claro.Constants.NumberZeroString,
                    tipoReciboDocumentoPago = strPrefijo,
                    idReciboOnBase = stridReciboOnBase
                };

                POSTPAID_DOCUMENTV3.obtenerDocumentoFisicoResponse objDocumentoFisico =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_DOCUMENTV3.obtenerDocumentoFisicoResponse>
                 (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_DOCUMENTV3, () =>
                 {
                     return Configuration.ServiceConfiguration.POSTPAID_DOCUMENTV3.obtenerDocumentoFisico(objDocumentoFisicoRequest);
                 });

                objGlobalDoc.CodigoError = objDocumentoFisico.defaultServiceResponse.idRespuesta;
                objGlobalDoc.MensajeError = objDocumentoFisico.defaultServiceResponse.mensaje;
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("GetFileAjuste: IdRespuesta- {0} , MsjError - {1}", objGlobalDoc.CodigoError, objGlobalDoc.MensajeError));

                if (objGlobalDoc.CodigoError.Equals(Claro.Constants.NumberZeroString))
                {
                    objGlobalDoc.Documento = objDocumentoFisico.documento;
                }
                else
                {
                    objGlobalDoc.Documento = null;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }

            return objGlobalDoc;
        }

        /// <summary>
        /// Método que obtiene archivo con suplantacion predeterminada.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns>Devuelve objeto FileDefaultImpersonationResponse con archivo de suplantación predeterminada.</returns>
        public static Entity.File.GlobalDocument GetFileAjuste(string strIdSession, string strTransaction, string strFileName, string strDateIssue)
        {
            Entity.File.GlobalDocument objGlobalDoc = new Entity.File.GlobalDocument();
            strFileName = System.IO.Path.GetFileNameWithoutExtension(strFileName);
            try
            {
                POSTPAID_DOCUMENT.AuditRequestType objAuditRequest = new POSTPAID_DOCUMENT.AuditRequestType()
                {
                    idTransaccional = strTransaction,
                    ipOrigen = "",
                    idAplicacionSolicitante = "",
                    usuarioLogueado = "",
                    fecha = DateTime.Now,
                    hora = DateTime.Now,
                    nombrePC = "",
                    usuarioVinculado = "",
                    usuarioDueno = "",
                };
                POSTPAID_DOCUMENT.obtenerDocumentoFisicoRequest objDocumentoFisicoRequest = new POSTPAID_DOCUMENT.obtenerDocumentoFisicoRequest()
              {
                  auditoria = objAuditRequest,
                  numeroRecibo = strFileName,
                  periodoRecibo = strDateIssue,
                  categoria = Claro.Constants.NumberOneString,
                  idProductoServicio = Claro.Constants.NumberZeroString,
                  idReciboProducto = Claro.Constants.NumberZeroString,
                  tipoReciboDocumentoPago = Claro.Constants.NumberZeroString,

              };
                POSTPAID_DOCUMENT.obtenerDocumentoFisicoResponse objDocumentoFisico =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_DOCUMENT.obtenerDocumentoFisicoResponse>
                 (strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_DOCUMENT, () =>
                 {
                     return Configuration.ServiceConfiguration.POSTPAID_DOCUMENT.obtenerDocumentoFisico(objDocumentoFisicoRequest);
                 });
                objGlobalDoc.CodigoError = objDocumentoFisico.defaultServiceResponse.idRespuesta;
                objGlobalDoc.MensajeError = objDocumentoFisico.defaultServiceResponse.mensaje;
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("GetFileAjuste: IdRespuesta- {0} , MsjError - {1}", objGlobalDoc.CodigoError, objGlobalDoc.MensajeError));

                if (objGlobalDoc.CodigoError.Equals(Claro.Constants.NumberZeroString))
                {
                    objGlobalDoc.Documento = objDocumentoFisico.documento;
                }
                else
                {
                    objGlobalDoc.Documento = null;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }

            return objGlobalDoc;
        }


        public static List<Recharge> GetBalanceM2M(Recharge objRecharge, Claro.Entity.AuditRequest objAudit, out string strResponseCode, out string strMessage)
        {
            List<Recharge> lstRecharge = new List<Recharge>();
            string strxMessage = "";
            string strxResponseCode = "";

            try
            {

                POSTPAID_M2M.auditRequestType objAuditRequest = new POSTPAID_M2M.auditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = ConfigurationManager.AppSettings("gConstAplicacionIDBroker"),
                    usuarioAplicacion = objAudit.UserName
                };
                POSTPAID_M2M.consultarSaldoM2MRequest objconsultarSaldoM2MRequest = new POSTPAID_M2M.consultarSaldoM2MRequest()
                {
                    auditRequest = objAuditRequest,
                    numeroLinea = objRecharge.MSISDN
                };
                POSTPAID_M2M.consultarSaldoM2MResponse objconsultarSaldoM2MResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_M2M.consultarSaldoM2MResponse>
               (objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.PAQUETE_M2M, () =>
               {
                   return Configuration.ServiceConfiguration.PAQUETE_M2M.consultarSaldoM2M(objconsultarSaldoM2MRequest);
               });


                strxMessage = objconsultarSaldoM2MResponse.auditResponse.mensajeRespuesta;
                strxResponseCode = objconsultarSaldoM2MResponse.auditResponse.codigoRespuesta;


                if (strxResponseCode == Claro.Constants.NumberZeroString && objconsultarSaldoM2MResponse.listaServiciosZona != null && objconsultarSaldoM2MResponse.listaServiciosZona.Length > Claro.Constants.NumberZero)
                {
                    for (int i = Claro.Constants.NumberZero; i < objconsultarSaldoM2MResponse.listaServiciosZona.Length; i++)
                    {
                        Recharge objRecharges = new Recharge();

                        objRecharges.BOLSA = objconsultarSaldoM2MResponse.listaServiciosZona[i].nombreServicio;
                        objRecharges.NOMBRE = objconsultarSaldoM2MResponse.listaServiciosZona[i].nombreEtiqueta;
                        objRecharges.CONSUMO = objconsultarSaldoM2MResponse.listaServiciosZona[i].saldoConsumido;
                        objRecharges.SALDO = objconsultarSaldoM2MResponse.listaServiciosZona[i].saldoRestante;
                        objRecharges.FECHA_EXPIRACION = objconsultarSaldoM2MResponse.listaServiciosZona[i].fechaExpiracion;

                        lstRecharge.Add(objRecharges);
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                strMessage = ConfigurationManager.AppSettings("gConstMensajeErrorM2M");
                strResponseCode = Claro.Constants.NumberFiveNegativeString;
            }
            strMessage = strxMessage;
            strResponseCode = strxResponseCode;
            return lstRecharge;

        }

        public static List<Parameter> GetBSCSParameter(string pid_parametro, string strIdSession, string strTransaction, out string rMsgText)
        {
            List<Parameter> lstParameter = new List<Parameter>();
            Parameter objParameter;
            string msg = "";
            try
            {
                DbParameter[] parameters = new DbParameter[] {
                new DbParameter("PI_CAMPO", DbType.String,250, ParameterDirection.Input, pid_parametro),
                new DbParameter("PO_CODE_RESULT", DbType.Int32,22, ParameterDirection.Output),
                new DbParameter("PO_MESSAGE_RESULT", DbType.String,250, ParameterDirection.Output),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };


                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SIACSS_PARAMETROSBSCS, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        objParameter = new Parameter()
                        {
                            DESCRIPCION = Claro.Convert.ToString(reader["DESCRIPCION"]),
                            VALOR = Claro.Convert.ToString(reader["VALOR1"]).ToString(),
                            VALOR2 = Claro.Convert.ToString(reader["VALOR2"]).ToString()
                        };

                        lstParameter.Add(objParameter);
                    }

                });
                msg = parameters[2].Value.ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rMsgText = msg;

            }
            return lstParameter;
        }


        public static Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse GetFlagAjuste(Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjsuteRequest objFlagAjsuteRequest)
        {
            Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse objFlagAjusteResponse = null;

            try
            {
                POSTPAID_DETAILAJUSTE.auditRequestType objAuditRequest = new POSTPAID_DETAILAJUSTE.auditRequestType()
                {
                    idTransaccion = objFlagAjsuteRequest.Audit.Transaction,
                    ipAplicacion = objFlagAjsuteRequest.Audit.IPAddress,
                    nombreAplicacion = objFlagAjsuteRequest.Audit.ApplicationName,
                    usuarioAplicacion = objFlagAjsuteRequest.Audit.UserName
                };
                POSTPAID_DETAILAJUSTE.validarAplicaAjusteDocRequest objDetalleDocAjusteResquest = new POSTPAID_DETAILAJUSTE.validarAplicaAjusteDocRequest()
                {
                    auditRequest = objAuditRequest,
                    numDocRef = objFlagAjsuteRequest.Document

                };
                POSTPAID_DETAILAJUSTE.validarAplicaAjusteDocResponse objResponseValidarAjsuteDoc =
                 Claro.Web.Logging.ExecuteMethod<POSTPAID_DETAILAJUSTE.validarAplicaAjusteDocResponse>
                 (objFlagAjsuteRequest.Audit.Session, objFlagAjsuteRequest.Audit.Transaction, Configuration.ServiceConfiguration.DETAIL_DOC_AJUSTE, () =>
                 {
                     return Configuration.ServiceConfiguration.DETAIL_DOC_AJUSTE.validarAplicaAjusteDoc(objDetalleDocAjusteResquest);
                 });
                if (objResponseValidarAjsuteDoc != null)
                {
                    objFlagAjusteResponse = new Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse();
                    objFlagAjusteResponse.AplicateAjsute = objResponseValidarAjsuteDoc.aplicaAjuste;
                    objFlagAjusteResponse.CodeResponse = objResponseValidarAjsuteDoc.auditResponse.codigoRespuesta;
                    objFlagAjusteResponse.MsjResponse = objResponseValidarAjsuteDoc.auditResponse.mensajeRespuesta;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFlagAjsuteRequest.Audit.Session, objFlagAjsuteRequest.Audit.Transaction, ex.Message);
                throw;
            }

            return objFlagAjusteResponse;
        }


        /// <summary>
        /// Método Obtiene documento de solicitud relationPlan
        /// </summary>
        /// <returns>Obtiene documento de solicitud relationPlan</returns>

        public static Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse GetDocumentSolicitude(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeRequests objDocumentSolicitudeRequests)
        {
            return RestService.PostInvoque<Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DOCUMENTO_SOLICITUD, objDocumentSolicitudeRequests.Audit, objDocumentSolicitudeRequests, true);
        }


        /// <summary>
        /// Método Obtiene documento de solicitud relationPlan
        /// </summary>
        /// <returns>Obtiene documento de solicitud relationPlan</returns>
        /// 
        public static List<Recharge> GetBalancePostpaidConsumerB2E(Claro.Entity.AuditRequest objAudit, string strMsidn, string strCustomerId, ref string strtCustomerCode, ref string claroPuntos, ref string strCodigoRespuesta, ref string strMensajeRespuesta)
        {
            List<Recharge> lstRecharge = new List<Recharge>();
            List<Parameter> lstParameter = new List<Parameter>();
            try
            {

                lstParameter.Add(
                    new Parameter()
                    {
                        PARAMETRO_ID = 0,
                        VALOR = "",
                        DESCRIPCION = "ListaBolsasPrincipales"

                    });
                lstParameter.Add(new Parameter()
                {
                    PARAMETRO_ID = 1,
                    VALOR = "",
                    DESCRIPCION = "ListaBolsasAdicionales"

                }
                   );
                POSTPREDATA.parametrosTypeObjetoOpcional[] lstparametrosTypeObjetoOpcional = new POSTPREDATA.parametrosTypeObjetoOpcional[lstParameter.Count];
                foreach (var item in lstParameter)
                {
                    POSTPREDATA.parametrosTypeObjetoOpcional objparametrosTypeObjetoOpcional = new POSTPREDATA.parametrosTypeObjetoOpcional { campo = item.DESCRIPCION, valor = item.VALOR };
                    lstparametrosTypeObjetoOpcional[Convert.ToInt(item.PARAMETRO_ID)] = objparametrosTypeObjetoOpcional;
                }


                POSTPREDATA.auditRequestType objAuditRequest = new POSTPREDATA.auditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = objAudit.ApplicationName,
                    usuarioAplicacion = objAudit.UserName

                };
                POSTPREDATA.ConsultarSaldosPostpagoRequest objConsultarSaldosPostpagoRequest = new POSTPREDATA.ConsultarSaldosPostpagoRequest()
                {
                    auditRequest = objAuditRequest,
                    codCliente = strtCustomerCode,
                    customerId = strCustomerId,
                    msisdn = strMsidn,
                    listaRequestOpcional = lstparametrosTypeObjetoOpcional
                };
                POSTPREDATA.ConsultaDatosPrePostWSService objConsultaDatosPrePostWSService = new POSTPREDATA.ConsultaDatosPrePostWSService()
                {
                    Url = KEY.AppSettings("gConstUrlConsultaSaldoWS"),
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Timeout = 100000

                };
                POSTPREDATA.ConsultarSaldosPostpagoResponse objConsultarSaldosPostpagoResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.ConsultarSaldosPostpagoResponse>
                   (objAudit.Session, objAudit.Transaction, () =>
                   {
                       return objConsultaDatosPrePostWSService.consultarSaldosPostpago(objConsultarSaldosPostpagoRequest);
                   });

                strCodigoRespuesta = objConsultarSaldosPostpagoResponse.auditResponse.codigoRespuesta;
                strMensajeRespuesta = objConsultarSaldosPostpagoResponse.auditResponse.mensajeRespuesta;
                claroPuntos = objConsultarSaldosPostpagoResponse.puntosTotales;

                if (strCodigoRespuesta == "0")
                {
                    foreach (POSTPREDATA.saldosPostpagoType item in objConsultarSaldosPostpagoResponse.listaSaldosPostpago)
                    {

                        string varConsultaBSCS, varIDBolsaAdicional;
                        varConsultaBSCS = KEY.AppSettings("strConsultaBSCS");
                        varIDBolsaAdicional = item.bolsaId;
                        if (varConsultaBSCS == "1")
                        {
                            if (varIDBolsaAdicional != KEY.AppSettings("strIDBolsaAdicional"))
                            {
                                item.consumo = "";
                                item.unidadesAsignadas = "";
                            }
                        }

                        lstRecharge.Add(new Recharge()
                        {
                            CONSUMO = item.consumo,
                            DESC_ZONA = item.destino,
                            FECHA_EXPIRACION = item.fechaExpiracion,
                            TIPO_PAQUETE = item.grupoBolsa,
                            BOLSA = item.nombreBolsa,
                            SALDO = item.saldoDisponible,
                            TIPO_UNIDAD = item.tipoUnidad,
                            UNIDAD = item.unidadesAsignadas,
                            ID_BOLSA = item.bolsaId
                        });
                    }
                    Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, new JavaScriptSerializer().Serialize(lstRecharge));
                }
            }
            catch (Exception ex)
            {
                strCodigoRespuesta = "-1";
                strMensajeRespuesta = ex.Message;
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }
            return lstRecharge;

        }

        //INICIATIVA 616 CONSULTA SALDO FIJA HFC
        public static List<Balance> GetBalanceFijaTobe(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Response.Response response = null;
            List<Balance> objListBalance = new List<Balance>();


            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Request.Request request = new Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Request.Request
                {
                    consultarSaldoRequest = new Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Request.ConsultarSaldoRequest
                    {
                        msisdn = strMsidn,
                        coIdPub = coIdPub,
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Common.ListaOpcional>()
                    }
                };

                Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, "");

                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetBalanceFijaTobe.Response.Response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.CONSULTAR_CONSUMO_SALDO_FIJA, objAudit, request, false);

                Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, "");

                if(response != null && 
                   response.consultarSaldoResponse != null &&
                   response.consultarSaldoResponse.responseAudit != null &&
                   response.consultarSaldoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                   response.consultarSaldoResponse.responseData != null &&
                   response.consultarSaldoResponse.responseData.listaBolsa != null)
                {
                    foreach (var objLista in response.consultarSaldoResponse.responseData.listaBolsa.Where(x => x.unificada != "0").ToList())
	                {
		                Balance objBalance = new Balance();
                        objBalance.WALLET_DESCRIPTION = objLista.detalleAsesor;
                        objBalance.CONSUMO = objLista.consumo;
                        objBalance.BALANCE = objLista.saldo;
                        objListBalance.Add(objBalance);
	                }                    
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            return objListBalance;
        }
        




        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.listaOpcional> GetBalancePostpaidConsumerB2ELegacyDegradation(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub)
        {
            Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, string.Format("Inicio GetBalancePostpaidConsumerB2ELegacyDegradation strMsidn : {0}, coIdPub:{1}  ", strMsidn, coIdPub));
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.listaOpcional> listOptional = null;
            Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.response response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request.Request request = new Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request.Request()
                {
                    consultarSaldoPostpagoRequest = new Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request.consultarSaldoPostpagoRequest()
                    {

                        msisdn = string.IsNullOrEmpty(strMsidn) ? string.Empty : strMsidn,
                        coIdPub = string.IsNullOrEmpty(coIdPub) ? string.Empty : coIdPub

                    },
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.CONSULTAR_CONSUMO_SALDO, objAudit, request, false);


                if (response.consultarSaldoPostpagoResponse != null &&
                    response.consultarSaldoPostpagoResponse.responseAudit != null &&
                    response.consultarSaldoPostpagoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.consultarSaldoPostpagoResponse.responseData != null &&
                    response.consultarSaldoPostpagoResponse.responseData.ListaOpcional != null)
                {
                    listOptional = new List<Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.listaOpcional>();
                    listOptional = response.consultarSaldoPostpagoResponse.responseData.ListaOpcional;
                }


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            return listOptional;

        }
        /// <remarks>GetBalancePostpaidConsumerB2ELegacy</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>07/01/2020.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>09/01/2020.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Listar el consumo de Bolsas Compartidas en TOBE</Mot></item></list>
        public static List<Recharge> GetBalancePostpaidConsumerB2ELegacy(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub, string strCustomerId, ref string strtCustomerCode, ref string claroPuntos, ref string strCodigoRespuesta, ref string strMensajeRespuesta, string FechaExpiracion)
        {
            Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, string.Format("Inicio GetBalancePostpaidConsumerB2ELegacy : {0} ", FechaExpiracion));

            int day = int.Parse(FechaExpiracion);
            DateTime fechaCompararCiclo = DateTime.Now;
            fechaCompararCiclo = DateTime.Parse(day.ToString() + "/" + fechaCompararCiclo.Month.ToString() + "/" + fechaCompararCiclo.Year.ToString() + " ");
            Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, string.Format("fechaCompararCiclo : {0} ", fechaCompararCiclo));
            if (day == 1)
            {
                fechaCompararCiclo = fechaCompararCiclo.AddMonths(1).AddDays(-1);
            }
            else
            {
                fechaCompararCiclo = fechaCompararCiclo.AddDays(-1);
            }
            Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, string.Format("fechaCompararCiclo un día antes: {0} ", fechaCompararCiclo));

            string fecha_exp = fechaCompararCiclo.ToString("dd/MM/yyyy");
            if (fechaCompararCiclo.Date < DateTime.Now.Date)
            {
                fecha_exp = fechaCompararCiclo.AddMonths(1).ToString("dd/MM/yyyy");
            }
            Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, string.Format("fecha_exp: {0} ", fecha_exp));


            List<Recharge> ListRecharge = new List<Recharge>();
            Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.response response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request.Request request = new Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request.Request()
                {
                    consultarSaldoPostpagoRequest = new Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Request.consultarSaldoPostpagoRequest()
                    {

                        msisdn = string.IsNullOrEmpty(strMsidn) ? string.Empty : strMsidn,
                        coIdPub = string.IsNullOrEmpty(coIdPub) ? string.Empty : coIdPub

                    },
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.CONSULTAR_CONSUMO_SALDO, objAudit, request, false);


                var contadorPlanes = 0;
                var contadorBonos = 0;
                var contadorPlanesDif0 = 3;

                if (response.consultarSaldoPostpagoResponse != null &&
                    response.consultarSaldoPostpagoResponse.responseAudit != null &&
                    response.consultarSaldoPostpagoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.consultarSaldoPostpagoResponse.responseData != null &&
                    response.consultarSaldoPostpagoResponse.responseData.listaBolsa != null)
                {
                    foreach (var item in response.consultarSaldoPostpagoResponse.responseData.listaBolsa)
                    {
                        {
                            Recharge ObjRecharge = new Recharge();
                            ObjRecharge.ZONA_DIF = item.zonaDif;
                            ObjRecharge.UNIFICADA = item.unificada;
                            ObjRecharge.NOMBRE = string.Empty;
                            ObjRecharge.FECHA_EXPIRACION = item.fechaExpiracion == "" ? fecha_exp + " 11:59:59 p.m." : item.fechaExpiracion;
                            ObjRecharge.BOLSA = item.detalleAsesor;
                            ObjRecharge.ID_BOLSA = item.id;

                            var index = item.grupobolsa.IndexOf("Paquete");
                            if (index >= 0)
                            {
                                item.grupobolsa = "Paquete";
                            }

                            ObjRecharge.TIPO_PAQUETE = item.grupobolsa;

                            switch (item.grupobolsa)
                            {
                                case "Plan":
                                    ObjRecharge.ID_ORDEN_BOLSA = "A";
                                    break;
                                case "Servicios Adicionales":
                                    ObjRecharge.ID_ORDEN_BOLSA = "B";
                                    break;
                                case "Paquete":
                                    ObjRecharge.ID_ORDEN_BOLSA = "C";
                                    break;
                            }

                            if (item.grupobolsa == "Plan" && item.unificada != "1")
                            {
                                if (item.unificada == "0")
                                {
                                    contadorPlanes++;
                                    ObjRecharge.INDEX_UNIFICADA = contadorPlanes.ToString();
                                }
                                else
                                {
                                    contadorPlanesDif0++;
                                    ObjRecharge.INDEX_UNIFICADA = contadorPlanesDif0.ToString();
                                }
                            }

                            if (item.grupobolsa == "Servicios Adicionales" && item.unificada != "1")
                            {
                                contadorBonos++;
                                ObjRecharge.INDEX_UNIFICADA = contadorBonos.ToString();
                            }

                            if (item.unidad == "DATOS" && item.unificada != "0" )//&& item.unificada != "6")
                            {
                                if (item.saldo.ToUpper().IndexOf("ILIMITADO") > -1)
                                {
                                    ObjRecharge.UNIDAD = item.total;
                                    ObjRecharge.SALDO = item.saldo;
                                    var conversionConsumo = Convert.ToDecimal(item.consumo);
                                    var conversionConsumoMayorA = Math.Round(((conversionConsumo / 1024)), 2);
                                    var conversionConsumoMenorA = conversionConsumo;
                                    //Campo Consumo
                                    if (conversionConsumo >= 1024)
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMayorA + " GB")).ToString();
                                        ObjRecharge.TIPO_UNIDAD = "GigaBytes";
                                    }
                                    else
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMenorA + " MB")).ToString();
                                        ObjRecharge.TIPO_UNIDAD = "MegaBytes";
                                    }
                                }
                                else
                                {
                                    var conversionTotal = Convert.ToDecimal(item.total);
                                    var conversionSaldo = Convert.ToDecimal(item.saldo);
                                    var conversionConsumo = Convert.ToDecimal(item.consumo);

                                    var conversionTotalMayorA = Math.Round(((conversionTotal / 1024)), 2);
                                    var conversionTotalMenorA = conversionTotal;

                                    var conversionSaldoMayorA = Math.Round(((conversionSaldo / 1024)), 2);
                                    var conversionSaldoMenorA = conversionSaldo;

                                    var conversionConsumoMayorA = Math.Round(((conversionConsumo / 1024)), 2);
                                    var conversionConsumoMenorA = conversionConsumo;

                                    if (conversionTotal >= 1024)
                                    {
                                        ObjRecharge.UNIDAD = (conversionTotalMayorA + " GB").ToString();
                                        ObjRecharge.TIPO_UNIDAD = "GigaBytes";
                                    }
                                    else
                                    {
                                        ObjRecharge.UNIDAD = (conversionTotalMenorA + " MB").ToString();
                                        ObjRecharge.TIPO_UNIDAD = "MegaBytes";
                                    }
                                    //Campo Saldo
                                    if (conversionSaldo >= 1024)
                                    {
                                        ObjRecharge.SALDO = (conversionSaldoMayorA + " GB").ToString();
                                    }
                                    else
                                    {
                                        ObjRecharge.SALDO = (conversionSaldoMenorA + " MB").ToString();
                                    }
                                    //Campo Consumo
                                    if (conversionConsumo >= 1024)
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMayorA + " GB")).ToString();
                                    }
                                    else
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMenorA + " MB")).ToString();
                                    }
                                }
                            }
                            else
                            {
                                ObjRecharge.CONSUMO = item.consumo;
                                ObjRecharge.UNIDAD = item.total;
                                ObjRecharge.SALDO = item.saldo;
                                switch (item.unidad)
                                {
                                    case "DATOS":
                                        ObjRecharge.TIPO_UNIDAD = "GigaBytes";
                                        break;
                                    case "VOZ":
                                        ObjRecharge.TIPO_UNIDAD = "Minutos con Segundos";
                                        break;
                                    case "SOLES":
                                        ObjRecharge.TIPO_UNIDAD = "Soles con Centimos";
                                        break;
                                    case "SMS":
                                        ObjRecharge.TIPO_UNIDAD = "Mensajes de texto";
                                        break;
                                    case "CLARO PUNTOS":
                                        ObjRecharge.TIPO_UNIDAD = "Puntos";
                                        break;
                                    case "MMS":
                                        ObjRecharge.TIPO_UNIDAD = "Mensajes Multimedia";
                                        break;
                                    default:
                                        ObjRecharge.TIPO_UNIDAD = "RPC";
                                        break;
                                }

                            }

                            ListRecharge.Add(ObjRecharge);

                        }

                    }
                    ListRecharge = ListRecharge.OrderBy(x => x.TIPO_UNIDAD).ToList();

                }


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
            }

            return ListRecharge;

        }
        //Adons

        public static List<Recharge> GetAddOnsToBe(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub, string strCustomerId, ref string strtCustomerCode, ref string claroPuntos, ref string strCodigoRespuesta, ref string strMensajeRespuesta, string FechaExpiracion)
        {
            Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, string.Format("Inicio GetAddOnsToBe : {0} ", FechaExpiracion));

            int day = int.Parse(FechaExpiracion);
            DateTime fechaCompararCiclo = DateTime.Now;
            fechaCompararCiclo = DateTime.Parse(day.ToString() + "/" + fechaCompararCiclo.Month.ToString() + "/" + fechaCompararCiclo.Year.ToString() + " ");
            fechaCompararCiclo = fechaCompararCiclo.AddDays(-1);

            string fecha_exp = fechaCompararCiclo.ToString("dd/MM/yyyy");
            if (fechaCompararCiclo.Date < DateTime.Now.Date)
            {
                fecha_exp = fechaCompararCiclo.AddMonths(1).ToString("dd/MM/yyyy");
            }

            var contadorAddOns = 0;
            List<Recharge> ListRecharge = new List<Recharge>();
            Entity.Dashboard.Postpaid.Legacy.GetAddOnsToBe.addOnsResponse response = null;

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetAddOnsToBe.addOnsRequest request = new Entity.Dashboard.Postpaid.Legacy.GetAddOnsToBe.addOnsRequest()
                {
                    consultarBolsasAddonRequest = new Entity.Dashboard.Postpaid.Legacy.GetAddOnsToBe.consultarBolsasAddonRequest()
                    {

                        msisdn = string.IsNullOrEmpty(strMsidn) ? string.Empty : strMsidn,
                        estado = String.Empty,
                    },
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetAddOnsToBe.addOnsResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.CONSULTAR_ADDONS, objAudit, request, false);




                if (response.consultarBolsasAddonResponse.responseData != null &&
                    response.consultarBolsasAddonResponse.responseAudit != null &&
                    response.consultarBolsasAddonResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.consultarBolsasAddonResponse.responseData != null &&
                    response.consultarBolsasAddonResponse.responseData.listaBolsa != null)
                {
                    foreach (var item in response.consultarBolsasAddonResponse.responseData.listaBolsa)
                    {
                        {
                            if(item.saldo!="0.00" || item.grupobolsa!="Paquetes"){

                            
                            Recharge ObjRecharge = new Recharge();
                            ObjRecharge.ZONA_DIF = item.zonaDif;
                            ObjRecharge.UNIFICADA = item.unificada;
                            ObjRecharge.NOMBRE = string.Empty;
                            ObjRecharge.FECHA_EXPIRACION = item.fechaExpiracion == "" ? fecha_exp + " 11:59:59 p.m." : item.fechaExpiracion;

                            var index = item.grupobolsa.IndexOf("Paquete");
                            if (index >= 0)
                            {
                                item.grupobolsa = "Paquete";
                            }

                            ObjRecharge.TIPO_PAQUETE = item.grupobolsa;
                            ObjRecharge.ID_BOLSA = item.id;


                            if (item.fechaExpiracion == "")
                            {

                                if (fechaCompararCiclo < DateTime.Now)
                                {
                                    ObjRecharge.BOLSA = item.nombreComercialCliente + " - Expirado";
                                }
                                else
                                {
                                        if (item.estado == "0" || item.estado == "1")
                                    {
                                        ObjRecharge.BOLSA = item.nombreComercialCliente + " - Activo";
                                    }
                                }
                            }
                            else
                            {
                                var fechaCompararServicio = DateTime.Parse(item.fechaExpiracion);
                                if (fechaCompararServicio < DateTime.Now)
                                {
                                    ObjRecharge.BOLSA = item.nombreComercialCliente + " - Expirado";
                                }
                                else
                                {
                                        if (item.estado == "0" || item.estado == "1")
                                    {
                                        ObjRecharge.BOLSA = item.nombreComercialCliente + " - Activo";
                                    }
                                }
                            }

                            switch (item.grupobolsa)
                            {
                                case "Plan":
                                    ObjRecharge.ID_ORDEN_BOLSA = "A";
                                    break;
                                case "Servicios Adicionales":
                                    ObjRecharge.ID_ORDEN_BOLSA = "B";
                                    break;
                                case "Paquete":
                                    ObjRecharge.ID_ORDEN_BOLSA = "C";
                                    break;

                            }

                            ObjRecharge.TIPO_PAQUETE = item.grupobolsa;

                            if (item.grupobolsa == "Paquete" && item.unificada != "1")
                            {
                                contadorAddOns++;
                                ObjRecharge.INDEX_UNIFICADA = contadorAddOns.ToString();
                            }


                            if (item.unidad == "DATOS")
                            {


                                if (item.saldo != null && (item.saldo.ToUpper().IndexOf("ILIMITADO") > -1))
                                {
                                    ObjRecharge.UNIDAD = item.total;
                                    ObjRecharge.SALDO = item.saldo;
                                    var conversionConsumo = Convert.ToDecimal(item.consumo);
                                    var conversionConsumoMayorA = Math.Round(((conversionConsumo / 1024)), 2);
                                    var conversionConsumoMenorA = conversionConsumo;
                                    //Campo Consumo
                                    if (conversionConsumo >= 1024)
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMayorA + " GB")).ToString();
                                        ObjRecharge.TIPO_UNIDAD = "GigaBytes";
                                    }
                                    else
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMenorA + " MB")).ToString();
                                        ObjRecharge.TIPO_UNIDAD = "MegaBytes";
                                    }
                                }
                                else
                                {
                                    var conversionTotal = Convert.ToDecimal(item.total);
                                    var conversionSaldo = Convert.ToDecimal(item.saldo);
                                    var conversionConsumo = Convert.ToDecimal(item.consumo);

                                    var conversionTotalMayorA = Math.Round(((conversionTotal / 1024)), 2);
                                    var conversionTotalMenorA = conversionTotal;

                                    var conversionSaldoMayorA = Math.Round(((conversionSaldo / 1024)), 2);
                                    var conversionSaldoMenorA = conversionSaldo;

                                    var conversionConsumoMayorA = Math.Round(((conversionConsumo / 1024)), 2);
                                    var conversionConsumoMenorA = conversionConsumo;

                                    if (conversionTotal >= 1024)
                                    {
                                        ObjRecharge.UNIDAD = (conversionTotalMayorA + " GB").ToString();
                                        ObjRecharge.TIPO_UNIDAD = "GigaBytes";
                                    }
                                    else
                                    {
                                        ObjRecharge.UNIDAD = (conversionTotalMenorA + " MB").ToString();
                                        ObjRecharge.TIPO_UNIDAD = "MegaBytes";
                                    }
                                    //Campo Saldo
                                    if (conversionSaldo >= 1024)
                                    {
                                        ObjRecharge.SALDO = (conversionSaldoMayorA + " GB").ToString();
                                    }
                                    else
                                    {
                                        ObjRecharge.SALDO = (conversionSaldoMenorA + " MB").ToString();
                                    }
                                    //Campo Consumo
                                    if (conversionConsumo >= 1024)
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMayorA + " GB")).ToString();
                                    }
                                    else
                                    {
                                        ObjRecharge.CONSUMO = ((conversionConsumoMenorA + " MB")).ToString();
                                    }
                                }
                            }
                            else
                            {
                                ObjRecharge.CONSUMO = item.consumo;
                                ObjRecharge.UNIDAD = item.total;
                                ObjRecharge.SALDO = item.saldo;

                                switch (item.unidad)
                                {
                                    case "VOZ":
                                        ObjRecharge.TIPO_UNIDAD = "Minutos con Segundos";
                                        break;
                                    case "SOLES":
                                        ObjRecharge.TIPO_UNIDAD = "Soles con Centimos";
                                        break;
                                    case "SMS":
                                        ObjRecharge.TIPO_UNIDAD = "Mensajes de texto";
                                        break;
                                    case "CLARO PUNTOS":
                                        ObjRecharge.TIPO_UNIDAD = "Puntos";
                                        break;
                                    default:
                                        ObjRecharge.TIPO_UNIDAD = "RPC";
                                        break;
                                }
                            }


                            ListRecharge.Add(ObjRecharge);
                         }
                        }

                    }

                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            return ListRecharge;

        }

        public static List<SharedBag> GetDataBagBalanceShared(Claro.Entity.AuditRequest objAudit, string Account, string CustomerId, string Telephone)
        {

            List<SharedBag> lstSharedBag = new List<SharedBag>();
            string strMensaje = string.Empty;
            string strTipoLinea = string.Empty;
            string strTipoCliente = string.Empty;
            string strPlataforma = string.Empty;



            POSTPAID_BALANCE.audiTypeRequest objAuditRequest = new POSTPAID_BALANCE.audiTypeRequest()
            {
                aplicacion = objAudit.ApplicationName,
                usrAplicacion = objAudit.UserName,
                idTransaccion = objAudit.Transaction,
                ipAplicacion = objAudit.IPAddress

            };

            POSTPAID_BALANCE.audiType objaudiType = null;

            POSTPAID_BALANCE.PostPagoCorportaivoType[] objPostPagoCorportaivoType = new POSTPAID_BALANCE.PostPagoCorportaivoType[0];
            POSTPAID_BALANCE.PostpagoDatosConsultaType objPostpagoDatosConsultaType = new POSTPAID_BALANCE.PostpagoDatosConsultaType();
            POSTPAID_BALANCE.PostPagoType[] objPostPagoType = new POSTPAID_BALANCE.PostPagoType[0];
            POSTPAID_BALANCE.PrepagoDatosConsultaType objPrepagoDatosConsultaType = new POSTPAID_BALANCE.PrepagoDatosConsultaType();

            try
            {


                objaudiType = Claro.Web.Logging.ExecuteMethod<POSTPAID_BALANCE.audiType>
                   (objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.POSTPAID_BALANCE, () =>
                   {
                       return Configuration.ServiceConfiguration.POSTPAID_BALANCE.consultaSaldo(objAuditRequest, ref Telephone, CustomerId, out strMensaje, out strTipoLinea, out strTipoCliente, out strPlataforma, out objPrepagoDatosConsultaType, out objPostpagoDatosConsultaType, out objPostPagoType, out objPostPagoCorportaivoType);
                   });


                if (objaudiType.codigoRespuesta != Claro.Constants.NumberZeroString)
                {

                    throw new Exception(objaudiType.mensajeRespuesta);
                }

                if (objPostPagoType != null && objPostPagoType.Length >= 1)
                {
                    for (int i = 0; i < objPostPagoType.Length; i++)
                    {
                        SharedBag objBolsaCompartida = new SharedBag();
                        objBolsaCompartida.GRUPO_BOLSA = Account;
                        if (objPostPagoType[i].unidad == KEY.AppSettings("strTipoUnidadMB"))
                        {
                            objPostPagoType[i].unidad = KEY.AppSettings("strMegabytes");
                        }

                        //RENOMBRADO DE BOLSAS COMPARTIDAS
                        objBolsaCompartida.GRUPO_BOLSA = KEY.AppSettings("strPlanBonosOnTop");
                        objBolsaCompartida.BOLSA = Convert.ToString(objPostPagoType[i].bolsa);
                        objBolsaCompartida.UNIDAD = Convert.ToString(objPostPagoType[i].unidad);
                        objBolsaCompartida.TOPE = Convert.ToString(objPostPagoType[i].topeConsumo);
                        objBolsaCompartida.CONSUMO = Convert.ToString(objPostPagoType[i].consumo);
                        objBolsaCompartida.SALDO = Convert.ToString(objPostPagoType[i].saldo);
                        objBolsaCompartida.FECHAVIGENCIA = Convert.ToString(objPostPagoType[i].fechaExpiracion);

                        objBolsaCompartida.OPCIONAL1 = objPostPagoType[i].opcional1;
                        objBolsaCompartida.OPCIONAL2 = objPostPagoType[i].opcional2;
                        lstSharedBag.Add(objBolsaCompartida);
                    }
                }

                foreach (SharedBag item in lstSharedBag)
                {
                    switch (item.UNIDAD.Trim())
                    {
                        case "SOLES":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToDecimal(item.CONSUMO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToDecimal(item.SALDO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            break;
                        case "MB":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToDecimal(item.CONSUMO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToDecimal(item.SALDO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            break;
                        case "Megabytes":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            break;
                        case "SMS":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToString(Math.Round(Convert.ToDecimal(item.TOPE), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToString(Math.Round(Convert.ToDecimal(item.CONSUMO), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToString(Math.Round(Convert.ToDecimal(item.SALDO), Claro.Constants.NumberZero));
                            }
                            break;
                        case "MMS":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToString(Math.Round(Convert.ToDecimal(item.TOPE), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToString(Math.Round(Convert.ToDecimal(item.CONSUMO), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToString(Math.Round(Convert.ToDecimal(item.SALDO), Claro.Constants.NumberZero));
                            }
                            break;
                    }
                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {


                objPostPagoCorportaivoType = null;
                objPostpagoDatosConsultaType = null;
                objPostPagoType = null;
                objPrepagoDatosConsultaType = null;


            }

            return lstSharedBag;
        }


        public static List<Entity.Dashboard.Postpaid.SharedBag> GetDataBagLineBalanceShared(string strCuenta, string strAplicacion, string strIdTransaccion, string strIPAplicacion, string strUsrAplicacion,
                                                                                 string strtelephone)
        {
            string strMensaje = "";
            string strTipoLinea = "";
            string strTipoCliente = "";
            string strPlataforma = "";

            SharedBag objSharedBag;
            List<SharedBag> listSharedBag = new List<SharedBag>();

            POSTPAID_BALANCE.PostPagoCorportaivoType[] objPostPagoCorportativoType = new POSTPAID_BALANCE.PostPagoCorportaivoType[0];
            POSTPAID_BALANCE.PostpagoDatosConsultaType objPostpagoDatosConsultaType = new POSTPAID_BALANCE.PostpagoDatosConsultaType();
            POSTPAID_BALANCE.PostPagoType[] objPostPagoType = new POSTPAID_BALANCE.PostPagoType[0];
            POSTPAID_BALANCE.PrepagoDatosConsultaType objPrepagoDatosConsultaType = new POSTPAID_BALANCE.PrepagoDatosConsultaType();

            POSTPAID_BALANCE.audiType objaudiType = Configuration.ServiceConfiguration.POSTPAID_BALANCE.consultaSaldo(new POSTPAID_BALANCE.audiTypeRequest()
            {
                aplicacion = strAplicacion,
                idTransaccion = strIdTransaccion,
                ipAplicacion = strIPAplicacion,
                usrAplicacion = strUsrAplicacion
            }, ref strtelephone, string.Empty, out strMensaje, out strTipoLinea, out strTipoCliente, out strPlataforma, out objPrepagoDatosConsultaType, out objPostpagoDatosConsultaType, out objPostPagoType, out objPostPagoCorportativoType);

            if (string.Equals(objaudiType.codigoRespuesta, Claro.Constants.NumberOneNegativeString, StringComparison.OrdinalIgnoreCase))
            {
                throw new Claro.MessageException(objaudiType.mensajeRespuesta);
            }

            if (objPostPagoCorportativoType != null && objPostPagoCorportativoType.Length >= 1)
            {
                decimal dblTope;
                decimal dblConsumo;

                foreach (POSTPAID_BALANCE.PostPagoCorportaivoType itemPostPago in objPostPagoCorportativoType)
                {
                    objSharedBag = new SharedBag();
                    dblTope = Convert.ToDecimal(itemPostPago.topeLinea);
                    dblConsumo = Convert.ToDecimal(itemPostPago.topeConsumo);




                    if (Convert.ToString(itemPostPago.unidad) == KEY.AppSettings("strTipoUnidadMB"))
                    {
                        objSharedBag.UNIDAD = KEY.AppSettings("strMegabytes");
                    }



                    objSharedBag.LINEA = strtelephone;
                    objSharedBag.BOLSA = Convert.ToString(itemPostPago.bolsa);
                    objSharedBag.UNIDAD = Convert.ToString(itemPostPago.unidad);
                    objSharedBag.TOPE = (dblTope > dblConsumo ? Convert.ToString(itemPostPago.topeConsumo) : Convert.ToString(itemPostPago.topeLinea));
                    objSharedBag.CONSUMO = Convert.ToString(itemPostPago.consumoLinea);
                    objSharedBag.SALDO = Convert.ToString(itemPostPago.saldo);
                    objSharedBag.FECHAVIGENCIA = Convert.ToString(itemPostPago.fechaExpiracion);
                    listSharedBag.Add(objSharedBag);
                }

                foreach (SharedBag item in listSharedBag)
                {
                    item.GRUPO_BOLSA = strCuenta;
                    switch (item.UNIDAD.Trim())
                    {
                        case "SOLES":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToDecimal(item.CONSUMO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToDecimal(item.SALDO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            break;
                        case "MB":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToDecimal(item.CONSUMO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToDecimal(item.SALDO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            break;
                        case "Megabytes":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToDecimal(item.TOPE).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToDecimal(item.CONSUMO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToDecimal(item.SALDO).ToString(Claro.Constants.FormatoNumericThree);
                            }
                            break;
                        case "SMS":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToString(Math.Round(Convert.ToDecimal(item.TOPE), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToString(Math.Round(Convert.ToDecimal(item.CONSUMO), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToString(Math.Round(Convert.ToDecimal(item.SALDO), Claro.Constants.NumberZero));
                            }
                            break;
                        case "MMS":
                            if (Convert.ToString(item.TOPE) != String.Empty)
                            {
                                item.TOPE = Convert.ToString(Math.Round(Convert.ToDecimal(item.TOPE), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.CONSUMO) != String.Empty)
                            {
                                item.CONSUMO = Convert.ToString(Math.Round(Convert.ToDecimal(item.CONSUMO), Claro.Constants.NumberZero));
                            }
                            if (Convert.ToString(item.SALDO) != String.Empty)
                            {
                                item.SALDO = Convert.ToString(Math.Round(Convert.ToDecimal(item.SALDO), Claro.Constants.NumberZero));
                            }
                            break;
                    }
                }
            }
            return listSharedBag;

        }
        public static List<Entity.Dashboard.Postpaid.HistoricalRecharge> GetHistoricalRecharge(Claro.Entity.AuditRequest objAudit, string strDateStart, string strMsisdn, string strDateEnd, string strFlagPlataform, ref string strCodigoRespuesta, ref string strMensajeRespuesta)
        {
            List<HistoricalRecharge> lstHistoricalRecharge = new List<HistoricalRecharge>();


            try
            {



                POSTPREDATA.auditRequestType objAuditRequest = new POSTPREDATA.auditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = ConfigurationManager.AppSettings("gConstAplicacionIDBroker"),
                    usuarioAplicacion = objAudit.UserName


                };
                POSTPREDATA.consultarHistoricoRecargasRequest objconsultarHistoricoRecargasRequest = new POSTPREDATA.consultarHistoricoRecargasRequest()
                {
                    auditRequest = objAuditRequest,
                    fechaFinal = strDateEnd,
                    fechaInicial = strDateStart,
                    msisdn = strMsisdn,
                    listaRequestOpcional = new POSTPREDATA.parametrosTypeObjetoOpcional[1]
                };

                objconsultarHistoricoRecargasRequest.listaRequestOpcional[0] = new POSTPREDATA.parametrosTypeObjetoOpcional()
                {
                    campo = "strFlagPlataform",
                    valor = strFlagPlataform
                };
                POSTPREDATA.ConsultaDatosPrePostWSService objConsultaDatosPrePostWSService = new POSTPREDATA.ConsultaDatosPrePostWSService()
                {
                    Url = KEY.AppSettings("gConstUrlConsultaSaldoWS"),
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Timeout = 100000

                };
                POSTPREDATA.consultarHistoricoRecargasResponse objconsultarHistoricoRecargasResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.consultarHistoricoRecargasResponse>
                   (objAudit.Session, objAudit.Transaction, () =>
                   {
                       return objConsultaDatosPrePostWSService.consultarHistoricoRecargas(objconsultarHistoricoRecargasRequest);
                   });

                strCodigoRespuesta = objconsultarHistoricoRecargasResponse.auditResponse.codigoRespuesta;
                strMensajeRespuesta = objconsultarHistoricoRecargasResponse.auditResponse.mensajeRespuesta;
                if (strCodigoRespuesta == "0")
                {
                    foreach (POSTPREDATA.historicoRecargasType item in objconsultarHistoricoRecargasResponse.listaHistoricoRecargas)
                    {
                        HistoricalRecharge objHistoricalRecharge = new HistoricalRecharge();
                        objHistoricalRecharge.AMOUNT_RECHARGE = item.montoRecarga;
                        objHistoricalRecharge.CANAL = item.canal;
                        objHistoricalRecharge.DATE_RECHARGE = item.fechaRecarga;
                        lstHistoricalRecharge.Add(objHistoricalRecharge);
                    }
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            return lstHistoricalRecharge;

        }

        public static List<ConsumptionRecharge> GetConsumptionHistoricalRecharge(Claro.Entity.AuditRequest objAudit, string strDateStart, string strMsisdn, string strDateEnd, string strTypeConsumption, ref string strCodigoRespuesta, ref string strMensajeRespuesta)
        {
            List<ConsumptionRecharge> lstConsumptionRecharge = new List<ConsumptionRecharge>();
            try
            {

                POSTPREDATA.auditRequestType objAuditRequest = new POSTPREDATA.auditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = ConfigurationManager.AppSettings("USRProceso"),
                    usuarioAplicacion = objAudit.UserName


                };
                POSTPREDATA.consultarConsumoRecargasRequest objconsultarConsumoRecargasRequest = new POSTPREDATA.consultarConsumoRecargasRequest()
                {
                    auditRequest = objAuditRequest,
                    fechaFinal = strDateEnd,
                    fechaInicial = strDateStart,
                    msisdn = strMsisdn,
                    tipoConsumo = strTypeConsumption
                };
                POSTPREDATA.ConsultaDatosPrePostWSService objConsultaDatosPrePostWSService = new POSTPREDATA.ConsultaDatosPrePostWSService()
                {
                    Url = KEY.AppSettings("gConstUrlConsultaSaldoWS"),
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Timeout = 100000

                };
                POSTPREDATA.consultarConsumoRecargasResponse objconsultarConsumoRecargasResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.consultarConsumoRecargasResponse>
                       (objAudit.Session, objAudit.Transaction, () =>
                       {
                           return objConsultaDatosPrePostWSService.consultarConsumoRecargas(objconsultarConsumoRecargasRequest);
                       });



                foreach (POSTPREDATA.consumoRecargasType item in objconsultarConsumoRecargasResponse.listaConsumoRecargas)
                {
                    ConsumptionRecharge objConsumptionRecharge = new ConsumptionRecharge();
                    objConsumptionRecharge.DateEvent = item.fechaEvento;
                    objConsumptionRecharge.TypeConsumption = item.tipoConsumo;
                    objConsumptionRecharge.NumberDestinationAPN = item.numeroDestinoAPN;
                    objConsumptionRecharge.IdBag = item.nombreBolsa;
                    objConsumptionRecharge.AmountConsumed = item.montoConsumido;
                    objConsumptionRecharge.Balance = item.saldo;
                    lstConsumptionRecharge.Add(objConsumptionRecharge);
                }

                strCodigoRespuesta = objconsultarConsumoRecargasResponse.auditResponse.codigoRespuesta;
                strMensajeRespuesta = objconsultarConsumoRecargasResponse.auditResponse.mensajeRespuesta;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            return lstConsumptionRecharge;
        }

        public static List<Entity.Dashboard.Prepaid.PackageODCS> GetPostConsumptionDataPacket(Claro.Entity.AuditRequest objAuditRequest, Entity.Dashboard.Prepaid.PackageODCS objPackageODCS, ref string codigoRespuesta, ref string mensajeRespuesta)
        {
            List<Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = new List<Entity.Dashboard.Prepaid.PackageODCS>();
            POSTPAID_PAQUETE.AuditType oAuditType = null;
            POSTPAID_PAQUETE.PaqueteAdquiridoType oType = null;
            int i, numCero;

            try
            {
                POSTPAID_PAQUETE.ConsultarPaquetesAdquiridosRequest objConsultarPaquetesAdquiridosRequest = new POSTPAID_PAQUETE.ConsultarPaquetesAdquiridosRequest()
                {
                    idTransaccion = objAuditRequest.Transaction,
                    ipAplicacion = objAuditRequest.IPAddress,
                    aplicacion = KEY.AppSettings("gConstAplicacionIDODCS"),
                    usrAplicacion = objAuditRequest.UserName,
                    msisdn = objPackageODCS.Msisdn,
                    canal = objPackageODCS.Channel,
                    codigoPaquete = objPackageODCS.PackageCode,
                    estadoPaquete = objPackageODCS.State,
                    fechaInicio = objPackageODCS.ActivationDate,
                    fechaFin = objPackageODCS.ExpirationDate,
                    idTipoLinea = objPackageODCS.LineTypeId,
                    familiaPaquete = objPackageODCS.FamilyPackage

                };

                POSTPAID_PAQUETE.ConsultarPaquetesAdquiridosResponse objConsultarPaquetesAdquiridosResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_PAQUETE.ConsultarPaquetesAdquiridosResponse>(objAuditRequest.Session, objAuditRequest.Transaction, Configuration.ServiceConfiguration.PREPAID_PAQUETE, () =>
                {
                    return Configuration.ServiceConfiguration.PREPAID_PAQUETE.ConsultarPaquetesAdquiridos(objConsultarPaquetesAdquiridosRequest);
                });
                oAuditType = objConsultarPaquetesAdquiridosResponse.Audit;
                codigoRespuesta = oAuditType.errorCode;
                mensajeRespuesta = oAuditType.errorMsg;


                numCero = Convert.ToInt(KEY.AppSettings("gConstValor00"));

                if (codigoRespuesta == KEY.AppSettings("gConstValor00") && objConsultarPaquetesAdquiridosResponse.ListaPaquetesAdquiridos != null && objConsultarPaquetesAdquiridosResponse.ListaPaquetesAdquiridos.Length > numCero)
                {


                    for (i = numCero; i < objConsultarPaquetesAdquiridosResponse.ListaPaquetesAdquiridos.Length; i++)
                    {
                        Entity.Dashboard.Prepaid.PackageODCS objPackagesODCS = new Entity.Dashboard.Prepaid.PackageODCS();

                        oType = objConsultarPaquetesAdquiridosResponse.ListaPaquetesAdquiridos[i];

                        objPackagesODCS.ConvergedCode = oType.codigoConvergente;
                        objPackagesODCS.PackageCode = oType.codigoPaquete;
                        objPackagesODCS.DescriptionPackage = oType.descripcion;
                        objPackagesODCS.ActivationDate = oType.fechaActivacion;
                        objPackagesODCS.ExpirationDate = oType.fechaExpiracion;
                        objPackagesODCS.Channel = oType.canal;
                        objPackagesODCS.Price = oType.precio;
                        objPackagesODCS.State = oType.estado;
                        objPackagesODCS.MBConsumed = oType.MBComsumidos;
                        objPackagesODCS.MBAvailable = oType.MBDisponibles;
                        objPackagesODCS.MBTotal = oType.MBTotales;
                        objPackagesODCS.RatingGroup = oType.RatingGroup;
                        objPackagesODCS.Zone = oType.zona;
                        objPackagesODCS.TypePurchase = oType.tipoCompra;

                        objPackagesODCS.AcquisitionDate = oType.listaPaqueteAdquiridoParametro[0].valor.ToString();

                        objPackagesODCS.IdPurchase = objPackagesODCS.PackageCode;
                        objPackagesODCS.Description = objPackagesODCS.DescriptionPackage;
                        objPackagesODCS.DatePost = objPackagesODCS.ActivationDate;
                        objPackagesODCS.mbincDES = objPackagesODCS.MBTotal;
                        objPackagesODCS.validity = string.Empty;

                        objPackagesODCS.NameBag = objPackagesODCS.DescriptionPackage;
                        objPackagesODCS.AcquisitionDate = objPackagesODCS.AcquisitionDate;
                        objPackagesODCS.ExpirationDatePost = objPackagesODCS.ExpirationDate;
                        objPackagesODCS.cost = objPackagesODCS.Price;
                        objPackagesODCS.TypePackage = objPackagesODCS.TypePurchase;

                        lstPackageODCS.Add(objPackagesODCS);
                    }
                }

            }
            catch (Exception ex)
            {

                mensajeRespuesta = KEY.AppSettings("gConstErrorAdquirirODCS");
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }


            return lstPackageODCS;
        }

        //vtorremo
        public static List<Entity.Dashboard.Prepaid.PackageODCS> GetHistoricalPackage(Claro.Entity.AuditRequest objAuditRequest, Entity.Dashboard.Prepaid.PackageODCS objPackageODCS, ref string codigoRespuesta, ref string mensajeRespuesta)
        {
            List<Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = new List<Entity.Dashboard.Prepaid.PackageODCS>();

            POSTPREDATA.auditRequestType oAuditType = new POSTPREDATA.auditRequestType()
           {
               idTransaccion = objAuditRequest.Transaction,
               ipAplicacion = objAuditRequest.IPAddress,
               nombreAplicacion = ConfigurationManager.AppSettings("USRProceso"),
               usuarioAplicacion = objAuditRequest.UserName
           };
            POSTPREDATA.consultarHistoricoPaquetesRequest objconsultarHistoricoPaquetesRequest = new POSTPREDATA.consultarHistoricoPaquetesRequest()
            {
                auditRequest = oAuditType,
                fechaFinal = objPackageODCS.EndDate,
                fechaInicial = objPackageODCS.StartDate,
                msisdn = objPackageODCS.Msisdn,
                tipoPaquete = objPackageODCS.TypePackage
            };
            POSTPREDATA.ConsultaDatosPrePostWSService objConsultaDatosPrePostWSService = new POSTPREDATA.ConsultaDatosPrePostWSService()
            {
                Url = KEY.AppSettings("gConstUrlConsultaSaldoWS"),
                Credentials = System.Net.CredentialCache.DefaultCredentials,
                Timeout = 100000

            };
            try
            {
                POSTPREDATA.consultarHistoricoPaquetesResponse objconsultarHistoricoPaquetesResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.consultarHistoricoPaquetesResponse>
                      (objAuditRequest.Session, objAuditRequest.Transaction, () =>
                      {
                          return objConsultaDatosPrePostWSService.consultarHistoricoPaquetes(objconsultarHistoricoPaquetesRequest);
                      });

                codigoRespuesta = objconsultarHistoricoPaquetesResponse.auditResponse.codigoRespuesta;
                mensajeRespuesta = objconsultarHistoricoPaquetesResponse.auditResponse.mensajeRespuesta;

                if (codigoRespuesta == "0")
                {
                    foreach (POSTPREDATA.historicoPaquetesType item in objconsultarHistoricoPaquetesResponse.listaHistoricoPaquetes)
                    {
                        Entity.Dashboard.Prepaid.PackageODCS itemPackageODCS = new Entity.Dashboard.Prepaid.PackageODCS();
                        itemPackageODCS.NameBag = item.nombreBolsa;
                        itemPackageODCS.TypePackage = item.tipoPaquete;
                        itemPackageODCS.cost = item.costo;
                        itemPackageODCS.State = item.estado;
                        itemPackageODCS.AcquisitionDate = item.fechaAdquisicion;
                        itemPackageODCS.ExpirationDatePost = item.fechaVencimiento;
                        itemPackageODCS.validity = item.vigencia;
                        lstPackageODCS.Add(itemPackageODCS);
                    }
                }

            }
            catch (Exception ex)
            {

                mensajeRespuesta = KEY.AppSettings("gConstErrorConsultaSaldoWS");
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }

            return lstPackageODCS;
        }

        //HISTORICO PAQUETES
        public static List<Entity.Dashboard.Prepaid.PackageODCS> GetHistoryPackageTobe(Claro.Entity.AuditRequest audit, Entity.Dashboard.Prepaid.PackageODCS objPackageODCS, ref string codigoRespuesta, ref string mensajeRespuesta)
        {
            List<Entity.Dashboard.Prepaid.PackageODCS> lstPackage = new List<Entity.Dashboard.Prepaid.PackageODCS>();

            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Request.HistoricoPaquetes request = new Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Request.HistoricoPaquetes()
                {
                    consultarHistoricoPaquetesRequest = new Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Request.ConsultarHistoricoPaquetesRequest()
                    {
                        numeroLinea = objPackageODCS.Msisdn.Length > Claro.Constants.NumberNine ? objPackageODCS.Msisdn.Substring(2, 9) : objPackageODCS.Msisdn,
                        coIdPub = objPackageODCS.coIdPub,
                        fechaInicio = objPackageODCS.StartDate + " 00:00:00",
                        fechaFin = objPackageODCS.EndDate + " 23:59:59",
                        tipoUnidad = objPackageODCS.TypePackage,
                        ListaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Common.ListaOpcional>(){
                            new Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Common.ListaOpcional(){
                                clave=string.Empty,
                                valor=string.Empty
                            }
                        }
                    }
                };

                Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Response.ConsultarHistoricoPaquetes response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetPackageHistory.Response.ConsultarHistoricoPaquetes>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_PACKAGE_HISTORY_TOBE, audit, request, false);

                codigoRespuesta = response.consultarHistoricoPaquetesResponse.responseAudit.codigoRespuesta;
                if (response != null &&
                    response.consultarHistoricoPaquetesResponse != null &&
                    response.consultarHistoricoPaquetesResponse.responseAudit != null &&
                    response.consultarHistoricoPaquetesResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.consultarHistoricoPaquetesResponse.responseData != null &&
                    response.consultarHistoricoPaquetesResponse.responseData.listaHistoricoPaquete != null)
                {

                    Entity.Dashboard.Prepaid.PackageODCS itemPackage = null;
                    double nroSaldo;
                    int numeroEntero;

                    foreach (var item in response.consultarHistoricoPaquetesResponse.responseData.listaHistoricoPaquete)
                    {
                        itemPackage = new Entity.Dashboard.Prepaid.PackageODCS();

                        itemPackage.NameBag = item.nombrePaquete;
                        string descripcionVigencia = "DÍA";
                        if (!string.IsNullOrEmpty(item.vigencia) && double.Parse(item.vigencia) > 1)
                        {
                            descripcionVigencia = "DÍAS";
                        }
                        itemPackage.validity = item.vigencia + ' ' + descripcionVigencia;
                        itemPackage.TypeValidity = item.tipoVigencia;
                        itemPackage.ExpirationDatePost = item.fechaVencimiento;
                        itemPackage.cost = item.costo + ".00";
                        itemPackage.TypePurchase = item.tipoCompra;
                        itemPackage.AcquisitionDate = item.fechaAdquisicion;
                        itemPackage.State = item.estado;

                        if (item.tipoPaquete.ToUpper() == "VOZ")
                        {
                            itemPackage.TypeBalance = item.saldo;
                            itemPackage.TypePackage = "MINUTOS";
                        }
                        else
                        {
                            if (item.saldo.ToUpper().Equals("ILIMITADO"))
                            {
                                item.saldo = Int32.MaxValue.ToString();
                                Claro.Web.Logging.Info(audit.Session, audit.Transaction, item.saldo);
                            }
                            nroSaldo = double.Parse(item.saldo, CultureInfo.InvariantCulture);
                            numeroEntero = Convert.ToInt(Math.Floor(nroSaldo));
                            itemPackage.TypeBalance = Convert.ToString(numeroEntero);

                            itemPackage.TypePackage = item.tipoPaquete;
                        }

                        lstPackage.Add(itemPackage);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }
            lstPackage = lstPackage.OrderBy(a => DateTime.ParseExact(a.AcquisitionDate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.CreateSpecificCulture("quz-PE"))).Reverse().ToList();
            return lstPackage;

        }


        public static bool CreateInteraction(string idTransaccion, Claro.SIACU.Entity.Dashboard.Interaction objInteraction, Claro.Entity.AuditRequest objAuditRequest)
        {
            bool strResult = false;


            try
            {

                SIACU_INTERAC.InteraccionType objInteraccionType = new SIACU_INTERAC.InteraccionType()
                {
                    clase = objInteraction.CLASE,
                    codigoEmpleado = objInteraction.CODIGOEMPLEADO,
                    codigoSistema = objInteraction.CODIGOSISTEMA,
                    flagCaso = objInteraction.FLAGCASO,
                    hechoEnUno = objInteraction.HECHOENUNO,
                    metodoContacto = objInteraction.METODOCONTACTO,
                    notas = objInteraction.NOTAS,
                    subClase = objInteraction.SUBCLASE,
                    telefono = objInteraction.TELEFONO,
                    textResultado = objInteraction.TEXTRESULTADO,
                    tipo = objInteraction.TIPO,
                    tipoInteraccion = objInteraction.TIPOINTERACCION

                };
                SIACU_INTERAC.TransaccionInteraccionesAsync objTransaccionInteraccionesAsync = new SIACU_INTERAC.TransaccionInteraccionesAsync()
                {
                    Url = KEY.AppSettings("gConstUrlTipificaHistPaquetes"),
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Timeout = 100000
                };
                SIACU_INTERAC.AuditType objAuditType = Claro.Web.Logging.ExecuteMethod<SIACU_INTERAC.AuditType>(objAuditRequest.Session, objAuditRequest.Transaction, () =>
                {
                    return objTransaccionInteraccionesAsync.crearInteraccion(idTransaccion, objInteraccionType, null, null, "");
                });


                if (objAuditType.errorCode.Equals("0"))
                {
                    strResult = true;
                }

            }
            catch (Exception ex)
            {


                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }


            return strResult;
        }

        public static string GetSpeed(string strMsisdn, Claro.Entity.AuditRequest objAudit)
        {
            Claro.Web.Logging.Info("GetSpeed", objAudit.Transaction, strMsisdn);

            string cadena = "";
            try
            {
                Claro.Web.Logging.Info("Try", objAudit.Transaction, strMsisdn);
                Claro.Web.Logging.Info("objAuditRequest", objAudit.Transaction, strMsisdn);
                POSTPAID_RESTRICTVELOC.auditRequestType objAuditRequest = new POSTPAID_RESTRICTVELOC.auditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = objAudit.ApplicationName,
                    usuarioAplicacion = objAudit.UserName
                };


                Claro.Web.Logging.Info("objconsultarTetheDegVelocRequest", objAudit.Transaction, strMsisdn);


                Claro.Web.Logging.Info("HeaderRequest", objAudit.Transaction, strMsisdn);
                POSTPAID_RESTRICTVELOC.HeaderRequestType HeaderRequest = new POSTPAID_RESTRICTVELOC.HeaderRequestType()
                {
                    consumer = KEY.AppSettings("strUsuarioAppWSRestricTetheVeloc"),
                    country = KEY.AppSettings("strCountryWSRestricTetheVeloc"),
                    dispositivo = string.Empty,
                    language = KEY.AppSettings("strLanguageWSRestricTetheVeloc"),
                    modulo = string.Empty,
                    msgType = KEY.AppSettings("strMsgTypeWSRestricTetheVeloc"),
                    operation = string.Empty,
                    pid = objAudit.Transaction,
                    system = string.Empty,
                    timestamp = DateTime.Now,
                    userId = objAudit.UserName,
                    VarArg = new POSTPAID_RESTRICTVELOC.ArgType[1],
                    wsIp = KEY.AppSettings("strWsIpWSRestricTetheVeloc")

                };

                HeaderRequest.VarArg[0] = new POSTPAID_RESTRICTVELOC.ArgType() { key = string.Empty, value = string.Empty };
                Claro.Web.Logging.Info("headerRequest1", objAudit.Transaction, strMsisdn);

                POSTPAID_RESTRICTVELOC.HeaderRequestType1 headerRequest1 = new POSTPAID_RESTRICTVELOC.HeaderRequestType1()
                {

                    canal = "",
                    fechaInicio = DateTime.Now,
                    idAplicacion = objAudit.IPAddress,
                    idTransaccionESB = objAudit.Transaction,
                    idTransaccionNegocio = "",
                    nodoAdicional = "",
                    usuarioAplicacion = objAudit.UserName,
                    usuarioSesion = ""


                };

                Claro.Web.Logging.Info("consultarTetheDegVelocRequest", objAudit.Transaction, strMsisdn);

                POSTPAID_RESTRICTVELOC.consultarTetheDegVelocRequest consultarTetheDegVelocRequest = new POSTPAID_RESTRICTVELOC.consultarTetheDegVelocRequest()
                {
                    auditRequest = objAuditRequest,
                    listaRequestOpcional = new POSTPAID_RESTRICTVELOC.parametrosTypeObjetoOpcional[1],
                    msisdn = strMsisdn
                };

                consultarTetheDegVelocRequest.listaRequestOpcional[0] = new POSTPAID_RESTRICTVELOC.parametrosTypeObjetoOpcional() { campo = string.Empty, valor = string.Empty };

                Claro.Web.Logging.Info("HeaderResponseType1", objAudit.Transaction, strMsisdn);
                POSTPAID_RESTRICTVELOC.HeaderResponseType1 headerResponse1 = new POSTPAID_RESTRICTVELOC.HeaderResponseType1();
                POSTPAID_RESTRICTVELOC.consultarTetheDegVelocResponse objconsultarTetheDegVelocResponse = new POSTPAID_RESTRICTVELOC.consultarTetheDegVelocResponse();
                POSTPAID_CONSULTCLAVE.desencriptarRequestBody objRequest = new POSTPAID_CONSULTCLAVE.desencriptarRequestBody();
                POSTPAID_CONSULTCLAVE.desencriptarResponseBody objResponse = new POSTPAID_CONSULTCLAVE.desencriptarResponseBody();

                Claro.Web.Logging.Info("KEY.AppSettings(strRestricTetheVelocWS)", objAudit.Transaction, strMsisdn);

                string strLlave = KEY.AppSettings("strRestricTetheVelocWS");

                objRequest.idTransaccion = objAudit.Transaction;
                objRequest.ipAplicacion = "";
                objRequest.ipTransicion = "";
                objRequest.usrAplicacion = objAudit.UserName;
                objRequest.idAplicacion = KEY.AppSettings("CodAplicacion");
                objRequest.codigoAplicacion = KEY.AppSettings("strCodigoAplicacion");
                objRequest.usuarioAplicacionEncriptado = leerRegistro(strLlave, "User");
                objRequest.claveEncriptado = leerRegistro(strLlave, "Password");

                Claro.Web.Logging.Info("desencriptarResponseBody: objResponse.codigoResultado", objRequest.usuarioAplicacionEncriptado, objRequest.claveEncriptado);

                objResponse.codigoResultado = Claro.SIACU.Data.Configuration.ServiceConfiguration.CONSULTCLAVE.desencriptar(ref objRequest.idTransaccion, objRequest.ipAplicacion, objRequest.ipTransicion, objRequest.usrAplicacion, objRequest.idAplicacion, objRequest.codigoAplicacion, objRequest.usuarioAplicacionEncriptado, objRequest.claveEncriptado, out objResponse.mensajeResultado, out objResponse.usuarioAplicacion, out objResponse.clave);

                Claro.Web.Logging.Info("desencriptarResponseBody: Resultado", objResponse.codigoResultado, objResponse.mensajeResultado);

                if (objResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info("OperationContextScope", objResponse.usuarioAplicacion, objResponse.clave);
                    using (new OperationContextScope(Configuration.ServiceConfiguration.RESTRICVELOC.InnerChannel))
                    {
                        Claro.Web.Logging.Info("OperationContextScope: Cabecera", objResponse.usuarioAplicacion, objResponse.clave);

                        OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(objAudit.Transaction, objResponse.usuarioAplicacion, objResponse.clave));
                        Claro.Web.Logging.Info("POSTPAID_RESTRICTVELOC: DataPower", objResponse.usuarioAplicacion, objResponse.clave);

                        Claro.Web.Logging.ExecuteMethod<POSTPAID_RESTRICTVELOC.HeaderResponseType>
                         (objAudit.Session, objAudit.Transaction, Claro.SIACU.Data.Configuration.ServiceConfiguration.RESTRICVELOC, () =>
                         {
                             return Claro.SIACU.Data.Configuration.ServiceConfiguration.RESTRICVELOC.consultarTetheDegVeloc(HeaderRequest,
                                 headerRequest1, consultarTetheDegVelocRequest, out headerResponse1, out objconsultarTetheDegVelocResponse);
                         });

                        Claro.Web.Logging.Info("POSTPAID_RESTRICTVELOC: Respuesta", objconsultarTetheDegVelocResponse.auditResponse.codigoRespuesta, objconsultarTetheDegVelocResponse.auditResponse.mensajeRespuesta);

                    }

                    Claro.Web.Logging.Info("POSTPAID_RESTRICTVELOC: Respuesta", objconsultarTetheDegVelocResponse.auditResponse.codigoRespuesta, objconsultarTetheDegVelocResponse.auditResponse.mensajeRespuesta);
                }
                string newDateV = "";
                Claro.Web.Logging.Info("objconsultarTetheDegVelocResponse: Resultado", objconsultarTetheDegVelocResponse.auditResponse.codigoRespuesta, objconsultarTetheDegVelocResponse.auditResponse.mensajeRespuesta);
                if (objconsultarTetheDegVelocResponse.auditResponse.codigoRespuesta == "0")
                {
                    if (objconsultarTetheDegVelocResponse.velocidad != "4G")
                    {
                        if (!string.IsNullOrEmpty(objconsultarTetheDegVelocResponse.fechaV))
                        {
                            newDateV = DateTime.Parse(objconsultarTetheDegVelocResponse.fechaV).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        cadena = "Si   " + newDateV + "|";
                    }
                    else if (objconsultarTetheDegVelocResponse.velocidad == "4G")
                    {
                        cadena = "NO|";
                    }

                    cadena += objconsultarTetheDegVelocResponse.tethering;
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);

            }

            return cadena;

        }

        public static string leerRegistro(string key, string value)
        {
            string result = string.Empty;
            try
            {
                Claro.Web.Logging.Info(key, value, result);
                string TIMRootRegistry = @"SOFTWARE\TIM";
                result = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + key).GetValue(value, "").ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(key, value, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(key, value, result);
            return result;
        }

        public static Claro.SIACU.ProxyService.SecurityService.OptionsResponse GetOptions(string strApplicationType, string strUserId, Claro.Entity.AuditRequest audit)
        {
            SECURITY_SERVICE.OptionsResponse objOptionsResponse;

            string strModuleType = "";

            string OPTIONS_PRODUCTO_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[2].ToString();
            strApplicationType = strApplicationType != KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString() ?
                                                       strApplicationType : KEY.AppSettings("strTipoGeneralFIJA").Split('|')[0].ToString();


            switch (strApplicationType)
            {
                case Claro.SIACU.Constants.PostpaidMajuscule:
                    strModuleType = Claro.SIACU.Constants.PostpaidMajusculeSpanish;
                    break;
                case Claro.SIACU.Constants.PrepaidMajuscule:
                    strModuleType = Claro.SIACU.Constants.PrepaidMajusculeSpanish;
                    break;
                case Claro.SIACU.Constants.DTH:
                case Claro.SIACU.Constants.HFC:
                case Claro.SIACU.Constants.LTE:
                case Claro.SIACU.Constants.IFI:
                    strModuleType = strApplicationType;
                    break;
                case Claro.SIACU.Constants.FIJA:
                    strModuleType = OPTIONS_PRODUCTO_FIXE;
                    break;
            }

            Claro.SIACU.ProxyService.SecurityService.OptionsRequest oOptionRequest = new Claro.SIACU.ProxyService.SecurityService.OptionsRequest()
            {
                audit = new ProxyService.SecurityService.AuditRequest()
                {
                    Session = audit.Session,
                    transaction = audit.Transaction,
                    applicationName = audit.ApplicationName,
                    userName = audit.UserName,
                    ipAddress = audit.IPAddress
                },
                module = strModuleType,
                applicationId = int.Parse(KEY.AppSettings("ApplicationCode")),
                userId = Int32.Parse(strUserId)
            };

            try
            {
                objOptionsResponse =
                Claro.Web.Logging.ExecuteMethod<SECURITY_SERVICE.OptionsResponse>
                (oOptionRequest.audit.Session, oOptionRequest.audit.transaction, Configuration.ServiceConfiguration.SECURITY_SERVICE, () =>
                {
                    return Configuration.ServiceConfiguration.SECURITY_SERVICE.GetOptions(oOptionRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
                objOptionsResponse = null;

            }

            return objOptionsResponse;
        }

        public static string GetMotiveCancellation(string strIdSession, string strTransaction, string strContractCode, string strCustomerCode, ref string strMotiveCancellation, ref string strCodError, ref string strDesError)
        {
            string strFlagResult;

            DbParameter[] parameters = new DbParameter[]
            {
                new DbParameter("P_CONTRACT_ID", DbType.String, ParameterDirection.Input, strContractCode),
                new DbParameter("P_CUSTOMER_ID", DbType.String, ParameterDirection.Input, strCustomerCode),
                new DbParameter("P_FLAGRESULT", DbType.String, 200, ParameterDirection.Output),
                new DbParameter("P_MOTIVECANCELLATION", DbType.String, 1000, ParameterDirection.Output),
                new DbParameter("P_COD_ERROR", DbType.Int32, 22, ParameterDirection.Output),
                new DbParameter("P_MSG_ERROR", DbType.String, 1000, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_OBTENER_MOTIVO_CANCELACION, parameters);

            strFlagResult = parameters[2].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[2].Value.ToString());
            strMotiveCancellation = parameters[3].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[3].Value.ToString());
            strCodError = parameters[4].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[4].Value.ToString());
            strDesError = parameters[5].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[5].Value.ToString());

            return strFlagResult;
        }

        /// <summary>
        /// Método que obtiene Segmento del cliente en BSCS.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intContratoId">Id de contrato</param>
        /// <param name="strNroCel">Número de la línea</param>
        /// <returns>Devuelve cadena con información de cliente.</returns>
        public static string GetTypeSegmentLineBSCS(string strIdSession, string strTransaction, int intContratoId, string strNroCel)
        {

            string strTipoPost;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("pi_co_id", DbType.Int32, ParameterDirection.Input, intContratoId),
                new DbParameter("pi_dn_num", DbType.String, ParameterDirection.Input, strNroCel),
                new DbParameter("po_etiqueta", DbType.String,30, ParameterDirection.Output),
                new DbParameter("po_cod_rpta", DbType.String,100, ParameterDirection.Output),
                new DbParameter("po_msj_rpta", DbType.String,100, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_BSCSS_SEGMENT_CLIENTE, parameters);
            strTipoPost = parameters[2].Value.ToString();

            return strTipoPost;
        }

        public static List<Entity.Dashboard.Postpaid.Annotation> GetAnnotationWSTobe(Claro.Entity.AuditRequest objAudit, string strContract, string strStatus, string flagconvivencia, out string strCodeResult, out string strMsgResult)
        {
            List<Entity.Dashboard.Postpaid.Annotation> listAnnotation = new List<Entity.Dashboard.Postpaid.Annotation>();
            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Request.request()
                {
                    consultarHistAnotacionesRequest = new Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Request.ConsultarHistAnotacionesRequest()
                    {
                        coId = strContract,
                        csType = strStatus,
                        listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Common.listaOpcional>()
                        {
                            new Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Common.listaOpcional(){
                                clave=KEY.AppSettings("strFlagConvivencia"),
                                valor =flagconvivencia,
}
                        }
                    }
                };

                Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetAnnotationWSTobe.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DATA_ANOTACIONES, objAudit, request, false);
                strMsgResult = string.Empty;
                strCodeResult = Claro.Constants.NumberOneNegativeString;
                if (response != null &&
                      response.consultarHistAnotacionesResponse != null &&
                      response.consultarHistAnotacionesResponse.responseAudit != null &&
                      response.consultarHistAnotacionesResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                      response.consultarHistAnotacionesResponse.responseData != null &&
                      response.consultarHistAnotacionesResponse.responseData.listaHistoricoAnotaciones != null

                      )
                {
                    strMsgResult = response.consultarHistAnotacionesResponse.responseAudit.mensajeRespuesta;
                    strCodeResult = response.consultarHistAnotacionesResponse.responseAudit.codigoRespuesta;
                    foreach (var item in response.consultarHistAnotacionesResponse.responseData.listaHistoricoAnotaciones)
                    {
                        Annotation objAnnotation = new Annotation();

                        objAnnotation.CODIGO_CLIENTE = Convert.ToString(item.cuenta);//NO
                        objAnnotation.CODIGO = Convert.ToString(item.codigoTickler); //SI
                        objAnnotation.ESTADO = Convert.ToString(item.estadoAnotacion);//SI
                        objAnnotation.PRIORIDAD = Convert.ToString(item.prioridad);//NO
                        objAnnotation.DESCRIPCION = Convert.ToString(item.descripcionCorta) + "|" + Convert.ToString(item.descripcionLarga);//SI
                        objAnnotation.USUARIO_SEGUIMIENTO = Convert.ToString(item.usuarioSeg);//NO
                        objAnnotation.FECHA_SEGUIMIENTO = Convert.ToString(item.fechaSeg);//NO
                        objAnnotation.FECHA_APERTURA = Convert.ToString(item.fechaApertura);//si
                        objAnnotation.FECHA_CIERRE = Convert.ToString(item.fechaCierre);//si
                        objAnnotation.FECHA_MODI = Convert.ToString(item.fechaModif);//si
                        objAnnotation.NRO_TICKLER = Convert.ToString(item.numeroTickler);//no
                        objAnnotation.ESTADO_ACTION = Convert.ToString(item.estado);//no

                        listAnnotation.Add(objAnnotation);
                    }

                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                throw;
            }
            return listAnnotation;
        }
        public static List<Entity.Dashboard.Postpaid.Annotation> GetAnnotationWS(Claro.Entity.AuditRequest objAudit, string strCustomerId, string strContract, string strStatus, string strNumberTickler, out string strCodeResult, out string strMsgResult)
        {
            List<Entity.Dashboard.Postpaid.Annotation> listAnnotation = new List<Entity.Dashboard.Postpaid.Annotation>();
            try
            {
                POSTPAID_ANNOTATION.auditRequestType objAuditRequest = new POSTPAID_ANNOTATION.auditRequestType()
                {
                    idTransaccion = objAudit.Transaction,
                    ipAplicacion = objAudit.IPAddress,
                    nombreAplicacion = objAudit.ApplicationName,
                    usuarioAplicacion = objAudit.UserName
                };
                POSTPAID_ANNOTATION.consultarRequest objconsultarRequest = new POSTPAID_ANNOTATION.consultarRequest()
                {
                    auditRequest = objAuditRequest,
                    customer_id = strCustomerId,
                    co_id = strContract,
                    cstype = strStatus,
                    tickler_number = strNumberTickler

                };
                POSTPAID_ANNOTATION.consultarResponse objconsultarResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_ANNOTATION.consultarResponse>
                (objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.POSTPAID_ANNOTATION, () =>
                {
                    return Configuration.ServiceConfiguration.POSTPAID_ANNOTATION.consultar(objconsultarRequest);
                });
                strMsgResult = objconsultarResponse.auditResponse.mensajeRespuesta;
                strCodeResult = objconsultarResponse.auditResponse.codigoRespuesta;

                if (Convert.ToInt(strCodeResult) == 0)
                {
                    int cantidad = objconsultarResponse.listaPet_AnotacionResponse.Length;

                    for (int i = 0; i < cantidad; i++)
                    {
                        Annotation objAnnotation = new Annotation();
                        if (objconsultarResponse.listaPet_AnotacionResponse[i].nro_tickler.Trim() == "" && objconsultarResponse.listaPet_AnotacionResponse[i].estado_anotacion.Trim() == "Abierto")
                        {
                            if (objconsultarResponse.listaPet_AnotacionResponse[i].estado.Trim() == "B")
                            {
                                objAnnotation.ACCION_SEGUIMIENTO = KEY.AppSettings("strAccSegBloqueoEncolado");
                            }
                            else if (objconsultarResponse.listaPet_AnotacionResponse[i].estado.Trim() == "D")
                            {
                                objAnnotation.ACCION_SEGUIMIENTO = KEY.AppSettings("strAccSegDesbloqueoEncolado");
                            }
                        }
                        else
                        {
                            objAnnotation.ACCION_SEGUIMIENTO = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].accion_seguimiento);
                        }

                        objAnnotation.CODIGO_CLIENTE = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].cuenta);
                        objAnnotation.CODIGO = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].codigo_tickler);
                        objAnnotation.ESTADO = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].estado_anotacion);
                        objAnnotation.PRIORIDAD = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].prioridad);
                        objAnnotation.DESCRIPCION = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].descripcion_corta) + "|" + string.Empty;
                        objAnnotation.USUARIO_SEGUIMIENTO = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].usuario_seguimiento);
                        objAnnotation.FECHA_SEGUIMIENTO = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].fecha_seguimiento);
                        objAnnotation.FECHA_APERTURA = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].fecha_apertura);
                        objAnnotation.FECHA_CIERRE = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].fecha_cierre);
                        objAnnotation.NRO_TICKLER = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].nro_tickler);
                        objAnnotation.ESTADO_ACTION = Convert.ToString(objconsultarResponse.listaPet_AnotacionResponse[i].estado);

                        listAnnotation.Add(objAnnotation);
                    }

                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw;
            }
            return listAnnotation;
        }
        /// <summary>
        /// Método para consultar el estado de cuenta. 
        /// </summary>
        /// <param name="tipoConsulta">Tipo de Consulta</param>
        /// <param name="cuenta">Cuenta de Cliente</param>
        /// <param name="periodo">Periodo</param>
        /// <param name="numeroDocumentos">Numeros de Documento</param>
        /// <param name="consultarEstadoCuenta">Datos del Estado de Cuenta</param>
        /// <returns>Devuelve objeto GetBSS_StatusAccountResponse con información de los estados de cuenta.</returns>
        public static BSS_StatusAccountResponse GetBSS_StatusAccount(Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            POSTPAID_STATUS_ACCOUNT.consultarResponseType oConsultarResponse = new POSTPAID_STATUS_ACCOUNT.consultarResponseType();
            BSS_StatusAccountResponse oGetBSS_StatusAccountResponse = new BSS_StatusAccountResponse();
            objBSS_StatusAccountRequest.consultStateAccount.txId = "";
            objBSS_StatusAccountRequest.consultStateAccount.codAplication = KEY.AppSettings("strCodApliOAC");
            objBSS_StatusAccountRequest.consultStateAccount.userAplic = objBSS_StatusAccountRequest.Audit.UserName;
            objBSS_StatusAccountRequest.consultStateAccount.queryType = "";
            objBSS_StatusAccountRequest.consultStateAccount.serviceType = KEY.AppSettings("strConsTipoServAjuste");
            objBSS_StatusAccountRequest.consultStateAccount.numAccount = (objBSS_StatusAccountRequest.consultStateAccount.numAccount != null) ? objBSS_StatusAccountRequest.consultStateAccount.numAccount : "";
            objBSS_StatusAccountRequest.consultStateAccount.phoneNumber = "";
            objBSS_StatusAccountRequest.consultStateAccount.flagOnlyBalance = "";
            objBSS_StatusAccountRequest.consultStateAccount.flagOnlyDispute = "";
            objBSS_StatusAccountRequest.consultStateAccount.dateFrom = "";
            objBSS_StatusAccountRequest.consultStateAccount.dateTo = "";
            objBSS_StatusAccountRequest.consultStateAccount.sizePage = KEY.AppSettings("strTamPaginaOAC");
            objBSS_StatusAccountRequest.consultStateAccount.numPage = KEY.AppSettings("strNroPagina");
            try
            {

                Claro.Web.Logging.ExecuteMethod<POSTPAID_STATUS_ACCOUNT.HeaderResponseType>
                (objBSS_StatusAccountRequest.Audit.Session, objBSS_StatusAccountRequest.Audit.Transaction, Configuration.ServiceConfiguration.POSTPAID_STATUS_ACCOUNT, () =>
                {
                    return Configuration.ServiceConfiguration.POSTPAID_STATUS_ACCOUNT.consultar(new POSTPAID_STATUS_ACCOUNT.HeaderRequestType()
                     {
                         canal = "",
                         idAplicacion = Convert.ToInt(KEY.AppSettings("CodAplicacion")),
                         usuarioAplicacion = objBSS_StatusAccountRequest.Audit.UserName,
                         usuarioSesion = objBSS_StatusAccountRequest.Audit.Session,
                         idTransaccionESB = objBSS_StatusAccountRequest.Audit.Transaction,
                         idTransaccionNegocio = "",
                         fechaInicio = DateTime.Now,
                         nodoAdicional = ""
                     }, new POSTPAID_STATUS_ACCOUNT.consultarRequestType()
                     {
                         tipoConsulta = objBSS_StatusAccountRequest.queryType,
                         cuenta = objBSS_StatusAccountRequest.account,
                         periodo = objBSS_StatusAccountRequest.period,
                         numeroDocumentos = objBSS_StatusAccountRequest.numberDocuments,

                         consultarEstadoCuenta = new POSTPAID_STATUS_ACCOUNT.consultarEstadoCuentaType()
                         {
                             txId = objBSS_StatusAccountRequest.consultStateAccount.txId,
                             pCodAplicacion = objBSS_StatusAccountRequest.consultStateAccount.codAplication,
                             pUsuarioAplic = objBSS_StatusAccountRequest.consultStateAccount.userAplic,
                             pTipoConsulta = objBSS_StatusAccountRequest.consultStateAccount.queryType,
                             pTipoServicio = objBSS_StatusAccountRequest.consultStateAccount.serviceType,
                             pCliNroCuenta = objBSS_StatusAccountRequest.consultStateAccount.numAccount,
                             pNroTelefono = objBSS_StatusAccountRequest.consultStateAccount.phoneNumber,
                             pFlagSoloSaldo = objBSS_StatusAccountRequest.consultStateAccount.flagOnlyBalance,
                             pFlagSoloDisputa = objBSS_StatusAccountRequest.consultStateAccount.flagOnlyDispute,
                             pFechaDesde = objBSS_StatusAccountRequest.consultStateAccount.dateFrom,
                             pFechaHasta = objBSS_StatusAccountRequest.consultStateAccount.dateTo,
                             pTamanoPagina = objBSS_StatusAccountRequest.consultStateAccount.sizePage,
                             pNroPagina = objBSS_StatusAccountRequest.consultStateAccount.numPage

                         }
                     }, out oConsultarResponse);
                });


                if (Convert.ToInt(oConsultarResponse.responseStatus.codigoRespuesta) < Claro.Constants.NumberZero)
                {
                    throw new Claro.MessageException("No se pudo realizar la consulta de estado de cuenta");
                }


                oGetBSS_StatusAccountResponse.responseStatus.codeResponse = oConsultarResponse.responseStatus.codigoRespuesta;
                oGetBSS_StatusAccountResponse.responseStatus.descriptionResponse = oConsultarResponse.responseStatus.descripcionRespuesta;

                if (oConsultarResponse.responseStatus.codigoRespuesta == "0")
                {
                    //deudaNoVencida
                    if (oConsultarResponse.responseDataConsultar.deudaNoVencida != null && oConsultarResponse.responseDataConsultar.deudaNoVencida.audit != null && oConsultarResponse.responseDataConsultar.deudaNoVencida.audit.errorCode == "0")
                    {
                        DetailStateAccountCab oDetailStateAccountCab;
                        foreach (var item in oConsultarResponse.responseDataConsultar.deudaNoVencida.xEstadoCuenta)
                        {
                            oDetailStateAccountCab = new DetailStateAccountCab();
                            oDetailStateAccountCab.clientName = item.xNombreCliente;
                            oDetailStateAccountCab.currentDebt = item.xDeudaActual;
                            oDetailStateAccountCab.expiredDebt = item.xDeudaVencida;
                            oDetailStateAccountCab.totalAmountDispute = item.xTotalMontoDisputa;
                            oDetailStateAccountCab.lastInvoiceDate = item.xFechaUltFactura;
                            oDetailStateAccountCab.lastPaymentDate = item.xFechaUtlPago;
                            oDetailStateAccountCab.codAccount = item.xCodCuenta;
                            oDetailStateAccountCab.codAlternateAccount = item.xCodCuentaAlterna;
                            oDetailStateAccountCab.ubigeoDescription = item.xDescUbigeo;
                            oDetailStateAccountCab.clientType = item.xTipoCliente;
                            oDetailStateAccountCab.stateAccount = item.xEstadoCuenta;
                            oDetailStateAccountCab.activationDate = item.xFechaActivacion;
                            oDetailStateAccountCab.billingCycle = item.xCicloFacturacion;
                            oDetailStateAccountCab.creditLimit = item.xLimiteCredito;
                            oDetailStateAccountCab.creditScore = item.xCreditScore;
                            oDetailStateAccountCab.typePay = item.xTipoPago;
                            if (item.xDetalleTrx != null)
                            {
                                DetailStateAccount oDetailStateAccount;
                                foreach (var detail in item.xDetalleTrx)
                                {
                                    oDetailStateAccount = new DetailStateAccount();
                                    oDetailStateAccount.typeDocument = detail.xTipoDocumento;
                                    oDetailStateAccount.nroDocument = detail.xNroDocumento;
                                    oDetailStateAccount.descripDocument = detail.xDescripDocumento;
                                    oDetailStateAccount.stateDocument = detail.xEstadoDocumento;
                                    oDetailStateAccount.registrationDate = detail.xFechaRegistro;
                                    oDetailStateAccount.emissionDate = detail.xFechaEmision;
                                    oDetailStateAccount.expirationDate = detail.xFechaVencimiento;
                                    oDetailStateAccount.typeMoney = detail.xTipoMoneda;
                                    oDetailStateAccount.amountDocument = detail.xMontoDocumento;
                                    oDetailStateAccount.amountFco = detail.xMontoFco;
                                    oDetailStateAccount.amountFinan = detail.xMontoFinan;
                                    oDetailStateAccount.balanceDocument = detail.xSaldoDocumento;
                                    oDetailStateAccount.balanceFco = detail.xSaldoFco;
                                    oDetailStateAccount.balanceFinan = detail.xSaldoFinan;
                                    oDetailStateAccount.amountPEN = detail.xMontoSoles;
                                    oDetailStateAccount.amountUSD = detail.xMontoDolares;
                                    oDetailStateAccount.charge = detail.xCargo;
                                    oDetailStateAccount.payment = detail.xAbono;
                                    oDetailStateAccount.balanceAccount = detail.xSaldoCuenta;
                                    oDetailStateAccount.numOperationPay = detail.xNroOperacionPago;
                                    oDetailStateAccount.datePay = detail.xFechaPago;
                                    oDetailStateAccount.wayPay = detail.xFormaPago;
                                    oDetailStateAccount.docYear = detail.xDocAnio;
                                    oDetailStateAccount.docMonth = detail.xDocMes;
                                    oDetailStateAccount.docYearExpired = detail.xDocAnioVenc;
                                    oDetailStateAccount.docMonthExpired = detail.xDocMesVenc;
                                    oDetailStateAccount.flagChargeCta = detail.xFlagCargoCta;
                                    oDetailStateAccount.nroTicket = detail.xNroTicket;
                                    oDetailStateAccount.amountReclaimed = detail.xMontoReclamado;
                                    oDetailStateAccount.phone = detail.xTelefono;
                                    oDetailStateAccount.user = detail.xUsuario;
                                    oDetailStateAccount.idDocOrigin = detail.xIdDocOrigen;
                                    oDetailStateAccount.descripExtend = detail.xDescripExtend;
                                    oDetailStateAccount.idDocOAC = detail.xIdDocOAC;
                                    oDetailStateAccountCab.detailTrx.Add(oDetailStateAccount);
                                }
                            }
                            oGetBSS_StatusAccountResponse.responseDataConsultar.debtNotExpired.StateAccount.Add(oDetailStateAccountCab);
                        }
                    }


                    //deudaVencida
                    if (oConsultarResponse.responseDataConsultar.deudaVencida != null && oConsultarResponse.responseDataConsultar.deudaVencida.audit != null && oConsultarResponse.responseDataConsultar.deudaVencida.audit.errorCode == "0")
                    {
                        DetailStateAccountCab oDetailStateAccountCab;
                        foreach (var item in oConsultarResponse.responseDataConsultar.deudaVencida.xEstadoCuenta)
                        {
                            oDetailStateAccountCab = new DetailStateAccountCab();
                            oDetailStateAccountCab.clientName = item.xNombreCliente;
                            oDetailStateAccountCab.currentDebt = item.xDeudaActual;
                            oDetailStateAccountCab.expiredDebt = item.xDeudaVencida;
                            oDetailStateAccountCab.totalAmountDispute = item.xTotalMontoDisputa;
                            oDetailStateAccountCab.lastInvoiceDate = item.xFechaUltFactura;
                            oDetailStateAccountCab.lastPaymentDate = item.xFechaUtlPago;
                            oDetailStateAccountCab.codAccount = item.xCodCuenta;
                            oDetailStateAccountCab.codAlternateAccount = item.xCodCuentaAlterna;
                            oDetailStateAccountCab.ubigeoDescription = item.xDescUbigeo;
                            oDetailStateAccountCab.clientType = item.xTipoCliente;
                            oDetailStateAccountCab.stateAccount = item.xEstadoCuenta;
                            oDetailStateAccountCab.activationDate = item.xFechaActivacion;
                            oDetailStateAccountCab.billingCycle = item.xCicloFacturacion;
                            oDetailStateAccountCab.creditLimit = item.xLimiteCredito;
                            oDetailStateAccountCab.creditScore = item.xCreditScore;
                            oDetailStateAccountCab.typePay = item.xTipoPago;
                            if (item.xDetalleTrx != null)
                            {
                                DetailStateAccount oDetailStateAccount;
                                foreach (var detail in item.xDetalleTrx)
                                {
                                    oDetailStateAccount = new DetailStateAccount();
                                    oDetailStateAccount.typeDocument = detail.xTipoDocumento;
                                    oDetailStateAccount.nroDocument = detail.xNroDocumento;
                                    oDetailStateAccount.descripDocument = detail.xDescripDocumento;
                                    oDetailStateAccount.stateDocument = detail.xEstadoDocumento;
                                    oDetailStateAccount.registrationDate = detail.xFechaRegistro;
                                    oDetailStateAccount.emissionDate = detail.xFechaEmision;
                                    oDetailStateAccount.expirationDate = detail.xFechaVencimiento;
                                    oDetailStateAccount.typeMoney = detail.xTipoMoneda;
                                    oDetailStateAccount.amountDocument = detail.xMontoDocumento;
                                    oDetailStateAccount.amountFco = detail.xMontoFco;
                                    oDetailStateAccount.amountFinan = detail.xMontoFinan;
                                    oDetailStateAccount.balanceDocument = detail.xSaldoDocumento;
                                    oDetailStateAccount.balanceFco = detail.xSaldoFco;
                                    oDetailStateAccount.balanceFinan = detail.xSaldoFinan;
                                    oDetailStateAccount.amountPEN = detail.xMontoSoles;
                                    oDetailStateAccount.amountUSD = detail.xMontoDolares;
                                    oDetailStateAccount.charge = detail.xCargo;
                                    oDetailStateAccount.payment = detail.xAbono;
                                    oDetailStateAccount.balanceAccount = detail.xSaldoCuenta;
                                    oDetailStateAccount.numOperationPay = detail.xNroOperacionPago;
                                    oDetailStateAccount.datePay = detail.xFechaPago;
                                    oDetailStateAccount.wayPay = detail.xFormaPago;
                                    oDetailStateAccount.docYear = detail.xDocAnio;
                                    oDetailStateAccount.docMonth = detail.xDocMes;
                                    oDetailStateAccount.docYearExpired = detail.xDocAnioVenc;
                                    oDetailStateAccount.docMonthExpired = detail.xDocMesVenc;
                                    oDetailStateAccount.flagChargeCta = detail.xFlagCargoCta;
                                    oDetailStateAccount.nroTicket = detail.xNroTicket;
                                    oDetailStateAccount.amountReclaimed = detail.xMontoReclamado;
                                    oDetailStateAccount.phone = detail.xTelefono;
                                    oDetailStateAccount.user = detail.xUsuario;
                                    oDetailStateAccount.idDocOrigin = detail.xIdDocOrigen;
                                    oDetailStateAccount.descripExtend = detail.xDescripExtend;
                                    oDetailStateAccount.idDocOAC = detail.xIdDocOAC;
                                    oDetailStateAccountCab.detailTrx.Add(oDetailStateAccount);

                                }
                            }
                            oGetBSS_StatusAccountResponse.responseDataConsultar.debtExpired.StateAccount.Add(oDetailStateAccountCab);
                        }
                    }

                    //listaCobranzaDiferida
                    if (oConsultarResponse.responseDataConsultar.listaCobranzaDiferida != null)
                    {
                        ReceivableDeferred oReceivableDeferred;
                        foreach (var item in oConsultarResponse.responseDataConsultar.listaCobranzaDiferida)
                        {
                            oReceivableDeferred = new ReceivableDeferred();
                            oReceivableDeferred.account = item.cuenta;
                            oReceivableDeferred.businessName = item.razonSocial;
                            oReceivableDeferred.nroOcc = item.nroOcc;
                            oReceivableDeferred.selectedInvoice = item.factSeleccionada;
                            oReceivableDeferred.periods = item.periodos;
                            oReceivableDeferred.amount = item.importe;
                            oReceivableDeferred.nameOcc = item.nombreOcc;
                            oReceivableDeferred.commentary = item.comentario;
                            oReceivableDeferred.admissionDate = item.fechaIngreso;
                            oReceivableDeferred.accountingAccount = item.cuentaContable;
                            oReceivableDeferred.dateValidity = item.fechaValidez;
                            oGetBSS_StatusAccountResponse.responseDataConsultar.ListReceivableDeferred.Add(oReceivableDeferred);
                        }
                    }

                    //listaReclamoPorRecibo
                    if (oConsultarResponse.responseDataConsultar.listaReclamoPorRecibo != null)
                    {
                        ClaimByReceipt oClaimByReceipt;
                        foreach (var item in oConsultarResponse.responseDataConsultar.listaReclamoPorRecibo)
                        {
                            oClaimByReceipt = new ClaimByReceipt();
                            oClaimByReceipt.claim = item.reclamo;
                            oClaimByReceipt.creation = item.creacion;
                            oClaimByReceipt.interaction = item.interaccion;
                            oClaimByReceipt.amount = item.monto;
                            oClaimByReceipt.state = item.estado;
                            oClaimByReceipt.document = item.documento;
                            oClaimByReceipt.idCase = item.caso;
                            oGetBSS_StatusAccountResponse.responseDataConsultar.ListClaimByReceipt.Add(oClaimByReceipt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objBSS_StatusAccountRequest.Audit.Session, objBSS_StatusAccountRequest.Audit.Transaction, ex.Message);
                oGetBSS_StatusAccountResponse = null;
            }


            return oGetBSS_StatusAccountResponse;
        }

        public static Int64 GetInteraction(string strIdSession, string strTransaction, string strDocument)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("PI_NUM_DOC_AJUSTES", DbType.String, 255, ParameterDirection.Input, strDocument),
                new DbParameter("PO_INTERACCION", DbType.Int64, ParameterDirection.Output),
                new DbParameter("PO_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("PO_MSG", DbType.String, 255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_INTERACCION, parameters);
            Claro.Web.Logging.Info(strIdSession, strTransaction, parameters[1].Value != null ? parameters[1].Value.ToString() : "es null param 1");
            Claro.Web.Logging.Info(strIdSession, strTransaction, parameters[2].Value != null ? parameters[1].Value.ToString() : "es null param 2");
            Claro.Web.Logging.Info(strIdSession, strTransaction, parameters[3].Value != null ? parameters[1].Value.ToString() : "es null param 3");
            //  Claro.Web.Logging.Info(strIdSession, strTransaction, "Id Interacción: " + parameters[1].Value.ToString() + " Resultado: " + parameters[2].Value.ToString() + " Mensaje: " + parameters[3].Value.ToString());
            return Convert.ToInt64(parameters[1].Value.ToString());
        }

        public static List<DataHistorical> getDataHistory(string strIdSession, string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID)
        {
            List<DataHistorical> Lista = new List<DataHistorical>();
            POSTPAID_CAMBIODATOS.obtenerDataHistoricaResponse oResponse = null;
            try
            {

                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

                POSTPAID_CAMBIODATOS.obtenerDataHistoricaRequest oRequest = new POSTPAID_CAMBIODATOS.obtenerDataHistoricaRequest()
                {
                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    customerId = Convert.ToInt(strCustomerID)
                };



                oResponse = oService.obtenerDataHistorica(oRequest);

                if (oResponse.listaAddress != null)
                {
                    foreach (var item in oResponse.listaAddress)
                    {
                        DataHistorical temp = new DataHistorical();

                        temp.CustomerId = item.customerId;
                        temp.NroDoc = item.dniRuc;
                        temp.Fax = item.dniRuc2;
                        temp.LegalRep = item.repreLegal;
                        temp.DocType = item.tipoDocumentoRep;
                        temp.BusinessName = item.razonSocial;
                        temp.FirstName = item.nombres;
                        temp.LastName = item.apellido;
                        temp.Telephone = item.numTelefono;
                        temp.Movil = item.numCelular;
                        temp.Email = item.email;
                        temp.Contact = item.contactoCliente;
                        temp.NationalityDesc = item.nacionalidad;
                        temp.Sex = item.sexo;
                        temp.MaritalStatus = item.estadoCivil;

                        temp.AddressFact = item.direccion_fact;
                        temp.AddressNoteFact = item.notasDireccion_fact;
                        temp.DistrictFact = item.distrito_fact;
                        temp.ProvinceFact = item.provincia_fact;
                        temp.DepartmentFact = item.departamento_fact;
                        temp.CountryFact = item.pais;

                        temp.AddressLegal = item.direccion_leg;
                        temp.AddressNoteLegal = item.notasDireccion_leg;
                        temp.DistrictLegal = item.distrito_leg;
                        temp.ProvinceLegal = item.provincia_leg;
                        temp.DepartmentLegal = item.departamento_leg;
                        temp.CountryLegal = item.pais;

                        temp.ChangeMot = item.motivo;
                        temp.fechaRegistroCli = item.fechaRegistro.Substring(8, 2) + "/" + item.fechaRegistro.Substring(5, 2) + "/" + item.fechaRegistro.Substring(0, 4);
                        temp.updCliente = item.updCliente;
                        temp.updDataMinor = item.updDataMinor;
                        temp.updDirLegal = item.updDirLegal;
                        temp.updDirFac = item.updDirFac;
                        temp.updRepLegal = item.updRepLegal;
                        temp.updContact = item.updContact;
                        Lista.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, ex.Message);
            }
            return Lista;

        }

        public static List<DataHistorical> getDataHistoryTobe(Claro.Entity.AuditRequest audit, string strCustomerID, string flagconvivencia)
        {
            List<DataHistorical> Lista = new List<DataHistorical>();
            var flagconv = string.Empty;
            try
            {
                //Flag = 0 -> Alta Nueva valor 1 al servicio
                if (flagconvivencia.Equals(Claro.Constants.NumberZeroString))
                {
                    flagconv = Claro.Constants.NumberTwoString;
                }//Flag = 1 -> Migrado  bsc7 a bsc9 valor 1 al servicio
                else
                {
                    flagconv = Claro.Constants.NumberOneString;
                }

                Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Request.request()
                {
                    consultarHistoricoDatosRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Request.ConsultarHistoricoDatosRequest()
                    {
                        customerId = strCustomerID,
                        listaOpcional = new Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Common.listaOpcional() { clave = "flagConvivencia", valor = flagconv }
                    }
                };

                Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Response.response response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataHistory.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_DATA_HISTORICO, audit, request, false);

                if (response != null &&
                    response.consultarHistoricoDatosResponse != null &&
                    response.consultarHistoricoDatosResponse.responseAudit != null &&
                    response.consultarHistoricoDatosResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    response.consultarHistoricoDatosResponse.responseData != null &&
                    response.consultarHistoricoDatosResponse.responseData.listaHistoricoDatos != null)
                {

                    foreach (var item in response.consultarHistoricoDatosResponse.responseData.listaHistoricoDatos)
                    {
                        DataHistorical temp = new DataHistorical();

                        temp.CustomerId = item.customerId;
                        temp.NroDoc = item.dniRuc;
                        temp.Fax = item.dniRucRepLegal;
                        temp.LegalRep = item.repreLegal;
                        temp.DocType = item.tipoDocRepLegal;
                        temp.BusinessName = item.razonSocial;
                        temp.FirstName = item.nombres;
                        temp.LastName = item.apellido;
                        temp.Telephone = item.numTelefono;
                        temp.Movil = item.numCelular;
                        temp.Email = item.email;
                        temp.Contact = item.contactoCliente;
                        temp.NationalityDesc = item.nacionalidad;
                        temp.Sex = item.sexo;
                        temp.MaritalStatus = item.estadoCivil;

                        temp.AddressFact = item.direccionFact;
                        temp.AddressNoteFact = item.notasDireccionFact;
                        temp.DistrictFact = item.distritoFact;
                        temp.ProvinceFact = item.provinciaFact;
                        temp.DepartmentFact = item.departamentoFact;
                        temp.CountryFact = item.pais;

                        temp.AddressLegal = item.direccionLeg;
                        temp.AddressNoteLegal = item.notasDireccionLeg;
                        temp.DistrictLegal = item.distritoLeg;
                        temp.ProvinceLegal = item.provinciaLeg;
                        temp.DepartmentLegal = item.departamentoLeg;
                        temp.CountryLegal = item.pais;

                        temp.ChangeMot = item.motivo;
                        temp.fechaRegistroCli = string.IsNullOrEmpty(item.fechaRegistro) ? string.Empty : item.fechaRegistro.Substring(8, 2) + "/" + item.fechaRegistro.Substring(5, 2) + "/" + item.fechaRegistro.Substring(0, 4);
                        temp.fechaComp = item.fechaRegistro;
                        temp.updCliente = item.updCliente;
                        temp.updDataMinor = item.updDataMinor;
                        temp.updDirLegal = item.updDirLegal;
                        temp.updDirFac = item.updDirFac;
                        temp.updRepLegal = item.updRepLegal;
                        temp.updContact = item.updContact;

                        Lista.Add(temp);
                    }

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }
            Lista = Lista.OrderBy(p => DateTime.Parse(p.fechaComp)).Reverse().ToList();
            Claro.Web.Logging.Info(audit.Session, audit.Transaction, new JavaScriptSerializer().Serialize(Lista));
            return Lista;

        }

        public static List<Customer> GetTypeDocumentDeubt(string strIdSession, string strTransaction, string strDocLinea, string strCod_consulta, out string strout_err_code, out string out_err_msg)
        {
            List<Customer> lstCustomer = null;


            DbParameter[] parameters = new[] {

               new DbParameter("in_docolinea", DbType.String,255 ,ParameterDirection.Input, strDocLinea),
               new DbParameter("in_codcons", DbType.Decimal, ParameterDirection.Input, strCod_consulta),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("out_err_code", DbType.Decimal, ParameterDirection.Output),
                new DbParameter("out_err_msg", DbType.String,255, ParameterDirection.Output)
               
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_DOC_OAC, parameters, (IDataReader reader) =>
            {
                lstCustomer = new List<Customer>();
                while (reader.Read())
                {

                    lstCustomer.Add(new Customer()
                    {
                        TIPO_DOC = Convert.ToString(reader["Doc OAC TYPE"].ToString()),
                        NRO_DOC = Convert.ToString(reader["CSCOMPREGNO"].ToString())
                    });

                    break;
                }
            });
            strout_err_code = parameters[3].Value.ToString();
            out_err_msg = parameters[4].Value.ToString();

            return lstCustomer;
        }

        /// <summary>
        /// Método que obtiene una lista de Consulta de sucursales por cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos sucursales.</returns>

        public static List<Branch> GetBranch(string strIdSession, string strTransaction, string strCustomerId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("an_customer_id", DbType.Int32, ParameterDirection.Input, Convert.ToInt(strCustomerId)),
                new DbParameter("a_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Branch> listBranch = null;

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_SUCURSALES_CLIENTE, parameters, (IDataReader reader) =>
            {
                listBranch = new List<Branch>();

                while (reader.Read())
                {
                    listBranch.Add(new Branch()
                    {
                        codsolot = Convert.ToString(reader["CODSOLOT"]),
                        tipotrabajo = Convert.ToString(reader["TIPTRA"]),
                        descripcion = Convert.ToString(reader["DESCRIPCION"]),
                        sucursal = Convert.ToString(reader["SUCURSAL"]),
                        codcliente = Convert.ToString(reader["CODCLIENTE"]),
                        contrato = Convert.ToString(reader["CONTRATO"]),
                        notas = Convert.ToString(reader["NOTAS"]),
                        departamento = Convert.ToString(reader["DEPARTAMENTO"]),
                        provincia = Convert.ToString(reader["PROVINCIA"]),
                        distrito = Convert.ToString(reader["DISTRITO"]),
                        plano = Convert.ToString(reader["PLANO"]),
                        ubigeo = Convert.ToString(reader["UBIGEO"])
                    });

                }
            });

            Claro.Web.Logging.Info(strIdSession, strTransaction, "listBranch:  " + listBranch.Count);
            return listBranch;

        }


        public static Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse GetBonusStatusFullClaro(Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse objResponse = new Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse();

            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetBonusStatusFullClaro: Parámetros de entrada: [coId] - {0} ", objRequest.MessageRequest.Body.coId));
                System.Collections.Hashtable objValues = new System.Collections.Hashtable();
                objValues.Add("coId", objRequest.MessageRequest.Body.coId);

                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                string.Format("Parámetros de Entrada: [coId] - {0} ; ", objRequest.MessageRequest.Body.coId));

                objResponse = RestService.GetInvoqueDP<Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_STATUS_BONO_FULLCLARO, objRequest.Audit, objValues, getCredentials(objRequest.Audit, "strUserDPFUllClaroEncript", "strPassDPFUllClaroEncript"), getAcceptRequest(objRequest.Audit), null);

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ; [mensajeError] - {2} ; " + "[codigoEtiqueta1] - {3} ; [nombreEtiqueta1] - {4} ; [codigoEtiqueta2] - {5} ; " +
                     "[nombreEtiqueta2] - {6}", objResponse.MessageResponse.Body.codigoRespuesta, objResponse.MessageResponse.Body.mensajeRespuesta, objResponse.MessageResponse.Body.mensajeError, objResponse.MessageResponse.Body.codigoEtiqueta1, objResponse.MessageResponse.Body.nombreEtiqueta1, objResponse.MessageResponse.Body.codigoEtiqueta2, objResponse.MessageResponse.Body.nombreEtiqueta2));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetBonusStatusFullClaro: Error al invocar al servicio");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static Claro.Entity.UsernameToken getCredentials(Claro.Entity.AuditRequest objAuditRequest, string keyUser, string keyPass)
        {
            UsernameToken objCrendentials = null;
            string User = KEY.AppSettings(keyUser);
            string pass = KEY.AppSettings(keyPass);
            objCrendentials = Decrypt(objAuditRequest, User, pass);
            return objCrendentials;
        }


        private static UsernameToken Decrypt(Claro.Entity.AuditRequest objAuditRequest, string user, string pass)
        {
            UsernameToken objCrendentials;
            string User = string.Empty;
            string Password = string.Empty;
            string resStatus = string.Empty;

            try
            {
                Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Inicio de Ejecución del WS ConsultaClaves - Método Desencriptar");
                POSTPAID_CONSULTCLAVE.desencriptarRequestBody objRequest = new POSTPAID_CONSULTCLAVE.desencriptarRequestBody();
                POSTPAID_CONSULTCLAVE.desencriptarResponseBody objResponse = new POSTPAID_CONSULTCLAVE.desencriptarResponseBody();


                objRequest.idTransaccion = objAuditRequest.Transaction;
                objRequest.ipAplicacion = "";
                objRequest.ipTransicion = "";
                objRequest.usrAplicacion = objAuditRequest.UserName;
                objRequest.idAplicacion = KEY.AppSettings("CodAplicacion");
                objRequest.codigoAplicacion = KEY.AppSettings("strCodigoAplicacionDP");
                objRequest.usuarioAplicacionEncriptado = user;
                objRequest.claveEncriptado = pass;

                Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.Transaction,
                   string.Format("Parámetros de entrada: [idTransaccion] - {0} ; [ipAplicacion] - {1} ; [ipTransicion] - {2} ; " +
                   "[usrAplicacion] - {3} ; [idAplicacion] - {4} ; [codigoAplicacion] - {5} ; " +
                   "[usuarioAplicacionEncriptado] - {6} ; [claveEncriptado] - {7}", objRequest.idTransaccion, objRequest.ipAplicacion, objRequest.ipTransicion, objRequest.usrAplicacion,
                   objRequest.idAplicacion, objRequest.codigoAplicacion, "****", "****"));

                Claro.Web.Logging.Info("desencriptarResponseBody: objResponse.codigoResultado", objRequest.usuarioAplicacionEncriptado, objRequest.claveEncriptado);

                objResponse.codigoResultado = Claro.SIACU.Data.Configuration.ServiceConfiguration.CONSULTCLAVE.desencriptar(ref objRequest.idTransaccion, objRequest.ipAplicacion, objRequest.ipTransicion, objRequest.usrAplicacion, objRequest.idAplicacion, objRequest.codigoAplicacion, objRequest.usuarioAplicacionEncriptado, objRequest.claveEncriptado, out resStatus, out User, out Password);

                if (objResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Codigo resultado  0, desencriptado con exito - Método Desencriptar");
                }
                else
                {
                    Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Codigo resultado diferente de 0, error  al desencriptar - Método Desencriptar");
                }

                Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Fin de Ejecución del WS ConsultaClaves - Método Desencriptar");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
                throw new Claro.MessageException(objAuditRequest.Transaction);
            }

            objCrendentials = new Claro.Entity.UsernameToken()
            {
                Username = User,
                Password = new Claro.Entity.Password()
                {
                    Value = Password
                }
            };
            return objCrendentials;
        }

        public static bool getActivateLogRequestRest(Claro.Entity.AuditRequest Audit)
        {
            bool keyActivateLog = false;
            string keyLogRest = "";
            try
            {
                keyLogRest = Claro.ConfigurationManager.AppSettings("flagLogRequestRest");
                keyActivateLog = keyLogRest == "1";
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(Audit.Session, Audit.Transaction, ex.Message);
                keyActivateLog = false;
            }
            return keyActivateLog;
        }

        public static string getAcceptRequest(Claro.Entity.AuditRequest Audit)
        {
            string keyAccept = "";
            try
            {
                keyAccept = Claro.ConfigurationManager.AppSettings("keyAcceptRequestRest");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(Audit.Session, Audit.Transaction, ex.Message);
                keyAccept = "application/json";
            }
            return keyAccept;
        }

        public static Entity.Dashboard.Postpaid.Customer GetPreviousCustomerHFC(string strIdSession, string strTransaction, string strTelephone, string strAccount, string strContactId, string strFlagRegistry, out string strFlagConsulta)
        {
            Entity.Dashboard.Postpaid.Customer objCustomer = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PHONE", DbType.String, ParameterDirection.Input, strTelephone),
                new DbParameter("P_ACCOUNT", DbType.String, ParameterDirection.Input,strAccount),
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64, ParameterDirection.Input,  (strContactId == "" ? Claro.Constants.NumberZero : Convert.ToInt(strContactId))),
                new DbParameter("P_FLAG_REG", DbType.String, 20, ParameterDirection.Input, strFlagRegistry),
                new DbParameter("P_FLAG_CONSULTA", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC, parameters, (IDataReader reader) =>
            {

                if (reader.Read())
                {
                    objCustomer = new Entity.Dashboard.Postpaid.Customer();
                    objCustomer.OBJID_CONTACTO = Convert.ToString(reader["OBJID_CONTACTO"]);
                    objCustomer.OBJID_SITE = Convert.ToString(reader["OBJID_SITE"]);
                    objCustomer.TELEFONO = Convert.ToString(reader["TELEFONO"]);
                    objCustomer.CUENTA = Convert.ToString(reader["CUENTA"]);
                    objCustomer.MODALIDAD = Convert.ToString(reader["MODALIDAD"]);
                    objCustomer.SEGMENTO = Convert.ToString(reader["SEGMENTO"]);

                    objCustomer.ROL_CONTACTO = Convert.ToString(reader["ROL_CONTACTO"]);
                    objCustomer.ESTADO_CONTACTO = Convert.ToString(reader["ESTADO_CONTACTO"]);
                    objCustomer.ESTADO_CONTRATO = Convert.ToString(reader["ESTADO_CONTRATO"]);
                    objCustomer.ESTADO_SITE = Convert.ToString(reader["ESTADO_SITE"]);
                    objCustomer.S_NOMBRES = Convert.ToString(reader["S_NOMBRES"]);
                    objCustomer.S_APELLIDOS = Convert.ToString(reader["S_APELLIDOS"]);
                    objCustomer.NOMBRES = Convert.ToString(reader["NOMBRES"]);
                    objCustomer.APELLIDOS = Convert.ToString(reader["APELLIDOS"]);

                    objCustomer.DOMICILIO = Convert.ToString(reader["DOMICILIO"]);
                    objCustomer.URBANIZACION = Convert.ToString(reader["URBANIZACION"]);
                    objCustomer.REFERENCIA = Convert.ToString(reader["REFERENCIA"]);
                    objCustomer.CIUDAD = Convert.ToString(reader["CIUDAD"]);
                    objCustomer.DISTRITO = Convert.ToString(reader["DISTRITO"]);
                    objCustomer.DEPARTAMENTO = Convert.ToString(reader["DEPARTAMENTO"]);

                    objCustomer.ZIPCODE = Convert.ToString(reader["ZIPCODE"]);
                    objCustomer.EMAIL = Convert.ToString(reader["EMAIL"]);
                    objCustomer.TELEF_REFERENCIA = Convert.ToString(reader["TELEF_REFERENCIA"]);
                    objCustomer.FAX = Convert.ToString(reader["FAX"]);
                    objCustomer.FECHA_NAC = Convert.ToString(reader["FECHA_NAC"]);
                    objCustomer.SEXO = Convert.ToString(reader["SEXO"]);

                    objCustomer.ESTADO_CIVIL = Convert.ToString(reader["ESTADO_CIVIL"]);
                    objCustomer.TIPO_DOC = Convert.ToString(reader["TIPO_DOC"]);
                    objCustomer.NRO_DOC = Convert.ToString(reader["NRO_DOC"]);
                    objCustomer.FECHA_ACT = DateTime.Parse(reader["FECHA_ACT"].ToString());
                    objCustomer.PUNTO_VENTA = Convert.ToString(reader["PUNTO_VENTA"]);
                    objCustomer.FLAG_REGISTRADO = Convert.ToInt(reader["FLAG_REGISTRADO"]);

                    objCustomer.OCUPACION = Convert.ToString(reader["OCUPACION"]);
                    objCustomer.CANT_REG = Convert.ToString(reader["CANT_REG"]);
                    objCustomer.FLAG_EMAIL = Convert.ToString(reader["FLAG_EMAIL"]);
                    objCustomer.MOTIVOREGISTRO = Convert.ToString(reader["MOTIVO_REGISTRO"]);
                    objCustomer.FUNCION = Convert.ToString(reader["FUNCION"]);
                    objCustomer.CARGO = Convert.ToString(reader["CARGO"]);
                    objCustomer.LUGAR_NACIMIENTO_DES = Convert.ToString(reader["LUGAR_NAC"]);


                }

            });

            strFlagConsulta = parameters[4].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[4].Value.ToString());
            return objCustomer;
        }

        public static List<Entity.Dashboard.Postpaid.Customer> GetCustomer(string strIdSession, string strTransaction, string strTelephone)
        {
            string strMensaje = string.Empty;
            List<Entity.Dashboard.Postpaid.Customer> oCustomers = new List<Entity.Dashboard.Postpaid.Customer>();

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("PI_CUSTOMER", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("PO_MESSAGE", DbType.String,255, ParameterDirection.Output),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SIACSS_CLIENTE_CUSTOMER, parameters, (IDataReader reader) =>
            {
                if (reader.Read())
                {
                    oCustomers.Add(
                        new Entity.Dashboard.Postpaid.Customer
                        {
                            TELEFONO = Convert.ToString(reader["TELEFONO"]),
                            CUENTA = Convert.ToString(reader["CUENTA"]),
                            CUSTOMER_ID = Convert.ToString(reader["CUSTOMER_ID"]),
                            CUSTOMER_ID_P = Convert.ToString(reader["CUSTOMER_ID_P"]),
                            NOMBRES = Convert.ToString(reader["NOMBRES"]),
                            APELLIDOS = Convert.ToString(reader["APELLIDOS"]),
                            RAZON_SOCIAL = Convert.ToString(reader["RAZON_SOCIAL"]),
                            TIPO_DOC = Convert.ToString(reader["TIPO_DOC"]),
                            NRO_DOC = Convert.ToString(reader["NRO_DOC"]),
                            DOMICILIO = Convert.ToString(reader["DOMICILIO"]),
                            DISTRITO = Convert.ToString(reader["DISTRITO"]),
                            DEPARTAMENTO = Convert.ToString(reader["DEPARTAMENTO"]),
                            TIPO_CLIENTE = Convert.ToString(reader["TIPO_CLIENTE"]),
                            EMAIL = Convert.ToString(reader["EMAIL"]),
                            PLAN = Convert.ToString(reader["PLAN"]),
                            NACIONALIDAD = Convert.ToInt(reader["NACIONALIDAD"])
                        });
                }
            });

            strMensaje = parameters[1].Value.ToString() == Claro.SIACU.Constants.NullString ? "" : Convert.ToString(parameters[1].Value.ToString());

            Claro.Web.Logging.Info(strIdSession, strTransaction, strMensaje);


            return oCustomers;
        }

        public static bool CreateCustomer(string strIdSession, string strTransaction, Entity.Dashboard.Postpaid.Customer objCustomer, string strUsuario, ref string strObjContact)
        {
            bool boolOut = false;
            string strFlag = string.Empty;
            string strMensaje = string.Empty;
            string strphone = Claro.Constants.LetterH + objCustomer.CUSTOMER_ID;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_phone", DbType.String, ParameterDirection.Input, strphone),
                new DbParameter("p_account", DbType.String, ParameterDirection.Input, objCustomer.CUENTA),
                new DbParameter("p_cust_id", DbType.String, ParameterDirection.Input, objCustomer.CUSTOMER_ID),
                new DbParameter("p_cust_id_p", DbType.String, ParameterDirection.Input, objCustomer.CUSTOMER_ID_P),
                new DbParameter("p_usuario", DbType.String, ParameterDirection.Input, strUsuario),
                new DbParameter("p_nombres", DbType.String, ParameterDirection.Input, objCustomer.NOMBRES),
                new DbParameter("p_apellidos", DbType.String, ParameterDirection.Input, objCustomer.APELLIDOS),
                new DbParameter("p_razonsocial", DbType.String, ParameterDirection.Input, objCustomer.RAZON_SOCIAL),
                new DbParameter("p_tipo_doc", DbType.String, ParameterDirection.Input, objCustomer.TIPO_DOC),
                new DbParameter("p_num_doc", DbType.String, ParameterDirection.Input, objCustomer.NRO_DOC),
                new DbParameter("p_domicilio", DbType.String, ParameterDirection.Input, objCustomer.DOMICILIO), 
                new DbParameter("p_distrito", DbType.String, ParameterDirection.Input, objCustomer.DISTRITO),
                new DbParameter("p_departamento", DbType.String, ParameterDirection.Input, objCustomer.DEPARTAMENTO),
                new DbParameter("p_modalidad", DbType.String, ParameterDirection.Input, objCustomer.MODALIDAD),
                new DbParameter("p_correo", DbType.String, ParameterDirection.Input, objCustomer.EMAIL),
                new DbParameter("p_plan", DbType.Int64, ParameterDirection.Input, objCustomer.PLAN),
                new DbParameter("p_flag_insert", DbType.String,255, ParameterDirection.Output),
                new DbParameter("p_msg_text", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CREATE_CONTACT_POSTPAGO, parameters);

            strFlag = Convert.ToString(parameters[16].Value);
            strMensaje = Convert.ToString(parameters[17].Value);

            if (string.Equals(strFlag, Constants.OK))
            {
                boolOut = true;
                strObjContact = strMensaje;

                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Se creo el Cliente Clarify con el id {0} para la linea {1}", strObjContact, strphone));
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al crear el Cliente Clarify para la linea {0}, {1}", strphone, strMensaje));
            }

            return boolOut;
        }

        public static bool UpdateNationality(string strIdSession, string strTransaction, Entity.Dashboard.Postpaid.Customer objCustomer)
        {
            bool boolOut = false;
            string strMessageResult = string.Empty;
            Int64 intCustomerID = Convert.ToInt(objCustomer.CUSTOMER_ID);
            string strphone = Claro.Constants.LetterH + objCustomer.CUSTOMER_ID;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("PI_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input, intCustomerID),
                new DbParameter("PI_NACIONALITY", DbType.Int64, ParameterDirection.Input, objCustomer.NACIONALIDAD),
                new DbParameter("PI_TELEPHONE", DbType.String, ParameterDirection.Input, strphone),
                new DbParameter("PO_CODE_RESULT", DbType.Int64, ParameterDirection.Output),
                new DbParameter("PO_MESSAGE_RESULT", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SIACSU_CLIENTE_NACIONALIDAD, parameters);

            strMessageResult = Convert.ToString(parameters[4].Value);

            if (string.Equals(strMessageResult, Constants.OK))
            {
                boolOut = true;
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Se actualizo la nacionalidad de la linea {0} con exito.", strphone));
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, strMessageResult);
            }

            return boolOut;
        }

        public static bool UpdateClient(string strIdSession, string strTransaction, Entity.Dashboard.Postpaid.Customer objCustomer)
        {
            bool boolOut = false;
            string strMessageResult = string.Empty;
            string strphone = Claro.Constants.LetterH + objCustomer.CUSTOMER_ID;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("PI_TELEPHONE", DbType.String, ParameterDirection.Input, strphone),
                new DbParameter("PI_NUM_DNI", DbType.String, ParameterDirection.Input, objCustomer.NRO_DOC),
                new DbParameter("PI_FIRST_NAME", DbType.String, ParameterDirection.Input, objCustomer.NOMBRES),
                new DbParameter("PI_S_FIRST_NAME", DbType.String, ParameterDirection.Input, objCustomer.NOMBRES),
                new DbParameter("PI_LAST_NAME", DbType.String, ParameterDirection.Input, objCustomer.APELLIDOS),
                new DbParameter("PI_S_LAST_NAME", DbType.String, ParameterDirection.Input, objCustomer.APELLIDOS),
                new DbParameter("PO_CODE_RESULT", DbType.Int64, ParameterDirection.Output),
                new DbParameter("PO_MESSAGE_RESULT", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SIACSU_UPDATE_DATOS_HFC, parameters);

            strMessageResult = Convert.ToString(parameters[7].Value);

            if (string.Equals(strMessageResult, Constants.OK))
            {
                boolOut = true;
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Se actualizo los datos de la linea {0} con exito.", strphone));
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, strMessageResult);
            }

            return boolOut;
        }

        public static Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetDataCustomer2(string strIdSession, string strTransaction, string strIpAplicacion, string strAplicacion, string strUsrApp, string strcustomerid, string strtelefono)
        {

            string strError = "";
            POSTPAID_CAMBIODATOS.obtenerDataClientResponse oResponse = null;
            Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse objGetCustomer = new Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse();
            Entity.Dashboard.Postpaid.Customer objTemp = new Entity.Dashboard.Postpaid.Customer();
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

                POSTPAID_CAMBIODATOS.obtenerDataClientRequest oRequest = new POSTPAID_CAMBIODATOS.obtenerDataClientRequest()
                {
                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strTransaction, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    custCode = strcustomerid,
                    numero = strtelefono
                };


                oResponse = oService.obtenerDataClient(oRequest);

                if (oResponse.auditResponse.codigoRespuesta == "0")
                {
                    objTemp.NOMBRES = oResponse.datosCliente.nombre;
                    objTemp.APELLIDOS = oResponse.datosCliente.apellidos;
                    objTemp.CUENTA = oResponse.datosCliente.cuenta;
                    objTemp.SEXO = oResponse.datosCliente.sexo;
                    objTemp.DOMICILIO = oResponse.datosCliente.direccion_fac;
                    objTemp.ZIPCODE = oResponse.datosCliente.cod_postal_fac;
                    objTemp.DEPARTAMENTO = oResponse.datosCliente.departamento_fac;
                    objTemp.DISTRITO = oResponse.datosCliente.distrito_fac;
                    objTemp.RAZON_SOCIAL = oResponse.datosCliente.razon_social;
                    objTemp.PROVINCIA = oResponse.datosCliente.provincia_fac;
                    objTemp.TIPO_DOC = oResponse.datosCliente.tip_doc_c;
                    objTemp.DNI_RUC = oResponse.datosCliente.ruc_dni;
                    objTemp.ASESOR = oResponse.datosCliente.asesor;
                    objTemp.CICLO_FACTURACION = oResponse.datosCliente.ciclo_fac;
                    objTemp.MODALIDAD = oResponse.datosCliente.modalidad;
                    objTemp.SEGMENTO = oResponse.datosCliente.segmento;
                    objTemp.FECHA_ACT = Convert.ToDate(oResponse.datosCliente.fecha_act);
                    objTemp.TIPO_CLIENTE = oResponse.datosCliente.tipo_cliente;
                    objTemp.REPRESENTANTE_LEGAL = oResponse.datosCliente.rep_legal;
                    objTemp.EMAIL = oResponse.datosCliente.email;
                    objTemp.TELEF_REFERENCIA = oResponse.datosCliente.telef_principal;
                    objTemp.CONTACTO_CLIENTE = oResponse.datosCliente.contacto_cliente;
                    objTemp.NRO_DOC = oResponse.datosCliente.num_doc;
                    objTemp.NOMBRE_COMERCIAL = oResponse.datosCliente.nomb_comercial;
                    objTemp.FAX = oResponse.datosCliente.fax;
                    objTemp.CARGO = oResponse.datosCliente.cargo;
                    objTemp.CUSTOMER_ID = oResponse.datosCliente.customer_id;
                    objTemp.CONTACTO_CLIENTE = oResponse.datosCliente.contacto_cliente;
                    objTemp.TELEFONO_CONTACTO = oResponse.datosCliente.telef_contacto;
                    objTemp.FORMA_PAGO = oResponse.datosCliente.direccion_fac;
                    objTemp.POSTAL_FAC = oResponse.datosCliente.cod_postal_fac;
                    objTemp.URBANIZACION_FAC = oResponse.datosCliente.urbanizacion_fac;
                    objTemp.DEPARTEMENTO_FAC = oResponse.datosCliente.departamento_fac;
                    objTemp.PROVINCIA_FAC = oResponse.datosCliente.provincia_fac;
                    objTemp.DISTRITO_FAC = oResponse.datosCliente.distrito_fac;
                    objTemp.TIPO_CLIENTE = oResponse.datosCliente.direccion_leg;
                    objTemp.POSTAL_LEGAL = oResponse.datosCliente.cod_postal_leg;
                    objTemp.URBANIZACION_LEGAL = oResponse.datosCliente.urbanizacion_leg;
                    objTemp.DEPARTEMENTO_LEGAL = oResponse.datosCliente.departamento_leg;
                    objTemp.PROVINCIA_LEGAL = oResponse.datosCliente.provincia_leg;
                    objTemp.DISTRITO_LEGAL = oResponse.datosCliente.distrito_leg;
                    objTemp.PAIS_FAC = oResponse.datosCliente.pais_fac;
                    objTemp.PAIS_LEGAL = oResponse.datosCliente.pais_leg;
                    objTemp.REFERENCIA = oResponse.datosCliente.urbanizacion_fac;
                    objTemp.CONTRATO_ID = oResponse.datosCliente.co_id;
                    objTemp.LUGAR_NACIMIENTO_DES = oResponse.datosCliente.lug_nac;
                    objTemp.LUGAR_NACIMIENTO_ID = oResponse.datosCliente.nacionalidad;
                    objTemp.FECHA_NAC = oResponse.datosCliente.fecha_nac.Substring(8, 2) + "/" + oResponse.datosCliente.fecha_nac.Substring(5, 2) + "/" + oResponse.datosCliente.fecha_nac.Substring(0, 4);
                    objTemp.ESTADO_CIVIL = oResponse.datosCliente.estado_civil;
                    objTemp.ESTADO_CIVIL_ID = oResponse.datosCliente.estado_civil_id;
                    objTemp.FORMA_PAGO = oResponse.datosCliente.forma_pago;
                    objTemp.COD_TIPO_CLIENTE = oResponse.datosCliente.codigo_tipo_cliente;
                }

                objGetCustomer.ObjCustomer = objTemp;
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("strError: {0}", strError));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Error: {0}", ex.Message));
                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Error: {0}", ex.InnerException));
            }
            return objGetCustomer;
        }

        public static Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse GetBonusStatusFullClaroClient(Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse objResponse = new Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse();

            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetBonusStatusFullClaroClient: Parámetros de entrada: [nroDocumento] - {0} ; [tipoDocumento] - {1}  ", objRequest.MessageRequest.Body.nroDocumento, objRequest.MessageRequest.Body.tipoDocumento));
                System.Collections.Hashtable objValues = new System.Collections.Hashtable();
                string strKeyGetNroDoc = KEY.AppSettings("strGetFCClientNroDoc");
                string strKeyGetTypDoc = KEY.AppSettings("strGetFCClientTypDoc");
                objValues.Add(strKeyGetNroDoc, objRequest.MessageRequest.Body.nroDocumento);
                objValues.Add(strKeyGetTypDoc, objRequest.MessageRequest.Body.tipoDocumento);

                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                string.Format("Parámetros de Entrada: [{0}] - {1} ; [{2}] - {3} ;", strKeyGetNroDoc, objRequest.MessageRequest.Body.nroDocumento, strKeyGetTypDoc, objRequest.MessageRequest.Body.tipoDocumento));

                objResponse = RestService.GetInvoqueDP<Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_STATUS_BONO_FULLCLARO_DOC, objRequest.Audit, objValues, getCredentials(objRequest.Audit, "strUserDPFullClaroClientEncript", "strPassDPFullClaroClientEncript"), getAcceptRequest(objRequest.Audit), null);

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ; [mensajeError] - {2} ; " + "[codigoEtiqueta1] - {3} ; [nombreEtiqueta1] - {4} ; ", objResponse.MessageResponse.Body.codigoRespuesta, objResponse.MessageResponse.Body.mensajeRespuesta, objResponse.MessageResponse.Body.mensajeError, objResponse.MessageResponse.Body.codigoEtiqueta1, objResponse.MessageResponse.Body.nombreEtiqueta1));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetBonusStatusFullClaroClient: Error al invocar al servicio");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse GetValidateCustomer
            (Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerRequest objRequest,
            Claro.Entity.AuditRequest auditRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse objResponse = null;

            try
            {
                Hashtable audit = new Hashtable();
                audit.Add("idTransaccion", auditRequest.Transaction);
                audit.Add("msgId", auditRequest.Transaction);
                audit.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                audit.Add("userId", auditRequest.UserName);

                Claro.Web.Logging.Info(auditRequest.Transaction, auditRequest.Session,
                    string.Format("Ejecucion de metodo GetValidateCustomer: Parámetros de entrada: [nroDocumento] - {0} ; [tipoDocumento] - {1}  ",
                    objRequest.validarClienteRequest.numeroDocumento, objRequest.validarClienteRequest.tipoDocumento));

                objResponse = RestService.PostInvoqueSDP<Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.VALIDAR_CLIENTE, audit, objRequest);


                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(auditRequest.Transaction, auditRequest.Session,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ; " + "[codigoEtiqueta1] - {2} ; [nombreEtiqueta1] - {3} ; ",
                     objResponse.validarClienteResponse.responseAudit.codigoRespuesta, objResponse.validarClienteResponse.responseAudit.mensajeRespuesta, objResponse.validarClienteResponse.responseData.codigoEtiqueta, objResponse.validarClienteResponse.responseData.nombreEtiqueta));
                }
                else
                {
                    Claro.Web.Logging.Error(auditRequest.Transaction, auditRequest.Session, "GetValidateCustomer: Error al invocar al servicio");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(auditRequest.Transaction, auditRequest.Session, ex.Message);
            }

            return objResponse;
        }

        public static Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse GetValidateLine
            (Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineRequest objRequest,
            Claro.Entity.AuditRequest auditRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse objResponse = null;

            try
            {
                Hashtable audit = new Hashtable();
                audit.Add("idTransaccion", auditRequest.Transaction);
                audit.Add("msgId", auditRequest.Transaction);
                audit.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                audit.Add("userId", auditRequest.UserName);

                Claro.Web.Logging.Info(auditRequest.Transaction, auditRequest.Session,
                    string.Format("Ejecucion de metodo GetValidateLine: Parámetros de entrada: [coId] - {0};  [numeroDocumento] - {1};  [tipoDocumento] - {2}; ",
                    objRequest.validarLineaRequest.coId, objRequest.validarLineaRequest.numeroDocumento, objRequest.validarLineaRequest.tipoDocumento));

                objResponse = RestService.PostInvoqueSDP<Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.VALIDAR_LINEA, audit, objRequest);


                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(auditRequest.Transaction, auditRequest.Session,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ; ",
                     objResponse.validarLineaResponse.responseAudit.codigoRespuesta, objResponse.validarLineaResponse.responseAudit.mensajeRespuesta));
                }
                else
                {
                    Claro.Web.Logging.Error(auditRequest.Transaction, auditRequest.Session, "GetValidateLine: Error al invocar al servicio");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(auditRequest.Transaction, auditRequest.Session, ex.Message);
            }

            return objResponse;
        }

        public static Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Method_Response GetUsageThresholdsCounter(Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.MethodCall_Request objMethodCall_Request, Claro.Entity.AuditRequest audit)
        {
            Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Method_Response objResponse = new Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Method_Response();

            try
            {
                objResponse = RestService.PostInvoqueXML<Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Method_Response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_USAGE_THRESHOLDS_COUNTER, objMethodCall_Request);

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public static Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse GetMiClaroApp(Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppRequest objMiClaroAppRequest)
        {
            Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse objResponse = new Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse();
            try
            {
                Claro.Web.Logging.Error("", "", "inicio GetMiClaroApp");
                string strUserEncriptado = "USER";
                string strPassEncriptado = "PASS";
                Claro.Web.Logging.Error("", "", "inicio GetMiClaroApp");
                Claro.Web.Logging.Error("", "", "strUserEncriptado :" + strUserEncriptado);
                Claro.Web.Logging.Error("", "", "strPassEncriptado :" + strPassEncriptado);
                Claro.Web.Logging.Error("", "", "urlRESTMiClaroApp :" + KEY.AppSettings("urlRESTMiClaroApp"));

                objResponse = RestService.PostInvoque2<Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse>(KEY.AppSettings("urlRESTMiClaroApp"), objMiClaroAppRequest.Audit, objMiClaroAppRequest, true, strUserEncriptado, strPassEncriptado);
                Claro.Web.Logging.Error("", "", "fin GetMiClaroApp");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMiClaroAppRequest.Audit.Session, objMiClaroAppRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse GetStateEquipo(Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse objResponse = new Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse();

            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetStatusEquipo: Parámetros de entrada: [PI_LINEA] - {0}   ", objRequest.MessageRequest.Body.PI_LINEA));
                System.Collections.Hashtable objValues = new System.Collections.Hashtable();
                string strKeyGetLinea = KEY.AppSettings("strGetLinea");

                objValues.Add(strKeyGetLinea, objRequest.MessageRequest.Body.PI_LINEA);


                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                string.Format("Parámetros de Entrada: [{0}] - {1}  ;", strKeyGetLinea, objRequest.MessageRequest.Body.PI_LINEA));

                objResponse = RestService.PostInvoqueDP<Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_STATUS_EQUIPO, objRequest.Audit, objRequest, false, getCredentials(objRequest.Audit, "strUserDPconsultaSitic", "strPassDPconsultaSitic"));

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ; [mensajeError] - {2} ", objResponse.MessageResponse.Body.PO_CODERROR, objResponse.MessageResponse.Body.PO_ESTADO, objResponse.MessageResponse.Body.PO_MSJERROR));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetStatusEquipo: Error al invocar al servicio");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }



         public static List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo> obtenerListaBloDesblo(Claro.Entity.AuditRequest auditoria, string ViCcontrato, ref string strMsgError)
         {
             List<ListaBloDesblo> salida = new List<ListaBloDesblo>();
             String strRespuesta = String.Empty;
             string status;
             string tipo;

             POSTPAID_BLOQDESBLOQLINEA.consultarBloDesbloRequest objRequest = new POSTPAID_BLOQDESBLOQLINEA.consultarBloDesbloRequest();
             POSTPAID_BLOQDESBLOQLINEA.consultarBloDesbloResponse objResponse = new POSTPAID_BLOQDESBLOQLINEA.consultarBloDesbloResponse();
             POSTPAID_BLOQDESBLOQLINEA.auditRequestType objRequestAudit = new POSTPAID_BLOQDESBLOQLINEA.auditRequestType();

             try
             {

                 objRequestAudit.idTransaccion = auditoria.Transaction;
                 objRequestAudit.ipAplicacion = auditoria.IPAddress;
                 objRequestAudit.usuarioAplicacion = auditoria.UserName;
                 objRequestAudit.nombreAplicacion = auditoria.ApplicationName;
                 objRequest.coId = ViCcontrato;

                 objRequest.auditRequest = objRequestAudit;


                 objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_BLOQDESBLOQLINEA.consultarBloDesbloResponse>
                ("obtenerListaBloDesblo", auditoria.Transaction, Configuration.ServiceConfiguration.POSTPAID_BLOQDESBLOQLINEA, () =>
                {
                    return Configuration.ServiceConfiguration.POSTPAID_BLOQDESBLOQLINEA.consultarBloDesblo(objRequest);
                }); 


                 Claro.Web.Logging.Info("obtenerListaBloDesblo", auditoria.Transaction, string.Format("Parámetros de Entrada => coId: {0}", ViCcontrato));

                 strRespuesta = objResponse.auditResponse.codigoRespuesta;

                 if (strRespuesta == "0")
                 {
                     Claro.Web.Logging.Info("obtenerListaBloDesblo", auditoria.Transaction, string.Format("Cantidad de registros listadoBloDesblo: {0}", objResponse.listaCurConsulta.Length));
                     if (objResponse.listaCurConsulta.Length > 0)
                     {
                         //ArrayList Bloq = new ArrayList();
                         foreach (var dr in objResponse.listaCurConsulta)
                         {
                             status = dr.ticklerStatus.ToString();
                             if (status == "OPEN")
                             {
                                 ListaBloDesblo item = new ListaBloDesblo();

                                 item.Nro_Tickler = dr.ticklerNumber.ToString();
                                 item.sFecha = dr.createdDate.ToString();
                                 item.Fecha = Convert.ToDate(dr.createdDate);
                                 tipo = dr.ticklerCode.ToString();
                                 item.TIPO = tipo;

                                 switch (tipo)
                                 {
                                     case "BLOQ_ROB":
                                         item.DESC_TIPO = "Robo";
                                         break;
                                     case "BLOQ_FRA":
                                         item.DESC_TIPO = "Fraude";
                                         break;
                                     case "BLOQ_FIN":
                                         item.DESC_TIPO = "Financiamiento";
                                         break;
                                     case "BLOQ_LC":
                                         item.DESC_TIPO = "Límite de Crédito";
                                         break;
                                     case "BLOQ_COB":
                                         item.DESC_TIPO = "Cobranza";
                                         break;
                                     case "BLOQ_S20":
                                         item.DESC_TIPO = "Bloqueo S20";
                                         break;
                                     case "BLOQ_OSP":
                                         item.DESC_TIPO = "Bloqueo Osiptel";
                                         break;
                                     default:
                                         item.DESC_TIPO = tipo;
                                         break;
                                 }
                                 item.Descripcion = dr.longDescription.ToString();
                                 item.Usuario_Seguimiento = dr.createdBy.ToString();
                                 item.Estado = "";
                                 salida.Add(item);
                                 Claro.Web.Logging.Info("obtenerListaBloDesblo", auditoria.Transaction, string.Format("Parámetros de Salida => ticklerStatus: {0}, ticklerNumber: {1}, createdDate: {2}, ticklerCode: {3}, longDescription: {4}, createdBy: {5}, estado: {6}", status, item.Nro_Tickler, item.sFecha, tipo, item.Descripcion, item.Usuario_Seguimiento, dr.estado.ToString()));
                             }

                         }
                     }
                     else
                     {
                         salida = null;
                         Claro.Web.Logging.Info("obtenerListaBloDesblo", auditoria.Transaction, string.Format(" Lista Vacia, {0}", salida));
                     }
                 }
                 else
                 {
                     strMsgError = objResponse.auditResponse.mensajeRespuesta;
                     Claro.Web.Logging.Info("obtenerListaBloDesblo", auditoria.Transaction, string.Format(" Lista Vacia, {0}", strMsgError));
                 }
             }
             catch (Exception ex)
             {
                 Claro.Web.Logging.Info("obtenerListaBloDesblo", auditoria.Transaction, string.Format("Error() {0}", ex.Message));
                 throw ex;
             }

             return salida;
         }

        public static ResponseListaBloqueos.ResponseListaObtenerBloqueos GetListaBloqueoDesbloqueo(RequestListaBloqueos.RequestListaobtenerBloqueos objRequest, Claro.Entity.AuditRequest objAuditoriaRequest)
        {
            ResponseListaBloqueos.ResponseListaObtenerBloqueos objResponse = null;
            try
            {
                Hashtable paramHeader = new Hashtable();
                paramHeader.Add("idTransaccion", objAuditoriaRequest.Transaction);
                paramHeader.Add("msgid", objAuditoriaRequest.Transaction);
                paramHeader.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                paramHeader.Add("userId", objAuditoriaRequest.UserName);
                objResponse = Claro.Data.RestService.PostInvoqueSDP<ResponseListaBloqueos.ResponseListaObtenerBloqueos>(RestServiceConfiguration.LISTA_BLOQUEO_DESBLOQUEO, paramHeader, objRequest);


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objAuditoriaRequest.Session, objAuditoriaRequest.Transaction, string.Format("Error en GetListaBloqueoDesbloqueo: {0}", ex));
                throw;
            }

            return objResponse;
        }


         //PROY-140464 Ajustes - INI
        public static ReasonCancelInvoiceResponse GetReasonCancelInvoice(ReasonCancelInvoiceRequest objRequest)
        {
            var objResponse = new Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse();

            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetReasonCancelInvoice - Obtiene la lista de motivos para la anulación."));

                objResponse = RestService.PostInvoqueDP<Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_MOTIVO_ANULACION_DP, objRequest.Audit, objRequest, false, getCredentials(objRequest.Audit, "strUserAjustesDP", "strPassAjustesDP"));

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ", objResponse.MessageResponse.Body.codigoRespuesta, objResponse.MessageResponse.Body.mensajeRespuesta));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetReasonCancelInvoice: Error al invocar al servicio.");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static CancelInvoiceResponse CancelInvoice(CancelInvoiceRequest objRequest)
        {
            var objResponse = new CancelInvoiceResponse();
            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo CancelInvoice - Realiza la anulación de la factura."));

                objResponse = RestService.PostInvoqueDP<Entity.Dashboard.Postpaid.CancelInvoice.CancelInvoiceResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.ANULAR_FACTURA_DP, objRequest.Audit, objRequest, false, getCredentials(objRequest.Audit, "strUserAjustesDP", "strPassAjustesDP"));

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ", objResponse.MessageResponse.Body.codigoRespuesta, objResponse.MessageResponse.Body.mensajeRespuesta));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "CancelInvoice Error al invocar al servicio.");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static StatusAccountDPResponse GetStatusAccountDP(StatusAccountDPRequest objRequest, string strIdSession, string strTransactionID)
        {
            var objResponse = new StatusAccountDPResponse();

            try
            {
                Claro.Web.Logging.Info(strIdSession, strTransactionID, string.Format("Ejecucion de metodo GetStatusAccountDP - Realiza la consulta del estado de cuenta,."));

                objResponse = RestService.PostInvoqueDP<StatusAccountDPResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_ESTADO_CUENTA_DP, objRequest.Audit, objRequest, false, getCredentials(objRequest.Audit, "strUserAjustesDP", "strPassAjustesDP"));

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(strIdSession, strTransactionID,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ", objResponse.MessageResponse.Body.codigoRespuesta, objResponse.MessageResponse.Body.mensajeRespuesta));
                }
                else
                {
                    Claro.Web.Logging.Error(strIdSession, strTransactionID, "GetStatusAccountDP - Error al invocar al servicio.");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
            }

            return objResponse;
        }

        public static TypificationResponse GetTyficationList(TypificationRequest objTypificationRequest)
        {
            TypificationResponse objTypificationResponse = null;

            DbParameter[] parameters = {
                new DbParameter("P_TRANSACCION", DbType.String, ParameterDirection.Input,objTypificationRequest.TRANSACTION_NAME),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
            try
            {
                objTypificationResponse = new TypificationResponse();
                DbFactory.ExecuteReader(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_TIPIFICACIONES, parameters, (IDataReader reader) =>
                {
                    objTypificationResponse.ListTypification = new List<Typification>();
                    while (reader.Read())
                    {
                        objTypificationResponse.ListTypification.Add(new Typification()
                        {
                            TIPO = Convert.ToString(reader["TIPO"]),
                            CLASE = Convert.ToString(reader["CLASE"]),
                            SUBCLASE = Convert.ToString(reader["SUBCLASE"]),
                            TIPO_CODE = Convert.ToString(reader["TIPO_CODE"]),
                            CLASE_CODE = Convert.ToString(reader["CLASE_CODE"]),
                            SUBCLASE_CODE = Convert.ToString(reader["SUBCLASE_CODE"]),
                            INTERACCION_CODE = Convert.ToString(reader["INTERACCION_CODE"])
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, ex.Message);
            }
            return objTypificationResponse;
        }

        public static Customer TypificationGetCustomer(TypificationRequest objTypificationRequest, string customerAccount = "", int vContactobjid1 = 0)
        {
            var vFlagReg = "1";
            Customer client = null;

            DbParameter[] parameters = {new DbParameter("P_PHONE", DbType.String,20,ParameterDirection.Input, objTypificationRequest.TELEPHONE_NUMBER),
                                        new DbParameter("P_ACCOUNT", DbType.String,80,ParameterDirection.Input, customerAccount),
                                        new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input, vContactobjid1),
                                        new DbParameter("P_FLAG_REG", DbType.String,20,ParameterDirection.Input, vFlagReg),
                                        new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output),
                                        new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
                                        new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                    DbFactory.ExecuteReader(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                client = new Customer()
                                {
                                    OBJID_CONTACTO = Convert.ToString(reader["OBJID_CONTACTO"]), 
                                    OBJID_SITE = Convert.ToString(reader["OBJID_SITE"]), 
                                    TELEFONO = Convert.ToString(reader["TELEFONO"]),
                                    CUENTA = Convert.ToString(reader["CUENTA"])
                                };
                            }
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, ex.Message);
            }

            return client;
        }

        public static EvaluateAmountResponse GetEvaluateAmount(EvaluateAmountRequest objRequest)
        {
            var objResponse = new EvaluateAmountResponse();
            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetEvaluateAmount - Realiza la evaluación de monto del perfil."));

                objResponse = RestService.PostInvoqueDP<Entity.Dashboard.Postpaid.EvaluateAmount.EvaluateAmountResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.EVALUAR_MONTO_MAXIMO_DP, objRequest.Audit, objRequest, false, getCredentials(objRequest.Audit, "strUserAjustesDP", "strPassAjustesDP"));

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ", objResponse.MessageResponse.Body.codigoRespuesta, objResponse.MessageResponse.Body.mensajeRespuesta));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "EvaluateAmount Error al invocar al servicio.");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static UserProfileResponse GetProfileUser(UserProfileRequest objRequest)
        {
            var objResponse = new UserProfileResponse();
            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetProfileUser - Obtiene los perfiles del usuario."));

                objResponse = RestService.PostInvoqueDP<Entity.Dashboard.Postpaid.GetUserProfile.UserProfileResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_PERFILES_DP, objRequest.Audit, objRequest, false, getCredentials(objRequest.Audit, "strUserAjustesDP", "strPassAjustesDP"));

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ", objResponse.MessageResponse.Body.errorCode, objResponse.MessageResponse.Body.errorMsg));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetProfileUser Error al invocar al servicio.");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        //PROY-140464 Ajustes - FIN

        //INICIATIVA-616
        public static void GetDataServiceTOBE(AuditRequest audit, string customerId, string coId, ref string telefono, ref string internet, ref string cable)
        {
            Entity.Dashboard.Postpaid.Legacy.GetDataServiceTOBE.Response.response response = null;
            try
            {
                Entity.Dashboard.Postpaid.Legacy.GetDataServiceTOBE.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataServiceTOBE.Request.request()
                {
                    idCustomer = customerId
                };
                response = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetDataServiceTOBE.Response.response>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_PRODUCTOS_X_CUSTOMER, audit, request, false);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            if (response != null &&
                response.responseStatus != null &&
                response.responseStatus.codigoRespuesta == Claro.Constants.NumberZeroString &&
                response.responseData != null &&
                response.responseData.datosHFC.Count > 0)
            {
                foreach (var objDatoHFC in response.responseData.datosHFC)
                {
                    if (objDatoHFC.coId.Equals(coId))
                    {
                        telefono = objDatoHFC.telefonia;
                        internet = objDatoHFC.internet;
                        cable = objDatoHFC.cable;
                        break;
                    }
                }
            }
        }
        //INICIATIVA-616

        //[INICIATIVA-616 | ONE FIJA - Postventa. Integración SIAC con CBIOS] KGC
        public static Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse GetProductosXCustomer(Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerRequest objRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse objResponse = new Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse();

            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Ejecucion de metodo GetProductosXCustomer"));

                objResponse = RestService.PostInvoque<Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse>(Claro.SIACU.Data.Configuration.RestServiceConfiguration.OBTENER_PRODUCTOS_X_CUSTOMER, objRequest.Audit, objRequest, false);

                if (objResponse != null)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                     string.Format("Parámetros de Salida: [codigoRespuesta] - {0} ; [mensajeRespuesta] - {1} ", objResponse.responseStatus.codigoRespuesta, objResponse.responseStatus.descripcionRespuesta));
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetProductosXCustomer: Error al invocar al servicio");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        //[INICIATIVA-616

        //[INICIATIVA-616 | ONE FIJA - Postventa. Integración SIAC con CBIOS] OBTENER SERVICIOS ACTUALES
        public static List<ContractServices> GetCurrencyServiceCbio(AuditRequest audit, string coIdPub, string codProducto)
        {
            List<ContractServices> listContractServices = new List<ContractServices>();
            ContractServices objContractServices;
            string fechaServicioBasico = "";
            Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.CurrencyServiceCbioResponse objResponse = null;

            try
            {
                Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.CurrencyServiceCbioReq request = new Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.CurrencyServiceCbioReq()
                {
                    Audit = audit,
                     CurrencyServiceCbioRequest = new Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.CurrencyServiceCbioRequest()
                        {
                            obtenerServiciosRequestType = new Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.obtenerServiciosRequestType()
                            {
                                coId = "",
                                msisdn = "",
                                coIdPub = coIdPub,
                                codTecnologia = codProducto,
                                codProd = null,
                                flagServAdicional = "0",
                                listaOpcional = new List<Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.listaOpcional>()
                                {
                                    new Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.listaOpcional
                                    {
                                        clave= "estado",
                                        valor= "all"
                                    },
                                    new Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.listaOpcional
                                    {
                                        clave= "caracteristicas",
                                        valor= "true"
                                    } 
                                }                    
                            }
                        }
                };

                objResponse = RestService.PostInvoqueRest<Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO.CurrencyServiceCbioResponse>(Configuration.RestServiceConfiguration.GET_CURRENCY_SERVICES_CBIO, audit, request.CurrencyServiceCbioRequest, false);

                foreach (var item in objResponse.obtenerServiciosResponseType.responseData.listaServiciosAsignados) //response.obtenerServiciosPlanPorContratoResponse.responseData.serviciosAsociados
                {

                    //if (!string.IsNullOrEmpty(item.numeroGrupo) && !string.IsNullOrEmpty(item.decripcionGrupo)
                    //&& !string.IsNullOrEmpty(item.bloqueoActivacion) && item.bloqueoActivacion != "X" &&
                    //!string.IsNullOrEmpty(item.bloqueoDesactivacion) && item.bloqueoDesactivacion != "X"
                    //&& !string.IsNullOrEmpty(item.tipoServicio))
                    if (!string.IsNullOrEmpty(item.numeroGrupo) && !string.IsNullOrEmpty(item.decripcionGrupo)
                     //&& item.bloqueoActivacion != "X" && item.bloqueoDesactivacion != "X"
                    && !string.IsNullOrEmpty(item.tipoServicio))
                    {
                        objContractServices = new ContractServices();
                        objContractServices.TIPOSERVICIO = item.tipoServicio;
                        objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;
                        objContractServices.MONTO_CARGO_SUS = item.suscripcion;
                        if (string.IsNullOrEmpty(item.numeroGrupo) && item.valorServicio.ToUpper() == ConfigurationManager.AppSettings("strDescripcionServicio").ToUpper() && item.tipoUnidad.ToUpper() == ConfigurationManager.AppSettings("strItemCargo").ToUpper())
                        {
                            objContractServices.POS_GRUPO = Claro.Constants.NumberOneString;
                            objContractServices.DES_GRUPO = ConfigurationManager.AppSettings("strGroupServicioBasico");

                        }
                        else
                        {
                            if (objContractServices.TIPOSERVICIO.ToUpper() == ConfigurationManager.AppSettings("strTipoSerOPT").ToUpper())
                            {
                                objContractServices.MONTO_CARGO_SUS = Claro.Constants.NumberZeroString;
                            }
                            else
                            {
                                objContractServices.MONTO_CARGO_SUS = item.suscripcion;
                            }

                            if (objContractServices.TIPOSERVICIO.ToUpper() == ConfigurationManager.AppSettings("strTipoSerAON").ToUpper())
                            {
                                objContractServices.MONTO_CARGO_FIJO = Claro.Constants.NumberZeroString;
                            }
                            else
                            {
                                objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;
                            }

                            if (item.numeroGrupo == Claro.Constants.NumberOneString && item.decripcionGrupo == ConfigurationManager.AppSettings("strGroupServicioBasico"))
                            {
                                fechaServicioBasico = item.validoDesde;
                            }
                            objContractServices.DES_GRUPO = item.decripcionGrupo;
                            objContractServices.POS_GRUPO = item.numeroGrupo;
                        }
                        objContractServices.FECHA_VALIDEZ = item.validoDesde;
                        objContractServices.COD_SERV = item.codigoServicio;
                        objContractServices.DES_SERV = item.spcodeDes;
                        objContractServices.POS_SERV = item.codigoServicio;
                        objContractServices.COD_EXCLUYENTE = item.codigoExcluyente;
                        objContractServices.DES_EXCLUYENTE = item.descripcionExcluyente;
                        objContractServices.ESTADO = item.estado;
                        objContractServices.CUOTA_MODIF = "0";//objContractServices.MONTO_CARGO_FIJO;
                        //objContractServices.MONTO_CARGO_FIJO = "0";
                        objContractServices.PERIODOS_VALIDOS = item.periodo;
                        if (!string.IsNullOrEmpty(objContractServices.PERIODOS_VALIDOS))
                        {
                            objContractServices.PERIODOS_VALIDOS = item.periodo + " " + item.tipoPeriodo;
                        }
                        else
                        {
                            objContractServices.PERIODOS_VALIDOS = string.Empty;
                        }

                        objContractServices.BLOQUEO_DESACT = item.bloqueoDesactivacion;
                        objContractServices.BLOQUEO_ACT = item.bloqueoActivacion;
                        objContractServices.DAPO = item.tipoPO;
                        objContractServices.SERVICIO = item.valorServicio;
                        objContractServices.CARGO = item.tipoUnidad;
                        objContractServices.CATSERVICIO = item.categoriaServicio;

                        listContractServices.Add(objContractServices);

                    }
                }

                //CARACTERISTICAS
                foreach (var item in objResponse.obtenerServiciosResponseType.responseData.listaCaracteristicas)
                {
                    if (!string.IsNullOrEmpty(item.numeroGrupo) && !string.IsNullOrEmpty(item.descripcionGrupo)                     
                    && !string.IsNullOrEmpty(item.tipoServicio))
                    {
                        objContractServices = new ContractServices();
                        objContractServices.TIPOSERVICIO = item.tipoServicio;
                        objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;                        
                        
                        objContractServices.MONTO_CARGO_SUS = Claro.Constants.NumberZeroString; 
                        

                        if (objContractServices.TIPOSERVICIO.ToUpper() == ConfigurationManager.AppSettings("strTipoSerAON").ToUpper())
                        {
                            objContractServices.MONTO_CARGO_FIJO = Claro.Constants.NumberZeroString;
                        }
                        else
                        {
                            objContractServices.MONTO_CARGO_FIJO = item.cargoFijo;
                        }

                        if (item.numeroGrupo == Claro.Constants.NumberOneString && item.descripcionGrupo == ConfigurationManager.AppSettings("strGroupServicioBasico"))
                        {
                            fechaServicioBasico = item.validoDesde;
                        }
                        objContractServices.DES_GRUPO = item.descripcionGrupo;
                        objContractServices.POS_GRUPO = item.numeroGrupo;
                        
                        objContractServices.FECHA_VALIDEZ = item.validoDesde;
                        objContractServices.COD_SERV = string.Empty;
                        objContractServices.DES_SERV = item.spcodedes;
                        objContractServices.POS_SERV = string.Empty;
                        objContractServices.COD_EXCLUYENTE = string.Empty;
                        objContractServices.DES_EXCLUYENTE = string.Empty;
                        objContractServices.ESTADO = item.estado;
                        objContractServices.CUOTA_MODIF = "0";//item.monto;//objContractServices.MONTO_CARGO_FIJO;

                        if (item.spcodedes.Contains("Consumo Adicional") || item.spcodedes.Contains("Consumo Exacto") || item.spcodedes.Contains("Consumo Abierto"))
                        {
                            objContractServices.MONTO_CARGO_FIJO = "0";
                        }
                        else
                        {
                            objContractServices.MONTO_CARGO_FIJO = item.monto;//"0";
                        }

                        
                        objContractServices.PERIODOS_VALIDOS = string.Empty;

                        objContractServices.BLOQUEO_DESACT = string.Empty;
                        objContractServices.BLOQUEO_ACT = string.Empty;
                        objContractServices.DAPO = item.tipoPO;
                        objContractServices.SERVICIO = null;
                        objContractServices.CARGO = string.Empty;
                        objContractServices.CATSERVICIO = item.categoriaServicio;

                        listContractServices.Add(objContractServices);
                    }
                }


                foreach (var item in listContractServices)
                {
                    if (item.POS_GRUPO.ToUpper() == Claro.Constants.NumberOneString
                         && item.DES_SERV.ToUpper() == ConfigurationManager.AppSettings("strDescripcionServicio").ToUpper())
                    {
                        item.FECHA_VALIDEZ = fechaServicioBasico;
                    }

                }

                listContractServices = listContractServices
                    .OrderBy(x => x.POS_GRUPO)
                    .ThenBy(z => z.DES_SERV)
                    .ToList();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }
            finally { }

            return listContractServices;
        }

        public static InvoceDetailsResponse GetHistoryInvoiceTobe(AuditRequest audit, InvoceDetailsRequest objInvoceDetailsRequest)
        {
            InvoceDetailsResponse objInvoceDetailsResponse = new InvoceDetailsResponse();
            try
            {
                Claro.Web.Logging.Error("GetHistoryInvoiceTobe", "GetHistoryInvoiceTobeDATA: ", objInvoceDetailsRequest.obtenerDatosFacturaRequest.csId);
                objInvoceDetailsResponse = RestService.PostInvoqueRest<InvoceDetailsResponse>(Configuration.RestServiceConfiguration.OBTENER_DATOS_FACTURA_TOBE, audit, objInvoceDetailsRequest, false);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
                objInvoceDetailsResponse = null;
            }
            return objInvoceDetailsResponse;
        }


    }
}

