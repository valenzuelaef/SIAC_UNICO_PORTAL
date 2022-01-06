using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays
{
    [DataContract(Name = "ActiveDaysResponsePostPaid")]
    public class ActiveDaysResponse 
    {
        [DataMember]
        public int ActiveDays { get; set; }
        [DataMember]
        public int DisableDays { get; set; } 
        [DataMember]
        public int Result { get; set; }
        [DataMember]
        public string MessageError { get; set; }
        [DataMember]
        public string Message { get; set; }  
    } 
}
