using System.Collections.Generic;
using DECO_MODELHELPERS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DecoHelper;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class DecosModel
    {
        public List<DECO_MODELHELPERS.Deco> Decos { get; set; }
    }
}