using System.Collections.Generic;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.CustomerCreateNotUseHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataCustomer
{
    public class CustomerCreateNotUserModel
    {
        public CustomerCreateNotUserModel() { }

        public List<HELPER_ITEM.CreateNotUserItem> listOccupation { get; set; }
        public List<HELPER_ITEM.CreateNotUserItem> listMotiveRegister { get; set; }
        public List<HELPER_ITEM.CreateNotUserItem> listStateCivil { get; set; }
        public List<HELPER_ITEM.CreateNotUserItem> listConfirmMail { get; set; }
        public List<HELPER_ITEM.CreateNotUserItem> listSex { get; set; }
        public List<HELPER_ITEM.CreateNotUserItem> listTypeDocumentCustomer { get; set; }
        
    }
}