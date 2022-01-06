using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne
{
    [DataContract(Name = "FixedChargeTimProBagDetailOneRequestPostPaid")]
    public class FixedChargeTimProBagDetailOneRequest: Claro.Entity.Request 
    {
        [DataMember]
        public string GroupBox { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
