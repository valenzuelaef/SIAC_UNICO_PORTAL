using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions
{
    [DataContract]
    public class RetrieveSubscriptionResponse
    {
        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public String CodeResponse { get; set; }

        [DataMember]
        public String DescriptionResponse { get; set; }

        [DataMember]
        public DateTime date { get; set; }

        [DataMember]
        public String Segment { get; set; }

        [DataMember]
        public string MigrateOne { get; set; }

        [DataMember]
        public List<Claro.SIACU.Entity.Coliving.getRetrieveSubscriptions.Subscription> Subscriptions { get; set; }
    }
}
