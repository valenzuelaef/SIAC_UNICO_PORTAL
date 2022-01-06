using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ConsumptionRechargeModel
    {
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ConsumptionType.ConsumptionTypeHelper> lstConsumptionTypeHelper { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ConsumptionRecharge.ConsumptionRechargeHelper> lstConsumptionRechargeHelper { get; set; }
        public string strMensajeAlert { get; set; }
    }
}