using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays
{
    [DataContract(Name = "ActiveDaysRequestPostPaid")]
    public class ActiveDaysRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContratoID { get; set; }
 
    }
}
