using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute
{
    [DataContract(Name = "DetailAmountDisputeRequestPostPaid")]
    public class DetailAmountDisputeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }

    }
}

