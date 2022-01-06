using System;

namespace Claro.SIACU.Web.WebApplication.Areas.Management.Helpers.BannerHelper
{

    public class Banner
    {
        public int idBanner { get; set; }
        public string message { get; set; }
        public int priorityOrder { get; set; }
        public DateTime dateValidityStart { get; set; }
        public DateTime dateValidityEnd { get; set; }
        public string status { get; set; }
        public string login { get; set; }
        public string phoneType { get; set; }
        public string loginRegistry { get; set; }
        public string loginModification { get; set; }
    }
}
