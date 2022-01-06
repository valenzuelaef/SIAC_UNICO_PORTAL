using Claro.Data;
using Claro.SIACU.ProxyService.Audit.Redirect;
using Claro.SIACU.ProxyService.SIAC.Segment;
using Claro.SIACU.ProxyService.SIACPost.AccountDue;
using Claro.SIACU.ProxyService.SIACPost.CreditBalance;
using Claro.SIACU.ProxyService.SIACPost.Customer;
using Claro.SIACU.ProxyService.SIACPost.CustomerHFC;
using Claro.SIACU.ProxyService.SIACPost.CustomerLTE;
using Claro.SIACU.ProxyService.SIACPost.DatosSAP;
using Claro.SIACU.ProxyService.SIACPost.FinanceManagement;
using Claro.SIACU.ProxyService.SIACPost.HLRUDB;
using Claro.SIACU.ProxyService.SIACPost.IMR;
using Claro.SIACU.ProxyService.SIACPost.PostpaidService;
using Claro.SIACU.ProxyService.SIACPre.BondTFI;
using Claro.SIACU.ProxyService.SIACPre.DataPrePost;
using Claro.SIACU.ProxyService.SIACPre.Service2;
using Claro.SIACU.ProxyService.SIACU.Customers;
using Claro.SIACU.ProxyService.SIACU.Post.Lines;
using Claro.SIACU.ProxyService.SIACU.Post.Products;
using Claro.SIACU.ProxyService.SIACU.Pre.Products;
using Claro.SIACU.ProxyService.SIACPre.PaqueteDatos;
using Claro.SIACU.ProxyService.SIAC.Seguridad;
using Claro.SIACU.ProxyService.SIACPost.PackageRoaming;
using Claro.SIACU.ProxyService.SIACPost.CustomerContact;
using Claro.SIACU.ProxyService.SIACPost.IGVHFC;
using Claro.SIACU.ProxyService.SIACPost.Validate4GLTE;
using Claro.SIACU.ProxyService.SIACPost.DocumentoFisico;
using Claro.SIACU.ProxyService.SIACSecurity.Permissions;
using Claro.SIACU.ProxyService.SIACPost.RechargesM2M;
using Claro.SIACU.ProxyService.SIACPost.DetalleAjuste;
using Claro.SIACU.ProxyService.SecurityService;
using Claro.SIACU.ProxyService.SIAC.ActivarVOLTE;
using Claro.SIACU.ProxyService.SIACPost.RestricTethecVeloc;
using Claro.SIACU.ProxyService.SIAC.Claves;
//<ASANCHEZ>
using Claro.SIACU.ProxyService.SIACPre.ConsultarDatosSIM;
//<ASANCHEZ>
using Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion;
using Claro.SIACU.ProxyService.SIAC.ParamaetrosClarify;
using Claro.SIACU.ProxyService.SIACU.Post.BSS_StatusAccount;
using Claro.SIACU.ProxyService.SIACPost.BloqDesbloqLineaPostpago;


