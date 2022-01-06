using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoryHR;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingHistoryHRModel
    {
        public List<HELPER_DATA.HistoryHR> listHistoryHR { get; set; }
    }
}