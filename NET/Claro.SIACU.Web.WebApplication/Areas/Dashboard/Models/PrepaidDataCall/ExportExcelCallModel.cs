using Claro.Helpers;
using System.Collections.Generic;
using HELPER = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataCallHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataCall
{
    public class ExportExcelCallModel : IExcel
    {
        public ExportExcelCallModel() { }

        [Header(Title = "PhoneDescription")]
        public string PhoneDescription { get; set; }

        [Header(Title = "TrafficDescription")]
        public string TrafficDescription { get; set; }

        [Header(Title = "DataCalls")]
        public List<HELPER.DataCall> DataCalls { get; set; }

    }
}