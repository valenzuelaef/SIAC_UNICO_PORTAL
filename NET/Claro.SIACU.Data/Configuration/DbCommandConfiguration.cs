using Claro.Data;

namespace Claro.SIACU.Data.Configuration
{
    internal struct DbCommandConfiguration : IDbCommandConfig
    {
        public static readonly IDbCommandConfig SIACU_SIUNSS_DATOS_SERVICIOS = DbCommandConfiguration.Create("SIACU_SIUNSS_DATOS_SERVICIOS");
        public static readonly IDbCommandConfig SIACU_SP_OBT_AFILIACION = DbCommandConfiguration.Create("SIACU_SP_OBT_AFILIACION");
        public static readonly IDbCommandConfig SIACU_SP_CUSTOMER_CLFY = DbCommandConfiguration.Create("SIACU_SP_CUSTOMER_CLFY");
        public static readonly IDbCommandConfig SIACU_SP_CON_DATOS_VENTA = DbCommandConfiguration.Create("SIACU_SP_CON_DATOS_VENTA");
        public static readonly IDbCommandConfig SIACU_SP_DATCLI_X_CUST_NUMREC_ICCID = DbCommandConfiguration.Create("SIACU_SP_DATCLI_X_CUST_NUMREC_ICCID");
        public static readonly IDbCommandConfig SIACU_SP_CLIENTE_JANUS = DbCommandConfiguration.Create("SIACU_SP_CLIENTE_JANUS");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_MAIL = DbCommandConfiguration.Create("SIACU_SP_VALIDA_MAIL");
        public static readonly IDbCommandConfig SIACU_TIM112_SP_GETINFO_CONTR_TEXT = DbCommandConfiguration.Create("SIACU_tim112_sp_getinfo_contr_text");
        public static readonly IDbCommandConfig SIACU_TIM100_CONSULTA_TOPE_CONSUMO = DbCommandConfiguration.Create("SIACU_TIM100_CONSULTA_TOPE_CONSUMO");
        public static readonly IDbCommandConfig SIACU_VALIDA_CLIENTE = DbCommandConfiguration.Create("SIACU_VALIDA_CLIENTE");
        public static readonly IDbCommandConfig SIACU_LISTA_SERVICIOS = DbCommandConfiguration.Create("SIACU_LISTA_SERVICIOS");
        public static readonly IDbCommandConfig SIACU_SP_NRO_DIAS_ACT_DES = DbCommandConfiguration.Create("SIACU_sp_nro_dias_act_des");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_HISTORICO_SIM = DbCommandConfiguration.Create("SIACU_SP_LISTAR_HISTORICO_SIM");
        public static readonly IDbCommandConfig SIACU_SP_CON_DIRECCION_DESPACHO = DbCommandConfiguration.Create("SIACU_SP_CON_DIRECCION_DESPACHO");
        public static readonly IDbCommandConfig SIACU_LISTAR_BANNER = DbCommandConfiguration.Create("SIACU_LISTAR_BANNER");
        public static readonly IDbCommandConfig SIACU_SP_CON_DIRECCION_INSTAL = DbCommandConfiguration.Create("SIACU_SP_CON_DIRECCION_INSTAL");
        public static readonly IDbCommandConfig SIACU_TOLS_OBTENERDATOSDECUENTA = DbCommandConfiguration.Create("SIACU_TOLS_OBTENERDATOSDECUENTA");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1220 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1220");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_DATO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_DATO");
        public static readonly IDbCommandConfig SIACU_OBTENER_PARAMETRO = DbCommandConfiguration.Create("SIACU_OBTENER_PARAMETRO");
        public static readonly IDbCommandConfig SIACU_SP_SEARCH_DATA_TERMINAL_TPI = DbCommandConfiguration.Create("SIACU_SP_SEARCH_DATA_TERMINAL_TPI");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_HUB = DbCommandConfiguration.Create("SIACU_p_consulta_hub");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_EQU = DbCommandConfiguration.Create("SIACU_p_consulta_equ");
        public static readonly IDbCommandConfig SIACU_MGRSS_GETMONTODISPUTA = DbCommandConfiguration.Create("SIACU_MGRSS_GETMONTODISPUTA");
        public static readonly IDbCommandConfig SIACU_SIUNSS_GET_TYPE_PRODUCT = DbCommandConfiguration.Create("SIACU_SIUNSS_GET_TYPE_PRODUCT");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_CLIENTEXCINTILLO = DbCommandConfiguration.Create("SIACU_p_consulta_clientexcintillo");
        public static readonly IDbCommandConfig SIACU_LISTAR_INSTANTANEA_VIGENTE = DbCommandConfiguration.Create("SIACU_LISTAR_INSTANTANEA_VIGENTE");
        public static readonly IDbCommandConfig SIACU_LISTAR_INSTANTANEA_VIGENTE_HFC = DbCommandConfiguration.Create("SIACU_LISTAR_INSTANTANEA_VIGENTE_HFC");
        public static readonly IDbCommandConfig SIACU_SP_COMPORTAMIENTO_PAGO = DbCommandConfiguration.Create("SIACU_SP_COMPORTAMIENTO_PAGO");
        public static readonly IDbCommandConfig SIACU_PROV_CONSULTA_SIAC = DbCommandConfiguration.Create("SIACU_PROV_CONSULTA_SIAC");
        public static readonly IDbCommandConfig SIACU_MGRSS_GETDETMONTODISPUTA = DbCommandConfiguration.Create("SIACU_MGRSS_GETDETMONTODISPUTA");
        public static readonly IDbCommandConfig SIACU_TIM046_CONSULTA_PIN_PUK = DbCommandConfiguration.Create("SIACU_TIM046_CONSULTA_PIN_PUK");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_BAJAS = DbCommandConfiguration.Create("SIACU_SP_LISTA_BAJAS");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_AFILIACION_DEBITO = DbCommandConfiguration.Create("SIACU_SP_LISTAR_AFILIACION_DEBITO");
        public static readonly IDbCommandConfig SIACU_SP_DETALLE_RECARGAS = DbCommandConfiguration.Create("SIACU_SP_DETALLE_RECARGAS");
        public static readonly IDbCommandConfig SIACU_SP_HIST_BONOS = DbCommandConfiguration.Create("SIACU_SP_HIST_BONOS");
        public static readonly IDbCommandConfig SIACU_SP_HIST_TRIACION_DETALLE = DbCommandConfiguration.Create("SIACU_SP_HIST_TRIACION_DETALLE");
        public static readonly IDbCommandConfig SIACU_MGRSS_LISTCASOSCLARIFY = DbCommandConfiguration.Create("SIACU_MGRSS_LISTCASOSCLARIFY");
        public static readonly IDbCommandConfig SIACU_SP_HIST_TRIACION_RFA_FILTROS = DbCommandConfiguration.Create("SIACU_SP_HIST_TRIACION_RFA_FILTROS");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_BONO = DbCommandConfiguration.Create("SIACU_SP_VALIDA_BONO"); //
        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_CANT_BONO = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_CANT_BONO"); //
        public static readonly IDbCommandConfig SIACU_SP_DETALLE_ANOTACION = DbCommandConfiguration.Create("SIACU_SP_DETALLE_ANOTACION");
        public static readonly IDbCommandConfig SIACU_SP_DETALLE_LLAMADAS_NEW = DbCommandConfiguration.Create("SIACU_SP_DETALLE_LLAMADAS_NEW");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_ANOTACIONES = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_ANOTACIONES");
        public static readonly IDbCommandConfig SIACU_LISTAR_BANNER_VIGENTE = DbCommandConfiguration.Create("SIACU_LISTAR_BANNER_VIGENTE");
        public static readonly IDbCommandConfig SIACU_FN_GETESTADOSUBSCRIBER2 = DbCommandConfiguration.Create("SIACU_FN_GETESTADOSUBSCRIBER2");
        public static readonly IDbCommandConfig SIACU_PR_CONSULTA_RA = DbCommandConfiguration.Create("SIACU_PR_CONSULTA_RA");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1460 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1460");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1470 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1470");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_CTA_DEUDA = DbCommandConfiguration.Create("SIACU_SP_OBTENER_CTA_DEUDA");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1405_PRO = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1405_PRO");
        public static readonly IDbCommandConfig SIACU_SP_LINEAS_CLIENTE = DbCommandConfiguration.Create("SIACU_SP_LINEAS_CLIENTE");
        public static readonly IDbCommandConfig SIACU_LISTAR_TRIACION = DbCommandConfiguration.Create("SIACU_LISTAR_TRIACION");
        public static readonly IDbCommandConfig SIACU_SP_PLAN_PREPAGO = DbCommandConfiguration.Create("SIACU_SP_PLAN_PREPAGO");
        public static readonly IDbCommandConfig SIACU_OBT_ULT_BLOQUEO = DbCommandConfiguration.Create("SIACU_OBT_ULT_BLOQUEO");
        public static readonly IDbCommandConfig SIACU_SP_GET_BOLSAS = DbCommandConfiguration.Create("SIACU_SP_GET_BOLSAS");
        public static readonly IDbCommandConfig SIACU_SP_GET_BOLSAS_SERVICIOS = DbCommandConfiguration.Create("SIACU_SP_GET_BOLSAS_SERVICIOS");
        public static readonly IDbCommandConfig SIACU_SP_DETALLE_LLAMADAS = DbCommandConfiguration.Create("SIACU_SP_DETALLE_LLAMADAS");
        public static readonly IDbCommandConfig SIACU_ADMPSS_CON_BENEFICIO = DbCommandConfiguration.Create("SIACU_ADMPSS_CON_BENEFICIO");
        public static readonly IDbCommandConfig SIACU_Tols_Consultartemptag1405_Max = DbCommandConfiguration.Create("SIACU_Tols_Consultartemptag1405_Max");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1225 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1225");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1425 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1425");
        public static readonly IDbCommandConfig SIACU_TOLS_OBTENER_RECIBOS = DbCommandConfiguration.Create("SIACU_TOLS_OBTENER_RECIBOS");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1480 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1480");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARCANTIDADSMS = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARCANTIDADSMS");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_PETICIONES = DbCommandConfiguration.Create("SIACU_SP_LISTAR_PETICIONES");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_PORTABILIDAD = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_PORTABILIDAD");
        public static readonly IDbCommandConfig SIACU_SP_TRIACIONES_HISTORICO = DbCommandConfiguration.Create("SIACU_SP_TRIACIONES_HISTORICO");
        public static readonly IDbCommandConfig SIACU_SP_CON_EQUIPOS = DbCommandConfiguration.Create("SIACU_SP_CON_EQUIPOS");
        public static readonly IDbCommandConfig SIACU_SP_ESTADO_CUENTA = DbCommandConfiguration.Create("SIACU_SP_ESTADO_CUENTA");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_NUMERO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_NUMERO");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_VALOR = DbCommandConfiguration.Create("SIACU_SP_OBTENER_VALOR");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_NUMERO_PORT = DbCommandConfiguration.Create("SIACU_SP_OBTENER_NUMERO_PORT");
        public static readonly IDbCommandConfig SIACU_SP_DATOS_SERVICIO = DbCommandConfiguration.Create("SIACU_SP_DATOS_SERVICIO");
        public static readonly IDbCommandConfig SIACU_CSPI_OAC_P_CONS_CF_ADIC_X_CTA = DbCommandConfiguration.Create("SIACU_CSPI_OAC_P_CONS_CF_ADIC_X_CTA");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_SUBCUENTAS = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_SUBCUENTAS");
        public static readonly IDbCommandConfig SIACU_SP_GET_PIN_PUK = DbCommandConfiguration.Create("SIACU_SP_GET_PIN_PUK");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_VALOR_PORT = DbCommandConfiguration.Create("SIACU_SP_OBTENER_VALOR_PORT");
        public static readonly IDbCommandConfig SIACU_SSSIGA_EXISTE_ACUERDO_SIACPO = DbCommandConfiguration.Create("SIACU_SSSIGA_EXISTE_ACUERDO_SIACPO");
        public static readonly IDbCommandConfig SIACU_SP_EXISTE_ACUERDO_SIACPO = DbCommandConfiguration.Create("SIACU_SP_EXISTE_ACUERDO_SIACPO");
        public static readonly IDbCommandConfig SIACU_SP_QUERY_BILLS_SUMMARY = DbCommandConfiguration.Create("SIACU_SP_QUERY_BILLS_SUMMARY");
        public static readonly IDbCommandConfig SIACU_SP_CONTRATOS_SUSPENDIDOS = DbCommandConfiguration.Create("SIACU_SP_CONTRATOS_SUSPENDIDOS");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_TIPO_DOCIDENTIDAD = DbCommandConfiguration.Create("SIACU_SP_OBTENER_TIPO_DOCIDENTIDAD");
        public static readonly IDbCommandConfig SIACU_SP_RELACION_PLANES_DET = DbCommandConfiguration.Create("SIACU_SP_RELACION_PLANES_DET");
        public static readonly IDbCommandConfig SIACU_SP_SSAPSS_PRODUCTOS_CLIENTE = DbCommandConfiguration.Create("SIACU_SP_SSAPSS_PRODUCTOS_CLIENTE");
        public static readonly IDbCommandConfig SIACU_CONBSS_CUENTAXRUC = DbCommandConfiguration.Create("SIACU_CONBSS_CUENTAXRUC");
        public static readonly IDbCommandConfig SIACU_SP_CARGA_SALDO = DbCommandConfiguration.Create("SIACU_SP_CARGA_SALDO");
        public static readonly IDbCommandConfig SIACU_SP_SALDO_IVR = DbCommandConfiguration.Create("SIACU_SP_SALDO_IVR");
        public static readonly IDbCommandConfig SIACU_OBTENER_CORRELATIVO_TRAN_SALDO = DbCommandConfiguration.Create("SIACU_OBTENER_CORRELATIVO_TRAN_SALDO");
        public static readonly IDbCommandConfig SIACU_PR_CONSUMED_TIME_API = DbCommandConfiguration.Create("SIACU_PR_CONSUMED_TIME_API");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1410 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1410");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1315 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1315");
        public static readonly IDbCommandConfig SIACU_SISACTSS_TCCT_LISTACFG = DbCommandConfiguration.Create("SIACU_SISACTSS_TCCT_LISTACFG");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1455 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1455");
        public static readonly IDbCommandConfig SIACU_P_DATOS_SERVICIOS = DbCommandConfiguration.Create("SIACU_P_DATOS_SERVICIOS");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_RECIBOHIS = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_RECIBOHIS");
        public static readonly IDbCommandConfig SIACU_SP_CUSTOMER_DOC_TYPE = DbCommandConfiguration.Create("SIACU_SP_CUSTOMER_DOC_TYPE");
        public static readonly IDbCommandConfig SIACU_SP_CUSTOMER_MARITAL_STATUS = DbCommandConfiguration.Create("SIACU_SP_CUSTOMER_MARITAL_STATUS");
        public static readonly IDbCommandConfig SIACU_SP_CUSTOMER_OCCUPATION = DbCommandConfiguration.Create("SIACU_SP_CUSTOMER_OCCUPATION");
        public static readonly IDbCommandConfig SIACU_SP_PREPAID_REGISTRATION_REASON = DbCommandConfiguration.Create("SIACU_SP_PREPAID_REGISTRATION_REASON");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TELEFONO = DbCommandConfiguration.Create("SIACU_SP_LISTA_TELEFONO");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_TELEFONO_LTE = DbCommandConfiguration.Create("SIACU_SP_LISTA_TELEFONO_LTE");
        public static readonly IDbCommandConfig SIACU_SISACTSS_TCCC_LISTACFG = DbCommandConfiguration.Create("SIACU_SISACTSS_TCCC_LISTACFG");
        public static readonly IDbCommandConfig SIACU_SISACT_CON_TIPO_DOC = DbCommandConfiguration.Create("SIACU_SISACT_CON_TIPO_DOC");
        public static readonly IDbCommandConfig SIACU_SISACTSS_DATOS_LISTA = DbCommandConfiguration.Create("SIACU_SISACTSS_DATOS_LISTA");
        public static readonly IDbCommandConfig SIACU_SISACTSS_CSCT_LISTA = DbCommandConfiguration.Create("SIACU_SISACTSS_CSCT_LISTA");
        public static readonly IDbCommandConfig SIACU_SISACTSS_TCXC_LISTACFG = DbCommandConfiguration.Create("SIACU_SISACTSS_TCXC_LISTACFG");
        public static readonly IDbCommandConfig SIACU_SISACTSU_GUARDAR_CONTACTOS = DbCommandConfiguration.Create("SIACU_SISACTSU_GUARDAR_CONTACTOS");
        public static readonly IDbCommandConfig SIACU_MANTSS_OBTENER_VALOR_PARAMETRO = DbCommandConfiguration.Create("SIACU_MANTSS_OBTENER_VALOR_PARAMETRO");
        public static readonly IDbCommandConfig SIACU_SP_CREATE_CONTACT_PRE_NOUSER = DbCommandConfiguration.Create("SIACU_SP_CREATE_CONTACT_PRE_NOUSER");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_DOC_CVE_CSR = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_DOC_CVE_CSR");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARDETTP = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARDETTP");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_SERVICIOS_USADOS_HFC = DbCommandConfiguration.Create("SIACU_SP_LISTA_SERVICIOS_USADOS_HFC");
        public static readonly IDbCommandConfig SIACU_TIMGSS_GETTIPODOC = DbCommandConfiguration.Create("SIACU_TIMGSS_GETTIPODOC");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARDETTM = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARDETTM");
        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_LISTAS = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_LISTAS");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARCANTP = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARCANTP");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARCANTM = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARCANTM");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARDETTPE = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARDETTPE");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARDETTME = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARDETTME");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARCANTPE = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARCANTPE");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARCANTME = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARCANTME");
        public static readonly IDbCommandConfig SIACU_TIM163_VENTA_CONVERG = DbCommandConfiguration.Create("SIACU_TIM163_VENTA_CONVERG");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA");
        public static readonly IDbCommandConfig SIACU_SP_SHOW_LIST_ELEMENT = DbCommandConfiguration.Create("SIACU_SP_SHOW_LIST_ELEMENT");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_CODIGO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_CODIGO");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_POSTT_SERVICIOPROG = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_POSTT_SERVICIOPROG");
        public static readonly IDbCommandConfig SIACU_INSERTAR_INSTANTANEA_NEW_HFC = DbCommandConfiguration.Create("SIACU_INSERTAR_INSTANTANEA_NEW_HFC");
        public static readonly IDbCommandConfig SIACU_ACTUALIZAR_INSTANTANEA_NEW = DbCommandConfiguration.Create("SIACU_ACTUALIZAR_INSTANTANEA_NEW");
        public static readonly IDbCommandConfig SIACU_ELIMINAR_INSTANTANEA = DbCommandConfiguration.Create("SIACU_ELIMINAR_INSTANTANEA");
        public static readonly IDbCommandConfig SIACU_SP_P_CONSULTA_EQU = DbCommandConfiguration.Create("SIACU_SP_P_CONSULTA_EQU");
        public static readonly IDbCommandConfig SIACU_LISTAR_INSTANTANEAS = DbCommandConfiguration.Create("SIACU_LISTAR_INSTANTANEAS");
        public static readonly IDbCommandConfig SIACU_OBTENER_INSTANTANEA_BY_ID = DbCommandConfiguration.Create("SIACU_OBTENER_INSTANTANEA_BY_ID");
        public static readonly IDbCommandConfig SIACU_INSERTAR_ACTUALIZAR_BANNER = DbCommandConfiguration.Create("SIACU_INSERTAR_ACTUALIZAR_BANNER");
        public static readonly IDbCommandConfig SIACU_DESACTIVAR_BANNER = DbCommandConfiguration.Create("SIACU_DESACTIVAR_BANNER");
        public static readonly IDbCommandConfig SIACU_OBTENER_BANNER_BY_ID = DbCommandConfiguration.Create("SIACU_OBTENER_BANNER_BY_ID");
        public static readonly IDbCommandConfig SIACU_OBTENER_LISTA_USUARIO_VIP = DbCommandConfiguration.Create("SIACU_OBTENER_LISTA_USUARIO_VIP");
        public static readonly IDbCommandConfig SIACU_BUSCA_CUOTAS_ROAMING = DbCommandConfiguration.Create("SIACU_BUSCA_CUOTAS_ROAMING");
        public static readonly IDbCommandConfig SIACU_CONSULTA_SERVICIO_COMERCIAL = DbCommandConfiguration.Create("SIACU_CONSULTA_SERVICIO_COMERCIAL");
        public static readonly IDbCommandConfig SIACU_SP_CORRELATIVO_INSTANTANEA = DbCommandConfiguration.Create("SIACU_SP_CORRELATIVO_INSTANTANEA");
        public static readonly IDbCommandConfig SIACU_SP_INSTANTANEAS_CABECERA = DbCommandConfiguration.Create("SIACU_SP_INSTANTANEAS_CABECERA");
        public static readonly IDbCommandConfig SIACU_SP_INSTANTANEAS_DETALLE = DbCommandConfiguration.Create("SIACU_SP_INSTANTANEAS_DETALLE");
        public static readonly IDbCommandConfig SIACU_TFUN015_ESTADO_SERVICIO = DbCommandConfiguration.Create("SIACU_TFUN015_ESTADO_SERVICIO");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_BL_TIT = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_BL_TIT");
        
        public static readonly IDbCommandConfig SP_MY_WIPBIN = DbCommandConfiguration.Create("SP_MY_WIPBIN");
        public static readonly IDbCommandConfig SP_MY_CASES_WIPBIN = DbCommandConfiguration.Create("SP_MY_CASES_WIPBIN");
        public static readonly IDbCommandConfig SP_MY_QUEUES = DbCommandConfiguration.Create("SP_MY_QUEUES");
        public static readonly IDbCommandConfig SP_QUEUES = DbCommandConfiguration.Create("SP_QUEUES");
        public static readonly IDbCommandConfig SP_CASES_QUEUE_USER = DbCommandConfiguration.Create("SP_CASES_QUEUE_USER");
        public static readonly IDbCommandConfig SP_NOTES_CASE = DbCommandConfiguration.Create("SP_NOTES_CASE");
        public static readonly IDbCommandConfig SP_QUERY_CASE_BY_ID = DbCommandConfiguration.Create("SP_QUERY_CASE_BY_ID");
        public static readonly IDbCommandConfig SIACU_MGRSS_DATCASO = DbCommandConfiguration.Create("SIACU_MGRSS_DATCASO");
        public static readonly IDbCommandConfig SIACU_MGRSS_HISTCASO_USU = DbCommandConfiguration.Create("SIACU_MGRSS_HISTCASO_USU");
        public static readonly IDbCommandConfig SIACU_MGRSS_CIERREXCASO = DbCommandConfiguration.Create("SIACU_MGRSS_CIERREXCASO");
        public static readonly IDbCommandConfig SIACU_MGRSS_REAPERTURASXCASO = DbCommandConfiguration.Create("SIACU_MGRSS_REAPERTURASXCASO");
        public static readonly IDbCommandConfig SIACU_MGRSS_HISTRPTCASO = DbCommandConfiguration.Create("SIACU_MGRSS_HISTRPTCASO");
        public static readonly IDbCommandConfig SIACU_MGRSS_DATOSRECLAMO = DbCommandConfiguration.Create("SIACU_MGRSS_DATOSRECLAMO");
        public static readonly IDbCommandConfig SIACU_MGRSS_ENVIOMD = DbCommandConfiguration.Create("SIACU_MGRSS_ENVIOMD");
        public static readonly IDbCommandConfig SIACU_SP_HIST_ACCIONS_HFC = DbCommandConfiguration.Create("SIACU_SP_HIST_ACCIONS_HFC");
        public static readonly IDbCommandConfig SIACU_SP_TFUN007_GET_STATUS_COID = DbCommandConfiguration.Create("SIACU_SP_TFUN007_GET_STATUS_COID");
        public static readonly IDbCommandConfig SIACU_SP_CLIENTE_CBIOS = DbCommandConfiguration.Create("SIACU_SP_CLIENTE_CBIOS");
        public static readonly IDbCommandConfig SIACU_SIACSS_PARAMETROSBSCS = DbCommandConfiguration.Create("SIACU_SIACSS_PARAMETROSBSCS");

        public static readonly IDbCommandConfig SIACU_P_CONSULTA_IDPLANOXCOID = DbCommandConfiguration.Create("SIACU_P_CONSULTA_IDPLANOXCOID");

        public static readonly IDbCommandConfig SIACU_SP_OBTENER_MOTIVO_CANCELACION = DbCommandConfiguration.Create("SIACU_SP_OBTENER_MOTIVO_CANCELACION");
        public static readonly IDbCommandConfig SIACU_BSCSS_SEGMENT_CLIENTE = DbCommandConfiguration.Create("SIACU_BSCSS_SEGMENT_CLIENTE");
        public static readonly IDbCommandConfig SIACU_OBTENER_INTERACCION = DbCommandConfiguration.Create("SIACU_OBTENER_INTERACCION");
        public static readonly IDbCommandConfig SIACU_SP_DOC_OAC = DbCommandConfiguration.Create("SIACU_SP_DOC_OAC");
 public static readonly IDbCommandConfig SIACU_SGASS_SUCURSALES_CLIENTE = DbCommandConfiguration.Create("SIACU_SGASS_SUCURSALES_CLIENTE");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
        public static readonly IDbCommandConfig SIACU_SIACSS_CLIENTE_CUSTOMER = DbCommandConfiguration.Create("SIACU_SIACSS_CLIENTE_CUSTOMER");
        public static readonly IDbCommandConfig SIACU_SP_CREATE_CONTACT_POSTPAGO = DbCommandConfiguration.Create("SIACU_SP_CREATE_CONTACT_POSTPAGO");
        public static readonly IDbCommandConfig SIACU_SIACSU_CLIENTE_NACIONALIDAD = DbCommandConfiguration.Create("SIACU_SIACSU_CLIENTE_NACIONALIDAD");
		public static readonly IDbCommandConfig SIACU_SIACSU_UPDATE_DATOS_HFC = DbCommandConfiguration.Create("SIACU_SIACSU_UPDATE_DATOS_HFC");
       //PROY-140073 Coliving - Inicio
        public static readonly IDbCommandConfig SIACU_SP_SIACSS_PARAMETROS = DbCommandConfiguration.Create("SIACU_SP_SIACSS_PARAMETROS");
        //Coliving - FIN

        public static readonly IDbCommandConfig SIACU_SP_BUSCAR_OST = DbCommandConfiguration.Create("SIACU_SP_BUSCAR_OST");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_DATOS_CLIENTE = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_DATOS_CLIENTE");
        //proyAjustes - INI
        public static readonly IDbCommandConfig SIACU_OBTENER_TIPIFICACIONES = DbCommandConfiguration.Create("SIACU_SP_OBTENER_TIPIFICACION");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY = Create("SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY");
        public static readonly IDbCommandConfig SIACU_SP_EVALUAR_MONTO_AUTORIZAR = Create("SIACU_SP_EVALUAR_MONTO_AUTORIZAR");
        //proyAjustes - FIN

        //INICIATIVA 616
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_HUB_TOBE = DbCommandConfiguration.Create("SIACU_p_consulta_hub_tobe");
        //INICIATIVA 616


        #region [Fields]

        private string m_name;

        #endregion

        #region [ Properties ]

        #region Name

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        #endregion

        #endregion

        #region SetName

        private void SetName(string name)
        {
            this.m_name = name;
        }

        #endregion

        private static IDbCommandConfig Create(string name)
        {
            DbCommandConfiguration helper = new DbCommandConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}
