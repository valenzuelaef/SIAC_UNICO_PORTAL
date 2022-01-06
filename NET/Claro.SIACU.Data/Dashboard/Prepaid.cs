using Claro.Data;
using Claro.SAP;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Dashboard;
using Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts;
using Claro.SIACU.Entity.Dashboard.Prepaid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using COMMON_HLR = Claro.SIACU.ProxyService.SIAC.HLRConsult;
using KEY = Claro.ConfigurationManager;
using PREPAID_CONSULTDATA = Claro.SIACU.ProxyService.SIACPre.DataPrePost;
using PREPAID_OPERATIONS = Claro.SIACU.ProxyService.SIACPre.BondTFI;
using PREPAID_PRODUCTS = Claro.SIACU.ProxyService.SIACU.Pre.Products;
using PREPAID_SERVICE = Claro.SIACU.ProxyService.SIACPre.Service;
using PREPAID_SERVICE2 = Claro.SIACU.ProxyService.SIACPre.Service2;
using PREPAID_SIMCARD = Claro.SIACU.ProxyService.SAP.Operations;
using PREPAID_PAQUETE = Claro.SIACU.ProxyService.SIACPre.PaqueteDatos;
using PREPAID_BSSOPERATIONS = Claro.SIACU.ProxyService.SIACPre.BSS_OperacionesINU;
using Claro.SIACU.Entity.Dashboard.Prepaid.GetRateState;
//<ASANCHEZ>
using CONSULTCLAVE = Claro.SIACU.ProxyService.SIAC.Claves;
using CONSULTAR_DATOS_SIM = Claro.SIACU.ProxyService.SIACPre.ConsultarDatosSIM;
using System.ServiceModel;
using Claro.Entity;
//</ASANCHEZ>

namespace Claro.SIACU.Data.Dashboard
{
    public static class Prepaid
    {
        /// <summary>
        /// Método que obtiene los datos del cliente prepago.
        /// </summary>
        /// <param name="Id de sesión">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono del cliente</param>
        /// <param name="strAccount">Número de cuenta</param>
        /// <param name="strContactId">Id del cliente</param>
        /// <param name="strFlagRegistry">Flag de registro</param>
        /// <returns>Devuelve objeto CUSTOMER con datos del cliente.</returns>

