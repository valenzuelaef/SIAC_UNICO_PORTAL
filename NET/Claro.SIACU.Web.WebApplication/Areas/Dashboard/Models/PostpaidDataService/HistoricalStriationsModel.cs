using Claro.Helpers;
using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoricalStriationHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class HistoricalStriationsModel : IExcel
    {
        [Header(Title = "Account")]
        public string Account { get; set; }
        [Header(Title = "Name")]
        public string Name { get; set; }
        [Header(Title = "NumberServices")]
        public string TelephoneNumber { get; set; }
        [Header(Title = "HistoricalStriations")]
        public List<HelperItem.HistoricalStriation> HistoricalStriations { get; set; }
      
      
    }
}