using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions
{
    [DataContract]
    public class RetrieveSubscriptionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string contractId { get; set; }
        [DataMember]
        public string customerId { get; set; }
        [DataMember]
        public string serviceIdentifier { get; set; }
        [DataMember]
        public int productType { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public string subscriptionStatus { get; set; }
        [DataMember]
        public string bundleId { get; set; }
        [DataMember]
        public List<Subscription> Subcription { get; set; }
    }
}
