using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingDebtDetail;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingDebtDetailModel
    {
        public List<ApadeceDebt> listApadeceDebt { get; set; }
        public string DocumentNumber { get; set; }
        public string DebtQuantity { get; set; }
    }
}