using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ContractedBusinessServicesHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ContractedBusinessServicesModel
    {
        public List<HelperItem.PhoneContract> PhoneContracts { get; set; }
        public List<HelperItem.ContractServices> ContractServices { get; set; }
        public List<HelperItem.BSCSService> BSCSServices { get; set; }
    }
}