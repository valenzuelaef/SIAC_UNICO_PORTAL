using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.BillingHistoricDelivery
{
    public class HisDelivery : IExcel
    {
        [Header(Title = "RECIBO", Order = Claro.Constants.NumberZero)]
        public string RECIBO { get; set; }

        [Header(Title = "FECHA EMISION", Order = Claro.Constants.NumberOne)]
        public string FECEMISION { get; set; }

        [Header(Title = "TIPO", Order = Claro.Constants.NumberTwo)]
        public string TIPO { get; set; }

        [Header(Title = "COURIER", Order = Claro.Constants.NumberThree)]
        public string COURIER { get; set; }

        [Header(Title = "ESTADO", Order = Claro.Constants.NumberFour)]
        public string ESTADO { get; set; }

        [Header(Title = "MOTIVO", Order = Claro.Constants.NumberFive)]
        public string MOTIVO { get; set; }

        [Header(Title = "FECHA ENTREGA", Order = Claro.Constants.NumberSix)]
        public string FECENTREGA { get; set; }
        public string Url { get; set; }
    }
}