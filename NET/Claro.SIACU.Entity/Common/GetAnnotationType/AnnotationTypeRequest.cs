using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetAnnotationType
{
    [DataContract(Name = "AnnotationRequestCommon")]
    public class AnnotationTypeRequest : Claro.Entity.Request 
    {
        [DataMember]
        public string plataforma { get; set; }
    }
}
