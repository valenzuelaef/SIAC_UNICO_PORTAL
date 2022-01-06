using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult
{
    [DataContract(Name = "IMRResponsePostPaid")]  
    public class IMRResponse
    {
        [DataMember]
        public string  IMRAmount { get; set; }
    }
}
