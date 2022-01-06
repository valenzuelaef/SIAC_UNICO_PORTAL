using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBalanceCBIOS
{
    [DataContract(Name = "BalanceCBIOSResponsePostPaid")]
    public class BalanceCBIOSResponse
    {
        [DataMember]
        public List<Claro.SIACU.Entity.Dashboard.Postpaid.BagBalanceCBIOS> lstBagBalanceCBIOS { get; set; }
        [DataMember]
        public string strResponseCode { get; set; }
        [DataMember]
        public string strMessage { get; set; }
    }
}
