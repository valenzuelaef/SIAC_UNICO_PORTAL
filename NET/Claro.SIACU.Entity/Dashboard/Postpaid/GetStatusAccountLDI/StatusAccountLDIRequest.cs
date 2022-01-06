using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetStatusAccountLDI
{
    [DataContract(Name = "StatusAccountLDIRequestPostPaid")]
    public class StatusAccountLDIRequest : Claro.Entity.Request
    {
         [DataMember]
         public string CustomerId { get; set; }

         [DataMember]
         public string Type { get; set; }
    }
}
