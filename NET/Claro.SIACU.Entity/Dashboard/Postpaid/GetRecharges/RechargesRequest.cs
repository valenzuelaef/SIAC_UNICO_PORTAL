using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetRecharges
{
    [DataContract(Name = "RechargesRequestPostPaid")]
    public class RechargesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string FlagPlatform { get; set; }
        [DataMember]
        public string CurrentUser { get; set; }
        [DataMember]
        public string Contract { get; set; }
        [DataMember]
        public string TypeCustomer { get; set; }
        [DataMember]
        public string StateLine { get; set; }
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string FlagPlataforma { get; set; }
        [DataMember]
        public string FechaExpiracion { get; set; } 
        
    }
}
