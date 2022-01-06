using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class obtenerServiciosRequestType
    {
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string msisdn { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string codTecnologia { get; set; }
        [DataMember]
        public string codProd { get; set; }
        [DataMember]
        public string flagServAdicional { get; set; }
        [DataMember]
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
