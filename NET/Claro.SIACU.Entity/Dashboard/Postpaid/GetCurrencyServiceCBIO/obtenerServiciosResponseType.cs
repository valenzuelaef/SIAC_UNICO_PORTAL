using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class obtenerServiciosResponseType
    {
        [DataMember]
        public responseAudit responseAudit { get; set; }
        [DataMember]
        public responseData responseData { get; set; }
    }
}
