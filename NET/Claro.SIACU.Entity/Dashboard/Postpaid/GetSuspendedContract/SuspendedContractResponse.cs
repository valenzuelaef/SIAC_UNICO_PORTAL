using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSuspendedContract
{
    [DataContract(Name = "SuspendedContractResponsePostPaid")]
    public class SuspendedContractResponse
    {
        [DataMember]
        public List<Contract> ListContract { get; set; }
        [DataMember]
        public List<Entity.ListItem> ListSuspendedType { get; set; }

    }
}
