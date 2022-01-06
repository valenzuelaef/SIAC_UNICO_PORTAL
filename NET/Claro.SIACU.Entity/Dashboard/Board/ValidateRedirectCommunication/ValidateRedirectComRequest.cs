using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication
{
    [DataContract(Name="ValidateRedirectComRequestDashboard")]
    public class ValidateRedirectComRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Sequence { get; set; }
        [DataMember]
        public string Server { get; set; }
    }
}
