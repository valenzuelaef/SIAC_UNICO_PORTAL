using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSubAccount
{
    [DataContract(Name = "SubAccountRequestPostPaid")]
    public class SubAccountRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Balance { get; set; }
        [DataMember]
        public string ExpirationDate { get; set; }
        [DataMember]
        public string Surnames { get; set; }
        [DataMember]
        public string ParentAccount { get; set; }
        [DataMember]
        public string AccountID { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Level { get; set; }
        [DataMember]
        public string CustomerID { get; set; }

    }
}
