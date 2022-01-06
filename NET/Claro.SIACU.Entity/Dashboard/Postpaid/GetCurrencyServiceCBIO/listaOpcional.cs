using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    public class listaOpcional
    {
        [DataMember]
        public string clave { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
}
