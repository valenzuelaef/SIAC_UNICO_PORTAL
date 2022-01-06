using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetConsumptionDataPacket
{
    [DataContract(Name = "ConsumptionDataPacketRequestPrePaid")]
    public class ConsumptionDataPacketRequest : Claro.Entity.Request
    {
        [DataMember]
        public PackageODCS PackageODCS { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
    }
}
