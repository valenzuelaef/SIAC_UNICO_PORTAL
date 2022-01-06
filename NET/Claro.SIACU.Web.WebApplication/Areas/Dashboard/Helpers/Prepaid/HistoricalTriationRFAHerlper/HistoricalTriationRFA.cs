using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.HistoricalTriationRFAHerlper
{
    public class HistoricalTriationRFA : IExcel
    {
        public string Interaction { get; set; }
        [Header(Title = "Opción Triación", Order = Claro.Constants.NumberZero)]
        public string Option { get; set; }
        [Header(Title = "Transacción", Order = Claro.Constants.NumberOne)]
        public string Transaction { get; set; }
        [Header(Title = "Fecha Registro", Order = Claro.Constants.NumberTwo)]
        public string Date { get; set; }
        [Header(Title = "Aplicativo", Order = Claro.Constants.NumberThree)]
        public string Applicative { get; set; }
        public int Accountant { get; set; }
    }
}