using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Request
{
    [DataContract]
    public class ConsultarHistoricoEntregasRequest
    {
        [DataMember]
        public string PV_CUSTOMER_ID { get; set; }
        [DataMember]
        public string PV_NRO_RECIBO { get; set; }
        [DataMember]
        public string PN_CANT_RECIBOS { get; set; }
    }
}
