
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class DataBillingModel
    {
        public string Application { get; set; }

        public string InvoiceNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string EmissionDate { get; set; }
        public decimal TotalAccess { get; set; }
        public decimal LocalTrafficConsume { get; set; }
        public decimal LocalTrafficAdditional { get; set; }
        public decimal TotalChargesMonth { get; set; }
        public decimal LongDistanceNational { get; set; }
        public decimal LongDistanceInternational { get; set; }
        public decimal RoamingInternational { get; set; }
        public decimal TotalSubscription { get; set; }
        public decimal TotalOCCs { get; set; }
        public bool SendMail { get; set; }
        public string InvoiceNumberOriginal { get; set; }

        public string FlagBill { get; set; }  
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string NameForm { get; set; }
        public bool btnHistHR { get; set; }
        public byte[] arrBuffer { get; set; }
        public string TypeMIME { get; set; }
        public string FilePathResumen { get; set; }
        public string FileNameResumen { get; set; }

    }
}