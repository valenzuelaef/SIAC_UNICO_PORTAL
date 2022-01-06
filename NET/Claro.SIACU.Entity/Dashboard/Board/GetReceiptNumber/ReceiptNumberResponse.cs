using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber
{
    [DataContract(Name = "ReceiptNumberResponseDashboard")]
   public class ReceiptNumberResponse
    {
         [DataMember] 
        public string ReceiptNumber { get; set; } 
    }
}
