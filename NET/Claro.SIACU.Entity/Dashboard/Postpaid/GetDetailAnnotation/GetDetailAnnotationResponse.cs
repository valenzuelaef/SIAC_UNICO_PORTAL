using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation
{
    [DataContract(Name = "DetailAnnotationResponsePostPaid")]
    public class GetDetailAnnotationResponse
    {
        [DataMember]
        public DetailAnnotation DetailAnnotation { get; set; } 
    }
}
