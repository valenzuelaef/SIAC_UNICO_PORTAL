using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetCallDetail
{
    [DataContract(Name = "CallDetailRequestPrePaid")]
    public class CallDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string VozFidel { get; set; }
        [DataMember]
        public string Voice1Promo { get; set; }
        [DataMember]
        public string SmsFidel { get; set; }
        [DataMember]
        public string SaldoMms { get; set; }
        [DataMember]
        public string SolesPromo { get; set; }
        [DataMember]
        public string SaldoSms { get; set; }
        [DataMember]
        public string Voice2Promo { get; set; }
        [DataMember]
        public string Promo1 { get; set; }
        [DataMember]
        public string Promo2 { get; set; }
        [DataMember]
        public string GPRS { get; set; }
    }
}
