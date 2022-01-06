using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class CurrencyServiceCbioReq: Claro.Entity.Request
    {
        [DataMember]
        public CurrencyServiceCbioRequest CurrencyServiceCbioRequest { get; set; }
    }
}
