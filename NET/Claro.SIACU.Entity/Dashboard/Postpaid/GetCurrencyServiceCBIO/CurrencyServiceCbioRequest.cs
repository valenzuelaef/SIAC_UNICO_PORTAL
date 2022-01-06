using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class CurrencyServiceCbioRequest
    {
        [DataMember]
        public obtenerServiciosRequestType obtenerServiciosRequestType { get; set; }
    }
}
