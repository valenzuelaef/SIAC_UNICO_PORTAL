using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFlagAjuste
{
    [DataContract(Name = "FlagAjusteResponse")]
    public class FlagAjusteResponse
    {
        [DataMember]
        public string AplicateAjsute { get; set; }
        [DataMember]
        public string CodeResponse { get; set; }
        [DataMember]
        public string MsjResponse { get; set; }

    }
}
