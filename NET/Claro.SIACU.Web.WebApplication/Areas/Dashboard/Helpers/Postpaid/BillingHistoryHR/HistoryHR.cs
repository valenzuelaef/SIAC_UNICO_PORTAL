
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoryHR
{
    public class HistoryHR
    {
       
        public string FechaEmision { get; set; }
        public string FechaVencimiento { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal TotalCurrentCharges { get; set; }
        public string InvoiceNumber { get; set; }
        public string Periodo { get; set; }
    }
}