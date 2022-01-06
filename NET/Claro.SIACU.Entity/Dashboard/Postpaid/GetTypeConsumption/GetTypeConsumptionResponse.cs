using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeConsumption
{
    [DataContract(Name = "GetTypeConsumptionResponseCommon")]
    public class GetTypeConsumptionResponse
    {
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
