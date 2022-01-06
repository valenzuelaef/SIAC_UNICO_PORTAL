using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.FixedChargeHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingFixedChargeTimProBagDetailModel
    {
        public List<HELPER_DATA.FixedChargeTimProBagDetail> listFixedChargeTimProBagDetail { get; set; }
    }
   
}