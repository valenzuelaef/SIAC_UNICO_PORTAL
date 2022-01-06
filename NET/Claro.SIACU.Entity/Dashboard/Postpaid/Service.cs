using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "ServicePostPaid")]
    public class Service
    {
        [DataMember]
        public DateTime FECHA_VALIDO_DESDE { get; set; }

        [DataMember]
        public string ESTADO { get; set; }


        [DataMember]
        public string Tethering { get; set; }

        [DataMember]
        public string Degradation { get; set; }
        [DataMember]
        public string CODID { get; set; }

        [DataMember]
        public string CAMPANIA { get; set; }

        [DataMember]
        public DateTime FECHA_ESTADO { get; set; }


        [DataMember]
        [Data.DbColumn("ESTADO")]
        public string ESTADO_LINEA { get; set; }

        [DataMember]
        public string MOTIVO { get; set; }


        [DataMember]
        [Data.DbColumn("PLAN_TARIFARIO")]
        public string PLAN { get; set; }

        [DataMember]
        public string PLAZO_CONTRATO { get; set; }

        [DataMember]
        public string NROICCID { get; set; }

        [DataMember]
        public string NROIMSI { get; set; }

        [DataMember]
        public string VENDEDOR { get; set; }

        [DataMember]
        public string FEC_ACTIVACION { get; set; }

        [DataMember]
        public string FEC_DESACTIVACION { get; set; }

        [DataMember]
        public string FLAG_PLATAFORMA { get; set; }

        [DataMember]
        public string PIN1 { get; set; }

        [DataMember]
        public string PIN2 { get; set; }

        [DataMember]
        public string PUK1 { get; set; }

        [DataMember]
        public string PUK2 { get; set; }

        [DataMember]
        public string COD_PLAN_TARIFARIO { get; set; }
        [DataMember]
        public string PLAN_TARIFARIO { get; set; }

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string NRO_CELULAR { get; set; }


        [DataMember]
        [Data.DbColumn("CO_ID")]
        public string CONTRATO_ID { get; set; }

        [DataMember]
        public string COD_RETORNO { get; set; }

        [DataMember]
        public string PROVIDER_ID { get; set; }

        [DataMember]
        public string MSISDN { get; set; }

        [DataMember]
        public string BANCA_MOVIL { get; set; }

        [DataMember]
        public string TIPO_SOLUCION { get; set; }


        [DataMember]
        [Data.DbColumn("TIPO_PROD")]
        public string TIPO_PRODUCTO { get; set; }

        [DataMember]
        public string TOPE_CONSUMO { get; set; }
        [DataMember]
        public string COID_PUB { get; set; }


        [DataMember]
        [Data.DbColumn("TELEFONIA")]
        public string TELEFONIA { get; set; }


        [DataMember]
        [Data.DbColumn("INTERNET")]
        public string INTERNET { get; set; }


        [DataMember]
        [Data.DbColumn("CABLE_TV")]
        public string CABLETV { get; set; }

        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string SERVICEPAQUETE { get; set; }

        [DataMember]
        public bool ESTADO_SERVICEPAQUETE { get; set; }

        [DataMember]
        public string FLAG_TFI { get; set; }

        [DataMember]
        public string TFI { get; set; }

        [DataMember]
        public bool ESTADO_TFI { get; set; }


        [DataMember]
        public string VENTA { get; set; }

        [DataMember]
        public string SERVICECOMBO { get; set; }

        [DataMember]
        public bool ESTADO_SERVICECOMBO { get; set; }

        [DataMember]
        public DateTime INTRODUCIDO_EL { get; set; }

        [DataMember]
        public string INTRODUCIDO_POR { get; set; }

        [DataMember]
        public DateTime VALIDO_DESDE { get; set; }
        [DataMember]
        public string CAMBIADO_POR { get; set; }

        [DataMember]
        public string NoEs3Play { get; set; }

        [DataMember]
        public string PAQUETE { get; set; }

        [DataMember]
        public string PAQUETE_ID { get; set; }

        [DataMember]
        public string CUOTA { get; set; }

        [DataMember]
        public string SALDO_PRINCIPAL { get; set; }

        [DataMember]
        public string FECHA_EXPIRACION_SALDO { get; set; }

        [DataMember]
        public string CAMBIOS_TRIOS_GRATIS { get; set; }

        [DataMember]
        public string CAMBIOS_TARIFA_GRATIS { get; set; }

        [DataMember]
        public string FECHA_DOL { get; set; }

        [DataMember]
        public string FECHA_EXPIRACION_LINEA { get; set; }

        [DataMember]
        public string SUBSCRIBIR_ESTADO { get; set; }

        [DataMember]
        public string CNT_NUMERO { get; set; }

        [DataMember]
        public string CNT_POSIBLE { get; set; }

        [DataMember]
        public string ESTADO_IMSI { get; set; }

        [DataMember]
        public string TIPO_TRIACION { get; set; }

        [DataMember]
        public string NUMERO_FAMILIA_AMIGOS { get; set; }

        [DataMember]
        public string CODIGO_RESPUESTA { get; set; }

        [DataMember]
        public List<Account> LISTA_ACCOUNT { get; set; }

        [DataMember]
        public List<Trio> LISTA_TRIO { get; set; }

        [DataMember]
        public bool Portability { get; set; }

        [DataMember]
        public string PortabilityState { get; set; }

        [DataMember]
        public bool ESTADO_DTH { get; set; }

        [DataMember]
        public bool Roaming { get; set; }
        [DataMember]
        public string TypeProductText { get; set; }

        [DataMember]
        public string SEGMENTO_CLIENTE { get; set; }

        [DataMember]
        public string StatusFullClaro { get; set; }
        [DataMember]
        public string StatusFullClaroCode { get; set; }
        [DataMember]
        public string statusFullClaroBono { get; set; }
        [DataMember]
        public string statusFullClaroBonoCode { get; set; }
        [DataMember]
        public string FirstReasonState { get; set; }
        [DataMember]
        public string LastReasonState { get; set; }
        [DataMember]
        public DateTime LastReasonStateDate { get; set; }

        [DataMember]
        public string topeConsumo { get; set; }
        [DataMember]
        public string bolsasAdicionales { get; set; }


    }
}