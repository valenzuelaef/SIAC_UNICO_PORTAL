using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailLongDistance
{
    [DataContract(Name = "DetailLongDistanceRequestPostPaidPostPaid")]
    public class DetailLongDistanceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TypeCall { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
