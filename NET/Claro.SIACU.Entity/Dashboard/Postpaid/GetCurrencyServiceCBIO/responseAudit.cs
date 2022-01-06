using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class responseAudit
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }
    }
}
