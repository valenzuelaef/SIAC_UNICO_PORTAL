using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBalanceCBIOS
{
    [DataContract(Name = "BalanceCBIOSRequestPostPaid")]
    public class BalanceCBIOSRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Host { get; set; }
        [DataMember]
        public string Telephone { get; set; }
         


    }
}
