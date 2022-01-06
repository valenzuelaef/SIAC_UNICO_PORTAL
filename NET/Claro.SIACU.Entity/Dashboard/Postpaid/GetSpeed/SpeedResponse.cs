using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSpeed
{
    [DataContract(Name = "SpeedResponsePostPaid")]
    public class SpeedResponse
    {
        [DataMember]
        public string strCadena { get; set; }
    }
}
