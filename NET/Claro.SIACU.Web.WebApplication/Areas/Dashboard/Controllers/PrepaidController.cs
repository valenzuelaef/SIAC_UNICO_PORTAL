using Claro.SIACU.Web.WebApplication.DashboardService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using HELPER_CALL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataCallHelper;
using HELPER_RELOAD = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper;
using KEY = Claro.ConfigurationManager;
using MODELS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{

    public class PrepaidController : Controller
    {
        public JsonResult GetValidateTelephoneCustomer(string strIdSession, string strTelephone, bool isTFI, string strProviderID, string strCodResponde, string strCustomerCode)
        {
            string strResponse = "", strMessage = "", strShowPopup = "", strShowData = "";

            PrepaidService.ValidateTelephoneResponsePrePaid objValidateTelephoneResponse;
            PrepaidService.ValidateTelephoneRequestPrePaid objValidateTelephoneRequest = new PrepaidService.ValidateTelephoneRequestPrePaid()
            {
                Telephone = strTelephone,
                TFI = isTFI,
                ProviderID = strProviderID,
                CodigoResponse = strCodResponde,
                CustomerCode = strCustomerCode,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objValidateTelephoneResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.ValidateTelephoneResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetValidateTelephone(objValidateTelephoneRequest); });
            }
            catch (Exception ex)
            {
                objValidateTelephoneResponse = null;
                Claro.Web.Logging.Error(strIdSession, objValidateTelephoneRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objValidateTelephoneRequest.audit.transaction);
            }

            if (objValidateTelephoneResponse != null)
            {
                strResponse = objValidateTelephoneResponse.Cadena;
                string[] parts = strResponse.Split('|');

                strMessage = parts[0].ToString();
                strShowPopup = parts[1].ToString();
                strShowData = parts[2].ToString();
            }

            return Json(new { message = strMessage, showPopud = strShowPopup, showData = strShowData });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataCustomer()
        {
            return PartialView();
        }

        public JsonResult GetDataCustomer(string strIdSession, string strCustomerCode, string strTelephone)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataCustomerModel objDataCustomer = null;
            PrepaidService.CustomerPrePaid oCustomer;
            PrepaidService.PreviousCustomerResponsePrePaid objPreviousCustomerResponse;
            PrepaidService.PreviousCustomerRequestPrePaid objPreviousCustomerRequest = new PrepaidService.PreviousCustomerRequestPrePaid
             {
                 Telephone = strTelephone,
                 Account = "",
                 ContactId = strCustomerCode,
                 FlagRegistry = Claro.Constants.NumberOneString,
                 IpApplication = App_Code.Common.GetApplicationIp(),
                 ApplicationName = App_Code.Common.GetApplicationName(),
                 UserApplication = App_Code.Common.CurrentUser
             };

            try
            {
                objPreviousCustomerRequest.audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession);
                objPreviousCustomerRequest.IdTransaction = objPreviousCustomerRequest.audit.transaction;
                objPreviousCustomerResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.PreviousCustomerResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetPreviousCustomer(objPreviousCustomerRequest); });
            }
            catch (Exception ex)
            {
                objPreviousCustomerResponse = null;
                Claro.Web.Logging.Error(strIdSession, objPreviousCustomerRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objPreviousCustomerRequest.audit.transaction);
            }

            if (objPreviousCustomerResponse != null && objPreviousCustomerResponse.objCustomer != null)
            {
                oCustomer = objPreviousCustomerResponse.objCustomer;
                objDataCustomer = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataCustomerModel()
                {
                    Name = String.IsNullOrEmpty(oCustomer.NOMBRES) ? "" : oCustomer.NOMBRES,
                    Lastname = String.IsNullOrEmpty(oCustomer.APELLIDOS) ? "" : oCustomer.APELLIDOS,
                    NumberDocument = String.IsNullOrEmpty(oCustomer.NRO_DOC) ? "" : oCustomer.NRO_DOC,
                    TypeDocument = String.IsNullOrEmpty(oCustomer.TIPO_DOC) ? "" : oCustomer.TIPO_DOC,
                    Address = String.IsNullOrEmpty(oCustomer.DOMICILIO) ? "" : oCustomer.DOMICILIO,
                    State = String.IsNullOrEmpty(oCustomer.ESTADO_CONTACTO) ? "" : oCustomer.ESTADO_CONTACTO,
                    Segment = String.IsNullOrEmpty(oCustomer.SEGMENTO) ? "" : oCustomer.SEGMENTO,
                    TelephoneCustomer = String.IsNullOrEmpty(oCustomer.TELEFONO) ? "" : oCustomer.TELEFONO,
                    Membership = oCustomer.AFILIACION,
                    Modality = String.IsNullOrEmpty(oCustomer.MODALIDAD) ? "" : oCustomer.MODALIDAD,
                    Sex = String.IsNullOrEmpty(oCustomer.SEXO) ? "" : oCustomer.SEXO,
                    DateBirth = String.IsNullOrEmpty(oCustomer.FECHA_NAC) ? "" : DateTime.Parse(oCustomer.FECHA_NAC).ToShortDateString(),
                    PlaceBirth = String.IsNullOrEmpty(oCustomer.LUGAR_NACIMIENTO_DES) ? "" : oCustomer.LUGAR_NACIMIENTO_DES,
                    TelephoneReference = String.IsNullOrEmpty(oCustomer.TELEF_REFERENCIA) ? "" : oCustomer.TELEF_REFERENCIA,
                    StateCivil = String.IsNullOrEmpty(oCustomer.ESTADO_CIVIL) ? "" : oCustomer.ESTADO_CIVIL,
                    Fax = String.IsNullOrEmpty(oCustomer.FAX) ? "" : oCustomer.FAX,
                    Email = String.IsNullOrEmpty(oCustomer.EMAIL) ? "" : oCustomer.EMAIL,
                    EmailConfirmation = String.IsNullOrEmpty(oCustomer.EMAIL_CONFIRMACION) ? "" : oCustomer.EMAIL_CONFIRMACION,
                    Ocupation = String.IsNullOrEmpty(oCustomer.OCUPACION) ? "" : oCustomer.OCUPACION,
                    Position = String.IsNullOrEmpty(oCustomer.CARGO) ? "" : oCustomer.CARGO,
                    Role = String.IsNullOrEmpty(oCustomer.ROL_CONTACTO) ? "" : oCustomer.ROL_CONTACTO,
                    ReasonRegistry = String.IsNullOrEmpty(oCustomer.MOTIVOREGISTRO) ? "" : oCustomer.MOTIVOREGISTRO,
                    Urbanization = String.IsNullOrEmpty(oCustomer.URBANIZACION) ? "" : oCustomer.URBANIZACION,
                    District = String.IsNullOrEmpty(oCustomer.DISTRITO) ? "" : oCustomer.DISTRITO,
                    PostalCode = String.IsNullOrEmpty(oCustomer.ZIPCODE) ? "" : oCustomer.ZIPCODE,
                    City = String.IsNullOrEmpty(oCustomer.CIUDAD) ? "" : oCustomer.CIUDAD,
                    Department = String.IsNullOrEmpty(oCustomer.DEPARTAMENTO) ? "" : oCustomer.DEPARTAMENTO,
                    Reference = String.IsNullOrEmpty(oCustomer.REFERENCIA) ? "" : oCustomer.REFERENCIA,
                    BannerMessage = String.IsNullOrEmpty(oCustomer.BANNER_MESSAGE) ? "" : oCustomer.BANNER_MESSAGE,
                    CustomerCode = String.IsNullOrEmpty(oCustomer.OBJID_CONTACTO) ? "" : oCustomer.OBJID_CONTACTO,
                    PermissionInteraction = String.IsNullOrEmpty(oCustomer.INTERACCION) ? "" : oCustomer.INTERACCION,
                    ContingencyClarify = String.IsNullOrEmpty(oCustomer.CONTINGENCIACLARIFY) ? "" : oCustomer.CONTINGENCIACLARIFY,
                    BlackList = String.IsNullOrEmpty(oCustomer.BLACKLIST) ? "" : oCustomer.BLACKLIST,
                    ProductType = oCustomer.TipoProductoTelefono

                };
            }
            return Json(objDataCustomer);
        }

        public PostpaidService.HeaderRequest getHeaderRequestAppMiClaro(string operation)
        {
            return new PostpaidService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivo"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("moduloSiac"),
                msgType = KEY.AppSettings("msgType"),
                operation = operation,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIpServer()
            };
        }

        public ActionResult DataCall(string strIdSession, string strTelephoneCustomer,bool isTFI)
        {
            List<HELPER_CALL.DataCall> listDataCallModel = new List<HELPER_CALL.DataCall>();
            PrepaidService.CallsResponsePrePaid objCallsResponse = null;
            string strnewMonth = ("00" + DateTime.Now.Month.ToString()).Substring(("00" + DateTime.Now.Month.ToString()).Length - 2);
            string strStartDate = "01/" + strnewMonth + "/" + DateTime.Now.Year;
            string strEndDate = DateTime.Now.ToString("dd/MM/yyyy");
            PrepaidService.CallsRequestPrePaid objCallsRequest = new PrepaidService.CallsRequestPrePaid()
            {
                Msisdn = App_Code.Common.GetPhoneNumber(strTelephoneCustomer),
                StartDate = strStartDate,
                EndDate = strEndDate,
                TypeQuery = Claro.Constants.NumberOneString,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession),
                TrafficType = "",
                FlagVisualize = true,
                isTFI=isTFI
            };

            try
            {
                objCallsResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.CallsResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetCalls(objCallsRequest); });
            }
            catch (Exception ex)
            {
                objCallsResponse = null;
                Claro.Web.Logging.Error(strIdSession, objCallsRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objCallsRequest.audit.transaction);
            }


            if (objCallsResponse.listCall != null && objCallsResponse.listCall.Count > 0)
            {
                listDataCallModel = new List<HELPER_CALL.DataCall>();
                foreach (PrepaidService.CallPrePaid objCall in objCallsResponse.listCall)
                {
                    listDataCallModel.Add(new HELPER_CALL.DataCall()
                    {
                        CallDateTime = System.Convert.ToDateTime(objCall.CallDateTime).ToString("dd/MM/yyyy hh:mm tt"),
                        CallTelephoneDestination = objCall.CallTelephoneDestination,
                        CallTypeTraffic = objCall.CallTypeTraffic,
                        CallDuration = objCall.CallDuration,
                        CallUptake = objCall.CallUptake,
                        CallBoughtPresented = objCall.CallBoughtPresented,
                        CallBalance = objCall.CallBalance,
                        CallBagId = objCall.CallBagId,
                        CallDescription = objCall.CallDescription,
                        CallPlan = objCall.CallPlan,
                        CallPromotion = objCall.CallPromotion,
                        CallDestination = objCall.CallDestination,
                        CallOperator = objCall.CallOperator,
                        CallCobroGroup = objCall.CallCobroGroup,
                        CallNetworkType = objCall.CallNetworkType,
                        CallImei = objCall.CallImei,
                        CallRoaming = objCall.CallRoaming,
                        CallTariffArea = objCall.CallTariffArea
                    });
                }
            }
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataCallModel objDataCallModelResponse = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataCallModel()
            {
                listDataCallModel = listDataCallModel,
                CallTelephoneCustomer = strTelephoneCustomer,
                DateFromCall = strStartDate,
                DateToCall = DateTime.Now.ToString("dd/MM/yyyy"),
                Transaction = objCallsRequest.audit.transaction,
                Headers = ConfigurationManager.AppSettings("strGridConsultaLlamada")
            };

            return View(objDataCallModelResponse);
        }

        public ActionResult DataReload(string strIdSession, string strTelephoneCustomer, string strMovementType, string strcreditoDebito)
        {
            string strnewMonth = ("00" + DateTime.Now.Month.ToString()).Substring(("00" + DateTime.Now.Month.ToString()).Length - 2);
            string strStartDate = "01/" + strnewMonth + "/" + DateTime.Now.Year;
            string strEndDate = DateTime.Now.ToString("dd/MM/yyyy");
            List<HELPER_RELOAD.DataReload> listReloadModel;
            PrepaidService.RechargesResponsePrePaid objRechargesResponse;
            strTelephoneCustomer = App_Code.Common.GetPhoneNumber(strTelephoneCustomer);

            PrepaidService.RechargesRequestPrePaid objRechargesRequest = new PrepaidService.RechargesRequestPrePaid
            {
                Msisdn = strTelephoneCustomer,
                StartDate = strStartDate,
                EndDate = strEndDate,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession),
                strcreditoDebito = strcreditoDebito,
                strMovementType = strMovementType,
                strtypeQuery = Claro.Constants.NumberOneString
            };

            try
            {
                objRechargesResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.RechargesResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetRecharges(objRechargesRequest); });
            }
            catch (Exception ex)
            {
                objRechargesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRechargesRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRechargesRequest.audit.transaction);
            }
            listReloadModel = new List<HELPER_RELOAD.DataReload>();

            if (objRechargesResponse != null && objRechargesResponse.listRecharge != null)
            {
                foreach (PrepaidService.RechargePrePaid objRecharge in objRechargesResponse.listRecharge)
                {
                    listReloadModel.Add(new HELPER_RELOAD.DataReload()
                    {
                        DateTimeOperation = System.Convert.ToDateTime(objRecharge.FECHARECARGA).ToString("dd/MM/yyyy hh:mm tt"),
                        Kindmovement = objRecharge.TIPOMOVIMIENTO,
                        kindReload = objRecharge.TIPORECARGA,
                        NominalAmount = objRecharge.MONTOEFECTIVO,
                        Balance = objRecharge.SALDO,
                        Credit = objRecharge.CREDDEBI,
                        Bag = objRecharge.BOLSA,
                        KindBalance = objRecharge.TIPOSALDO,
                        Description = objRecharge.DESCRIPCION,
                        Detail = objRecharge.DETALLE
                    });
                }
            }

            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataReloadModel objReloadModelResponse = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataReloadModel()
            {
                listDataReloadModel = listReloadModel,
                ReloadTelephoneCustomer = strTelephoneCustomer,
                ReloadDateFrom = Claro.Constants.NumberZeroOne_ + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString(),
                ReloadDateTo = DateTime.Now.ToString("dd/MM/yyyy"),
                Transaction = strIdSession,
                Headers = KEY.AppSettings("strGridConsulta")
            };

            return View(objReloadModelResponse);
        }

        public Models.Prepaid.DataCustomerModel DataCustomerModel
            (DashboardService.CustomerPrePaid objCustomer, string strSearchValue)
        {

            MODELS.Prepaid.DataCustomerModel objDataCustomer = null;
            var isAppMiClaro = false;
            var AppMiClaroVersion = string.Empty;
            var AppMiClaroLastDate = string.Empty;

            if (objCustomer != null)
            {
                MODELS.Prepaid.DataServiceModel objDataService = null;
                MODELS.PrepaidDataService.PortabilityModel objPortability = null;
                List<MODELS.PrepaidDataService.AccountModel> oListAccount = null;
                List<MODELS.PrepaidDataService.TrioModel> oListTrio = null;
                List<MODELS.PrepaidDataService.BalanceModel> oListBalance = null;

                //call new REST Service
                Claro.Web.Logging.Error("", "", "INICIO LLAMADA SERVICIO REST");
                var objRequest = new PostpaidService.MiClaroAppRequest();
                var objResponse = new PostpaidService.MiClaroAppResponse();
                objRequest.MessageRequest = new PostpaidService.MessageRequestAppMiclaro();
                objRequest.MessageRequest.Header = new PostpaidService.HeadersRequest();
                objRequest.MessageRequest.Header.HeaderRequest = new PostpaidService.HeaderRequest();
                objRequest.MessageRequest.Header.HeaderRequest = getHeaderRequestAppMiClaro("consultaAppMiclaro");
                objRequest.audit = new PostpaidService.AuditRequest()
                {
                    applicationName = "",
                    ipAddress = App_Code.Common.GetApplicationIp(),
                    Session = "",
                    transaction = "",
                    userName = App_Code.Common.CurrentUser
                };
                objRequest.MessageRequest.Body = new PostpaidService.BodyAppMiClaroRequest();
                objRequest.MessageRequest.Body.numeroLinea = strSearchValue;//validar, era strValueSearch

                Claro.Web.Logging.Error("", "", string.Format("objRequest=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objRequest)));
                try
                {
                    objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.MiClaroAppResponse>(() =>
                    {
                        return (new PostpaidService.PostpaidServiceClient()).GetMiClaroApp(objRequest);
                    });
                    if (objResponse.MessageResponse.body.codigoRespuesta == "0")
                    {
                        isAppMiClaro = objResponse.MessageResponse.body.datosAppClaro.flagAppClaro == "1" ? true : false;
                        AppMiClaroVersion = objResponse.MessageResponse.body.datosAppClaro.version;
                        AppMiClaroLastDate = Convert.ToDate(objResponse.MessageResponse.body.datosAppClaro.ultimaTransaccion).ToString("dd/MM/yyyy");
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error("", "", string.Format("Error GetMiClaroApp : Message=>{0}, StackTrace=>{1}", ex.Message, ex.StackTrace));
                }


                Claro.Web.Logging.Error("", "", "FIN LLAMADA SERVICIO REST");
                //end call new REST Service

                if (objCustomer.oService != null && objCustomer.oService.oPortability != null)
                {
                    objPortability = new MODELS.PrepaidDataService.PortabilityModel()
                    {
                        NumberRequest = objCustomer.oService.oPortability.NUMERO_SOLICITUD,
                        ProcessStatus = objCustomer.oService.oPortability.ESTADO_PROCESO,
                        DescriptionProcessStatus = objCustomer.oService.oPortability.DESCRPCION_ESTADO_PROCESO,
                        RegistrationDate = objCustomer.oService.oPortability.FECHA_REGISTRO.ToString(),
                        TypePortability = objCustomer.oService.oPortability.TIPO_PORTABILIDAD,
                        ExecutionDate = objCustomer.oService.oPortability.FECHA_EJECUCION.ToString(),
                        TransferorCode = objCustomer.oService.oPortability.CODIGO_OPERADOR_CEDENTE,
                        ReceivingOperatorCode = objCustomer.oService.oPortability.CODIGO_OPERADOR_RECEPTOR,
                        Answer = objCustomer.oService.oPortability.RESPUESTA,
                        TypeProcessDate = objCustomer.oService.oPortability.TIPO_PROCESO_FECHA,
                        TypeProcessOperator = objCustomer.oService.oPortability.TIPO_PROCESO_OPERADOR,
                        Operator = objCustomer.oService.oPortability.OPERADOR
                    };

                }
                else
                {
                    objPortability = new MODELS.PrepaidDataService.PortabilityModel()
                    {
                        Answer = ""
                    };
                }

                if (objCustomer.oService != null)
                {
                    if (objCustomer.oService.listAccounts != null && objCustomer.oService.listAccounts.Count > 0)
                    {
                        oListAccount = new List<MODELS.PrepaidDataService.AccountModel>();

                        foreach (AccountPrepaid item in objCustomer.oService.listAccounts)
                        {
                            if (item != null && !string.IsNullOrEmpty(item.Nombre) && !string.IsNullOrEmpty(item.Orden))
                                oListAccount.Add(new Models.PrepaidDataService.AccountModel()
                                {
                                    Name = item.Nombre,
                                    Balance = item.Saldo,
                                    ExpirationDate = item.FechaExpiracion,
                                    Order = item.Orden
                                });
                        }
                        oListAccount.Sort((x, y) => int.Parse(x.Order).CompareTo(int.Parse(y.Order)));

                    }

                    if (objCustomer.oService.listBalance != null && objCustomer.oService.listBalance.Count > 0)
                    {
                        oListBalance = new List<MODELS.PrepaidDataService.BalanceModel>();

                        foreach (BalancePrePaid item in objCustomer.oService.listBalance)
                        {
                            if (!String.IsNullOrEmpty(item.Expiration))
                            {
                                if (DateTime.Parse(item.Expiration) >= DateTime.Now)
                                {
                                        oListBalance.Add(new Models.PrepaidDataService.BalanceModel()
                                        {
                                               Balance = item._Balance,
                                               CommercialName = item.CommercialName,
                                               ConstancyOrder = item.ConstancyOrder,
                                               Destination = item.Destination,
                                               Expiration = item.Expiration,
                                               MovementTypeName = item.MovementTypeName,
                                               Name = item.Name,
                                               NameCode = item.NameCode,
                                               Order = item.Order,
                                               OtherIndicator = item.OtherIndicator,
                                               PromotionalBonus = item.PromotionalBonus,
                                               Unity = item.Unity,
                                               UnityIndicator = item.UnityIndicator,
                                         });
                                }
                            }else
                            {
                                                oListBalance.Add(new Models.PrepaidDataService.BalanceModel()
                                           {
                                               Balance = item._Balance,
                                               CommercialName = item.CommercialName,
                                               ConstancyOrder = item.ConstancyOrder,
                                               Destination = item.Destination,
                                               Expiration = item.Expiration,
                                               MovementTypeName = item.MovementTypeName,
                                               Name = item.Name,
                                               NameCode = item.NameCode,
                                               Order = item.Order,
                                               OtherIndicator = item.OtherIndicator,
                                               PromotionalBonus = item.PromotionalBonus,
                                               Unity = item.Unity,
                                               UnityIndicator = item.UnityIndicator,
                                           });
                            }
                        }
                    }

                    if (objCustomer.oService.listTrio != null && objCustomer.oService.listTrio.Count > 0)
                    {
                        oListTrio = new List<MODELS.PrepaidDataService.TrioModel>();
                        foreach (TrioPrepaid item in objCustomer.oService.listTrio)
                        {
                            oListTrio.Add(new Models.PrepaidDataService.TrioModel()
                            {
                                Detail = item.Codigo,
                                Number = item.Descripcion
                            });
                        }
                    }

                    if (objCustomer.oService.NroCelular != null)
                    {
                        objDataService = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataServiceModel()
                        {
                            NumberCellphone = objCustomer.oService.NroCelular,
                            BalancePrincipal = objCustomer.oService.SaldoPrincipal,
                            DateExpirationBalance = objCustomer.oService.FechaExpiracionSaldo,
                            StateLine = objCustomer.oService.StatusLinea,
                            FailedAttemptsRefills = objCustomer.oService.CambiosTriosGratis,
                            ExchangeRatesMade = objCustomer.oService.CambiosTarifaGratis,
                            PlanTariff = objCustomer.oService.PlanTarifario,
                            PlanRate = objCustomer.oService.PlanTarifario,
                            ProviderID = objCustomer.oService.ProviderID,
                            BalanceMinutesSelect = objCustomer.oService.SaldoMinutosSelect,
                            DateExpirationSelect = objCustomer.oService.FechaExpSelect,
                            IsSelect = objCustomer.oService.IsSelect,
                            DateActivation = objCustomer.oService.FecActivacion,
                            DateExpirationLine = objCustomer.oService.FecExpLinea,
                            SubscriberStatus = objCustomer.oService.SubscriberStatus,
                            CNTNumber = objCustomer.oService.CNTNumber,
                            IsCNTPossible = objCustomer.oService.IsCNTPossible,
                            NumberIMSI = objCustomer.oService.NroIMSI,
                            StatusIMSI = objCustomer.oService.StatusIMSI,
                            TypeTriation = objCustomer.oService.TipoTriacion,
                            Balance = objCustomer.oService.Saldo,
                            QuantityTrios = objCustomer.oService.NroFamAmigos,
                            IsTFI = objCustomer.oService.EsTFI,
                            oPortability = objPortability,
                            listAccount = oListAccount,
                            listBalance = GetListBalancePrepaid(oListBalance),
                            listTrio = oListTrio,
                            ContactType = objCustomer.oService.TipoContacto,
                            BondAmountRFA = objCustomer.oService.BondAmountRFA,
                            BondRFA = objCustomer.oService.BondRFA,
                            BalancePending = objCustomer.oService.SaldoPendiente,

                        };
                    }
                }

                if (objCustomer.TELEFONO != null || objCustomer.oService.NroCelular != null)
                {
                    StringBuilder sb = new StringBuilder();
                    objDataCustomer = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataCustomerModel()
                    {
                        Name = string.IsNullOrWhiteSpace(objCustomer.NOMBRES) ? "" : objCustomer.NOMBRES,
                        Lastname = string.IsNullOrWhiteSpace(objCustomer.APELLIDOS) ? "" : objCustomer.APELLIDOS,
                        FullName = sb.Append(string.IsNullOrWhiteSpace(objCustomer.NOMBRES) ? "" : objCustomer.NOMBRES).Append(" ").Append(string.IsNullOrWhiteSpace(objCustomer.APELLIDOS) ? "" : objCustomer.APELLIDOS).ToString(),
                        DNIRUC = string.IsNullOrWhiteSpace(objCustomer.NRO_DOC) ? "" : objCustomer.NRO_DOC,
                        NumberDocument = string.IsNullOrWhiteSpace(objCustomer.NRO_DOC) ? "" : objCustomer.NRO_DOC,
                        TypeDocument = string.IsNullOrWhiteSpace(objCustomer.TIPO_DOC) ? "" : objCustomer.TIPO_DOC,
                        Address = string.IsNullOrWhiteSpace(objCustomer.DOMICILIO) ? "" : objCustomer.DOMICILIO,
                        State = string.IsNullOrWhiteSpace(objCustomer.ESTADO_CONTACTO) ? "" : objCustomer.ESTADO_CONTACTO,
                        Segment = string.IsNullOrWhiteSpace(objCustomer.SEGMENTO) ? "" : objCustomer.SEGMENTO,
                        TelephoneCustomer = string.IsNullOrWhiteSpace(objCustomer.TELEFONO) ? "" : objCustomer.TELEFONO,
                        Membership = objCustomer.AFILIACION,
                        Modality = string.IsNullOrWhiteSpace(objCustomer.MODALIDAD) ? "" : objCustomer.MODALIDAD,
                        Sex = string.IsNullOrWhiteSpace(objCustomer.SEXO) ? "" : objCustomer.SEXO,
                        DateBirth = !string.IsNullOrEmpty(objCustomer.FECHA_NAC) ? DateTime.Parse(objCustomer.FECHA_NAC).ToShortDateString() : "",
                        PlaceBirth = string.IsNullOrWhiteSpace(objCustomer.LUGAR_NACIMIENTO_DES) ? "" : objCustomer.LUGAR_NACIMIENTO_DES,
                        TelephoneReference = string.IsNullOrWhiteSpace(objCustomer.TELEF_REFERENCIA) ? "" : objCustomer.TELEF_REFERENCIA,
                        StateCivil = string.IsNullOrEmpty(objCustomer.ESTADO_CIVIL) ? "" : objCustomer.ESTADO_CIVIL,
                        Fax = string.IsNullOrEmpty(objCustomer.FAX) ? "" : objCustomer.FAX,
                        Email = string.IsNullOrEmpty(objCustomer.EMAIL) ? "" : objCustomer.EMAIL,
                        EmailConfirmation = string.IsNullOrEmpty(objCustomer.EMAIL_CONFIRMACION) ? "" : objCustomer.EMAIL_CONFIRMACION,
                        Ocupation = string.IsNullOrEmpty(objCustomer.OCUPACION) ? "" : objCustomer.OCUPACION,
                        Position = string.IsNullOrEmpty(objCustomer.CARGO) ? "" : objCustomer.CARGO,
                        Role = string.IsNullOrEmpty(objCustomer.ROL_CONTACTO) ? "" : objCustomer.ROL_CONTACTO,
                        ReasonRegistry = string.IsNullOrEmpty(objCustomer.MOTIVOREGISTRO) ? "" : objCustomer.MOTIVOREGISTRO,
                        Urbanization = string.IsNullOrEmpty(objCustomer.URBANIZACION) ? "" : objCustomer.URBANIZACION,
                        District = string.IsNullOrEmpty(objCustomer.DISTRITO) ? "" : objCustomer.DISTRITO,
                        PostalCode = string.IsNullOrEmpty(objCustomer.ZIPCODE) ? "" : objCustomer.ZIPCODE,
                        City = string.IsNullOrEmpty(objCustomer.CIUDAD) ? "" : objCustomer.CIUDAD,
                        Department = string.IsNullOrEmpty(objCustomer.DEPARTAMENTO) ? "" : objCustomer.DEPARTAMENTO,
                        Reference = string.IsNullOrEmpty(objCustomer.REFERENCIA) ? "" : objCustomer.REFERENCIA,
                        Registered = objCustomer.FLAG_REGISTRADO.ToString(),
                        BannerMessage = string.IsNullOrEmpty(objCustomer.BANNER_MESSAGE) ? "" : objCustomer.BANNER_MESSAGE,
                        oDataService = objDataService,
                        CustomerCode = string.IsNullOrEmpty(objCustomer.OBJID_CONTACTO) ? "" : objCustomer.OBJID_CONTACTO,
                        PermissionInteraction = string.IsNullOrEmpty(objCustomer.INTERACCION) ? "" : objCustomer.INTERACCION,
                        ContingencyClarify = string.IsNullOrEmpty(objCustomer.CONTINGENCIACLARIFY) ? "" : objCustomer.CONTINGENCIACLARIFY,
                        SiteId = string.IsNullOrEmpty(objCustomer.CUENTA) ? "" : objCustomer.CUENTA,
                        Account = string.IsNullOrEmpty(objCustomer.CUENTA) ? "" : objCustomer.CUENTA,
                        Application = string.IsNullOrWhiteSpace(objCustomer.TipoProducto) ? "" : objCustomer.TipoProducto,
                        BlackList = string.IsNullOrWhiteSpace(objCustomer.BLACKLIST) ? "" : objCustomer.BLACKLIST,
                        ProductType = string.IsNullOrWhiteSpace(objCustomer.TipoProductoTelefono) ? "" : objCustomer.TipoProductoTelefono,
                        ContactCode = string.IsNullOrWhiteSpace(objCustomer.OBJID_CONTACTO) ? "" : objCustomer.OBJID_CONTACTO,
                        StateContact = string.IsNullOrWhiteSpace(objCustomer.ESTADO_CONTACTO) ? "" : objCustomer.ESTADO_CONTACTO
                    };
                }
            }
            objDataCustomer.IsAppMiclaro = isAppMiClaro;
            objDataCustomer.AppMiclaroVersion = AppMiClaroVersion;
            objDataCustomer.AppMiClaroLastDate = AppMiClaroLastDate;
            return objDataCustomer;
        }

        public List<MODELS.PrepaidDataService.BalanceModel> GetListBalancePrepaid(List<MODELS.PrepaidDataService.BalanceModel> oListBalance)
        {
            List<MODELS.PrepaidDataService.BalanceModel> ListBalance = null;
            try
            {
				if (oListBalance != null){
					if (oListBalance.Count > 0)
					{
						ListBalance = new List<MODELS.PrepaidDataService.BalanceModel>();
						foreach (var item in oListBalance)
						{
							string strUnity = ConfigurationManager.AppSettings("strUnityListBalance") != null ? ConfigurationManager.AppSettings("strUnityListBalance") :string.Empty;
							string strIlimitado = ConfigurationManager.AppSettings("strIlimitado") != null ? ConfigurationManager.AppSettings("strIlimitado") : string.Empty;
							string numIlimitado = ConfigurationManager.AppSettings("numIlimitado") != null ? ConfigurationManager.AppSettings("numIlimitado") : string.Empty;
							if (item.Unity.Equals(strUnity))
							{
								string balanceItem = string.Empty;
								if (item.Balance.Contains(":"))
								{
									balanceItem = item.Balance.Split(':')[0];
								}
								int balanceInt = 0;
								bool blnBalance = int.TryParse(balanceItem, out balanceInt);
								if (blnBalance)
								{
									if (balanceInt > Convert.ToInt(numIlimitado))
									{
										item.Balance = strIlimitado;
									}
								}
							}
							ListBalance.Add(new Models.PrepaidDataService.BalanceModel()
							{
								Balance = item.Balance,
								CommercialName = item.CommercialName,
								ConstancyOrder = item.ConstancyOrder,
								Destination = item.Destination,
								Expiration = item.Expiration,
								MovementTypeName = item.MovementTypeName,
								Name = item.Name,
								NameCode = item.NameCode,
								Order = item.Order,
								OtherIndicator = item.OtherIndicator,
								PromotionalBonus = item.PromotionalBonus,
								Unity = item.Unity,
								UnityIndicator = item.UnityIndicator,
							});
						}
					}
				}
                else
                {
                    ListBalance = new List<MODELS.PrepaidDataService.BalanceModel>();
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("GetListBalancePrepaid()", "Error:", ex.Message);
            }
            return ListBalance;
        }

        public ActionResult DataService(string strIdSession, bool Clear, Models.Prepaid.DataServiceModel objDataService)
        {
            Models.Prepaid.DataServiceModel objDataServiceModel = new MODELS.Prepaid.DataServiceModel();

            PrepaidService.ServiceResponsePrePaid objGetServiceResponse;
            PrepaidService.ServiceRequestPrePaid objGetServiceRequest = new PrepaidService.ServiceRequestPrePaid()
            {
                objService = Service(objDataService),
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            try
            {
                if (objGetServiceRequest.objService != null && !Clear)
                {
                    objGetServiceResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.ServiceResponsePrePaid>(() => { return new PrepaidService.PrepaidServiceClient().GetService(objGetServiceRequest); });
                    objDataServiceModel = DataServiceModel(objGetServiceResponse);
                    objDataServiceModel.RateMB = GetRateState(strIdSession, objDataServiceModel.NumberCellphone);
                    if (objDataServiceModel.listTrio == null)
                    {
                        objDataServiceModel.listTrio = new List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.TrioModel>();
                    }
                }
                if (Clear)
                {
                    objDataServiceModel = new MODELS.Prepaid.DataServiceModel();
                }
            }
            catch (Exception ex)
            {

                objDataServiceModel = new MODELS.Prepaid.DataServiceModel();
                objDataServiceModel.Transaction = objGetServiceRequest.audit.transaction;
                Claro.Web.Logging.Error(strIdSession, objGetServiceRequest.audit.transaction, ex.Message);
            }
            return PartialView(objDataServiceModel);
        }

        public PrepaidService.ServicePrePaid Service(Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataServiceModel oDataService)
        {
            PrepaidService.ServicePrePaid oService = null;

            if (oDataService != null)
            {
                PrepaidService.Portability oPortability = null;
                oService = new PrepaidService.ServicePrePaid();

                if (!string.IsNullOrEmpty(oDataService.NumberCellphone))
                {
                    oService.NroCelular = oDataService.NumberCellphone;
                    oService.SaldoPrincipal = oDataService.BalancePrincipal;
                    oService.FechaExpiracionSaldo = oDataService.DateExpirationBalance;
                    oService.StatusLinea = oDataService.StateLine;
                    oService.CambiosTriosGratis = oDataService.FailedAttemptsRefills;
                    oService.CambiosTarifaGratis = oDataService.ExchangeRatesMade;
                    oService.PlanTarifario = oDataService.PlanTariff;
                    oService.ProviderID = oDataService.ProviderID;
                    oService.SaldoMinutosSelect = oDataService.BalanceMinutesSelect;
                    oService.FechaExpSelect = oDataService.DateExpirationSelect;
                    oService.IsSelect = oDataService.IsSelect;
                    oService.FecActivacion = oDataService.DateActivation;
                    oService.FecExpLinea = oDataService.DateExpirationLine;
                    oService.SubscriberStatus = oDataService.SubscriberStatus;
                    oService.CNTNumber = oDataService.CNTNumber;
                    oService.IsCNTPossible = oDataService.IsCNTPossible;
                    oService.NroIMSI = oDataService.NumberIMSI;
                    oService.StatusIMSI = oDataService.StatusIMSI;
                    oService.TipoTriacion = oDataService.TypeTriation;
                    oService.Saldo = oDataService.Balance;
                    oService.NroFamAmigos = oDataService.QuantityTrios;
                    oService.EsTFI = oDataService.IsTFI;
                    oService.SaldoPendiente = oDataService.BalancePending;
                    if (oDataService.oPortability != null)
                    {
                        oPortability = new PrepaidService.Portability()
                        {
                            NUMERO_SOLICITUD = oDataService.oPortability.NumberRequest,
                            TIPO_PORTABILIDAD = oDataService.oPortability.TypePortability,
                            ESTADO_PROCESO = oDataService.oPortability.ProcessStatus,
                            DESCRPCION_ESTADO_PROCESO = oDataService.oPortability.DescriptionProcessStatus,
                            FECHA_REGISTRO = Convert.ToDate(oDataService.oPortability.RegistrationDate),
                            FECHA_EJECUCION = Convert.ToDate(oDataService.oPortability.ExecutionDate),
                            CODIGO_OPERADOR_CEDENTE = oDataService.oPortability.TransferorCode,
                            CODIGO_OPERADOR_RECEPTOR = oDataService.oPortability.ReceivingOperatorCode,
                            RESPUESTA = oDataService.oPortability.Answer,
                            TIPO_PROCESO_FECHA = oDataService.oPortability.TypeProcessDate,
                            TIPO_PROCESO_OPERADOR = oDataService.oPortability.TypeProcessOperator,
                            OPERADOR = oDataService.oPortability.Operator
                        };
                        oService.oPortability = oPortability;
                    }

                    if (oDataService.listAccount != null && oDataService.listAccount.Count > 0)
                    {
                        oService.listAccount = ListAccount(oDataService);
                    }

                    if (oDataService.listBalance != null && oDataService.listBalance.Count > 0)
                    {
                        oService.listBalance = ListBalance(oDataService);
                    }

                    if (oDataService.listTrio != null && oDataService.listTrio.Count > 0)
                    {
                        oService.listTrio = ListTrio(oDataService);
                    }
                }
            }
            return oService;
        }
        private List<PrepaidService.AccountPrepaid> ListAccount(Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataServiceModel oDataService)
        {
            List<PrepaidService.AccountPrepaid> oListAccount = new List<PrepaidService.AccountPrepaid>();
            foreach (Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.AccountModel item in oDataService.listAccount)
            {
                if (item != null && !string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Order))
                    oListAccount.Add(new PrepaidService.AccountPrepaid()
                    {
                        Nombre = item.Name,
                        Saldo = item.Balance,
                        FechaExpiracion = item.ExpirationDate,
                        Orden = item.Order
                    });
            }

            return oListAccount;
        }

        private List<PrepaidService.BalancePrePaid> ListBalance(Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataServiceModel oDataService)
        {
            List<PrepaidService.BalancePrePaid> oListBalance = new List<PrepaidService.BalancePrePaid>();
            List<MODELS.PrepaidDataService.BalanceModel> ListBalanceModel = new List<MODELS.PrepaidDataService.BalanceModel>();
            ListBalanceModel = GetListBalancePrepaid(oDataService.listBalance);
            
            foreach (Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.BalanceModel item in ListBalanceModel)
            {
                if (item != null)
                    oListBalance.Add(new PrepaidService.BalancePrePaid()
                         {
                             _Balance = item.Balance,
                             CommercialName = item.CommercialName,
                             ConstancyOrder = item.ConstancyOrder,
                             Destination = item.Destination,
                             Expiration = item.Expiration,
                             MovementTypeName = item.MovementTypeName,
                             Name = item.Name,
                             NameCode = item.NameCode,
                             Order = item.Order,
                             OtherIndicator = item.OtherIndicator,
                             PromotionalBonus = item.PromotionalBonus,
                             Unity = item.Unity,
                             UnityIndicator = item.UnityIndicator,
                         });
            }
            return oListBalance;
        }

        private List<PrepaidService.TrioPrepaid> ListTrio(Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataServiceModel oDataService)
        {
            List<PrepaidService.TrioPrepaid> oListTrio = new List<PrepaidService.TrioPrepaid>();
            foreach (Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.TrioModel item in oDataService.listTrio)
            {
                if (item != null)
                {
                    oListTrio.Add(new PrepaidService.TrioPrepaid()
                    {
                        Codigo = item.Detail,
                        Descripcion = item.Number
                    });
                }
                else
                {
                    oListTrio = null;
                    break;
                }
            }

            return oListTrio;
        }

        private Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid.DataServiceModel DataServiceModel(PrepaidService.ServiceResponsePrePaid objGetServiceResponse)
        {
            MODELS.Prepaid.DataServiceModel oDataService = new MODELS.Prepaid.DataServiceModel();
            if (objGetServiceResponse != null)
            {
                oDataService.NumberCellphone = objGetServiceResponse.objService.NroCelular;
                oDataService.ProviderID = objGetServiceResponse.objService.ProviderID;
                oDataService.DateExpirationLine = objGetServiceResponse.objService.FecExpLinea;
                oDataService.IsTFI = objGetServiceResponse.objService.EsTFI;
                oDataService.BalancePrincipal = objGetServiceResponse.objService.SaldoPrincipal;
                oDataService.DateExpirationBalance = objGetServiceResponse.objService.FechaExpiracionSaldo;
                oDataService.FailedAttemptsRefills = objGetServiceResponse.objService.CambiosTriosGratis;
                oDataService.ExchangeRatesMade = objGetServiceResponse.objService.CambiosTarifaGratis;
                oDataService.Answer = objGetServiceResponse.objService.Respuesta;
                oDataService.Payment = objGetServiceResponse.objService.Pago;
                oDataService.StatusIMSI = objGetServiceResponse.objService.StatusIMSI;
                oDataService.DateActivation = objGetServiceResponse.objService.FecActivacion;
                oDataService.StateLine_2 = objGetServiceResponse.objService.EstadoLinea;
                oDataService.StateLine = objGetServiceResponse.objService.StatusLinea;
                oDataService.TypeTriation = objGetServiceResponse.objService.TipoTriacion;
                oDataService.ReasonBlocking = objGetServiceResponse.objService.MotivoBloqueo;
                oDataService.AlertBlocking = objGetServiceResponse.objService.AlertaBloqueo;
                oDataService.DescriptionTypeTriation = objGetServiceResponse.objService.DescriptionTypeTriation;
                oDataService.Segment = objGetServiceResponse.objService.Segmento;
                oDataService.ICCID = objGetServiceResponse.objService.ICCID;
                oDataService.BankingMobile = objGetServiceResponse.objService.BancaMovil;
                oDataService.PlanRate = objGetServiceResponse.objService.DescripcionPlan;
                oDataService.QuantityTrios = objGetServiceResponse.objService.NroFamAmigos;
                oDataService.StateDays = objGetServiceResponse.objService.DiasEstado;
                oDataService.StateSubscriber = objGetServiceResponse.objService.EstadoSubscriber;
                oDataService.Technology4G = objGetServiceResponse.objService.Tecnologia4G;
                /*PROY-31249*/
                oDataService.TechnologyVoLTE = objGetServiceResponse.objService.TechnologyVoLTE;
                oDataService.TechnologyVoWifi = objGetServiceResponse.objService.TechnologyVoWifi;

                oDataService.BalancePending = objGetServiceResponse.objService.SaldoPendiente;
                oDataService.BondAmountRFA = objGetServiceResponse.objService.BondAmountRFA;
                oDataService.BondRFA = objGetServiceResponse.objService.BondRFA;

                if (objGetServiceResponse.objService.listAccount != null && objGetServiceResponse.objService.listAccount.Count > 0)
                {
                    oDataService.listAccount = ListModelAccount(objGetServiceResponse);
                }

                if (objGetServiceResponse.objService.listBalance != null && objGetServiceResponse.objService.listBalance.Count > 0)
                {
                    oDataService.listBalance = ListModelBalance(objGetServiceResponse);
                }

                if (objGetServiceResponse.objService.listTrio != null && objGetServiceResponse.objService.listTrio.Count > 0)
                {
                    oDataService.listTrio = ListModelTrio(objGetServiceResponse);
                }
            }

            return oDataService;
        }

private string GetRateState(string strIdSession, string strTelephone)
        {
            string State="";
            PrepaidService.RateStateResponse objRateStateResponse;
            PrepaidService.RateStateRequest objRateStateRequest = new PrepaidService.RateStateRequest()
            {
                strPhoneNumber = strTelephone,
                audit = App_Code.Common.CreateAuditRequest<PrepaidService.AuditRequest>(strIdSession)
            };

            try
            {
                objRateStateResponse = Claro.Web.Logging.ExecuteMethod<PrepaidService.RateStateResponse>(() => { return new PrepaidService.PrepaidServiceClient().GetRateState(objRateStateRequest); });
            }
            catch (Exception ex)
            {
                objRateStateResponse = null;
                Claro.Web.Logging.Error(strIdSession, objRateStateRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRateStateRequest.audit.transaction);
            }


            if (!string.IsNullOrEmpty(objRateStateResponse.codRespuesta))
            {
                if (objRateStateResponse.codRespuesta == "0" && objRateStateResponse.msjRespuesta != string.Empty)
                {
                    State = objRateStateResponse.estado;
                }
            }


            return State;


        }
        private List<MODELS.PrepaidDataService.AccountModel> ListModelAccount(PrepaidService.ServiceResponsePrePaid objGetServiceResponse)
        {
            List<MODELS.PrepaidDataService.AccountModel> oListAccount = new List<MODELS.PrepaidDataService.AccountModel>();
            foreach (PrepaidService.AccountPrepaid item in objGetServiceResponse.objService.listAccount)
            {
                oListAccount.Add(new MODELS.PrepaidDataService.AccountModel()
                {
                    Name = item.Nombre,
                    Balance = item.Saldo,
                    ExpirationDate = item.FechaExpiracion
                });
            }

            return oListAccount;
        }

        private List<MODELS.PrepaidDataService.BalanceModel> ListModelBalance(PrepaidService.ServiceResponsePrePaid objGetServiceResponse)
        {
            List<MODELS.PrepaidDataService.BalanceModel> oListBalance = new List<MODELS.PrepaidDataService.BalanceModel>();

            foreach (PrepaidService.BalancePrePaid item in objGetServiceResponse.objService.listBalance)
            {
                oListBalance.Add(new Models.PrepaidDataService.BalanceModel()
                {
                    Balance = item._Balance,
                    CommercialName = item.CommercialName,
                    ConstancyOrder = item.ConstancyOrder,
                    Destination = item.Destination,
                    Expiration = item.Expiration,
                    MovementTypeName = item.MovementTypeName,
                    Name = item.Name,
                    NameCode = item.NameCode,
                    Order = item.Order,
                    OtherIndicator = item.OtherIndicator,
                    PromotionalBonus = item.PromotionalBonus,
                    Unity = item.Unity,
                    UnityIndicator = item.UnityIndicator,
                });
            }

            return GetListBalancePrepaid(oListBalance);
        }

        private List<MODELS.PrepaidDataService.TrioModel> ListModelTrio(PrepaidService.ServiceResponsePrePaid objGetServiceResponse)
        {
            List<MODELS.PrepaidDataService.TrioModel> oListTrio = new List<MODELS.PrepaidDataService.TrioModel>();

            foreach (PrepaidService.TrioPrepaid item in objGetServiceResponse.objService.listTrio)
            {
                oListTrio.Add(new MODELS.PrepaidDataService.TrioModel()
                {
                    Detail = item.Codigo,
                    Number = item.Descripcion
                });
            }

            return oListTrio;
        }

        public JsonResult GetMovementType(string strIdSession)
        {
            CommonService.EventTypeResponseCommon objEventTypeResponseCommon;
            CommonService.EventTypeRequestCommon objEventTypeRequestCommon = new CommonService.EventTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                EventType = "ListaTipoMovimiento"
            };

            try
            {
                objEventTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.EventTypeResponseCommon>(() => { return new CommonService.CommonServiceClient().GetEventType(objEventTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objEventTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objEventTypeRequestCommon.audit.transaction);
            }

            MODELS.PrepaidDataReload.OnlineReloadModel objOnlineReloadModel = new MODELS.PrepaidDataReload.OnlineReloadModel();

            if (objEventTypeResponseCommon.EventTypes != null)
            {
                List<HELPER_RELOAD.OnlineHelper.EventType> listEventTypes = new List<HELPER_RELOAD.OnlineHelper.EventType>();

                foreach (CommonService.ListItem item in objEventTypeResponseCommon.EventTypes)
                {
                    listEventTypes.Add(new HELPER_RELOAD.OnlineHelper.EventType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objOnlineReloadModel.EventTypes = listEventTypes;
            }

            return Json(new { data = objOnlineReloadModel });
        }

        public JsonResult GetCredDevType(string strIdSession)
        {
            CommonService.EventTypeResponseCommon objEventTypeResponseCommon;
            CommonService.EventTypeRequestCommon objEventTypeRequestCommon = new CommonService.EventTypeRequestCommon()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession),
                EventType = "ListaCreditoDebito"
            };

            try
            {
                objEventTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<CommonService.EventTypeResponseCommon>(() => { return new CommonService.CommonServiceClient().GetEventType(objEventTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objEventTypeRequestCommon.audit.transaction, ex.Message);
                throw new Claro.MessageException(objEventTypeRequestCommon.audit.transaction);
            }

            MODELS.PrepaidDataReload.OnlineReloadModel objOnlineReloadModel = new MODELS.PrepaidDataReload.OnlineReloadModel();

            if (objEventTypeResponseCommon.EventTypes != null)
            {
                List<HELPER_RELOAD.OnlineHelper.EventType> listEventTypes = new List<HELPER_RELOAD.OnlineHelper.EventType>();

                foreach (CommonService.ListItem item in objEventTypeResponseCommon.EventTypes)
                {
                    listEventTypes.Add(new HELPER_RELOAD.OnlineHelper.EventType()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }

                objOnlineReloadModel.EventTypes = listEventTypes;
            }

            return Json(new { data = objOnlineReloadModel });
        }
    }
}