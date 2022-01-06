using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHelper
{
    public class AccountBagDetail : IExcel
    {
        [Header(Title = "Bolsa", Order = Claro.Constants.NumberZero)]
        public string BagTypeBagDetail { get; set; }
        [Header(Title = "Cargo", Order = Claro.Constants.NumberOne)]
        public string FixedChargeBagDetail { get; set; }
        [Header(Title = "Fecha Activación", Order = Claro.Constants.NumberTwo)]
        public string ActivationDateBagDetail { get; set; }
        [Header(Title = "Cant. de lineas", Order = Claro.Constants.NumberFour)]
        public string NumberLinesBagDetail { get; set; }
        [Header(Title = "Unidades", Order = Claro.Constants.NumberThree)]
        public string MinSolBagDetail { get; set; }
    }
}