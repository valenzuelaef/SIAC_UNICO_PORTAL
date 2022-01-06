using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "HistoricalRechargePostPaid")]
    public class HistoricalRecharge
    {
        [DataMember]
        public string AMOUNT_RECHARGE { get; set; }
        [DataMember]
        public string CANAL { get; set; }
        [DataMember]
        public string DATE_RECHARGE { get; set; }



    }
}
