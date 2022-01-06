using Claro.Helpers;
using System.Collections.Generic;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ScheduledTasksHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ScheduledTasksModel: IExcel
    {
        public List<HELPER_ITEM.StateType> StateTypes { get; set; }
        public List<HELPER_ITEM.TransactionType> TransactionTypes { get; set; }
        public List<HELPER_ITEM.CacDacType> CacDacTypes { get; set; }
        [Header(Title = "ScheduledTransactions")]
        public List<HELPER_ITEM.ScheduledTransaction> ScheduledTransactions { get; set; }
    }
}