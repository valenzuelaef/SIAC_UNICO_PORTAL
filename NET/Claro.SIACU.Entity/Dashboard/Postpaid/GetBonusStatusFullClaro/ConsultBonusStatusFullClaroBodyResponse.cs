using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro
{
    [DataContract(Name = "ConsultBonusStatusFullClaroBodyResponse")]
    public class ConsultBonusStatusFullClaroBodyResponse
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

        [DataMember(Name = "codigoEtiqueta2")]
        public string codigoEtiqueta2 { get; set; }

        [DataMember(Name = "nombreEtiqueta2")]
        public string nombreEtiqueta2 { get; set; }

        [DataMember(Name = "Fault")]
        public Common.GetDataPower.FaultResponse Fault { get; set; }
    }
}
