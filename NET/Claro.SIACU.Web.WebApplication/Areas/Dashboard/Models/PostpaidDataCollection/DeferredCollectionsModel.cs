using Claro.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class DeferredCollectionsModel : IExcel
    {
        [Header(Title = "DeferredCollections")]
        public List<Helpers.Postpaid.DeferredCollectionsHelper.DeferredCollections> DeferredCollections { get; set; }
        [Header(Title = "TotalBilledAmount")]
        public decimal TotalBilledAmount { get; set; }
        [Header(Title = "TotalAmountNotBilled")]
        public decimal TotalAmountNotBilled { get; set; }
        [Header(Title = "NameClient")]
        public string NameClient { get; set; }
        [Header(Title = "NumberServices")]
        public string NumberServices { get; set; }
        [Header(Title = "ContratoId")]
        public string ContratoId { get; set; }
    }
}