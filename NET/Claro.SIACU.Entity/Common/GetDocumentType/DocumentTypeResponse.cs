using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetDocumentType
{
    [DataContract(Name = "DocumentTypeResponseCommon")]
    public class DocumentTypeResponse
    {
        [DataMember]
        public List<ListItem> DocumentTypes { get; set; }
    }
}
