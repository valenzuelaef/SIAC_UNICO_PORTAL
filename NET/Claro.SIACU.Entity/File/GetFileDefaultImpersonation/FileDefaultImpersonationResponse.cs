using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.File.GetFileDefaultImpersonation
{
    [DataContract(Name = "FileDefaultImpersonationResponseDashboard")]
    public class FileDefaultImpersonationResponse
    {
        [DataMember]
        public GlobalDocument ObjGlobalDocument { get; set; }
    }
}
