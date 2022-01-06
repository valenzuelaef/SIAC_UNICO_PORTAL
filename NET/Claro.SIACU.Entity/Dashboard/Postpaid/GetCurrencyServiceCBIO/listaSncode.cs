using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class listaSncode
    {
        [DataMember]
        public string sncode { get; set; }
        [DataMember]
        public string sncodeDes { get; set; }
        [DataMember]
        public string pop { get; set; }
        [DataMember]
        public string cfss { get; set; }
    }
}
