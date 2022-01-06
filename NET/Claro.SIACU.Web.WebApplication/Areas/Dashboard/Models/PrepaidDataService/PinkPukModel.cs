using System.Collections.Generic;
using HELPER_PINPUK = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.PinkPukHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class PinkPukModel
    {
        public HELPER_PINPUK.PinPuk objPinpukData { get; set; }
        public List<HELPER_PINPUK.PinPuk> listPinpukData { get; set; }

        public string strTelephone { get; set; }
        public string Answer { get; set; }

    }
    
}