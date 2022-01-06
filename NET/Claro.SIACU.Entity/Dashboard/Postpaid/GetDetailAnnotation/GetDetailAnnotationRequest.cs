using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation
{
    [DataContract(Name = "DetailAnnotationRequestPostPaid")]
    public class GetDetailAnnotationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string NumberTickler { get; set; }


    }
}
