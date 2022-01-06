using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryHR
{
    [DataContract(Name = "HistoryHRRequestPostPaid")]
    public class HistoryHRRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int PageSize { get; set; }  

    }
}
