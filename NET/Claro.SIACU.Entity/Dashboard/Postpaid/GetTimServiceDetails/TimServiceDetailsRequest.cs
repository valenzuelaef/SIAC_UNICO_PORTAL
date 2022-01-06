using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTimServiceDetails
{
    [DataContract(Name = "TimServiceDetailsRequestPostPaid")]
    public class TimServiceDetailsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InvoiceNumber { get; set; } 
    }
}
