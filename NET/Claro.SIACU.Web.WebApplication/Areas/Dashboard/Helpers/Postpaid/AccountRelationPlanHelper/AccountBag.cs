using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHelper
{
    public class AccountBag : IExcel
    {
        [Header(Title = "BagType", Order = Claro.Constants.NumberZero)]
        public string BagType { get; set; }
        [Header(Title = "FixedCharge", Order = Claro.Constants.NumberOne)]
        public string FixedCharge { get; set; }
        [Header(Title = "ActivationDate", Order = Claro.Constants.NumberTwo)]
        public string ActivationDate { get; set; }
        [Header(Title = "NumberLines", Order = Claro.Constants.NumberThree)]
        public string NumberLines { get; set; }
        [Header(Title = "MinSol", Order = Claro.Constants.NumberFour)]
        public string MinSol { get; set; }
        public string Account { get; set; }
        public int FlagBagService { get; set; }  
    }
}