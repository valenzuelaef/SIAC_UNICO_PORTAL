using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne
{
    [DataContract(Name = "FixedChargeDetailTimProBagOneRequestPostPaid")]
    public class FixedChargeDetailTimProBagOneRequest: Claro.Entity.Request 
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
