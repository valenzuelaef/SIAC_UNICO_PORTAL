using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro
{
    [DataContract(Name = "FixedChargeDetailTimProRequestPostPaid")]          
    public class FixedChargeDetailTimProRequest : Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
