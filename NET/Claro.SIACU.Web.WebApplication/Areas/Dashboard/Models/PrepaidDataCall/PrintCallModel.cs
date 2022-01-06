using System.Collections.Generic;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataCall
{
    public class PrintCallModel
    {
        public PrintCallModel() { }

        public string DetCallTelephone { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Headers { get; set; }
        public List<Helpers.Prepaid.DataCallHelper.DataCall> listCallDetailModel { get; set; }

    }
}