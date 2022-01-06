using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "CreditLimitPostPaid")]
    public class CreditLimit
    {

        [Data.DbColumn("V_PERIODO")]
        [DataMember]
        public string PERIODO { get; set; }

        [Data.DbColumn("V_CUSTOMER_ID")]
        [DataMember]
        public string CUSTOMER_ID { get; set; }
        
        [DataMember]
        public string FECHA_MARCA_SMS1 { get; set; }

        [Data.DbColumn("V_MSG_TEXT")]
        [DataMember]
        public string MSG_TEXT { get; set; }

        [DataMember]
        public string FECHA_MARCA_SMS2 { get; set; }

        [DataMember]
        public string TIENE_PAC { get; set; }

        [DataMember]
        public decimal MONTO_PAC { get; set; }

        [DataMember]
        public string NUMERO_PAC { get; set; }

        [DataMember]
        public string FECHA_BLOQ_PROGRAMADA { get; set; }

        [DataMember]
        public string FECHA_BLOQ_EJECUCION { get; set; }

        [DataMember]
        public decimal MONTO_CONSUMIDO { get; set; }

        [Data.DbColumn("N_CARGO_FIJO")]
        [DataMember]
        public decimal CARGO_FIJO { get; set; }

        [Data.DbColumn("N_CARGO_ADICIONAL")]
        [DataMember]
        public decimal CARGO_ADICIONAL { get; set; }

        [DataMember]
        public decimal CARGO_PRORRATEO { get; set; }

        [DataMember]
        public string FECHA_DESBLOQ_EJECUCION { get; set; }

        [DataMember]
        public string DESCRIPCION_PAGO { get; set; }

        [DataMember]
        public string IMPORTE_PENDIENTE { get; set; }

        [DataMember]
        public string TIPO { get; set; }

        [DataMember]
        public string DOCUMENTO { get; set; }

        [Data.DbColumn("I_FLAG")]
        [DataMember]
        public string FLAG { get; set; }
    }
}