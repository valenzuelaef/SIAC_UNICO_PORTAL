using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetReopenOfTheCase
{
    [DataContract(Name = "ReopenOfTheCaseRequestPostPaid")]
    public class ReopenOfTheCaseRequest : Claro.Entity.Request
    {
        [DataMember]
        public double IdCaso { get; set; }
    }
}
