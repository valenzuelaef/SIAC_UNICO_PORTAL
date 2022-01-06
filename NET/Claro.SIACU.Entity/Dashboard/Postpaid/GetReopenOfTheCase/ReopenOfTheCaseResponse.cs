using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReopenOfTheCase
{
    [DataContract(Name = "ReopenOfTheCaseResponsePostPaid")]
    public class ReopenOfTheCaseResponse
    {
        [DataMember]
        public List<Reclosures> Reclosures { get; set; }
    }
}
