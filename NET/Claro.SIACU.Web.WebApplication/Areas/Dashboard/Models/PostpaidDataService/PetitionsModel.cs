using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.PetitionsHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataService
{
    public class PetitionsModel
    {
        public List<HelperItem.PetitionType> PetitionTypes { get; set; }
        public List<HelperItem.Petition> Petitions { get; set; }
    }
}