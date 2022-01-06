using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations
{
    [DataContract(Name = "AnnotationsResponsePostPaid")]
    public class AnnotationsResponse
    {
        [DataMember]
        public List<Annotation> ListAnnotation { get; set; }
    }
}
