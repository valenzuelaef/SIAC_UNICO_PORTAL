using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTimServiceDetails
{
    [DataContract(Name = "TimServiceDetailsResponsePostPaid")]
    public class TimServiceDetailsResponse
    {
        [DataMember]
        public List<CallDetailTimService> ListCallDetail { get; set; }  
    }
}

