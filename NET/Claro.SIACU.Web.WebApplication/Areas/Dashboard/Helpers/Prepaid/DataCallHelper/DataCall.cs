
using Claro.Helpers;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataCallHelper
{
    public class DataCall
    {
        public DataCall() { }

        [Header(Title = "Fecha Hora", Order = Claro.Constants.NumberZero)]
        public string CallDateTime { get; set; }

        [Header(Title = "Teléfono destino", Order = Claro.Constants.NumberOne)]
        public string CallTelephoneDestination { get; set; }

        [Header(Title = "Tipo de Tráfico", Order = Claro.Constants.NumberTwo)]
        public string CallTypeTraffic { get; set; }

        [Header(Title = "Duración", Order = Claro.Constants.NumberThree)]
        public string CallDuration { get; set; }

        [Header(Title = "Consumo", Order = Claro.Constants.NumberFour)]
        public string CallUptake { get; set; }

        [Header(Title = "Comprado / Regalado", Order = Claro.Constants.NumberFive)]
        public string CallBoughtPresented { get; set; }

        [Header(Title = "Saldo", Order = Claro.Constants.NumberSix)]
        public string CallBalance { get; set; }

        [Header(Title = "Bolsa ID", Order = Claro.Constants.NumberSeven)]
        public string CallBagId { get; set; }

        [Header(Title = "Descripción", Order = Claro.Constants.NumberEight)]
        public string CallDescription { get; set; }

        [Header(Title = "Plan", Order = Claro.Constants.NumberNine)]
        public string CallPlan { get; set; }

        [Header(Title = "Promoción", Order = Claro.Constants.NumberTen)]
        public string CallPromotion { get; set; }

        [Header(Title = "Destino", Order = Claro.Constants.NumberEleven)]
        public string CallDestination { get; set; }

        [Header(Title = "Operador", Order = Claro.Constants.NumberTwelve)]
        public string CallOperator { get; set; }

        [Header(Title = "Grupo de Cobro", Order = Claro.Constants.NumberThirteen)]
        public string CallCobroGroup { get; set; }

        [Header(Title = "Tipo de Red", Order = Claro.Constants.NumberFourteen)]
        public string CallNetworkType { get; set; }

        [Header(Title = "IMEI", Order = Claro.Constants.NumberFifteen)]
        public string CallImei { get; set; }

        [Header(Title = "Roaming", Order = Claro.Constants.NumberSixteen)]
        public string CallRoaming { get; set; }

        [Header(Title = "Zona Tarifaria", Order = Claro.Constants.NumberSeventeen)]
        public string CallTariffArea { get; set; }

        public string CallStart { get; set; }

        public string CallEnd { get; set; }

        public string CallCost { get; set; }

        public string CallBag { get; set; }

        public string CallEventType { get; set; }
    }
}