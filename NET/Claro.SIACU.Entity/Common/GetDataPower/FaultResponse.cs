using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Common.GetDataPower
{
    [DataContract(Name = "Fault")]
    public class FaultResponse
    {
        [DataMember(Name = "faultcode")]
        public string faultcode { get; set; }

        [DataMember(Name = "faultstring")]
        public string faultstring { get; set; }

        [DataMember(Name = "faultactor")]
        public string faultactor { get; set; }

        [DataMember(Name = "detail")]
        public FaultDetail detail { get; set; }

    }

    [DataContract(Name = "FaultDetail")]
    public class FaultDetail
    {
        [DataMember(Name = "IntegrationError")]
        public string IntegrationError { get; set; }

    }
}
