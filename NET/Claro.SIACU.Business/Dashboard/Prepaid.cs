using Claro.SIACU.Entity.Dashboard.Prepaid;
using Claro.SIACU.Entity.Dashboard.Prepaid.GetRateState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HELPER_CREATEUSER = Claro.SIACU.Entity.Dashboard.Prepaid.CreateContactNotUSer;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Business.Dashboard
{
    public static class Prepaid
    {
        /// <summary>
        /// Método para obtener los datos del servicio prepago.
        /// </summary>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve objeto ServiceResponse con información del servicio prepago.</returns>
        public static Entity.Dashboard.Prepaid.GetService.ServiceResponse GetService(Entity.Dashboard.Prepaid.GetService.ServiceRequest objGetServiceRequest)
        {
            string strContactType = "", strModality = "", strSystem = Claro.Constants.NumberOneString, strDescriptionErrorHLR = "";
            string strProviderPrepaid = "", strModalidadTFI = "";
            string strModalidadPrepagoDBPre = "", strModalidadDBPreNoDataFound = "", strConstEstadoLineaBloqueado = "", strZIPCodePeru = "";
            string strEstadoWSMetodoHLR = "", strFlagConsultaHLR = "", strMsjeError4G = "", strNumeracionDOS = "", strNumeracionCUATRO = "";
            string strCodeZip = "", strSegment = "", strDescriptionTypeTriation = "", strDescriptionPlan = "";
            string strStateDays = "", strStateSubscriber = "", srtAuxCNTNumber = "", strActionId = "", strFieldName = "", responsePortability = "";
            string strMsjeErrorSoporteSim = "", strMsjeErrorTechVo = "", strMsjSimNoSoporta = "", strMsjeErrorWSSoporteSim = "";
            int intTypeTriacion;
            string[] strTextStateDays;
            Entity.Dashboard.Prepaid.ReasonBlocking oReasonBlocking;
            Entity.Dashboard.Prepaid.BankingMobile oBankingMobile;
            List<Entity.Dashboard.HLR> oListHLR;
            Entity.Dashboard.Prepaid.GetService.ServiceResponse objGetServiceResponse;
            int varSoporteTech = -1, varActiveTech = -1;
            bool boolActSrv = false;
            string strActionIdIServiceVoLTE = "", strActionIdIServiceVoWifi = "";
            try
            {
                strProviderPrepaid = KEY.AppSettings("strProviderPrepago");
                strModalidadTFI = KEY.AppSettings("strModalidadTFI");
                strModalidadPrepagoDBPre = KEY.AppSettings("strModalidadPrepagoDBPre");
                strModalidadDBPreNoDataFound = KEY.AppSettings("strModalidadDBPreNoDataFound");
                strConstEstadoLineaBloqueado = KEY.AppSettings("gConstEstadoLineaBloqueado");
                strZIPCodePeru = KEY.AppSettings("strZIPCodePeru");
                strEstadoWSMetodoHLR = KEY.AppSettings("strEstadoWSMetodoHLR");
                strFlagConsultaHLR = KEY.AppSettings("strFlagConsultaHLR");
                strMsjeError4G = KEY.AppSettings("strMsjeError4G");
                strNumeracionDOS = KEY.AppSettings("strNumeracionDOS");
                strNumeracionCUATRO = KEY.AppSettings("strNumeracionCUATRO");
                strActionId = KEY.AppSettings("strIdAccionConsultaUDB");
                strFieldName = KEY.AppSettings("strParamMSISDNConectorUDB");
                strMsjeErrorSoporteSim = KEY.AppSettings("strMsjeErrorSoporteSim");
                strMsjeErrorTechVo = KEY.AppSettings("strMsjeErrorTechVo");
                strMsjSimNoSoporta = KEY.AppSettings("strMsjSimNoSoporta");
                strActionIdIServiceVoLTE = KEY.AppSettings("strIdAccionConsultaServicioVoLTE");
                strActionIdIServiceVoWifi = KEY.AppSettings("strIdAccionConsultaServicioVoWIFI");
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
            }


            if (objGetServiceRequest.objService != null)
            {
                if (objGetServiceRequest.objService.EsTFI.Equals(true))
                {
                    strModality = strModalidadTFI;
                    objGetServiceRequest.objService.Pago = Claro.SIACU.Constants.PaymentPending;
                    strContactType = Claro.Constants.LetterT;
                }
                else
                {
                    strModality = (strProviderPrepaid.IndexOf(objGetServiceRequest.objService.ProviderID) > 0) ? strModalidadPrepagoDBPre : Claro.Constants.LetterX;
                    objGetServiceRequest.objService.Pago = "";
                    if (strProviderPrepaid.IndexOf(objGetServiceRequest.objService.ProviderID) > 0 && !string.IsNullOrEmpty(objGetServiceRequest.objService.ProviderID))
                    {
                        strContactType = Claro.Constants.LetterP;
                    }
                    else if (string.Equals(strModality, strModalidadDBPreNoDataFound, StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(strModality, Claro.Constants.LetterX, StringComparison.OrdinalIgnoreCase))
                    {
                        strContactType = "";
                    }
                    else
                    {
                        strContactType = (objGetServiceRequest.objService.NroCelular.Length == 8) ? Claro.Constants.LetterT : Claro.Constants.LetterP;
                    }
                }

                while (objGetServiceRequest.objService.SubscriberStatus.Length < 10)
                {
                    objGetServiceRequest.objService.SubscriberStatus = objGetServiceRequest.objService.SubscriberStatus + Claro.Constants.NumberZeroString;
                }


                if (!string.IsNullOrEmpty(objGetServiceRequest.objService.StatusIMSI))
                {
                    if (string.Equals(objGetServiceRequest.objService.StatusIMSI.ToUpper(), Claro.SIACU.Constants.True, StringComparison.OrdinalIgnoreCase))
                    {
                        objGetServiceRequest.objService.StatusIMSI = Claro.SIACU.Constants.Locked;
                    }
                    else
                    {
                        objGetServiceRequest.objService.StatusIMSI = (string.Equals(objGetServiceRequest.objService.StatusIMSI.ToUpper(), Claro.SIACU.Constants.False, StringComparison.OrdinalIgnoreCase)) ? Claro.SIACU.Constants.Unlocked : "";
                    }
                }
                else
                {
                    objGetServiceRequest.objService.StatusIMSI = "";
                }

                if (objGetServiceRequest.objService.NroCelular.StartsWith(Claro.Constants.NumberTwoString))
                {
                    objGetServiceRequest.objService.NroCelular = objGetServiceRequest.objService.NroCelular.Remove(0, Claro.Constants.NumberOne);
                }

                try
                {
                  
                    oBankingMobile = GetMobileBankingSAP(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, objGetServiceRequest.ApplicationID, objGetServiceRequest.ApplicationName, objGetServiceRequest.Audit.UserName, objGetServiceRequest.objService.NroCelular, objGetServiceRequest.Matter, objGetServiceRequest.IssueSeries);

                }
                catch (Exception ex)
                {
                    oBankingMobile = null;
                    Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                }

                if (oBankingMobile != null)
                {
                    objGetServiceRequest.objService.ICCID = oBankingMobile.ICCID;
                    objGetServiceRequest.objService.BancaMovil = oBankingMobile.BancaMovil;
                }

                if (!string.IsNullOrEmpty(objGetServiceRequest.objService.StatusIMSI) &&
                    string.Equals(objGetServiceRequest.objService.StatusIMSI, strConstEstadoLineaBloqueado, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        oReasonBlocking = GetReasonLineBiometrics(objGetServiceRequest.Audit, objGetServiceRequest.objService.NroCelular);
                    }
                    catch (Exception ex)
                    {
                        oReasonBlocking = null;
                        Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                    }

                    if (oReasonBlocking != null)
                    {
                        objGetServiceRequest.objService.MotivoBloqueo = oReasonBlocking.MotivoBloqueo;
                        objGetServiceRequest.objService.AlertaBloqueo = oReasonBlocking.AlertaBloqueo;
                    }
                }

                if (!string.IsNullOrEmpty(objGetServiceRequest.objService.TipoTriacion))
                {
                    intTypeTriacion = System.Convert.ToInt32(objGetServiceRequest.objService.TipoTriacion);
                    try
                    {
                        strDescriptionTypeTriation = GetListTriation(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, strSystem, intTypeTriacion);
                    }
                    catch (Exception ex)
                    {
                        strDescriptionTypeTriation = String.Empty;
                        Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                    }

                    if (!string.IsNullOrEmpty(strDescriptionTypeTriation))
                    {
                        objGetServiceRequest.objService.DescriptionTypeTriation = strDescriptionTypeTriation;
                    }
                }

                if (objGetServiceRequest.objService.oPortability != null)
                {
                    objGetServiceRequest.objService.listPortability = GetPortability(objGetServiceRequest.Audit, objGetServiceRequest.objService.NroCelular, ref responsePortability);
                    objGetServiceRequest.objService.oPortability.RESPUESTA = responsePortability;
                    objGetServiceRequest.objService.Respuesta = responsePortability;
                }
               

                strCodeZip = strZIPCodePeru;
                try
                {
                    strSegment = GetSegment(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, strCodeZip + objGetServiceRequest.objService.NroCelular);
                }
                catch (Exception ex)
                {
                    strSegment = "";
                    Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                }

                objGetServiceRequest.objService.Segmento = (!string.IsNullOrEmpty(strSegment)) ? strSegment : Claro.SIACU.Constants.Any;
                try
                {
                    strDescriptionPlan = GetDescriptionPlan(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, objGetServiceRequest.objService.ProviderID, objGetServiceRequest.objService.PlanTarifario, objGetServiceRequest.objService.SubscriberStatus);
                }
                catch (Exception ex)
                {
                    strDescriptionPlan = "";
                    Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                }

                if (!string.IsNullOrEmpty(strDescriptionPlan) && !strDescriptionPlan.Equals(Claro.SIACU.Constants.Null, StringComparison.OrdinalIgnoreCase))
                {
                    objGetServiceRequest.objService.DescripcionPlan = strDescriptionPlan;
                }

                objGetServiceRequest.objService.EstadoLinea = Helper.GetValueXML(objGetServiceRequest.objService.StatusLinea.Trim(), Claro.SIACU.Constants.ListStatusLine);

                if (string.Equals(strEstadoWSMetodoHLR, Claro.Constants.NumberOneString, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(strFlagConsultaHLR, Claro.Constants.NumberOneString, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        oListHLR = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.HLR>>(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, () =>
                        { return Data.Dashboard.Common.GetHLRUDB(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, objGetServiceRequest.Audit.ApplicationName, objGetServiceRequest.Audit.IPAddress, objGetServiceRequest.Audit.UserName, objGetServiceRequest.objService.NroCelular, strActionId, strFieldName, String.Empty, out strDescriptionErrorHLR); });
                    }
                    catch (Exception ex)
                    {
                        oListHLR = null;
                        Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                    }

                    if (oListHLR != null && oListHLR.Count > 0)
                    {
                        objGetServiceRequest.objService.Tecnologia4G = (string.Equals(strDescriptionErrorHLR, Claro.SIACU.Constants.ZeroNumber, StringComparison.OrdinalIgnoreCase) ? Common.GetServicesState4G(objGetServiceRequest.Audit, oListHLR, strDescriptionErrorHLR) : strMsjeError4G);
                    }
                    else
                    {
                        objGetServiceRequest.objService.Tecnologia4G = strMsjeError4G;
                    }
                }

                // PROY-31249
                try
                {
                    if (!string.IsNullOrEmpty(objGetServiceRequest.objService.ICCID))
                    {
                        varSoporteTech = Common.ConsultarTecVoLTE(objGetServiceRequest.Audit, objGetServiceRequest.objService.ICCID, out strMsjeErrorWSSoporteSim);
                    }
                }
                catch (Exception ex)
                {
                    varSoporteTech = -3;
                    Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                }
                
                if (varSoporteTech == 0)
                {
                    try
                    {

                        varActiveTech = Common.ConsultarAprovisionamientoVoLTEoWifi(objGetServiceRequest.Audit, objGetServiceRequest.objService.NroCelular, strActionIdIServiceVoLTE, out boolActSrv);
                        if (varActiveTech == 0)
                        {
                            objGetServiceRequest.objService.TechnologyVoLTE = boolActSrv ? Constants.Active : Constants.Deactivated;
                        }
                        else{
                            objGetServiceRequest.objService.TechnologyVoLTE = strMsjeErrorTechVo;
                        }

                        varActiveTech = Common.ConsultarAprovisionamientoVoLTEoWifi(objGetServiceRequest.Audit, objGetServiceRequest.objService.NroCelular, strActionIdIServiceVoWifi, out boolActSrv);
                        if (varActiveTech == 0)
                        {
                            objGetServiceRequest.objService.TechnologyVoWifi = boolActSrv ? Constants.Active : Constants.Deactivated;
                        }
                        else
                        {
                            objGetServiceRequest.objService.TechnologyVoWifi = strMsjeErrorTechVo;
                        }
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                        objGetServiceRequest.objService.TechnologyVoLTE = strMsjeErrorTechVo;
                        objGetServiceRequest.objService.TechnologyVoWifi = strMsjeErrorTechVo;
                    }
                }
                else
                {
                    if (varSoporteTech == 1)
                    {
                        objGetServiceRequest.objService.TechnologyVoLTE = strMsjSimNoSoporta;
                        objGetServiceRequest.objService.TechnologyVoWifi = strMsjSimNoSoporta;
                    }
                    else
                    {
                        objGetServiceRequest.objService.TechnologyVoLTE = strMsjeErrorSoporteSim;
                        objGetServiceRequest.objService.TechnologyVoWifi = strMsjeErrorSoporteSim;
                    }
                }

                if (string.Equals(objGetServiceRequest.objService.StatusLinea, strNumeracionDOS, StringComparison.OrdinalIgnoreCase) || string.Equals(objGetServiceRequest.objService.StatusLinea, strNumeracionCUATRO, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        strStateSubscriber = GetStateSubscriber(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, System.Convert.ToInt32(objGetServiceRequest.objService.NroCelular));
                    }
                    catch (Exception ex)
                    {
                        strStateSubscriber = "";
                        Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                    }

                    if (!string.IsNullOrEmpty(strStateSubscriber))
                    {
                        strStateDays = strStateSubscriber;
                        strTextStateDays = strStateDays.Split('|');

                        if (strTextStateDays.Length == 2)
                        {
                            objGetServiceRequest.objService.EstadoSubscriber = "(" + strTextStateDays[0] + ")";
                            objGetServiceRequest.objService.DiasEstado = strTextStateDays[Claro.Constants.NumberOne];
                        }
                    }
                }

                if (!string.IsNullOrEmpty(objGetServiceRequest.objService.CNTNumber))
                {
                    srtAuxCNTNumber = Convert.ToString(objGetServiceRequest.objService.CNTNumber);
                    if (srtAuxCNTNumber.Equals(Claro.Constants.NumberZeroString)) { srtAuxCNTNumber = ""; }
                }

                objGetServiceRequest.objService.CNTNumber = srtAuxCNTNumber;

                objGetServiceResponse = new Entity.Dashboard.Prepaid.GetService.ServiceResponse()
                {
                    objService = objGetServiceRequest.objService,

                };

                if (string.Equals(strContactType, Claro.Constants.LetterP, StringComparison.OrdinalIgnoreCase) || string.Equals(strContactType, Claro.Constants.LetterT, StringComparison.OrdinalIgnoreCase) || string.Equals(strContactType, Claro.Constants.LetterF, StringComparison.OrdinalIgnoreCase))
                {
                    objGetServiceResponse.objService.TipoContacto = strContactType;
                }
                else
                {
                    objGetServiceResponse = null;
                }



                try
                {
                    string outMessage = "";
                    string validateBondRFA = "";
                    objGetServiceRequest.objService.BondRFA = "N";
                    string nroLine = "";
                    string amountbond = "";
                    objGetServiceRequest.objService.BondAmountRFA = "";

                    validateBondRFA = Claro.Web.Logging.ExecuteMethod<string>(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Prepaid.ValidateBond(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, objGetServiceRequest.objService.SubscriberStatus, out outMessage);
                    });

                    if (validateBondRFA == "0")
                    {
                        objGetServiceRequest.objService.BondRFA = "S";
                        nroLine = objGetServiceRequest.objService.NroCelular;
                        validateBondRFA = Data.Dashboard.Prepaid.GetAmountBond(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, outMessage);

                        if (validateBondRFA == "0")
                        {
                            objGetServiceRequest.objService.BondAmountRFA = amountbond;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objGetServiceRequest.Audit.Session, objGetServiceRequest.Audit.Transaction, ex.Message);
                }


            }
            else
            {
                objGetServiceResponse = null;
            }

            return objGetServiceResponse;
        }

        /// <summary>
        /// Método para obtener información de portabilidad.
        /// </summary>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns>Devuelve objeto Portability con información de portabilidad.</returns>
        private static List<Entity.Dashboard.Portability> GetPortability(Claro.Entity.AuditRequest objAudit, string strTelephone, ref string strResponse)
        {
            List<Entity.Dashboard.Portability> objPortability = null;
            try
            {
                objPortability = Data.Dashboard.Common.GetPortability(objAudit.Session, objAudit.Transaction, strTelephone, out strResponse);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }
            return objPortability;
        }

        /// <summary>
        /// Método para obtener información de Banca Movil SAP
        /// </summary>
        /// <param name="strTransactionID"> ID de la Transacción</param>
        /// <param name="strApplicationIP">IP de la Aplicación</param>
        /// <param name="strApplicationName">Nonbre de la Aplicación</param>
        /// <param name="strApplicationUser">Usuario de la Aplicación</param>
        /// <param name="strPhoneNumber">Numero de Teléfono</param>
        /// <param name="strMatter">Materia</param>
        /// <param name="strIssueSeries">Número de Serie</param>
        /// <returns>Devuelve objeto BankingMobile con información de banca movil SAP.</returns>
        private static Entity.Dashboard.Prepaid.BankingMobile GetMobileBankingSAP(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber, string strMatter, string strIssueSeries)
        {
            Entity.Dashboard.Prepaid.BankingMobile oBankingMobile = null;


            string strICCID = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
            {
                return Data.Dashboard.Prepaid.GetMobileBankingSAP(strIdSession, strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strPhoneNumber, strMatter, strIssueSeries).Trim();
            });
            if (!string.IsNullOrEmpty(strICCID))
            {
                oBankingMobile = new Entity.Dashboard.Prepaid.BankingMobile();
                oBankingMobile.ICCID = strICCID;

                if (strICCID.Substring(8, Claro.Constants.NumberOne) == Claro.Constants.NumberTwoString && (strICCID.Substring(9, Claro.Constants.NumberOne) == Claro.Constants.NumberZeroString || strICCID.Substring(9, Claro.Constants.NumberOne) == Claro.Constants.NumberTwoString))
                {
                    oBankingMobile.BancaMovil = Claro.SIACU.Constants.Yes;
                }
                else
                {
                    oBankingMobile.BancaMovil = Claro.SIACU.Constants.Not;
                }

            }
            return oBankingMobile;

        }

        /// <summary>
        /// Método para confirmar si es bloqueo por biometría.
        /// </summary>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <returns>Devuelve objeto ReasonBlocking con información de bloqueo por biometría.</returns>
        private static Entity.Dashboard.Prepaid.ReasonBlocking GetReasonLineBiometrics(Claro.Entity.AuditRequest objAuditRequest, string strTelephone)
        {
            Entity.Dashboard.Prepaid.ReasonBlocking oReasonBlocking = null;
            string[] arrTextReasonLatestBlocking;
            string strTextReasonLatestBlocking = string.Empty;
            string strConstCodigoBloqueoNoBio = "", strConstMotivoBloqueoNoBio = "", strMsgErrBloqxNoBiometria = "";
            bool boolResponse = Claro.Web.Logging.ExecuteMethod<bool>(objAuditRequest.Session, objAuditRequest.Transaction, () =>
            {
                return Data.Dashboard.Prepaid.GetReasonLineBiometrics(objAuditRequest.Session, objAuditRequest.Transaction, strTelephone);
            });

            arrTextReasonLatestBlocking = strTextReasonLatestBlocking.Split('|');
            string strCodeBlocking = arrTextReasonLatestBlocking[0];

            try
            {
                strConstCodigoBloqueoNoBio = KEY.AppSettings("gConstCodigoBloqueoNoBio");
                strConstMotivoBloqueoNoBio = KEY.AppSettings("gConstMotivoBloqueoNoBio");
                strMsgErrBloqxNoBiometria = KEY.AppSettings("strMsgErrBloqxNoBiometria");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }

            if (boolResponse && string.Equals(strCodeBlocking, strConstCodigoBloqueoNoBio, StringComparison.OrdinalIgnoreCase))
            {
                oReasonBlocking = new Entity.Dashboard.Prepaid.ReasonBlocking();
                oReasonBlocking.MotivoBloqueo = strConstMotivoBloqueoNoBio;
                oReasonBlocking.AlertaBloqueo = strMsgErrBloqxNoBiometria;
            }
            return oReasonBlocking;
        }

        /// <summary>
        /// Método para obtener información del segmento.
        /// </summary>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve cadena con descripción del segmento.</returns>
        private static string GetSegment(string strIdSession, string strTransaction, string strPhoneNumber)
        {
            string strDescripcion = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransaction, () =>
            {
                return Data.Dashboard.Prepaid.GetSegment(strIdSession, strTransaction, strPhoneNumber).Trim();
            });
            if (string.IsNullOrEmpty(strDescripcion))
            {
                strDescripcion = Claro.SIACU.Constants.Any;
            }
            return strDescripcion;
        }

        /// <summary>
        /// Método para obtener el listado de tipo de trios.
        /// </summary>
        /// <param name="strSystem">Sistema</param>
        /// <param name="intTypeTriationId">Id de tipo de triación </param>
        /// <returns>Devuelve cadena con la descripción del tipo de triación.</returns>
        private static string GetListTriation(string strIdSession, string strTransaction, string strSystem, int intTypeTriationId)
        {
            string DescriptionTypeTriation = "";
            List<Entity.Dashboard.Prepaid.TypeTriation> oListTypeTriation = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.TypeTriation>>(strIdSession, strTransaction, () =>
            {
                return Data.Dashboard.Prepaid.GetListTriation(strIdSession, strTransaction, strSystem);
            });


            if (oListTypeTriation.Count > 0)
            {
                foreach (TypeTriation item in oListTypeTriation)
                {
                    if (item.Codigo == intTypeTriationId)
                    {
                        DescriptionTypeTriation = item.Descripcion;
                        break;
                    }
                }
            }
            return DescriptionTypeTriation;
        }

        /// <summary>
        /// Método para obtener la descripción del plan.
        /// </summary>
        /// <param name="strProviderID">Id de proveedor</param>
        /// <param name="strPlanTariff">Plan tarifario</param>
        /// <param name="strSubscriberStatus">Estado de Suscripción</param>
        /// <returns>Devuelve cadena con descripción del plan</returns>
        private static string GetDescriptionPlan(string strIdSession, string strTransaction, string strProviderID, string strPlanTariff, string strSubscriberStatus)
        {
            return Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransaction, () =>
            {
                return Data.Dashboard.Prepaid.GetDescriptionPlan(strIdSession, strTransaction, strProviderID, strPlanTariff, strSubscriberStatus).Trim();
            });
        }

        /// <summary>
        /// Método para obtener el estado del subscriber llamando al ods.
        /// </summary>
        /// <param name="intPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve cadena con los días de expiración.</returns>
        private static string GetStateSubscriber(string strIdSession, string strTransaction, int intPhoneNumber)
        {
            return Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransaction, () =>
            {
                return Data.Dashboard.Prepaid.GetStateSubscriber(strIdSession, strTransaction, intPhoneNumber).Trim();
            });
        }

        /// <summary>
        /// Método para obtener los mensajes de validación para los clientes prepago.
        /// </summary>
        /// <param name="strTelephone">Número de teléfono</param>
        /// <param name="isTFI">Indica si el número es TFI</param>
        /// <param name="strProviderID">Id de proveedor</param>
        /// <returns>Devuelve la validación del teléfono.</returns>
        public static Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse GetValidateTelephone(Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneRequest objValidateTelephoneRequest)
        {
            string strResponse = "", strModality = "", strCustomerCodeResponse = "", strFlagCustomerInactive = "", strShowPopup = Claro.Constants.NumberZeroString, strShowData = Claro.Constants.NumberOneString;
            string strModalidadTFI = "", strModalidadPrepagoDBPre = "", strModalidadDBPreNoDataFound = "";
            string strMensajeTelefonoNoExiste = "", strMensajeClieActControl = "", strMensajeClieActNoTFI = "", strMensajeClieActNoPrepago = "";
            string strProviderPrepaid = "";
            try
            {
                strProviderPrepaid = KEY.AppSettings("strProviderPrepago");
                strModalidadTFI = KEY.AppSettings("strModalidadTFI");
                strModalidadPrepagoDBPre = KEY.AppSettings("strModalidadPrepagoDBPre");
                strModalidadDBPreNoDataFound = KEY.AppSettings("strModalidadDBPreNoDataFound");
                strMensajeTelefonoNoExiste = KEY.AppSettings("strMensajeTelefonoNoExiste");
                strMensajeClieActControl = KEY.AppSettings("strMensajeClieActControl");
                strMensajeClieActNoTFI = KEY.AppSettings("strMensajeClieActNoTFI");
                strMensajeClieActNoPrepago = KEY.AppSettings("strMensajeClieActNoPrepago");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objValidateTelephoneRequest.Audit.Session, objValidateTelephoneRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse objValidateTelephoneResponse;


            if (string.IsNullOrEmpty(objValidateTelephoneRequest.CustomerCode))
            {
                strFlagCustomerInactive = Claro.SIACU.Constants.Not;
            }

            if (objValidateTelephoneRequest.CodigoResponse == "")
            {
                if (objValidateTelephoneRequest.TFI.Equals(true))
                {
                    strModality = strModalidadTFI;
                }
                else
                {
                    strModality = (strProviderPrepaid.IndexOf(objValidateTelephoneRequest.ProviderID) > 0 || objValidateTelephoneRequest.ProviderID=="") ? strModalidadPrepagoDBPre : Claro.Constants.LetterX;
                }
            }
            else
            {
                strModality = "";
            }

            if (objValidateTelephoneRequest.TFI.Equals(true))
            {
                strResponse = "";
            }
            else
            {
                if (!string.IsNullOrEmpty(objValidateTelephoneRequest.ProviderID) && (strProviderPrepaid.IndexOf(objValidateTelephoneRequest.ProviderID) > 0))
                {
                    strResponse = "";
                    strCustomerCodeResponse = objValidateTelephoneRequest.CustomerCode.Trim();
                }
                else if (strModality.Equals(strModalidadDBPreNoDataFound))
                {
                    strResponse = strMensajeTelefonoNoExiste;
                    strCustomerCodeResponse = "";
                }
                else if (strModality.Equals(Claro.Constants.LetterX))
                {
                    strCustomerCodeResponse = "";
                    if (objValidateTelephoneRequest.Telephone.Trim().Length == 8)
                    {
                        strResponse = strMensajeTelefonoNoExiste;
                    }
                    else
                    {
                        strResponse = strMensajeClieActControl;
                        strShowData = Claro.Constants.NumberZeroString;
                    }
                }
                else
                {
                    strCustomerCodeResponse = objValidateTelephoneRequest.CustomerCode.Trim();
                    strResponse = (objValidateTelephoneRequest.Telephone.Trim().Length == 8) ? strMensajeClieActNoTFI : strMensajeClieActNoPrepago;
                    if (!string.IsNullOrEmpty(strCustomerCodeResponse)) strFlagCustomerInactive = Claro.SIACU.Constants.Yes;
                }
            }

            if (string.IsNullOrEmpty(strCustomerCodeResponse))
            {
                strShowPopup = (strFlagCustomerInactive.Equals(Claro.SIACU.Constants.Not)) ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
            }
            else
            {
                strShowPopup = (strFlagCustomerInactive.Equals(Claro.SIACU.Constants.Yes)) ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
            }

            objValidateTelephoneResponse = new Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse();
            objValidateTelephoneResponse.Cadena = strResponse + "|" + strShowPopup + "|" + strShowData;

            return objValidateTelephoneResponse;
        }

        /// <summary>
        /// Método para obtener los datos del cliente.
        /// </summary>
        /// <param name="strTelephone">Teléfono del cliente</param>
        /// <param name="strAccount">Número de Cuenta</param>
        /// <param name="strContactId">Id del cliente</param>
        /// <param name="strFlagRegistry">Flag de Registro</param>
        /// <param name="strIdTransaction">Id de Trasacción</param>
        /// <param name="strIpApplication">Ip de Aplicación</param>
        /// <param name="strApplicationName">Nombre de la aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <returns>Devuelve objeto PreviousCustomerResponse con información del cliente</returns>
        public static Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse GetPreviousCustomer(Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objPreviousCustomerRequest)
        {
            Entity.Dashboard.Prepaid.Customer oCustomer;
            Entity.Dashboard.Prepaid.Service oService;
            List<Entity.ListItem> listDocumentType = null;
            string strDocumentTypeCode = "", strDocumentList = "";
            string strTelephoneTypePre = "", strPortIN = "", strPortOUT = "", strDocumentType = "", strDocumentLong = "";
            string strCaracterRellenoNroDoc = "", strSegment = "", strCodeResponse = "", strInteraction = "", strContingencyClarify = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                strTelephoneTypePre = KEY.AppSettings("strTipoTelefonoPrepago");
                strPortIN = KEY.AppSettings("PortabilidadPortIN");
                strPortOUT = KEY.AppSettings("PortabilidadPortOUT");
                strDocumentType = KEY.AppSettings("strDocumentType");
                strDocumentLong = KEY.AppSettings("strDocumentLong");
                strCaracterRellenoNroDoc = KEY.AppSettings("strCaracterRellenoNroDoc");
                strSegment = KEY.AppSettings("strSegment");
                strInteraction = KEY.AppSettings("strConstOpcBtnInteraccion");
                strContingencyClarify = KEY.AppSettings("strConstContingenciaClarify");
                strDocumentList = Claro.SIACU.Constants.DocumentList;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
            }

            try
            {
                oService = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.Service>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetDataService(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Telephone, objPreviousCustomerRequest.Audit.Transaction, objPreviousCustomerRequest.IpApplication, objPreviousCustomerRequest.ApplicationName, objPreviousCustomerRequest.UserApplication,out strCodeResponse);
                });


                if (oService != null)
                {

                    try
                    {
                        string outMessage = "";
                        string validateBondRFA = "";
                        oService.BondRFA = "N";
                        string nroLine = "";
                        string amountbond = "";
                        oService.BondAmountRFA = "";
                        string responsePortability = "", strPrincipalBalance = "", strPrincipalExpiration = "", MessageResponse = "", CodeResponse = "";

                        validateBondRFA = Claro.Web.Logging.ExecuteMethod<string>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Prepaid.ValidateBond(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, oService.SubscriberStatus, out outMessage);
                        });

                        if (validateBondRFA == "0")
                        {
                            oService.BondRFA = "S";
                            nroLine = oService.NroCelular;
                            validateBondRFA = Data.Dashboard.Prepaid.GetAmountBond(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, outMessage);

                            if (validateBondRFA == "0")
                            {
                                oService.BondAmountRFA = amountbond;
                            }

                        }
                        oService.oPortability = new Entity.Dashboard.Portability();
                        oService.listPortability = GetPortability(objPreviousCustomerRequest.Audit, oService.NroCelular, ref responsePortability);
                        oService.oPortability.RESPUESTA = responsePortability;

                        oService.listBalance = Data.Dashboard.Prepaid.GetPrepaidBalance(objPreviousCustomerRequest.Telephone, objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, objPreviousCustomerRequest.Audit.IPAddress, Claro.SIACU.Constants.SiacPreAbrev, objPreviousCustomerRequest.Audit.UserName, ref CodeResponse, ref MessageResponse, ref strPrincipalBalance, ref strPrincipalExpiration, oService.EsTFI);
                        if (oService.EsTFI)
                        {
                            objPreviousCustomerRequest.Telephone = Claro.SIACU.Data.Helper.ReturnTFI(objPreviousCustomerRequest.Telephone);
                        }
                        oService.SaldoPrincipal = strPrincipalBalance;
                        oService.FechaExpiracionSaldo = strPrincipalExpiration;
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
                    }
                }
                else { oService = new Service(); }
            }
            catch (Exception ex)
            {
                oService = new Service();
                oService.Transaction = objPreviousCustomerRequest.Audit.Transaction;
                Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
            }



            try
            {
                oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.Customer>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetPreviousCustomer(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, objPreviousCustomerRequest.Telephone, objPreviousCustomerRequest.Account, objPreviousCustomerRequest.ContactId, objPreviousCustomerRequest.FlagRegistry);
                });

                if (oCustomer != null)
                {
                    Entity.Dashboard.MembershipElectronic oMembershipElectronic;

                    try
                    {
                        oMembershipElectronic = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.MembershipElectronic>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Common.GetMembershipVoucherElectronic(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, oCustomer.TIPO_DOC, oCustomer.NRO_DOC);
                        });
                    }
                    catch (Exception ex)
                    {
                        oMembershipElectronic = null;
                        Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
                    }

                    if (oMembershipElectronic != null)
                    {
                        if (oMembershipElectronic.ESTADO_AFILIACION.Equals(Claro.Constants.LetterA))
                        {
                            oCustomer.AFILIACION = true;
                        }
                        else { oCustomer.AFILIACION = false; }

                    }
                    else
                    {
                        oCustomer.AFILIACION = false;
                    }
                    int cantcaracterdoc = oCustomer.NRO_DOC.Trim().Length;
                    string strSplitDNIRUC= KEY.AppSettings("strSplitDNIRUC");//PROY-SEGMENTOVALOR 140351

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


                    Entity.Dashboard.Prepaid.Customer oCustomers = new Entity.Dashboard.Prepaid.Customer()
                    {
                        TIPO_DOC = strDocumentType,
                        NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.NRO_DOC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)),
                        LONG_NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.NRO_DOC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)).Length.ToString()
                    };

                    try
                    {
                        oCustomer.SEGMENTO = Claro.Web.Logging.ExecuteMethod<string>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Common.GetSegmentCustomerQuery(oCustomers, objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.UserApplication, objPreviousCustomerRequest.IpApplication, objPreviousCustomerRequest.Audit.Transaction, objPreviousCustomerRequest.ApplicationName);
                        });

                    }
                    catch (Exception ex)
                    {
                        oCustomer.SEGMENTO = strSegment;
                        Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
                    }

                    oCustomer.FLAG_EMAIL = string.IsNullOrEmpty(oCustomer.FLAG_EMAIL) ? Claro.Constants.NumberOneNegativeString : oCustomer.FLAG_EMAIL;
                    oCustomer.EMAIL_CONFIRMACION = Helper.GetValueXML(oCustomer.FLAG_EMAIL.Trim(), Claro.SIACU.Constants.ListMailConfirm);

                    try
                    {
                        oCustomer.BANNER_MESSAGE = Dashboard.GetBanner(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, DateTime.Now, "", strTelephoneTypePre, Claro.SIACU.Constants.PrepaidMajuscule);
                    }
                    catch (Exception ex)
                    {
                        oCustomer.BANNER_MESSAGE = "";
                        Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
                    }

                    try
                    {
                        listDocumentType = Claro.Web.Logging.ExecuteMethod<List<Entity.ListItem>>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetDocumentType(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, strDocumentList); });
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
                        throw new Claro.MessageException(ex.Message.ToString());
                    }

                    if (listDocumentType != null)
                    {
                        foreach (Entity.ListItem item in listDocumentType)
                        {
                            if (item.Description.Equals(oCustomer.TIPO_DOC))
                            {
                                strDocumentTypeCode = item.Code;
                                break;
                            }
                        }
                    }

                    try
                    {
                        oCustomer.BLACKLIST = Claro.Web.Logging.ExecuteMethod<string>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Prepaid.ValidateBlackList(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, oCustomer.TELEFONO, strDocumentTypeCode, oCustomer.NRO_DOC); });
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
                        throw new Claro.MessageException(ex.Message.ToString());
                    }

                    if (oService != null && !string.IsNullOrWhiteSpace(oService.NroCelular) && (oService.NroCelular.Length == 9) && (oService.NroCelular.Substring(0, 1) == "9"))
                    {
                        oCustomer.TipoProductoTelefono = sb.Append(Claro.SIACU.Constants.PrepaidSpanish_).Append(Claro.SIACU.Constants.Movil).ToString();
                    }
                    else
                    {
                        oCustomer.TipoProductoTelefono = sb.Append(Claro.SIACU.Constants.PrepaidSpanish_).Append(Claro.SIACU.Constants.TFI).ToString();
                    }


                   

                    oCustomer.INTERACCION = strInteraction;
                    oCustomer.CONTINGENCIACLARIFY = strContingencyClarify;


                }
                else { oCustomer = new Customer(); }

            }
            catch (Exception ex)
            {
                oCustomer = new Customer();
                oCustomer.Transaction = objPreviousCustomerRequest.Audit.Transaction;
                Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
            }


            oCustomer.oService = oService;
            Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objGetPreviousCustomerResponse = new Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse()
            {
                objCustomer = oCustomer,
                CodeResponse = strCodeResponse
            };

            return objGetPreviousCustomerResponse;
        }



        /// <summary>
        /// Método para obtener listado de clientes.
        /// </summary>
        /// <param name="strTelephone">Teléfono de cliente</param>
        /// <param name="strAccount">Número de cuenta</param>
        /// <param name="strContactId">Id de contacto</param>
        /// <param name="strFlagRegistry">Flag de registro</param>
        /// <param name="strFlagGetAll">Flag para traer la data ordenada</param>
        /// <returns>Devuelve objeto PreviousCustomersResponse con el listado de clientes</returns>
        public static Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse GetPreviousCustomers(Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersRequest objPreviousCustomersRequest)
        {
            Entity.Dashboard.Prepaid.Customer oCustomer = null;
            Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse objPreviousCustomersResponse;
            List<Entity.Dashboard.Prepaid.Customer> oListCustomer = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.Customer>>(objPreviousCustomersRequest.Audit.Session, objPreviousCustomersRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Prepaid.GetPreviousCustomers(objPreviousCustomersRequest.Audit.Session, objPreviousCustomersRequest.Audit.Transaction, objPreviousCustomersRequest.Telephone, objPreviousCustomersRequest.Account, objPreviousCustomersRequest.ContactId, objPreviousCustomersRequest.FlagRegistry, objPreviousCustomersRequest.FlagGetAll);
            });

            List<Entity.Dashboard.Prepaid.Customer> oListCustomerOrdered = null;
            List<Entity.Dashboard.Prepaid.Customer> oListCustomerResponse = null;
            List<Entity.Dashboard.Prepaid.MotiveLow> listMotiveLow = null;

            if (objPreviousCustomersRequest.FlagGetAll.Equals(Claro.Constants.LetterS))
            {
                if (oListCustomer != null && oListCustomer.Count > 0)
                {
                    oListCustomerOrdered = oListCustomer.OrderByDescending(a => a.ESTADO_CONTACTO).OrderByDescending(a => a.FECHA_ACT).ToList();
                }
            }
            else
            {
                if (oListCustomer != null && oListCustomer.Count > 0)
                {
                    oListCustomerOrdered = new List<Entity.Dashboard.Prepaid.Customer>();
                    oCustomer = new Entity.Dashboard.Prepaid.Customer()
                    {
                        OBJID_CONTACTO = oListCustomer[0].OBJID_CONTACTO,
                        NOMBRES = oListCustomer[0].NOMBRES,
                        APELLIDOS = oListCustomer[0].APELLIDOS,
                        TELEFONO = oListCustomer[0].TELEFONO,
                        MODALIDAD = oListCustomer[0].MODALIDAD,
                        ESTADO_CONTACTO = oListCustomer[0].ESTADO_CONTACTO,
                        ESTADO_CONTRATO = oListCustomer[0].ESTADO_CONTRATO,
                        FECHA_ACT = oListCustomer[0].FECHA_ACT
                    };
                    oListCustomerOrdered.Add(oCustomer);
                }
            }

            if (oListCustomerOrdered != null && oListCustomerOrdered.Count > 0)
            {
                oListCustomerResponse = new List<Entity.Dashboard.Prepaid.Customer>();

                foreach (Entity.Dashboard.Prepaid.Customer itemCustomer in oListCustomerOrdered)
                {
                    if (itemCustomer.ESTADO_CONTACTO.ToUpper() != KEY.AppSettings("strContactoActivo").ToUpper())
                    {
                        listMotiveLow = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.MotiveLow>>(objPreviousCustomersRequest.Audit.Session, objPreviousCustomersRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Prepaid.GetListMotiveLow(objPreviousCustomersRequest.Audit.Session, objPreviousCustomersRequest.Audit.Transaction, objPreviousCustomersRequest.FlagHistory, itemCustomer.TELEFONO);
                        });


                        if (listMotiveLow != null && listMotiveLow.Count > 0)
                        {
                            bool esfind = false;
                            foreach (Entity.Dashboard.Prepaid.MotiveLow itemMotiveLow in listMotiveLow)
                            {
                                if (itemCustomer.OBJID_CONTACTO == itemMotiveLow.CodCli)
                                {
                                    itemCustomer.MOTIVOREGISTRO = itemMotiveLow.Motivo;
                                    itemCustomer.FAX = itemMotiveLow.FechaBaja.ToString();
                                    oListCustomerResponse.Add(itemCustomer);
                                    esfind = true;
                                }
                            }

                            if (!esfind)
                            {
                                itemCustomer.MOTIVOREGISTRO = string.Empty;
                                itemCustomer.FAX = string.Empty;
                                oListCustomerResponse.Add(itemCustomer);
                            }
                        }
                        else
                        {
                            itemCustomer.MOTIVOREGISTRO = string.Empty;
                            itemCustomer.FAX = string.Empty;
                            oListCustomerResponse.Add(itemCustomer);
                        }

                    }
                }
            }


            objPreviousCustomersResponse = new Entity.Dashboard.Prepaid.GetPreviousCustomers.PreviousCustomersResponse();
            objPreviousCustomersResponse.listCustomer = oListCustomerResponse;
            return objPreviousCustomersResponse;
        }

        /// <summary>
        /// Método para obtener el listado de llamadas del mes actual.
        /// </summary>
        /// <param name="strMsisdn">Parámetro de búsqueda de cliente</param>      
        /// <returns>Devuelve el objeto CallsResponse con el listado de llamadas del mes.</returns>
        public static Entity.Dashboard.Prepaid.GetCalls.CallsResponse GetCalls(Entity.Dashboard.Prepaid.GetCalls.CallsRequest objCallsRequest)
        {
           
            string clase=KEY.AppSettings("strClaseTipificaDatosLlamada"),subClase=KEY.AppSettings("strSubClaseTipificaDatosLlamada"),tipo=KEY.AppSettings("strPrepago");
            if (objCallsRequest.isTFI)
            {
                clase = KEY.AppSettings("strClaseTipificaDatosLlamadaFijo");
                subClase = KEY.AppSettings("strSubClaseTipificaDatosLlamadaFijo");
                tipo = KEY.AppSettings("strPrepagoFijo");
            }
            Claro.SIACU.Entity.Dashboard.Interaction objInteraction = new Entity.Dashboard.Interaction()
                    {
                       
                        CLASE = clase,
                        CODIGOEMPLEADO = objCallsRequest.Audit.UserName,
                        CODIGOSISTEMA = KEY.AppSettings("USRProceso"),
                        FLAGCASO = Claro.Constants.NumberZeroString,
                        HECHOENUNO = Claro.Constants.NumberZeroString,
                        METODOCONTACTO =KEY.AppSettings("MetodoContactoTelefonoDefault"),
                        NOTAS = KEY.AppSettings("strMensajeTipificaDatosLlamadas") + objCallsRequest.StartDate + " hasta " + objCallsRequest.EndDate,
                        SUBCLASE = subClase,
                        TELEFONO = objCallsRequest.TelephoneTipi,
                        TEXTRESULTADO = KEY.AppSettings("strNinguno"),
                        TIPOINTERACCION = KEY.AppSettings("AtencionDefault"),
                        TIPO = tipo

                    };
          
            Entity.Dashboard.Prepaid.GetCalls.CallsResponse objCallsResponse = new Entity.Dashboard.Prepaid.GetCalls.CallsResponse()
            {
                listCall = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.Call>>(objCallsRequest.Audit.Session, objCallsRequest.Audit.Transaction, () => { return Data.Dashboard.Prepaid.GetCalls(objCallsRequest.Audit, objCallsRequest.Msisdn, objCallsRequest.StartDate, objCallsRequest.EndDate, objCallsRequest.TypeQuery, objCallsRequest.TrafficType,objCallsRequest.FlagVisualize); }),
                resultTipi= Claro.Web.Logging.ExecuteMethod<bool>(objCallsRequest.Audit.Session, objCallsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.CreateInteraction(objCallsRequest.Audit.Transaction, objInteraction, objCallsRequest.Audit); })
            };

            return objCallsResponse;
        }

        /// <summary>
        /// Método para obtener el listado de recargas del cliente.
        /// </summary>
        /// <param name="strMsisdn">Número de teléfono</param>
        /// <param name="strStartDate">Fecha de inicio de búsqueda</param>
        /// <param name="strEndDate">Fecha de fin de búsqueda</param>
        /// <param name="strFlag">Flag</param>
        /// <param name="intNumberRecords">Número de registros que va se va a devolver</param>
        /// <returns>Devuelve objeto RechargesResponse con el listado de recargas del cliente.</returns>
        public static Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse GetRecharges(Entity.Dashboard.Prepaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            List<Entity.Dashboard.Prepaid.Recharge> listRecharge = null;
            try
            {
                listRecharge = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.Recharge>>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetRecharges(objRechargesRequest.Audit, objRechargesRequest.Msisdn, objRechargesRequest.StartDate, objRechargesRequest.EndDate,
                       objRechargesRequest.strtypeQuery,
                       objRechargesRequest.strMovementType,
                       objRechargesRequest.strcreditoDebito);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, ex.Message);
            }
            Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Prepaid.GetRecharges.RechargesResponse()
            {
                listRecharge = listRecharge
            };
            return objRechargesResponse;
        }

        /// <summary>
        /// Método para obtener récord de triación RFA.
        /// </summary>
        /// <param name="objRecordTriationRFARequest">Contiene rango de fechas, plan tarifario y número de teléfono.</param>
        /// <returns>Devuelve objeto RecordTriationRFAResponse con el listado de récord de triación RFA.</returns>
        public static Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse GetRecordTriationRFA(Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFARequest objRecordTriationRFARequest)
        {
            Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse objRecordTriationRFAResponse = new Entity.Dashboard.Prepaid.GetRecordTriationRFA.RecordTriationRFAResponse()
            {
                listHistoricalTriationRFA = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.HistoricalTriationRFA>>(objRecordTriationRFARequest.Audit.Session, objRecordTriationRFARequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetRecordTriationRFA(objRecordTriationRFARequest.Audit.Session, objRecordTriationRFARequest.Audit.Transaction, objRecordTriationRFARequest.Telephone, objRecordTriationRFARequest.DateStart, objRecordTriationRFARequest.DateEnd);
                })

            };
            return objRecordTriationRFAResponse;
        }

        /// <summary>3
        /// Método para obtener detalle de triación RFA.
        /// </summary>
        /// <param name="objDetailTriationRFARequest">Contiene id de interacción y número de teléfono.</param>
        /// <returns>Devuelve objeto DetailTriationRFAResponse con la información de triación de RFA.</returns>
        public static Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse GetDetailTriationRFA(Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFARequest objDetailTriationRFARequest)
        {
            Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse objDetailTriationRFAResponse = new Entity.Dashboard.Prepaid.GetDetailTriationRFA.DetailTriationRFAResponse()
            {
                listNumbersTriation = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.NumbersTriation>>(objDetailTriationRFARequest.Audit.Session, objDetailTriationRFARequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetDetailTriationRFA(objDetailTriationRFARequest.Audit.Session, objDetailTriationRFARequest.Audit.Transaction, objDetailTriationRFARequest.Telephone, objDetailTriationRFARequest.IdInteraction);
                })
            };

            return objDetailTriationRFAResponse;
        }

        /// <summary>
        /// Método para obtener bonos históricos.
        /// </summary>
        /// <param name="objHistoricalBondsRequest">Contiene teléfono y rango de fechas.</param>
        /// <returns>Devuelve objeto HistoricalBondsResponse con listado de bonos históricos.</returns>
        public static Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse GetHistoricalBonds(Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsRequest objHistoricalBondsRequest)
        {
            Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse objHistoricalBondsResponse = new Entity.Dashboard.Prepaid.GetHistoricalBonds.HistoricalBondsResponse()
            {
                listHistoricalBonds = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.HistoricalBonds>>(objHistoricalBondsRequest.Audit.Session, objHistoricalBondsRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetHistoricalBonds(objHistoricalBondsRequest.Audit.Session, objHistoricalBondsRequest.Audit.Transaction, objHistoricalBondsRequest.Telephone, objHistoricalBondsRequest.DateStart, objHistoricalBondsRequest.DateEnd);
                })
            };
            return objHistoricalBondsResponse;
        }

        /// <summary>
        /// Método para obtener información de Pin y Puk.
        /// </summary>
        /// <param name="objPinPukRequest">Contiene tipo, llave y rango de fechas.</param>
        /// <returns>Devuelve objeto PinPukResponse con información de Pin y puk.</returns>
        public static Claro.SIACU.Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse GetPinPuk(Claro.SIACU.Entity.Dashboard.Prepaid.GetPinPuk.PinPukRequest objPinPukRequest)
        {
            string strMensaje = "", strNroIMSI = "", strListName = "", strActionId = "", strFieldName = "", strDescriptionErrorHLR = "";
            bool blnPermisoBuscarBSCS = false;
            List<Entity.Dashboard.Prepaid.PinPuk> listPinPuk = null;
            List<Entity.Dashboard.HLR> listHLR = null;

            try
            {
                strListName = Claro.SIACU.Constants.ListNameSendList;
                strActionId = Claro.SIACU.Constants.IdActionConsultZmioMsisdn;
                strFieldName = KEY.AppSettings("strParamMSISDNConectorUDB");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, ex.Message);
            }

            try
            {
                listHLR = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.HLR>>(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, () =>
                { return Data.Dashboard.Common.GetHLRUDB(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, objPinPukRequest.Audit.ApplicationName, objPinPukRequest.Audit.IPAddress, objPinPukRequest.Audit.UserName, objPinPukRequest.Key, strActionId, strFieldName, strListName, out strDescriptionErrorHLR); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }

            if (listHLR != null)
            {
                if (strDescriptionErrorHLR.Equals(Claro.SIACU.Constants.ZeroNumber))
                {
                    foreach (Entity.Dashboard.HLR item in listHLR)
                    {
                        if (item.Code.Equals(Claro.SIACU.Constants.ZmioImsi))
                        {
                            strNroIMSI = item.Description;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strNroIMSI))
                {
                    strMensaje = "  Búsqueda en IN(Pre-Pago) : Input = " + objPinPukRequest.Key + " / Output-IMSI = " + strNroIMSI + " / Registro Encontrado. ";
                    blnPermisoBuscarBSCS = true;
                }
                else
                {
                    strMensaje = "  Búsqueda en IN(Pre-Pago) : Input = " + objPinPukRequest.Key + " / IMSI no encontrado. ";
                }

                if (blnPermisoBuscarBSCS)
                {
                    listPinPuk = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.PinPuk>>(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Prepaid.GetListPinPuk(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, strNroIMSI, objPinPukRequest.Type, objPinPukRequest.StarDate, objPinPukRequest.EndDate);
                    });

                    if (listPinPuk != null)
                    {
                        strMensaje = strMensaje + "  Búsqueda en BSCS(PostPago) : Input = " + strNroIMSI + " / Output = Registro encontrado. ";
                    }
                    else
                    {
                        strMensaje = strMensaje + "  Búsqueda en BSCS(PostPago) : Input = " + strNroIMSI + " / Output = Registro NO encontrado. ";
                    }
                }
            }

            Claro.SIACU.Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse objPinPukResponse = new Entity.Dashboard.Prepaid.GetPinPuk.PinPukResponse()
            {
                listPinPuk = listPinPuk,
                Respuesta = strMensaje
            };
            return objPinPukResponse;
        }

        /// <summary>
        /// Método para obtener las instantáneas.
        /// </summary>
        /// <param name="objInstantRequest">Contiene tipo y valor de búsqueda.</param>
        /// <returns>Devuelve objeto InstantResponse con información de instantáneas.</returns>
        public static Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantResponse GetInstant(Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantRequest objInstantRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetInstant.InstantResponse objInstantResponse = new Entity.Dashboard.Prepaid.GetInstant.InstantResponse()
            {
                listInstant = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Instant>>(objInstantRequest.Audit.Session, objInstantRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Prepaid.GetListInstant(objInstantRequest.Audit.Session, objInstantRequest.Audit.Transaction, objInstantRequest.DataSearch, objInstantRequest.TypeSearch);
                    })
            };
            return objInstantResponse;
        }

        /// <summary>
        /// Método para obtener información PEL para prepago.
        /// </summary>
        /// <param name="objPELRequest">Contiene teléfono</param>
        /// <returns>Devuelve objeto PEL.</returns>
        public static Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELResponse GetPEL(Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELRequest objPELRequest)
        {

            Claro.SIACU.Entity.Dashboard.Prepaid.PEL objPEL = null;
            try
            {
                objPEL = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Prepaid.PEL>(objPELRequest.Audit.Session, objPELRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Prepaid.GetPEL(objPELRequest.Audit.Session, objPELRequest.Audit.Transaction, objPELRequest.Telephone);
                    });

            }
            catch (Exception ex)
            {
                objPEL = null;
                Claro.Web.Logging.Error(objPELRequest.Audit.Session, objPELRequest.Audit.Transaction, ex.Message);
            }

            if (objPEL != null)
            {
                if (!string.IsNullOrEmpty(objPEL.P_FECHA_ACT))
                {
                    objPEL.P_FECHA_ACT = objPEL.P_FECHA_ACT.Substring(0, 4) + "/" + objPEL.P_FECHA_ACT.Substring(4, 2) + "/" + objPEL.P_FECHA_ACT.Substring(6, 2) + " " + objPEL.P_FECHA_ACT.Substring(8, 2) + ":" + objPEL.P_FECHA_ACT.Substring(10, 2) + ":" + objPEL.P_FECHA_ACT.Substring(12, 2);
                }
            }
            else
            {
                objPEL = new Claro.SIACU.Entity.Dashboard.Prepaid.PEL();
            }
            Claro.SIACU.Entity.Dashboard.Prepaid.GetPEL.PELResponse objPELResponse = new Entity.Dashboard.Prepaid.GetPEL.PELResponse()
            {
                objPEL = objPEL
            };
            return objPELResponse;
        }

        /// <summary>
        /// Método para obtener información de la venta.
        /// </summary>
        /// <param name="strSession">Sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strApplicationID">Id de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strApplicationUser">Usuario de aplicación</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strMatter">Importe</param>
        /// <param name="strIssueSeries">Serie de emisión</param>
        /// <returns>Devuelve objeto TelephoneZsansData con información de la venta.</returns>
        public static Entity.Dashboard.Prepaid.TelephoneZsansData GetSalesData(string strSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber, string strMatter, string strIssueSeries)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.TelephoneZsansData objTelephoneZsansData = null;

            try
            {
                objTelephoneZsansData = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Prepaid.TelephoneZsansData>(strSession, strTransactionID, () =>
                {
                    return Data.Dashboard.Prepaid.GetSalesData(strSession, strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strPhoneNumber, strMatter, strIssueSeries);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strSession, strTransactionID, ex.Message);
            }
            return objTelephoneZsansData;
        }

        /// <summary>
        /// Método para obtener información de la venta PEL.
        /// </summary>
        /// <param name="objSalesDataRequest">Contiene series de emisión, importe y número de teléfono.</param>
        /// <returns>Devuelve objeto SalesDataResponse con información de venta PEL.</returns>
        public static Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse GetSalesDataPEL(Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataRequest objSalesDataRequest)
        {

            Claro.SIACU.Entity.Dashboard.Prepaid.DataSalePEL objDataSalePEL = null;
            Claro.SIACU.Entity.Dashboard.Prepaid.TelephoneZsansData listTelephoneZsansData;

            listTelephoneZsansData = GetSalesData(objSalesDataRequest.Audit.Session, objSalesDataRequest.Audit.Transaction, objSalesDataRequest.ApplicationID, objSalesDataRequest.ApplicationName, objSalesDataRequest.ApplicationUser, objSalesDataRequest.PhoneNumber, objSalesDataRequest.Matter, objSalesDataRequest.IssueSeries);

            if (listTelephoneZsansData != null)
            {
                objDataSalePEL = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Prepaid.DataSalePEL>(objSalesDataRequest.Audit.Session, objSalesDataRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetSalesDataPEL(objSalesDataRequest.Audit.Session, objSalesDataRequest.Audit.Transaction, listTelephoneZsansData.NumeroTelefono, listTelephoneZsansData.MATNR, listTelephoneZsansData.SERNR);
                });

                if (string.IsNullOrEmpty(objDataSalePEL.IMEI) && string.IsNullOrEmpty(objDataSalePEL.SerieEquipo))
                {
                    objDataSalePEL.DescripcionVenta = Claro.SIACU.Constants.Pack;
                }
                else
                {
                    if (string.IsNullOrEmpty(objDataSalePEL.IMEI) && !string.IsNullOrEmpty(objDataSalePEL.SerieEquipo))
                    {
                        objDataSalePEL.DescripcionVenta = Claro.SIACU.Constants.Chip;
                    }
                }
            }

            Claro.SIACU.Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse objSalesDataResponse = new Entity.Dashboard.Prepaid.GetSalesData.SalesDataResponse()
            {
                objDataSalePEL = objDataSalePEL
            };
            return objSalesDataResponse;
        }

        /// <summary>
        /// Método para obtener información de las deudas.
        /// </summary>
        /// <param name="objDebtRequest">Contiene teléfono y bukrs.</param>
        /// <returns>Devuelve objeto DebtResponse con información de la deuda.</returns>
        public static Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtResponse GetDebt(Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtRequest objDebtRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.GetDebt.DebtResponse objDebtResponse = new Entity.Dashboard.Prepaid.GetDebt.DebtResponse()
            {
                objDebtSaleDues = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Prepaid.DebtSaleDues>(objDebtRequest.Audit.Session, objDebtRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetDebt(objDebtRequest.Audit.Session, objDebtRequest.Audit.Transaction, objDebtRequest.Bukrs, objDebtRequest.Telephone);
                })
            };
            return objDebtResponse;
        }

        /// <summary>
        /// Método para crear contacto sin usuario.
        /// </summary>
        /// <param name="objCreateContactNotUSerRequest">Contiene modalidad, tipo de proceso y motivo de registro</param>
        /// <returns>Devuelve objeto CreateContactNotUSerResponse con información de la creación del contacto sin usuario.</returns>
        public static HELPER_CREATEUSER.CreateContactNotUSerResponse CreateContactNotUSer(HELPER_CREATEUSER.CreateContactNotUSerRequest objCreateContactNotUSerRequest)
        {
            string strFlagUpdate = "", strMessageText = "";
            string strModalityKey = "", strModalityKeyNotUser = "";
            try
            {
                strModalityKey = KEY.AppSettings("strModalidadPrepagoClarify");
                strModalityKeyNotUser = KEY.AppSettings("strModalidadNoUsuario");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCreateContactNotUSerRequest.Audit.Session, objCreateContactNotUSerRequest.Audit.Transaction, ex.Message);
            }

            Customer objCustomer = new Customer();
            if (objCreateContactNotUSerRequest.TypeProcess.Equals(Claro.SIACU.Constants.CC))
            {
                if (objCreateContactNotUSerRequest.Modality.Equals(Claro.SIACU.Constants.PC))
                {
                    objCustomer.MODALIDAD = strModalityKey;
                    objCustomer.FLAG_REGISTRADO = (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Registered)) ? Claro.Constants.NumberOne : 0;
                    if (!objCreateContactNotUSerRequest.MotiveRegister.Equals(Claro.Constants.NumberOneNegativeString))
                        objCustomer.MOTIVOREGISTRO = objCreateContactNotUSerRequest.MotiveRegister;
                }
                else if (objCreateContactNotUSerRequest.Modality.Equals(Claro.SIACU.Constants.NU))
                {
                    objCustomer.MODALIDAD = strModalityKeyNotUser;
                    objCustomer.FLAG_REGISTRADO = 0;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Registered))
                {
                    objCustomer.FLAG_REGISTRADO = Claro.Constants.NumberOne;
                    if (!objCreateContactNotUSerRequest.MotiveRegister.Equals(Claro.Constants.NumberOneNegativeString)) objCustomer.MOTIVOREGISTRO = objCreateContactNotUSerRequest.MotiveRegister;
                }
                else
                {
                    objCustomer.FLAG_REGISTRADO = 0;
                }
            }

            objCustomer.TELEFONO = objCreateContactNotUSerRequest.TelephoneCustomer;
            objCustomer.USUARIO = objCreateContactNotUSerRequest.Audit.UserName;
            objCustomer.NOMBRES = objCreateContactNotUSerRequest.Name;
            objCustomer.APELLIDOS = objCreateContactNotUSerRequest.LastName;

            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.DateBirth)) objCustomer.FECHA_NAC = objCreateContactNotUSerRequest.DateBirth;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Sex)) objCustomer.SEXO = objCreateContactNotUSerRequest.Sex;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.TypeDocument)) objCustomer.TIPO_DOC = objCreateContactNotUSerRequest.TypeDocument;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.NumberDocument)) objCustomer.NRO_DOC = objCreateContactNotUSerRequest.NumberDocument;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.StateCivil)) objCustomer.ESTADO_CIVIL = objCreateContactNotUSerRequest.StateCivil;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Occupation)) objCustomer.OCUPACION = objCreateContactNotUSerRequest.Occupation;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Position)) objCustomer.CARGO = objCreateContactNotUSerRequest.Position;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.TelephoneReference)) objCustomer.TELEF_REFERENCIA = objCreateContactNotUSerRequest.TelephoneReference;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Fax)) objCustomer.FAX = objCreateContactNotUSerRequest.Fax;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Email)) objCustomer.EMAIL = objCreateContactNotUSerRequest.Email;
            objCustomer.DOMICILIO = objCreateContactNotUSerRequest.Address;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.District)) objCustomer.DISTRITO = objCreateContactNotUSerRequest.District;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.ZipCode)) objCustomer.ZIPCODE = objCreateContactNotUSerRequest.ZipCode;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Urbanization)) objCustomer.URBANIZACION = objCreateContactNotUSerRequest.Urbanization;
            objCustomer.CIUDAD = objCreateContactNotUSerRequest.City;

            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Department)) objCustomer.DEPARTAMENTO = objCreateContactNotUSerRequest.Department;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.Reference)) objCustomer.REFERENCIA = objCreateContactNotUSerRequest.Reference;
            if (!string.IsNullOrEmpty(objCreateContactNotUSerRequest.ConfirmMail)) objCustomer.FLAG_EMAIL = objCreateContactNotUSerRequest.ConfirmMail;

            HELPER_CREATEUSER.CreateContactNotUSerResponse objCreateContactNotUSerResponse = new HELPER_CREATEUSER.CreateContactNotUSerResponse()
            {
                Response = Claro.Web.Logging.ExecuteMethod<bool>(objCreateContactNotUSerRequest.Audit.Session, objCreateContactNotUSerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.CreateContactNotUSer(objCreateContactNotUSerRequest.Audit.Session, objCreateContactNotUSerRequest.Audit.Transaction, objCustomer, out strFlagUpdate, out strMessageText);
                }),

                FlagUpdate = strFlagUpdate,
                MessageText = strMessageText
            };

            return objCreateContactNotUSerResponse;
        }

        



        /// <summary>
        /// Método para obtener los datos del cliente.
        /// </summary>
        /// <param name="strTelephone">Teléfono del cliente</param>
        /// <param name="strAccount">Número de Cuenta</param>
        /// <param name="strContactId">Id del cliente</param>
        /// <param name="strFlagRegistry">Flag de Registro</param>
        /// <param name="strIdTransaction">Id de Trasacción</param>
        /// <param name="strIpApplication">Ip de Aplicación</param>
        /// <param name="strApplicationName">Nombre de la aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <returns>Devuelve objeto PreviousCustomerResponse con información del cliente</returns>
        public static Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse GetOnlyDataCustomer(Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objPreviousCustomerRequest)
        {
            Entity.Dashboard.Prepaid.Customer oCustomer;

            try
            {
                oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.Customer>(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetPreviousCustomer(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, objPreviousCustomerRequest.Telephone, objPreviousCustomerRequest.Account, objPreviousCustomerRequest.ContactId, objPreviousCustomerRequest.FlagRegistry);
                });
            }
            catch (Exception ex)
            {
                oCustomer = null;
                Claro.Web.Logging.Error(objPreviousCustomerRequest.Audit.Session, objPreviousCustomerRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objGetPreviousCustomerResponse = new Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse()
            {
                objCustomer = oCustomer
            };

            return objGetPreviousCustomerResponse;
        }

        public static Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse GetConsumptionDataPacket(Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketRequest objConsumptionDataPacketRequest)
        {
            objConsumptionDataPacketRequest.PackageODCS.LineTypeId = Claro.Constants.NumberOneString;
            Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse objConsumptionDataPacketResponse = new Entity.Dashboard.Prepaid.GetConsumptionDataPacket.ConsumptionDataPacketResponse()
            {
                PackageODCSs = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS>>(objConsumptionDataPacketRequest.Audit.Session, objConsumptionDataPacketRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetConsumptionDataPacket(objConsumptionDataPacketRequest.Audit, objConsumptionDataPacketRequest.PackageODCS);
                })
            };
            if (objConsumptionDataPacketResponse.PackageODCSs != null)
                objConsumptionDataPacketResponse.PackageODCSs = (from PackageODCS p in objConsumptionDataPacketResponse.PackageODCSs
                                                                 where Convert.ToDate(p.ActivationDate).Date >= objConsumptionDataPacketRequest.StartDate.Date && Convert.ToDate(p.ActivationDate).Date <= objConsumptionDataPacketRequest.EndDate.Date
                                                                 select p).ToList();

            return objConsumptionDataPacketResponse;
        }


        public static RateStateResponse GetRateState(RateStateRequest objRequest)
        {

            RateStateResponse RateState = null;

            try
            {
                RateState = Claro.Web.Logging.ExecuteMethod<RateStateResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                {
                    return RateState = Data.Dashboard.Prepaid.GetRateState(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return RateState;


        }

    }
}
