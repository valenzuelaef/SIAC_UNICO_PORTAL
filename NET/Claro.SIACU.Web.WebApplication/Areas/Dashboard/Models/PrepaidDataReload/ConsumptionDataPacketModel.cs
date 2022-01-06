using Claro.Helpers;
using System;
using System.Collections.Generic;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataReload
{
    public class ConsumptionDataPacketModel
    {
        [Header(Title = "ConsumptionDataPackets")]
        public List<HELPER_ITEM.ConsumptionDataPacketHelper.ConsumptionDataPacket> ConsumptionDataPackets { get; set; }

        [Header(Title = "Phone")]
        public string Phone { get; set; }
    }
}