namespace Claro.SIACU.Data.Configuration
{
    internal struct ServiceConfiguration
    {
        public static readonly ebsConsultaSegmentoPortTypeClient PREPAID_SEGMENT = Service.Create<ebsConsultaSegmentoPortTypeClient>("SIACPrepagoSegmento");
        public static readonly SIACPostpagoConsultasWSClient POSTPAID_CONSULTCLIENT = Service.Create<SIACPostpagoConsultasWSClient>("SIACPostpagoConsultas");
        public static readonly ConsultaClienteUnificadoWSPortTypeClient COMMON_CONSULTCLIENT = Service.Create<ConsultaClienteUnificadoWSPortTypeClient>("ConsultaClienteUnificadoWS");
        public static readonly DetalleProductoPostpagoClienteWSPortTypeClient POSTPAID_PRODUCTDETAIL = Service.Create<DetalleProductoPostpagoClienteWSPortTypeClient>("DetalleProductoPostpagoClienteWS");
        public static readonly ProductosPostpagoClienteWSPortTypeClient POSTPAID_PRODUCTS = Service.Create<ProductosPostpagoClienteWSPortTypeClient>("ProductosPostpagoClienteWS");
        public static readonly ProductosPrepagoClienteWSPortTypeClient PREPAID_PRODUCTS = Service.Create<ProductosPrepagoClienteWSPortTypeClient>("ProductosPrepagoClienteWS");
        public static readonly clienteHFCPortTypeClient POSTPAID_HFC = Service.Create<clienteHFCPortTypeClient>("WSURLClienteHFC");
        public static readonly clienteLTEPortTypeClient POSTPAID_LTE = Service.Create<clienteLTEPortTypeClient>("WSURLClienteLTE");
        public static readonly ebsOperacionesINPortTypeClient PREPAID_OPERATIONS = Service.Create<ebsOperacionesINPortTypeClient>("strUrlWSBono5Prepago");
        public static readonly ValidarCredencialesSUWSPortTypeClient AUDIT_CREDENTIALS = Service.Create<ValidarCredencialesSUWSPortTypeClient>("RedirectWS");
        public static readonly ebsConsultaSaldoPortTypeClient POSTPAID_BALANCE = Service.Create<ebsConsultaSaldoPortTypeClient>("SIACConsultaSaldoWS");
        public static readonly ServiciosPostPagoWSClient POSTPAID_SERVICES = Service.Create<ServiciosPostPagoWSClient>("SIACServiciosComerciales");
        public static readonly EbsSWSAPClient POSTPAID_SAP = Service.Create<EbsSWSAPClient>("SIACDatosSAPWS");
        public static readonly ConsultaDeudaCuentaClient POSTPAID_ACCOUNT = Service.Create<ConsultaDeudaCuentaClient>("SIACPostpagoDeudaNroDocumentoOAC");
        public static readonly EbsDatosPrepagoV2Client PREPAID_SERVICE2 = Service.Create<EbsDatosPrepagoV2Client>("gConstWebServiceDatosPregagoNuevoV2");
        public static readonly ebsConsultaIMRPortTypeClient POSTPAID_IMR = Service.Create<ebsConsultaIMRPortTypeClient>("WSURLPostpagoIMR");
        public static readonly EbsFinanceManagementClient POSTPAID_FINANCE = Service.Create<EbsFinanceManagementClient>("SIACFinanceManagement");
        public static readonly UDBWSPortTypeSOAP11Client COMMON_HLRUDB = Service.Create<UDBWSPortTypeSOAP11Client>("strConnectorHLRUDB");
        public static readonly Claro.SIACU.ProxyService.SIACPre.DataPrePost.ConsultaDatosPrePostWSPortTypeClient PREPAID_CONSULTDATA = Service.Create<Claro.SIACU.ProxyService.SIACPre.DataPrePost.ConsultaDatosPrePostWSPortTypeClient>("strConsultaDatosPrePost"); 
        public static readonly EbsPaqueteDatosClient PREPAID_PAQUETE = Service.Create<EbsPaqueteDatosClient>("PaqueteDatosWS");
        public static readonly ConsultaSeguridadClient COMMON_CONSULTA_SEGURIDAD = Service.Create<ConsultaSeguridadClient>("strWebServiceDBAUDIT");
        public static readonly Validacion4GLTEWSPortTypeClient POSTPAID_VALIDATE4GLTE = Service.Create<Validacion4GLTEWSPortTypeClient>("gConstUrlValida4G");  
        public static readonly PaquetesRoamingPortTypeClient PAQUETE_ROAMING = Service.Create<PaquetesRoamingPortTypeClient>("strWSPaqueteViajero");
        public static readonly NumeroContactosClienteWSPortTypeClient POSTPAID_CUSTOMERCONTACT= Service.Create<NumeroContactosClienteWSPortTypeClient>("BSWContactosClientes");
        public static readonly ConsultaIGVWSPortTypeClient IGV_HFC = Service.Create<ConsultaIGVWSPortTypeClient>("strWebServiceConsultarIGVS");
        public static readonly Claro.SIACU.ProxyService.SIACPost.DocumentoFisico.DocumentosFisicoWSPortTypeClient POSTPAID_DOCUMENT = Service.Create<Claro.SIACU.ProxyService.SIACPost.DocumentoFisico.DocumentosFisicoWSPortTypeClient>("strWebServiceDocumentoAjuste");
        public static readonly Claro.SIACU.ProxyService.SIACPost.DocumentoFisicoV3.DocumentosFisicoWSPortTypeClient POSTPAID_DOCUMENTV3 = Service.Create<Claro.SIACU.ProxyService.SIACPost.DocumentoFisicoV3.DocumentosFisicoWSPortTypeClient>("strWebServiceDocumentoV3");

  public static readonly EbsAuditoriaClient SECURITY_PERMISSIONS = Service.Create<EbsAuditoriaClient>("strWebServiceSecurityPermissions");
        