        public static Entity.Dashboard.Prepaid.Customer GetPreviousCustomer(string strIdSession, string strTransaction, string strTelephone, string strAccount, string strContactId, string strFlagRegistry)
        {
            Entity.Dashboard.Prepaid.Customer objCustomer = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PHONE", DbType.String, ParameterDirection.Input, strTelephone),
                new DbParameter("P_ACCOUNT", DbType.String, ParameterDirection.Input,strAccount),
                new DbParameter("P_CONTACTOBJID_1", DbType.Int32, ParameterDirection.Input,  (strContactId == "" ? Claro.Constants.NumberZero : Convert.ToInt(strContactId))),
                new DbParameter("P_FLAG_REG", DbType.String, 255, ParameterDirection.Input, strFlagRegistry),
                new DbParameter("P_FLAG_CONSULTA", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_CLFY, parameters, (IDataReader reader) =>
            {

                if (reader.Read())
                {
                    objCustomer = new Entity.Dashboard.Prepaid.Customer();
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
            return objCustomer;
        }
        /// <summary>
        /// Método que obtiene una lista de clientes prepago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Teléfono de cliente</param>
        /// <param name="strAccount">Número de cuenta</param>
        /// <param name="strContactId">Id de contacto</param>
        /// <param name="strFlagRegistry">Flag de registro</param>
        /// <param name="strFlagGetAll">Flag para obtener n registros</param>
        /// <returns>Devuelve lista con datos del cliente.</returns>
        /// 
        public static List<Entity.Dashboard.Prepaid.Customer> GetPreviousCustomers(string strIdSession, string strTransaction, string strTelephone, string strAccount, string strContactId, string strFlagRegistry, string strFlagGetAll)
        {
            List<Entity.Dashboard.Prepaid.Customer> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PHONE", DbType.String, ParameterDirection.Input, strTelephone),
                new DbParameter("P_ACCOUNT", DbType.String, ParameterDirection.Input,strAccount),
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64, ParameterDirection.Input,  (strContactId == "" ? Claro.Constants.NumberZero : Convert.ToInt64(strContactId))),
                new DbParameter("P_FLAG_REG", DbType.String, 255, ParameterDirection.Input, strFlagRegistry),
                new DbParameter("P_FLAG_CONSULTA", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            if (strFlagGetAll == "S")
            {
                list = DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.Customer>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_CLFY, parameters);
            }
            else
            {
                list = new List<Entity.Dashboard.Prepaid.Customer>();
                list.Add(DbFactory.ExecuteReader<Entity.Dashboard.Prepaid.Customer>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_CLFY, parameters));
            }
            return list;
        }

        /// <summary>
        /// Método que obtiene el motivo de baja del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strFlagHistoric">Flag histórico (1 Histórica, 0 Normal)</param>
        /// <param name="strLine">Número de teléfono</param>
        /// <returns>Devuelve lista de motivos de baja del cliente</returns>

        public static List<Entity.Dashboard.Prepaid.MotiveLow> GetListMotiveLow(string strIdSession, string strTransaction, string strFlagHistoric, string strLine)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("LINEA", DbType.String,20, ParameterDirection.Input,strLine),
                new DbParameter("FLAG_HIS", DbType.String,20, ParameterDirection.Input,strFlagHistoric),
                new DbParameter("CUR_LINEAS", DbType.Object, ParameterDirection.Output)
            };
            return DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.MotiveLow>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_LISTA_BAJAS, parameters);
        }


        public static List<Entity.Dashboard.Prepaid.Recharge> GetRecharges(Claro.Entity.AuditRequest objAudit, string strMsisdn, string strStartDate, string strEndDate, string strtypeQuery, string strMovementType, string strcreditoDebito)
        {
            PREPAID_CONSULTDATA.auditRequestType objauditRequestType = new PREPAID_CONSULTDATA.auditRequestType()
            {
                idTransaccion = objAudit.Transaction,
                ipAplicacion = objAudit.IPAddress,
                nombreAplicacion = objAudit.ApplicationName,
                usuarioAplicacion = objAudit.UserName
            };
            PREPAID_CONSULTDATA.ConsultarDetalleRecargasRequest objConsultarDetalleRecargasRequest = new PREPAID_CONSULTDATA.ConsultarDetalleRecargasRequest()
            {
                auditRequest = objauditRequestType,
                fechaFinal = strEndDate,
                fechaInicial = strStartDate,
                msisdn = strMsisdn,
                tipoConsulta = strtypeQuery,
                tipoMovimiento = strMovementType,
                aplicativo = string.Empty,
                creditoDebito = strcreditoDebito
            };
            PREPAID_CONSULTDATA.ConsultarDetalleRecargasResponse objConsultarDetalleLlamadasResponse = Claro.Web.Logging.ExecuteMethod<PREPAID_CONSULTDATA.ConsultarDetalleRecargasResponse>(objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.PREPAID_CONSULTDATA, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_CONSULTDATA.consultarDetalleRecargas(objConsultarDetalleRecargasRequest);
            });
            List<Recharge> ListEstOrd = new List<Recharge>();
            if (objConsultarDetalleLlamadasResponse.auditResponse.codigoRespuesta == "-1")
            {
                throw new Claro.MessageException("");
            }
            List<Entity.Dashboard.Prepaid.Recharge> lstRecharge = new List<Recharge>();
            PREPAID_CONSULTDATA.parametrosTypeRecargasObjetoOpcionalRecargas[][] detalleRecargas;
            if (objConsultarDetalleLlamadasResponse.auditResponse.codigoRespuesta.Equals("0") && objConsultarDetalleLlamadasResponse.detalleRecargas != null && objConsultarDetalleLlamadasResponse.detalleRecargas.Any())
            {
                detalleRecargas = objConsultarDetalleLlamadasResponse.detalleRecargas;

                if (strtypeQuery == "1")
                {
                    for (int i = 0; i < detalleRecargas.Length; i++)
                    {
                        Recharge objRecharge = new Recharge();

                        if (detalleRecargas[i][0].campo.Equals("FECHA_HORA"))
                        {
                            objRecharge.FECHARECARGA = String.IsNullOrEmpty(detalleRecargas[i][0].valor) ? "" : detalleRecargas[i][0].valor;
                        }
                        if (detalleRecargas[i][1].campo.Equals("TIPO_DE_MOVIMIENTO"))
                        {
                            objRecharge.TIPOMOVIMIENTO = String.IsNullOrEmpty(detalleRecargas[i][1].valor) ? "" : detalleRecargas[i][1].valor;
                        }
                        if (detalleRecargas[i][2].campo.Equals("TIPO_RECARGA"))
                        {
                            objRecharge.TIPORECARGA = String.IsNullOrEmpty(detalleRecargas[i][2].valor) ? "" : detalleRecargas[i][2].valor;
                        }
                        if (detalleRecargas[i][3].campo.Equals("MONTO"))
                        {
                            objRecharge.MONTOEFECTIVO = String.IsNullOrEmpty(detalleRecargas[i][3].valor) ? "" : detalleRecargas[i][3].valor;
                        }
                        if (detalleRecargas[i][4].campo.Equals("SALDO"))
                        {
                            objRecharge.SALDO = String.IsNullOrEmpty(detalleRecargas[i][4].valor) ? "" : detalleRecargas[i][4].valor;
                        }
                        if (detalleRecargas[i][5].campo.Equals("CREDITO_DEBITO"))
                        {
                            objRecharge.CREDDEBI = String.IsNullOrEmpty(detalleRecargas[i][5].valor) ? "" : detalleRecargas[i][5].valor;
                        }
                        if (detalleRecargas[i][6].campo.Equals("BOLSA"))
                        {
                            objRecharge.BOLSA = String.IsNullOrEmpty(detalleRecargas[i][6].valor) ? "" : detalleRecargas[i][6].valor;
                        }
                        if (detalleRecargas[i][7].campo.Equals("TIPO_DE_SALDO"))
                        {
                            objRecharge.TIPOSALDO = String.IsNullOrEmpty(detalleRecargas[i][7].valor) ? "" : detalleRecargas[i][7].valor;
                        }
                        if (detalleRecargas[i][8].campo.Equals("DESCRIPCION"))
                        {
                            objRecharge.DESCRIPCION = String.IsNullOrEmpty(detalleRecargas[i][8].valor) ? "" : detalleRecargas[i][8].valor;
                        }
                        if (detalleRecargas[i][9].campo.Equals("DETALLE"))
                        {
                            objRecharge.DETALLE = String.IsNullOrEmpty(detalleRecargas[i][9].valor) ? "" : detalleRecargas[i][9].valor;
                        }

                        lstRecharge.Add(objRecharge);
                    }
                }
                else if (strtypeQuery == "0")
                {
                    for (int i = 0; i < detalleRecargas.Length; i++)
                    {
                        Recharge objRecharge = new Recharge();

                        if (detalleRecargas[i][0].campo.Equals("FECHA_HORA"))
                        {
                            objRecharge.FECHARECARGA = String.IsNullOrEmpty(detalleRecargas[i][0].valor) ? "" : detalleRecargas[i][0].valor;
                        }
                        if (detalleRecargas[i][1].campo.Equals("TIPO_DE_EVENTO"))
                        {
                            objRecharge.TIPOEVENTO = String.IsNullOrEmpty(detalleRecargas[i][1].valor) ? "" : detalleRecargas[i][1].valor;
                        }
                        if (detalleRecargas[i][2].campo.Equals("MONTO"))
                        {
                            objRecharge.MONTOEFECTIVO = String.IsNullOrEmpty(detalleRecargas[i][2].valor) ? "" : detalleRecargas[i][2].valor;
                        }
                        if (detalleRecargas[i][3].campo.Equals("SALDO_FINAL"))
                        {
                            objRecharge.SALDO = String.IsNullOrEmpty(detalleRecargas[i][3].valor) ? "" : detalleRecargas[i][3].valor;
                        }
                        if (detalleRecargas[i][4].campo.Equals("BOLSA"))
                        {
                            objRecharge.BOLSA = String.IsNullOrEmpty(detalleRecargas[i][4].valor) ? "" : detalleRecargas[i][4].valor;
                        }
                        if (detalleRecargas[i][5].campo.Equals("DETALLE"))
                        {
                            objRecharge.DETALLE = String.IsNullOrEmpty(detalleRecargas[i][5].valor) ? "" : detalleRecargas[i][5].valor;
                        }
                        if (detalleRecargas[i][6].campo.Equals("FECHA_EXPIRACION_RECARGA"))
                        {
                            objRecharge.FECHAEXPIRACION = String.IsNullOrEmpty(detalleRecargas[i][6].valor) ? "" : detalleRecargas[i][6].valor;
                        }

                        lstRecharge.Add(objRecharge);
                    }
                }
                ListEstOrd = lstRecharge.OrderByDescending(o => o.FECHARECARGA).ToList();

            }
            if (ListEstOrd.Count == 0)
            {
                ListEstOrd = null;
            }
            return ListEstOrd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAudit"></param>
        /// <param name="linea"></param>
        /// <param name="strfechaInicio"></param>
        /// <param name="strfechaFin"></param>
        /// <param name="strTipoConsulta"></param>
        /// <returns></returns>
        public static List<Entity.Dashboard.Prepaid.Call> GetCalls(Claro.Entity.AuditRequest objAudit, string linea, string strfechaInicio
            , string strfechaFin, string strTipoConsulta, string strTipoTrafico, bool blFlagVisualizar)
        {

            if (!string.IsNullOrEmpty(strTipoTrafico))
            {
                strTipoTrafico = strTipoTrafico.ToUpperInvariant().Trim();
            }
            else
            {
                strTipoTrafico = string.Empty;
            }

            PREPAID_CONSULTDATA.auditRequestType objAuditRequestType = new PREPAID_CONSULTDATA.auditRequestType()
            {
                idTransaccion = objAudit.Transaction,
                ipAplicacion = objAudit.IPAddress,
                nombreAplicacion = objAudit.ApplicationName,
                usuarioAplicacion = objAudit.UserName
            };

            PREPAID_CONSULTDATA.ConsultarDetalleLlamadasRequest objConsultarDetalleLlamadasRequest = new PREPAID_CONSULTDATA.ConsultarDetalleLlamadasRequest()
            {
                auditRequest = objAuditRequestType,
                fechaInicial = strfechaInicio,
                fechaFinal = strfechaFin,
                tipoConsulta = strTipoConsulta,
                tipoTrafico = strTipoTrafico,
                msisdn = linea

            };
            PREPAID_CONSULTDATA.ConsultarDetalleLlamadasResponse objConsultarDetalleLlamadasResponse = Claro.Web.Logging.ExecuteMethod<PREPAID_CONSULTDATA.ConsultarDetalleLlamadasResponse>(objAudit.Session, objAudit.Transaction, Configuration.ServiceConfiguration.PREPAID_CONSULTDATA, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_CONSULTDATA.consultarDetalleLlamadas(objConsultarDetalleLlamadasRequest);
            });

            List<Entity.Dashboard.Prepaid.Call> oLisCall = null;
            List<Entity.Dashboard.Prepaid.Call> ListEstOrd = null;

            if (objConsultarDetalleLlamadasResponse.detalleLlamadas != null && objConsultarDetalleLlamadasResponse.detalleLlamadas.Length > 0)
            {
                PREPAID_CONSULTDATA.parametrosTypeLlamadasObjetoOpcionalLlamadas[][] detalleLlamadas = objConsultarDetalleLlamadasResponse.detalleLlamadas;
                oLisCall = new List<Call>();
                int max = 0;
                if (!blFlagVisualizar)
                {
                    max = detalleLlamadas.Length;
                }
                else max = int.Parse(ConfigurationManager.AppSettings("NroRegistrosMostrarRecarga"));

                for (int i = 0; i < detalleLlamadas.Length; i++)
                {
                    if (i > max - 1) break;
                    Call item = new Call();

                    for (int j = 0; j < detalleLlamadas[i].Length; j++)
                    {
                        if (detalleLlamadas[i][j].campo.Equals("FECHA_HORA"))
                        {
                            item.CallDateTime = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("TELEFONO_DESTINO"))
                        {
                            item.CallTelephoneDestination = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("TIPO_DE_TRAFICO"))
                        {
                            item.CallTypeTraffic = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("DURACION"))
                        {
                            item.CallDuration = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("CONSUMO"))
                        {
                            item.CallUptake = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("COMPRADO_REGALADO"))
                        {
                            item.CallBoughtPresented = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("SALDO"))
                        {
                            item.CallBalance = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("BOLSA_ID"))
                        {
                            item.CallBagId = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("DESCRIPCION"))
                        {
                            item.CallDescription = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("PLAN"))
                        {
                            item.CallPlan = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("PROMOCION"))
                        {
                            item.CallPromotion = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("DESTINO"))
                        {
                            item.CallDestination = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("OPERADOR"))
                        {
                            item.CallOperator = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("GRUPO_DE_COBRO"))
                        {
                            item.CallCobroGroup = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("TIPO_DE_RED"))
                        {
                            item.CallNetworkType = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("IMEI"))
                        {
                            item.CallImei = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("ROAMING"))
                        {
                            item.CallRoaming = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("ZONA_TARIFARIA"))
                        {
                            item.CallTariffArea = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("COSTO"))
                        {
                            item.CallCost = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("INICIO_LLAMADA"))
                        {
                            item.CallStart = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("FIN_DE_LLAMADA"))
                        {
                            item.CallEnd = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("BOLSA"))
                        {
                            item.CallBag = detalleLlamadas[i][j].valor;
                        }
                        else if (detalleLlamadas[i][j].campo.Equals("TIPO_DE_EVENTO"))
                        {
                            item.CallEventType = detalleLlamadas[i][j].valor;
                        }
                    }
                    oLisCall.Add(item);
                }

                if (strTipoTrafico.Equals("VOZ"))
                {
                    ListEstOrd = oLisCall.Where(o => o.CallTypeTraffic == "VOZ").ToList();
                }
                else if (strTipoTrafico.Equals("SMS"))
                {
                    ListEstOrd = oLisCall.Where(o => o.CallTypeTraffic == "SMS").ToList();
                }
                else if (strTipoTrafico.Equals("MMS"))
                {
                    ListEstOrd = oLisCall.Where(o => o.CallTypeTraffic == "MMS").ToList();
                }
                else if (strTipoTrafico.Equals("DATOS"))
                {
                    ListEstOrd = oLisCall.Where(o => o.CallTypeTraffic == "DATOS").ToList();
                }
                else if (strTipoTrafico.Equals("VAS"))
                {
                    ListEstOrd = oLisCall.Where(o => o.CallTypeTraffic == "VAS").ToList();
                }
                else if (strTipoTrafico.Equals("USSD"))
                {
                    ListEstOrd = oLisCall.Where(o => o.CallTypeTraffic == "USSD").ToList();
                }
                else
                {
                    ListEstOrd = oLisCall.OrderByDescending(o => o.CallDateTime).ToList();
                }

            }
            return ListEstOrd;
        }

        public static List<Balance> GetPrepaidBalance(string strTelephone, string strIdSession, string strTransactionID, string strApplicationIP, string strApplicationName, string strApplicationUser, ref string strCodeResponse, ref string strMessageResponse, ref string strPrincipalBalance, ref string strPrincipalExpiration, bool isTFI)
        {
            if (!isTFI)
            {
                strTelephone = Claro.SIACU.Data.Helper.ReturnTFI(strTelephone);
            }
            List<Balance> listEstatic = new List<Balance>();
            List<Balance> listDinamicPackage = new List<Balance>();
            List<Balance> ListStaticOrder = new List<Balance>();
            List<Balance> listDinamicPackageBonus;
            List<Balance> ListDinamicPackagePromotional;
            List<Balance> listDinamicPackageBonusOrder = new List<Balance>();
            List<Balance> listDinamicPackagePromotionalorder = new List<Balance>();
            List<Balance> listPackageOtherLimited;
            List<Balance> listPackageOrderUnlimited;
            List<Balance> response = new List<Balance>();
            Balance item = null;
            PREPAID_CONSULTDATA.ConsultarSaldosPrepagoRequest oRequest = new PREPAID_CONSULTDATA.ConsultarSaldosPrepagoRequest();
            PREPAID_CONSULTDATA.auditRequestType oAudit = new PREPAID_CONSULTDATA.auditRequestType();
            PREPAID_CONSULTDATA.ConsultarSaldosPrepagoResponse oResponse;
            PREPAID_CONSULTDATA.parametrosTypeObjetoOpcional[] oListaOpcional = new PREPAID_CONSULTDATA.parametrosTypeObjetoOpcional[0];

            oRequest.msisdn = strTelephone;
            oAudit.idTransaccion = strTransactionID;
            oAudit.ipAplicacion = strApplicationIP;
            oAudit.nombreAplicacion = strApplicationName;
            oAudit.usuarioAplicacion = strApplicationUser;
            oRequest.auditRequest = oAudit;
            oRequest.listaRequestOpcional = oListaOpcional;

            oResponse = Claro.Web.Logging.ExecuteMethod<PREPAID_CONSULTDATA.ConsultarSaldosPrepagoResponse>(strIdSession, strTransactionID, Configuration.ServiceConfiguration.PREPAID_CONSULTDATA, () =>
                {
                    return Configuration.ServiceConfiguration.PREPAID_CONSULTDATA.consultarSaldosPrepago(oRequest);
                });

            if (oResponse.auditResponse.codigoRespuesta.Equals("0") && oResponse.listaOperacionesConsulta != null && oResponse.listaOperacionesConsulta.operacionesRespuesta != null)
            {
                //SALDO PRINCIPAL
                if (oResponse.unicostResponse != null)
                {
                    strPrincipalBalance = oResponse.unicostResponse.SALDO + " " + oResponse.unicostResponse.ENTIV_UNIDAD_PRESENT;
                    strPrincipalExpiration = Convert.ToDate(oResponse.unicostResponse.VENCIMIENTO).ToString();
                }
                //BOLSAS ESTATICAS
                PREPAID_CONSULTDATA.parametroType[][] listEstaticAux;
                PREPAID_CONSULTDATA.parametroType[] listEstaticDetailAux;

                if (oResponse.listaOperacionesConsulta.operacionesRespuesta.listaEstaticas != null && oResponse.listaOperacionesConsulta.operacionesRespuesta.listaEstaticas.Length > 0)
                {
                    listEstaticAux = oResponse.listaOperacionesConsulta.operacionesRespuesta.listaEstaticas;

                    for (int i = 0; i < listEstaticAux.Length; i++)
                    {
                        listEstaticDetailAux = listEstaticAux[i];
                        if (listEstaticDetailAux != null)
                        {
                            item = new Balance();
                            for (int ii = 0; ii < listEstaticDetailAux.Length; ii++)
                            {
                                //SALDO
                                if (listEstaticDetailAux[ii].nombre.Equals("SALDO") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item._Balance = listEstaticDetailAux[ii].valor;
                                }
                                //ORDEN
                                if (listEstaticDetailAux[ii].nombre.Equals("AGRUI_ORDEN"))
                                {
                                    if (listEstaticDetailAux[ii].valor == null) { item.Order = Claro.Constants.NumberZeroString; } else { item.Order = listEstaticDetailAux[ii].valor; }
                                }
                                //NOMBRE
                                if (listEstaticDetailAux[ii].nombre.Equals("AGRUV_NOMBRE"))
                                {
                                    if (listEstaticDetailAux[ii].valor == null) { item.OtherIndicator = KEY.AppSettings("strConsIndSi"); } else { item.Name = listEstaticDetailAux[ii].valor; item.OtherIndicator = KEY.AppSettings("strConsIndNo"); }
                                }
                                //UNIDAD
                                if (listEstaticDetailAux[ii].nombre.Equals("ENTIV_UNIDAD_PRESENT") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item.Unity = listEstaticDetailAux[ii].valor;
                                }
                                //TMOVV_NOMBRE
                                if (listEstaticDetailAux[ii].nombre.Equals("TMOVV_NOMBRE") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item.MovementTypeName = listEstaticDetailAux[ii].valor;
                                }
                                //NOMBRE COMERCIAL
                                if (listEstaticDetailAux[ii].nombre.Equals("BOLSV_NOM_COMERCIAL") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item.CommercialName = listEstaticDetailAux[ii].valor;
                                }
                                //DESTINO
                                if (listEstaticDetailAux[ii].nombre.Equals("DESTV_NOMBRE") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item.Destination = listEstaticDetailAux[ii].valor;
                                }
                                //TCOSV_NOMBRE
                                if (listEstaticDetailAux[ii].nombre.Equals("TCOSV_NOMBRE") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item.NameCode = listEstaticDetailAux[ii].valor;
                                }
                                //VENCIMIENTO
                                if (listEstaticDetailAux[ii].nombre.Equals("FECHA_DE_VENCIMIENTO") && listEstaticDetailAux[ii].valor != null)
                                {
                                    item.Expiration = Convert.ToDate(listEstaticDetailAux[ii].valor).ToString();
                                }
                            }
                            listEstatic.Add(item);
                        }
                    }
                }

                for (int j = 0; j < listEstatic.Count; j++)
                {
                    if (listEstatic[j].Unity != null)
                    {
                        if (listEstatic[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidSOL").ToUpper()))
                        {
                            listEstatic[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid1");
                        }
                        else if (listEstatic[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMIN").ToUpper()))
                        {
                            listEstatic[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid2");
                        }
                        else if (listEstatic[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidSMS").ToUpper()))
                        {
                            listEstatic[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid3");
                        }
                        else if (listEstatic[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMMS").ToUpper()))
                        {
                            listEstatic[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid4");
                        }
                        else if (listEstatic[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMGB").ToUpper()))
                        {
                            listEstatic[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid5");
                        }
                    }

                    if (listEstatic[j].MovementTypeName != null && listEstatic[j].MovementTypeName.ToUpper().Equals(KEY.AppSettings("strConsTMovvNomIli").ToUpper()))
                    {
                        listEstatic[j]._Balance = KEY.AppSettings("strConsTMovvNomIli");
                    }

                    ListStaticOrder.Add(listEstatic[j]);
                }

                ListStaticOrder = ListStaticOrder.OrderBy(o => o.Order).ThenBy(o => o.UnityIndicator).ThenBy(o => o.Name).ThenBy(o => o.CommercialName).ToList();

                for (int j = 0; j < ListStaticOrder.Count; j++)
                {
                    response.Add(ListStaticOrder[j]);
                }

                //BOLSAS DINAMICAS
                PREPAID_CONSULTDATA.parametroType[][] listDinamicGroup;
                PREPAID_CONSULTDATA.parametroType[] listDinamicAux;
                if (oResponse.listaOperacionesConsulta.operacionesRespuesta.listaDinamicasAgrupadas != null && oResponse.listaOperacionesConsulta.operacionesRespuesta.listaDinamicasAgrupadas.Length > 0)
                {
                    listDinamicGroup = oResponse.listaOperacionesConsulta.operacionesRespuesta.listaDinamicasAgrupadas;
                    for (int i = 0; i < listDinamicGroup.Length; i++)
                    {
                        listDinamicAux = listDinamicGroup[i];
                        if (listDinamicAux != null)
                        {
                            item = new Balance();
                            for (int ii = 0; ii < listDinamicAux.Length; ii++)
                            {
                                //ORDEN
                                if (listDinamicAux[ii].nombre.Equals("AGRUI_ORDEN"))
                                {
                                    if (listDinamicAux[ii].valor == null) { item.Order = Claro.Constants.NumberZeroString; } else { item.Order = listDinamicAux[ii].valor; }
                                }
                                //NOMBRE
                                if (listDinamicAux[ii].nombre.Equals("AGRUV_NOMBRE"))
                                {
                                    if (listDinamicAux[ii].valor == null) { item.OtherIndicator = KEY.AppSettings("strConsIndSi"); } else { item.Name = listDinamicAux[ii].valor; item.OtherIndicator = KEY.AppSettings("strConsIndNo"); }
                                }
                                //NOMBRE COMERCIAL
                                if (listDinamicAux[ii].nombre.Equals("BOLSV_NOM_COMERCIAL") && listDinamicAux[ii].valor != null)
                                {
                                    item.CommercialName = listDinamicAux[ii].valor;
                                }
                                //DESTINO
                                if (listDinamicAux[ii].nombre.Equals("DESTV_NOMBRE") && listDinamicAux[ii].valor != null)
                                {
                                    item.Destination = listDinamicAux[ii].valor;
                                }
                                //SALDO
                                if (listDinamicAux[ii].nombre.Equals("SALDO") && listDinamicAux[ii].valor != null)
                                {
                                    item._Balance = listDinamicAux[ii].valor;
                                }
                                //UNIDAD
                                if (listDinamicAux[ii].nombre.Equals("ENTIV_UNIDAD_PRESENT") && listDinamicAux[ii].valor != null)
                                {
                                    item.Unity = listDinamicAux[ii].valor;
                                }
                                //FECHA VENCIMIENTO
                                if (listDinamicAux[ii].nombre.Equals("FECHA_DE_VENCIMIENTO") && listDinamicAux[ii].valor != null)
                                {
                                    item.Expiration = Convert.ToDate(listDinamicAux[ii].valor).ToString();
                                }
                                //TMOVV_NOMBRE
                                if (listDinamicAux[ii].nombre.Equals("TMOVV_NOMBRE") && listDinamicAux[ii].valor != null)
                                {
                                    item.MovementTypeName = listDinamicAux[ii].valor;
                                }
                                //TCOSV_NOMBRE
                                if (listDinamicAux[ii].nombre.Equals("TCOSV_NOMBRE") && listDinamicAux[ii].valor != null)
                                {
                                    item.NameCode = listDinamicAux[ii].valor;
                                }
                                //PROM BONO
                                if (listDinamicAux[ii].nombre.Equals("PROMV_ES_BONO") && listDinamicAux[ii].valor != null)
                                {
                                    item.PromotionalBonus = listDinamicAux[ii].valor;
                                }
                            }
                            listDinamicPackage.Add(item);
                        }
                    }
                }

                //PAQUETES
                PREPAID_CONSULTDATA.parametroType[][] listPackageGroup;
                PREPAID_CONSULTDATA.parametroType[] listPackageAux;
                if (oResponse.listaOperacionesConsulta.operacionesRespuesta.listaPaquetesAgrupadas != null && oResponse.listaOperacionesConsulta.operacionesRespuesta.listaPaquetesAgrupadas.Length > 0)
                {
                    listPackageGroup = oResponse.listaOperacionesConsulta.operacionesRespuesta.listaPaquetesAgrupadas;
                    for (int i = 0; i < listPackageGroup.Length; i++)
                    {
                        listPackageAux = listPackageGroup[i];
                        if (listPackageAux != null)
                        {
                            item = new Balance();
                            for (int ii = 0; ii < listPackageAux.Length; ii++)
                            {
                                //ORDEN
                                if (listPackageAux[ii].nombre.Equals("ORDEN"))
                                {
                                    if (listPackageAux[ii].valor == null) { item.Order = "0"; } else { item.Order = listPackageAux[ii].valor; }
                                }
                                //NOMBRE
                                if (listPackageAux[ii].nombre.Equals("NOMBRE_DE_LA_PROMOCION_O_BONO"))
                                {
                                    if (listPackageAux[ii].valor == null || listPackageAux[ii].valor == string.Empty) { item.OtherIndicator = ConfigurationManager.AppSettings("strConsIndSi"); } else { item.Name = listPackageAux[ii].valor; item.OtherIndicator = ConfigurationManager.AppSettings("strConsIndNo"); }
                                }
                                //NOMBRE COMERCIAL
                                if (listPackageAux[ii].nombre.Equals("PAQUETE") && listPackageAux[ii].valor != null)
                                {
                                    item.CommercialName = listPackageAux[ii].valor;
                                }
                                //SALDO
                                if (listPackageAux[ii].nombre.Equals("SALDO") && listPackageAux[ii].valor != null)
                                {
                                    item._Balance = listPackageAux[ii].valor;
                                }
                                //UNIDAD
                                if (listPackageAux[ii].nombre.Equals("UNIDAD_A_PRESENTAR") && listPackageAux[ii].valor != null)
                                {
                                    item.Unity = listPackageAux[ii].valor;
                                }
                                //FECHA VENCIMIENTO
                                if (listPackageAux[ii].nombre.Equals("FECHA_DE_VENCIMIENTO") && listPackageAux[ii].valor != null)
                                {

                                    item.Expiration = Convert.ToDate(listPackageAux[ii].valor).ToString();
                                }
                                //PROM BONO
                                if (listPackageAux[ii].nombre.Equals("PROMV_ES_BONO") && listPackageAux[ii].valor != null)
                                {
                                    item.PromotionalBonus = listPackageAux[ii].valor;
                                }
                            }
                            listDinamicPackage.Add(item);
                        }
                    }
                }

                //BONO
                listDinamicPackageBonus = listDinamicPackage.Where(o => o.PromotionalBonus == KEY.AppSettings("strConsOrdListDinmReg") && o.OtherIndicator == KEY.AppSettings("strConsIndNo")).ToList();

                for (int j = 0; j < listDinamicPackageBonus.Count; j++)
                {
                    listDinamicPackage.Remove(listDinamicPackageBonus[j]);

                    if (listDinamicPackageBonus[j].Unity != null)
                    {
                        if (listDinamicPackageBonus[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidSOL").ToUpper()))
                        {
                            listDinamicPackageBonus[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid1");
                        }
                        else if (listDinamicPackageBonus[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMIN").ToUpper()))
                        {
                            listDinamicPackageBonus[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid2");
                        }
                        else if (listDinamicPackageBonus[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidSMS").ToUpper()))
                        {
                            listDinamicPackageBonus[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid3");
                        }
                        else if (listDinamicPackageBonus[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMMS").ToUpper()))
                        {
                            listDinamicPackageBonus[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid4");
                        }
                        else if (listDinamicPackageBonus[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMGB").ToUpper()))
                        {
                            listDinamicPackageBonus[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid5");
                        }
                    }

                    if (listDinamicPackageBonus[j].MovementTypeName != null && listDinamicPackageBonus[j].MovementTypeName.ToUpper().Equals(KEY.AppSettings("strConsTMovvNomIli").ToUpper()))
                    {
                        listDinamicPackageBonus[j]._Balance = KEY.AppSettings("strConsTMovvNomIli");
                    }

                    listDinamicPackageBonusOrder.Add(listDinamicPackageBonus[j]);
                }

                listDinamicPackageBonusOrder = listDinamicPackageBonusOrder.OrderBy(o => o.Order).ThenBy(o => o.UnityIndicator).ThenBy(o => o.Name).ThenBy(o => o.CommercialName).ToList();

                for (int j = 0; j < listDinamicPackageBonusOrder.Count; j++)
                {
                    response.Add(listDinamicPackageBonusOrder[j]);
                }

                //PROMOCION
                ListDinamicPackagePromotional = listDinamicPackage.Where(o => o.PromotionalBonus == KEY.AppSettings("strConsOrdListDinmCom") && o.OtherIndicator == KEY.AppSettings("strConsIndNo")).ToList();

                for (int j = 0; j < ListDinamicPackagePromotional.Count; j++)
                {
                    listDinamicPackage.Remove(ListDinamicPackagePromotional[j]);

                    if (ListDinamicPackagePromotional[j].Unity != null)
                    {
                        if (ListDinamicPackagePromotional[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidSOL").ToUpper()))
                        {
                            ListDinamicPackagePromotional[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid1");
                        }
                        else if (ListDinamicPackagePromotional[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMIN").ToUpper()))
                        {
                            ListDinamicPackagePromotional[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid2");
                        }
                        else if (ListDinamicPackagePromotional[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidSMS").ToUpper()))
                        {
                            ListDinamicPackagePromotional[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid3");
                        }
                        else if (ListDinamicPackagePromotional[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMMS").ToUpper()))
                        {
                            ListDinamicPackagePromotional[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid4");
                        }
                        else if (ListDinamicPackagePromotional[j].Unity.ToUpper().Equals(KEY.AppSettings("strConsIndUnidMGB").ToUpper()))
                        {
                            ListDinamicPackagePromotional[j].UnityIndicator = KEY.AppSettings("strConsOrdUnid5");
                        }
                    }

                    if (ListDinamicPackagePromotional[j].MovementTypeName != null && ListDinamicPackagePromotional[j].MovementTypeName.ToUpper().Equals(KEY.AppSettings("strConsTMovvNomIli").ToUpper()))
                    {
                        ListDinamicPackagePromotional[j]._Balance = KEY.AppSettings("strConsTMovvNomIli");
                    }

                    listDinamicPackagePromotionalorder.Add(ListDinamicPackagePromotional[j]);
                }

                listDinamicPackagePromotionalorder = listDinamicPackagePromotionalorder.OrderBy(o => o.Order).ThenBy(o => o.UnityIndicator).ThenBy(o => o.Name).ThenBy(o => o.CommercialName).ToList();
                for (int j = 0; j < listDinamicPackagePromotionalorder.Count; j++)
                {
                    response.Add(listDinamicPackagePromotionalorder[j]);
                }

                //OTROS SIN NOMBRE
                listPackageOtherLimited = listDinamicPackage.Where(o => o.OtherIndicator == KEY.AppSettings("strConsIndSi") && o._Balance != null && o.CommercialName != KEY.AppSettings("strConsTarifaDia") && o.Name != KEY.AppSettings("strConsTarifAuto")).OrderBy(o => o.CommercialName).ToList();
                for (int j = 0; j < listPackageOtherLimited.Count; j++)
                {
                    listDinamicPackage.Remove(listPackageOtherLimited[j]);
                    response.Add(listPackageOtherLimited[j]);
                }

                //OTROS CON NOMBRE
                listPackageOrderUnlimited = listDinamicPackage.Where(o => o.OtherIndicator == KEY.AppSettings("strConsIndNo") && o._Balance != null && o.CommercialName != KEY.AppSettings("strConsTarifaDia") && o.Name != KEY.AppSettings("strConsTarifAuto")).OrderBy(o => o.CommercialName).ToList();
                for (int j = 0; j < listPackageOrderUnlimited.Count; j++)
                {
                    listDinamicPackage.Remove(listPackageOrderUnlimited[j]);
                    response.Add(listPackageOrderUnlimited[j]);
                }

                //CLARO PUNTOS
                if (oResponse.unicostResponse != null)
                {
                    item = new Balance();

                    item.Name = KEY.AppSettings("strConstNombrePromocionPuntos");
                    item._Balance = oResponse.unicostResponse.PUNTOS;
                    item.Unity = KEY.AppSettings("strConstUnidadPromocionPuntos");
                    response.Add(item);
                }



                strCodeResponse = oResponse.auditResponse.codigoRespuesta;
                strMessageResponse = oResponse.auditResponse.mensajeRespuesta;

            }
            else
            {
                strCodeResponse = oResponse.auditResponse.codigoRespuesta;
                strMessageResponse = oResponse.auditResponse.mensajeRespuesta;
            }

            return response;
        }


        /// <summary>
        /// Método que obtiene los datos del servicio prepago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strApplicationID">Id de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strCodeResponse"></param>
        /// <returns>Devuelve datos de la búsqueda de número prepago</returns>

        public static Entity.Dashboard.Prepaid.Service GetDataService(string strIdSession, string strPhoneNumber, string strIdTransaction, string strApplicationID, string strApplicationName, string strUserApplication, out string strCodeResponse)
        {
            Entity.Dashboard.Prepaid.Service oService = null;
            List<Entity.Dashboard.Prepaid.Trio> oListTrio = null;
            List<Entity.Dashboard.Prepaid.Account> oListAccountTFI = null;
            List<Entity.Dashboard.Prepaid.Account> oListAccount;

            string strNumInTFIB = "";
            string strProviderTFI = "";
            string strFlagNuevoDatosPrepago = "";
            string strCounterChangeTariffForFree = "";

            strCodeResponse = "";

            try
            {
                strNumInTFIB = KEY.AppSettings("strNumInTFIB");
                strProviderTFI = KEY.AppSettings("strProviderTFI");
                strFlagNuevoDatosPrepago = KEY.AppSettings("FlagNuevoDatosPrepago");
                strCounterChangeTariffForFree = KEY.AppSettings("strCounterChangeTariffForFree");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            int i = Claro.Constants.NumberOne;
            if (string.Equals(strFlagNuevoDatosPrepago, Claro.Constants.NumberOneString, StringComparison.OrdinalIgnoreCase))
            {
                PREPAID_SERVICE.INDatosPrepagoResponse oPrepaidRespons = Claro.Web.Logging.ExecuteMethod<PREPAID_SERVICE.INDatosPrepagoResponse>(strIdSession, strIdTransaction, Configuration.WebServiceConfiguration.PREPAID_SERVICE, () =>
                {
                    return Configuration.WebServiceConfiguration.PREPAID_SERVICE.leerDatosPrepago(new PREPAID_SERVICE.INDatosPrepagoRequest()
                    {
                        telefono = strPhoneNumber
                    });
                });

                PREPAID_SERVICE.DatosPrepago oPrepaidResult;

                if (string.Equals(oPrepaidRespons.resultado.Trim(), Claro.Constants.NumberZeroString, StringComparison.OrdinalIgnoreCase))
                {
                    oService = new Entity.Dashboard.Prepaid.Service();
                    oPrepaidResult = oPrepaidRespons.datosPrePago;
                    oService.NroCelular = Convert.ToString(strPhoneNumber);
                    oService.SaldoPrincipal = Utils.ConvertSoles(oPrepaidResult.onPeakAccountIDBalance);
                    oService.FechaExpiracionSaldo = Convert.ToDate(oPrepaidResult.expiryDate).ToString("dd/MM/yyyy HH:mm:ss");
                    oService.StatusLinea = Convert.ToString(oPrepaidResult.subscriberLifeCycleStatus);
                    oService.CambiosTriosGratis = Convert.ToString(oPrepaidResult.voucherRchFraudCounter);
                    oService.CambiosTarifaGratis = strCounterChangeTariffForFree;
                    oService.PlanTarifario = Convert.ToString(oPrepaidResult.tariffModelNumber);
                    oService.ProviderID = Convert.ToString(oPrepaidResult.providerID);
                    oService.SaldoMinutosSelect = Convert.ToString(oPrepaidResult.bonusCounter_Account54Balance);
                    oService.FechaExpSelect = Convert.ToString(oPrepaidResult.bonusCounter_Account54ExpiryDate);
                    oService.IsSelect = Convert.ToString(oPrepaidResult.isSelect);
                    oService.FecActivacion = Convert.ToDate(oPrepaidResult.firstCallDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.FecExpLinea = Convert.ToDate(oPrepaidResult.deletionDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.FecDol = " ";
                    oService.SubscriberStatus = Convert.ToString(oPrepaidResult.subscriberStatus);
                    oService.CNTNumber = Convert.ToString(oPrepaidResult.cNTNumber);
                    oService.IsCNTPossible = Convert.ToString(oPrepaidResult.isCNTPossible);
                    oService.NroIMSI = Convert.ToString(oPrepaidResult.imsi);
                    oService.StatusIMSI = Convert.ToString(oPrepaidResult.isLocked);
                    oService.TipoTriacion = Convert.ToString(oPrepaidResult.activeFnFOption);
                    oService.Saldo = Convert.ToDouble(oPrepaidResult.mMSPromoAccountIDBalance);
                    oService.FactorDivision = Helper.GetFactorBag("MMSPromoAccountID", "ListaBolsa");

                    if (string.Equals(oService.ProviderID, strProviderTFI, StringComparison.OrdinalIgnoreCase))
                    {
                        oService.EsTFI = true;
                    }
                    else
                    {
                        oService.EsTFI = false;
                    }

                    oListAccount = new List<Entity.Dashboard.Prepaid.Account>();
                    double dblFactorDivision = Helper.GetFactorBag("SMSPromoAccountID", "ListaBolsa");
                    double dblBalance = Convert.ToDouble(oPrepaidResult.sMSPromoAccountIDBalance);
                    string strNombre = "";

                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalanceSms,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.sMSPromoAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.voice1PromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Voice1PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Voice1PromoAccount,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.voice1PromoAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.voice2PromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Voice2PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Voice2PromoAccount,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.voice2PromoAccountIDExpiryDate)
                    });

                    if (oService.EsTFI.Equals(true))
                    {
                        oListAccountTFI = GetBondsTFIPrepaid(strIdSession, strIdTransaction, strApplicationID, strApplicationName, strPhoneNumber, strNumInTFIB);

                        if (oListAccountTFI != null && oListAccountTFI.Count == 2)
                        {
                            oListAccount.Add((Account)oListAccountTFI[1]);
                        }

                        dblBalance = Convert.ToDouble(oPrepaidResult.mMSPromoAccountIDBalance);
                        dblFactorDivision = Helper.GetFactorBag("MMSPromoAccountID", "ListaBolsa");
                        oService.SaldoPendiente = Convert.ToString(dblBalance / dblFactorDivision);
                    }
                    else
                    {
                        dblBalance = Convert.ToDouble(oPrepaidResult.mMSPromoAccountIDBalance);
                        dblFactorDivision = Helper.GetFactorBag("MMSPromoAccountID", "ListaBolsa");
                        oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                        {
                            Nombre = Claro.SIACU.Constants.BalanceMms,
                            Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                            FechaExpiracion = Convert.ToString(oPrepaidResult.mMSPromoAccountIDExpiryDate)
                        });

                    }

                    dblBalance = Convert.ToDouble(oPrepaidResult.bonusPromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("BonusPromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalancePromo,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.bonusPromoAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.sMSLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("SMSLoyaltyAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalanceSmsLoyalty,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.sMSLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.voiceLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("VoiceLoyaltyAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalanceVoiceLoyalty,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.voiceLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.gPRSLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Bonus1PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Promo1Sun,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.gPRSLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.mMSLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Bonus2PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Promo2Sun,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.mMSLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.bonusCounter_Account54Balance);
                    dblFactorDivision = Helper.GetFactorBag("bonusCounterAccount54", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BagMultidestination,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.bonusCounter_Account54ExpiryDate)
                    });

                    if (oService.EsTFI.Equals(true))
                    {
                        strNombre = Claro.SIACU.Constants.BagNumbersFrequentsTfi;
                        dblFactorDivision = Helper.GetFactorBag("BonusCounter_Account52", "ListaBolsa");
                    }
                    else
                    {
                        strNombre = Claro.SIACU.Constants.BagMinutesFree;
                        dblFactorDivision = Helper.GetFactorBag("bonusCounterAccount54", "ListaBolsa");
                    }
                    dblBalance = Convert.ToDouble(oPrepaidResult.bonusCounter_Account52Balance);

                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = strNombre,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.bonusCounter_Account52ExpiryDate)
                    });

                    if (oService.EsTFI.Equals(true) && (oListAccountTFI != null && oListAccountTFI.Count == 2))
                    {
                        oListAccount.Add((Account)oListAccountTFI[0]);
                    }

                    dblBalance = Convert.ToDouble(oPrepaidResult.bonusCounter_Account57Balance);
                    dblFactorDivision = Helper.GetFactorBag("BonusCounterAccount57", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.SmsNumbersFrequents,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.bonusCounter_Account57ExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.bonusCounter_Account58Balance);
                    dblFactorDivision = Helper.GetFactorBag("BonusCounterAccount58", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.SegNumbersFrequents,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.bonusCounter_Account58ExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResult.gPRSPromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("GPRSPromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Gprskb,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResult.gPRSPromoAccountIDExpiryDate)
                    });

                    oService.listAccount = oListAccount;

                    oListTrio = new List<Trio>();

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber0.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber0)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber1.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber1)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber2.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber2)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber3.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber3)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber4.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber4)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber5.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber5)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber6.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber6)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber7.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber7)
                        });
                        i++;
                    }

                    if (!string.IsNullOrEmpty(oPrepaidResult.fnFNumber8.Trim()))
                    {
                        oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                        {
                            Codigo = Claro.SIACU.Constants.NumbersTriad + i.ToString(),
                            Descripcion = Convert.ToString(oPrepaidResult.fnFNumber8)
                        });
                        i++;
                    }

                    oService.NroFamAmigos = oListTrio.Count.ToString();
                    oService.listTrio = oListTrio;



                    string strProviderPrepaid = "";
                    string strModality = "";
                    string strModalidadDBPreNoDataFound = "";
                    string strModalidadTFI = "";
                    try
                    {
                        strProviderPrepaid = KEY.AppSettings("strProviderPrepago");
                        strModalidadDBPreNoDataFound = KEY.AppSettings("strModalidadDBPreNoDataFound");
                        strModalidadTFI = KEY.AppSettings("strModalidadTFI");
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                    if (oService != null && oService.EsTFI.Equals(true))
                    {
                        strModality = strModalidadTFI;
                        oService.TipoContacto = Claro.Constants.LetterT;
                    }
                    else
                    {
                        if (oService != null && strProviderPrepaid.IndexOf(oService.ProviderID) > 0 && !string.IsNullOrEmpty(oService.ProviderID))
                        {
                            oService.TipoContacto = Claro.Constants.LetterP;
                        }
                        else if (string.Equals(strModality, strModalidadDBPreNoDataFound, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(strModality, Claro.Constants.LetterX, StringComparison.OrdinalIgnoreCase))
                        {
                            oService.TipoContacto = "";
                        }
                        else
                        {
                            if (oService != null && oService.NroCelular.Length == 8)
                            {
                                oService.TipoContacto = Claro.Constants.LetterT;
                            }
                            else
                            {
                                oService.TipoContacto = Claro.Constants.LetterP;
                            }
                        }
                    }
                }

                if (oPrepaidRespons.resultado.Trim().Equals(Claro.Constants.NumberZeroString))
                {
                    strCodeResponse = "";
                }
                else
                {
                    strCodeResponse = oPrepaidRespons.resultado.Trim();
                }

            }
            else if (string.Equals(strFlagNuevoDatosPrepago, Claro.Constants.NumberTwoString, StringComparison.OrdinalIgnoreCase))
            {
                PREPAID_SERVICE2.EbsDatosPrepagoV2Client S = Configuration.ServiceConfiguration.PREPAID_SERVICE2;

                PREPAID_SERVICE2.INDatosPrepagoResponse oPrepaidResponsV2 = Claro.Web.Logging.ExecuteMethod<PREPAID_SERVICE2.INDatosPrepagoResponse>(strIdSession, strIdTransaction, Configuration.ServiceConfiguration.PREPAID_SERVICE2, () =>
                {
                    return S.leerDatosPrepago(new PREPAID_SERVICE2.INDatosPrepagoRequest()
                    {
                        telefono = strPhoneNumber
                    });
                });

                PREPAID_SERVICE2.DatosPrepago oPrepaidResultV2;
                if (string.Equals(oPrepaidResponsV2.resultado.Trim(), Claro.Constants.NumberZeroString, StringComparison.OrdinalIgnoreCase))
                {
                    oService = new Entity.Dashboard.Prepaid.Service();
                    oPrepaidResultV2 = oPrepaidResponsV2.datosPrePago;
                    oService.NroCelular = strPhoneNumber;
                    oService.SaldoPrincipal = Utils.ConvertSoles(oPrepaidResultV2.onPeakAccountIDBalance);
                    oService.FechaExpiracionSaldo = Convert.ToDate(oPrepaidResultV2.expiryDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.StatusLinea = Convert.ToString(oPrepaidResultV2.subscriberLifeCycleStatus);
                    oService.CambiosTriosGratis = Convert.ToString(oPrepaidResultV2.voucherRchFraudCounter);
                    oService.CambiosTarifaGratis = strCounterChangeTariffForFree;
                    oService.PlanTarifario = Convert.ToString(oPrepaidResultV2.tariffModelNumber);
                    oService.ProviderID = Convert.ToString(oPrepaidResultV2.providerID);
                    oService.SaldoMinutosSelect = Convert.ToString(oPrepaidResultV2.bonusCounter_Account54Balance);
                    oService.FechaExpSelect = Convert.ToString(oPrepaidResultV2.bonusCounter_Account54ExpiryDate);
                    oService.IsSelect = Convert.ToString(oPrepaidResultV2.isSelect);
                    oService.FecActivacion = Convert.ToDate(oPrepaidResultV2.firstCallDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.FecExpLinea = Convert.ToDate(oPrepaidResultV2.deletionDate).ToString("dd/MM/yyyy hh:mm:ss");
                    oService.FecDol = " ";
                    oService.SubscriberStatus = Convert.ToString(oPrepaidResultV2.subscriberStatus);
                    oService.CNTNumber = Convert.ToString(oPrepaidResultV2.cNTNumber);
                    oService.IsCNTPossible = Convert.ToString(oPrepaidResultV2.isCNTPossible);
                    oService.NroIMSI = Convert.ToString(oPrepaidResultV2.imsi);
                    oService.StatusIMSI = Convert.ToString(oPrepaidResultV2.isLocked);
                    oService.TipoTriacion = Convert.ToString(oPrepaidResultV2.activeFnFOption);
                    oService.Saldo = Convert.ToDouble(oPrepaidResultV2.mMSPromoAccountIDBalance);
                    oService.FactorDivision = Helper.GetFactorBag("MMSPromoAccountID", "ListaBolsa");

                    if (string.Equals(oService.ProviderID, strProviderTFI, StringComparison.OrdinalIgnoreCase))
                    {
                        oService.EsTFI = true;
                    }
                    else
                    {
                        oService.EsTFI = false;
                    }

                    oListAccount = new List<Entity.Dashboard.Prepaid.Account>();
                    double dblFactorDivision = Helper.GetFactorBag("SMSPromoAccountID", "ListaBolsa");
                    double dblBalance = Convert.ToDouble(oPrepaidResultV2.sMSPromoAccountIDBalance);
                    string strNombre = "";


                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalanceSms,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.sMSPromoAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.voice1PromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Voice1PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Voice1PromoAccount,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.voice1PromoAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.voice2PromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Voice2PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Voice2PromoAccount,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.voice2PromoAccountIDExpiryDate)
                    });

                    if (oService.EsTFI.Equals(true))
                    {
                        oListAccountTFI = GetBondsTFIPrepaid(strIdSession, strIdTransaction, strApplicationID, strApplicationName, strPhoneNumber, strNumInTFIB);

                        if (oListAccountTFI != null && oListAccountTFI.Count == 2)
                        {

                            oListAccount.Add((Account)oListAccountTFI[1]);

                        }

                        dblBalance = Convert.ToDouble(oPrepaidResultV2.mMSPromoAccountIDBalance);
                        dblFactorDivision = Helper.GetFactorBag("MMSPromoAccountID", "ListaBolsa");
                        oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                        {
                            Nombre = Claro.SIACU.Constants.BalanceMms,
                            Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                            FechaExpiracion = Convert.ToString(oPrepaidResultV2.mMSPromoAccountIDExpiryDate)
                        });
                    }
                    else
                    {
                        dblBalance = Convert.ToDouble(oPrepaidResultV2.mMSPromoAccountIDBalance);
                        dblFactorDivision = Helper.GetFactorBag("MMSPromoAccountID", "ListaBolsa");
                        oService.SaldoPendiente = Convert.ToString(dblBalance / dblFactorDivision);
                    }

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.bonusPromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("BonusPromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalancePromo,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.bonusPromoAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.sMSLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("SMSLoyaltyAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalanceSmsLoyalty,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.sMSLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.voiceLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("VoiceLoyaltyAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BalanceVoiceLoyalty,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.voiceLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.gPRSLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Bonus1PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Promo1Sun,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.gPRSLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.mMSLoyaltyAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("Bonus2PromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Promo2Sun,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.mMSLoyaltyAccountIDExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.bonusCounter_Account54Balance);
                    dblFactorDivision = Helper.GetFactorBag("bonusCounterAccount54", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.BagMultidestination,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.bonusCounter_Account54ExpiryDate)
                    });

                    if (oService.EsTFI.Equals(true))
                    {
                        strNombre = Claro.SIACU.Constants.BagNumbersFrequentsTfi;
                        dblFactorDivision = Helper.GetFactorBag("BonusCounter_Account52", "ListaBolsa");
                    }
                    else
                    {
                        strNombre = Claro.SIACU.Constants.BagMinutesFree;
                        dblFactorDivision = Helper.GetFactorBag("bonusCounterAccount54", "ListaBolsa");
                    }
                    dblBalance = Convert.ToDouble(oPrepaidResultV2.bonusCounter_Account52Balance);

                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = strNombre,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.bonusCounter_Account52ExpiryDate)
                    });


                    if (oService.EsTFI.Equals(true) && (oListAccountTFI != null && oListAccountTFI.Count == 2))
                    {
                        oListAccount.Add((Account)oListAccountTFI[0]);

                    }

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.bonusCounter_Account57Balance);
                    dblFactorDivision = Helper.GetFactorBag("BonusCounterAccount57", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.SmsNumbersFrequents,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.bonusCounter_Account57ExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.bonusCounter_Account58Balance);
                    dblFactorDivision = Helper.GetFactorBag("BonusCounterAccount58", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.SegNumbersFrequents,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.bonusCounter_Account58ExpiryDate)
                    });

                    dblBalance = Convert.ToDouble(oPrepaidResultV2.gPRSPromoAccountIDBalance);
                    dblFactorDivision = Helper.GetFactorBag("GPRSPromoAccountID", "ListaBolsa");
                    oListAccount.Add(new Entity.Dashboard.Prepaid.Account()
                    {
                        Nombre = Claro.SIACU.Constants.Gprskb,
                        Saldo = new Claro.Utils().GetDivision(dblBalance, dblFactorDivision),
                        FechaExpiracion = Convert.ToString(oPrepaidResultV2.gPRSPromoAccountIDExpiryDate)
                    });

                    oService.listAccount = oListAccount;

                    if (oPrepaidResultV2.fnfNumber != null)
                    {
                        for (int j = 0; j < oPrepaidResultV2.fnfNumber.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(oPrepaidResultV2.fnfNumber[j].telefono))
                            {
                                oListTrio = new List<Entity.Dashboard.Prepaid.Trio>();
                                oListTrio.Add(new Entity.Dashboard.Prepaid.Trio()
                                {
                                    Codigo = Claro.SIACU.Constants.NumbersTriad + (j + 1).ToString(),
                                    Descripcion = Convert.ToString(oPrepaidResultV2.fnfNumber[j].telefono)
                                });
                            }
                        }
                    }
                    oService.NroFamAmigos = oListTrio.Count.ToString();
                    oService.listTrio = oListTrio;


                    string strProviderPrepaid = "";
                    string strModality = "";
                    string strModalidadDBPreNoDataFound = "";
                    string strModalidadTFI = "";
                    try
                    {
                        strProviderPrepaid = KEY.AppSettings("strProviderPrepago");
                        strModalidadDBPreNoDataFound = KEY.AppSettings("strModalidadDBPreNoDataFound");
                        strModalidadTFI = KEY.AppSettings("strModalidadTFI");
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                    if (oService != null && oService.EsTFI.Equals(true))
                    {
                        strModality = strModalidadTFI;
                        oService.TipoContacto = Claro.Constants.LetterT;
                    }
                    else
                    {
                        if (oService != null && strProviderPrepaid.IndexOf(oService.ProviderID) > 0 && !string.IsNullOrEmpty(oService.ProviderID))
                        {
                            oService.TipoContacto = Claro.Constants.LetterP;
                        }
                        else if (string.Equals(strModality, strModalidadDBPreNoDataFound, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(strModality, Claro.Constants.LetterX, StringComparison.OrdinalIgnoreCase))
                        {
                            oService.TipoContacto = "";
                        }
                        else
                        {
                            if (oService != null && oService.NroCelular.Length == 8)
                            {
                                oService.TipoContacto = Claro.Constants.LetterT;
                            }
                            else
                            {
                                oService.TipoContacto = Claro.Constants.LetterP;
                            }
                        }
                    }
                }

                if (oPrepaidResponsV2.resultado.Trim().Equals(Claro.Constants.NumberZeroString))
                {
                    strCodeResponse = "";
                }
                else
                {
                    strCodeResponse = oPrepaidResponsV2.resultado.Trim();
                }
            }
            string strfecha = "";
            string strbalance = "";

            if (oService != null)
            {
                try
                {

                    oService.listAccounts = getDataBag(strIdSession, strIdTransaction, strApplicationID, strApplicationName, strUserApplication, strPhoneNumber, ref strfecha, ref strbalance);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }
            }

            return oService;
        }

        private static List<Entity.Dashboard.Prepaid.Account> getDataBag(string strIdSession, string strIdTransaction, string strApplicationID,
            string strApplicationName, string strUserApplication, string line, ref string strfecha, ref string strbalance)
        {
            List<Entity.Dashboard.Prepaid.Account> oListAccount = new List<Entity.Dashboard.Prepaid.Account>();



            PREPAID_CONSULTDATA.consultarDatosPrepagoRequest request = new PREPAID_CONSULTDATA.consultarDatosPrepagoRequest();
            request.auditRequest = new PREPAID_CONSULTDATA.auditRequestType()
            {
                idTransaccion = strIdTransaction,
                ipAplicacion = strApplicationID,
                nombreAplicacion = strApplicationName,
                usuarioAplicacion = strUserApplication
            };
            request.msisdn = line;
            request.tipoConsulta = "compuesto";


            PREPAID_CONSULTDATA.operacionesType[] op = new PREPAID_CONSULTDATA.operacionesType[1];
            op[0] = new PREPAID_CONSULTDATA.operacionesType();
            op[0].codigoOperacion = "EntireRead";
            PREPAID_CONSULTDATA.modificadorType[] mod = new PREPAID_CONSULTDATA.modificadorType[3];

            mod[0] = new PREPAID_CONSULTDATA.modificadorType();
            mod[0].nombreModificador = "Customer";
            PREPAID_CONSULTDATA.parametroType[] param1 = new PREPAID_CONSULTDATA.parametroType[4];
            param1[0] = new PREPAID_CONSULTDATA.parametroType();
            param1[0].nombre = "billCycleId";
            param1[1] = new PREPAID_CONSULTDATA.parametroType();
            param1[1].nombre = "billCycleIdAfterSwitch";
            param1[2] = new PREPAID_CONSULTDATA.parametroType();
            param1[2].nombre = "billCycleSwitch";
            param1[3] = new PREPAID_CONSULTDATA.parametroType();
            param1[3].nombre = "category";
            mod[0].parametro = param1;

            mod[1] = new PREPAID_CONSULTDATA.modificadorType();
            mod[1].nombreModificador = "ROP";
            PREPAID_CONSULTDATA.parametroType[] param2 = new PREPAID_CONSULTDATA.parametroType[6];
            param2[0] = new PREPAID_CONSULTDATA.parametroType();
            param2[0].nombre = "ActiveEndDate";
            param2[1] = new PREPAID_CONSULTDATA.parametroType();
            param2[1].nombre = "GraceEndDate";
            param2[2] = new PREPAID_CONSULTDATA.parametroType();
            param2[2].nombre = "IsMTCLockUsed";
            param2[3] = new PREPAID_CONSULTDATA.parametroType();
            param2[3].nombre = "s_CRMTitle";
            param2[4] = new PREPAID_CONSULTDATA.parametroType();
            param2[4].nombre = "s_OfferId";
            param2[5] = new PREPAID_CONSULTDATA.parametroType();
            param2[5].nombre = "OnPeakAccountID_FU";
            mod[1].parametro = param2;

            mod[2] = new PREPAID_CONSULTDATA.modificadorType();
            mod[2].nombreModificador = "RPP";
            PREPAID_CONSULTDATA.parametroType[] param3 = new PREPAID_CONSULTDATA.parametroType[2];
            PREPAID_CONSULTDATA.parametroListaType[] paramLista1 = new PREPAID_CONSULTDATA.parametroListaType[2];
            param3[0] = new PREPAID_CONSULTDATA.parametroType();
            param3[0].nombre = "s_ActivationEndTime";
            param3[1] = new PREPAID_CONSULTDATA.parametroType();
            param3[1].nombre = "s_PackageId";

            paramLista1[0] = new PREPAID_CONSULTDATA.parametroListaType();
            paramLista1[0].nombre = "s_PeriodicBonus";
            PREPAID_CONSULTDATA.parametroType[] paramLista1_param1 = new PREPAID_CONSULTDATA.parametroType[1];
            paramLista1_param1[0] = new PREPAID_CONSULTDATA.parametroType();
            paramLista1_param1[0].nombre = "ExpiryDate";
            paramLista1[0].parametro = paramLista1_param1;

            paramLista1[1] = new PREPAID_CONSULTDATA.parametroListaType();
            paramLista1[1].nombre = "s_PeriodicBonus_FU";
            PREPAID_CONSULTDATA.parametroType[] paramLista1_param2 = new PREPAID_CONSULTDATA.parametroType[1];
            paramLista1_param2[0] = new PREPAID_CONSULTDATA.parametroType();
            paramLista1_param2[0].nombre = "Balance";
            paramLista1[1].parametro = paramLista1_param2;
            mod[2].parametroLista = paramLista1;
            mod[2].parametro = param3;

            op[0].listaModificador = mod;

            request.listaOperacionesConsulta = op;

            PREPAID_CONSULTDATA.consultarDatosPrepagoResponse response = Claro.Web.Logging.ExecuteMethod<PREPAID_CONSULTDATA.consultarDatosPrepagoResponse>(strIdSession, strIdTransaction, Configuration.ServiceConfiguration.PREPAID_CONSULTDATA, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_CONSULTDATA.consultarDatosPrepagoSiac(request);
            });

            if (response.auditResponse.codigoRespuesta.Equals("0"))
            {
                PREPAID_CONSULTDATA.modificadorType modificador;
                PREPAID_CONSULTDATA.parametroType[] paramentros;
                PREPAID_CONSULTDATA.parametroListaType[] paramentrosListaM;
                PREPAID_CONSULTDATA.parametroType[] paramentrosM1;

                for (int i = 0; i < response.listaOperacionesRespuesta[0].listaModificador.Length; i++)
                {

                    modificador = response.listaOperacionesRespuesta[0].listaModificador[i];

                    if (modificador.nombreModificador.Equals("ROP"))
                    {
                        paramentros = modificador.parametro;
                        paramentrosListaM = modificador.parametroLista;

                        for (int e = 0; e < paramentros.Length; e++)
                        {
                            if (paramentros[e].nombre.Equals("ActiveEndDate"))
                            {
                                strfecha = paramentros[e].valor;
                            }
                        }

                        for (int j = 0; j < paramentrosListaM.Length; j++)
                        {

                            if (paramentrosListaM[j].nombre.Equals("OnPeakAccountID_FU"))
                            {

                                paramentrosM1 = paramentrosListaM[j].parametro;
                                for (int u = 0; u < paramentrosM1.Length; u++)
                                {
                                    if (paramentrosM1[u].nombre.Equals("Balance"))
                                    {
                                        strbalance = paramentrosM1[u].valor;
                                    }
                                }
                            }
                        }

                    }
                    if (modificador.nombreModificador.Equals("RPP"))
                    {

                        paramentros = modificador.parametro;
                        paramentrosListaM = modificador.parametroLista;

                        Entity.Dashboard.Prepaid.Account bp = new Entity.Dashboard.Prepaid.Account();
                        for (int e = 0; e < paramentros.Length; e++)
                        {
                            if (paramentros[e].nombre.Equals("NombreComercial"))
                            {
                                bp.Nombre = paramentros[e].valor;
                            }
                            else if (paramentros[e].nombre.Equals("Orden"))
                            {
                                bp.Orden = paramentros[e].valor;
                            }
                            else if (paramentros[e].nombre.Equals("FechaVigenciaBolsa"))
                            {
                                bp.FechaExpiracion = paramentros[e].valor;
                            }
                        }

                        for (int j = 0; j < paramentrosListaM.Length; j++)
                        {

                            if (paramentrosListaM[j].nombre.Equals("s_PeriodicBonus_FU"))
                            {
                                paramentrosM1 = paramentrosListaM[j].parametro;
                                for (int u = 0; u < paramentrosM1.Length; u++)
                                {
                                    if (paramentrosM1[u].nombre.Equals("Balance"))
                                    {
                                        bp.Saldo = paramentrosM1[u].valor;
                                    }
                                }
                            }
                        }

                        oListAccount.Add(bp);

                    }
                }
            }

            return oListAccount;
        }
        /// <summary>
        /// Método que retorna Banca Móvil SAP.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strApplicationID">Id de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strApplicationUser">Usuario de aplicación</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strMatter">Materia</param>
        /// <param name="strIssueSeries">Número de serie</param>
        /// <returns>Devuelve cadena ICCID</returns>

        public static string GetMobileBankingSAP(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber, string strMatter, string strIssueSeries)
        {
            string strICCID = "";
            string CodigoRespuesta = "";
            string MensajeRespuesta = "";

            Claro.Web.Logging.Info(strIdSession, strTransactionID, "Inicio del Método GetMobileBankingSAP");

            Claro.Web.Logging.Info(strIdSession, strTransactionID,
            string.Format("Parámetros de entrada: [strIdSession] - {0} ; [strTransactionID] - {1} ; [strApplicationID] - {2} ; " +
                          "[strApplicationName] - {3} ; [strApplicationUser] - {4} ; [strPhoneNumber] - {5} ; " +
                          "[strMatter] - {6} ; [strIssueSeries] - {7}", strIdSession, strTransactionID, strApplicationID, strApplicationName,
                          strApplicationUser, strPhoneNumber, strMatter, strIssueSeries));

            Claro.Web.Logging.Info(strApplicationUser, strTransactionID, "Inicio de Ejecución del WS ConsultaClaves - Método Desencriptar");


            CONSULTCLAVE.desencriptarRequestBody objRequest = new CONSULTCLAVE.desencriptarRequestBody();
            CONSULTCLAVE.desencriptarResponseBody objResponse = new CONSULTCLAVE.desencriptarResponseBody();

            objRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            objRequest.ipAplicacion = KEY.AppSettings("consIpAppConsultaClavesWS");
            objRequest.ipTransicion = KEY.AppSettings("consIpTransConsultaClavesWS");
            objRequest.usrAplicacion = strApplicationUser;
            objRequest.idAplicacion = KEY.AppSettings("CodAplicacion");
            objRequest.codigoAplicacion = KEY.AppSettings("strCodigoAplicacion");
            objRequest.usuarioAplicacionEncriptado = KEY.AppSettings("SIACU_UserEnc_ConsultaDatosSim");
            objRequest.claveEncriptado = KEY.AppSettings("SIACU_PassEnc_ConsultaDatosSim");

            Claro.Web.Logging.Info(strIdSession, strTransactionID,
            string.Format("Parámetros de entrada: [idTransaccion] - {0} ; [ipAplicacion] - {1} ; [ipTransicion] - {2} ; " +
            "[usrAplicacion] - {3} ; [idAplicacion] - {4} ; [codigoAplicacion] - {5} ; " +
            "[usuarioAplicacionEncriptado] - {6} ; [claveEncriptado] - {7}", objRequest.idTransaccion, objRequest.ipAplicacion, objRequest.ipTransicion, objRequest.usrAplicacion,
            objRequest.idAplicacion, objRequest.codigoAplicacion, "****", "****"));

            objResponse.codigoResultado = Claro.SIACU.Data.Configuration.ServiceConfiguration.CONSULTCLAVE.desencriptar(ref objRequest.idTransaccion, objRequest.ipAplicacion,
            objRequest.ipTransicion, objRequest.usrAplicacion, objRequest.idAplicacion, objRequest.codigoAplicacion, objRequest.usuarioAplicacionEncriptado,
            objRequest.claveEncriptado, out objResponse.mensajeResultado, out objResponse.usuarioAplicacion, out objResponse.clave);

            Claro.Web.Logging.Info(strIdSession, strTransactionID, string.Format("Parámetros de salida: [mensajeResultado] - {0} ; [usuarioDesencriptado] - {1} ; [claveDesencriptado] - {2}",
            objResponse.mensajeResultado, "****", "****"));

            Claro.Web.Logging.Info(strApplicationUser, strTransactionID, "Fin de Ejecución del WS ConsultaClaves - Método Desencriptar");

            if (objResponse.codigoResultado == "0")
            {
                Claro.Web.Logging.Info(strApplicationUser, strTransactionID, "Inicio de Ejecución del WS ConsultaDatosSIM - Método ConsultarDatosSIM");

                CONSULTAR_DATOS_SIM.HeaderRequestType objHeaderRequestType = new CONSULTAR_DATOS_SIM.HeaderRequestType()
            {
                country = KEY.AppSettings("consCountry"),
                language = KEY.AppSettings("consLanguage"),
                consumer = KEY.AppSettings("consConsumer"),
                system = KEY.AppSettings("consSystem"),
                modulo = KEY.AppSettings("consModulo"),
                pid = strTransactionID,
                userId = KEY.AppSettings("consUserId"),
                dispositivo = KEY.AppSettings("consDispositivo"),
                wsIp = KEY.AppSettings("consIpBSS_ConsultaDatosSim"),
                operation = KEY.AppSettings("consOperation"),
                timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz"),
                VarArg = new CONSULTAR_DATOS_SIM.ArgType[1],
                msgType = KEY.AppSettings("consMsgType")
            };

                objHeaderRequestType.VarArg[0] = new CONSULTAR_DATOS_SIM.ArgType() { key = string.Empty, value = string.Empty };

                CONSULTAR_DATOS_SIM.HeaderRequestType1 objHeaderRequestType1 = new CONSULTAR_DATOS_SIM.HeaderRequestType1()
                {
                    canal = KEY.AppSettings("consCanal"),
                    idAplicacion = KEY.AppSettings("consIdAplicacion"),
                    idAplicacionSpecified = true,
                    usuarioAplicacion = KEY.AppSettings("consUserApp"),
                    usuarioSesion = strApplicationUser,
                    idTransaccionNegocio = strTransactionID,
                    idTransaccionESB = strTransactionID,
                    fechaInicio = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz"),
                    fechaInicioSpecified = true,
                    nodoAdicional = KEY.AppSettings("consNodoAdicional_BSS_ConsultaDatosSim")
                };

                Claro.Web.Logging.Info(strIdSession, strTransactionID,
                string.Format("Parámetros de entrada DP: [country] - {0} ; [language] - {1} ; [consumer] - {2} ; [system] - {3} ; [modulo] - {4} ; [pid] - {5} ; " +
                "[userId] - {6} ; [dispositivo] - {7} ; [wsIp] - {8} ; [operation] - {9} ; [timestamp] - {10} ; [msgType] - {11}", objHeaderRequestType.country,
                objHeaderRequestType.language, objHeaderRequestType.consumer, objHeaderRequestType.system, objHeaderRequestType.modulo, objHeaderRequestType.pid,
                objHeaderRequestType.userId, objHeaderRequestType.dispositivo, objHeaderRequestType.wsIp, objHeaderRequestType.operation, objHeaderRequestType.timestamp,
                objHeaderRequestType.msgType));

                Claro.Web.Logging.Info(strIdSession, strTransactionID,
                string.Format("Parámetros de entrada OSB: [canal] - {0} ; [idAplicacion] - {1} ; [usuarioAplicacion] - {2} ; [usuarioSesion] - {3} ; [idTransaccionESB] - {4} ; [fechaInicio] - {5} ; " +
                "[nodoAdicional] - {6}", objHeaderRequestType1.canal, objHeaderRequestType1.idAplicacion, objHeaderRequestType1.usuarioAplicacion, objHeaderRequestType1.usuarioSesion,
                objHeaderRequestType1.idTransaccionESB, objHeaderRequestType1.fechaInicio, objHeaderRequestType1.nodoAdicional));

                CONSULTAR_DATOS_SIM.consultarDatosSIMRequest objConsultarDatosSIMRequest = new CONSULTAR_DATOS_SIM.consultarDatosSIMRequest();
                CONSULTAR_DATOS_SIM.consultaDatosSIMType type = new CONSULTAR_DATOS_SIM.consultaDatosSIMType();
                type.msisdn = strPhoneNumber;
                objConsultarDatosSIMRequest.listaRequestOpcional = new CONSULTAR_DATOS_SIM.RequestOpcionalTypeRequestOpcional[1];

                objConsultarDatosSIMRequest.consultaDatosSIM = type;
                objConsultarDatosSIMRequest.listaRequestOpcional[0] = new CONSULTAR_DATOS_SIM.RequestOpcionalTypeRequestOpcional { campo = string.Empty, valor = string.Empty };

                CONSULTAR_DATOS_SIM.HeaderResponseType1 headerResponse1 = new CONSULTAR_DATOS_SIM.HeaderResponseType1();
                CONSULTAR_DATOS_SIM.consultarDatosSIMResponse objConsultarDatosSIMResponse = new CONSULTAR_DATOS_SIM.consultarDatosSIMResponse();

                Claro.Web.Logging.Info(strIdSession, strTransactionID, "OperationContextScope");

                using (new OperationContextScope(Configuration.ServiceConfiguration.CONSULTA_DATOS_SIM.InnerChannel))
                {
                    Claro.Web.Logging.Info(strIdSession, strTransactionID, "OperationContextScope: Cabecera");
                    OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(strTransactionID, objResponse.usuarioAplicacion, objResponse.clave));
                    Claro.Web.Logging.Info("ConsultaDatosSIM: DataPower", "****", "****");

                    CONSULTAR_DATOS_SIM.HeaderResponseType objHeaderResponseType = Claro.Web.Logging.ExecuteMethod<CONSULTAR_DATOS_SIM.HeaderResponseType>
                    (strIdSession, strTransactionID, Claro.SIACU.Data.Configuration.ServiceConfiguration.CONSULTA_DATOS_SIM, () =>
            {
                return Claro.SIACU.Data.Configuration.ServiceConfiguration.CONSULTA_DATOS_SIM.consultarDatosSIM(objHeaderRequestType,
                objHeaderRequestType1, objConsultarDatosSIMRequest, out headerResponse1, out objConsultarDatosSIMResponse);
            });
                }

                CodigoRespuesta = objConsultarDatosSIMResponse.responseStatus.codigoRespuesta;
                MensajeRespuesta = objConsultarDatosSIMResponse.responseStatus.descripcionRespuesta;
                strICCID = objConsultarDatosSIMResponse.responseData.listaDatosSIM.iccid;

                Claro.Web.Logging.Info(strIdSession, strTransactionID, string.Format("CONSULTA_DATOS_SIM -> Respuesta: [CodigoRespuesta] - {0} ; [MensajeRespuesta] - {1}", objConsultarDatosSIMResponse.responseStatus.codigoRespuesta, objConsultarDatosSIMResponse.responseStatus.descripcionRespuesta));
            }

            Claro.Web.Logging.Info(strIdSession, strTransactionID, "Fin del Método GetMobileBankingSAP");

            return strICCID;
        }

        /// <summary>
        /// Método que retorna si es o no bloqueo por Biometría.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns>Devuelve verdad o falso según el número consultado.</returns>

        public static bool GetReasonLineBiometrics(string strIdSession, string strTransaction, string strTelephone)
        {
            bool boolResponse;
            string strBloqueo = "";
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_phone", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("txtBloqueo", DbType.Object, ParameterDirection.Output,strBloqueo)
            };
            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBT_ULT_BLOQUEO, parameters);
            boolResponse = true;
            return boolResponse;
        }

        /// <summary>
        /// Método que obtiene el segmento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve la descripción del segmento.</returns>

        public static string GetSegment(string strIdSession, string strTransaction, string strPhoneNumber)
        {
            StringBuilder strDescripcion = new StringBuilder();
            string strComa = "";
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TELEFONO", DbType.String,255, ParameterDirection.Input,strPhoneNumber),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
            List<Claro.SIACU.Entity.Dashboard.Prepaid.SegmentDescription> list = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Dashboard.Prepaid.SegmentDescription>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_CLAROCLUB, DbCommandConfiguration.SIACU_ADMPSS_CON_BENEFICIO, parameters);
            if (list != null)
            {
                foreach (var item in list)
                {
                    strDescripcion.AppendFormat("{0}{1}", strComa, Convert.ToString(item.Descripcion));
                    strComa = ", ";
                }
            }
            return strDescripcion.ToString();
        }

        /// <summary>
        /// Método que obtiene el listado de tipo de tríos.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strSystem">Valor de sistema</param>
        /// <returns>Devuelve lista con valores de tipo de triación.</returns>


        public static List<Entity.Dashboard.Prepaid.TypeTriation> GetListTriation(string strIdSession, string strTransaction, string strSystem)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_sistema", DbType.String,1, ParameterDirection.Input,strSystem),
                new DbParameter("cursor_salida", DbType.Object, ParameterDirection.Output)
            };
            return DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.TypeTriation>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_TRIACION, parameters);
        }


        /// <summary>
        /// Método que obtiene descripción plan prepago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strProviderID">Id provider</param>
        /// <param name="strPlanTariff">Plan tarifario</param>
        /// <param name="strSubscriberStatus">Estado de subscriptor</param>
        /// <returns>Devuele la descripción del plan prepago.</returns>

        public static string GetDescriptionPlan(string strIdSession, string strTransaction, string strProviderID, string strPlanTariff, string strSubscriberStatus)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_SUBSCRIBER", DbType.String,255,ParameterDirection.Input, strSubscriberStatus),
                new DbParameter("P_PROVIDER_ID", DbType.String,255, ParameterDirection.Input, strProviderID),
                new DbParameter("P_TARIFF_MODEL", DbType.String,255,ParameterDirection.Input, strPlanTariff),
                new DbParameter("P_DESC_PLAN", DbType.String,255, ParameterDirection.Output)
            };
            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_PLAN_PREPAGO, parameters);
            return Convert.ToString(parameters[3].Value);
        }

        /// <summary>
        /// Método que obtiene el ods para recuperar el estado del subscriber.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intPhoneNumber">Número de teléfono</param>
        /// <returns>Retorna el días experiación</returns>

        public static string GetStateSubscriber(string strIdSession, string strTransaction, int intPhoneNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("v_retorno", DbType.String, 80, ParameterDirection.ReturnValue),
                new DbParameter("i_msisdn", DbType.Int32, ParameterDirection.Input, intPhoneNumber)
            };
            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DWO, DbCommandConfiguration.SIACU_FN_GETESTADOSUBSCRIBER2, parameters);

            return Convert.ToString(parameters[0].Value);
        }

        /// <summary>
        /// Método que obtiene la lista de una consulta HLR.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strPhoneNumber">Número de Teléfono</param>
        /// <returns>Retorna Lista HLR</returns>

        public static List<Entity.Dashboard.HLR> GetHLR(string strIdSession, string strTransaction, string strPhoneNumber)
        {
            List<Entity.Dashboard.HLR> oListHLR = null;
            string strgConstKeyFlgZMQO = "";
            try
            {
                strgConstKeyFlgZMQO = KEY.AppSettings("gConstKeyFlgZMQO");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            COMMON_HLR.HLRResponseSIAC objhlrResponse = null;
            try
            {

                objhlrResponse = Claro.Web.Logging.ExecuteMethod<COMMON_HLR.HLRResponseSIAC>(strIdSession, strTransaction, Configuration.WebServiceConfiguration.COMMON_HLR, () =>
                {
                    return objhlrResponse = Configuration.WebServiceConfiguration.COMMON_HLR.hlrConsultasSIAC(new COMMON_HLR.HLRRequestSIAC()
                    {
                        msisdn = strPhoneNumber
                    });
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            if (objhlrResponse != null)
            {
                COMMON_HLR.HLRResponseSIACHLRResponseZMIO objZMIO = objhlrResponse.HLRResponseZMIO;
                COMMON_HLR.HLRResponseSIACHLRResponseZMNO objZMNO = objhlrResponse.HLRResponseZMNO;
                COMMON_HLR.HLRResponseSIACHLRResponseZMGO objZMGO = objhlrResponse.HLRResponseZMGO;
                COMMON_HLR.HLRResponseSIACHLRResponseZMSO objZMSO = objhlrResponse.HLRResponseZMSO;
                COMMON_HLR.HLRResponseSIACHLRResponseZMQODispIN objZMQODispIN = objhlrResponse.HLRResponseZMQODispIN;
                COMMON_HLR.HLRResponseSIACHLRResponseZMQO objZMQO = objhlrResponse.HLRResponseZMQO;
                string strMessageError = objhlrResponse.mensaje.ToString();

                if (!Convert.ToString(objZMIO).Equals(String.Empty) && objZMIO.lineas.Length > 0)
                {
                    if (oListHLR == null) oListHLR = new List<HLR>();
                    for (int i = 0; i < objZMIO.lineas.Length; i++)
                    {
                        oListHLR.Add(new Entity.Dashboard.HLR()
                        {
                            Code = i.ToString(),
                            Description = objZMIO.lineas[i].linea,
                            DescriptionError = strMessageError
                        });
                    }
                }

                if (!Convert.ToString(objZMNO).Equals(String.Empty) && objZMNO.lineas.Length > 0)
                {
                    if (oListHLR == null) oListHLR = new List<HLR>();
                    for (int i = 0; i < objZMNO.lineas.Length; i++)
                    {
                        oListHLR.Add(new Entity.Dashboard.HLR()
                        {
                            Code = i.ToString(),
                            Description = objZMNO.lineas[i].linea,
                            DescriptionError = strMessageError
                        });
                    }
                }

                if (!Convert.ToString(objZMGO).Equals(String.Empty) && objZMGO.lineas.Length > 0)
                {
                    if (oListHLR == null) oListHLR = new List<HLR>();
                    for (int i = 0; i < objZMGO.lineas.Length; i++)
                    {
                        oListHLR.Add(new Entity.Dashboard.HLR()
                        {
                            Code = i.ToString(),
                            Description = objZMGO.lineas[i].linea,
                            DescriptionError = strMessageError
                        });
                    }
                }

                if (!Convert.ToString(objZMSO).Equals(String.Empty) && objZMSO.lineas.Length > 0)
                {
                    if (oListHLR == null) oListHLR = new List<HLR>();
                    for (int i = 0; i < objZMSO.lineas.Length; i++)
                    {
                        oListHLR.Add(new Entity.Dashboard.HLR()
                        {
                            Code = i.ToString(),
                            Description = objZMSO.lineas[i].linea,
                            DescriptionError = strMessageError
                        });
                    }
                }

                if (!Convert.ToString(objZMQODispIN).Equals(String.Empty) && objZMQODispIN.lineas.Length > 0)
                {
                    if (oListHLR == null) oListHLR = new List<HLR>();
                    for (int i = 0; i < objZMQODispIN.lineas.Length; i++)
                    {
                        oListHLR.Add(new Entity.Dashboard.HLR()
                        {
                            Code = i.ToString(),
                            Description = objZMQODispIN.lineas[i].linea,
                            DescriptionError = strMessageError
                        });
                    }
                }

                if (!Convert.ToString(objZMQO).Equals(String.Empty) && objZMQO.lineas.Length > 0)
                {
                    if (oListHLR == null) oListHLR = new List<HLR>();
                    for (int i = 0; i < objZMQO.lineas.Length; i++)
                    {
                        oListHLR.Add(new Entity.Dashboard.HLR()
                        {
                            Code = i.ToString(),
                            Description = objZMQO.lineas[i].linea,
                            DescriptionError = strMessageError,
                            State = strgConstKeyFlgZMQO
                        });
                    }
                }
            }

            return oListHLR;
        }

        /// <summary>
        /// Método que retorna el Banner.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="dteDate">Fecha actual</param>
        /// <param name="strTelephoneType">Número de teléfono</param>
        /// <returns>Devuelve </returns>

        public static List<Banner> GetBanner(string strIdSession, string strTransaction, DateTime dteDate, string strTelephoneType)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_FECHA_PROCESO", DbType.Date,ParameterDirection.Input, dteDate),
                new DbParameter("P_TIPO_TELEFONO", DbType.String,255, ParameterDirection.Input, strTelephoneType),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Banner>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_BANNER_VIGENTE, parameters);
        }


        /// <summary>
        /// Método que devuelve datos de los productos prepago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strUserName">Nombre de usuario</param>
        /// <param name="strIPAddress">Dirección de IP</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDocumentIdentity">Documento de identidad</param>
        /// <param name="strTypeDocument">Tipo de documento</param>
        /// <param name="strState">Estado</param>
        /// <returns>Devuelve datos de productos prepago</returns>

        public static PrepaidProductsResponse GetPrepaidProducts(string strIdSession, string strUserName, string strIPAddress, string strApplicationName, string strTransaction, string strDocumentIdentity, string strTypeDocument, string strState)
        {
            PrepaidProductsResponse oGetPrepaidProductsResponse = null;
            PREPAID_PRODUCTS.ConsultarProductosPrepagoResponse oConsultarProductosPrepagoResponse = Claro.Web.Logging.ExecuteMethod<PREPAID_PRODUCTS.ConsultarProductosPrepagoResponse>(() =>
            {
                return Configuration.ServiceConfiguration.PREPAID_PRODUCTS.consultarProductosPrepago(new PREPAID_PRODUCTS.ConsultarProductosPrepagoRequest()
                {
                    docIdentidad = strDocumentIdentity,
                    tipoDocIdentidad = strTypeDocument,
                    estado = strState,
                    auditRequest = new ProxyService.SIACU.Pre.Products.auditRequestType()
                    {
                        usuarioAplicacion = strUserName,
                        ipAplicacion = strIPAddress,
                        nombreAplicacion = strApplicationName,
                        idTransaccion = strTransaction
                    },
                    listaRequestOpcional = new Claro.SIACU.ProxyService.SIACU.Pre.Products.parametrosTypeObjetoOpcional[1]
                });
            });

            if (oConsultarProductosPrepagoResponse != null)
            {
                if (oConsultarProductosPrepagoResponse.auditResponse.codigoRespuesta.Equals(Claro.Constants.NumberTwoString) || Convert.ToInt(oConsultarProductosPrepagoResponse.auditResponse.codigoRespuesta) < Claro.Constants.NumberZero)
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Constants.MessageNotConsultationProductsPrepaid);
                    throw new MessageException(Claro.SIACU.Constants.MessageNotConsultationProductsPrepaid);
                }

                oGetPrepaidProductsResponse = new PrepaidProductsResponse()
                {
                    ErrorMsg = oConsultarProductosPrepagoResponse.auditResponse.mensajeRespuesta,
                    CodeError = oConsultarProductosPrepagoResponse.auditResponse.codigoRespuesta
                };

                if (oConsultarProductosPrepagoResponse.datosPrepagoResponse != null)
                {
                    Product oServicePre;
                    foreach (var item in oConsultarProductosPrepagoResponse.datosPrepagoResponse.listaServicios)
                    {
                        oServicePre = new Product();
                        string state = Helper.GetValueXML(!string.IsNullOrEmpty(item.estado.Trim()) ? item.estado : "", KEY.AppSettings("ListaEstadoChangeLanguage"));
                        oServicePre.Estado = !string.IsNullOrEmpty(state) ? state : item.estado;
                        oServicePre.FechaActivacion = item.fechaActivacion;
                        oServicePre.ObjId = item.objId;
                        oServicePre.Telefono = item.telefono;
                        oServicePre.TipoProducto = item.tipoProducto;

                        oGetPrepaidProductsResponse.ListProduct.Add(oServicePre);
                    }

                }
            }

            return oGetPrepaidProductsResponse;
        }

        /// <summary>
        /// Método que devuelve lista de instantáneas de acuerdo a los parámetros de búsqueda.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strDataSearch">Dats de búsqueda</param>
        /// <param name="strTypePhone">Tipo de teléfono</param>
        /// <returns></returns>

        public static List<Instant> GetListInstant(string strIdSession, string strTransaction, string strDataSearch, string strTypePhone)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_DATO_BUSQUEDA", DbType.String,255, ParameterDirection.Input, strDataSearch),
                new DbParameter("P_TIPO_TELEFONO", DbType.String,255, ParameterDirection.Input, strTypePhone),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Instant>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_LISTAR_INSTANTANEA_VIGENTE, parameters);
        }

        /// <summary>
        /// Método que valida Bonos
        /// </summary>
        /// <param name="inSSUSCRIBER"></param>
        /// <param name="ouMSG_ERR"></param>
        /// <returns></returns>

        public static string ValidateBond(string strIdSession, string strTransaction, string inSSUSCRIBER, out string ouMSG_ERR)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("inSSUSCRIBER", DbType.String,255, ParameterDirection.Input, inSSUSCRIBER),
                new DbParameter("ouCOD_ERR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_VALIDA_BONO, parameters);

            ouMSG_ERR = parameters[2].Value.ToString();

            return parameters[1].Value.ToString();
        }

        /// <summary>
        /// Método para obtener cantidad de Bonos
        /// </summary>
        /// <param name="MSISDN"></param>
        /// <param name="cantBono"></param>
        /// <returns></returns>

        public static string GetAmountBond(string strIdSession, string strTransaction, string MSISDN)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("inMSISDN", DbType.String,255, ParameterDirection.Input, MSISDN),
                new DbParameter("ouCANT", DbType.String,255, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_CANT_BONO, parameters);

            return parameters[2].Value.ToString();
        }

        /// <summary>
        /// Método que devuelve una lista con el historial de triaciones de una línea.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">I de transacción</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <param name="strDateStart">Fecha inicial</param>
        /// <param name="strDateEnd">Fecha final</param>
        /// <returns></returns>


        public static List<Entity.Dashboard.Prepaid.HistoricalTriationRFA> GetRecordTriationRFA(string strIdSession, string strTransaction, string strTelephone, string strDateStart, string strDateEnd)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("inMSISDN", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("P_FECHA_INI", DbType.String,255, ParameterDirection.Input, strDateStart),
                new DbParameter("P_FECHA_FIN", DbType.String,255, ParameterDirection.Input, strDateEnd),
                new DbParameter("ouCUR_HISTRIA", DbType.Object, 255, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String, 255, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.HistoricalTriationRFA>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_HIST_TRIACION_RFA_FILTROS, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el detalle del historial de triaciones de la línea.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <param name="strIdInteraction">Id de interacción</param>
        /// <returns></returns>

        public static List<Entity.Dashboard.Prepaid.NumbersTriation> GetDetailTriationRFA(string strIdSession, string strTransaction, string strTelephone, string strIdInteraction)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_phone", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("p_interact_id", DbType.String,255, ParameterDirection.Input, strIdInteraction),
                new DbParameter("ouCUR_HISTRIA", DbType.Object, 255, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String, 255, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.NumbersTriation>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_HIST_TRIACION_DETALLE, parameters);
        }

        /// <summary>
        /// Método que devuelve una lista con el historial de bonos.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <param name="strDateStart">Fecha de inicio</param>
        /// <param name="strDateEnd">Fecha de fin</param>
        /// <returns></returns>

        public static List<Entity.Dashboard.Prepaid.HistoricalBonds> GetHistoricalBonds(string strIdSession, string strTransaction, string strTelephone, string strDateStart, string strDateEnd)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("inMSISDN", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("P_FECHA_INI", DbType.String,255, ParameterDirection.Input, strDateStart),
                new DbParameter("P_FECHA_FIN", DbType.String,255, ParameterDirection.Input, strDateEnd),
                new DbParameter("ouCUR_HISBONO", DbType.Object, 255, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String, 255, ParameterDirection.Output)
            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.HistoricalBonds>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_HIST_BONOS, parameters);

        }

        /// <summary>
        /// Método que obtiene una lista con los datos Pin y Puk
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strKey">Key</param>
        /// <param name="strType">Tipo</param>
        /// <param name="strStarDate">Fecha de inicio</param>
        /// <param name="strEndDate">Fecha final</param>
        /// <returns></returns>

        public static List<Entity.Dashboard.Prepaid.PinPuk> GetListPinPuk(string strIdSession, string strTransaction, string strKey, string strType, string strStarDate, string strEndDate)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CLAVE", DbType.String, 20, ParameterDirection.Input, strKey),
                new DbParameter("P_TIPO", DbType.String, 20, ParameterDirection.Input, strType),
                new DbParameter("P_FECHA_INI", DbType.String, 20, ParameterDirection.Input, strStarDate),
                new DbParameter("P_FECHA_FIN", DbType.String, 20, ParameterDirection.Input, strEndDate),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)

            };

            return DbFactory.ExecuteReader<List<Entity.Dashboard.Prepaid.PinPuk>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TIM046_CONSULTA_PIN_PUK, parameters);
        }

        /// <summary>
        /// Método que obtiene datos de una línea prepago por número de teléfono.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns></returns>

        public static Entity.Dashboard.Prepaid.PEL GetPEL(string strIdSession, string strTransaction, string strTelephone)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TELEFONO", DbType.String,255, ParameterDirection.Input, strTelephone),
                new DbParameter("P_ICCID", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_ICCID_COD", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_IMEI", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_IMEI_COD", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_OFICINA", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_FECHA_ACT", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_PRODEDENCIA", DbType.String, 255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_SIXPROV, DbCommandConfiguration.SIACU_PROV_CONSULTA_SIAC, parameters);

            Entity.Dashboard.Prepaid.PEL objDataPEL = new Entity.Dashboard.Prepaid.PEL()
            {
                P_TELEFONO = Convert.ToInt(parameters[0].Value),
                P_ICCID = Convert.ToString(parameters[1].Value),
                P_ICCID_COD = Convert.ToString(parameters[2].Value),
                P_IMEI = Convert.ToString(parameters[3].Value),
                P_IMEI_COD = Convert.ToString(parameters[4].Value),
                P_OFICINA = Convert.ToString(parameters[5].Value),
                P_FECHA_ACT = Convert.ToString(parameters[6].Value),
                P_PROCEDENCIA = Convert.ToString(parameters[7].Value)
            };
            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("P_TELEFONO:{0},P_ICCID:{1}, P_ICCID_COD:{2}, P_IMEI:{3}, P_IMEI_COD:{4}, P_OFICINA:{5}, P_FECHA_ACT:{6}, P_PROCEDENCIA:{7}", objDataPEL.P_TELEFONO, objDataPEL.P_ICCID, objDataPEL.P_ICCID_COD, objDataPEL.P_IMEI, objDataPEL.P_IMEI_COD, objDataPEL.P_OFICINA, objDataPEL.P_FECHA_ACT, objDataPEL.P_PROCEDENCIA));
            return objDataPEL;
        }

        /// <summary>
        /// Método que obtiene los datos de una venta.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strApplicationID">Id de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strApplicationUser">Usuario de aplicación</param>
        /// <param name="strPhoneNumber">Número de téléfono</param>
        /// <param name="strMatter">Materail</param>
        /// <param name="strIssueSeries">Número de series</param>
        /// <returns></returns>

        public static Entity.Dashboard.Prepaid.TelephoneZsansData GetSalesData(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber, string strMatter, string strIssueSeries)
        {
            string strMessage = "";
            Entity.Dashboard.Prepaid.TelephoneZsansData objTelephoneZsansData = null;
            PREPAID_SIMCARD.nroSimcardsDataType[] oSimData = null;
            PREPAID_SIMCARD.itReturnType[] oReturnType;

            string strReturn = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, Configuration.WebServiceConfiguration.PREPAID_SIMCARD, () =>
            {
                return Configuration.WebServiceConfiguration.PREPAID_SIMCARD.obtenerDatosNroTelef(ref strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strPhoneNumber, strMatter, strIssueSeries, out strMessage, out oSimData, out oReturnType);
            });

            if (string.Equals(strReturn, Claro.Constants.NumberZeroString, StringComparison.OrdinalIgnoreCase))
            {
                if (oSimData != null && oSimData.Length > 0)
                {
                    foreach (var item in oSimData)
                    {
                        objTelephoneZsansData = new Entity.Dashboard.Prepaid.TelephoneZsansData()
                        {
                            MATNR = item.matNr,
                            SERNR = item.serNr,
                        };
                    }
                }
            }
            else
            {
                throw new MessageException(strMessage);
            }

            return objTelephoneZsansData;
        }

        /// <summary>
        /// Método que obtiene datos de una venta de línea prepago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <param name="strCodeMaterial">Material</param>
        /// <param name="strSeries">Número de series</param>
        /// <returns></returns>

        public static Entity.Dashboard.Prepaid.DataSalePEL GetSalesDataPEL(string strIdSession, string strTransaction, string strTelephone, string strCodeMaterial, string strSeries)
        {
            Entity.Dashboard.Prepaid.DataSalePEL objDataSalePEL;

            DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("P_TELEFONO", DbType.String,255, ParameterDirection.Input, strTelephone),
                    new DbParameter("P_COD_MATERIAL", DbType.String,255, ParameterDirection.Input,strCodeMaterial),
                    new DbParameter("P_SERIE",DbType.String,255,ParameterDirection.Input, strSeries),
                    new DbParameter("C_DATOS_VENTA", DbType.Object, 255, ParameterDirection.Output),
                    new DbParameter("ouCOD_ERR", DbType.String, 255, ParameterDirection.Output),
                    new DbParameter("ouMSG_ERR", DbType.String, 255, ParameterDirection.Output)
            };

            objDataSalePEL = DbFactory.ExecuteReader<Entity.Dashboard.Prepaid.DataSalePEL>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_DATOS_VENTA, parameters);
            return objDataSalePEL;

        }

        /// <summary>
        /// Método que obtiene datos de la deuda de un cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strBukrs"></param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns></returns>

        public static Entity.Dashboard.Prepaid.DebtSaleDues GetDebt(string strIdSession, string strTransaction, string strBukrs, string strTelephone)
        {
            Entity.Dashboard.Prepaid.DebtSaleDues objDebtSaleDues = null;
            Entity.Dashboard.Prepaid.PendingSaleDues objPendingSaleDues = null;
            List<Entity.Dashboard.Prepaid.PendingSaleDues> listPendingSaleDues = null;
            Entity.Dashboard.Prepaid.CanceledSaleDues objCanceledSaleDues = null;
            List<Entity.Dashboard.Prepaid.CanceledSaleDues> listCanceledSaleDues = null;
            DataSet dsData = SAPMethods.GetDebt(strBukrs, strTelephone);
            string strDate = "";

            if (dsData != null)
            {
                objDebtSaleDues = new Entity.Dashboard.Prepaid.DebtSaleDues();
                objDebtSaleDues.KUNNR = SAPMethods.GetKUNNR();
                objDebtSaleDues.Mwsbk = SAPMethods.GetMWSBK();
                objDebtSaleDues.Name = SAPMethods.GetNAME1();
                objDebtSaleDues.Netwr = SAPMethods.GetNETWR();
                objDebtSaleDues.Stcd1 = SAPMethods.GetSTCD1();
                objDebtSaleDues.Total = SAPMethods.GetTOTAL();


                if (dsData.Tables[0].Rows.Count > 0)
                {

                    listPendingSaleDues = new List<PendingSaleDues>();
                    listCanceledSaleDues = new List<CanceledSaleDues>();

                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        if (Convert.ToString(dr["Fecha_Pago"]) == Claro.Constants.FormatEightZero)
                        {
                            objPendingSaleDues = new Entity.Dashboard.Prepaid.PendingSaleDues();
                            objPendingSaleDues.BKTXT = Convert.ToString(dr["Bktxt"]);
                            strDate = Convert.ToString(dr["Fecha"]);
                            objPendingSaleDues.FECHA = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4);
                            objPendingSaleDues.MONTO = Convert.ToString(dr["Monto"]);
                            objPendingSaleDues.XBLNR = Convert.ToString(dr["Xblnr"]);
                            objPendingSaleDues.FECHAPAGO = "";
                            listPendingSaleDues.Add(objPendingSaleDues);
                        }
                        else
                        {
                            objCanceledSaleDues = new Entity.Dashboard.Prepaid.CanceledSaleDues();
                            strDate = Convert.ToString(dr["Fecha_Pago"]);
                            objCanceledSaleDues.BKTXT = Convert.ToString(dr["Bktxt"]);
                            objCanceledSaleDues.FECHA = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4);
                            objCanceledSaleDues.MONTO = Convert.ToString(dr["Monto"]);
                            objCanceledSaleDues.XBLNR = Convert.ToString(dr["Xblnr"]);
                            objCanceledSaleDues.FECHAPAGO = strDate.Substring(6, 2) + "/" + strDate.Substring(4, 2) + "/" + strDate.Substring(0, 4);
                            listCanceledSaleDues.Add(objCanceledSaleDues);
                        }
                    }
                    objDebtSaleDues.listPendingSaleDues = listPendingSaleDues;
                    objDebtSaleDues.listCanceledSaleDues = listCanceledSaleDues;
                }
            }
            else
            {
                objDebtSaleDues = new Entity.Dashboard.Prepaid.DebtSaleDues();
            }
            return objDebtSaleDues;
        }

        /// <summary>
        /// Método que obtiene lista de bonos TFI para líneas prepago. 
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strApplicationID">Id de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <param name="strNumberIn"></param>
        /// <returns></returns>

        public static List<Entity.Dashboard.Prepaid.Account> GetBondsTFIPrepaid(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strTelephone, string strNumberIn)
        {
            Entity.Dashboard.Prepaid.Account objAccount;
            List<Entity.Dashboard.Prepaid.Account> oListAccount = new List<Entity.Dashboard.Prepaid.Account>();
            string strbolsa51Ex = "";
            string strbolsa51Ba = "";
            string strBolCMACC1 = "";
            string strbolsa53Ba = "";
            string strbolsa53Ex = "";
            string strBolCMACC3 = "";

            try
            {
                strbolsa51Ex = KEY.AppSettings("strbolsa51Ex");
                strbolsa51Ba = KEY.AppSettings("strbolsa51Ba");
                strBolCMACC1 = KEY.AppSettings("strBolCMACC1");
                strbolsa53Ba = KEY.AppSettings("strbolsa53Ba");
                strbolsa53Ex = KEY.AppSettings("strbolsa53Ex");
                strBolCMACC3 = KEY.AppSettings("strBolCMACC3");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
            }

            string[] strTableParameter = new string[4];
            strTableParameter[0] = strbolsa51Ba;
            strTableParameter[1] = strbolsa51Ex;
            strTableParameter[2] = strbolsa53Ba;
            strTableParameter[3] = strbolsa53Ex;

            PREPAID_OPERATIONS.consultarLineaINResponse objAnswer = Claro.Web.Logging.ExecuteMethod<PREPAID_OPERATIONS.consultarLineaINResponse>(strIdSession, strTransactionID, Configuration.ServiceConfiguration.PREPAID_OPERATIONS, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_OPERATIONS.consultarLineaIN(new PREPAID_OPERATIONS.consultarLineaINRequest()
                {
                    idTransaccion = strTransactionID,
                    ipAplicacion = strApplicationID,
                    nombreAplicacion = strApplicationName,
                    msisdn = strTelephone,
                    @in = strNumberIn,
                    listaParametrosRequest = strTableParameter
                });
            });

            Int16 intsw = Claro.Constants.NumberZero;
            string strBalance = "";
            string strExpirationDate = "";
            string strName = "";

            if (objAnswer != null)
            {
                if (objAnswer.codigoRespuesta == Claro.Constants.NumberZeroString)
                {
                    foreach (PREPAID_OPERATIONS.ParametrosObjectType item in objAnswer.listaParametrosResponse)
                    {
                        if (item.parametro == strbolsa51Ex || item.parametro == strbolsa51Ba)
                        {
                            strBalance = (intsw == 0 ? item.valor : strBalance);
                            strExpirationDate = (intsw == 1 ? item.valor : strExpirationDate);
                            strName = strBolCMACC1;
                        }
                        if (item.parametro == strbolsa53Ba || item.parametro == strbolsa53Ex)
                        {
                            strBalance = (intsw == 0 ? item.valor : strBalance);
                            strExpirationDate = (intsw == 1 ? item.valor : strExpirationDate);
                            strName = strBolCMACC3;
                        }
                        intsw += 1;
                        if (intsw == 2)
                        {
                            intsw = Claro.Constants.NumberZero;
                            objAccount = new Entity.Dashboard.Prepaid.Account();
                            objAccount.Saldo = strBalance;
                            objAccount.FechaExpiracion = strExpirationDate;
                            objAccount.Nombre = strName;
                            oListAccount.Add(objAccount);
                            strBalance = "";
                            strExpirationDate = "";
                            strName = "";
                        }
                    }
                }
                else
                {
                    oListAccount = new List<Entity.Dashboard.Prepaid.Account>();
                    return oListAccount;
                }
            }

            return oListAccount;
        }

        /// <summary>
        /// Método que devuelve si se creó o no satisfactoriamente un contacto no usuario.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="objCustomer">ID Customer</param>
        /// <param name="strFlagUpdate"></param>
        /// <param name="strMessageText"></param>
        /// <returns></returns>

        public static bool CreateContactNotUSer(string strIdSession, string strTransaction, Customer objCustomer, out string strFlagUpdate, out string strMessageText)
        {
            bool isRegister = false;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PHONE", DbType.String,255, ParameterDirection.Input, objCustomer.TELEFONO),
                new DbParameter("P_USUARIO", DbType.String,255, ParameterDirection.Input, objCustomer.USUARIO),
                new DbParameter("P_NOMBRES", DbType.String,255, ParameterDirection.Input, objCustomer.NOMBRES),
                new DbParameter("P_APELLIDOS", DbType.String,255, ParameterDirection.Input, objCustomer.APELLIDOS),
                new DbParameter("P_TIPO_DOC", DbType.String,255, ParameterDirection.Input, objCustomer.TIPO_DOC),
                new DbParameter("P_NUM_DOC", DbType.String,255, ParameterDirection.Input, objCustomer.NRO_DOC),
                new DbParameter("P_SEXO", DbType.String,255, ParameterDirection.Input, objCustomer.SEXO),

                new DbParameter("P_FECHA_NAC", DbType.DateTime, ParameterDirection.Input, Convert.ToDate(objCustomer.FECHA_NAC)),
                new DbParameter("P_LUGAR_NAC", DbType.String,40, ParameterDirection.Input, objCustomer.LUGAR_NACIMIENTO_DES),

                new DbParameter("P_TELEFONO_REF", DbType.String,255, ParameterDirection.Input, objCustomer.TELEF_REFERENCIA),
                new DbParameter("P_ESTADO_CIVIL", DbType.String,255, ParameterDirection.Input, objCustomer.ESTADO_CIVIL),
                new DbParameter("P_FAX", DbType.String,255, ParameterDirection.Input, objCustomer.FAX),
                new DbParameter("P_MAIL", DbType.String,255, ParameterDirection.Input, objCustomer.EMAIL),
                new DbParameter("P_OCUPACION", DbType.String,255, ParameterDirection.Input, objCustomer.OCUPACION),
                new DbParameter("P_DOMICILIO", DbType.String,255, ParameterDirection.Input, objCustomer.DOMICILIO),
                new DbParameter("P_DISTRITO", DbType.String,255, ParameterDirection.Input, objCustomer.DISTRITO),
                new DbParameter("P_ZIPCODE", DbType.String,255, ParameterDirection.Input, objCustomer.ZIPCODE),
                new DbParameter("P_URBANIZACION", DbType.String,255, ParameterDirection.Input,objCustomer.URBANIZACION),
                new DbParameter("P_CIUDAD", DbType.String,255, ParameterDirection.Input, objCustomer.CIUDAD),
                new DbParameter("P_DEPARTAMENTO", DbType.String,255, ParameterDirection.Input,objCustomer.DEPARTAMENTO),
                new DbParameter("P_REFERENCIA", DbType.String,255, ParameterDirection.Input, objCustomer.REFERENCIA),
                new DbParameter("P_FLAG_MAIL", DbType.String,255, ParameterDirection.Input,objCustomer.FLAG_EMAIL),
                new DbParameter("P_FLAG_REGISTRADO", DbType.Int32, ParameterDirection.Input, objCustomer.FLAG_REGISTRADO),
                new DbParameter("P_MOTIVO_REGISTRO", DbType.String,255, ParameterDirection.Input,objCustomer.MOTIVOREGISTRO),
                new DbParameter("P_CARGO", DbType.String,255, ParameterDirection.Input, objCustomer.CARGO),
                new DbParameter("P_MODALIDAD", DbType.String,255, ParameterDirection.Input, objCustomer.MODALIDAD),

                new DbParameter("P_FLAG_INSERT", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CREATE_CONTACT_PRE_NOUSER, parameters);

            strFlagUpdate = parameters[26].Value.ToString();
            strMessageText = parameters[27].Value.ToString();

            if (!strFlagUpdate.Equals(Claro.SIACU.Constants.OK))
            {
                isRegister = false;
            }
            else
            {
                isRegister = true;
            }
            return isRegister;
        }

        /// <summary>
        /// Método de ValidateBlackList
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strPhone"></param>
        /// <param name="strDocumentType"></param>
        /// <param name="strDocumentNumber"></param>
        /// <returns></returns>
        public static string ValidateBlackList(string strIdSession, string strTransaction, string strPhone, string strDocumentType, string strDocumentNumber)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_nro_telefono", DbType.String,255, ParameterDirection.Input, strPhone),
                new DbParameter("p_tipo_doc", DbType.String,255, ParameterDirection.Input, strDocumentType),
                new DbParameter("p_nro_dni", DbType.String,255, ParameterDirection.Input, strDocumentNumber),
                new DbParameter("p_lista", DbType.Object, ParameterDirection.Output),
                new DbParameter("p_resultado", DbType.String,255, ParameterDirection.Output),
                new DbParameter("p_mensaje_error", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_CONSULTA_BL_TIT, parameters);

            return parameters[4].Value.ToString();
        }


        public static List<Entity.Dashboard.Prepaid.PackageODCS> GetConsumptionDataPacket(Claro.Entity.AuditRequest objAuditRequest, Entity.Dashboard.Prepaid.PackageODCS objPackageODCS)
        {

            PREPAID_PAQUETE.ConsultarPaquetesAdquiridosRequest objConsultarPaquetesAdquiridosRequest = new PREPAID_PAQUETE.ConsultarPaquetesAdquiridosRequest()
            {
                idTransaccion = objAuditRequest.Transaction,
                ipAplicacion = objAuditRequest.IPAddress,
                aplicacion = KEY.AppSettings("gConstAplicacionIDBroker"),
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

            PREPAID_PAQUETE.ConsultarPaquetesAdquiridosResponse objConsultarPaquetesAdquiridosResponse = Claro.Web.Logging.ExecuteMethod<PREPAID_PAQUETE.ConsultarPaquetesAdquiridosResponse>(objAuditRequest.Session, objAuditRequest.Transaction, Configuration.ServiceConfiguration.PREPAID_PAQUETE, () =>
            {
                return Configuration.ServiceConfiguration.PREPAID_PAQUETE.ConsultarPaquetesAdquiridos(objConsultarPaquetesAdquiridosRequest);
            });

            List<PackageODCS> objPackageODCSs = null;

            if (objConsultarPaquetesAdquiridosResponse.ListaPaquetesAdquiridos != null)
            {
                objPackageODCSs = new List<PackageODCS>();

                foreach (PREPAID_PAQUETE.PaqueteAdquiridoType item in objConsultarPaquetesAdquiridosResponse.ListaPaquetesAdquiridos)
                {
                    objPackageODCSs.Add(new PackageODCS()
                    {
                        ConvergedCode = item.codigoConvergente,
                        PackageCode = item.codigoPaquete,
                        DescriptionPackage = item.descripcion,
                        ActivationDate = item.fechaActivacion,
                        ExpirationDate = item.fechaExpiracion,
                        Channel = item.canal,
                        Price = item.precio,
                        State = item.estado,
                        MBConsumed = item.MBComsumidos,
                        MBAvailable = item.MBDisponibles,
                        MBTotal = item.MBTotales,
                        RatingGroup = item.RatingGroup,
                        Zone = item.zona,
                        TypePurchase = item.tipoCompra
                    });
                }
            }

            return objPackageODCSs;
        }

        /////////////////TARIFA
        public static RateStateResponse GetRateState(RateStateRequest objRequest)
        {
            RateStateResponse objRateStateResponse = new RateStateResponse();
            try
            {
                PREPAID_BSSOPERATIONS.HeaderRequestType headerRequest = new PREPAID_BSSOPERATIONS.HeaderRequestType()
                {

                    canal = Claro.Constants.ApplicationPrepaid,
                    fechaInicio = System.DateTime.Now,
                    idAplicacion = objRequest.Audit.ApplicationName,
                    idTransaccionESB = objRequest.Audit.Transaction,
                    idTransaccionNegocio = objRequest.Audit.Transaction,
                    nodoAdicional = "",
                    usuarioAplicacion = objRequest.Audit.UserName,
                    usuarioSesion = objRequest.Audit.UserName
                };
                PREPAID_BSSOPERATIONS.HeaderRequestType D = new PREPAID_BSSOPERATIONS.HeaderRequestType();

                PREPAID_BSSOPERATIONS.consultarTarifaGranelRequest request = new PREPAID_BSSOPERATIONS.consultarTarifaGranelRequest()
                {
                    headerRequest = new PREPAID_BSSOPERATIONS.HeaderRequestType()
                    {
                        canal = Claro.Constants.ApplicationPrepaid,
                        fechaInicio = System.DateTime.Now,
                        idAplicacion = objRequest.Audit.ApplicationName,
                        idTransaccionESB = objRequest.Audit.Transaction,
                        idTransaccionNegocio = objRequest.Audit.Transaction,
                        nodoAdicional = "",
                        usuarioAplicacion = objRequest.Audit.UserName,
                        usuarioSesion = objRequest.Audit.UserName,
                    },
                    listaRequestOpcional = new PREPAID_BSSOPERATIONS.ListaRequestOpcionalTypeListaRequestOpcional[0],
                    msisdn = objRequest.strPhoneNumber.Length == 9 ? "51" + objRequest.strPhoneNumber : objRequest.strPhoneNumber,
                    offer = "",
                    subscriberstatus = "",
                    subscriptionString = ""
                };
                PREPAID_BSSOPERATIONS.consultarTarifaGranelResponse response = new PREPAID_BSSOPERATIONS.consultarTarifaGranelResponse();
                PREPAID_BSSOPERATIONS.OperacionesINUPortTypeClient objClient = new PREPAID_BSSOPERATIONS.OperacionesINUPortTypeClient();
                var X = objClient.consultarTarifaGranel(headerRequest, request, out response);

                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Datos enviados: msisdn:{0}, idTransaccion:{1}, ipAplicacion:{2}, nombreAplicacion:{3},usuarioAplicacion :{4}", objRequest.strPhoneNumber, objRequest.Audit.Transaction, objRequest.Audit.IPAddress, objRequest.Audit.ApplicationName, objRequest.Audit.UserName));
                if (response != null)
                {
                    if (response.auditResponse != null)
                    {
                        if (response.auditResponse.errorCode != null && response.auditResponse.errorCode.Equals("0"))
                        {
                            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Datos de Salida: codigoRespuesta:{0}", response.auditResponse.errorCode));
                            objRateStateResponse.codRespuesta = response.auditResponse.errorCode;
                            objRateStateResponse.msjRespuesta = response.auditResponse.errorMsg;
                            objRateStateResponse.estado = response.estadoActual;
                        }
                        else
                        {
                            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Datos de Salida: codigoRespuesta:{0}", response.auditResponse.errorCode));
                            objRateStateResponse.codRespuesta = response.auditResponse.errorCode;
                            objRateStateResponse.msjRespuesta = response.auditResponse.errorMsg;
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Diferente audit, editar el reference"));

                    }
                }
                else
                {
                    objRateStateResponse.estado = "-";
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Diferentes response"));

                }
            }
            catch (Exception exe)
            {
                string men = exe.Message;
                objRateStateResponse.codRespuesta = "-1";
                objRateStateResponse.msjRespuesta = men;
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, exe.Message);
                objRateStateResponse.estado = "";
                return objRateStateResponse;
            }
            return objRateStateResponse;
        }
        //////////////////

    }
}