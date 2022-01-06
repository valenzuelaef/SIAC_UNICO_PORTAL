using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountPinPukHelper
{
    public class AccountPinPuk : IExcel
    {
        [Header(Title = "N°", Order = Claro.Constants.NumberZero)]
        public int Nro { get; set; }

        [Header(Title = "ICC ID", Order = Claro.Constants.NumberOne)]
        public string ICC_IC { get; set; }

        [Header(Title = "IMSI", Order = Claro.Constants.NumberTwo)]
        public string IMSI { get; set; }

        [Header(Title = "TELEFONO", Order = Claro.Constants.NumberThree)]
        public string MSISDN { get; set; }

        [Header(Title = "PIN1", Order = Claro.Constants.NumberFour)]
        public string PIN1 { get; set; }

        [Header(Title = "PUK1", Order = Claro.Constants.NumberFive)]
        public string PUK1 { get; set; }

    }
}