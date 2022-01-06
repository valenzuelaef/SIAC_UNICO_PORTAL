using Claro.Helpers;
using System;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccount
{
    public class CollectionStatusAccount : IExcel
    {

        [Header(Title = "Nro", Order = Claro.Constants.NumberZero)]
        public int CorrelativeNumber { get; set; }

        [Header(Title = "Tipo", Order = Claro.Constants.NumberOne)]
        public string Type { get; set; }

        [Header(Title = "Documento", Order = Claro.Constants.NumberTwo)]
        public string Document { get; set; }

        public string Ajuste { get; set; }

        [Header(Title = "Descripción Pago", Order = Claro.Constants.NumberThree)]
        public string DescriptionPaid { get; set; }

        [Header(Title = "Fecha Registro", Order = Claro.Constants.NumberFour)]
        public string DateRegister { get; set; }

        [Header(Title = "Fecha Emisión", Order = Claro.Constants.NumberFive)]
        public string DateIssue { get; set; }

        [Header(Title = "Fecha Vencimiento", Order = Claro.Constants.NumberSix)]
        public string DateDue { get; set; }

        [Header(Title = "Cargo", Order = Claro.Constants.NumberSeven)]
        public string Charge { get; set; }

        [Header(Title = "Abono", Order = Claro.Constants.NumberEight)]
        public string Payment { get; set; }

        [Header(Title = "Importe Pendiente", Order = Claro.Constants.NumberNine)]
        public string ImportPending { get; set; }

        [Header(Title="Monto Reclamado",Order=Claro.Constants.NumberTen)]
        public string AmountClaimed { get; set; }

        [Header(Title = "Saldo", Order = Claro.Constants.NumberEleven)]
        public string Balance { get; set; }

        public string ClientId { get; set; }

        public string NameClient { get; set; }

        public string NumberServices { get; set; }

       
        public DateTime DateRegisterTime { get; set; }

        public string DateIssueTime { get; set; }

        public string DateDueTime { get; set; }

        public string IdOnBase { get; set; }

        public string IsInvoceCancelable { get; set; }

        public string StatusDocument { get; set; }
    }
}