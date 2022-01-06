using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount
{
    public class EvaluateAmountBodyRequest
    {
        [DataMember(Name = "perfil")]
        public string perfil { get; set; }

        [DataMember(Name = "modalidad")]
        public string modalidad { get; set; }

        [DataMember(Name = "unidad")]
        public string unidad { get; set; }

        [DataMember(Name = "tipoTelefono")]
        public string tipoTelefono { get; set; }
    }
}
