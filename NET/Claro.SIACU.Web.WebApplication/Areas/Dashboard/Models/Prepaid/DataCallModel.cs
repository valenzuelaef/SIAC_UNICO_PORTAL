using System.Collections.Generic;
using HELPER_CALL = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataCallHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid
{
    public class DataCallModel
    {
       

        public List<HELPER_CALL.DataCall> listDataCallModel { get; set; }
        public string CallTelephoneCustomer { get; set; }
        public string DateFromCall { get; set; }
        public string DateToCall { get; set; }
        public string Transaction { get; set; }
        public string Headers { get; set; }
    }
}