using System.Collections.Generic;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataCallHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataCall
{
    public class OnlineCallModel
    {
        public List<HELPER_ITEM.DataCall> DataCalls { get; set; }
        public List<HELPER_ITEM.InLineHelper.EventType> EventTypes { get; set; }
    }
}