using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge
{
    [DataContract(Name = "ConsumptionHistoricalRechargeResponsePospaid")]
    public class ConsumptionHistoricalRechargeResponsePospaid
    {
        [DataMember]
        public string strCodigoRespuesta { get; set; }
        [DataMember]
        public string strMensajeRespuesta { get; set; }
        [DataMember]
        public List<Entity.Dashboard.Postpaid.ConsumptionRecharge> lstConsumptionRecharge { get; set; }
        [DataMember]
        public string strMensajeAlert { get; set; }
    }
}
