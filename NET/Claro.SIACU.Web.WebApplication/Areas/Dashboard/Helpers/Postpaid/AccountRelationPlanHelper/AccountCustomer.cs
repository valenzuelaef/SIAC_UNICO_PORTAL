using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHelper
{
    public class AccountCustomer : IExcel
    {
        [Header(Title = "BusinessName", Order = Claro.Constants.NumberZero)]
        public string BusinessName { get; set; }
        [Header(Title = "CustomerID", Order = Claro.Constants.NumberZero)]
        public string CustomerID { get; set; }
        [Header(Title = "Account", Order = Claro.Constants.NumberZero)]
        public string Account { get; set; }
        [Header(Title = "InvoiceAddress", Order = Claro.Constants.NumberZero)]
        public string InvoiceAddress { get; set; }
        [Header(Title = "InvoiceDistrict", Order = Claro.Constants.NumberZero)]
        public string InvoiceDistrict { get; set; }
        [Header(Title = "InvoiceProvince", Order = Claro.Constants.NumberZero)]
        public string InvoiceProvince { get; set; }
        [Header(Title = "FullName", Order = Claro.Constants.NumberZero)]
        public string FullName { get; set; }
        [Header(Title = "Fax", Order = Claro.Constants.NumberZero)]
        public string Fax { get; set; }
        [Header(Title = "LegalAgent", Order = Claro.Constants.NumberZero)]
        public string LegalAgent { get; set; }
        [Header(Title = "DNIRUC", Order = Claro.Constants.NumberZero)]
        public string DNIRUC { get; set; }
        [Header(Title = "CustomerContact", Order = Claro.Constants.NumberZero)]
        public string CustomerContact { get; set; }
        [Header(Title = "PhoneReference", Order = Claro.Constants.NumberZero)]
        public string PhoneReference { get; set; }
        [Header(Title = "CreditLimit", Order = Claro.Constants.NumberZero)]
        public string CreditLimit { get; set; }
        [Header(Title = "Email", Order = Claro.Constants.NumberZero)]
        public string Email { get; set; }
        [Header(Title = "TotalLines", Order = Claro.Constants.NumberZero)]
        public string TotalLines { get; set; }
        [Header(Title = "Consultant", Order = Claro.Constants.NumberZero)]
        public string Consultant { get; set; }

    }
}