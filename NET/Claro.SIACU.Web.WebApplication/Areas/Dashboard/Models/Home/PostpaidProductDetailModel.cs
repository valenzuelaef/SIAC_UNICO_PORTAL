using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.PostpaidProductDetail;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models
{
    public class PostpaidProductDetailModel
    {

        public PostpaidProductDetailModel()
        {
            listProducDetailPost = new List<DetailProduct>();
            StateLines = "";
            TypeProduct = ""; 
        }

        public string StateLines { get; set; }
        public string TypeProduct { get; set; } 
        public List<DetailProduct> listProducDetailPost { get; set; } 
    }
}