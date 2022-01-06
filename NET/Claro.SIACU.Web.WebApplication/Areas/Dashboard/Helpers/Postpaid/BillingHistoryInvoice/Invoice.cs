
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoryInvoice
{
    public class Invoice
    {
        public string FechaEmision { get; set; }
        public string FechaVencimiento { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal TotalCurrentCharges { get; set; }

        public string InvoiceNumber { get; set; }
        public string CCName { get; set; }
        public string ContactClient { get; set; }
        public string CCAddr1 { get; set; }
        public string CCAddr2 { get; set; }
        public string Distrito { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string NroDoc { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
       
        public string CodCliente { get; set; }
        public string NroCiclo { get; set; }
        public string Periodo { get; set; }
        public decimal TotalPrevCharges { get; set; }
        public decimal TotalPaymentsRcvd { get; set; }
        public decimal TotalPrevBalance { get; set; }
      
      
        public string Mes { get; set; }
        public string Anho { get; set; }
    }
}