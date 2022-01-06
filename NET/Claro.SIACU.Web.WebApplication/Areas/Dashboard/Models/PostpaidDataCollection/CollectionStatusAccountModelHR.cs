using Claro.Helpers;
using System.Collections.Generic;
using STATUSACCOUNTHR = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccountHR;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class CollectionStatusAccountModelHR : IExcel
    {
        [Header(Title = "ClientId")]
        public string ClientId { get; set; }
        [Header(Title = "NameClient")]
        public string NameClient { get; set; }
        [Header(Title = "NumberServices")]
        public string NumberServices { get; set; }


        [Header(Title = "objStatusAccountHR")]
        public STATUSACCOUNTHR.CollectionStatusAccountHR objStatusAccountHR { get; set; }

        [Header(Title = "listStatusAccountModel")]
        public List<STATUSACCOUNTHR.CollectionStatusAccountHR> listStatusAccountModel { get; set; }
    }
}