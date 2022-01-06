using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSharedBag
{
    [DataContract(Name = "SharedBagResponsePostPaid")]
    public class SharedBagResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.SharedBag ObjSharedBag { get; set; }

        [DataMember]
        public List<Entity.Dashboard.Postpaid.SharedBag> ListSharedBag { get; set; }

        [DataMember]
        public List<Entity.Dashboard.Postpaid.SharedBag> ListSharedBagLine { get; set; }
        [DataMember]
        public string MessageTypeCustomer { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string CountSharedBag { get; set; }
    }
}
