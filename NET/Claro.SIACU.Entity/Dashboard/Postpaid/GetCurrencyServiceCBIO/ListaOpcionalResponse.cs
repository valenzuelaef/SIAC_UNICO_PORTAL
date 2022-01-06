using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class ListaOpcionalResponse
    {
        [DataMember]
        public string clave { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
}
