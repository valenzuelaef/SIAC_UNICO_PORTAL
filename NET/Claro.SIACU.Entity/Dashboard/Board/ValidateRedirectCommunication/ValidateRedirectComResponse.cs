using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication
{
    [DataContract(Name="ValidateRedirectComResponseDashboard")]
    public class ValidateRedirectComResponse
    {
        [DataMember]
        public Boolean ResultValCommunication { get; set; }
        [DataMember]
        public string UrlDest { get; set; }
        [DataMember]
        public string Availability { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public string JsonParameters { get; set; }
    }
}
