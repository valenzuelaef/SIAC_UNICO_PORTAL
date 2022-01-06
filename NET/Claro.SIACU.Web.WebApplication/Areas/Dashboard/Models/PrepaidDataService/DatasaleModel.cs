
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class DataSaleModel
    {
        public string Telephone { get; set; }
        public string PointSale { get; set; }
        public string Seller { get; set; }
        public string Bell { get; set; }
        public string DatePurchase { get; set; }
        public string IMEI { get; set; }
        public string ItemCode { get; set; }
        public string BrandModel { get; set; }
        public string DescriptionSale { get; set; }        

        public DataSalePELModel objDataSalePELModel { get; set; }

        public DataSaleModel()
        {
            objDataSalePELModel = new DataSalePELModel();
        }
    }
}