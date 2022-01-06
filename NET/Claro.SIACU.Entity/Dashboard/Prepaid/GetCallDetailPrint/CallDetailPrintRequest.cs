using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid.GetCallDetailPrint
{
    [DataContract(Name = "CallDetailPrintRequestPrePaid")]
    public class CallDetailPrintRequest : Claro.Entity.Request
    {

        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
    }
}
