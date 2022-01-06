using System.Collections.Generic;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataReload
{
    public class OnlineReloadModel
    {
        public List<HELPER_ITEM.DataReload> DataCalls { get; set; }
        public List<HELPER_ITEM.OnlineHelper.EventType> EventTypes { get; set; }
    }
}