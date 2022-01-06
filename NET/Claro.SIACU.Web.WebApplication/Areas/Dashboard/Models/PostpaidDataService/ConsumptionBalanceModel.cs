using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ConsumptionBalanceHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ConsumptionBalanceModel
    {
        public List<HelperItem.Recharge> Recharges { get; set; }
        public List<HelperItem.Service> Services { get; set; }
        public HelperItem.Service Service { get; set; }
        public string Title { get; set; }
        public string Telephone { get; set; }
        public string Message { get; set; }
        public string MessageRecharges { get; set; }
        public string MessageRechargesM2M { get; set; }
        public string ActivationDate { get; set; }
        public bool MessageRechargesVisible { get; set; }
        public bool MessageRechargesM2MVisible { get; set; }
        public bool MessageVisible { get; set; }
        public string ColorStateLine { get; set; }
        
    }
}