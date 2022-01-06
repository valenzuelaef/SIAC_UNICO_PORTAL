using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions
{
    [DataContract]
    public class Subscription
    {
        [DataMember]
        public string ContractId { get; set; }
        [DataMember]
        public string BundleId { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string BillingAccountId { get; set; }
        [DataMember]
        public string BillingCycle { get; set; }
        [DataMember]
        public string BillingAccountStatus { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string ServiceIdentifier { get; set; }
        [DataMember]
        public string Technology { get; set; }
        [DataMember]
        public string Family { get; set; }
        [DataMember]
        public string TelephonyType { get; set; }
        [DataMember]
        public string PoId { get; set; }
        [DataMember]
        public string PoName { get; set; }
        [DataMember]
        public string SubscriptionStatus { get; set; }
        [DataMember]
        public string DateOfLastSubscriptionStatus { get; set; }
        [DataMember]
        public string CoActivated { get; set; }
    }
}
