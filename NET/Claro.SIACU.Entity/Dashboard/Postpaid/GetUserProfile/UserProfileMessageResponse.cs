using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile
{
    public class UserProfileMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public UserProfileBodyResponse Body { get; set; }
    }
}
