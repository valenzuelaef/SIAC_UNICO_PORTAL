using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.HistoricalStriationHelper
{
    public class HistoricalStriation : IExcel
    {
        public string Client { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string ActivationArea { get; set; }
        public string Contract { get; set; }
        [Header(Title = "Plan", Order = Claro.Constants.NumberZero)]
        public string Plan { get; set; }
        public string TelephoneNumber { get; set; }
        [Header(Title = "Usuario", Order = Claro.Constants.NumberTwo)]
        public string User { get; set; }
        [Header(Title = "Fecha", Order = Claro.Constants.NumberThree)]
        public string Date { get; set; }
        public string Origin { get; set; }
        [Header(Title = "Destino", Order = Claro.Constants.NumberFour)]
        public string Destination { get; set; }
        [Header(Title = "Descripcion", Order = Claro.Constants.NumberFive)]
        public string Description { get; set; }
        public string State { get; set; }
        public string Reason { get; set; }
        [Header(Title = "Factor Escalado", Order = Claro.Constants.NumberSix)]
        public string Factor { get; set; }
        [Header(Title = "Modificación", Order = Claro.Constants.NumberOne)]
        public string Modification { get; set; }
    }
}