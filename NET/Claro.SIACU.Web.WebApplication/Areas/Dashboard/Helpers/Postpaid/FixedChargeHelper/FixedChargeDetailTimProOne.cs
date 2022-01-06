
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.FixedChargeHelper
{
    public class FixedChargeDetailTimProOne
    {
        public FixedChargeDetailTimProOne()
        {
            Msisdn = "";
            RatePlan = "";
            FromDate = "";
            ToDate = "";
            Fu1 = "";
            Fu2 = "";
            Fu3 = "";
            Fu4 = "";
            Amount = Claro.Constants.NumberZero;
            MsgTexto5 = Claro.Constants.NumberZero;
        }
        public string Msisdn { get; set; }
        public string RatePlan { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Fu1 { get; set; }
        public string Fu2 { get; set; }
        public string Fu3 { get; set; }
        public string Fu4 { get; set; }
        public decimal Amount { get; set; }
        public int MsgTexto5 { get; set; }
    }
}

