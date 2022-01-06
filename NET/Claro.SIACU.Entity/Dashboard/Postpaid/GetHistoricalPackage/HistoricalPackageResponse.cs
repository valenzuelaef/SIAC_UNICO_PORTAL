using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalPackage
{
    [DataContract(Name = "HistoricalPackageResponsePospaid")]
    public class HistoricalPackageResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS { get; set; }
        [DataMember]
        public string TotalMBCicle { get; set; }
        [DataMember]
        public string TotalRows { get; set; }
    }
}
