using Claro.Data;
using Claro.SIACU.ProxyService.SAP.Operations;
using Claro.SIACU.ProxyService.SIAC.HLRConsult;
using Claro.SIACU.ProxyService.SIACPost.Recharges;
using Claro.SIACU.ProxyService.SIACPost.StateAccount;
using Claro.SIACU.ProxyService.SIACPre.Service;
using Claro.SIACU.ProxyService.SIACPost.GetCustomerManagementFS;
using Claro.SIACU.ProxyService.CambioDatosSiacWS;

namespace Claro.SIACU.Data.Configuration
{
    internal struct WebServiceConfiguration 
    {
        public static readonly ebsSimcards PREPAID_SIMCARD = WebService.Create<ebsSimcards>("strWSZsansTransaladoTelf");
        public static readonly ConsultaEstadoCuenta POSTPAID_STATE = WebService.Create<ConsultaEstadoCuenta>("SIACConsultaEstadoCuentaWS");
        public static readonly EbsDatosPrepagoService PREPAID_SERVICE = WebService.Create<EbsDatosPrepagoService>("SIACPrepagoConsultasWS");
        public static readonly HlrSiacWSService COMMON_HLR = WebService.Create<HlrSiacWSService>("strWebServiceConsultaHLRConsult");
        public static readonly ConsultasRecargasSOAP11BindingQSService POSTPAID_RECHARGE = WebService.Create<ConsultasRecargasSOAP11BindingQSService>("SIACConsultasRecargas");
        public static readonly CustomerManagementFS POSTPAID_CUSTOMER_MG_FS = WebService.Create<CustomerManagementFS>("gConstUrlConsultaSaldoCBIOS");
        //Cambio de Datos
        public static readonly CambioDatosSIACWSService CambioDatosWS = WebService.Create<CambioDatosSIACWSService>("strWebServiceCambioDatos");
    
    }
}
