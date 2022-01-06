using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetInstants
{
    [DataContract(Name = "InstantsRequestDashboard")]
    public class InstantsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string DataSearch { get; set; }
        [DataMember]
        public string TypePhone { get; set; }
        [DataMember]
        public string OriginType { get; set; }

    }
}
