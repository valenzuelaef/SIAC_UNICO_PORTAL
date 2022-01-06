using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetBSS_StatusAccount
{
    [DataContract(Name = "BSS_StatusAccountResponse")]
    public class BSS_StatusAccountResponse
    {
        public BSS_StatusAccountResponse()
        {
            responseStatus = new responseStatus();
            responseDataConsultar = new responseDataConsultar();
        }

        [DataMember]
        public responseStatus responseStatus { get; set; }

        [DataMember]
        public responseDataConsultar responseDataConsultar { get; set; }
    }
}
