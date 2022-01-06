using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch
{
    public class DocumentCustomerModel : SearchCustomerModel
    {
        public List<PostPaidServiceModel> ListPostPaidService { get; set; }
        public List<PrePaidServiceModel> ListPrePaidService { get; set; }
    }
}