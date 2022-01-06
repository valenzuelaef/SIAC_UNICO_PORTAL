using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class BalanceCBIOS
    {
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS> lstBolsaIndividual { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS> lstBolsaOnTop { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BalanceCBIOS.BalanceCBIOS> lstBolsaCorporativa { get; set; }
        public string Message { get; set; }
    }
}