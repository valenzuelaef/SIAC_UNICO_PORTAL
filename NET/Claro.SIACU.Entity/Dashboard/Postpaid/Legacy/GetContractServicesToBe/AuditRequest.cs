using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServicesToBe
{
    [DataContract]
    public class AuditRequest
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string msgid { get; set; }
        [DataMember]
        public string userId { get; set; }
        [DataMember]
        public string timestamp { get; set; }
        [DataMember]
        public string nombreAplicacion { get; set; }
    }
}
