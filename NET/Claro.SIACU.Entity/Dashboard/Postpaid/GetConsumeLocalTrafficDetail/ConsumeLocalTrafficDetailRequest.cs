using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail
{
    [DataContract(Name = "ConsumeLocalTrafficDetailRequestPostPaid")]
    public class ConsumeLocalTrafficDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
