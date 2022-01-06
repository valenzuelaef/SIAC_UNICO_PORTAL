using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusFullClaroClient
{
    [DataContract(Name = "BonusFullClaroClientBodyResponse")]
    public class BonusFullClaroClientBodyResponse
    {
        [DataMember(Name = "mensajeError")]
        public string mensajeError { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }

        [DataMember(Name = "codigoEtiqueta1")]
        public string codigoEtiqueta1 { get; set; }

        [DataMember(Name = "nombreEtiqueta1")]
        public string nombreEtiqueta1 { get; set; }

        [DataMember(Name = "bonoElegido")]
        public string bonoElegido { get; set; }

        [DataMember(Name = "bonoLinea")]
        public string bonoLinea { get; set; }

        [DataMember(Name = "estado")]
        public string estado { get; set; }

        [DataMember(Name = "Fault")]
        public Common.GetDataPower.FaultResponse Fault { get; set; }
    }
}
