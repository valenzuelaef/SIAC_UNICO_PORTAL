using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService;
using Claro.SIACU.Web.WebApplication.PostpaidService;
using System;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid;
using KEY = Claro.ConfigurationManager;
using System.Collections.Generic;
using System.Linq;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Controllers
{
    public class PostpaidController : Controller
    {
        private readonly PostpaidService.PostpaidServiceClient oServicepost = new PostpaidService.PostpaidServiceClient();
        private readonly DashboardService.DashboardServiceClient oService = new DashboardService.DashboardServiceClient();
        private readonly CommonService.CommonServiceClient _oServiceCommmon = new CommonService.CommonServiceClient();

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult DataCustomer()
        {
            return PartialView();

        }

        public ActionResult DataAccount()
        {
            return PartialView();
        }

        public JsonResult DataServiceContent(string strphonespeed, string strIdSession, string strContratoID, string strApplication, string strCustomerType, string strDocumentType, string strDocumentNumber, string strModality, string strIsLTE, string strphone, string plataformaAT, string flagconvivencia, string coIdPub, string strCustomerId = null)
        {
            //dataservicemg13
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService objDataLineServiceModel;
            PostpaidService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (!string.IsNullOrEmpty(strContratoID) && !strContratoID.Equals(Claro.Constants.NumberZeroString))
            {
                try
                {
                    Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid objLine = GetDataServiceLines(strphonespeed, strContratoID, strApplication, strCustomerType, strDocumentType, strDocumentNumber, strModality, objAuditRequest, strphone, plataformaAT, flagconvivencia, coIdPub);

                    //INICIATIVA-616
                    if (plataformaAT.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && strApplication.Equals(Claro.SIACU.Constants.HFC) && objLine != null)
                    {
                        PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                        Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerResponse objresponse = new Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerResponse();
                        Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerRequest objrequest = new Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerRequest();
                        objrequest.idCustomer = strCustomerId;
                        objrequest.audit = audit;

                        objresponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.obtenerProductosXCustomerResponse>(() =>
                        { return oServicepost.GetProductosXCustomer(objrequest); });


                        if (objresponse.responseData!= null && objresponse.responseData.datosHFC != null)
                        {
                            foreach (var item in objresponse.responseData.datosHFC)
                            {
                                if (item.coId.Equals(strContratoID))
                                {
                                    objLine.TELEFONIA = item.telefonia;
                                    objLine.INTERNET = item.internet;
                                    objLine.CABLETV = item.cable;                                   
                                    break;
                                }
                            }
                        }

                    }
                    //INICIATIVA-616
                    objDataLineServiceModel = DataLineServiceModel(objLine, strApplication);
                    objDataLineServiceModel.IsLTE = strIsLTE;
                    if (objDataLineServiceModel.CodePlanTariff != null)
                    {
                        if (plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                        {
                            objDataLineServiceModel.TypeService = ValidateTypeService(strIdSession, objDataLineServiceModel.CodePlanTariff);
                        }

                    }

                    if (objDataLineServiceModel.StateLine == null)
                    {
                        objDataLineServiceModel.EnableEquipment = false;
                    }
                    else
                    {
                        if (objDataLineServiceModel.StateLine.ToUpper().Equals(Claro.Constants.ActiveState) || objDataLineServiceModel.StateLine.ToUpper().Equals(Claro.Constants.SupendedContract))
                        {
                            objDataLineServiceModel.EnableEquipment = true;
                        }
                        else
                        {
                            objDataLineServiceModel.EnableEquipment = false;
                        }

                        if (ConfigurationManager.AppSettings("strFlagMotiveCancellation").Equals("1") && (strApplication.Equals(Constants.HFC) || strApplication.Equals(Constants.LTE)))
                        {
                            if (objDataLineServiceModel.StateLine.ToUpper().Equals("DESACTIVO"))
                            {
                                if (plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                                {

                                    var objMotiveCancellation = GetMotiveCancellation(strIdSession, strCustomerId, strContratoID);
                                    if (objMotiveCancellation.FlagResult == "1")
                                    {
                                        objDataLineServiceModel.Reason = objMotiveCancellation.MotiveCancellation;
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                    Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                    throw new Claro.MessageException(objAuditRequest.transaction);
                }
            }
            else
            {
                objDataLineServiceModel = DataLineServiceModel(null, strApplication);
            }

            return Json(new { data = objDataLineServiceModel });
        }

        public ActionResult DataService()
        {
            return PartialView();
        }

        public ActionResult DataBilling(string strIdSession, string strCustomerID, string strApplication, string strContratoID, string strInvoiceNumber, string strFechaEmision, string strFechaVencimiento, string plataformaAT, string strcsIdPub, string strbmIdPub, string coIdPub)
        {
            bool isEnvioMail = false;
            Dashboard.Models.Postpaid.DataBillingModel objDataBillingModel = new Models.Postpaid.DataBillingModel();
            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                ContractID = strContratoID,
                audit = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.PostpaidService.AuditRequest>(strIdSession),
                plataformaAT = plataformaAT,
                coIdPub = coIdPub

            };

            if (!string.IsNullOrEmpty(strCustomerID))
            {
                try
                {
                    Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptPostPaid oReceipt = null;

                    oReceipt = GetDataReceipt(strCustomerID, strIdSession, strInvoiceNumber, plataformaAT, strcsIdPub, strbmIdPub, out isEnvioMail);

                    Claro.Web.Logging.Info(strIdSession, "", "Inicio Validacion para obtener datos de un cliente migrado");
                    Claro.Web.Logging.Info(strIdSession, "", "coIdPub: " + coIdPub);

                    if (oReceipt == null && coIdPub.Contains("MIG"))
                    {
                        Claro.Web.Logging.Info(strIdSession, "", "oReceipt es null , se va buscar en ASIS");
                        oReceipt = GetDataReceipt(strCustomerID, strIdSession, strInvoiceNumber, "ASIS", strcsIdPub, strbmIdPub, out isEnvioMail);
                    }

                    Claro.Web.Logging.Info(strIdSession, "", "Fin Validacion para obtener datos de un cliente migrado");
                    
                    objDataBillingModel = DataBillingModel(strIdSession, oReceipt, strApplication, strFechaEmision, strFechaVencimiento, isEnvioMail);

                    Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid strGetDataLine = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(
                                     () => { return oServicepost.GetDataLine(objRequest); });

                    if (strGetDataLine.ObjService != null)
                    {
                        if (strGetDataLine.ObjService.TFI != "")
                        {
                            objDataBillingModel.btnHistHR = true;
                        }
                        else
                        {
                            objDataBillingModel.btnHistHR = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    objDataBillingModel.btnHistHR = false;
                    Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objRequest.audit.transaction);
                }
            }

            return PartialView(objDataBillingModel);
        }

		public ActionResult DataPaymentCollection(string strIdSession, string strCustomerId, string csIdPub, string strPaymentMethod, string strCodePlanTariff, string plataformaAT)
        {
            Dashboard.Models.Postpaid.DataPaymentCollectionModel objDataPaymentCollectionModel = new Models.Postpaid.DataPaymentCollectionModel();
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            if (!string.IsNullOrEmpty(strCustomerId))
            {
                try
                {
                    Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionPostPaid oPaymentCollection = GetPaymentCollection(strCustomerId, csIdPub, strIdSession);
                    if (plataformaAT.Equals(Constants.ASIS)) //everis
                    { 
                        oPaymentCollection.PAYMENT_FORM = strPaymentMethod; 
                    }
                    objDataPaymentCollectionModel = DataPaymentCollectionModel(strCustomerId, oPaymentCollection, strCodePlanTariff);

                }
                catch (Exception ex)
                {
                    string message = Claro.MessageException.GetOriginalExceptionMessage(ex);
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, message);
                    throw new Claro.MessageException(audit.transaction);
                }
            }
            return PartialView(objDataPaymentCollectionModel);
        }

        public JsonResult GetCustomer(string strIdSession, string strValue, string strApplication)
        {
            DashboardService.CustomerPostPaid objCustomer;
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataCustomerModel objCustomerModel = null;
            DashboardService.AuditRequest audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
            try
            {
                objCustomer = GetDataCustomer(strIdSession, strValue, strApplication, audit);
                objCustomerModel = DataCustomerModel(objCustomer, strValue, strApplication);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }
            return Json(new { data = objCustomerModel });
        }

        public DashboardService.CustomerPostPaid GetDataCustomer(string strIdSession, string strValue, string strApplication, DashboardService.AuditRequest audit)
        {
            string strTypeSearch = "";

            if (strApplication == Claro.SIACU.Constants.PostpaidMajuscule || strApplication.Equals(Claro.SIACU.Constants.Post))
            {
                strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                strTypeSearch = Claro.Constants.NumberOneString;
            }
            else
                strTypeSearch = Claro.Constants.NumberThreeString;

            DashboardService.CustomerRequestDashboard objCustomerRequestDashboard = new DashboardService.CustomerRequestDashboard()
            {
                TypeSearch = strTypeSearch,
                ValueSearch = strValue,
                ApplicationType = strApplication,
                audit = audit,
            };

            objCustomerRequestDashboard.audit = audit;
            DashboardService.CustomerResponseDashboard objCustomerResponseDashboard = Claro.Web.Logging.ExecuteMethod<DashboardService.CustomerResponseDashboard>(
                () =>
                { return oService.GetCustomer(objCustomerRequestDashboard); });

            DashboardService.CustomerPostPaid objCustomer = (DashboardService.CustomerPostPaid)objCustomerResponseDashboard.InterfaceCustomer;

            if (objCustomer != null)
            {
                objCustomer.ValueSearch = strValue;
            }

            return objCustomer;
        }

        public Dashboard.Models.Postpaid.DataCustomerModel DataCustomerModel(DashboardService.CustomerPostPaid oCustomer, string strValueSearch, string strApplication)
        {
            Dashboard.Models.Postpaid.DataCustomerModel objDataCustomer = DataCustomer(oCustomer, strValueSearch);
            if (objDataCustomer != null)
            {
                objDataCustomer.Application = strApplication;
                objDataCustomer.ValueSearch = strValueSearch;
            }
            return objDataCustomer;
        }

        private Dashboard.Models.Postpaid.DataCustomerModel DataCustomer(DashboardService.CustomerPostPaid oCustomer, string strValueSearch)
        {
            Dashboard.Models.Postpaid.DataCustomerModel objPostDataCustomer = null;
            if (oCustomer != null)
            {
                string vPlanCode = string.Empty;
                if (!string.IsNullOrEmpty(oCustomer.CONTRATO_ID))
                    vPlanCode = GetFlatCode(Convert.ToString(oCustomer.CONTRATO_ID), oCustomer.Application, string.Empty);

                if (!string.IsNullOrEmpty(vPlanCode))
                {
                    if (vPlanCode.ToUpper() == "NULL")
                    {
                        vPlanCode = string.Empty;
                    }
                }


                //call new REST Service
                Claro.Web.Logging.Error("", "", "INICIO LLAMADA SERVICIO REST");
                var objRequest = new PostpaidService.MiClaroAppRequest();
                var objResponse = new PostpaidService.MiClaroAppResponse();
                var isAppMiClaro = false;
                var AppMiClaroVersion = string.Empty;
                var AppMiClaroLastDate = string.Empty;

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
                objRequest.MessageRequest.Body.numeroLinea = strValueSearch;

                Claro.Web.Logging.Error("", "", string.Format("objRequest=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objRequest)));
                try
                {
                    objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.MiClaroAppResponse>(() =>
                                {
                                    return oServicepost.GetMiClaroApp(objRequest);
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


                objPostDataCustomer = new Dashboard.Models.Postpaid.DataCustomerModel()
                {
                    BusinessName = Convert.ToString(oCustomer.RAZON_SOCIAL),
                    Tradename = Convert.ToString(oCustomer.NOMBRE_COMERCIAL),
                    FullName = Convert.ToString(oCustomer.NOMBRE_COMPLETO),
                    Name = Convert.ToString(oCustomer.NOMBRES),
                    LastName = Convert.ToString(oCustomer.APELLIDOS),
                    DocumentNumber = Convert.ToString(oCustomer.NRO_DOC),
                    DNIRUC = Convert.ToString(oCustomer.DNI_RUC),
                    Account = Convert.ToString(oCustomer.CUENTA),
                    LegalAgent = Convert.ToString(oCustomer.REPRESENTANTE_LEGAL),
                    Modality = Convert.ToString(oCustomer.MODALIDAD),
                    CustomerType = Convert.ToString(oCustomer.TIPO_CLIENTE),
                    CustomerID = Convert.ToString(oCustomer.CUSTOMER_ID),
                    ContractID = Convert.ToString(oCustomer.CONTRATO_ID),
                    PaymentMethod = Convert.ToString(oCustomer.FORMA_PAGO),
                    PhoneReference = Convert.ToString(oCustomer.TELEF_REFERENCIA),
                    Email = Convert.ToString(oCustomer.EMAIL),
                    Segment2 = Convert.ToString(oCustomer.SEGMENTO2),
                    Reference = Convert.ToString(oCustomer.REFERENCIA),
                    Departament = Convert.ToString(oCustomer.DEPARTAMENTO),
                    Province = Convert.ToString(oCustomer.PROVINCIA),
                    District = Convert.ToString(oCustomer.DISTRITO),
                    Membership = oCustomer.AFILIACION,
                    Position = Convert.ToString(oCustomer.CARGO),
                    BirthDate = String.IsNullOrEmpty(oCustomer.FECHA_NAC) ? "" : DateTime.Parse(oCustomer.FECHA_NAC).ToShortDateString(),
                    BirthPlace = Convert.ToString(oCustomer.LUGAR_NACIMIENTO_DES),
                    BirthPlaceID = Convert.ToString(oCustomer.LUGAR_NACIMIENTO_ID),
                    CivilStatus = Convert.ToString(oCustomer.ESTADO_CIVIL),
                    CivilStatusID = Convert.ToString(oCustomer.ESTADO_CIVIL_ID),
                    CustomerContact = Convert.ToString(oCustomer.CONTACTO_CLIENTE),
                    DocumentType = Convert.ToString(oCustomer.TIPO_DOC),
                    Fax = Convert.ToString(oCustomer.FAX),
                    PhoneContact = Convert.ToString(oCustomer.TELEFONO_CONTACTO),
                    Country = Convert.ToString(oCustomer.PAIS),
                    Assessor = Convert.ToString(oCustomer.ASESOR),
                    CodCustomerType = Convert.ToString(oCustomer.COD_TIPO_CLIENTE),
                    InvoiceCountry = Convert.ToString(oCustomer.PAIS_FAC),
                    InvoiceAddress = Convert.ToString(oCustomer.DOMICILIO_FAC),
                    InvoiceUrbanization = Convert.ToString(oCustomer.URBANIZACION_FAC),
                    InvoiceDistrict = Convert.ToString(oCustomer.DISTRITO_FAC),
                    InvoiceProvince = Convert.ToString(oCustomer.PROVINCIA_FAC),
                    InvoiceCode = Convert.ToString(oCustomer.POSTAL_FAC),
                    InvoiceDepartament = Convert.ToString(oCustomer.DEPARTEMENTO_FAC),
                    LegalCountry = Convert.ToString(oCustomer.PAIS_LEGAL),
                    LegalAddress = Convert.ToString(oCustomer.DOMICILIO_LEGAL),
                    LegalUrbanization = Convert.ToString(oCustomer.URBANIZACION_LEGAL),
                    LegalDistrict = Convert.ToString(oCustomer.DISTRITO_LEGAL),
                    LegalProvince = Convert.ToString(oCustomer.PROVINCIA_LEGAL),
                    LegalCode = Convert.ToString(oCustomer.POSTAL_LEGAL),
                    LegalDepartament = Convert.ToString(oCustomer.DEPARTEMENTO_LEGAL),
                    OfficeAddress = Convert.ToString(oCustomer.DIRECCION_DESPACHO),
                    BillingCycle = Convert.ToString(oCustomer.CICLO_FACTURACION),
                    ActivationDate = Convert.ToString(oCustomer.FECHA_ACT.ToString("dd/MM/yyyy")),
                    PlaneCodeBilling = (string.IsNullOrEmpty(vPlanCode) ? Convert.ToString(oCustomer.CODIGO_PLANO_FACT) : vPlanCode),
                    PlaneCodeInstallation = (string.IsNullOrEmpty(vPlanCode) ? Convert.ToString(oCustomer.CODIGO_PLANO_INST) : vPlanCode),
                    CodeCenterPopulate = Convert.ToString(oCustomer.COD_CENTRO_POBLADO),
                    DescriptionCenterPopulate = Convert.ToString(oCustomer.DES_CENTRO_POBLADO),
                    PlaneCode = oCustomer.COD_PLANO,
                    HubCode = Convert.ToString(oCustomer.COD_HUB),
                    BannerMessage = Convert.ToString(oCustomer.BANNER_MESSAGE),
                    InvoiceUbigeo = Convert.ToString(oCustomer.UBIGEO_FACT),
                    InstallUbigeo = Convert.ToString(oCustomer.UBIGEO_INST),
                    ProductTypeText = Convert.ToString(oCustomer.TipoProducto),
                    ProductType = Convert.ToString(oCustomer.TIPO_PRODUCTO),
                    IsLTE = oCustomer.EsServicioLTE,
                    ContactCode = Convert.ToString(oCustomer.OBJID_CONTACTO),
                    IdContactObject = Convert.ToString(oCustomer.OBJID_CONTACTO),
                    ContactFlag = KEY.AppSettings("ContactoFlag"),
                    CodePlanTariff = Convert.ToString(oCustomer.COD_PLAN_TARIFARIO),
                    Address = Convert.ToString(oCustomer.DOMICILIO_FAC),
                    Telephone = Convert.ToString(oCustomer.ValueSearch),
                    StateAgreement = Convert.ToString(oCustomer.oCUENTA.ESTADO_CUENTA),
                    LegalPostal = Convert.ToString(oCustomer.POSTAL_LEGAL),
                    ContactCustomerCode = Convert.ToString(oCustomer.ContactCustomerCode),
                    ContactCntCode = Convert.ToString(oCustomer.ContactCntCode),
                    ContactNumberReference1 = Convert.ToString(oCustomer.ContactNumberReference1),
                    ContactNumberReference2 = Convert.ToString(oCustomer.ContactNumberReference2),
                    StatusCodeFullClaro = Convert.ToString(oCustomer.StatusFullClaroCodeC),
                    StatusCodeFullClaroDesc = Convert.ToString(oCustomer.StatusFullClaroC),
                    coIdPub = oCustomer.coIdPub,
                    csIdPub = oCustomer.csIdPub,
                    IsAppMiclaro = isAppMiClaro,// "1", //PROY-140447
                    AppMiclaroVersion = AppMiClaroVersion, //"5.1", //PROY-140447
                    AppMiClaroLastDate = AppMiClaroLastDate,//"27/04/2019" //PROY-140447
                    Sex = oCustomer.SEXO,
                    DocumentTypeId = oCustomer.TIPO_DOC_ID,
                    origen = oCustomer.origen,
                    apellido_mat_tobe = oCustomer.APELLIDO_MAT_TOBE,
                    apellido_pat_tobe = oCustomer.APELLIDO_PAT_TOBE,
                    indicadorCambioNumero = oCustomer.indicadorCambioNumero,
                    telefonoTOBE = oCustomer.telefonoTOBE //INICIATIVA 616                

                };
                if (oCustomer.itm != null)
                {
                    objPostDataCustomer.itm = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.Itm()
                    {
                        origenCuenta = oCustomer.itm.origenCuenta,
                        codCuenta = oCustomer.itm.codCuenta,
                        coId = oCustomer.itm.coId,
                        identificacion = oCustomer.itm.identificacion,
                        actCuentaProd = oCustomer.itm.actCuentaProd,
                        migCuentaProd = oCustomer.itm.migCuentaProd,
                        origenRegistro = oCustomer.itm.origenRegistro,
                        estado = oCustomer.itm.estado,
                        usrCrea = oCustomer.itm.usrCrea,
                        usrModif = oCustomer.itm.usrModif,
                        fchCreacion = oCustomer.itm.fchCreacion,
                        fchModif = oCustomer.itm.fchModif,
                        custCode = oCustomer.itm.custCode
                    };
                }
                else
                {
                    objPostDataCustomer.itm = new Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.Itm();
                }

                
                //INICIATIVA 616
                if (oCustomer.itm != null && oCustomer.itm.origenRegistro.Equals(KEY.AppSettings("strOrigenRegistroTOBE")))
                {
                    objPostDataCustomer.InvoiceAddress = string.Format("{0} {1} {2} {3}", oCustomer.tipoVia, oCustomer.nombreCalle, oCustomer.numeroCuadra, oCustomer.tipoPredio);//oCustomer.nombreCalle;
                    //objPostDataCustomer.Reference = oCustomer.tipoVia;
                    objPostDataCustomer.flagConvivencia = oCustomer.itm.estado.Equals(KEY.AppSettings("MIGRACION_FIJA")) ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
                }
                else
                {
                    objPostDataCustomer.flagConvivencia = oCustomer.itm != null ? oCustomer.itm.origenRegistro.Equals(KEY.AppSettings("MIGRACION")) ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString : null;
                }
                //INICIATIVA 616
                
                if (objPostDataCustomer.flagConvivencia!=null && objPostDataCustomer.flagConvivencia.Equals(Claro.Constants.NumberOneString))
                {
                    objPostDataCustomer.origen = KEY.AppSettings("keyMigrated");
                }

                if (oCustomer.oCUENTA != null)
                {
                    objPostDataCustomer.objPostDataAccount = DataAccountModel(oCustomer.oCUENTA);
                }
            }

            return objPostDataCustomer;
        }

        public Dashboard.Models.Postpaid.DataAccountModel DataAccountModel(DashboardService.AccountPostPaid oAccount)
        {
            Dashboard.Models.Postpaid.DataAccountModel oDataAccount = DataAccount(oAccount);
            return oDataAccount;
        }

        private Dashboard.Models.Postpaid.DataAccountModel DataAccount(DashboardService.AccountPostPaid oAccount)
        {
            return new Dashboard.Models.Postpaid.DataAccountModel()
            {
                CustomerType = oAccount.TIPO_CLIENTE,
                Modality = oAccount.MODALIDAD,
                Segment = oAccount.SEGMENTO,
                BillingCycle = oAccount.CICLO_FACTURACION,
                Niche = oAccount.NICHO,
                Consultant_Account = oAccount.CONSULTOR_ACCOUNT,
                AccountStatus = oAccount.ESTADO_CUENTA,
                ResponsiblePayment = oAccount.RESPONSABLE_PAGO,
                CreditLimit = oAccount.LIMITE_CREDITO,
                TotalLines = oAccount.TOTAL_LINEAS,
                TotalAccounts = oAccount.TOTAL_CUENTAS,
                ActivationDate = Convert.ToDate(oAccount.FECHA_ACT).ToString("dd/MM/yyyy"),
                Consultant = oAccount.CONSULTOR,
                plataformaAT = string.IsNullOrEmpty(oAccount.plataformaAT) ? "ASIS" : oAccount.plataformaAT,
                billingAccountId = oAccount.billingAccountId,
                bmIdPub = oAccount.bmIdPub,
                IsSendEmail = oAccount.IsSendEmail,
                contactSeqno = oAccount.contactSeqno,
                SaldoCreditLimit = oAccount.SALDO_LIMITE_CREDITO

            };
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptPostPaid GetDataReceipt(string strCustomerCode, string strIdSession, string strInvoiceNumber, string plataformaAT, string strcsIdPub, string strbmIdPub, out bool isEnvioMail)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptRequestPostPaid();
            objRequest.CustomerCode = strCustomerCode;
            objRequest.InvoiceNumber = strInvoiceNumber;
            objRequest.plataformaAT = plataformaAT;
            objRequest.csIdPub = strcsIdPub;
            objRequest.bmIdPub = strbmIdPub;
            objRequest.audit = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.PostpaidService.AuditRequest>(strIdSession);

            Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptResponsePostPaid>(
                () => { return oServicepost.GetDataInvoice(objRequest); });

            isEnvioMail = objResponse.isEnvioEmail;
            return objResponse.ObjReceipt;
        }

        public Dashboard.Models.Postpaid.DataBillingModel DataBillingModel(string strIdSession, Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptPostPaid objReceipt, string strApplication, string strFechaEmision, string strFechaVencimiento, bool isEnvioMail)
        {
            Dashboard.Models.Postpaid.DataBillingModel objDataBilling = DataBilling(strIdSession, objReceipt, strFechaEmision, strFechaVencimiento, isEnvioMail);
            objDataBilling.Application = strApplication;
            return objDataBilling;
        }

        private Dashboard.Models.Postpaid.DataBillingModel DataBilling(string strIdSession, Claro.SIACU.Web.WebApplication.PostpaidService.ReceiptPostPaid objReceipt, string strFechaEmision, string strFechaVencimiento, bool isEnvioMail)
        {
            Dashboard.Models.Postpaid.DataBillingModel objDataBilling = new Dashboard.Models.Postpaid.DataBillingModel();
            objDataBilling.SendMail = isEnvioMail;
            if (objReceipt != null)
            {

                objDataBilling.InvoiceNumber = objReceipt.INVOICENUMBER;
                objDataBilling.ExpirationDate = strFechaVencimiento != null ? strFechaVencimiento : objReceipt.FECHA_VENCIMIENTO;
                objDataBilling.EmissionDate = strFechaEmision != null ? strFechaEmision : objReceipt.FECHA_EMISION;
                if (objReceipt.RECIBO_DETALLADO != null)
                {
                    objDataBilling.LocalTrafficAdditional = objReceipt.RECIBO_DETALLADO.TRAFICO_LOCAL_ADICIONAL;
                    objDataBilling.LocalTrafficConsume = objReceipt.RECIBO_DETALLADO.TRAFICO_LOCAL_A_CONSUMO;
                    objDataBilling.RoamingInternational = objReceipt.RECIBO_DETALLADO.ROAMING_INTERNACIONAL;
                    objDataBilling.TotalSubscription = objReceipt.RECIBO_DETALLADO.TOTALSUBSCRIPTION;
                    objDataBilling.TotalOCCs = objReceipt.RECIBO_DETALLADO.TOTALOCCS;
                    objDataBilling.TotalChargesMonth = objReceipt.RECIBO_DETALLADO.TOTAL_CARGOS_DEL_MES;
                    objDataBilling.LongDistanceNational = objReceipt.RECIBO_DETALLADO.LARGA_DISTANCIA_NACIONAL;
                    objDataBilling.LongDistanceInternational = objReceipt.RECIBO_DETALLADO.LARGA_DISTANCIA_INTERNACIONAL;
                    objDataBilling.TotalAccess = objReceipt.RECIBO_DETALLADO.TOTALACCESS;
                }

                objDataBilling.InvoiceNumberOriginal = objReceipt.INVOICE_NUM;




                string pathInvoice = Claro.ConfigurationManager.AppSettings("strDirectorioFacturas");

                if (!string.IsNullOrEmpty(objDataBilling.InvoiceNumber) && strFechaEmision == null)
                {

                    objDataBilling.FlagBill = Claro.Constants.NumberOneString;
                    objDataBilling.FilePath = pathInvoice + "\\";
                    objDataBilling.FileName = objReceipt.PERIODO.Trim() + "\\" + objReceipt.INVOICENUMBER.Trim() + Claro.SIACU.Constants.PointPdf;

                }
                else if (strFechaEmision != null)
                {
                    objDataBilling.FlagBill = Claro.Constants.NumberOneString;
                    objDataBilling.FilePath = pathInvoice + "\\";
                    DateTime dtDate = DateTime.Parse(strFechaEmision);
                    string strPeriodo = int.Parse(dtDate.Month.ToString()) < 10 ? "0" + dtDate.Month.ToString() : dtDate.Month.ToString();
                    string strYear = dtDate.Year.ToString();
                    objDataBilling.FileName = strYear + strPeriodo.Trim() + "\\" + objReceipt.INVOICENUMBER.Trim() + Claro.SIACU.Constants.PointPdf;
                }
                else
                {
                    objDataBilling.FlagBill = Claro.Constants.NumberZeroString;
                }
                if (!string.IsNullOrEmpty(objDataBilling.FilePath) && !string.IsNullOrEmpty(objDataBilling.FileName))
                {
                    objDataBilling.FilePath = objDataBilling.FilePath.Replace("\\", "|");
                    objDataBilling.FileName = objDataBilling.FileName.Replace("\\", "|");
                }
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, "", "Metodo DataBilling: variable objReceipt == null");
            }
            objDataBilling.NameForm = "";

            return objDataBilling;
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid GetDataServiceLines(string strphonespeed, string strContratoID, string strApplication, string strCustomerType, string strDocumentType, string strDocumentNumber, string strModality, PostpaidService.AuditRequest objAuditRequest, string phone, string plataformaAT, string flagconvivencia, string coIdPub)
        {
            if (strApplication.Equals(Claro.SIACU.Constants.PostpaidMajuscule) || strApplication.Equals(Claro.SIACU.Constants.Post))
            {
                strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
            }

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.ServiceRequestPostPaid()
            {
                ContractID = strContratoID,
                Application = strApplication,
                IdTransaction = objAuditRequest.transaction,
                IpApplication = App_Code.Common.GetApplicationIp(),
                ApplicationName = App_Code.Common.GetApplicationName(),
                UserApplication = App_Code.Common.CurrentUser,
                CustomerType = strCustomerType,
                DocumentType = strDocumentType,
                DocumentNumber = strDocumentNumber,
                Modality = strModality,
                audit = objAuditRequest,
                Telephone = phone,
                strphonespeed = strphonespeed,
                plataformaAT = plataformaAT,
                coIdPub = coIdPub,
                flagConvivencia = flagconvivencia
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.ServiceResponsePostPaid>(
                () =>
                { return oServicepost.GetDataServiceLine(objRequest); });

            return objResponse.ObjService;

        }





        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService DataLineServiceModel(Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid objLineService, string strApplication)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService.DataLineService objDataService = DataLineService(objLineService);
            objDataService.Application = strApplication;
            return objDataService;
        }

        private DataLineService DataLineService(ServicePostPaid objLineService)
        {
            DataLineService objDataService = new Helpers.Postpaid.DataLineService.DataLineService();
            if (objLineService != null)
            {
                objDataService.Tethering = objLineService.Tethering;
                objDataService.Degradation = objLineService.Degradation;
                objDataService.ContractID = objLineService.CONTRATO_ID;
                objDataService.Campaign = objLineService.CAMPANIA;
                objDataService.CellPhone = objLineService.NRO_CELULAR;
                objDataService.ActivationDate = objLineService.FEC_ACTIVACION;
                objDataService.Plan = objLineService.PLAN;
                objDataService.PlanTariff = objLineService.PLAN_TARIFARIO;
                objDataService.TermContract = objLineService.PLAZO_CONTRATO;
                objDataService.Reason = objLineService.MOTIVO;
                objDataService.StateLine = objLineService.ESTADO_LINEA;
                objDataService.StateDate = objLineService.FECHA_ESTADO.ToString();
                objDataService.NumberICCID = objLineService.NROICCID;
                objDataService.NumberIMSI = objLineService.NROIMSI;
                objDataService.MobileBanking = objLineService.BANCA_MOVIL;
                objDataService.TypeSolution = objLineService.TIPO_SOLUCION;
                objDataService.LimitConsume = objLineService.TOPE_CONSUMO;
                objDataService.Seller = objLineService.VENDEDOR;
                objDataService.ServicePackage = objLineService.SERVICEPAQUETE;
                objDataService.StateServicePackage = objLineService.ESTADO_SERVICEPAQUETE;
                objDataService.ServiceCombo = objLineService.SERVICECOMBO;
                objDataService.StateServiceCombo = objLineService.ESTADO_SERVICECOMBO;
                objDataService.ServiceTFI = objLineService.TFI;
                objDataService.StateServiceTFI = objLineService.ESTADO_TFI;
                objDataService.FlagTFI = objLineService.FLAG_TFI;
                objDataService.FlagPlatform = objLineService.FLAG_PLATAFORMA;
                objDataService.IsNot3Play = objLineService.NoEs3Play;
                objDataService.CodePlanTariff = objLineService.COD_PLAN_TARIFARIO;
                objDataService.PIN1 = objLineService.PIN1;
                objDataService.PIN2 = objLineService.PIN2;
                objDataService.PUK1 = objLineService.PUK1;
                objDataService.PUK2 = objLineService.PUK2;
                objDataService.TelephonyValue = objLineService.TELEFONIA;
                objDataService.InternetValue = objLineService.INTERNET;
                objDataService.CableValue = objLineService.CABLETV;
                objDataService.Telephony = objLineService.TELEFONIA == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : objLineService.TELEFONIA == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                objDataService.Internet = objLineService.INTERNET == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : objLineService.INTERNET == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                objDataService.Cable = objLineService.CABLETV == Claro.Constants.LetterF ? "glyphicon glyphicon-remove" : objLineService.CABLETV == Claro.Constants.LetterT ? "glyphicon glyphicon-ok" : "";
                objDataService.ServiceI = objLineService.Application == Claro.SIACU.Constants.LTE ? "glyphicon glyphicon-ok" : "";
                objDataService.ServiceA = objLineService.Application == Claro.SIACU.Constants.LTE ? "" : "glyphicon glyphicon-ok";
                objDataService.Portability = objLineService.Portability;
                objDataService.PortabilityState = objLineService.PortabilityState;
                objDataService.Roaming = objLineService.Roaming;
                objDataService.StateDTH = objLineService.ESTADO_DTH;
                objDataService.TypeProduct = objLineService.TypeProductText;
                objDataService.SegmentoCliente = objLineService.SEGMENTO_CLIENTE;
                objDataService.topeConsumo = objLineService.topeConsumo;
                objDataService.bolsasAdicionales = objLineService.bolsasAdicionales;
            }

            return objDataService;
        }

        private Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionPostPaid GetPaymentCollection(string strCustomerId, string csIdPub, string strIdSession)
        {
            Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionRequestPostPaid objRequest = new Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionRequestPostPaid()
            {
                CustomerId = strCustomerId,
                csIdPub = csIdPub,
                audit = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.PostpaidService.AuditRequest>(strIdSession)
            };

            Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionResponsePostPaid objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionResponsePostPaid>(
                () =>
                { return oServicepost.GetPaymentCollection(objRequest); });

            return objResponse.ObjPaymentCollection;
        }

        public Dashboard.Models.Postpaid.DataPaymentCollectionModel DataPaymentCollectionModel(string strCustomerId, Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionPostPaid oPaymentCollection, string strCodePlanTariff)
        {
            Dashboard.Models.Postpaid.DataPaymentCollectionModel objDataPaymentCollection = DataPaymentCollection(oPaymentCollection);
            objDataPaymentCollection.CustomerId = strCustomerId;
            objDataPaymentCollection.DaysPastDue = "";
            objDataPaymentCollection.StatusCollection = "";
            objDataPaymentCollection.ManagerCollection = "";
            objDataPaymentCollection.btnStatusAccountHR = StatusAccountHRValidate(objDataPaymentCollection.Value_C, strCodePlanTariff) ? "visible" : "hidden";
            return objDataPaymentCollection;
        }

        private Dashboard.Models.Postpaid.DataPaymentCollectionModel DataPaymentCollection(Claro.SIACU.Web.WebApplication.PostpaidService.PaymentCollectionPostPaid oPaymentCollection)
        {
            Dashboard.Models.Postpaid.DataPaymentCollectionModel objPostDataPaymentCollection = new Dashboard.Models.Postpaid.DataPaymentCollectionModel()
            {
                PaymentForm = oPaymentCollection.PAYMENT_FORM,
                Debt = oPaymentCollection.DEBT,
                AmoutDispute = oPaymentCollection.AMOUT_DISPUTE,
                PaymentBehavior = oPaymentCollection.PAYMENT_BEHAVIOR,
                ParameterId = oPaymentCollection.PARAMETER_DATA != null ? oPaymentCollection.PARAMETER_DATA.PARAMETRO_ID : 0,
                Name = oPaymentCollection.PARAMETER_DATA != null ? oPaymentCollection.PARAMETER_DATA.NOMBRE : "",
                Description = oPaymentCollection.PARAMETER_DATA != null ? oPaymentCollection.PARAMETER_DATA.DESCRIPCION : "",
                Value_C = oPaymentCollection.PARAMETER_DATA != null ? oPaymentCollection.PARAMETER_DATA.VALOR_C : "",
                Value_N = oPaymentCollection.PARAMETER_DATA != null ? oPaymentCollection.PARAMETER_DATA.VALOR_N : 0,
                Value_L = oPaymentCollection.PARAMETER_DATA != null ? oPaymentCollection.PARAMETER_DATA.VALOR_L : ""
            };

            return objPostDataPaymentCollection;
        }

        private bool StatusAccountHRValidate(string strItem, string strCodePlanTariff)
        {
            bool blnStatusAccountHR = false;
            if (!string.IsNullOrEmpty(strItem))
            {
                string[] arrParameter = strItem.Split(';');
                for (int z = 0; z <= arrParameter.Length - 2; z++)
                {
                    string[] arrSubParameter = arrParameter[z].ToString().Split(':');
                    if ((arrSubParameter != null) && (!string.IsNullOrEmpty(arrSubParameter[1].ToString()) && ((arrSubParameter[0].ToString().Trim().Equals("TPI")) || (arrSubParameter[0].ToString().Trim().Equals("Internet")))))
                    {
                        string[] arrSubParameters = arrSubParameter[1].ToString().Split(',');

                        for (int p = 0; p <= arrSubParameters.Length - 1; p++)
                        {
                            if (Convert.ToInt(strCodePlanTariff) == Convert.ToInt(arrSubParameters[p].Trim().ToString()))
                            {
                                blnStatusAccountHR = true;
                                break;
                            }
                            else
                            {
                                blnStatusAccountHR = false;
                            }
                        }
                    }

                    if (blnStatusAccountHR) break;
                }
            }
            else
            {
                blnStatusAccountHR = false;
            }

            return blnStatusAccountHR;
        }


        private string ValidateTypeService(string strIdSession, string strPlanTariff)
        {
            PostpaidService.TypeServiceResponsePostpaid objTypeServiceResponse;
            PostpaidService.TypeServiceRequestPostpaid objTypeServiceRequest = new PostpaidService.TypeServiceRequestPostpaid()
            {
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                CodePlanTariff = strPlanTariff
            };


            objTypeServiceResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.TypeServiceResponsePostpaid>(
                objTypeServiceRequest.audit.Session,
                objTypeServiceRequest.audit.transaction,
                () => { return new PostpaidService.PostpaidServiceClient().ValidateTypeService(objTypeServiceRequest); }
                );

            if (objTypeServiceResponse != null)
            {
                return objTypeServiceResponse.NameTypeService.ToUpper();
            }
            else
            {
                return "";
            }

        }

        public string GetFlatCode(string contractId, string application, string flatCodeFromBscs)
        {
            string planeCode = null;

            if (application.Equals(Claro.Constants.ApplicationHfc))
            {
                CommonService.FlatCodeRequest objRequest = new CommonService.FlatCodeRequest
                {
                    ContractId = contractId,
                    audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(Claro.Constants.NumberZeroString)
                };

                CommonService.FlatCodeResponse objResponse = Claro.Web.Logging.ExecuteMethod(() => _oServiceCommmon.GetFlatCode(objRequest));

                if (objResponse != null)
                {
                    planeCode = objResponse.FlatCode;
                }

                if (string.IsNullOrEmpty(planeCode))
                {
                    planeCode = flatCodeFromBscs;
                }
            }
            else
            {
                planeCode = flatCodeFromBscs;
            }

            return planeCode;
        }

        private DataMotiveCancellationModel GetMotiveCancellation(string strIdSession, string customerCode, string contractCode)
        {
            var objModelView = new DataMotiveCancellationModel();

            PostpaidService.MotiveCancellationRequest objRequest = new PostpaidService.MotiveCancellationRequest
            {
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession),
                CustomerCode = customerCode,
                ContractCode = contractCode
            };


            var objResponse = Claro.Web.Logging.ExecuteMethod<PostpaidService.MotiveCancellationResponse>(
                objRequest.audit.Session,
                objRequest.audit.transaction,
                () => { return new PostpaidService.PostpaidServiceClient().GetMotiveCancellation(objRequest); }
            );

            if (objResponse != null)
            {
                objModelView.CodeError = objResponse.CodeError;
                objModelView.DescriptionError = objResponse.DescriptionError;
                objModelView.MotiveCancellation = objResponse.MotiveCancellation;
                objModelView.FlagResult = objResponse.FlagResult;
            }

            return objModelView;
        }
        public JsonResult GetParameterTerminalTPI(string strIdSession)
        {

            PostpaidService.ParameterTerminalTPIRequestPostPaid objParameterTerminalTPIRequestPostPaid = new ParameterTerminalTPIRequestPostPaid();
            objParameterTerminalTPIRequestPostPaid.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            PostpaidService.ParameterTerminalTPIResponsePostPaid objParameterTerminalTPIResponsePostPaid = Claro.Web.Logging.ExecuteMethod(() => oServicepost.GetParameterTerminalTPI(objParameterTerminalTPIRequestPostPaid));
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataParameterTPIModel objDataParameterTPIModel = new Models.Postpaid.DataParameterTPIModel()
            {
                AbrvPermissions = objParameterTerminalTPIResponsePostPaid.AbrvPermissions,
                CodePlanTariffTFI = objParameterTerminalTPIResponsePostPaid.CodePlanTariffTFI
            };
            return Json(new { data = objDataParameterTPIModel });
        }

        public PostpaidService.ConsultBonusStatusFullClaroBodyResponse getFullClaroStatusLine(string strCoId, string strIdSession)
        {
            ConsultBonusStatusFullClaroResponse response = new ConsultBonusStatusFullClaroResponse();
            ConsultBonusStatusFullClaroRequest request = new ConsultBonusStatusFullClaroRequest();
            try
            {
                request = new ConsultBonusStatusFullClaroRequest();
                request.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                request.AuditService = new Audit();
                request.MessageRequest = new ConsultBonusStatusFullClaroMessageRequest();
                request.MessageRequest.Header = new ConsultBonusStatusFullClaroHeaderRequest();
                request.MessageRequest.Header.HeaderRequest = getHeaderRequest("getFullClaroStatusLine");
                request.MessageRequest.Body = new ConsultBonusStatusFullClaroBodyRequest();
                request.MessageRequest.Body.coId = strCoId;

                response = oServicepost.GetBonusStatusFullClaro(request);

                if (response != null ? (response.MessageResponse != null ? (response.MessageResponse.Body != null ? (response.MessageResponse.Body.codigoRespuesta.Equals("-99") ? false : true) : false) : false) : false)
                {
                    Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.mensajeRespuesta);
                }
                else
                {
                    response = new ConsultBonusStatusFullClaroResponse();
                    response.MessageResponse = new ConsultBonusStatusFullClaroMessageResponse();
                    response.MessageResponse.Body = new ConsultBonusStatusFullClaroBodyResponse();
                    response.MessageResponse.Body.nombreEtiqueta1 = KEY.AppSettings("strFullClaroMessageErrorClient");
                    response.MessageResponse.Body.codigoEtiqueta1 = "0";
                    response.MessageResponse.Body.nombreEtiqueta2 = KEY.AppSettings("strFullClaroMessageErrorService");
                    response.MessageResponse.Body.codigoEtiqueta2 = "10";
                    response.MessageResponse.Body.codigoRespuesta = "-99";
                    response.MessageResponse.Body.mensajeError = "Response Null";
                    response.MessageResponse.Body.mensajeRespuesta = "Response null";
                    Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.mensajeError);
                }
            }
            catch (Exception ex)
            {
                response = new ConsultBonusStatusFullClaroResponse();
                response.MessageResponse = new ConsultBonusStatusFullClaroMessageResponse();
                response.MessageResponse.Body = new ConsultBonusStatusFullClaroBodyResponse();
                response.MessageResponse.Body.nombreEtiqueta1 = KEY.AppSettings("strFullClaroMessageErrorClient");
                response.MessageResponse.Body.codigoEtiqueta1 = "0";
                response.MessageResponse.Body.nombreEtiqueta2 = KEY.AppSettings("strFullClaroMessageErrorService");
                response.MessageResponse.Body.codigoEtiqueta2 = "10";
                response.MessageResponse.Body.codigoRespuesta = "-99";
                response.MessageResponse.Body.mensajeError = "Response Null";
                response.MessageResponse.Body.mensajeRespuesta = "Response null";
                Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, ex.Message);
            }
            return response.MessageResponse.Body;
        }

        public PostpaidService.GetValidateLineResponse getFullClaroStatusLineTobe(string strCoId, string numeroDoc, string tipoDoc, string strIdSession)
        {
            GetValidateLineResponse response = null;
            GetValidateLineRequest request = null;
            AuditRequest audit = null;
            try
            {
                request = new GetValidateLineRequest();
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                request.validarLineaRequest = new validarLineaRequest();
                request.validarLineaRequest.coId = strCoId;
                request.validarLineaRequest.tipoDocumento = tipoDoc;
                request.validarLineaRequest.numeroDocumento = numeroDoc;

                response = oServicepost.GetValidateLine(request, audit);

                if (response != null ? (response.validarLineaResponse != null ? (response.validarLineaResponse.responseAudit != null ? (response.validarLineaResponse.responseAudit.codigoRespuesta.Equals("-99") ? false : true) : false) : false) : false)
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, response.validarLineaResponse.responseAudit.mensajeRespuesta);
                }
                else
                {
                    response = new GetValidateLineResponse();
                    response.validarLineaResponse = new validarLineaResponse();
                    response.validarLineaResponse.responseAudit = new responseAudit1();
                    response.validarLineaResponse.responseData = new responseData1();
                    response.validarLineaResponse.responseData.nombreEtiqueta1 = KEY.AppSettings("strFullClaroMessageErrorClient");
                    response.validarLineaResponse.responseData.codigoEtiqueta1 = "0";
                    response.validarLineaResponse.responseData.nombreEtiqueta2 = KEY.AppSettings("strFullClaroMessageErrorService");
                    response.validarLineaResponse.responseData.codigoEtiqueta2 = "10";
                    response.validarLineaResponse.responseAudit.codigoRespuesta = "-99";
                    response.validarLineaResponse.responseAudit.mensajeRespuesta = "Response Null";
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, response.validarLineaResponse.responseAudit.mensajeRespuesta);
                }
            }
            catch (Exception ex)
            {
                response = new GetValidateLineResponse();
                response.validarLineaResponse = new validarLineaResponse();
                response.validarLineaResponse.responseAudit = new responseAudit1();
                response.validarLineaResponse.responseData = new responseData1();
                response.validarLineaResponse.responseData.nombreEtiqueta1 = KEY.AppSettings("strFullClaroMessageErrorClient");
                response.validarLineaResponse.responseData.codigoEtiqueta1 = "0";
                response.validarLineaResponse.responseData.nombreEtiqueta2 = KEY.AppSettings("strFullClaroMessageErrorService");
                response.validarLineaResponse.responseData.codigoEtiqueta2 = "10";
                response.validarLineaResponse.responseAudit.codigoRespuesta = "-99";
                response.validarLineaResponse.responseAudit.mensajeRespuesta = "Response Null";
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return response;
        }

        public PostpaidService.HeaderRequest getHeaderRequest(string operation)
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
                            wsIp = App_Code.Common.GetApplicationIp()
                        };
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

        public JsonResult getFullClaroStatusLineObj(string strNroDoc, string strTypeDoc, string strIdSession, string plataformaAT)
        {
            validarClienteModel validarCliente = new validarClienteModel();
            try
            {
                if (plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                {
                    BonusFullClaroClientResponse response = new BonusFullClaroClientResponse();
                    BonusFullClaroClientRequest request = new BonusFullClaroClientRequest();
                    request = new BonusFullClaroClientRequest();
                    request.audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                    request.AuditService = new Audit();
                    request.MessageRequest = new BonusFullClaroClientMessageRequest();
                    request.MessageRequest.Header = new BonusFullClaroClientHeaderRequest();
                    request.MessageRequest.Header.HeaderRequest = getHeaderRequest("getFullClaroStatusLine");
                    request.MessageRequest.Body = new BonusFullClaroClientBodyRequest();
                    request.MessageRequest.Body.nroDocumento = strNroDoc;
                    request.MessageRequest.Body.tipoDocumento = strTypeDoc;

                    response = oServicepost.GetBonusStatusFullClaroClient(request);

                    if (response != null ? (response.MessageResponse != null ? (response.MessageResponse.Body != null ? (response.MessageResponse.Body.codigoRespuesta.Equals("-99") ? false : true) : false) : false) : false)
                    {
                        validarCliente.nombreEtiqueta = response.MessageResponse.Body.nombreEtiqueta1;
                        validarCliente.codigoEtiqueta = response.MessageResponse.Body.codigoEtiqueta1;
                        validarCliente.codigoRespuesta = response.MessageResponse.Body.nombreEtiqueta1;
                        validarCliente.bonoElegido = response.MessageResponse.Body.bonoElegido;
                        validarCliente.bonoLinea = response.MessageResponse.Body.bonoLinea;
                        validarCliente.estado = response.MessageResponse.Body.estado;
                        validarCliente.mensajeRespuesta = response.MessageResponse.Body.mensajeRespuesta;

                        Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.mensajeRespuesta);
                    }
                    else
                    {
                        validarCliente.nombreEtiqueta = KEY.AppSettings("strFullClaroMessageErrorClient");
                        validarCliente.codigoEtiqueta = "1";
                        validarCliente.codigoRespuesta = "-99";
                        validarCliente.mensajeRespuesta = "Response Null";

                        Claro.Web.Logging.Error(request.audit.Session, request.audit.transaction, response.MessageResponse.Body.mensajeError);
                    }

                }
                else
                {
                    GetValidateCustomerRequest request = new GetValidateCustomerRequest();
                    GetValidateCustomerResponse response = new GetValidateCustomerResponse();
                    AuditRequest audit = new AuditRequest();


                    request = new GetValidateCustomerRequest();
                    audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                    request.validarClienteRequest = new validarClienteRequest();
                    request.validarClienteRequest.tipoDocumento = strTypeDoc;
                    request.validarClienteRequest.numeroDocumento = strNroDoc;

                    response = oServicepost.GetValidateCustomer(request, audit);

                    if (response != null ?
                        (response.validarClienteResponse != null ?
                        (response.validarClienteResponse.responseAudit != null ?
                        (response.validarClienteResponse.responseData != null ?
                        (response.validarClienteResponse.responseAudit.codigoRespuesta.Equals("-99") ?
                        false : true) : false) : false) : false) : false)
                    {
                        validarCliente.nombreEtiqueta = response.validarClienteResponse.responseData.nombreEtiqueta;
                        validarCliente.codigoEtiqueta = response.validarClienteResponse.responseData.codigoEtiqueta;
                        validarCliente.codigoRespuesta = response.validarClienteResponse.responseAudit.codigoRespuesta;
                        validarCliente.bonoElegido = response.validarClienteResponse.responseData.bonoElegido;
                        validarCliente.bonoLinea = response.validarClienteResponse.responseData.bonoLinea;
                        validarCliente.estado = response.validarClienteResponse.responseData.estado;
                        validarCliente.mensajeRespuesta = response.validarClienteResponse.responseAudit.mensajeRespuesta;
                        Claro.Web.Logging.Info(audit.transaction, audit.Session, response.validarClienteResponse.responseAudit.mensajeRespuesta);
                    }
                    else
                    {
                        validarCliente.nombreEtiqueta = KEY.AppSettings("strFullClaroMessageErrorClient");
                        validarCliente.codigoEtiqueta = "1";
                        validarCliente.codigoRespuesta = "-99";
                        validarCliente.mensajeRespuesta = "Response Null";

                        Claro.Web.Logging.Error(audit.transaction, audit.Session, response.validarClienteResponse.responseAudit.mensajeRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                validarCliente.nombreEtiqueta = KEY.AppSettings("strFullClaroMessageErrorClient");
                validarCliente.codigoEtiqueta = "1";
                validarCliente.codigoRespuesta = "-99";
                validarCliente.mensajeRespuesta = "Response Null";

                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);
            }
            return Json(new { data = validarCliente });
        }
        public JsonResult getFullClaroStatusCoIdObj(string strCoId, string strNroDoc, string strTypeDoc, string plataformaAT, string strIdSession)
        {
            Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.ValidarLineaModel objValidarLineaModel = new ValidarLineaModel();
            try
            {
                if (plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                {
                    PostpaidService.ConsultBonusStatusFullClaroBodyResponse objFullClaro = new PostpaidService.ConsultBonusStatusFullClaroBodyResponse();
                    objFullClaro = getFullClaroStatusLine(strCoId, strIdSession);

                    if (objFullClaro != null)
                    {
                        objValidarLineaModel.nombreEtiqueta1 = objFullClaro.nombreEtiqueta1;
                        objValidarLineaModel.codigoEtiqueta1 = objFullClaro.codigoEtiqueta1;
                        objValidarLineaModel.nombreEtiqueta2 = objFullClaro.nombreEtiqueta2;
                        objValidarLineaModel.codigoEtiqueta2 = objFullClaro.codigoEtiqueta2;
                    }
                }
                else
                {
                    GetValidateLineResponse response = null;
                    response = getFullClaroStatusLineTobe(strCoId, strNroDoc, strTypeDoc, strIdSession);
                    if (response != null)
                    {
                        objValidarLineaModel.nombreEtiqueta1 = response.validarLineaResponse.responseData.nombreEtiqueta1;
                        objValidarLineaModel.codigoEtiqueta1 = response.validarLineaResponse.responseData.codigoEtiqueta1;
                        objValidarLineaModel.nombreEtiqueta2 = response.validarLineaResponse.responseData.nombreEtiqueta2;
                        objValidarLineaModel.codigoEtiqueta2 = response.validarLineaResponse.responseData.codigoEtiqueta2;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);
                objValidarLineaModel.codigoEtiqueta1 = "1";
                objValidarLineaModel.codigoEtiqueta2 = "10";
                objValidarLineaModel.nombreEtiqueta1 = KEY.AppSettings("strFullClaroMessageErrorClient");
                objValidarLineaModel.nombreEtiqueta2 = KEY.AppSettings("strFullClaroMessageErrorService");
            }
            return Json(new { data = objValidarLineaModel });
        }

        //mg13  
        public JsonResult getLastReasonState(string strIdSession, string strContractID, string plataform, string flagConvivencia, string coIdPub)
        {
            string LastReasonState = string.Empty;
            PostpaidService.AuditRequest audit = null;
            try
            {
                audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
                List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstServicePostPaid = new PostpaidDataServiceController().GetDataLineHistory(strContractID, plataform, audit, flagConvivencia, coIdPub);
                List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid> lstServicePostPaidAux = new List<Claro.SIACU.Web.WebApplication.PostpaidService.ServicePostPaid>();

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Lista: " + Newtonsoft.Json.JsonConvert.SerializeObject(Json(new { lstServicePostPaid })));

                lstServicePostPaidAux = lstServicePostPaid.OrderByDescending(x => x.INTRODUCIDO_EL).Reverse().Take(1).ToList();

                if (lstServicePostPaidAux[0].MOTIVO.ToUpper().Equals("RETENIDO"))
                {
                    lstServicePostPaid = lstServicePostPaid.OrderByDescending(x => x.INTRODUCIDO_EL).Reverse().Skip(1).Take(1).ToList();
                }
                else
                {
                    lstServicePostPaid = lstServicePostPaid.OrderByDescending(x => x.INTRODUCIDO_EL).Reverse().Take(1).ToList();
                }

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "LastReasonState: " + Newtonsoft.Json.JsonConvert.SerializeObject(Json(new { lstServicePostPaid })));
                LastReasonState = lstServicePostPaid[0].MOTIVO;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw;
            }

            return Json(LastReasonState);

        }
        //mg13

        public JsonResult getDegradationTobe(string strIdSession, string strMsidn, string coIdPub)
        {

            DashboardService.AuditRequest audit = null;
            DashboardService.GetDegradationResponseTobe response = null;
            try
            {
                audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                DashboardService.GetDegradationRequestTobe request = new DashboardService.GetDegradationRequestTobe()
                {
                    audit = audit,
                    coIdPub = coIdPub,
                    strMsidn = strMsidn
                };

                response = oService.GetBalancePostpaidConsumerB2ELegacyDegradation(request);


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw;
            }

            return Json(new { listOptional = response.listOptional });

        }

 public JsonResult GetCovid19(string strIdSession, string strContratoID, string strOpcion)
        {
            string resultado = "0";
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            List<PostpaidService.ListaBloDesblo> lista = new List<PostpaidService.ListaBloDesblo>();
            string mensaje_error = string.Empty;
            string ContratoID = strContratoID;
            try
            {

                if (ConfigurationManager.AppSettings("strFlagCovid19").Equals("1"))
                {
                    string bloqcovid19 = ConfigurationManager.AppSettings("strBloqCovid19");
                    string listaOpciones = ConfigurationManager.AppSettings("strListaOpcionesCovid19");
                    string[] Opciones = listaOpciones.Split('|');
                    bool exitLoop = false;
                    foreach (string item in Opciones)
                    {
                        if (strOpcion == item)
                        {
                            exitLoop = true;
                            lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                            {
                                return oServicepost.obtenerListaBloDesblo(out mensaje_error, audit, ContratoID);
                            });

                            if (lista.Count > 0)
                            {
                                foreach (PostpaidService.ListaBloDesblo x in lista)
                                {
                                    if (x._tipo == bloqcovid19)
                                    {
                                        resultado = "1";
                                    }
                                }
                            }
                        }
                        if (exitLoop) break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Message resultado : " + resultado);
            return Json(new { data = resultado });
        }

        public JsonResult GetBloqSES(string strIdSession, string strContratoID, string strOpcion)
        {
            string resultado = "0";
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            List<PostpaidService.ListaBloDesblo> lista = new List<PostpaidService.ListaBloDesblo>();
            string mensaje_error = string.Empty;
            string ContratoID = strContratoID;
            try
            {

                if (ConfigurationManager.AppSettings("strFlagBloqSES").Equals("1"))
                {
                    string bloqSES = ConfigurationManager.AppSettings("strBloqSES");
                    string listaOpciones = ConfigurationManager.AppSettings("strListaOpcionesBloqSES");
                    string[] Opciones = listaOpciones.Split('|');
                    bool exitLoop = false;
                    foreach (string item in Opciones)
                    {
                        if (strOpcion == item)
                        {
                            exitLoop = true;
                            lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                            {
                                return oServicepost.obtenerListaBloDesblo(out mensaje_error, audit, ContratoID);
                            });

                            if (lista.Count > 0)
                            {
                                foreach (PostpaidService.ListaBloDesblo x in lista)
                                {
                                    if (x._tipo == bloqSES)
                                    {
                                        resultado = "1";
                                    }
                                }
                            }
                        }
                        if (exitLoop) break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Message resultado : " + resultado);
            return Json(new { data = resultado });
        }

        public JsonResult GetBloqueosClaro(string strIdSession, string strContratoID, string strOpcion)
        {

            string resultado = "0";
            PostpaidService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostpaidService.AuditRequest>(strIdSession);
            List<PostpaidService.ListaBloDesblo> lista = new List<PostpaidService.ListaBloDesblo>();
            string mensaje_error = string.Empty;
            string ContratoID = strContratoID;
            string strMensaje = string.Empty;
            string bloqueoClaro = ConfigurationManager.AppSettings("strbloqueoClaro");

            List<ParameterPostPaid> listaBloqueos = new List<ParameterPostPaid>();

            try
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "strContratoID : " + strContratoID.ToString());
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "strOpcion : " + strOpcion.ToString());
                bool exitLoop = false;
                listaBloqueos = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicepost.ObtenerBloqueosClaro(out mensaje_error, audit.Session, audit.transaction, bloqueoClaro);
                });

                if (listaBloqueos.Count > 0)
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "listaBloqueos : " + listaBloqueos.Count.ToString());

                    foreach (ParameterPostPaid bloq in listaBloqueos)
                    {
                        if (bloq.VALOR_N.Equals(1))
                        {
                            string bloqueo = bloq.DESCRIPCION;

                            lista = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                            {
                                return oServicepost.obtenerListaBloDesblo(out mensaje_error, audit, ContratoID);
                            });

                            if (lista.Count > 0)
                            {
                                if (lista.Exists(x => x._tipo == bloqueo))
                                {
                                    string listaOpciones = bloq.VALOR_L;
                                    List<string> Opciones = listaOpciones.Split('|').ToList();
                                    if (Opciones.Exists(x => x == strOpcion))
                                    {
                                        exitLoop = true;
                                        resultado = "1";
                                        strMensaje = bloq.VALOR_C;
                                    }
                                }
                            }
                        }
                        if (exitLoop) break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Message resultado : " + resultado);
            return Json(new { data = resultado, message = strMensaje });
        }
        public JsonResult GetListaBloqueoDebloqueoTobe(string strIdSession, string strCoidPub, string strLinea, string strOpcion)
        {
            Claro.Web.Logging.Info(strIdSession, "T" + strIdSession, "Inicio de GetListaBloqueoDebloqueoTobe");
            
            DashboardService.ResponseListaObtenerBloqueos objResponse = new DashboardService.ResponseListaObtenerBloqueos();
            List<ModelListaBloqueo> listaModel = new List<ModelListaBloqueo>();
            string mensaje_error = string.Empty;
            bool exitLoop = false;
            string resultadoBloq = "0";
            string resultadoCovid = "0";
            string resultadoSES = "0";
            string strMensajeBloq = "";
            try
            {
                string bloqueoClaro = ConfigurationManager.AppSettings("strbloqueosClaroCBIO");
                DashboardService.AuditRequest audit = App_Code.Common.CreateAuditRequest<DashboardService.AuditRequest>(strIdSession);
                List<ParameterPostPaid> listaBloqueos = new List<ParameterPostPaid>();
                DashboardService.RequestListaobtenerBloqueos objRequest = new DashboardService.RequestListaobtenerBloqueos()
                {
                    obtenerBloqueosContratoRequest = new DashboardService.ObtenerBloqueosContratoRequest()
                    {
                        codigoRazon = "",
                        coIdPub = strCoidPub,
                        telefono = strLinea
                    }
                };
             
                listaBloqueos = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicepost.ObtenerBloqueosClaro(out mensaje_error, audit.Session, audit.transaction, bloqueoClaro);
                });
                Tools.Traces.Logging.Info("", "", string.Format("listaBloqueos .Response=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(listaBloqueos)));
                Claro.Web.Logging.Info(strIdSession, "T" + strIdSession, string.Format(" mensaje_error: {0}", mensaje_error));

                Tools.Traces.Logging.Info("", "", string.Format(" GetListaBloqueoDesbloqueo objRequest=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objRequest)));
                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oService.GetListaBloqueoDesbloqueo(objRequest, audit);
                });
                Tools.Traces.Logging.Info("", "", string.Format("GetListaBloqueoDesbloqueo .Response=>{0}", Newtonsoft.Json.JsonConvert.SerializeObject(objResponse)));

                if (objResponse.obtenerBloqueosContratoResponse.responseAudit.codigoRespuesta == Constants.ZeroNumber ||
                    objResponse.obtenerBloqueosContratoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberThreeString ||
                    objResponse.obtenerBloqueosContratoResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberFiveString)
                {
                    foreach (var item in objResponse.obtenerBloqueosContratoResponse.responseData.listaBloqueo)
                    {
                        ModelListaBloqueo objModel = new ModelListaBloqueo();
                     
                        objModel.dsc = item.longDescription;
                        objModel.ticklerNumber = item.ticklerNumber;
                        objModel.ticklerCode = item.ticklerCode;
                        objModel.ticklerStatus = item.ticklerStatus;
                        objModel.createdBy = item.createdBy;
                        objModel.createdDate = item.createdDate;
                        objModel.longDescription = item.longDescription;
                        objModel.estado = item.estado;
                        listaModel.Add(objModel);
                    }
                }
                else
                {
                    listaModel = null;
                }
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "listaBloqueos : " + listaBloqueos.Count.ToString());
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "listaModel : " + listaModel.Count.ToString());
                //GetBloqueosClaro
                if (listaBloqueos.Count > 0 && listaModel != null)
                {
                    foreach (ParameterPostPaid bloq in listaBloqueos)
                    {
                        if (bloq.VALOR_N.Equals(1))
                        {
                            string bloqueo = bloq.DESCRIPCION;
                            if (listaModel.Count > 0)
                            {
                                if (listaModel.Exists(x => x.dsc == bloqueo))
                                {
                                    string listaOpciones = bloq.VALOR_L;
                                    List<string> Opciones = listaOpciones.Split('|').ToList();
                                    if (Opciones.Exists(x => x == strOpcion))
                                    {
                                        exitLoop = true;
                                        resultadoBloq = "1";
                                        strMensajeBloq = bloq.VALOR_C;
                                    }
                                }
                            }
                        }
                        if (exitLoop) break;
                    }
                }

                //BloqueoCovid
                if (ConfigurationManager.AppSettings("strFlagCovid19").Equals("1"))
                {
                    string bloqcovid19 = ConfigurationManager.AppSettings("strBloqCovid19");
                    string listaOpciones = ConfigurationManager.AppSettings("strListaOpcionesCovid19");
                    string[] Opciones = listaOpciones.Split('|');
                    bool exitLoopCovid = false;
                    foreach (string item in Opciones)
                    {
                        if (strOpcion == item)
                        {
                            exitLoopCovid = true;

                            if (listaModel.Count > 0)
                            {
                                foreach (ModelListaBloqueo x in listaModel)
                                {
                                    if (x.dsc == bloqcovid19)
                                    {
                                        resultadoCovid = "1";
                                    }
                                }
                            }
                        }
                        if (exitLoopCovid) break;
                    }
                }
                //BloqueSes
                if (ConfigurationManager.AppSettings("strFlagBloqSES").Equals("1"))
                {
                    string bloqSES = ConfigurationManager.AppSettings("strBloqSES");
                    string listaOpciones = ConfigurationManager.AppSettings("strListaOpcionesBloqSES");
                    string[] Opciones = listaOpciones.Split('|');
                    bool exitLoopSess = false;
                    foreach (string item in Opciones)
                    {
                        if (strOpcion == item)
                        {
                            exitLoopSess = true;

                            if (listaModel.Count > 0)
                            {
                                foreach (ModelListaBloqueo x in listaModel)
                                {
                                    if (x.dsc == bloqSES)
                                    {
                                        resultadoSES = "1";
                                    }
                                }
                            }
                        }
                        if (exitLoopSess) break;
                    }
                }


    }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, "T" + strIdSession, string.Format(" Ex- GetListaBloqueoDebloqueoTobe: {0}", ex));
                throw;
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format(" resultadoBloq: {0} / strMensajeBloq:{1} / resultadoSES:{2} /resultadoCovid:{3}", resultadoBloq, strMensajeBloq, resultadoSES, resultadoCovid));
            return Json(new { dataBloq = resultadoBloq, strBloq = strMensajeBloq, dataSES = resultadoSES, dataCovid = resultadoCovid });
        }
    }
}