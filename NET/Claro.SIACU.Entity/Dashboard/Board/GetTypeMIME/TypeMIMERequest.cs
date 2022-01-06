using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME
{
    [DataContract(Name = "GetTypeMIMERequestDashboard")]
    public class TypeMIMERequest : Claro.Entity.Request
    {
       [DataMember]
       public string Extension { get; set; } 
    }
}
