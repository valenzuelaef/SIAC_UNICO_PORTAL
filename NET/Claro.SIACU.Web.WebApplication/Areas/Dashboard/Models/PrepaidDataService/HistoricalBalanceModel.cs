using Claro.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class HistoricalBalanceModel : IExcel
    {
        
       public string startDateHistoricalBalance { get; set; }
         
       public string endDateHistoricalBalance { get; set; }

        [Header(Title = "lstHistoricalBalance")]
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.BalanceModel> lstHistoricalBalance { get; set; }

    }
}