using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.File.GetFileDefaultImpersonation
{

    [DataContract(Name = "FileDefaultImpersonationRequestDashboard")]
    public class FileDefaultImpersonationRequest : Claro.Entity.Request
    {
       [DataMember]
       public string Path { get; set; }
       [DataMember]
       public string DateRegister { get; set; }
       [DataMember]
       public string strIdOnBase { get; set; }
        [DataMember]
       public string strDateIssue { get; set; }
         [DataMember]
       public string strNumeroRecibo { get; set; }
         
    }
}
