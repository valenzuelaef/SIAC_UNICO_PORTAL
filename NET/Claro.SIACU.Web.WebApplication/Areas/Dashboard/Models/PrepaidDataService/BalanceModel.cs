
using Claro.Helpers;
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PrepaidDataService
{
    public class BalanceModel : IExcel
    {
        [Header(Title = "Grupo/Bono/Promoción", Order = Claro.Constants.NumberZero)]
        public string Name { get; set; }
        [Header(Title = "Nombre Comercial de la Bolsa", Order = Claro.Constants.NumberOne)]
        public string CommercialName { get; set; }
        [Header(Title = "Destino", Order = Claro.Constants.NumberTwo)]
        public string Destination { get; set; }
        [Header(Title = "Saldo", Order = Claro.Constants.NumberThree)]
        public string Balance { get; set; }
        [Header(Title = "Unidad", Order = Claro.Constants.NumberFour)]
        public string Unity { get; set; }
        [Header(Title = "Vencimiento", Order = Claro.Constants.NumberFive)]
        public string Expiration { get; set; }
        [Header(Title = "Comprado/Regalado", Order = Claro.Constants.NumberSix)]
        public string NameCode { get; set; }
        public string Order { get; set; }
        public string MovementTypeName { get; set; }    
        public string PromotionalBonus { get; set; }
        public string ConstancyOrder { get; set; }
        public string UnityIndicator { get; set; }
        public string OtherIndicator { get; set; }
    }
}