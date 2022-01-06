using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.DataLineService;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class DataLineServiceModel
    {
        public string Application { get; set; }
        public string ContractID { get; set; }
        public string coIdPub { get; set; }
        public List<DataLineService> lstPostServices { get; set; }
        public List<DataLineService> lstHfcServices { get; set; }
        public List<DataLineService> lstServices { get; set; }

        public DataLineServiceModel()
        {
            lstPostServices = new List<DataLineService>();
            lstHfcServices = new List<DataLineService>();
            lstServices = new List<DataLineService>();
        }
    }
}