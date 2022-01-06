using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "StatusAccountLDIPostPaid")]
    public class StatusAccountLDI
    {
        [DataMember]
        public string Detalle { get; set; }

        [DataMember]
        public string PaymentId { get; set; }

        [DataMember]
        public string Payment { get; set; }

        [DataMember]
        public string ClaroBillId { get; set; }

        [DataMember]
        public string LdiBillId { get; set; }

        [DataMember]
        public string OperatorShort { get; set; }

        [DataMember]
        public string RegistryDate { get; set; }

        [DataMember]
        public string PaymentDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string BankDesc { get; set; }

        [DataMember]
        public string EmittedDate { get; set; }

        [DataMember]
        public string MaturityDate { get; set; }

        [DataMember]
        public double BillTotal { get; set; }

        [DataMember]
        public double ImportePend { get; set; }

        [DataMember]
        public double AmountPen { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public double AmountNotRequired { get; set; }
    }
}