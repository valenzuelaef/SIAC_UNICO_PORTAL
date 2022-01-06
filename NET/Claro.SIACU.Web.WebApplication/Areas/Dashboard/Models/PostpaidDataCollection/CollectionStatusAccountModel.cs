using Claro.Helpers;
using System.Collections.Generic;
using STATUSACCOUNT = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccount;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class CollectionStatusAccountModel : IExcel
    {
        [Header(Title = "ClientId")]
        public string ClientId { get; set; }
        [Header(Title = "NameClient")]
        public string NameClient { get; set; }

        [Header(Title = "NumberServices")]
        public string NumberServices { get; set; }
        [Header(Title = "ContratoId")]
        public string ContratoId { get; set; }
        [Header(Title = "NameClient2")]
        public string NameClient2 { get; set; }
         
        public bool IsEnabled { get; set; }

        [Header(Title = "objStatusAccount")]
        public STATUSACCOUNT.CollectionStatusAccount objStatusAccount { get; set; }

        [Header(Title = "listStatusAccountModel")]
        public List<STATUSACCOUNT.CollectionStatusAccount> listStatusAccountModel { get; set; }
        
        public bool blMessageStatusAccountVisible { get; set; }
        public string strMessageStatusAccount { get; set; }
    }
}