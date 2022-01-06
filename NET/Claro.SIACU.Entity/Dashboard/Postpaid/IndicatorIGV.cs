using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "IndicatorIGVPostPaid")]
    public class IndicatorIGV
    {
        [DataMember]
        public string ID_INDICADOR { get; set; }
        [DataMember]
        public string INDICADOR { get; set; }
        [DataMember]
        public double PORCENTAJE { get; set; }
        [DataMember]
        public double PORCENTAJED { get; set; }
        [DataMember]
        public string TIPO_DOCUMENTO { get; set; }
        [DataMember]
        public DateTime FEC_CREATE { get; set; }
        [DataMember]
        public DateTime FEC_INI_VIGENCIA { get; set; }
        [DataMember]
        public DateTime FEC_FIN_VIGENCIA { get; set; }
    }
}
