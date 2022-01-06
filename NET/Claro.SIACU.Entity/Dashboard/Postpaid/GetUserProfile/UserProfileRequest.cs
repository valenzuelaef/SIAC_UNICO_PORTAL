using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile
{
    public class UserProfileRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public UserProfileMessageRequest MessageRequest { get; set; }
    }
}
