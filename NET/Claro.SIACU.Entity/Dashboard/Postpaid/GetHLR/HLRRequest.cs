using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR
{
    [DataContract(Name = "HLRRequestPostPaid")]
    public class HLRRequest : Claro.Entity.Request
    {
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
