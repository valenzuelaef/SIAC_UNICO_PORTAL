using System.Collections.Generic;
using QUERYOAC_MODELHELPERS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.QueryOAC;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class QueryOACModel
    {
        public QUERYOAC_MODELHELPERS.QueryOAC QueryOAC { get; set; }
        public List<QUERYOAC_MODELHELPERS.QueryOAC> QueryOACs { get; set; }
    }
}