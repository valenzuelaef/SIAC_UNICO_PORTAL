
using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.LoanRental
{
    public class LoanRentalNoClarify
    {
        [Header(Title = "Razón Social", Order = Claro.Constants.NumberZero)]
        public string Businessname { get; set; }
        [Header(Title = "RUC", Order = Claro.Constants.NumberOne)]
        public string RUC { get; set; }
        [Header(Title = "Dirección", Order = Claro.Constants.NumberTwo)]
        public string address { get; set; }
        [Header(Title = "Modelo", Order = Claro.Constants.NumberThree)]
        public string Model { get; set; }
        [Header(Title = "IMEI", Order = Claro.Constants.NumberFour)]
        public string IMEI { get; set; }
        [Header(Title = "Motivo SAP", Order = Claro.Constants.NumberFive)]
        public string reasonSAP { get; set; }
        [Header(Title = "Modalidad SAP", Order = Claro.Constants.NumberSix)]
        public string modalitySAP { get; set; }
        [Header(Title = "Fecha", Order = Claro.Constants.NumberEight)]
        public string Date { get; set; }
        [Header(Title = "Nro. Pedido", Order = Claro.Constants.NumberNine)]
        public string NroOrder { get; set; }
        [Header(Title = "Denominación", Order = Claro.Constants.NumberTen)]
        public string Denomination { get; set; }
        [Header(Title = "Valor Neto", Order = Claro.Constants.NumberTwelve)]
        public string networth { get; set; }
    }
}