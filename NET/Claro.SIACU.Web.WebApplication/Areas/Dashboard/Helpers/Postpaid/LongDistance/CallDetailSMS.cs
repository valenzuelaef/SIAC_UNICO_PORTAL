
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LongDistance
{
    public class CallDetailSMS
    {
        public CallDetailSMS()
        {
            SMSNUMBER = "";
            SMSDATE = "";
            SMSTIME = "";
            SMSTOTAL = Claro.Constants.NumberZero;
            SMSDESTINATION = "";

        }
        public string SMSNUMBER { get; set; }
        public string SMSDATE { get; set; }
        public string SMSTIME { get; set; }
        public decimal SMSTOTAL { get; set; }
        public string SMSDESTINATION { get; set; }
    }
}