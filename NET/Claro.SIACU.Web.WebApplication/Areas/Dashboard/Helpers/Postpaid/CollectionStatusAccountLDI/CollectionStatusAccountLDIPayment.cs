using Claro.Helpers;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccountLDI
{
    public class CollectionStatusAccountLDIPayment : IExcel
    {

        [Header(Title = "N°", Order = Claro.Constants.NumberZero)]
        public int N { get; set; }

        [Header(Title = "Detalle", Order = Claro.Constants.NumberOne)]
        public string Payment { get; set; }


        [Header(Title = "Nº Ref", Order = Claro.Constants.NumberTwo)]
        public string ReferenceNumber { get; set; }

        [Header(Title = "Factua Claro Asociada", Order = Claro.Constants.NumberThree)]
        public string ClaroBillId { get; set; }

        [Header(Title = "Factura LDI", Order = Claro.Constants.NumberFour)]
        public string LdiBillId { get; set; }

        [Header(Title = "Operador Larga Distancia", Order = Claro.Constants.NumberFive)]
        public string OperatorShort { get; set; }

        [Header(Title = "Descripción Pago", Order = Claro.Constants.NumberSix)]
        public string BankDesc { get; set; }

        [Header(Title = "Fecha Registro", Order = Claro.Constants.NumberSeven)]
        public string RegistryDate { get; set; }

         [Header(Title = "Fecha Pago", Order = Claro.Constants.NumberEight)]
        public string PaymentDate { get; set; }

        [Header(Title = "Importe Pagado", Order = Claro.Constants.NumberNine)]
        public double AmountPen { get; set; }

     
    }
}