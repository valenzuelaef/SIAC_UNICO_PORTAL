using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne
{
    [DataContract(Name = "FixedChargeDetailTimProOneRequestPostPaid")]
    public class FixedChargeDetailTimProOneRequest: Claro.Entity.Request
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
