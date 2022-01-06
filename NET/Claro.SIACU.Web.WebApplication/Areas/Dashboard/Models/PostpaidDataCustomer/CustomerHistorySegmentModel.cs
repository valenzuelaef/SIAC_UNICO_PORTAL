using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CustomerHistorySegment;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCustomer
{
    public class CustomerHistorySegmentModel
    {
        public List<CustomerHistorySegment> lstCustomerHistorySegment { get; set; }


        public CustomerHistorySegmentModel() {
            lstCustomerHistorySegment = new List<CustomerHistorySegment>();
        }
    }
}