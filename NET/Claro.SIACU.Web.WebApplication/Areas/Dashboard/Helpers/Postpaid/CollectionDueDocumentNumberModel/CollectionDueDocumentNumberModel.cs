using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionDueDocumentNumberModel
{
    public class CollectionDueDocumentNumberModel : IExcel
    {
       
        public int CorrelativeNumber { get; set; }

        [Header(Title = "Código Cliente", Order = Claro.Constants.NumberZero)]
        public string IdAccount { get; set; }

        [Header(Title = "Estado Cuenta", Order = Claro.Constants.NumberOne)]
        public string StatusAccount { get; set; }

        [Header(Title = "Promedio Facturado", Order = Claro.Constants.NumberTwo)]
        public string AverageInvoiced { get; set; }

        [Header(Title = "Deuda Corriente", Order = Claro.Constants.NumberThree)]
        public string CurrentDue { get; set; }

        [Header(Title = "Deuda Vencida", Order = Claro.Constants.NumberFour)]
        public string ExpiredDue { get; set; }

        [Header(Title = "Deuda Castigada", Order = Claro.Constants.NumberFive)]
        public string PunishedDue { get; set; }

        [Header(Title = "Cant.Servicios", Order = Claro.Constants.NumberSix)]
        public string AccountServices { get; set; }

        [Header(Title = "Central Riesgos", Order = Claro.Constants.NumberSeven)]
        public string RiskCenter { get; set; }

        [Header(Title = "Tipo Cuenta", Order = Claro.Constants.NumberEight)]
        public string TypeService { get; set; }

        [Header(Title = "NameClient", Order = Claro.Constants.NumberNine)]
        public string NameClient { get; set; }

        [Header(Title = "ReferenceDocument", Order = Claro.Constants.NumberTen)]
        public string ReferenceDocument { get; set; }

        [Header(Title = "MobileDue", Order = Claro.Constants.NumberEleven)]
        public decimal? MobileDue { get; set; }

        [Header(Title = "FixedDue", Order = Claro.Constants.NumberTwelve)]
        public decimal? FixedDue { get; set; }

        [Header(Title = "OverdueMobileDue", Order = Claro.Constants.NumberThirteen)]
        public decimal? OverdueMobileDue { get; set; }

        [Header(Title = "OverdueFixedDue", Order = Claro.Constants.NumberFourteen)]
        public decimal? OverdueFixedDue { get; set; }

        [Header(Title = "PunishedMobileDue", Order = Claro.Constants.NumberFifteen)]
        public decimal? PunishedMobileDue { get; set; }

        [Header(Title = "PunishedFixedDue", Order = Claro.Constants.NumberSixteen)]
        public decimal? PunishedFixedDue { get; set; }

        [Header(Title = "AntiquityDue", Order = Claro.Constants.NumberSeventeen)]
        public decimal? AntiquityDue { get; set; }

        [Header(Title = "AllServices", Order = Claro.Constants.NumberEighteen)]
        public decimal? AllServices { get; set; }

        [Header(Title = "NumberDocument", Order = Claro.Constants.NumberNineteen)]
        public string NumberDocument { get; set; }
    }
}