using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetLoanRental
{

    [DataContract(Name = "LoanRentalRequestPostPaid")]
    public class LoanRentalResquest : Claro.Entity.Request
    {
        [DataMember]
        public string StarDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string NroDocumento { get; set; }
    }
}
