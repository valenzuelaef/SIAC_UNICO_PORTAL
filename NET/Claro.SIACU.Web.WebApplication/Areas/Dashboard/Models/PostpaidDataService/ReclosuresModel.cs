using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class ReclosuresModel
    {
        public List<Helpers.Postpaid.Reclosures.Reclosures> ManagementOfClosures { get; set; }
        public List<Helpers.Postpaid.Reclosures.Reclosures> ReopenOfTheCases { get; set; }
    }
}