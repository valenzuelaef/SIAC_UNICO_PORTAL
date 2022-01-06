
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.ConsumptionBalanceHFCLTEHelper
{
    public class Balance
    {
        public long BalanceUnits { get; set; }
        public string CreditDescription { get; set; }
        public byte CreditType { get; set; }
        public string ExpiryDate { get; set; }
        public long MiniumBalance { get; set; }
        public string TarifDescription { get; set; }
        public ulong TarifId { get; set; }
        public string UnitDescription { get; set; }
        public string WalletDescription { get; set; }
        public long WalletId { get; set; }
        public string Wallet_Short_Description { get; set; }
        public string Consumo { get; set; }
        public string Balances { get; set; }
    }
}