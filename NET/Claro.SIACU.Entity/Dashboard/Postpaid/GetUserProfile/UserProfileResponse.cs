using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile
{
    public class UserProfileResponse
    {
        [DataMember(Name = "MessageResponse")]
        public UserProfileMessageResponse MessageResponse { get; set; }
    }
}
