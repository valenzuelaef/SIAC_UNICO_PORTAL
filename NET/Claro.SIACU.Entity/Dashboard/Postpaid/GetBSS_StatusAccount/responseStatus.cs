using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "responseStatus")]
    public class responseStatus
    {
        [DataMember(Name = "estado")]
        public string state { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codeResponse { get; set; }

        [DataMember(Name = "descripcionRespuesta")]
        public string descriptionResponse { get; set; }

        [DataMember(Name = "ubicacionError")]
        public string locationError { get; set; }

        [DataMember(Name = "fecha")]
        public string date { get; set; }

        [DataMember(Name = "origen")]
        public string origin{ get; set; }
    }
}
