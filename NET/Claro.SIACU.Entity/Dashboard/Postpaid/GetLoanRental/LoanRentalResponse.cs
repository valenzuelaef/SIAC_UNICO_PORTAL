using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetLoanRental
{

    [DataContract(Name = "LoanRentalResponsePostPaid")]
    public class LoanRentalResponse
    {
        [DataMember]
        public Entity.Dashboard.Postpaid.LoanRental ObjLoanRental { get; set; }

    }
}
