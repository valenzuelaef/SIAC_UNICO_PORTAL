using Claro.Helpers;
using System.Collections.Generic;
using HELPER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataReload
{
    public class ExportExcelReloadModel
    {
        public ExportExcelReloadModel() { }

        [Header(Title = "RechargeDescription")]
        public string RechargeDescription { get; set; }

        [Header(Title = "PhoneDescription")]
        public string PhoneDescription { get; set; }

        [Header(Title = "ExportExcelReloadDetailHelpers")]
        public List<HELPER.ExportExcelReloadDetailHelper> ExportExcelReloadDetailHelpers { get; set; }

    }
}