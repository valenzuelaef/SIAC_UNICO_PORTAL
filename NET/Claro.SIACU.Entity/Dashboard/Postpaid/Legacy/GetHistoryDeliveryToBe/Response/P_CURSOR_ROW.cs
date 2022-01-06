using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response
{
    [DataContract]
    public class P_CURSOR_ROW
    {
        [DataMember]
        public string nro_Recibo { get; set; }
        [DataMember]
        public string fechaRegistro { get; set; }
        [DataMember]
        public string fechaEmision { get; set; }
        [DataMember]
        public string fechaVencimiento { get; set; }
        [DataMember]
        public string periodo { get; set; }
       
    }
}
