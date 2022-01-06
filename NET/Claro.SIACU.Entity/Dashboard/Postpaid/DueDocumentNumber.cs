using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DueDocumentNumberPostPaid")]
    public class DueDocumentNumber
    {
        [DataMember]
        public DueDocumentNumberAccount ObjDueDocumentNumberAccount { get; set; }

        [DataMember]
        public List<DueDocumentNumberAccount> ListDueDocumentNumberAccountFijo { get; set; }

        [DataMember]
        public List<DueDocumentNumberAccount> ListDueDocumentNumberAccountMovil { get; set; }
    }
}
