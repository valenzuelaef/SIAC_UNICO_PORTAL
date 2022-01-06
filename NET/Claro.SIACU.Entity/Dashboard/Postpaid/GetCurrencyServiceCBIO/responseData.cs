using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class responseData
    {
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string codProd { get; set; }
        [DataMember]
        public List<listaServiciosAsignados> listaServiciosAsignados { get; set; }
        [DataMember]
        public List<listaServiciosAdicionalesCatalogo> listaServiciosAdicionalesCatalogo { get; set; }
        [DataMember]
        public List<ListaOpcionalResponse> listaOpcional { get; set; }
        [DataMember]
        public List<listaCaracteristicas> listaCaracteristicas { get; set; }
    }
}
