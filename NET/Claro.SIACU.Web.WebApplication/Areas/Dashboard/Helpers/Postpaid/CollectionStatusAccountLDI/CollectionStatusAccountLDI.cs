using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccountLDI
{
    public class CollectionStatusAccountLDI : IExcel
    {

        [Header(Title = "N°", Order = Claro.Constants.NumberZero)]
        public int N { get; set; }

        [Header(Title = "Detalle", Order = Claro.Constants.NumberOne)]
        public string Detalle { get; set; }

        [Header(Title = "Factura Claro Asociada", Order = Claro.Constants.NumberTwo)]
        public string ClaroBillId { get; set; }

        [Header(Title = "Factura LDI", Order = Claro.Constants.NumberThree)]
        public string LdiBillId { get; set; }

        [Header(Title = "Operador Larga Distancia", Order = Claro.Constants.NumberFour)]
        public string OperatorShort { get; set; }

        [Header(Title = "Fecha Registro", Order = Claro.Constants.NumberFive)]
        public string RegistryDate { get; set; }

        [Header(Title = "Fecha Emisión", Order = Claro.Constants.NumberSix)]
        public string EmittedDate { get; set; }

        [Header(Title = "Fecha de Vencimiento", Order = Claro.Constants.NumberSeven)]
        public string MaturityDate { get; set; }

        [Header(Title = "Monto Facturado", Order = Claro.Constants.NumberEight)]
        public double BillTotal { get; set; }

        [Header(Title = "Monto no Exigible", Order = Claro.Constants.NumberNine)]
        public double AmountNotRequired { get; set; }

        [Header(Title = "Importe Pendiente", Order = Claro.Constants.NumberTen)]
        public double ImportePend { get; set; }

        [Header(Title = "Transferido para Cobranza", Order = Claro.Constants.NumberEleven)]
        public string Status { get; set; }
    }
}