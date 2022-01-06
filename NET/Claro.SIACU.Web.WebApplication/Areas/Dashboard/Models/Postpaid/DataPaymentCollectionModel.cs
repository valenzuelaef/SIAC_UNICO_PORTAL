
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class DataPaymentCollectionModel
    {
        public string CustomerId { get; set; }
        public string PaymentForm { get; set; }
        public string Debt { get; set; }
        public string AmoutDispute { get; set; }
        public string PaymentBehavior { get; set; }
        public decimal ParameterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Value_C { get; set; }
        public decimal Value_N { get; set; }
        public string Value_L { get; set; }
        public string DaysPastDue { get; set; }
        public string StatusCollection { get; set; }
        public string ManagerCollection { get; set; }
        public string  btnStatusAccountHR { get; set; }
    }
}