using System;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ConsumptionBalanceHelper
{
    public class Service
    {
        public string IdCode { get; set; }
        public string Campaign { get; set; }
        public DateTime DateStatus { get; set; }
        public string StateLine { get; set; }
        public string Reason { get; set; }
        public string Plan { get; set; }
        public string TermContract { get; set; }
        public string NumberIccid { get; set; }
        public string NumberImsi { get; set; }
        public string Seller { get; set; }
        public string ActivationDate { get; set; }
        public string OffDate { get; set; }
        public string FlagPlatform { get; set; }
        public string Pin1 { get; set; }
        public string Pin2 { get; set; }
        public string Puk1 { get; set; }
        public string Puk2 { get; set; }
        public string CodeRatePlan { get; set; }
        public string RatePlan { get; set; }
        public string CellPhoneNumber { get; set; }
        public string IdContract { get; set; }
        public string ReturnCode { get; set; }
        public string IdProvider { get; set; }
        public string MSISDN { get; set; }
        public string MobileBanking { get; set; }
        public string TypeSolution { get; set; }
        public string ProductType { get; set; }
        public string TopConsumption { get; set; }
        public string Telephony { get; set; }
        public string Internet { get; set; }
        public string CableTv { get; set; }
        public string Application { get; set; }
        public string ServicePackage { get; set; }
        public bool StateServicePackage { get; set; }
        public string FlagTfi { get; set; }
        public string Tfi { get; set; }
        public bool Estado_Tfi { get; set; }
        public string Sale { get; set; }
        public string ServiceCombo { get; set; }
        public bool StateServiceCombo { get; set; }
        public DateTime Introduced { get; set; }
        public string IntroducedBy { get; set; }
        public DateTime ValidFrom { get; set; }
        public string Changed { get; set; }
        public string ItIsNot3Play { get; set; }
        public string Package { get; set; }
        public string IdPackage { get; set; }
        public string Share { get; set; }
        public string PrincipalBalance { get; set; }
        public string ExpirationDateBalance { get; set; }
        public string FreeTriosChanges { get; set; }
        public string FreeRateChanges { get; set; }
        public string DateDol { get; set; }
        public string ExpirationDateLine { get; set; }
        public string SubscribeState { get; set; }
        public string CntNumber { get; set; }
        public string CntPossible { get; set; }
        public string StateImsi { get; set; }
        public string TreasonType { get; set; }
        public string NumberFamilyFriends { get; set; }
        public string ResponseCode { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Trio> Trios { get; set; }
    }
}