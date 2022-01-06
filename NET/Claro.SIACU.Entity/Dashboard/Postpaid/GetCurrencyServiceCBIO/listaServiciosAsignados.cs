using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class listaServiciosAsignados
    {
        [DataMember]
        public string poServicio { get; set; }
        [DataMember]
        public string tipoPO { get; set; }
        [DataMember]
        public string codTipoServicio { get; set; }
        [DataMember]
        public string tipoServicio { get; set; }
        [DataMember]
        public string spcode { get; set; }
        [DataMember]
        public string spcodeDes { get; set; }
        [DataMember]
        public string categoriaServicio { get; set; }
        [DataMember]
        public string codigoServicio { get; set; }
        [DataMember]
        public string numeroGrupo { get; set; }
        [DataMember]
        public string decripcionGrupo { get; set; }
        [DataMember]
        public string codigoExcluyente { get; set; }
        [DataMember]
        public string descripcionExcluyente { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string validoDesde { get; set; }
        [DataMember]
        public string cargoFijo { get; set; }
        [DataMember]
        public string suscripcion { get; set; }
        [DataMember]
        public string cuota { get; set; }
        [DataMember]
        public string bloqueoActivacion { get; set; }
        [DataMember]
        public string bloqueoDesactivacion { get; set; }
        [DataMember]
        public string productId { get; set; }
        [DataMember]
        public string unidad { get; set; }
        [DataMember]
        public string tipoUnidad { get; set; }
        [DataMember]
        public string periodo { get; set; }
        [DataMember]
        public string tipoPeriodo { get; set; }
        [DataMember]
        public string tipoCompra { get; set; }
        [DataMember]
        public string codigoConvergente { get; set; }
        [DataMember]
        public string valorServicio { get; set; }
        [DataMember]
        public string umbralConsumo { get; set; }
        [DataMember]
        public string tipoLimiteCredito { get; set; }
        [DataMember]
        public List<listaSncode> listaSncode { get; set; }
    }
}
