using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataCall
{
    public class VisualizeCallModel
    {
        public VisualizeCallModel() { }

        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataCall.GenericBagModel> listGenericBagModel { get; set; }

        public string DetCallTelephone { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string TrafficType { get; set; }

    }
}