using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "RechargePostPaid")]
    public class Recharge
    {
        [DataMember]
        public string NOMBRE_APLICACION { get; set; }
        [DataMember]
        public string ID_TRANSACCION { get; set; }
        [DataMember]
        public string IP_APLICACION { get; set; }
        [DataMember]
        public string USUARIO_APLICACION { get; set; }
        [DataMember]
        public string MSISDN { get; set; }
        [DataMember]
        public string ID_CLIENTE { get; set; }
        [DataMember]
        public string ID{ get; set; }
        [DataMember]
        public string POID { get; set; } 
        [DataMember]
        public string PLATAFORMA { get; set; }
        [DataMember]
        public string PLAN { get; set; }
        [DataMember]
        public string PRODUCTO { get; set; }
        [DataMember]
        public string ID_BOLSA { get; set; }
        [DataMember]
        public string BOLSA { get; set; }
        [DataMember]
        public string TIPO_BOLSA { get; set; }
        [DataMember]
        public string SALDO { get; set; }
        [DataMember]
        public string UNIDAD { get; set; }
        [DataMember]
        public string TOPE_CONSUMO { get; set; }
        [DataMember]
        public string CONSUMO { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string TIPO { get; set; }
        [DataMember]
        public string UNIFICADA { get; set; }
        [DataMember]
        public string FECHA_ACTIVACION { get; set; }
        [DataMember]
        public string FECHA_EXPIRACION { get; set; }
        [DataMember]
        public string CREDITO { get; set; }
        [DataMember]
        public string TIPO_CREDITO { get; set; }
        [DataMember]
        public string TARIFA { get; set; }
        [DataMember]
        public string ID_TARIFA { get; set; }
        [DataMember]
        public string OPCIONAL1 { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public string TIPO_PAQUETE { get; set; }
        [DataMember]
        public string DESC_ZONA { get; set; }
        [DataMember]
        public string TIPO_UNIDAD { get; set; }
        [DataMember]
        public string DESTINO { get; set; }
        [DataMember]
        public string DETALLE_ASESOR { get; set; }
        [DataMember]
        public string NOMBRE_COMERCIAL_CLIENTE { get; set; }
        [DataMember]
        public string TOTAL { get; set; }
        [DataMember]
        public string GRUPO_BOLSA { get; set; }

        [DataMember]
        public string ID_ORDEN_BOLSA { get; set; }
        [DataMember]
        public string INDEX_UNIFICADA { get; set; }
        [DataMember]
        public string ZONA_DIF { get; set; }

    }
}
