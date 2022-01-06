using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Common.GetTypeDocumentCustomer
{
    [DataContract(Name = "TypeDocumentCustomerResponseCommon")]
    public class TypeDocumentCustomerResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
