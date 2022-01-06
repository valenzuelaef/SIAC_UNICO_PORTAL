using System.Collections.Generic;
using HELPER_RELOAD = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid
{
    public class DataReloadModel
    {
        public DataReloadModel() { }

        public List<HELPER_RELOAD.DataReload> listDataReloadModel { get; set; }
        public string ReloadTelephoneCustomer { get; set; }
        public string ReloadDateFrom { get; set; }
        public string ReloadDateTo { get; set; }
        public string Transaction { get; set; }
        public string Headers { get; set; }
    }
}