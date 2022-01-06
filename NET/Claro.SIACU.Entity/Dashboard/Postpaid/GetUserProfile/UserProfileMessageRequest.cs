using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile
{
    public class UserProfileMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public UserProfileBodyRequest Body { get; set; }
    }
}
