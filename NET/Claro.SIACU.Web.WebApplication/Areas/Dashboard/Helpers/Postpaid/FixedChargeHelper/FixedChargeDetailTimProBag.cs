
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.FixedChargeHelper
{
    public class FixedChargeDetailTimProBag
    {
        public FixedChargeDetailTimProBag()
        {
            
            RatePlan = "";
            FromDate = "";
            ToDate = "";
            Fu1 = "";
            Fu2 = "";
            Fu3 = "";
            Amount = Claro.Constants.NumberZero;
            Quantity = Claro.Constants.NumberZero;
        }
       
        public string RatePlan { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Fu1 { get; set; }
        public string Fu2 { get; set; }
        public string Fu3 { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
    }
}
