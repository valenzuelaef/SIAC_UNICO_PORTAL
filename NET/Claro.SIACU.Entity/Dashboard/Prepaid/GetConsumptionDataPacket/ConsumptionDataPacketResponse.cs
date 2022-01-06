using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetConsumptionDataPacket
{
    [DataContract(Name = "ConsumptionDataPacketResponsePrePaid")]
    public class ConsumptionDataPacketResponse
    {
        [DataMember]
        public List<PackageODCS> PackageODCSs { get; set; }
    }
}
