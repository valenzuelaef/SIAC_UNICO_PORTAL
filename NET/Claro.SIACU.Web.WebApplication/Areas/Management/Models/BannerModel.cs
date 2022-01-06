using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Areas.Management.Helpers.BannerHelper;


namespace Claro.SIACU.Web.WebApplication.Areas.Management.Models
{
    public class BannerModel
    {
        public List<HelperItem.Banner> salida { get; set; }
        public HelperItem.Banner Banner { get; set; }

    }
}