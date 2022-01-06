using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetSMSDetails
{
    [DataContract(Name = "SMSDetailsResponsePostPaid")]
    public class SMSDetailsResponse
    {
        [DataMember]
        public List<CallDetailSMS> ListCallDetail { get; set; }  

        [DataMember]
        public decimal DecCantidadSMS { get; set; }
    }
}

