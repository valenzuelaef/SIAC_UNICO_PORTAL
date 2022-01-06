using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.ServicePostpaid;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models
{
    public class PostpaidProductModel
    {
        public PostpaidProductModel()
        {         
        }
        public string DataOrigin { get; set; }
        public List<ServicePostpaid> olistProducPost { get; set; }  
    }

}