        public static readonly PaqueteM2MPortTypeClient PAQUETE_M2M = Service.Create<PaqueteM2MPortTypeClient>("strWebServiceRechargesBrokerM2M");
        public static readonly ebsDetalleDocAjustePortTypeClient DETAIL_DOC_AJUSTE = Service.Create<ebsDetalleDocAjustePortTypeClient>("strDetailDocAjuste");
        public static readonly SecurityClient SECURITY_SERVICE = Service.Create<SecurityClient>("strSecurityService");
        public static readonly ActivarVOLTEWSPortTypeClient COMMON_ACTIVARVOLTEWS = Service.Create<ActivarVOLTEWSPortTypeClient>("strActivarVOLTEWS");
        public static readonly RestricTetheVelocWSPortTypeClient RESTRICVELOC = Service.Create<RestricTetheVelocWSPortTypeClient>("strRestricTetheVelocWS");
        public static readonly ebsConsultaClavesPortTypeClient CONSULTCLAVE = Service.Create<ebsConsultaClavesPortTypeClient>("strConsultaClavesWS");
        //<ASANCHEZ>
        public static readonly BSS_ConsultaDatosSIMPortClient CONSULTA_DATOS_SIM = Service.Create<BSS_ConsultaDatosSIMPortClient>("strConsultarDatosSIMWS");
        //</ASANCHEZ>
        public static readonly PeticionAnotacionWSPortTypeClient POSTPAID_ANNOTATION = Service.Create<PeticionAnotacionWSPortTypeClient>("strPeticionAnotacionWS");
 public static readonly ParametrosClarifyPortTypeClient PARAMATERCLARIFY = Service.Create<ParametrosClarifyPortTypeClient>("strParamaterClarify");

        public static readonly estadoCuentaPortClient POSTPAID_STATUS_ACCOUNT = Service.Create<estadoCuentaPortClient>("strStatusAccount");

        public static readonly BloqDesbloqLineaPostpagoWSPortTypeClient POSTPAID_BLOQDESBLOQLINEA = Service.Create<BloqDesbloqLineaPostpagoWSPortTypeClient>("strWebServiceBloqDesbloqLineaPostpago");

        //<ColivingServices> 
        public static readonly Claro.SIACU.ProxyService.SIAC.ConsultaLineaCuenta.ConsultaLineaCuentaWSPortTypeClient CONSULTALINEACUENTA = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIAC.ConsultaLineaCuenta.ConsultaLineaCuentaWSPortTypeClient>("strConsultaLineaCliente");
        public static readonly Claro.SIACU.ProxyService.SIACU.BusquedaLineaCliente.obtenerDatosClienteColivingPortClient OBTENER_DATOSCLIENTEDP = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIACU.BusquedaLineaCliente.obtenerDatosClienteColivingPortClient>("strBusquedaLineaClienteDP");
        public static readonly Claro.SIACU.ProxyService.SIACU.CustomerManagement.BssCustomerManagementPortClient CUSTOMER_MANAGEMENTDP = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIACU.CustomerManagement.BssCustomerManagementPortClient>("strCustomerManagementDP");
        public static readonly Claro.SIACU.ProxyService.SIACU.RetrieveSubscriptions.BssAgreementManagementPortClient AGREEMENT_MANAGEMENTDP = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIACU.RetrieveSubscriptions.BssAgreementManagementPortClient>("strAgreementManagementDP");
        public static readonly Claro.SIACU.ProxyService.SIACU.CustomerInfo.BssCustomerManagementPortClient CUSTOMER_INFODP = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIACU.CustomerInfo.BssCustomerManagementPortClient>("strCustomerInfoDP");
        //</ColivingServices>


        public static readonly Claro.SIACU.ProxyService.SIACU.TechnicalServicesByCustomer.BssServiceProblemManagementPortClient TECHNICAL_SERVICES_BYCUSTOMER = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIACU.TechnicalServicesByCustomer.BssServiceProblemManagementPortClient>("strTechnicalServicesByCustomer");
        public static readonly Claro.SIACU.ProxyService.SIACU.TechnicalServiceDetails.BssServiceProblemManagementPortClient TECHNICAL_SERVICES_DETAILS = Claro.Data.Service.Create<Claro.SIACU.ProxyService.SIACU.TechnicalServiceDetails.BssServiceProblemManagementPortClient>("strTechnicalServicesDetails");

    }
}
