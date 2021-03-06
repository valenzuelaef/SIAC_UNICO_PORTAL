using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount
{
    public class EvaluateAmountBodyResponse
    {
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }

        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "monto")]
        public double monto { get; set; }
    }
}
