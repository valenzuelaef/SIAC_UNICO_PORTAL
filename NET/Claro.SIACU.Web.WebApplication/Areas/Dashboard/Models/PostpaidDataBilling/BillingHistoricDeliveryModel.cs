using Claro.Helpers;
using System.Collections.Generic;
using HISTDELIVERY_MODELHELPERS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoricDelivery;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataBilling
{
    public class BillingHistoricDeliveryModel : IExcel
    {
        [Header(Title = "listHisDelivery")]
        public List<HISTDELIVERY_MODELHELPERS.HisDelivery> listHisDelivery { get; set; }

        [Header(Title = "TELEFONO")]
        public string TELEPHONE { get; set; }

    }
}