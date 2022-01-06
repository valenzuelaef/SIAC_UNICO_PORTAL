using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetMbBag
{
    [DataContract(Name = "MbBagResponsePospaid")]
    public class MbBagResponse
    {
        [DataMember]
        public List<Recharge> lstRecharge { get; set; }

        public string strtCustomerCode { get; set; }
        [DataMember]
        public string claroPuntos { get; set; }
        [DataMember]
        public string strCodigoRespuesta { get; set; }
        [DataMember]
        public string strMensajeRespuesta { get; set; }
        [DataMember]
        public string strMbAvailable { get; set; }

    }
}
