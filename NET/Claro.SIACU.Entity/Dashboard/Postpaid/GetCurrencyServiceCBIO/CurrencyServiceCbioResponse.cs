using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class CurrencyServiceCbioResponse
    {
        [DataMember]
        public obtenerServiciosResponseType obtenerServiciosResponseType { get; set; }
    }
}
