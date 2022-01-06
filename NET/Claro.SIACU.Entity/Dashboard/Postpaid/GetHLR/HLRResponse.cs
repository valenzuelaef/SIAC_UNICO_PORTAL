using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetHLR
{
    [DataContract(Name = "HLRResponsePostPaid")]
    public class HLRResponse
    {
        [DataMember]
        public List<HLR> ListHLR { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string Tecnologia4G { get; set; }

       

    }
}
