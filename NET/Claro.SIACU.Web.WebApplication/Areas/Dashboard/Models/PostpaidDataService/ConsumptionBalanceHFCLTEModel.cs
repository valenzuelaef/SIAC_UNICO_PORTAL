using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ConsumptionBalanceHFCLTEModel
    {
        public List<HelperItem.TelephoneType> TelephoneTypes { get; set; }
        public List<HelperItem.Balance> Balances { get; set; }
    }
}