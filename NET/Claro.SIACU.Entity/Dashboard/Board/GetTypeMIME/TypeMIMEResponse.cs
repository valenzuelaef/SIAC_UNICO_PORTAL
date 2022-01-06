using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME
{
    [DataContract(Name = "GetTypeMIMEResponseDashboard")]
    public class TypeMIMEResponse
    {
        [DataMember]
        public string TypeMime { get; set; }
    }
}
