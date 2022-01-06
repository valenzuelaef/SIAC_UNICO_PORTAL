using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class HistoricalPackageModel
    {
        public HELPER.GetMbTotal.MbTotalHelper objMbTotalHelper { get; set; }
        public HELPER.GetMbAvailable.MbAvailableHelper objMbAvailableHelper { get; set; }
        public HELPER.GetHistoricalPackage.HistoricalPackageHelper objHistoricalPackageHelper { get; set; }
    }
}