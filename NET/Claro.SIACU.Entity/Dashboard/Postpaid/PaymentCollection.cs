using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "PaymentCollectionPostPaid")]
    public class PaymentCollection
    {
        [DataMember]
        public string PAYMENT_FORM { get; set; }

        [DataMember]
        public string DEBT { get; set; }

        [DataMember]
        public string AMOUT_DISPUTE { get; set; }

        [DataMember]
        public string PAYMENT_BEHAVIOR { get; set; }

        [DataMember]
        public Parameter PARAMETER_DATA { get; set; }
    }
}