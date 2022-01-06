using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "FixedChargeDetailTimProOnePostPaid")]
    public class FixedChargeDetailTimProOne
    {
        [Data.DbColumn("Msisdn")]
        [DataMember]
        public string Msisdn { get; set; }

        [Data.DbColumn("Descripcion")]
        [DataMember]
        public string Description { get; set; }

        [Data.DbColumn("FromDate")]
        [DataMember]
        public string FromDate { get; set; }

        [Data.DbColumn("ToDate")]
        [DataMember]
        public string ToDate { get; set; }

        [Data.DbColumn("Amount")]
        [DataMember]
        public decimal Amount { get; set; }

        [Data.DbColumn("RatePlan")]
        [DataMember]
        public string RatePlan { get; set; }

        [Data.DbColumn("Fu1")]
        [DataMember]
        public string Fu1 { get; set; }

        [Data.DbColumn("Fu2")]
        [DataMember]
        public string Fu2 { get; set; }

        [Data.DbColumn("Fu3")]
        [DataMember]
        public string Fu3 { get; set; }

        [Data.DbColumn("Fu4")]
        [DataMember]
        public string Fu4 { get; set; }

        [Data.DbColumn("MsgTexto2")]
        [DataMember]
        public string MsgTexto2 { get; set; }

        [Data.DbColumn("MsgTexto4")]
        [DataMember]
        public string MsgTexto4 { get; set; }

        [Data.DbColumn("MsgTexto5")]
        [DataMember]
        public int MsgTexto5 { get; set; }

    }
}
