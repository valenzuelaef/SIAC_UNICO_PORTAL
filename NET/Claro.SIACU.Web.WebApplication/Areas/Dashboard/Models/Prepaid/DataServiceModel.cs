using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid
{
    public class DataServiceModel
    {
        public DataServiceModel() { }

        public string NumberCellphone { get; set; }
        public string StateLine { get; set; }
        public string Portability { get; set; }
        public string ProviderID { get; set; }
        public string DaysExpiration { get; set; }
        public string PlanRate { get; set; }
        public string DateActivation { get; set; }
        public string DateExpirationLine { get; set; }
        public string Technology4G { get; set; }
        public string PromotionFidelity { get; set; }
        public string ICCID { get; set; }
        public string BankingMobile { get; set; }
        public string BalancePrincipal { get; set; }
        public string DateExpirationBalance { get; set; }
        public string IntentReloadDefaulted { get; set; }
        public string ChangeRatesDone { get; set; }
        public string QuantityTrios { get; set; }
        public string TypeTriation { get; set; }
        public string HistoricalStriations { get; set; }
        public string HistoricalBonds { get; set; }
        public string BondRFA { get; set; }
        public string BondAmountRFA { get; set; }
        public string State { get; set; }
        public string Reason { get; set; }
        public bool IsTFI { get; set; }
        public string Payment { get; set; }
        public string FailedAttemptsRefills { get; set; }
        public string ExchangeRatesMade { get; set; }
        public string Answer { get; set; }
        public string StatusIMSI { get; set; }
        public string StateLine_2 { get; set; }
        public string ReasonBlocking { get; set; }
        public string AlertBlocking { get; set; }
        public string Segment { get; set; }
        public string DescriptionTypeTriation { get; set; }
        public string StateDays { get; set; }
        public string StateSubscriber { get; set; }
        public string BalancePending { get; set; }
        public string PlanTariff { get; set; }
        public string SubscriberStatus { get; set; }
        public string BalanceMinutesSelect { get; set; }
        public string DateExpirationSelect { get; set; }
        public string IsSelect { get; set; }
        public string CNTNumber { get; set; }
        public string IsCNTPossible { get; set; }
        public string NumberIMSI { get; set; }
        public double Balance { get; set; }
        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.PortabilityModel oPortability { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.AccountModel> listAccount { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.BalanceModel> listBalance { get; set; }
        public List<Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService.TrioModel> listTrio { get; set; }

        public string NumberFamFriends { get; set; }
        public string Plan { get; set; }
        public string FlagPlatform { get; set; }
        public string FlagLoadDataService { get; set; }
        public string IsDTH { get; set; }
        public string LeaveDate { get; set; }

        public string Transaction { get; set; }

        public string ContactType { get; set; }

        public string TechnologyVoLTE { get; set; }
        public string TechnologyVoWifi { get; set; }
        public string RateMB { get; set; }
    }
}