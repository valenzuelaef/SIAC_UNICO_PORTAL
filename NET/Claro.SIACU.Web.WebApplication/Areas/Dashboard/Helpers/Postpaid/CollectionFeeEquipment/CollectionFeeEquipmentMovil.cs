using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionFeeEquipment
{
    public class CollectionFeeEquipmentMovil : IExcel
    {
        [Header(Title = "Nro", Order = Claro.Constants.NumberZero)]
        public int CorrelativeNumber { get; set; }

        [Header(Title = "Documento", Order = Claro.Constants.NumberOne)]
        public string Document { get; set; }

        [Header(Title = "Descripción Pago", Order = Claro.Constants.NumberTwo)]
        public string NumberSisact { get; set; }

        public string NumberFunding { get; set; }

        [Header(Title = "IMEI", Order = Claro.Constants.NumberThree)]
        public string NumberIMEI { get; set; }
        [Header(Title = "Estado", Order = Claro.Constants.NumberFour)]
        public string State { get; set; }

        [Header(Title = "Fec Emisión", Order = Claro.Constants.NumberFive)]
        public string DateIssue { get; set; }

        [Header(Title = "Fec Venc", Order = Claro.Constants.NumberSix)]
        public string DateDue { get; set; }

        [Header(Title = "Importe Original", Order = Claro.Constants.NumberSeven)]
        public string ImportOrigin { get; set; }

        [Header(Title = "Importe Pendiente", Order = Claro.Constants.NumberEight)]
        public string ImportPending { get; set; }


        public string ClientId { get; set; }

        public string NameClient { get; set; }

        public string NumberServices { get; set; }
    }
}