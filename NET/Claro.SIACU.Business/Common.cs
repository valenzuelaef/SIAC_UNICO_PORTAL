using Claro.SIACU.Entity;
using System;
using System.Collections.Generic;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Business
{
    public static class Common
    {
        /// <summary>
        ///  Método para obtener listado de los clientes históricos por medio del número de documento.
        /// </summary>
        /// <param name="strTypeDocument">Tipo de Documento</param>
        /// <param name="strNumberDocument">Número de Documento</param>
        /// <returns>Devuelve objeto SegmentsResponse con el listado de clientes históricos.</returns>
        public static Entity.Common.GetSegments.SegmentsResponse GetSegments(Entity.Common.GetSegments.SegmentsRequest objSegmentsRequest)
        {
            string strCodeResponse = "";
            Entity.Common.GetSegments.SegmentsResponse objSegmentsResponse = new Entity.Common.GetSegments.SegmentsResponse()
            {
                ListSegment = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Segment>>(objSegmentsRequest.Audit.Session, objSegmentsRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetSegments(objSegmentsRequest.Audit.Session, objSegmentsRequest.Audit.Transaction, objSegmentsRequest.TypeDocument, objSegmentsRequest.NumberDocument, out strCodeResponse); })
            };
            objSegmentsResponse.code = strCodeResponse;
      
            return objSegmentsResponse;
        }

        /// <summary>
        /// Método para obtener listado de grupos de bolsas que se encuentra en un XML.
        /// </summary>
        /// <param name="strNameFunction">Nombre de la lista de valores</param>
        /// <returns>Devuelve objeto GroupBagListResponse con el listado de grupo de bolsas del xml.</returns>
        public static Entity.Common.GetGroupBagList.GroupBagListResponse GetGroupBagList(Entity.Common.GetGroupBagList.GroupBagLisRequest objGroupBagListRequest)
        {
            Entity.Common.GetGroupBagList.GroupBagListResponse objGroupBagListResponse = new Entity.Common.GetGroupBagList.GroupBagListResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objGroupBagListRequest.Audit.Session, objGroupBagListRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("listaTipoTrafico"); })
            };
            return objGroupBagListResponse;
        }

        public static Entity.Common.GetEventType.EventTypeResponse GetEventType(Entity.Common.GetEventType.EventTypeRequest objEventTypeRequest)
        {
            Entity.Common.GetEventType.EventTypeResponse objEventTypeResponse = new Entity.Common.GetEventType.EventTypeResponse()
            {
                EventTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objEventTypeRequest.Audit.Session, objEventTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML(objEventTypeRequest.EventType); })
            };
            return objEventTypeResponse;
        }

        /// <summary>
        /// Método para obtener listado de bonos comprados.
        /// </summary>
        /// <returns>Devuelve objeto BagListResponse con el listado de bonos comprados.</returns>
        public static Entity.Common.GetBagList.BagListResponse GetBagList(Entity.Common.GetBagList.BagListResquest objBagListRequest)
        {
            Entity.Common.GetBagList.BagListResponse objBagListResponse = new Entity.Common.GetBagList.BagListResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objBagListRequest.Audit.Session, objBagListRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("listaBonosComprados"); })
            };
            return objBagListResponse;
        }

        /// <summary>
        /// Método para obtener listado de tipo de búsqueda.
        /// </summary>
        /// <returns>Devuelve objeto SearchTypeResponse con el listado de tipos de búsqueda.</returns>
        public static Entity.Common.GetSearchType.SearchTypeResponse GetSearchTypeList(Entity.Common.GetSearchType.SearchTypeRequest objSearchTypeRequest)
        {
            Entity.Common.GetSearchType.SearchTypeResponse objSearchTypeResponse = new Entity.Common.GetSearchType.SearchTypeResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objSearchTypeRequest.Audit.Session, objSearchTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("listaTipoBusqueda"); })
            };
            return objSearchTypeResponse;
        }

        /// <summary>
        /// Método para obtener información de portabilidad.
        /// </summary>
        /// <param name="objPortabilityRequest">Contiene teléfono</param>
        /// <returns>Devuelve objeto PortabilityResponse con la información de portabilidad.</returns>
        public static Entity.Common.GetPortability.PortabilityResponse GetPortability(Entity.Common.GetPortability.PortabilityRequest objPortabilityRequest)
        {
            string strRespuesta = "", portIN = "", portOUT = "";

            try
            {
                portIN = KEY.AppSettings("PortabilidadPortIN");
                portOUT = KEY.AppSettings("PortabilidadPortOUT");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, ex.Message);
            }

            Entity.Common.GetPortability.PortabilityResponse objPortabilityResponse = new Entity.Common.GetPortability.PortabilityResponse
            {
                ListPortability = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Portability>>(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetPortability(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, objPortabilityRequest.Telephone, out strRespuesta); })
            };

            if (objPortabilityResponse != null && objPortabilityResponse.ListPortability != null && objPortabilityResponse.ListPortability.Count > 0)
            {
                Claro.SIACU.Entity.Dashboard.Portability objPortabilitys = objPortabilityResponse.ListPortability[0];
                objPortabilityResponse.Respuesta = strRespuesta;
                objPortabilityResponse.ApplicationNumber = objPortabilitys.NUMERO_SOLICITUD;
                objPortabilityResponse.State = objPortabilitys.DESCRPCION_ESTADO_PROCESO;
                objPortabilityResponse.RegistrationDate = objPortabilitys.FECHA_REGISTRO;
                objPortabilityResponse.TrTypeProcessDate = false;
                objPortabilityResponse.TrTypeProcessOperator = false;

                if (strRespuesta != null && strRespuesta != Claro.SIACU.Constants.InProcessPortability)
                {
                    objPortabilityResponse.TrTypeProcessDate = true;
                    objPortabilityResponse.TrTypeProcessOperator = true;

                    if (objPortabilitys.TIPO_PORTABILIDAD == portOUT)
                    {
                        objPortabilityResponse.TypeProcessDate = Claro.SIACU.Constants.DateDeactivation;
                        objPortabilityResponse.TypeProcessOperator = Claro.SIACU.Constants.OperatorToWhichPort;
                        objPortabilityResponse.ExecutionDate = objPortabilitys.FECHA_EJECUCION;
                        objPortabilityResponse.Operator = ValidateDescriptionPortability(objPortabilitys.CODIGO_OPERADOR_RECEPTOR);
                    }
                    else if (objPortabilitys.TIPO_PORTABILIDAD == portIN)
                    {
                        objPortabilityResponse.TypeProcessDate = Claro.SIACU.Constants.DateActivation;
                        objPortabilityResponse.TypeProcessOperator = Claro.SIACU.Constants.OperatorOfThePort;
                        objPortabilityResponse.ExecutionDate = objPortabilitys.FECHA_EJECUCION;
                        objPortabilityResponse.Operator = ValidateDescriptionPortability(objPortabilitys.CODIGO_OPERADOR_CEDENTE);
                    }
                }
            }

            return objPortabilityResponse;
        }

        /// <summary>
        /// Método para validar la descripción de portabilidad.
        /// </summary>
        /// <param name="strReceivingOperatorCode">Código de operador receptor.</param>
        /// <returns>Devuelve cadena con la descripción de portabilidad.</returns>
        private static string ValidateDescriptionPortability(string strReceivingOperatorCode)
        {
            string strAnswer = "";
            switch (strReceivingOperatorCode)
            {
                case Claro.SIACU.Constants.OperatorCode00A01:
                    strAnswer = Claro.SIACU.Constants.Answer00A01;
                    break;
                case Claro.SIACU.Constants.OperatorCode00A02:
                    strAnswer = Claro.SIACU.Constants.Answer00A02;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R01:
                    strAnswer = Claro.SIACU.Constants.Answer01R01;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R02:
                    strAnswer = Claro.SIACU.Constants.Answer01R02;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R03:
                    strAnswer = Claro.SIACU.Constants.Answer01R03;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R04:
                    strAnswer = Claro.SIACU.Constants.Answer01R04;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R05:
                    strAnswer = Claro.SIACU.Constants.Answer01R05;
                    break;
                case Claro.SIACU.Constants.OperatorCode01D01:
                    strAnswer = Claro.SIACU.Constants.Answer01D01;
                    break;
                case Claro.SIACU.Constants.OperatorCode01D02:
                    strAnswer = Claro.SIACU.Constants.Answer01D02;
                    break;
                case Claro.SIACU.Constants.OperatorCode01D03:
                    strAnswer = Claro.SIACU.Constants.Answer01D03;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A03:
                    strAnswer = Claro.SIACU.Constants.Answer01A03;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A04:
                    strAnswer = Claro.SIACU.Constants.Answer01A04;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A05:
                    strAnswer = Claro.SIACU.Constants.Answer01A05;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R06:
                    strAnswer = Claro.SIACU.Constants.Answer01R06;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A06:
                    strAnswer = Claro.SIACU.Constants.Answer01A06;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R07:
                    strAnswer = Claro.SIACU.Constants.Answer01R07;
                    break;
                case Claro.Constants.NumberTwentyString:
                    strAnswer = Claro.SIACU.Constants.Answer20;
                    break;
                case Claro.Constants.NumberTwentyOneString:
                    strAnswer = Claro.SIACU.Constants.Answer21;
                    break;
                case Claro.Constants.NumberTwentyTwoString:
                    strAnswer = Claro.SIACU.Constants.Answer22;
                    break;
            }
            return strAnswer;
        }

        /// <summary>
        /// Método para obtener tipo de documento de cliente.
        /// </summary>
        /// <param name="objTypeDocumentCustomerRequest">No contiene información.</param>
        /// <returns>Devuelve objeto TypeDocumentCustomerResponse con el listado de tipo de documento del cliente.</returns>
        public static Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse GetTypeDocumentCustomer(Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerRequest objTypeDocumentCustomerRequest)
        {
            Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse objTypeDocumentCustomerResponse = new Entity.Common.GetTypeDocumentCustomer.TypeDocumentCustomerResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objTypeDocumentCustomerRequest.Audit.Session, objTypeDocumentCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetTypeDocumentCustomer(objTypeDocumentCustomerRequest.Audit.Session, objTypeDocumentCustomerRequest.Audit.Transaction); })
            };
            return objTypeDocumentCustomerResponse;
        }

        /// <summary>
        /// Método para obtener listado de estado civil.
        /// </summary>
        /// <param name="objStateCivilRequest">No contiene información.</param>
        /// <returns>Devuelve objeto StateCivilResponse con el listado de estado civil.</returns>
        public static Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse GetStateCivil(Claro.SIACU.Entity.Common.GetStateCivil.StateCivilRequest objStateCivilRequest)
        {
            Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse objStateCivilResponse = new Claro.SIACU.Entity.Common.GetStateCivil.StateCivilResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objStateCivilRequest.Audit.Session, objStateCivilRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetStateCivil(objStateCivilRequest.Audit.Session, objStateCivilRequest.Audit.Transaction); })
            };
            return objStateCivilResponse;
        }

        /// <summary>
        /// Método para obtener listado de ocupaciones.
        /// </summary>
        /// <param name="objOccupationRequest">No contiene información.</param>
        /// <returns>Devuelve objeto OccupationResponse con el listado de ocupaciones.</returns>
        public static Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse GetOccupation(Claro.SIACU.Entity.Common.GetOccupation.OccupationRequest objOccupationRequest)
        {
            Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse objOccupationResponse = new Claro.SIACU.Entity.Common.GetOccupation.OccupationResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objOccupationRequest.Audit.Session, objOccupationRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetOccupation(objOccupationRequest.Audit.Session, objOccupationRequest.Audit.Transaction); })
            };
            return objOccupationResponse;
        }

        /// <summary>
        /// Método para obtener listado de sexos.
        /// </summary>
        /// <param name="objSexRequest">No contiene información</param>
        /// <returns>Devuelve objeto SexResponse con el listado de sexos.</returns>
        public static Claro.SIACU.Entity.Common.GetSex.SexResponse GetSex(Claro.SIACU.Entity.Common.GetSex.SexRequest objSexRequest)
        {
            Claro.SIACU.Entity.Common.GetSex.SexResponse objSexResponse = new Claro.SIACU.Entity.Common.GetSex.SexResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objSexRequest.Audit.Session, objSexRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaSexo"); })
            };
            return objSexResponse;
        }

        /// <summary>
        /// Método para obtener listado de Mail a confirmar.
        /// </summary>
        /// <param name="objConfirmMailRequest">No contiene información.</param>
        /// <returns>Devuelve objeto ConfirmMailResponse con el listado de mails a confirmar.</returns>
        public static Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse GetConfirmMail(Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailRequest objConfirmMailRequest)
        {
            Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse objConfirmMailResponse = new Claro.SIACU.Entity.Common.GetConfirmMail.ConfirmMailResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objConfirmMailRequest.Audit.Session, objConfirmMailRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaMailConfirmar"); })
            };
            return objConfirmMailResponse;
        }

        /// <summary>
        /// Método para obtener motivos de registro.
        /// </summary>
        /// <param name="objMotiveRegisterRequest"></param>
        /// <returns>Devuelve objeto MotiveRegisterResponse con los motivos de registro.</returns>
        public static Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse GetMotiveRegister(Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterRequest objMotiveRegisterRequest)
        {
            Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse objMotiveRegisterResponse = new Claro.SIACU.Entity.Common.GetMotiveRegister.MotiveRegisterResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objMotiveRegisterRequest.Audit.Session, objMotiveRegisterRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetMotiveRegister(objMotiveRegisterRequest.Audit.Session, objMotiveRegisterRequest.Audit.Transaction); })
            };
            return objMotiveRegisterResponse;
        }

        /// <summary>
        /// Método para obtener tipos de estados.
        /// </summary>
        /// <param name="objStateTypeRequest">Contiene id de lista.</param>
        /// <returns>Devuelve objeto StateTypeResponse con listado de tipos de estados.</returns>
        public static Entity.Common.GetStateType.StateTypeResponse GetStateType(Entity.Common.GetStateType.StateTypeRequest objStateTypeRequest)
        {
            Entity.Common.GetStateType.StateTypeResponse objStateTypeResponse = new Entity.Common.GetStateType.StateTypeResponse()
            {
                StateTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objStateTypeRequest.Audit.Session, objStateTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetStateType(objStateTypeRequest.Audit.Session, objStateTypeRequest.Audit.Transaction, objStateTypeRequest.IdList); })
            };
            return objStateTypeResponse;
        }

        /// <summary>
        /// Método para obtener tipo de transacciones.
        /// </summary>
        /// <returns>Devuelve objeto TransactionTypeResponse con el listado de tipos de transacciones.</returns>
        public static Entity.Common.GetTransactionType.TransactionTypeResponse GetTransactionType(Entity.Common.GetTransactionType.TransactionTypeRequest objRequest)
        {
            Entity.Common.GetTransactionType.TransactionTypeResponse objTransactionTypeResponse = new Entity.Common.GetTransactionType.TransactionTypeResponse()
            {
                TransactionTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetTransactionType(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };

            return objTransactionTypeResponse;
        }

        /// <summary>
        /// Método para obtener tipo de cac o dac.
        /// </summary>
        /// <param name="objCacDacTypeRequest">No contiene información.</param>
        /// <returns>Devuelve objeto CacDacTypeResponse con los tipos de cac o dac.</returns>
        public static Entity.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest)
        {
            Entity.Common.GetCacDacType.CacDacTypeResponse objCacDacTypeResponse = new Entity.Common.GetCacDacType.CacDacTypeResponse()
            {
                CacDacTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objCacDacTypeRequest.Audit.Session, objCacDacTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetCacDacType(objCacDacTypeRequest.Audit.Session, objCacDacTypeRequest.Audit.Transaction, Data.Dashboard.Common.GetCodeList(objCacDacTypeRequest.Audit.Session, objCacDacTypeRequest.Audit.Transaction, Claro.SIACU.Constants.DAC)); })
            };

            return objCacDacTypeResponse;
        }

        /// <summary>
        /// Método para obtener tipo de teléfono por contrato.
        /// </summary>
        /// <param name="objTelephoneTypeRequest">Contiene id de contrato.</param>
        /// <returns>Devuelve objeto TelephoneTypeResponse con listado de tipos de teléfono.</returns>
        public static Entity.Common.GetTelephoneType.TelephoneTypeResponse GetTelephoneType(Entity.Common.GetTelephoneType.TelephoneTypeRequest objTelephoneTypeRequest)
        {
            Entity.Common.GetTelephoneType.TelephoneTypeResponse objTelephoneTypeResponse = new Entity.Common.GetTelephoneType.TelephoneTypeResponse();

            if ((objTelephoneTypeRequest.Application.Equals(Claro.SIACU.Constants.LTE)))
            {
                objTelephoneTypeResponse.TelephoneTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objTelephoneTypeRequest.Audit.Session, objTelephoneTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetTelephoneTypeLTE(objTelephoneTypeRequest.Audit.Session, objTelephoneTypeRequest.Audit.Transaction, objTelephoneTypeRequest.ContractId); });
            }
            else 
            {
                objTelephoneTypeResponse.TelephoneTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objTelephoneTypeRequest.Audit.Session, objTelephoneTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetTelephoneType(objTelephoneTypeRequest.Audit.Session, objTelephoneTypeRequest.Audit.Transaction, objTelephoneTypeRequest.ContractId); });
            }

            return objTelephoneTypeResponse;
            
        }

        /// <summary>
        /// Método para obtener tipo de documento.
        /// </summary>
        /// <param name="objDocumentTypeResquest">No contiene información.</param>
        /// <returns>Devuelve objeto DocumentTypeResponse con información de tipo de documento.</returns>
        public static Entity.Common.GetDocumentType.DocumentTypeResponse GetDocumentType(Entity.Common.GetDocumentType.DocumentTypeResquest objDocumentTypeResquest)
        {
            string strCodCargaDdl = "";

            try
            {
                strCodCargaDdl = KEY.AppSettings("strCodCargaDdl");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDocumentTypeResquest.Audit.Session, objDocumentTypeResquest.Audit.Transaction, ex.Message);
            }

            Entity.Common.GetDocumentType.DocumentTypeResponse objDocumentTypeResponse = new Entity.Common.GetDocumentType.DocumentTypeResponse()
            {
                DocumentTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objDocumentTypeResquest.Audit.Session, objDocumentTypeResquest.Audit.Transaction, () => { return Data.Dashboard.Common.GetDocumentType(objDocumentTypeResquest.Audit.Session, objDocumentTypeResquest.Audit.Transaction, strCodCargaDdl); })
            };

            return objDocumentTypeResponse;
        }

        /// <summary>
        /// Método para obtener estado de la tecnologia 4G.
        /// </summary>
        /// <param name="oListGenericItem">Lista genérica con datos de HLR</param>
        /// <returns>Devuelve estado de la tecnología 4G</returns>
        public static string GetServicesState4G(Claro.Entity.AuditRequest objAudit, List<Entity.Dashboard.HLR> listHLR, string strError)
        {
            string strTecnologia4G = String.Empty;
            string strDescription = String.Empty;
            string strEpsStatus = String.Empty;
            string[] strParameter;
            int intFieldLength;
            string strFieldValue = String.Empty;
            string strFieldList = String.Empty;
            string strActivo = String.Empty;
            string strDesactivo = String.Empty;
            string strChrgCAUNR = String.Empty;
            string strParamHLRConsultaUDB = String.Empty;

            try
            {
                strActivo = KEY.AppSettings("strActivo");
                strDesactivo = KEY.AppSettings("strDesactivo");
                strChrgCAUNR = KEY.AppSettings("strChrgCAUNR");
                strParamHLRConsultaUDB = KEY.AppSettings("strParamHLRConsultaUDB");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            if (string.Equals(strError, Claro.SIACU.Constants.ZeroNumber, StringComparison.OrdinalIgnoreCase) && (listHLR != null && listHLR.Count > Claro.Constants.NumberZero))
            {
                foreach (Entity.Dashboard.HLR item in listHLR)
                {
                    strDescription = item.Code;
                    if (!string.IsNullOrEmpty(strDescription) && strDescription != strParamHLRConsultaUDB)
                    {
                        strParameter = strDescription.Split('/');
                        intFieldLength = strParameter.Length;
                        strFieldValue = strParameter.GetValue(intFieldLength - Claro.Constants.NumberOne).ToString();
                        strFieldList = strParameter.GetValue(Claro.Constants.NumberOne).ToString();

                        if (intFieldLength > Claro.Constants.NumberZero && strFieldList == Claro.SIACU.Constants.ZmnfList && strFieldValue == Claro.SIACU.Constants.EpsStatusPar && strEpsStatus == String.Empty)
                        {
                            strEpsStatus = item.Description;
                        }
                    }

                }
            }

            switch (strEpsStatus)
            {
                case Claro.SIACU.Constants.Granted:
                    strTecnologia4G = strActivo;
                    break;
                case Claro.SIACU.Constants.Denied_:
                    strTecnologia4G = strDesactivo;
                    break;
                default:
                    strTecnologia4G = strChrgCAUNR;
                    break;
            }
            return strTecnologia4G;
        }

        public static Entity.Common.GetFlatCode.FlatCodeResponse GetFlatCode(Entity.Common.GetFlatCode.FlatCodeRequest objRequest)
        {
            Entity.Common.GetFlatCode.FlatCodeResponse objResponse = new Entity.Common.GetFlatCode.FlatCodeResponse
            {
                FlatCode = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetFlatCode(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ContractId); })
            };

            return objResponse;
        }

        /// <summary>
        /// Método que permite validar si el SIM soporta las tecnologías VoLTE y VoWIFI, es decir, su versión es > a 10.02.
        /// </summary>
        /// <param name="objAudit">Objeto auditoria</param>
        /// <param name="strSerie">Serie</param>
        /// <param name="strMssg">Mensaje</param>
        /// <returns>Devuelve un entero que especifica si el sim soporta o no la tecnología</returns>
        /// <returns>PROY-31249</returns>
        public static int ConsultarTecVoLTE(Claro.Entity.AuditRequest objAudit, string strSerie,  out string strMssg)
        {
            
            int strCodRetorno = -1;
            strMssg = "";
            
            Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTERequest oBEConsultarTecVoLTERequest = new Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTERequest();
            
            oBEConsultarTecVoLTERequest.Audit = objAudit;
            oBEConsultarTecVoLTERequest.serieVOLTE = strSerie;

            Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTEResponse oConsultarTecVoLTEResponse;
            try
            {
                oConsultarTecVoLTEResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetConsultarTecVoLTE.ConsultarTecVoLTEResponse>(objAudit.Session, objAudit.Transaction, () => { return Data.Dashboard.Common.ConsultarTecVoLTE(oBEConsultarTecVoLTERequest); });

                if (oConsultarTecVoLTEResponse != null)
                {
                    strMssg = oConsultarTecVoLTEResponse.mensajeResultado;
                    Claro.Web.Logging.Info(objAudit.Session, objAudit.Transaction, String.Format("codigoMaterial: {0}, existeChip: {1}, autenticaVOLTE: {2}, codigoResultado: {3}, mensajeResultado: {4}", oConsultarTecVoLTEResponse.codigoMaterial, oConsultarTecVoLTEResponse.existeChip, oConsultarTecVoLTEResponse.autenticaVOLTE, oConsultarTecVoLTEResponse.codigoResultado, oConsultarTecVoLTEResponse.mensajeResultado));
                    if (oConsultarTecVoLTEResponse.codigoResultado.Equals("0"))
                    {
                        strCodRetorno = oConsultarTecVoLTEResponse.autenticaVOLTE == Claro.Constants.LetterS ? 0 : 1;
                    }
                    else
                    {
                        if (oConsultarTecVoLTEResponse.codigoResultado.Equals("1"))
                        {
                            strCodRetorno = 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                strCodRetorno = -3;
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, e.Message);
            }
            return strCodRetorno;
        }

        /// <summary>
        /// Método que permite validar si los servicios VoLTE y VoWIFI están aprovisionados
        /// </summary>
        /// <param name="objAudit">Información</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strActionId">ID Accion</param>
        /// <param name="boolActSrv">Servicio activo</param>
        /// <returns>Devuelve el estado del servicio</returns>
        public static int ConsultarAprovisionamientoVoLTEoWifi(Claro.Entity.AuditRequest objAudit, string strPhoneNumber, string strActionId, out bool boolActSrv)
        {
            int strCodRetorno = -1;
            
            string strFieldName = "";
            string strFieldList = String.Empty;
            string strVo = String.Empty;
            string strProv = String.Empty;
            bool boolstrVoWifi = false;
            bool boolstrVoWifiNon3GPP = false;
            string strVoWifiCampo = String.Empty;
            string strVoWifiValor = String.Empty;
            string strVoWifiNon3GPPCampo = String.Empty;
            string strVoWifiNon3GPPValor = String.Empty;
            boolActSrv = false;
            try
            {
                strFieldName = KEY.AppSettings("strParamMSISDNConectorUDB");
                strVo = Constants.Vo;
                strProv = Constants.Prov;
                strFieldList = Constants.ListNameSendList;
                strVoWifiCampo = Constants.VoWifiField;
                strVoWifiValor = Constants.VoWifiValue;
                strVoWifiNon3GPPCampo = Constants.VoWifiNon3GPPField;
                strVoWifiNon3GPPValor = Constants.VoWifiNon3GPPValue;
            }
            catch (Exception ex)
            {
                strCodRetorno = -2;
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                return strCodRetorno;
            }

            try
            {
                Entity.Common.GetConsultaUDB.ConsultaUDBResponse oConsultaUDBResponse = new Entity.Common.GetConsultaUDB.ConsultaUDBResponse();
                Entity.Common.GetConsultaUDB.ConsultaUDBRequest oConsultaUDBRequest = new Entity.Common.GetConsultaUDB.ConsultaUDBRequest();
                oConsultaUDBRequest.Audit = objAudit;
                oConsultaUDBRequest.oAccionRequest  = new Entity.Common.GetConsultaUDB.AccionUDB();
                oConsultaUDBRequest.oAccionRequest.idAccion = strActionId;
                oConsultaUDBRequest.oAccionRequest.lstParametro = new List<Entity.Common.GetConsultaUDB.ListaParametro>();
                
                Entity.Common.GetConsultaUDB.ListaParametro oListaParametro = new Entity.Common.GetConsultaUDB.ListaParametro();
                oListaParametro.nombreLista = strFieldList;
                oListaParametro.lstParametro = new List<ItemOpcional>();
                oListaParametro.lstParametro.Add(new ItemOpcional() { campo = strFieldName, valor = strPhoneNumber });
                oConsultaUDBRequest.oAccionRequest.lstParametro.Add(oListaParametro);

                oConsultaUDBResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetConsultaUDB.ConsultaUDBResponse>(objAudit.Session, objAudit.Transaction, () => { return Data.Dashboard.Common.GetConsultaUDB(oConsultaUDBRequest); });
                if(oConsultaUDBResponse != null && oConsultaUDBResponse.oAuditResponse != null)
                {
                    if (string.Equals(oConsultaUDBResponse.oAuditResponse.codigoRespuesta, "0"))
                    {
                        strCodRetorno = 0;

                        if (oConsultaUDBResponse.oAccionResponse != null)
                        {
                            Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, string.Format("ID. Accion: {0}", oConsultaUDBResponse.oAccionResponse.idAccion));

                            if (oConsultaUDBResponse.oAccionResponse.lstParametro != null)
                            {
                                for (int i = 0; i < oConsultaUDBResponse.oAccionResponse.lstParametro.Count; i++)
                                {
                                    if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].nombreLista == strActionId)
                                    {
                                        if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro != null)
                                        {
                                            //aquì debo validar el IDACCION
                                            if (strActionId == KEY.AppSettings("strIdAccionConsultaServicioVoLTE")) 
                                            {
                                                for (int j = 0; j < oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro.Count; j++)
                                                {
                                                    if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro[j].campo == strVo)
                                                    {
                                                        if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro[j].valor == strProv)
                                                        {
                                                            boolActSrv = true;
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            if (strActionId == KEY.AppSettings("strIdAccionConsultaServicioVoWIFI"))
                                            {
                                                for (int j = 0; j < oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro.Count; j++)
                                                {
                                                    if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro[j].campo == strVoWifiCampo)
                                                    {
                                                        if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro[j].valor == strVoWifiValor)
                                                        {
                                                            boolstrVoWifi = true;
                                                        }
                                                    }
                                                    if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro[j].campo == strVoWifiNon3GPPCampo)
                                                    {
                                                        if (oConsultaUDBResponse.oAccionResponse.lstParametro[i].lstParametro[j].valor == strVoWifiNon3GPPValor)
                                                        {
                                                            boolstrVoWifiNon3GPP = true;
                                                        }
                                                    }
                                                }
                                                boolActSrv = boolstrVoWifi && boolstrVoWifiNon3GPP;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
            catch(Exception e){
                strCodRetorno = -2;
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, e.Message);
            }
            
            return strCodRetorno;
        }

        /// <summary>
        /// Método para obtener tipo de cac o dac.
        /// </summary>
        /// <param name="objCacDacTypeRequest">No contiene información.</param>
        /// <returns>Devuelve objeto CacDacTypeResponse con los tipos de cac o dac.</returns>
        public static Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse GetDescriptions(Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsRequest objGetDescriptionsRequest)
        {
            Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse objGetDescriptionsResponse = null;

            try
            {
                objGetDescriptionsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse>(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, () =>
                   {
                      return Data.Dashboard.Common.GetDescriptions(objGetDescriptionsRequest);
                   });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, ex.Message);
            }

            //Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse objGetDescriptionsResponse = new Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse();
            //objGetDescriptionsResponse = Claro.Web.Logging.ExecuteMethod<Entity.Common.GetParamaterClarify.GetDescriptions.GetDescriptionsResponse>(objGetDescriptionsRequest.Audit.Session, objGetDescriptionsRequest.Audit.Transaction, ()
            //    => { return Data.Dashboard.Common.GetDescriptions(objGetDescriptionsRequest); });

            return objGetDescriptionsResponse;
        }

       

    }
}
