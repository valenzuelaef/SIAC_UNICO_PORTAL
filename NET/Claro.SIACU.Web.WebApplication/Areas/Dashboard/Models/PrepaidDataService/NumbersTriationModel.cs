using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class NumbersTriationModel
    {
        public NumbersTriationModel() { }

        [Header(Title = "Trío 1", Order = Claro.Constants.NumberZero)]
        public string Description { get; set; }
        [Header(Title = "Trío 2", Order = Claro.Constants.NumberOne)]
        public string Description2 { get; set; }
        [Header(Title = "Trío 3", Order = Claro.Constants.NumberTwo)]
        public string Description3 { get; set; }
        [Header(Title = "Trío 4", Order = Claro.Constants.NumberThree)]
        public string Description4 { get; set; }
        [Header(Title = "Trío 5", Order = Claro.Constants.NumberFour)]
        public string Description5 { get; set; }
        [Header(Title = "Trío 6", Order = Claro.Constants.NumberFive)]
        public string Description6 { get; set; }
        [Header(Title = "Trío 7", Order = Claro.Constants.NumberSix)]
        public string Description7 { get; set; }
        [Header(Title = "Trío 8", Order = Claro.Constants.NumberSeven)]
        public string Description8 { get; set; }
        [Header(Title = "Trío 9", Order = Claro.Constants.NumberEight)]
        public string Description9 { get; set; }
        [Header(Title = "Trío 10", Order = Claro.Constants.NumberNine)]
        public string Description10 { get; set; }
        [Header(Title = "Monto a Cobrar", Order = Claro.Constants.NumberTen)]
        public string Amount { get; set; }
    }
}