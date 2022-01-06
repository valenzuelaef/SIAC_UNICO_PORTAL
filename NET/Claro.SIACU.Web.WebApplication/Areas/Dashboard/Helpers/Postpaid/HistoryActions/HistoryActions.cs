using System;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoryActions
{
    public class HistoryActions
    {
        public int Nro { get; set; }
        public int Contract { get; set; }
        public string Description { get; set; }
        public DateTime DateOrder { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Service { get; set; }
        public string User { get; set; }
    }
}