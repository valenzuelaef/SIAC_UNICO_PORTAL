using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoryInvoice;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingHistoryInvoiceModel
    {
        public List<HELPER_DATA.Invoice> listInvoice { get; set; }
    }
    
}