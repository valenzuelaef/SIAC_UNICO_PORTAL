using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEY = Claro.ConfigurationManager;
namespace Claro.SIACU.Data.Configuration
{
    internal struct RestServiceConfiguration
    {
        public static readonly string PLAN_MASIVO = "strPlanMasivoREST";
        public static readonly string OBTENER_SOLICITUD = "strRutaDataPowerObtenerSolicitud";
        public static readonly string REGISTER_SOLICITUD = "strRutaDataPowerRegistrarSolicitud";
        public static readonly string OBTENER_DOCUMENTO_SOLICITUD = "strRutaDataPowerObtenerDocumentoSolicitud";
        public static readonly string OBTENER_STATUS_BONO_FULLCLARO = "strRutaDataPowerObtenerStatusFullClaro";
        public static readonly string OBTENER_STATUS_BONO_FULLCLARO_DOC = "strRutaDPObtenerStatusFullClaroDoc";

        //PROY-140464 Ajustes
        public static readonly string OBTENER_ESTADO_CUENTA_DP = "strRutaObtenerEstadoCuentaDP";
        public static readonly string OBTENER_MOTIVO_ANULACION_DP = "strRutaObtenerMotivoAnulacionDP";
        public static readonly string ANULAR_FACTURA_DP = "strRutaAnulacionFacturaDP";
        public static readonly string EVALUAR_MONTO_MAXIMO_DP = "strRutaEvaluarMontoMaximoDP";
        public static readonly string OBTENER_PERFILES_DP = "strRutaObtenerPerfilesDP";
   

        public static readonly string OBTENER_TIPO_PRODUCTO_TOBE = "strGetTypeProductTobeRest";
        public static readonly string OBTENER_TIPO_PRODUCTO_DAT_TOBE = "strGetTypeProductDatTobeRest";
        public static readonly string OBTENER_CUENTA_TEL_CUST_TOBE = "strGetAccountTelephoneCustomerTobeRest";
        public static readonly string OBTENER_DATOS_CLIENTE_TOBE = "strGetDataCustomerTobeRest";
        public static readonly string OBTENER_DATOS_LINEA_TOBE = "strGetDataLineTobeRest";

        //Inciativa 218
        public static readonly string OBTENER_ESTADO_CUENTA_LINEA_TOBE = "strGetDataAccountStateTobeRest";
        public static readonly string OBTENER_HISTORICO_ESTADO_LINEA_TOBE = "strGetDataLineHistoryTobeRest";
        public static readonly string OBTENER_FACTURA = "strGetInvoice";
        public static readonly string OBTENER_DATOS_INVOICE_TOBE = "strGetDataInvoiceAndDetailTobeRest";
        public static readonly string OBTENER_COMPORTAMIENTO_PAGO_TOBE = "strGetPaymentBehaviorTobeRest";
        public static readonly string OBTENER_FORMA_PAGO_TOBE = "strGetPaymentMethodTobeRest";
        public static readonly string OBTENER_USAGE_THRESHOLDS_COUNTER = "strGetUsageThresholdsCounter";
        public static readonly string OBTENER_DETALLE_PAGO = "strGetPaymentMethodDetailRest";
        public static readonly string OBTENER_PETICIONES_LEGACY = "strGetPetitionLegacyRest";
        public static readonly string OBTENER_HISTORICO_ACCIONES_TOBE = "strGetHistoryActionsTobe";
        public static readonly string CONSULTAR_CONSUMO_SALDO = "strGetConsultBalanceConsumptionTobeRest";
        public static readonly string CONSULTAR_ADDONS = "strGetConsultAddOns";

        //INICIATIVA 616
        public static readonly string CONSULTAR_CONSUMO_SALDO_FIJA = "strGetConsultBalanceFijaTobeRest";


        public static readonly string OBTENER_SERVICIOS_POR_CSIDPUB = "strGetServiceByCustomerCodeTobeRest";
        public static readonly string OBTENER_DATA_HISTORICO = "strGetDataHistoryRestTobe";
        public static readonly string OBTENER_DATA_ANOTACIONES = "STRGetAnnotationWSRestTobe";

        public static readonly string OBTENER_HISTORIAL_PLAN_POR_COID = "strGetHistoryServiceLineTobeRest";
        public static readonly string OBTENER_LISTADO_HISTORICO_IMSI = "StrGetHistorySIMRestTobe";
        public static readonly string OBTENER_LISTA_SERVICIOS_CONTRATO = "strGetContractServicesRest";
        public static readonly string OBTENER_TELEFONO_CONTRATO = "strGetPhoneContractClientRest";
        public static readonly string OBTENER_STATUS_EQUIPO = "strRutaDPObtenerStatusEquipo";
        public static readonly string OBTENER_HISTORICO_ENTREGAS = "strGetHistoricoEntregasRest";
        public static readonly string OBTENER_PACKAGE_HISTORY_TOBE = "strObtenerHistoricoPaquetes";
        public static readonly string VALIDAR_CLIENTE = "strValidarCliente";
        public static readonly string VALIDAR_LINEA = "strValidarLinea";

        public static readonly string LISTA_BLOQUEO_DESBLOQUEO = "strObtenerListaBloDesblo";
		//INICIATIVA-616
        public static readonly string OBTENER_PRODUCTOS_X_CUSTOMER = "strobtenerProductosXCustomer";
        public static readonly string GET_CURRENCY_SERVICES_CBIO = "strCurrencyServicesByPlanCbio";//INICIATIVA-616
        public static readonly string OBTENER_PETICIONES_FIJA = "strGetPetitionsTobeFijaRest";
        public static readonly string GET_LISTAR_LINEAS = "strGetListarLineas";
        public static readonly string OBTENER_DATOS_FACTURA_TOBE = "strObtenerDatosFactura"; 
        //INICIATIVA-616
    }
}
