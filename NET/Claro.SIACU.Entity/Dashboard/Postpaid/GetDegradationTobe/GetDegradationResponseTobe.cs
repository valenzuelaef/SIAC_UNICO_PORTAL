using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDegradationTobe
{
    [DataContract]
    public class GetDegradationResponseTobe
    {
        [DataMember]
        public List<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.listaOpcional> listOptional { get; set; }
    }
}
