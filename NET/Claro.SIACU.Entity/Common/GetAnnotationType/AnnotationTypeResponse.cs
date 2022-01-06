using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetAnnotationType
{
    [DataContract(Name = "AnnotationTypeResponseCommon")]
    public class AnnotationTypeResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }

    }
}
