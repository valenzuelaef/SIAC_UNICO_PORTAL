using Claro.Helpers;
using System.Collections.Generic;
using STATUSACCOUNTLDI = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccountLDI;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class CollectionStatusAccountLDIModel : IExcel
    {
        [Header(Title = "ClientId")]
        public string ClientId { get; set; }
        [Header(Title = "NameClient")]
        public string NameClient { get; set; }
        [Header(Title = "NumberServices")]
        public string NumberServices { get; set; }


        [Header(Title = "objStatusAccountLDI")]
        public STATUSACCOUNTLDI.CollectionStatusAccountLDI objStatusAccountLDI { get; set; }

        [Header(Title = "listBillStatusAccountLDIModel")]
        public List<STATUSACCOUNTLDI.CollectionStatusAccountLDI> listBillStatusAccountLDIModel { get; set; }

        [Header(Title = "listPaymentStatusAccountLDIModel")]
        public List<STATUSACCOUNTLDI.CollectionStatusAccountLDIPayment> listPaymentStatusAccountLDIModel { get; set; }
    }
}