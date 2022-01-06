using Claro.Helpers;
using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.ExportExcelHistoricalBondsHelper
{
    public class HistoricalBondsHelper : IExcel
    {
        [Header(Title = "Telephone")]
        public string Telephone { get; set; }
         [Header(Title = "listHistoricalBondsModel")]
        public List<HistoricalBondsModel> listHistoricalBondsModel { get; set; }
    }
}