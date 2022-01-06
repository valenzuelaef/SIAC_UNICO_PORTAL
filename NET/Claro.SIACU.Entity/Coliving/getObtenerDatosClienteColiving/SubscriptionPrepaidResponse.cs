using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Coliving.getObtenerDatosClienteColiving
{
    [DataContract]
    public class SubscriptionPrepaidResponse
    {
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Segment { get; set; }
        [DataMember]
        public string AccountId { get; set; }
        [DataMember]
        public string LineNumber { get; set; }
        [DataMember]
        public string LineStatus { get; set; }
        [DataMember]
        public string RatePlan { get; set; }
        [DataMember]
        public string origenInfoPre { get; set; }
        [DataMember]
        public string MigrateOne { get; set; }
    }
}
