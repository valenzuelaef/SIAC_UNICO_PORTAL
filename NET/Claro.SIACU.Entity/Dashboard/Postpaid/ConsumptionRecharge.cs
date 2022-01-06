using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ConsumptionRechargePostPaid")]
    public class ConsumptionRecharge
    {
        [DataMember]
        public string DateEvent { get; set; }
        [DataMember]
        public string TypeConsumption { get; set; }
        [DataMember]
        public string NumberDestinationAPN { get; set; }
        [DataMember]
        public string IdBag { get; set; }
        [DataMember]
        public string AmountConsumed { get; set; }
        [DataMember]
        public string Balance { get; set; }




    }
}



