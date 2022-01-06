using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccountHR
{
    public class CollectionStatusAccountHR : IExcel
    {

        public int CorrelativeNumber { get; set; }

        [Header(Title = "Detalle", Order = Claro.Constants.NumberZero)]
        public string Type { get; set; }

        [Header(Title = "Documento", Order = Claro.Constants.NumberOne)]
        public string Document { get; set; }
        [Header(Title = "Factura Asociada", Order = Claro.Constants.NumberTwo)]
        public string InvoiceAssociated
        {
            get;
            set;
        }
        [Header(Title = "Descripción Pago", Order = Claro.Constants.NumberTwo)]
        public string DescriptionPaid { get; set; }

        [Header(Title = "Fecha Registro", Order = Claro.Constants.NumberThree)]
        public string DateRegister { get; set; }

        [Header(Title = "Fecha Emisión", Order = Claro.Constants.NumberFour)]
        public string DateIssue { get; set; }

        [Header(Title = "Fecha Vencimiento", Order = Claro.Constants.NumberFive)]
        public string DateDue { get; set; }

        [Header(Title = "Cargo", Order = Claro.Constants.NumberSix)]
        public string Charge { get; set; }

        [Header(Title = "Abono", Order = Claro.Constants.NumberSeven)]
        public string Payment { get; set; }

        [Header(Title = "Importe Pendiente", Order = Claro.Constants.NumberEight)]
        public string ImportPending { get; set; }
     
        public string AmountClaimed { get; set; }

        [Header(Title = "Saldo", Order = Claro.Constants.NumberTen)]
        public string Balance { get; set; }

    }
}