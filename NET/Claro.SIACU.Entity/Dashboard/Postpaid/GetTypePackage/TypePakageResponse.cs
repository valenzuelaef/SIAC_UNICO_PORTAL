using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTypePackage
{
    [DataContract(Name = "TypePakageResponseCommon")]
    public class TypePakageResponse
    {
        [DataMember]
        public Claro.Entity.AuditRequest objAudit { get; set; }
        [DataMember]
        public List<Entity.ListItem> ListItem { get; set; }
    }
}
