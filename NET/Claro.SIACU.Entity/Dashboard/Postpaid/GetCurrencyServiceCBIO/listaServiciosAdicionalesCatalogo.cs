using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetCurrencyServiceCBIO
{
    [DataContract]
    public class listaServiciosAdicionalesCatalogo
    {
        [DataMember]
        public string POServicio { get; set; }
        [DataMember]
        public string tipoPO { get; set; }
        [DataMember]
        public string codTipoServicio { get; set; }
        [DataMember]
        public string tipoServicio { get; set; }
        [DataMember]
        public string spcode { get; set; }
        [DataMember]
        public string categoriaServicio { get; set; }
        [DataMember]
        public string codigoServicio { get; set; }
        [DataMember]
        public string descripcionServicio { get; set; }
        [DataMember]
        public string tooltip { get; set; }
        [DataMember]
        public string numeroGrupo { get; set; }
        [DataMember]
        public string decripcionGrupo { get; set; }
        [DataMember]
        public string codigoExcluyente { get; set; }
        [DataMember]
        public string descripcionExcluyente { get; set; }
        [DataMember]
        public string cargoFijo { get; set; }
        [DataMember]
        public string suscripcion { get; set; }
        [DataMember]
        public string bloqueoActivacion { get; set; }
        [DataMember]
        public string bloqueoDesactivacion { get; set; }
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
        public string idEquipo { get; set; }
        [DataMember]
        public string equipo { get; set; }
        [DataMember]
        public string cantEquipo { get; set; }
        [DataMember]
        public string codSap { get; set; }
        [DataMember]
        public string desSap { get; set; }
        [DataMember]
        public string tipoEquipo { get; set; }
        [DataMember]
        public string codExt { get; set; }
        [DataMember]
        public string desExt { get; set; }
        [DataMember]
        public string campaña { get; set; }
        [DataMember]
        public string valorServicio { get; set; }
    }
}
