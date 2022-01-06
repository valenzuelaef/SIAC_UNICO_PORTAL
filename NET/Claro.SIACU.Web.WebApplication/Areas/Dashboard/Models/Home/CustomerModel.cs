using System.Collections.Generic;
using HELPER_DATA = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.CustomerHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models
{

    public class DataCustomerModel
    {
        public string Namelength { get; set; } 
        public string LastNamelength { get; set; } 

        public DataCustomerModel() {
            listDataCustomerModel = new List<HELPER_DATA.Customer>();
            listDataCustomerModelPrepaid = new List<HELPER_DATA.Customer>();
            listDataCustomerModelPostpaid = new List<HELPER_DATA.Customer>();
            ListOptionalObjectCustomer = new List<HELPER_DATA.OptionalObjectCustomer>();
        }
        public List<HELPER_DATA.Customer> listDataCustomerModel { get; set; }
        public List<HELPER_DATA.Customer> listDataCustomerModelPrepaid { get; set; }
        public List<HELPER_DATA.Customer> listDataCustomerModelPostpaid { get; set; }
        public List<HELPER_DATA.OptionalObjectCustomer> ListOptionalObjectCustomer { get; set; }
    }


}