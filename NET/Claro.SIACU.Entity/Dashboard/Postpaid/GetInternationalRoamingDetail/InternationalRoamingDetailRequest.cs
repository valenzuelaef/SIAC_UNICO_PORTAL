using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetInternationalRoamingDetail
{
    [DataContract(Name = "InternationalRoamingDetailRequestPostPaid")]
    public class InternationalRoamingDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
