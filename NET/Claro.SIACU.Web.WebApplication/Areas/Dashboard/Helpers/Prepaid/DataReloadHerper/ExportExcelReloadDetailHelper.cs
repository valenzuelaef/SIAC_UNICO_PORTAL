using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.DataReloadHerper
{
    
    public class ExportExcelReloadDetailHelper
    {
        [Header(Title = "Fecha Hora", Order = Claro.Constants.NumberZero)]
        public string DateTimeOperation { get; set; }

        [Header(Title = "Tipo de Movimiento", Order = Claro.Constants.NumberOne)]
        public string MovementType { get; set; }

        [Header(Title = "Tipo Recarga", Order = Claro.Constants.NumberTwo)]
        public string kindReload { get; set; }

        [Header(Title = "Monto", Order = Claro.Constants.NumberThree)]
        public string CashAmount { get; set; }

        [Header(Title = "Saldo", Order = Claro.Constants.NumberFour)]
        public string Balance { get; set; }

        [Header(Title = "Crédito/Débito", Order = Claro.Constants.NumberFive)]
        public string Credit { get; set; }

        [Header(Title = "Bolsa", Order = Claro.Constants.NumberSix)]
        public string Bag { get; set; }

        [Header(Title = "Tipo de Saldo", Order = Claro.Constants.NumberSeven)]
        public string BalanceType { get; set; }

        [Header(Title = "Descripción", Order = Claro.Constants.NumberEight)]
        public string Description { get; set; }

        [Header(Title = "Detalle", Order = Claro.Constants.NumberNine)]
        public string Detail { get; set; }

    }
}