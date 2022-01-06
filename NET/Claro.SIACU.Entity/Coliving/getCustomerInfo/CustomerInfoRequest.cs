using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getCustomerInfo
{
    [DataContract]
    public class CustomerInfoRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string CustomerIdTcrm { get; set; }
        [DataMember]
        public string ServiceIdentifier { get; set; }
        [DataMember]
        public int SubscriptionType { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public int CustomerType { get; set; }

    }
}
