using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalPackage
{
     [DataContract(Name = "HistoricalPackageRequestPospaid")]
    public class HistoricalPackageRequest
    {
        [DataMember]
        public Claro.Entity.AuditRequest objAudit { get; set; }
        [DataMember]
        public Entity.Dashboard.Prepaid.PackageODCS objPackageODCS { get; set; }
    }
}
