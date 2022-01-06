using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DetailAmountDisputePostPaid")]
    public class DetailAmountDispute
    {
        [Data.DbColumn("NRO_RECIBO")]
        [DataMember]
        public string NRO_RECIBO { get; set; }

        [Data.DbColumn("FECHA_EMI_DOC")]
        [DataMember]
        public string FECHA_EMI_DOC { get; set; }

        [Data.DbColumn("MONTO_TOTAL_DOC")]
        [DataMember]
        public string MONTO_TOTAL_DOC { get; set; }

        [Data.DbColumn("MONTO_RECLAMO")]
        [DataMember]
        public string MONTO_RECLAMO { get; set; }

        [Data.DbColumn("FECHA_REG_RECLAMO")]
        [DataMember]
        public string FECHA_REG_RECLAMO { get; set; }

        [Data.DbColumn("ESTADO_RECLAMO")]
        [DataMember]
        public string ESTADO_RECLAMO { get; set; }

        [Data.DbColumn("MOTIVO")]
        [DataMember]
        public string MOTIVO { get; set; }

        [Data.DbColumn("FECHA_CIERRE")]
        [DataMember]
        public string FECHA_CIERRE { get; set; }

        [Data.DbColumn("CASO_CLARIFY")]
        [DataMember]
        public string CASO_CLARIFY { get; set; }

        [Data.DbColumn("ULTIMO_DIA")]
        [DataMember]
        public string ULTIMO_DIA { get; set; }
    }
}