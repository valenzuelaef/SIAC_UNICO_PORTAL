using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste
{
    [DataContract(Name = "FlagAjsuteRequest")]
    public class FlagAjsuteRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Document { get; set; }
    }
}
