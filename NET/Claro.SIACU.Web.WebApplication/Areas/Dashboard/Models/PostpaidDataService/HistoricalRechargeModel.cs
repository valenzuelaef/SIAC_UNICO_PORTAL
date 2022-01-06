using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class HistoricalRechargeModel
    {
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoricalRecharge.HistoricalRechargeHelper> lstHistoricalRechargeHelper { get; set; }
        public string MessageAlert { get; set; }
        public string strDateStart { get; set; }
        public string strDateEnd { get; set; }
    }
}