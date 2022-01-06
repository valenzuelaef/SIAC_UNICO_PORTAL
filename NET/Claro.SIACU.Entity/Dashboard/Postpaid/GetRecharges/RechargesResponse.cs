using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRecharges
{
    [DataContract(Name = "RechargesResponsePostPaid")]
    public class RechargesResponse
    {
        [DataMember]
        public List<Recharge> Recharges { get; set; }
        [DataMember]
        public List<Service> Services { get; set; }
        [DataMember]
        public Service Service { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ActivationDate { get; set; }
        [DataMember]
        public string MessageRecharges { get; set; }
        [DataMember]
        public bool MessageRechargesVisible { get; set; }
        [DataMember]
        public bool MessageVisible { get; set; }
        [DataMember]
        public string MessageBalanceM2M { get; set; }
        [DataMember]
        public bool MessageBalanceM2MVisible { get; set; }
        [DataMember]
        public string ForeColorStateLine { get; set; }
    }
}
