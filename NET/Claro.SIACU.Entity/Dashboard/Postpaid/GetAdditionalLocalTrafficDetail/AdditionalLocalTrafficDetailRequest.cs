using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail
{
    [DataContract(Name = "AdditionalLocalTrafficDetailRequestPostPaid")]
    public class AdditionalLocalTrafficDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
