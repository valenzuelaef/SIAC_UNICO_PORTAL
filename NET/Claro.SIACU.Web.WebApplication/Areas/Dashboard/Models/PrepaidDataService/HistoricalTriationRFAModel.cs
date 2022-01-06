using Claro.Helpers;
using System.Collections.Generic;
using HELPER_TRIATION = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Prepaid.HistoricalTriationRFAHerlper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class HistoricalTriationRFAModel
    {
        [Header(Title = "Telephone")]
        public string Telephone { get; set; }
        [Header(Title = "DateFrom")]
        public string DateFrom { get; set; }
        [Header(Title = "DateTo")]
        public string DateTo { get; set; }
        [Header(Title = "PlanTariff")]
        public string PlanTariff { get; set; }
        [Header(Title = "Option", Order = Claro.Constants.NumberThree)]
        public string Option { get; set; }
        [Header(Title = "Transaction", Order = Claro.Constants.NumberThree)]
        public string Transaction { get; set; }
        [Header(Title = "Date", Order = Claro.Constants.NumberThree)]
        public string Date { get; set; }
        [Header(Title = "Applicative", Order = Claro.Constants.NumberThree)]
        public string Applicative { get; set; }
        [Header(Title = "objStatusAccountHR")]
        public HELPER_TRIATION.HistoricalTriationRFA objStatusAccountHR { get; set; }
        [Header(Title = "lstHistoricalTriationRFA")]
        public List<HELPER_TRIATION.HistoricalTriationRFA> lstHistoricalTriationRFA { get; set; }
        [Header(Title = "lstNumbersTriationModel")]
        public List<NumbersTriationModel> lstNumbersTriationModel { get; set; }
    }
}