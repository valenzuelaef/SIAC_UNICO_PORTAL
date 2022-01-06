
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.FixedChargeHelper
{
    public class FixedChargeDetailTimPro
    {
        public FixedChargeDetailTimPro()
        {
            Msisdn = "";
            RatePlan = "";
            FromDate = "";
            ToDate = "";
            Fu1 = "";
            Fu2 = "";
            Amount = Claro.Constants.NumberZero;
        }
        public string Msisdn { get; set; }
        public string RatePlan { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Fu1 { get; set; }
        public string Fu2 { get; set; }
        public decimal Amount { get; set; }
    }
}