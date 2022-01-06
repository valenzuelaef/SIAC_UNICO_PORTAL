using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMbBag
{
    [DataContract(Name = "MbBagRequestPospaid")]
    public class MbBagRequest : Claro.Entity.Request
    {
        [DataMember]
        public Claro.Entity.AuditRequest objAudit { get; set; }
        [DataMember]
        public string strMsidn { get; set; }
        [DataMember]
        public string strCustomerId { get; set; }  
        //[DataMember]
        //public string strtCustomerCode { get; set; }
        [DataMember]
        public string idTransaccion { get; set; }        
       [DataMember]
        public string msisdn { get; set; }
        
    }
}
