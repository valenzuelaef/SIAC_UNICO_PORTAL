using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "EquipmentPostPaid")]
   public class Equipment
    {

        [Data.DbColumn("P_CUSTOMER_ID")]
        [DataMember]
        public string CUSTOMER_ID { get; set; }

        [Data.DbColumn("CODIGO")]
        [DataMember]
        public string CODIGO { get; set; }

        [Data.DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION { get; set; }

        [Data.DbColumn("SERIE")]
        [DataMember]
        public string SERIE { get; set; }

        [Data.DbColumn("MODELO")]
        [DataMember]
        public string MODELO { get; set; }

        [Data.DbColumn("PRECIO")]
        [DataMember]
        public string PRECIO { get; set; }

        [Data.DbColumn("CAMPANA")]
        [DataMember]
        public string CAMPANA { get; set; }

        [Data.DbColumn("COMBO")]
        [DataMember]
        public string COMBO { get; set; }

        [Data.DbColumn("PDV")]
        [DataMember]
        public string PDV { get; set; }

        [Data.DbColumn("NRO_CUOTA")]
        [DataMember]
        public string NRO_CUOTA { get; set; }

        [Data.DbColumn("MONTO_CUOTA")]
        [DataMember]
        public string MONTO_CUOTA { get; set; }

        [Data.DbColumn("MONTO_INICIAL_CUOTA")]
        [DataMember]
        public string MONTO_INICIAL_CUOTA { get; set; }

        [Data.DbColumn("FECHA_VENTA")]
        [DataMember]
        public string FECHA_VENTA { get; set; }




    }
}
