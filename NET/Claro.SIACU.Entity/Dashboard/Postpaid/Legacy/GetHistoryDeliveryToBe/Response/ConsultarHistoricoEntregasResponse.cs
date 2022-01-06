using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response
{
    [DataContract]
    public class ConsultarHistoricoEntregasResponse
    {
        [DataMember]
        public P_CURSOR P_CURSOR { get; set; }
        [DataMember]
        public string XV_CODIGORESPUESTA { get; set; }
        [DataMember]
        public string XV_MENSAJERESPUESTA { get; set; }

    }
}
