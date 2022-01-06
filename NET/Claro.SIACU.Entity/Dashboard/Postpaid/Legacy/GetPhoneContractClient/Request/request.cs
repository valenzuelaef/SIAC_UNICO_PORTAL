using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetPhoneContractClient.Request
{
    [DataContract]
    public class request
    {
        [DataMember]
        public ConsultarContratosLineaRequest consultarContratosLineaRequest { get; set; }
    }
}
